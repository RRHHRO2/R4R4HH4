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
    public class ContratoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Contrato
        public ActionResult Index(string filtroCedula)
        {
            var contratos = db.Contratos
                .Include(c => c.Empleado)
                .Include(c => c.Empleado.TipoDocumento)
                .Include(c => c.TipoContrato)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filtroCedula))
            {
                contratos = contratos.Where(c => c.Empleado.NumeroDocumento.Contains(filtroCedula));
            }

            return View(contratos.ToList());
        }

        // GET: Contrato/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contrato contrato = db.Contratos.Find(id);
            if (contrato == null)
            {
                return HttpNotFound();
            }
            return View(contrato);
        }

        // GET: Contrato/Create
        public ActionResult Create()
        {
            ViewBag.IdEmpleado = new SelectList(db.Empleados, "IdEmpleado", "Apellidos");
            ViewBag.IdTipoContrato = new SelectList(db.TiposContrato, "IdTipoContrato", "NombreTipo");
            return View();
        }

        // POST: Contrato/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdTipoContrato,IdEmpleado,FechaInicio,FechaFin")] Contrato contrato)
        {
            if (ModelState.IsValid)
            {
                db.Contratos.Add(contrato);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdEmpleado = new SelectList(db.Empleados, "IdEmpleado", "Apellidos", contrato.IdEmpleado);
            ViewBag.IdTipoContrato = new SelectList(db.TiposContrato, "IdTipoContrato", "NombreTipo", contrato.IdTipoContrato);
            return View(contrato);
        }

        // GET: Contrato/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contrato contrato = db.Contratos.Find(id);
            if (contrato == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdEmpleado = new SelectList(db.Empleados, "IdEmpleado", "Apellidos", contrato.IdEmpleado);
            ViewBag.IdTipoContrato = new SelectList(db.TiposContrato, "IdTipoContrato", "NombreTipo", contrato.IdTipoContrato);
            return View(contrato);
        }

        // POST: Contrato/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdTipoContrato,IdEmpleado,FechaInicio,FechaFin")] Contrato contrato)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contrato).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdEmpleado = new SelectList(db.Empleados, "IdEmpleado", "NumeroDocumento", contrato.IdEmpleado);
            ViewBag.IdTipoContrato = new SelectList(db.TiposContrato, "IdTipoContrato", "NombreTipo", contrato.IdTipoContrato);
            return View(contrato);
        }

        // GET: Contrato/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contrato contrato = db.Contratos.Find(id);
            if (contrato == null)
            {
                return HttpNotFound();
            }
            return View(contrato);
        }

        // POST: Contrato/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contrato contrato = db.Contratos.Find(id);
            db.Contratos.Remove(contrato);
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
