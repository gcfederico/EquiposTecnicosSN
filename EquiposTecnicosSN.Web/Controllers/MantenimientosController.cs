using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using EquiposTecnicosSN.Web.DataContexts;
using EquiposTecnicosSN.Entities.Mantenimiento;
using System;

namespace EquiposTecnicosSN.Web.Controllers
{
    public class MantenimientosController : Controller
    {
        private EquiposDbContext db = new EquiposDbContext();

        public string GetNextMantenimiento()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        // GET: MantenimientosEquipo
        public ActionResult Index()
        {
            var mantenimientoEquipoes = db.MantenimientosEquipo.Include(m => m.Equipo);
            return View(mantenimientoEquipoes.ToList());
        }

        // GET: MantenimientosEquipo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mantenimiento mantenimientoEquipo = db.MantenimientosEquipo.Find(id);
            ViewBag.MantenimientoId = mantenimientoEquipo.MantenimientoId;
            ViewBag.EquipoId = mantenimientoEquipo.EquipoId;
            if (mantenimientoEquipo == null)
            {
                return HttpNotFound();
            }
            return View(mantenimientoEquipo);
        }

        // GET: MantenimientosEquipo/Create
        public ActionResult Create(int? mantenimientoId)
        {
            ViewBag.EquipoId = new SelectList(db.Equipos, "EquipoId", "NombreCompleto");
            var nRef = GetNextMantenimiento();
            var model = new Mantenimiento { NumeroReferencia = nRef };
            return View(model);
        }

        // POST: MantenimientosEquipo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MantenimientoId,EquipoId,NumeroReferencia,Estado,Descripcion,FechaDeInicio,FechaDeFin")] Mantenimiento mantenimientoEquipo)
        {
            if (ModelState.IsValid)
            {
                db.MantenimientosEquipo.Add(mantenimientoEquipo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EquipoId = new SelectList(db.Equipos, "EquipoId", "NombreCompleto", mantenimientoEquipo.EquipoId);
            return View(mantenimientoEquipo);
        }

        // GET: MantenimientosEquipo/CreateForEquipo/5
        public ActionResult CreateForEquipo(int idEquipo)
        {
            ViewBag.EquipoId = new SelectList(db.Equipos, "EquipoId", "NombreCompleto", idEquipo);
            var model = new Mantenimiento { EquipoId = idEquipo, NumeroReferencia = GetNextMantenimiento() };
            return View(model);
        }

        // POST: MantenimientosEquipo/CreateForEquipo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateForEquipo([Bind(Include = "MantenimientoId,EquipoId,NumeroReferencia,Estado,Descripcion,FechaDeInicio,FechaDeFin")] Mantenimiento mantenimientoEquipo)
        {
            if (ModelState.IsValid)
            {
                db.MantenimientosEquipo.Add(mantenimientoEquipo);
                db.SaveChanges();
                return RedirectToAction("Details", "EquiposClimatizacion", new { id = mantenimientoEquipo.EquipoId });
            }

            ViewBag.EquipoId = new SelectList(db.Equipos, "EquipoId", "NombreCompleto", mantenimientoEquipo.EquipoId);
            return View(mantenimientoEquipo);
        }

        // GET: MantenimientosEquipo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mantenimiento mantenimientoEquipo = db.MantenimientosEquipo.Find(id);
            if (mantenimientoEquipo == null)
            {
                return HttpNotFound();
            }
            ViewBag.EquipoId = new SelectList(db.Equipos, "EquipoId", "NombreCompleto", mantenimientoEquipo.EquipoId);
            return View(mantenimientoEquipo);
        }

        // POST: MantenimientosEquipo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MantenimientoId,EquipoId,NumeroReferencia,Estado,Descripcion,FechaDeInicio,FechaDeFin")] Mantenimiento mantenimientoEquipo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mantenimientoEquipo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EquipoId = new SelectList(db.Equipos, "EquipoId", "NombreCompleto", mantenimientoEquipo.EquipoId);
            return View(mantenimientoEquipo);
        }

        // GET: MantenimientosEquipo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mantenimiento mantenimientoEquipo = db.MantenimientosEquipo.Find(id);
            if (mantenimientoEquipo == null)
            {
                return HttpNotFound();
            }
            return View(mantenimientoEquipo);
        }

        // POST: MantenimientosEquipo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mantenimiento mantenimientoEquipo = db.MantenimientosEquipo.Find(id);
            db.MantenimientosEquipo.Remove(mantenimientoEquipo);
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
