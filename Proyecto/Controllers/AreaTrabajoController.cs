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
    public class AreaTrabajoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AreaTrabajo
        public ActionResult Index()
        {
            return View(db.AreasTrabajo.ToList());
        }

        // GET: AreaTrabajo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreaTrabajo areaTrabajo = db.AreasTrabajo.Find(id);
            if (areaTrabajo == null)
            {
                return HttpNotFound();
            }
            return View(areaTrabajo);
        }

        // GET: AreaTrabajo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AreaTrabajo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdArea,NombreArea,Descripcion")] AreaTrabajo areaTrabajo)
        {
            if (ModelState.IsValid)
            {
                db.AreasTrabajo.Add(areaTrabajo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(areaTrabajo);
        }

        // GET: AreaTrabajo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreaTrabajo areaTrabajo = db.AreasTrabajo.Find(id);
            if (areaTrabajo == null)
            {
                return HttpNotFound();
            }
            return View(areaTrabajo);
        }

        // POST: AreaTrabajo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdArea,NombreArea,Descripcion")] AreaTrabajo areaTrabajo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(areaTrabajo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(areaTrabajo);
        }

        // GET: AreaTrabajo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreaTrabajo areaTrabajo = db.AreasTrabajo.Find(id);
            if (areaTrabajo == null)
            {
                return HttpNotFound();
            }
            return View(areaTrabajo);
        }

        // POST: AreaTrabajo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AreaTrabajo areaTrabajo = db.AreasTrabajo.Find(id);
            db.AreasTrabajo.Remove(areaTrabajo);
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
