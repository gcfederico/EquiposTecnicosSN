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
    public class EquiposBaseController : Controller
    {
        private EquiposBaseDbContext db = new EquiposBaseDbContext();

        // GET: EquiposBase
        public ActionResult Index()
        {
            return View(db.Equipos.ToList());
        }

        // GET: EquiposBase/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipoBase equipoBase = db.Equipos.Find(id);
            if (equipoBase == null)
            {
                return HttpNotFound();
            }
            return View(equipoBase);
        }

        // GET: EquiposBase/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EquiposBase/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NombreCompleto,UMDNS,Tipo,NumeroSerie,Modelo,fechaCompra,numeroInventario")] EquipoBase equipoBase)
        {
            if (ModelState.IsValid)
            {
                db.Equipos.Add(equipoBase);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(equipoBase);
        }

        // GET: EquiposBase/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipoBase equipoBase = db.Equipos.Find(id);
            if (equipoBase == null)
            {
                return HttpNotFound();
            }
            return View(equipoBase);
        }

        // POST: EquiposBase/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NombreCompleto,UMDNS,Tipo,NumeroSerie,Modelo,fechaCompra,numeroInventario")] EquipoBase equipoBase)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipoBase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(equipoBase);
        }

        // GET: EquiposBase/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipoBase equipoBase = db.Equipos.Find(id);
            if (equipoBase == null)
            {
                return HttpNotFound();
            }
            return View(equipoBase);
        }

        // POST: EquiposBase/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EquipoBase equipoBase = db.Equipos.Find(id);
            db.Equipos.Remove(equipoBase);
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
