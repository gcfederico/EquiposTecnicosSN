using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EquiposTecnicosSN.Entities.Otras;
using EquiposTecnicosSN.Web.DataContexts;

namespace EquiposTecnicosSN.Web.Controllers
{
    public class MarcasController : Controller
    {
        private EquiposDbContext db = new EquiposDbContext();

        // GET: Marcas
        public async Task<ActionResult> Index()
        {
            var marcas = db.Marcas.Include(m => m.Fabricante);
            return View(await marcas.ToListAsync());
        }

        // GET: Marcas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marca marca = await db.Marcas.FindAsync(id);
            if (marca == null)
            {
                return HttpNotFound();
            }
            return View(marca);
        }

        // GET: Marcas/Create
        public ActionResult Create()
        {
            ViewBag.FabricanteId = new SelectList(db.Fabricantes, "FabricanteId", "Nombre");
            return View();
        }

        // POST: Marcas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MarcaId,Nombre,FabricanteId")] Marca marca)
        {
            if (ModelState.IsValid)
            {
                db.Marcas.Add(marca);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FabricanteId = new SelectList(db.Fabricantes, "FabricanteId", "Nombre", marca.FabricanteId);
            return View(marca);
        }

        // GET: Marcas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marca marca = await db.Marcas.FindAsync(id);
            if (marca == null)
            {
                return HttpNotFound();
            }
            ViewBag.FabricanteId = new SelectList(db.Fabricantes, "FabricanteId", "Nombre", marca.FabricanteId);
            return View(marca);
        }

        // POST: Marcas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MarcaId,Nombre,FabricanteId")] Marca marca)
        {
            if (ModelState.IsValid)
            {
                db.Entry(marca).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FabricanteId = new SelectList(db.Fabricantes, "FabricanteId", "Nombre", marca.FabricanteId);
            return View(marca);
        }

        // GET: Marcas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marca marca = await db.Marcas.FindAsync(id);
            if (marca == null)
            {
                return HttpNotFound();
            }
            return View(marca);
        }

        // POST: Marcas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Marca marca = await db.Marcas.FindAsync(id);
            db.Marcas.Remove(marca);
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
