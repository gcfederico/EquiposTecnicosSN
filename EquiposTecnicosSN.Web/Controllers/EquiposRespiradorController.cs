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
    public class EquiposRespiradorController : Controller
    {
        private EquiposRespiradorDb db = new EquiposRespiradorDb();

        // GET: EquiposRespirador
        public ActionResult Index()
        {
            return View(db.EquiposRespiradores.ToList());
        }

        // GET: EquiposRespirador/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipoRespirador equipoRespirador = db.EquiposRespiradores.Find(id);
            if (equipoRespirador == null)
            {
                return HttpNotFound();
            }
            return View(equipoRespirador);
        }

        // GET: EquiposRespirador/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EquiposRespirador/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NombreCompleto,UMDNS,Tipo,NumeroSerie,Modelo,fechaCompra,numeroInventario,Estado")] EquipoRespirador equipoRespirador)
        {
            if (ModelState.IsValid)
            {
                db.Equipos.Add(equipoRespirador);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(equipoRespirador);
        }

        // GET: EquiposRespirador/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipoRespirador equipoRespirador = db.EquiposRespiradores.Find(id);
            if (equipoRespirador == null)
            {
                return HttpNotFound();
            }
            return View(equipoRespirador);
        }

        // POST: EquiposRespirador/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NombreCompleto,UMDNS,Tipo,NumeroSerie,Modelo,fechaCompra,numeroInventario,Estado")] EquipoRespirador equipoRespirador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipoRespirador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(equipoRespirador);
        }

        // GET: EquiposRespirador/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipoRespirador equipoRespirador = db.EquiposRespiradores.Find(id);
            if (equipoRespirador == null)
            {
                return HttpNotFound();
            }
            return View(equipoRespirador);
        }

        // POST: EquiposRespirador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EquipoRespirador equipoRespirador = db.EquiposRespiradores.Find(id);
            db.EquiposRespiradores.Remove(equipoRespirador);
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
