using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EquiposTecnicosSN.Entities;
using EquiposTecnicosSN.Web.DataContexts;

namespace EquiposTecnicosSN.Web.Controllers
{
    public class OrdenesDeTrabajoController : Controller
    {
        private EquiposDbContext db = new EquiposDbContext();

        // GET: OrdenesDeTrabajo
        public async Task<ActionResult> Index()
        {
            var ordenesDeTrabajo = db.OrdenesDeTrabajo.Include(o => o.MantenimientoDeEquipo).Include(o => o.Proveedor);
            return View(await ordenesDeTrabajo.ToListAsync());
        }

        // GET: OrdenesDeTrabajo/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrdenDeTrabajo ordenDeTrabajo = await db.OrdenesDeTrabajo.FindAsync(id);
            if (ordenDeTrabajo == null)
            {
                return HttpNotFound();
            }
            return View(ordenDeTrabajo);
        }

        // GET: OrdenesDeTrabajo/Create
        public ActionResult Create()
        {
            ViewBag.MantenimientoDeEquipoId = new SelectList(db.MantenimientosEquipo, "MantenimientoEquipoId", "Descripcion");
            ViewBag.ProveedorId = new SelectList(db.Proveedores, "ProveedorId", "Nombre");
            return View();
        }

        // POST: OrdenesDeTrabajo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MantenimientoDeEquipoId,OrdenDeTrabajoId,Diagnostico,Resolucion,ProveedorId")] OrdenDeTrabajo ordenDeTrabajo)
        {
            if (ModelState.IsValid)
            {
                db.OrdenesDeTrabajo.Add(ordenDeTrabajo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MantenimientoDeEquipoId = new SelectList(db.MantenimientosEquipo, "MantenimientoEquipoId", "Descripcion", ordenDeTrabajo.MantenimientoDeEquipoId);
            ViewBag.ProveedorId = new SelectList(db.Proveedores, "ProveedorId", "Nombre", ordenDeTrabajo.ProveedorId);
            return View(ordenDeTrabajo);
        }

        // GET: OrdenesDeTrabajo/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrdenDeTrabajo ordenDeTrabajo = await db.OrdenesDeTrabajo.FindAsync(id);
            if (ordenDeTrabajo == null)
            {
                return HttpNotFound();
            }
            ViewBag.MantenimientoDeEquipoId = new SelectList(db.MantenimientosEquipo, "MantenimientoEquipoId", "Descripcion", ordenDeTrabajo.MantenimientoDeEquipoId);
            ViewBag.ProveedorId = new SelectList(db.Proveedores, "ProveedorId", "Nombre", ordenDeTrabajo.ProveedorId);
            return View(ordenDeTrabajo);
        }

        // POST: OrdenesDeTrabajo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MantenimientoDeEquipoId,OrdenDeTrabajoId,Diagnostico,Resolucion,ProveedorId")] OrdenDeTrabajo ordenDeTrabajo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ordenDeTrabajo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MantenimientoDeEquipoId = new SelectList(db.MantenimientosEquipo, "MantenimientoEquipoId", "Descripcion", ordenDeTrabajo.MantenimientoDeEquipoId);
            ViewBag.ProveedorId = new SelectList(db.Proveedores, "ProveedorId", "Nombre", ordenDeTrabajo.ProveedorId);
            return View(ordenDeTrabajo);
        }

        // GET: OrdenesDeTrabajo/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrdenDeTrabajo ordenDeTrabajo = await db.OrdenesDeTrabajo.FindAsync(id);
            if (ordenDeTrabajo == null)
            {
                return HttpNotFound();
            }
            return View(ordenDeTrabajo);
        }

        // POST: OrdenesDeTrabajo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            OrdenDeTrabajo ordenDeTrabajo = await db.OrdenesDeTrabajo.FindAsync(id);
            db.OrdenesDeTrabajo.Remove(ordenDeTrabajo);
            await db.SaveChangesAsync();
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
