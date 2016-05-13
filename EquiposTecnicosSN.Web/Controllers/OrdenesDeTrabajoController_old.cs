using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using EquiposTecnicosSN.Web.DataContexts;
using EquiposTecnicosSN.Entities.Mantenimiento;

namespace EquiposTecnicosSN.Web.Controllers
{
    [Authorize]
    public class OrdenesDeTrabajoController_old : Controller
    {
        private EquiposDbContext db = new EquiposDbContext();

        public ActionResult AddGastoToOrden()
        {
            return PartialView("_AddGastoToOrden", new GastoOrdenDeTrabajo());
        }

        // GET: OrdenesDeTrabajo/IndexForMantenimiento/5
        public ActionResult IndexForMantenimiento(int mantenimientoId, int equipoId)
        {
            /*            ViewBag.MantenimientoId = mantenimientoId;
                        ViewBag.EquipoId = equipoId;
                        var ordenesForMantenimiento = db.OrdenesDeTrabajo.Where(o => o.MantenimientoId == mantenimientoId);
                        return View(await ordenesForMantenimiento.ToListAsync());
                        */
            return View();
        }

        // GET: OrdenesDeTrabajo/CreateForMantenimiento/5
        public ActionResult CreateForMantenimiento(int mantenimientoId, int equipoId)
        {
            /*var model = new OrdenDeTrabajo();
            model.MantenimientoId = mantenimientoId;
            model.Gastos = new List<GastoOrdenDeTrabajo>();
            ViewBag.MantenimientoId = new SelectList(db.MantenimientosEquipo, "MantenimientoId", "Descripcion", mantenimientoId);
            ViewBag.ProveedorId = new SelectList(db.Proveedores, "ProveedorId", "Nombre");
            ViewBag.EquipoId = equipoId;
            return View(model);
            */
            return View();
        }

        // POST: OrdenesDeTrabajo/CreateForMantenimiento
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateForMantenimiento([Bind(Include = "OrdenDeTrabajoId,MantenimientoId,Diagnostico,Resolucion,ProveedorId")] OrdenDeTrabajo ordenDeTrabajo, IEnumerable<GastoOrdenDeTrabajo> gastos, int equipoId)
        {
            if (ModelState.IsValid)
            {
                ordenDeTrabajo.Gastos = (ICollection<GastoOrdenDeTrabajo>)gastos;
                db.OrdenesDeTrabajo.Add(ordenDeTrabajo);
                await db.SaveChangesAsync();
                //return RedirectToAction("IndexForMantenimiento",new { mantenimientoId = ordenDeTrabajo.MantenimientoId, equipoId = equipoId });
                return View(ordenDeTrabajo);
            }

            //ViewBag.MantenimientoId = new SelectList(db.MantenimientosEquipo, "MantenimientoId", "Descripcion", ordenDeTrabajo.MantenimientoId);
            //ViewBag.ProveedorId = new SelectList(db.Proveedores, "ProveedorId", "Nombre", ordenDeTrabajo.ProveedorId);
            ViewBag.EquipoId = equipoId;
            return View(ordenDeTrabajo);
        }


        // GET: OrdenesDeTrabajo
        public ActionResult Index(int? id)
        {
            /*
            if (id == null)
            {
                return View(await 
                    db.OrdenesDeTrabajo
                    .Include(o => o.Mantenimiento)
                    .Include(o => o.Proveedor)
                    .ToListAsync());
            }
            else
            {
                return View(await 
                    db.OrdenesDeTrabajo
                    .Where(o => o.MantenimientoId == id)
                    .Include(o => o.Mantenimiento)
                    .Include(o => o.Proveedor)
                    .ToListAsync());
            }*/
            return View();
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
            //ViewBag.MantenimientoId = new SelectList(db.MantenimientosEquipo, "MantenimientoId", "Descripcion");
            ViewBag.ProveedorId = new SelectList(db.Proveedores, "ProveedorId", "Nombre");
            var model = new OrdenDeTrabajo();
            model.Gastos = new List<GastoOrdenDeTrabajo>();
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

           // ViewBag.MantenimientoId = new SelectList(db.MantenimientosEquipo, "MantenimientoId", "Descripcion", ordenDeTrabajo.MantenimientoId);
            //ViewBag.ProveedorId = new SelectList(db.Proveedores, "ProveedorId", "Nombre", ordenDeTrabajo.ProveedorId);
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
            //ViewBag.MantenimientoId = new SelectList(db.MantenimientosEquipo, "MantenimientoId", "Descripcion", ordenDeTrabajo.MantenimientoId);
            //ViewBag.ProveedorId = new SelectList(db.Proveedores, "ProveedorId", "Nombre", ordenDeTrabajo.ProveedorId);
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
                var gastosEntidad = await db.GastosOrdenesDeTrabajo.Where(g => g.OrdenDeTrabajoId == ordenDeTrabajo.OrdenDeTrabajoId).ToListAsync();

                foreach(var gastoE in gastosEntidad)
                {
                    if (gastos.Any(g => g.GastoOrdenDeTrabajoId == gastoE.GastoOrdenDeTrabajoId))
                    {
                        var edicion = gastos.Where(g => g.GastoOrdenDeTrabajoId == gastoE.GastoOrdenDeTrabajoId).Single();
                        gastoE.Monto = edicion.Monto;
                        gastoE.Concepto = edicion.Concepto;
                        db.Entry(gastoE).State = EntityState.Modified;
                    }
                    else
                    {
                        db.GastosOrdenesDeTrabajo.Remove(gastoE);
                    }
                }

                var nuevosGastos = gastos.Where(g => g.GastoOrdenDeTrabajoId == 0).ToList();
                nuevosGastos.ForEach(g => {
                    g.OrdenDeTrabajoId = ordenDeTrabajo.OrdenDeTrabajoId;
                    db.GastosOrdenesDeTrabajo.Add(g);
                });

                db.Entry(ordenDeTrabajo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //ViewBag.MantenimientoId = new SelectList(db.MantenimientosEquipo, "MantenimientoId", "Descripcion", ordenDeTrabajo.MantenimientoId);
            //ViewBag.ProveedorId = new SelectList(db.Proveedores, "ProveedorId", "Nombre", ordenDeTrabajo.ProveedorId);
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
