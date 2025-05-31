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

namespace Proyecto.Controllers
{
    [Authorize]
    public class AusenciasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Ausencias
        public ActionResult Index(string filtroCedula)
        {
            var ausencias = db.Ausencias.Include(a => a.Empleado);

            if (!string.IsNullOrEmpty(filtroCedula))
            {
                ausencias = ausencias.Where(a => a.Empleado.NumeroDocumento.Contains(filtroCedula));
            }

            return View(ausencias.ToList());
        }

        // GET: Ausencias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ausencia ausencia = db.Ausencias.Find(id);
            if (ausencia == null)
            {
                return HttpNotFound();
            }
            return View(ausencia);
        }

        // GET: Ausencias/Create
        public ActionResult Create()
        {
            ViewBag.IdEmpleado = new SelectList(db.Empleados, "IdEmpleado", "NumeroDocumento");
            return View();
        }

        // POST: Ausencias/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdEmpleado,FechaInicio,FechaFin,TipoAusencia,Justificacion")] Ausencia ausencia)
        {
            if (ModelState.IsValid)
            {
                db.Ausencias.Add(ausencia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdEmpleado = new SelectList(db.Empleados, "IdEmpleado", "NumeroDocumento", ausencia.IdEmpleado);
            return View(ausencia);
        }

        // GET: Ausencias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ausencia ausencia = db.Ausencias.Find(id);
            if (ausencia == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdEmpleado = new SelectList(db.Empleados, "IdEmpleado", "NumeroDocumento", ausencia.IdEmpleado);
            return View(ausencia);
        }

        // POST: Ausencias/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdEmpleado,FechaInicio,FechaFin,TipoAusencia,Justificacion")] Ausencia ausencia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ausencia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdEmpleado = new SelectList(db.Empleados, "IdEmpleado", "NumeroDocumento", ausencia.IdEmpleado);
            return View(ausencia);
        }

        // GET: Ausencias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ausencia ausencia = db.Ausencias.Find(id);
            if (ausencia == null)
            {
                return HttpNotFound();
            }
            return View(ausencia);
        }

        // POST: Ausencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ausencia ausencia = db.Ausencias.Find(id);
            db.Ausencias.Remove(ausencia);
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
