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
using EquiposTecnicosSN.Entities.Mantenimientos;
using EquiposTecnicosSN.Web.Models;

namespace EquiposTecnicosSN.Web.Controllers
{
    public class OrdenesDeTrabajoController : Controller
    {
        private EquiposDbContext db = new EquiposDbContext();

        public ActionResult AddGastoToOrden()
        {
            return PartialView("_AddGastoToOrden", new GastoOrdenDeTrabajo());
        }

        // GET: OrdenesDeTrabajo/ShowOrdenesForMantenimiento/5
        public async Task<ActionResult> ShowOrdenesForMantenimiento(int? idMantenimiento)
        {
            var ordenesForMantenimiento = db.OrdenesDeTrabajo.Where(o => o.MantenimientoId == idMantenimiento); //.Include(o => o.MantenimientoEquipo).Include(o => o.Proveedor);
            return View(await ordenesForMantenimiento.ToListAsync());
        }


        // GET: OrdenesDeTrabajo
        public async Task<ActionResult> Index(int? id)
        {
            
            if (id == null)
            {
                return View(await 
                    db.OrdenesDeTrabajo
                    .Include(o => o.Mantenimiento)
                    .Include(o => o.Proveedor)
                    .ToListAsync());
            } else
            {
                return View(await 
                    db.OrdenesDeTrabajo
                    .Where(o => o.MantenimientoId == id)
                    .Include(o => o.Mantenimiento)
                    .Include(o => o.Proveedor)
                    .ToListAsync());
            }
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
            ViewBag.MantenimientoId = new SelectList(db.MantenimientosEquipo, "MantenimientoId", "Descripcion");
            ViewBag.ProveedorId = new SelectList(db.Proveedores, "ProveedorId", "Nombre");
            var gastosList = new GastoOrdenDeTrabajo[]
                {
                    new GastoOrdenDeTrabajo { Concepto = "lala", Monto = 2 },
                    new GastoOrdenDeTrabajo { Concepto = "lele", Monto = 33332 }
                };
            var model = new OrdenDeTrabajo{ Gastos = gastosList };
            ViewBag.gastos = gastosList;
            return View(model);
        }

        // POST: OrdenesDeTrabajo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create(NewOrdenDeTrabajoViewModel newOrdenViewModel)
        public async Task<ActionResult> Create([Bind(Include = "OrdenDeTrabajoId,MantenimientoId,Diagnostico,Resolucion,ProveedorId")] OrdenDeTrabajo ordenDeTrabajo, IEnumerable<GastoOrdenDeTrabajo> gastos)
        {
            if (ModelState.IsValid)
            {
                ordenDeTrabajo.Gastos = (ICollection<GastoOrdenDeTrabajo>) gastos;
                db.OrdenesDeTrabajo.Add(ordenDeTrabajo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MantenimientoId = new SelectList(db.MantenimientosEquipo, "MantenimientoId", "Descripcion", ordenDeTrabajo.MantenimientoId);
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
            ViewBag.MantenimientoId = new SelectList(db.MantenimientosEquipo, "MantenimientoId", "Descripcion", ordenDeTrabajo.MantenimientoId);
            ViewBag.ProveedorId = new SelectList(db.Proveedores, "ProveedorId", "Nombre", ordenDeTrabajo.ProveedorId);
            return View(ordenDeTrabajo);
        }

        // POST: OrdenesDeTrabajo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "OrdenDeTrabajoId,MantenimientoId,Diagnostico,Resolucion,ProveedorId")] OrdenDeTrabajo ordenDeTrabajo, IEnumerable<GastoOrdenDeTrabajo> gastos)
        {
            if (ModelState.IsValid)
            {

                var gastosOriginales = await db.GastosOrdenesDeTrabajo.Where(g => g.OrdenDeTrabajoId == ordenDeTrabajo.OrdenDeTrabajoId).ToListAsync();

                foreach (var gasto in gastos)
                {
                    if (gasto.GastoOrdenDeTrabajoId == 0)
                    {
                        gasto.OrdenDeTrabajoId = ordenDeTrabajo.OrdenDeTrabajoId;
                        db.GastosOrdenesDeTrabajo.Add(gasto);
                    }
                    else if (gastosOriginales.Any(g => g.GastoOrdenDeTrabajoId == gasto.GastoOrdenDeTrabajoId))
                    {
                        db.Entry(gasto).State = EntityState.Modified;
                    }
                    else
                    {
                        db.GastosOrdenesDeTrabajo.Remove(gasto);
                    }
                }
                db.Entry(ordenDeTrabajo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MantenimientoId = new SelectList(db.MantenimientosEquipo, "MantenimientoId", "Descripcion", ordenDeTrabajo.MantenimientoId);
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
