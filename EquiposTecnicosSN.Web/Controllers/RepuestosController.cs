using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EquiposTecnicosSN.Entities.Mantenimiento;
using EquiposTecnicosSN.Web.DataContexts;

namespace EquiposTecnicosSN.Web.Controllers
{
    public class RepuestosController : Controller
    {
        private EquiposDbContext db = new EquiposDbContext();

        // GET
        public async Task<JsonResult> CheckStockRepuesto(string codigo, int cantidad)
        {
            var stock = await db.StockRepuestos.Where(s => s.Repuesto.Codigo == codigo).SingleOrDefaultAsync();

            var response = new {
                hayStock = stock != null && stock.CantidadDisponible >= cantidad,
                proveedorId = stock.Repuesto.ProveedorId
            };

            return Json(response, JsonRequestBehavior.AllowGet);

        }


        // GET
        public ActionResult AutocompleteCodigoRepuesto(string term)
        {
            var model =
                db.Repuestos
                .Where(r => r.Codigo.Contains(term))
                .Take(10)
                .Select(r => new
                {
                    label = r.Codigo,
                    value = r.Nombre
                });

            return Json(model, JsonRequestBehavior.AllowGet);

        }

        // GET: Repuestos
        public async Task<ActionResult> Index()
        {
            var repuestos = db.Repuestos.Include(r => r.Proveedor);
            return View(await repuestos.ToListAsync());
        }

        // GET: Repuestos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repuesto repuesto = await db.Repuestos.FindAsync(id);
            if (repuesto == null)
            {
                return HttpNotFound();
            }
            return View(repuesto);
        }

        // GET: Repuestos/Create
        public ActionResult Create()
        {
            ViewBag.ProveedorId = new SelectList(db.Proveedores, "ProveedorId", "Nombre");
            return View();
        }

        // POST: Repuestos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "RepuestoId,Codigo,Nombre,ProveedorId,Costo")] Repuesto repuesto)
        {
            if (ModelState.IsValid)
            {
                db.Repuestos.Add(repuesto);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ProveedorId = new SelectList(db.Proveedores, "ProveedorId", "Nombre", repuesto.ProveedorId);
            return View(repuesto);
        }

        // GET: Repuestos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repuesto repuesto = await db.Repuestos.FindAsync(id);
            if (repuesto == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProveedorId = new SelectList(db.Proveedores, "ProveedorId", "Nombre", repuesto.ProveedorId);
            return View(repuesto);
        }

        // POST: Repuestos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "RepuestoId,Codigo,Nombre,ProveedorId,Costo")] Repuesto repuesto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(repuesto).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ProveedorId = new SelectList(db.Proveedores, "ProveedorId", "Nombre", repuesto.ProveedorId);
            return View(repuesto);
        }

        // GET: Repuestos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repuesto repuesto = await db.Repuestos.FindAsync(id);
            if (repuesto == null)
            {
                return HttpNotFound();
            }
            return View(repuesto);
        }

        // POST: Repuestos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Repuesto repuesto = await db.Repuestos.FindAsync(id);
            db.Repuestos.Remove(repuesto);
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
