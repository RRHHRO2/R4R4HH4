using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto.Context;
using Proyecto.Models;
using Proyecto.Security;
using Proyecto.Utils;

namespace Proyecto.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private readonly IPasswordEncripter _passwordEncripter = new PasswordEncripter();

        // GET: Usuarios
        public ActionResult Index()
        {
            var usuarios = db.Usuarios.Include(u => u.AreaTrabajo).Include(u => u.Rol);
            return View(usuarios.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.IdArea = new SelectList(db.AreasTrabajo, "IdArea", "NombreArea");
            ViewBag.IdRol = new SelectList(db.Roles, "IdRol", "NombreRol");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var hash = new List<byte[]>();
                usuario.Contrasena = this._passwordEncripter.Encript(usuario.Contrasena, out hash);
                usuario.HashKey = hash[0];
                usuario.HashIV = hash[1];

                db.Usuarios.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdArea = new SelectList(db.AreasTrabajo, "IdArea", "NombreArea", usuario.IdArea);
            ViewBag.IdRol = new SelectList(db.Roles, "IdRol", "NombreRol", usuario.IdRol);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdArea = new SelectList(db.AreasTrabajo, "IdArea", "NombreArea", usuario.IdArea);
            ViewBag.IdRol = new SelectList(db.Roles, "IdRol", "NombreRol", usuario.IdRol);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(usuario.NuevaContrasena))
                {
                    usuario.Contrasena = _passwordEncripter.Encript(usuario.NuevaContrasena, new List<byte[]>()
                  .AddHash(usuario.HashKey)
                  .AddHash(usuario.HashIV));
                }

                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdArea = new SelectList(db.AreasTrabajo, "IdArea", "NombreArea", usuario.IdArea);
            ViewBag.IdRol = new SelectList(db.Roles, "IdRol", "NombreRol", usuario.IdRol);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
