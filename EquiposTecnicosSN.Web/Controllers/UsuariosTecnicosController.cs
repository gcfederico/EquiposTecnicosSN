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
    public class UsuariosTecnicosController : Controller
    {
        private EquiposDbContext db = new EquiposDbContext();

        // GET: UsuariosTecnicos
        public ActionResult Index()
        {
            var usuarioTecnicoes = db.UsuariosTecnicos.Include(u => u.Ubicacion);
            return View(usuarioTecnicoes.ToList());
        }

        // GET: UsuariosTecnicos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioTecnico usuarioTecnico = db.UsuariosTecnicos.Find(id);
            if (usuarioTecnico == null)
            {
                return HttpNotFound();
            }
            return View(usuarioTecnico);
        }

        // GET: UsuariosTecnicos/Create
        public ActionResult Create()
        {
            ViewBag.UbicacionId = new SelectList(db.Ubicaciones, "UbicacionId", "NombreCompleto");
            return View();
        }

        // POST: UsuariosTecnicos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UsuarioTecnicoId,Email,UbicacionId")] UsuarioTecnico usuarioTecnico)
        {
            if (ModelState.IsValid)
            {
                db.UsuariosTecnicos.Add(usuarioTecnico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UbicacionId = new SelectList(db.Ubicaciones, "UbicacionId", "NombreCompleto", usuarioTecnico.UbicacionId);
            return View(usuarioTecnico);
        }

        // GET: UsuariosTecnicos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioTecnico usuarioTecnico = db.UsuariosTecnicos.Find(id);
            if (usuarioTecnico == null)
            {
                return HttpNotFound();
            }
            ViewBag.UbicacionId = new SelectList(db.Ubicaciones, "UbicacionId", "NombreCompleto", usuarioTecnico.UbicacionId);
            return View(usuarioTecnico);
        }

        // POST: UsuariosTecnicos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UsuarioTecnicoId,Email,UbicacionId")] UsuarioTecnico usuarioTecnico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuarioTecnico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UbicacionId = new SelectList(db.Ubicaciones, "UbicacionId", "NombreCompleto", usuarioTecnico.UbicacionId);
            return View(usuarioTecnico);
        }

        // GET: UsuariosTecnicos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioTecnico usuarioTecnico = db.UsuariosTecnicos.Find(id);
            if (usuarioTecnico == null)
            {
                return HttpNotFound();
            }
            return View(usuarioTecnico);
        }

        // POST: UsuariosTecnicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UsuarioTecnico usuarioTecnico = db.UsuariosTecnicos.Find(id);
            db.UsuariosTecnicos.Remove(usuarioTecnico);
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
