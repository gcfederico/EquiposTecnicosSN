using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EquiposTecnicosSN.Entities;
using EquiposTecnicosSN.Web.DataContexts;

namespace EquiposTecnicosSN.Web.Controllers
{
    public class InformacionesComercialesController : Controller
    {
        private EquiposDbContext db = new EquiposDbContext();

        // GET: InformacionesComerciales
        public ActionResult Index()
        {
            var informacionesComerciales = db.InformacionesComerciales.Include(i => i.Equipo).Include(i => i.Proveedor);
            return View(informacionesComerciales.ToList());
        }

        // GET: InformacionesComerciales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InformacionComercial informacionComercial = db.InformacionesComerciales.Find(id);
            if (informacionComercial == null)
            {
                return HttpNotFound();
            }
            return View(informacionComercial);
        }

        // GET: InformacionesComerciales/Create
        public ActionResult Create()
        {
            ViewBag.EquipoId = new SelectList(db.Equipos, "EquipoId", "NombreCompleto");
            ViewBag.ProveedorId = new SelectList(db.Proveedores, "ProveedorId", "Nombre");
            return View();
        }

        // POST: InformacionesComerciales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EquipoId,FechaCompra,PrecioCompra,ValorRestante,EsGrantiaContrato,FechaFinGarantia,NotasGarantia,ProveedorId")] InformacionComercial informacionComercial)
        {
            if (ModelState.IsValid)
            {
                db.InformacionesComerciales.Add(informacionComercial);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EquipoId = new SelectList(db.Equipos, "EquipoId", "NombreCompleto", informacionComercial.EquipoId);
            ViewBag.ProveedorId = new SelectList(db.Proveedores, "ProveedorId", "Nombre", informacionComercial.ProveedorId);
            return View(informacionComercial);
        }

        // GET: InformacionesComerciales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InformacionComercial informacionComercial = db.InformacionesComerciales.Find(id);
            if (informacionComercial == null)
            {
                return HttpNotFound();
            }
            ViewBag.EquipoId = new SelectList(db.Equipos, "EquipoId", "NombreCompleto", informacionComercial.EquipoId);
            ViewBag.ProveedorId = new SelectList(db.Proveedores, "ProveedorId", "Nombre", informacionComercial.ProveedorId);
            return View(informacionComercial);
        }

        // POST: InformacionesComerciales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EquipoId,FechaCompra,PrecioCompra,ValorRestante,EsGrantiaContrato,FechaFinGarantia,NotasGarantia,ProveedorId")] InformacionComercial informacionComercial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(informacionComercial).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EquipoId = new SelectList(db.Equipos, "EquipoId", "NombreCompleto", informacionComercial.EquipoId);
            ViewBag.ProveedorId = new SelectList(db.Proveedores, "ProveedorId", "Nombre", informacionComercial.ProveedorId);
            return View(informacionComercial);
        }

        // GET: InformacionesComerciales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InformacionComercial informacionComercial = db.InformacionesComerciales.Find(id);
            if (informacionComercial == null)
            {
                return HttpNotFound();
            }
            return View(informacionComercial);
        }

        // POST: InformacionesComerciales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InformacionComercial informacionComercial = db.InformacionesComerciales.Find(id);
            db.InformacionesComerciales.Remove(informacionComercial);
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
