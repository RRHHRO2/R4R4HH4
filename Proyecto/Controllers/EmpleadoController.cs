using System;
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
    public class EmpleadoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Empleado
        public ActionResult Index(string filtroCedula)
        {
            var empleados = db.Empleados
                .Include(e => e.AreaTrabajo)
                .Include(e => e.SSEPS)
                .Include(e => e.SSAFC)
                .Include(e => e.SSAFP)
                .Include(e => e.MunicipioExpedicionDoc)
                .Include(e => e.MunicipioNacimiento)
                .Include(e => e.TipoDocumento)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filtroCedula))
            {
                empleados = empleados.Where(e => e.NumeroDocumento.Contains(filtroCedula));
            }

            return View(empleados.ToList());
        }

        // GET: Empleado/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Empleado empleado = db.Empleados.Find(id);
            if (empleado == null) return HttpNotFound();

            return View(empleado);
        }

        // GET: Empleado/Create
        public ActionResult Create()
        {
            ViewBag.IdAreaTrabajo = new SelectList(db.AreasTrabajo, "IdArea", "NombreArea");
            ViewBag.EPS = new SelectList(db.SeguridadSociales, "IdSeguridadSocial", "Nombre");
            ViewBag.FondoCesantias = new SelectList(db.SeguridadSociales, "IdSeguridadSocial", "Nombre");
            ViewBag.FondoPension = new SelectList(db.SeguridadSociales, "IdSeguridadSocial", "Nombre");
            ViewBag.MunicipioExpedicion = new SelectList(db.Municipios, "IdMunicipio", "Nombre");
            ViewBag.LugarNacimiento = new SelectList(db.Municipios, "IdMunicipio", "Nombre");
            ViewBag.IdTipoDocumento = new SelectList(db.TiposDocumento, "IdTipoDocumento", "Nombre");
            return View();
        }

        // POST: Empleado/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdEmpleado,IdTipoDocumento,NumeroDocumento,FechaExpedicion,MunicipioExpedicion,Nombres,Apellidos,LugarNacimiento,Direccion,Barrio,Telefono,Celular,Correo,EPS,FondoPension,FondoCesantias,IdAreaTrabajo,Profesion")] Empleado empleado, HttpPostedFileBase archivoPDF)
        {
            if (archivoPDF != null && archivoPDF.ContentLength > 0)
            {
                if (Path.GetExtension(archivoPDF.FileName).ToLower() != ".pdf")
                {
                    ModelState.AddModelError("ArchivoPDF", "Solo se permiten archivos PDF.");
                }
                else
                {
                    using (var reader = new BinaryReader(archivoPDF.InputStream))
                    {
                        empleado.ArchivoPDF = reader.ReadBytes(archivoPDF.ContentLength);
                    }
                }
            }

            if (ModelState.IsValid)
            {
                db.Empleados.Add(empleado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Volver a cargar combos si hay error
            ViewBag.IdAreaTrabajo = new SelectList(db.AreasTrabajo, "IdArea", "NombreArea", empleado.IdAreaTrabajo);
            ViewBag.EPS = new SelectList(db.SeguridadSociales, "IdSeguridadSocial", "Nombre", empleado.EPS);
            ViewBag.FondoCesantias = new SelectList(db.SeguridadSociales, "IdSeguridadSocial", "Nombre", empleado.FondoCesantias);
            ViewBag.FondoPension = new SelectList(db.SeguridadSociales, "IdSeguridadSocial", "Nombre", empleado.FondoPension);
            ViewBag.MunicipioExpedicion = new SelectList(db.Municipios, "IdMunicipio", "Nombre", empleado.MunicipioExpedicion);
            ViewBag.LugarNacimiento = new SelectList(db.Municipios, "IdMunicipio", "Nombre", empleado.LugarNacimiento);
            ViewBag.IdTipoDocumento = new SelectList(db.TiposDocumento, "IdTipoDocumento", "Nombre", empleado.IdTipoDocumento);
            return View(empleado);
        }

        // GET: Empleado/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Empleado empleado = db.Empleados.Find(id);
            if (empleado == null) return HttpNotFound();

            ViewBag.IdAreaTrabajo = new SelectList(db.AreasTrabajo, "IdArea", "NombreArea", empleado.IdAreaTrabajo);
            ViewBag.EPS = new SelectList(db.SeguridadSociales, "IdSeguridadSocial", "Nombre", empleado.EPS);
            ViewBag.FondoCesantias = new SelectList(db.SeguridadSociales, "IdSeguridadSocial", "Nombre", empleado.FondoCesantias);
            ViewBag.FondoPension = new SelectList(db.SeguridadSociales, "IdSeguridadSocial", "Nombre", empleado.FondoPension);
            ViewBag.MunicipioExpedicion = new SelectList(db.Municipios, "IdMunicipio", "Nombre", empleado.MunicipioExpedicion);
            ViewBag.LugarNacimiento = new SelectList(db.Municipios, "IdMunicipio", "Nombre", empleado.LugarNacimiento);
            ViewBag.IdTipoDocumento = new SelectList(db.TiposDocumento, "IdTipoDocumento", "Nombre", empleado.IdTipoDocumento);
            return View(empleado);
        }

        // POST: Empleado/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdEmpleado,IdTipoDocumento,NumeroDocumento,FechaExpedicion,MunicipioExpedicion,Nombres,Apellidos,LugarNacimiento,Direccion,Barrio,Telefono,Celular,Correo,EPS,FondoPension,FondoCesantias,IdAreaTrabajo,Profesion")] Empleado empleado, HttpPostedFileBase archivoPDF)
        {
            if (archivoPDF != null && archivoPDF.ContentLength > 0)
            {
                if (Path.GetExtension(archivoPDF.FileName).ToLower() != ".pdf")
                {
                    ModelState.AddModelError("ArchivoPDF", "Solo se permiten archivos PDF.");
                }
                else
                {
                    using (var reader = new BinaryReader(archivoPDF.InputStream))
                    {
                        empleado.ArchivoPDF = reader.ReadBytes(archivoPDF.ContentLength);
                    }
                }
            }
            else
            {
                // Conserva el PDF existente si no se subió uno nuevo
                var empleadoExistente = db.Empleados.AsNoTracking().FirstOrDefault(e => e.IdEmpleado == empleado.IdEmpleado);
                if (empleadoExistente != null)
                {
                    empleado.ArchivoPDF = empleadoExistente.ArchivoPDF;
                }
            }

            if (ModelState.IsValid)
            {
                db.Entry(empleado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdAreaTrabajo = new SelectList(db.AreasTrabajo, "IdArea", "NombreArea", empleado.IdAreaTrabajo);
            ViewBag.EPS = new SelectList(db.SeguridadSociales, "IdSeguridadSocial", "Nombre", empleado.EPS);
            ViewBag.FondoCesantias = new SelectList(db.SeguridadSociales, "IdSeguridadSocial", "Nombre", empleado.FondoCesantias);
            ViewBag.FondoPension = new SelectList(db.SeguridadSociales, "IdSeguridadSocial", "Nombre", empleado.FondoPension);
            ViewBag.MunicipioExpedicion = new SelectList(db.Municipios, "IdMunicipio", "Nombre", empleado.MunicipioExpedicion);
            ViewBag.LugarNacimiento = new SelectList(db.Municipios, "IdMunicipio", "Nombre", empleado.LugarNacimiento);
            ViewBag.IdTipoDocumento = new SelectList(db.TiposDocumento, "IdTipoDocumento", "Nombre", empleado.IdTipoDocumento);
            return View(empleado);
        }

        // GET: Empleado/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Empleado empleado = db.Empleados.Find(id);
            if (empleado == null) return HttpNotFound();

            return View(empleado);
        }

        // POST: Empleado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empleado empleado = db.Empleados.Find(id);
            db.Empleados.Remove(empleado);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Empleado/VerPDF/5
        public ActionResult VerPDF(int id)
        {
            var empleado = db.Empleados.Find(id);

            if (empleado == null || empleado.ArchivoPDF == null || empleado.ArchivoPDF.Length == 0)
            {
                return HttpNotFound("El empleado no existe o no tiene PDF asociado.");
            }

            return File(empleado.ArchivoPDF, "application/pdf");
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