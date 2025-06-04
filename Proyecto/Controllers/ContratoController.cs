using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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

            // Alerta por contratos próximos a vencer
            DateTime hoy = DateTime.Today;
            DateTime limite = hoy.AddMonths(1);
            var proximosAVencer = contratos.Where(c => c.FechaFin >= hoy && c.FechaFin <= limite).ToList();

            if (proximosAVencer.Any())
            {
                ViewBag.Alerta = $"Hay {proximosAVencer.Count} contratos próximos a vencer en menos de un mes.";
            }

            return View(contratos.ToList());
        }

        // GET: Contrato/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Contrato contrato = db.Contratos.Find(id);
            if (contrato == null) return HttpNotFound();

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
        public ActionResult Create([Bind(Include = "Id,IdTipoContrato,IdEmpleado,FechaInicio,FechaFin")] Contrato contrato, HttpPostedFileBase ArchivoPDF)
        {
            if (ModelState.IsValid)
            {
                if (ArchivoPDF != null && ArchivoPDF.ContentLength > 0)
                {
                    using (var reader = new BinaryReader(ArchivoPDF.InputStream))
                    {
                        contrato.ArchivoPDF = reader.ReadBytes(ArchivoPDF.ContentLength);
                    }
                }

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
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Contrato contrato = db.Contratos.Find(id);
            if (contrato == null) return HttpNotFound();

            ViewBag.IdEmpleado = new SelectList(db.Empleados, "IdEmpleado", "Apellidos", contrato.IdEmpleado);
            ViewBag.IdTipoContrato = new SelectList(db.TiposContrato, "IdTipoContrato", "NombreTipo", contrato.IdTipoContrato);
            return View(contrato);
        }

        // POST: Contrato/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdTipoContrato,IdEmpleado,FechaInicio,FechaFin")] Contrato contrato, HttpPostedFileBase ArchivoPDF)
        {
            if (ModelState.IsValid)
            {
                var contratoExistente = db.Contratos.Find(contrato.Id);
                if (contratoExistente == null) return HttpNotFound();

                contratoExistente.IdTipoContrato = contrato.IdTipoContrato;
                contratoExistente.IdEmpleado = contrato.IdEmpleado;
                contratoExistente.FechaInicio = contrato.FechaInicio;
                contratoExistente.FechaFin = contrato.FechaFin;

                if (ArchivoPDF != null && ArchivoPDF.ContentLength > 0)
                {
                    using (var reader = new BinaryReader(ArchivoPDF.InputStream))
                    {
                        contratoExistente.ArchivoPDF = reader.ReadBytes(ArchivoPDF.ContentLength);
                    }
                }

                db.Entry(contratoExistente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdEmpleado = new SelectList(db.Empleados, "IdEmpleado", "Apellidos", contrato.IdEmpleado);
            ViewBag.IdTipoContrato = new SelectList(db.TiposContrato, "IdTipoContrato", "NombreTipo", contrato.IdTipoContrato);
            return View(contrato);
        }

        // GET: Contrato/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Contrato contrato = db.Contratos.Find(id);
            if (contrato == null) return HttpNotFound();

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

        // GET: Contrato/DescargarPDF/5
        public FileResult DescargarPDF(int id)
        {
            var contrato = db.Contratos.Find(id);
            if (contrato == null || contrato.ArchivoPDF == null)
            {
                return null;
            }

            return File(contrato.ArchivoPDF, "application/pdf", $"Contrato_{id}.pdf");
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
