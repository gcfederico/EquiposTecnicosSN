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
using EquiposTecnicosSN.Web.CustomExtensions;

namespace EquiposTecnicosSN.Web.Controllers
{
    [Authorize]
    public class ODTMantenimientoCorrectivoController : ODTController
    {

        // GET: OrdenesDeTrabajo/OrdenesPorPrioridadCount
        [HttpPost]
        public JsonResult OrdenesPorPrioridadCount()
        {
            var counts = new
            {
                Emergencia = db.ODTMantenimientosCorrectivos.Where(o => o.Prioridad == OrdenDeTrabajoPrioridad.Emergencia).Count(),
                Urgente = db.ODTMantenimientosCorrectivos.Where(o => o.Prioridad == OrdenDeTrabajoPrioridad.Urgencia).Count(),
                Normal = db.ODTMantenimientosCorrectivos.Where(o => o.Prioridad == OrdenDeTrabajoPrioridad.Normal).Count()
            };

            return Json(counts);
        }

        // GET: OrdenesDeTrabajo/Details/5
        [HttpGet]
        override public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrdenDeTrabajoMantenimientoCorrectivo ordenDeTrabajo = db.ODTMantenimientosCorrectivos.Find(id);
            if (ordenDeTrabajo == null)
            {
                return HttpNotFound();
            }
            ordenDeTrabajo.SolicitudesRespuestos = db.SolicitudesRepuestosServicios.Where(s => s.OrdenDeTrabajoId == id).ToList();

            return View(ordenDeTrabajo);
        }

        // GET: OrdenesDeTrabajo/CreateForEquipo/id
        [HttpGet]
        override public ActionResult CreateForEquipo(int id)
        {
            var equipo = db.Equipos.Find(id);
            var model = new OrdenDeTrabajoMantenimientoCorrectivo
            {
                EquipoId = equipo.EquipoId,
                Equipo = equipo,
                Estado = OrdenDeTrabajoEstado.Abierta,
                FechaInicio = DateTime.Now,
                NumeroReferencia = DateTime.Now.ToString("yyyyMMddHHmmssff"),
                Prioridad = OrdenDeTrabajoPrioridad.Normal
            };
            return View(model);
        }

        // POST: OrdenesDeTrabajo/CreateForEquipo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateForEquipo(OrdenDeTrabajoMantenimientoCorrectivo ordenDeTrabajo, string action)
        {
            if (ModelState.IsValid)
            {
                ordenDeTrabajo.FechaInicio = DateTime.Now;
                ordenDeTrabajo.UsuarioInicioId = 1; //TODO: hardcode
                db.ODTMantenimientosCorrectivos.Add(ordenDeTrabajo);
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
        [HttpGet]
        public ActionResult FillDiagnose(int id)
        {
            var model = db.ODTMantenimientosCorrectivos.Find(id);
            return View(model);
        }

        // POST: OrdenesDeTrabajo/FillDiagnose
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> FillDiagnose(OrdenDeTrabajoMantenimientoCorrectivo ordenDeTrabajo, IEnumerable<GastoOrdenDeTrabajo> gastos)
        {
            if (ordenDeTrabajo.Diagnostico == null &&
                ordenDeTrabajo.Gastos == null &&
                gastos == null)
            {
                return View(ordenDeTrabajo);
            }


            var orden = db.ODTMantenimientosCorrectivos.Find(ordenDeTrabajo.OrdenDeTrabajoId);
            //datos de diagnostico
            orden.Diagnostico = ordenDeTrabajo.Diagnostico;
            orden.FechaDiagnostico = DateTime.Now;
            orden.UsuarioDiagnosticoId = 1; //HARDCODE!!
            //gastos
            if (gastos != null)
            {
                SaveGastos(gastos, orden.OrdenDeTrabajoId);
            }

            db.Entry(orden).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Details", new { id = orden.OrdenDeTrabajoId });
        }

        // GET: OrdenesDeTrabajo/Close/id
        [HttpGet]
        override public ActionResult Close(int id)
        {
            var model = db.ODTMantenimientosCorrectivos.Find(id);
            return View(model);
        }

        // POST: OrdenesDeTrabajo/FillRepair
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Close(OrdenDeTrabajoMantenimientoCorrectivo ordenDeTrabajo, IEnumerable<GastoOrdenDeTrabajo> gastos)
        {
            OrdenDeTrabajoMantenimientoCorrectivo orden = await db.ODTMantenimientosCorrectivos
                .Include(o => o.SolicitudesRespuestos)
                .Where(o => o.OrdenDeTrabajoId == ordenDeTrabajo.OrdenDeTrabajoId)
                .SingleOrDefaultAsync();

            if (orden == null)
            {
                return HttpNotFound();
            }

            if (ordenDeTrabajo.DetalleReparacion != null &&
                ordenDeTrabajo.CausaRaiz != null)
            {
                orden.DetalleReparacion = ordenDeTrabajo.DetalleReparacion;
                orden.CausaRaiz = ordenDeTrabajo.CausaRaiz;
                orden.Limpieza = ordenDeTrabajo.Limpieza;
                orden.VerificacionFuncionamiento = ordenDeTrabajo.VerificacionFuncionamiento;
                if (ordenDeTrabajo.Observaciones != null)
                {
                    orden.Observaciones = ordenDeTrabajo.Observaciones;
                }
                orden.Estado = OrdenDeTrabajoEstado.Cerrada;
                orden.FechaReparacion = DateTime.Now;
                orden.FechaCierre = DateTime.Now;
                orden.UsuarioReparacionId = 1; //HARDCODE
                orden.UsuarioCierreId = 1; //HARDCODE

                //gastos
                SaveGastos(gastos, orden.OrdenDeTrabajoId);

                //solicitudes
                CloseSolicitudesRepuestos(orden);

                db.Entry(orden).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Details", new { id = ordenDeTrabajo.OrdenDeTrabajoId });
            }


            return View(ordenDeTrabajo);
        }

        // GET: OrdenesDeTrabajo/EditGastos/id
        [HttpGet]
        override public ActionResult EditGastos(int id)
        {
            var model = db.ODTMantenimientosCorrectivos.Find(id);
            return View(model);
        }

        // POST: OrdenesDeTrabajo/EditGastos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditGastos(int ordenDeTrabajoId, IEnumerable<GastoOrdenDeTrabajo> gastos)
        {
            var orden = db.ODTMantenimientosCorrectivos.Find(ordenDeTrabajoId);
            //gastos
            SaveGastos(gastos, orden.OrdenDeTrabajoId);

            db.Entry(orden).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Details", new { id = orden.OrdenDeTrabajoId });
        }



        // GET: OrdenesDeTrabajo/CountOrdenesPrioridad
        [HttpGet]
        override public ActionResult CountOrdenesPrioridad(String prioridad)
        {
            var count = db.ODTMantenimientosCorrectivos.Where(o => o.Prioridad == OrdenDeTrabajoPrioridad.Emergencia).Count();
            return Json(count);
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
        public async Task<ActionResult> Create(OrdenDeTrabajoMantenimientoCorrectivo ordenDeTrabajo)
        {
            if (ModelState.IsValid)
            {
                db.ODTMantenimientosCorrectivos.Add(ordenDeTrabajo);
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
            OrdenDeTrabajoMantenimientoCorrectivo ordenDeTrabajo = await db.ODTMantenimientosCorrectivos.FindAsync(id);
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
        public async Task<ActionResult> Edit(OrdenDeTrabajoMantenimientoCorrectivo ordenDeTrabajo)
        {
            if (ModelState.IsValid)
            {

                //gastos
                SaveGastos(ordenDeTrabajo.Gastos, ordenDeTrabajo.OrdenDeTrabajoId);

                db.Entry(ordenDeTrabajo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.EquipoId = new SelectList(db.Equipos, "EquipoId", "NombreCompleto", ordenDeTrabajo.EquipoId);
            return View(ordenDeTrabajo);
        }

        // GET: OrdenesDeTrabajo
        public async Task<ActionResult> Index(String prioridad)
        {
            if (prioridad == null)
            {
                var ordenesDeTrabajo = db.ODTMantenimientosCorrectivos;
                return View(await ordenesDeTrabajo.ToListAsync());
            }
            else if (prioridad.Equals(OrdenDeTrabajoPrioridad.Emergencia.DisplayName()))
            {
                var ordenesDeTrabajo = db.ODTMantenimientosCorrectivos.Where(o => o.Prioridad == OrdenDeTrabajoPrioridad.Emergencia);
                return View(await ordenesDeTrabajo.ToListAsync());
            }
            else
            {
                var ordenesDeTrabajo = db.ODTMantenimientosCorrectivos.Include(o => o.Equipo);
                return View(await ordenesDeTrabajo.ToListAsync());
            }
        }



        // GET: OrdenesDeTrabajo/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrdenDeTrabajoMantenimientoCorrectivo ordenDeTrabajo = await db.ODTMantenimientosCorrectivos.FindAsync(id);
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
            OrdenDeTrabajoMantenimientoCorrectivo ordenDeTrabajo = await db.ODTMantenimientosCorrectivos.FindAsync(id);
            db.ODTMantenimientosCorrectivos.Remove(ordenDeTrabajo);
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
