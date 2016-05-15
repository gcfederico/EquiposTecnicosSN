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
    public class OrdenesDeTrabajoController : Controller
    {
        private EquiposDbContext db = new EquiposDbContext();

        // GET: OrdenesDeTrabajo/AddGasto
        public ActionResult AddGasto()
        {
            return PartialView("_AddGasto", new GastoOrdenDeTrabajo());
        }

        // GET: OrdenesDeTrabajo/CreateForEquipo/id
        public ActionResult CreateForEquipo(int id)
        {
            var equipo = db.Equipos.Find(id);
            var model = new OrdenDeTrabajo {
                EquipoId = equipo.EquipoId,
                Equipo = equipo,
                Estado = OrdenDeTrabajoEstado.Abierto,
                FechaInicio = DateTime.Now,
                NumeroReferencia = DateTime.Now.ToString("yyyyMMddHHmmssff"),
                Prioridad = OrdenDeTrabajoPrioridad.Normal
            };
            return View(model);
        }

        // POST: OrdenesDeTrabajo/CreateForEquipo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateForEquipo(OrdenDeTrabajo ordenDeTrabajo, string action)
        {
            if (ModelState.IsValid)
            {
                ordenDeTrabajo.FechaInicio = DateTime.Now;
                ordenDeTrabajo.UsuarioInicioId = 1; //TODO: hardcode
                db.OrdenesDeTrabajo.Add(ordenDeTrabajo);
                await db.SaveChangesAsync();

                if (action.Equals("Guardar"))
                {
                    return RedirectToAction("Details", "EquiposClimatizacion", new { id = ordenDeTrabajo.EquipoId });
                }
                else
                {
                    return RedirectToAction("FillDiagnose", new { id = ordenDeTrabajo.OrdenDeTrabajoId });
                }
            }

            return View(ordenDeTrabajo);
        }

        // GET: OrdenesDeTrabajo/FillDiagnose/id
        public ActionResult FillDiagnose(int id)
        {
            var model = db.OrdenesDeTrabajo.Find(id);
            return View(model);
        }

        // POST: OrdenesDeTrabajo/FillDiagnose
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> FillDiagnose(OrdenDeTrabajo ordenDeTrabajo, IEnumerable<GastoOrdenDeTrabajo> gastos)
        {

            if (ordenDeTrabajo.Diagnostico != null)
            {
                var orden = db.OrdenesDeTrabajo.Find(ordenDeTrabajo.OrdenDeTrabajoId);
                //datos de diagnostico
                orden.Diagnostico = ordenDeTrabajo.Diagnostico;
                orden.FechaDiagnostico = DateTime.Now;
                orden.UsuarioDiagnosticoId = 1; //HARDCODE!!
                //gastos
                orden.Gastos = (ICollection<GastoOrdenDeTrabajo>)gastos;

                db.Entry(orden).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "EquiposClimatizacion", new { id = orden.EquipoId });
            }

            return View(ordenDeTrabajo);
        }

        // GET: OrdenesDeTrabajo/FillRepair/id
        public ActionResult FillRepair(int id)
        {
            var model = db.OrdenesDeTrabajo.Find(id);
            return View(model);
        }

        // POST: OrdenesDeTrabajo/FillRepair
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> FillRepair(OrdenDeTrabajo ordenDeTrabajo)
        {
            var orden = db.OrdenesDeTrabajo.Find(ordenDeTrabajo.OrdenDeTrabajoId);
            orden.Diagnostico = ordenDeTrabajo.Diagnostico;
            orden.FechaDiagnostico = DateTime.Now;
            orden.UsuarioDiagnosticoId = 1;
            db.Entry(orden).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: OrdenesDeTrabajo/OrderReplacement/id
        public ActionResult OrderReplacement(int id)
        {
            var model = db.OrdenesDeTrabajo.Find(id);
            return View(model);
        }

        // POST: OrdenesDeTrabajo/OrderReplacement
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> OrderReplacement(OrdenDeTrabajo ordenDeTrabajo)
        {
            var orden = db.OrdenesDeTrabajo.Find(ordenDeTrabajo.OrdenDeTrabajoId);
            orden.Diagnostico = ordenDeTrabajo.Diagnostico;
            orden.FechaDiagnostico = DateTime.Now;
            orden.UsuarioDiagnosticoId = 1;
            db.Entry(orden).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }









        // GET: OrdenesDeTrabajo/Create
        public ActionResult Create()
        {
            ViewBag.EquipoId = new SelectList(db.Equipos, "EquipoId", "NombreCompleto");
            return View();
        }

        // POST: OrdenesDeTrabajo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "OrdenDeTrabajoId,EquipoId,Estado,FechaInicio,UsuarioInicioId,Prioridad,EquipoParado,Descripcion,Diagnostico,DetalleReparacion,CausaRaiz,FechaDiagnostico,UsuarioDiagnosticoId,FechaReparacion,UsuarioReparacionId,FechaCierre,UsuarioCierreId")] OrdenDeTrabajo ordenDeTrabajo)
        {
            if (ModelState.IsValid)
            {
                db.OrdenesDeTrabajo.Add(ordenDeTrabajo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.EquipoId = new SelectList(db.Equipos, "EquipoId", "NombreCompleto", ordenDeTrabajo.EquipoId);
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
            ViewBag.EquipoId = new SelectList(db.Equipos, "EquipoId", "NombreCompleto", ordenDeTrabajo.EquipoId);
            return View(ordenDeTrabajo);
        }

        // POST: OrdenesDeTrabajo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "OrdenDeTrabajoId,EquipoId,Estado,FechaInicio,UsuarioInicioId,Prioridad,EquipoParado,Descripcion,Diagnostico,DetalleReparacion,CausaRaiz,FechaDiagnostico,UsuarioDiagnosticoId,FechaReparacion,UsuarioReparacionId,FechaCierre,UsuarioCierreId")] OrdenDeTrabajo ordenDeTrabajo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ordenDeTrabajo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.EquipoId = new SelectList(db.Equipos, "EquipoId", "NombreCompleto", ordenDeTrabajo.EquipoId);
            return View(ordenDeTrabajo);
        }

        // GET: OrdenesDeTrabajo
        public async Task<ActionResult> Index()
        {
            var ordenesDeTrabajo = db.OrdenesDeTrabajo.Include(o => o.Equipo);
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
