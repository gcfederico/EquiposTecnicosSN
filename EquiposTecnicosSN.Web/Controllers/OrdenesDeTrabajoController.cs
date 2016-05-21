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
    public class OrdenesDeTrabajoController : Controller
    {
        private EquiposDbContext db = new EquiposDbContext();

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
            ordenDeTrabajo.SolicitudesRespuestos = await db.SolicitudesRepuestosServicios.Where(s => s.OrdenDeTrabajoId == id).ToListAsync();

            return View(ordenDeTrabajo);
        }

        // GET: OrdenesDeTrabajo/AddGasto
        public ActionResult AddGasto()
        {
            return PartialView("_AddGasto", new GastoOrdenDeTrabajo());
        }

        // GET: OrdenesDeTrabajo/CreateForEquipo/id
        public ActionResult CreateForEquipo(int id)
        {
            var equipo = db.Equipos.Find(id);
            var model = new OrdenDeTrabajo
            {
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

            if (ordenDeTrabajo.Descripcion == null && ordenDeTrabajo.Gastos == null)
            {
                return View(ordenDeTrabajo);
            }

            var orden = db.OrdenesDeTrabajo.Find(ordenDeTrabajo.OrdenDeTrabajoId);
            //datos de diagnostico
            orden.Diagnostico = ordenDeTrabajo.Diagnostico;
            orden.FechaDiagnostico = DateTime.Now;
            orden.UsuarioDiagnosticoId = 1; //HARDCODE!!

            //gastos
            SaveGastos(gastos, orden.OrdenDeTrabajoId);

            db.Entry(orden).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Details", new { id = orden.OrdenDeTrabajoId });
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
            //falta guardar y cerrar
            orden.FechaReparacion = DateTime.Now;
            orden.UsuarioReparacionId = 1; //HARDCODE

            db.Entry(orden).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Details", new { id = ordenDeTrabajo.OrdenDeTrabajoId });
        }

        // GET: OrdenesDeTrabajo/OrderReplacementService/id
        public ActionResult OrderReplacementService(int id)
        {
            ViewBag.ProveedorId = new SelectList(db.Proveedores, "ProveedorId", "Nombre");
            var odt = db.OrdenesDeTrabajo.Find(id);
            var model = new SolicitudRepuestoServicio
            {
                OrdenDeTrabajoId = id,
                FechaInicio = DateTime.Now,
                Repuesto = new Repuesto(),
            };
            return View(model);
        }

        // POST: OrdenesDeTrabajo/OrderReplacementService
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> OrderReplacementService(SolicitudRepuestoServicio solicitud)
        {
            var orden = db.OrdenesDeTrabajo.Find(solicitud.OrdenDeTrabajoId);
            if (solicitud.Repuesto.Codigo != null && solicitud.Repuesto.Nombre != null)
            {
                var repuesto = await db.Repuestos.Where(r => r.Codigo == solicitud.Repuesto.Codigo).SingleOrDefaultAsync();

                if (repuesto != null)
                {
                    solicitud.OrdenDeTrabajo = orden;
                    solicitud.Repuesto = repuesto;
                    solicitud.UsuarioSolicitudId = 1; //HARDCODE
                    db.SolicitudesRepuestosServicios.Add(solicitud);

                    orden.Estado = OrdenDeTrabajoEstado.EsperaRepuesto;
                    db.Entry(orden).State = EntityState.Modified;

                    await db.SaveChangesAsync();
                    return RedirectToAction("Details", new { id = solicitud.OrdenDeTrabajoId });
                }
            }
            ViewBag.ProveedorId = new SelectList(db.Proveedores, "ProveedorId", "Nombre", solicitud.ProveedorId);
            return View(solicitud);
        }



        // GET: OrdenesDeTrabajo/EditGastos/id
        public ActionResult EditGastos(int id)
        {
            var model = db.OrdenesDeTrabajo.Find(id);
            return View(model);
        }

        // POST: OrdenesDeTrabajo/EditGastos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditGastos(int ordenDeTrabajoId, IEnumerable<GastoOrdenDeTrabajo> gastos)
        {
            var orden = db.OrdenesDeTrabajo.Find(ordenDeTrabajoId);
            //gastos
            SaveGastos(gastos, orden.OrdenDeTrabajoId);

            db.Entry(orden).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Details", new { id = orden.OrdenDeTrabajoId });
        }





        // Solicitudes de repuesto o servicios

        // GET: OrdenesDeTrabajo/DetailsSolicitudRepuestoServicio/id
        public ActionResult DetailsSolicitudRepuestoServicio(int id)
        {
            var srs = db.SolicitudesRepuestosServicios.Find(id);
            return View(srs);
        }


        //
        public void SaveGastos(IEnumerable<GastoOrdenDeTrabajo> gastos, int ordenDeTrabajoId)
        {
            var gastosEntidad = db.GastosOrdenesDeTrabajo.Where(g => g.OrdenDeTrabajoId == ordenDeTrabajoId).ToList();

            foreach (var gastoE in gastosEntidad)
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
                g.OrdenDeTrabajoId = ordenDeTrabajoId;
                db.GastosOrdenesDeTrabajo.Add(g);
            });
        }

        // GET: OrdenesDeTrabajo/CountOrdenesPrioridad
        [HttpGet]
        public ActionResult CountOrdenesPrioridad(String prioridad)
        {

            var count = db.OrdenesDeTrabajo.Where(o => o.Prioridad == OrdenDeTrabajoPrioridad.Emergencia).Count();
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
        public async Task<ActionResult> Edit(OrdenDeTrabajo ordenDeTrabajo)
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
            if (prioridad.Equals(OrdenDeTrabajoPrioridad.Emergencia.DisplayName()))
            {
                var ordenesDeTrabajo = db.OrdenesDeTrabajo.Where(o => o.Prioridad == OrdenDeTrabajoPrioridad.Emergencia);
                return View(await ordenesDeTrabajo.ToListAsync());
            }
            else
            {
                var ordenesDeTrabajo = db.OrdenesDeTrabajo.Include(o => o.Equipo);
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
