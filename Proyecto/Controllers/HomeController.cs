using Newtonsoft.Json;
using Proyecto.Context;
using Proyecto.Models;
using Proyecto.Services;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Proyecto.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthorizationService _authService = new AuthorizationService();
        private ApplicationDbContext db = new ApplicationDbContext();

        // Página principal: muestra login si no hay sesión, o redirige al dashboard
        public ActionResult Index()
        {
            if (Session["Username"] != null)
            {
                return RedirectToAction("Dashboard");
            }

            return View("Login");
        }

        [Authorize]
        public ActionResult Dashboard()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Index");
            }

            // Evita que se pueda volver con el botón "Atrás" después del logout
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetNoStore();

            ViewBag.TotalEmpleados = db.Empleados.Count();
            ViewBag.TotalAusencias = db.Ausencias.Count();
            ViewBag.TotalContratos = db.Contratos.Count();
            ViewBag.Username = Session["Username"];

            // Lógica de contratos próximos a vencer
            DateTime hoy = DateTime.Today;
            DateTime limite = hoy.AddMonths(1);

            var contratosProximos = db.Contratos
                .Where(c => c.FechaFin >= hoy && c.FechaFin <= limite)
                .ToList();

            ViewBag.ContratosVencer = contratosProximos.Count;

            if (contratosProximos.Any())
            {
                ViewBag.Alerta = $"Hay {contratosProximos.Count} contrato(s) que vencerán en menos de un mes.";
            }

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["AlertMessage"] = "Por favor ingrese usuario y clave.";
                return RedirectToAction("Index");
            }

            Usuario usuario;
            var result = _authService.Auth(model.Correo, model.Clave, out usuario);

            switch (result)
            {
                case AuthResults.Success:
                    CookieUpdate(usuario);
                    return RedirectToAction("Dashboard", "Home");

                case AuthResults.PasswordNotMatch:
                    TempData["AlertMessage"] = "La contraseña es incorrecta.";
                    break;

                case AuthResults.NotExists:
                    TempData["AlertMessage"] = "El usuario no existe.";
                    break;

                default:
                    TempData["AlertMessage"] = "Ocurrió un error inesperado.";
                    break;
            }

            return RedirectToAction("Index");
        }

        // Acción de cierre de sesión mejorada
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            // Limpiar sesión y autenticación
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();

            // Eliminar todas las cookies del navegador
            EliminarTodasLasCookies();

            return RedirectToAction("Index");
        }

        // Interno: Elimina todas las cookies
        private void EliminarTodasLasCookies()
        {
            if (Request.Cookies != null)
            {
                foreach (string cookieName in Request.Cookies.AllKeys)
                {
                    var expiredCookie = new HttpCookie(cookieName)
                    {
                        Expires = DateTime.Now.AddDays(-1),
                        Value = string.Empty
                    };
                    Response.Cookies.Add(expiredCookie);
                }
            }
        }

        // Refrescar cookies (opcional, útil para mantener sesión activa con AJAX)
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult RefreshLogin(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { Message = "" }, JsonRequestBehavior.AllowGet);
            }

            Usuario usuario;
            var result = _authService.Auth(model.Correo, model.Clave, out usuario);

            switch (result)
            {
                case AuthResults.Success:
                    CookieUpdate(usuario);
                    return Json(new { Message = "Sesión actualizada correctamente." }, JsonRequestBehavior.AllowGet);

                case AuthResults.PasswordNotMatch:
                    return Json(new { Message = "La contraseña es incorrecta." }, JsonRequestBehavior.AllowGet);

                case AuthResults.NotExists:
                    return Json(new { Message = "El usuario no existe." }, JsonRequestBehavior.AllowGet);

                default:
                    return Json(new { Message = "Error inesperado." }, JsonRequestBehavior.AllowGet);
            }
        }

        // Interno: Crea la cookie de autenticación
        private void CookieUpdate(Usuario usuario)
        {
            FormsAuthentication.SetAuthCookie(usuario.Correo, false);

            var ticket = new FormsAuthenticationTicket(
                2,
                usuario.Correo,
                DateTime.Now,
                DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes),
                false,
                JsonConvert.SerializeObject(usuario, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                })
            );

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            Response.Cookies.Add(cookie);

            Session["Username"] = usuario.Correo;
            Session["Rol"] = usuario.Rol?.NombreRol;
        }
    }
}
