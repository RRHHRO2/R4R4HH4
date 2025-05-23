using Newtonsoft.Json;
using Proyecto.Models;
using Proyecto.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Proyecto.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthorizationService _authService = new AuthorizationService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult LoginPartial()
        {
            return PartialView("_LoginPartial");
        }

        public ActionResult LogoutPartial()
        {
            return PartialView("_LogoutPartial");
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            string returnUrl = Url.Action("Index", "Home");

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }

            Usuario usuario = new Usuario();
            var result = _authService.Auth(model.Correo, model.Clave, out usuario);
            switch (result)
            {
                case AuthResults.Success:
                    CookieUpdate(usuario);
                    return Redirect(returnUrl ?? Url.Action("Index", "Home"));
                case AuthResults.PasswordNotMatch:
                    TempData["AlertMessage"] = "La Contrasena es incorrecta.";
                    return RedirectToAction("Index", "Home");
                case AuthResults.NotExists:
                    TempData["AlertMessage"] = "El usuario no existe.";
                    return RedirectToAction("Index", "Home");
                default:
                    return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        [OutputCache(NoStore = true, Duration = 0)]
        [ValidateAntiForgeryToken]
        public ActionResult RefreshLogin(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { Message = "" }, JsonRequestBehavior.AllowGet);
            }
            Usuario usuario = new Usuario();
            var result = _authService.Auth(model.Correo, model.Clave, out usuario);
            switch (result)
            {
                case AuthResults.Success:
                    CookieUpdate(usuario);
                    return Json(new { Message = "Cookies Refrescados Correctamente." }, JsonRequestBehavior.AllowGet);
                case AuthResults.PasswordNotMatch:
                    return Json(new { Message = "La contraseña no es valida." }, JsonRequestBehavior.AllowGet);
                case AuthResults.NotExists:
                    return Json(new { Message = "El usuario no es valido." }, JsonRequestBehavior.AllowGet);
                default:
                    return Json(new { Message = "Error" }, JsonRequestBehavior.AllowGet);
            }
        }

        private void CookieUpdate(Usuario usuario)
        {
            var ticket = new FormsAuthenticationTicket(2,
                    usuario.Correo,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes),
                    false,
                    JsonConvert.SerializeObject(usuario, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    })
            );
            Session["Username"] = usuario.Correo;
            Session["Rol"] = usuario.Rol.NombreRol;
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket)) { };
            Response.AppendCookie(cookie);
        }
    }
}