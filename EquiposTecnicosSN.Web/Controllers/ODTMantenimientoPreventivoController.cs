using EquiposTecnicosSN.Entities.Mantenimiento;
using EquiposTecnicosSN.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EquiposTecnicosSN.Web.Controllers
{
    public class ODTMantenimientoPreventivoController : ODTController
    {

        public override ActionResult CountOrdenesPrioridad(string prioridad)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public override ActionResult CreateForEquipo(int id)
        {
            ViewBag.ChecklistId = new SelectList(db.ChecklistsMantenimientoPreventivo, "ChecklistMantenimientoPreventivoId", "Nombre");

            var equipo = db.Equipos.Find(id);
            var odt = new OrdenDeTrabajoMantenimientoPreventivo
            {
                EquipoId = equipo.EquipoId,
                Equipo = equipo,
                Estado = OrdenDeTrabajoEstado.Abierta,
                FechaInicio = DateTime.Now,
                NumeroReferencia = DateTime.Now.ToString("yyyyMMddHHmmssff"),
                Prioridad = OrdenDeTrabajoPrioridad.Normal
            };

            var model = new MPViewModel();
            model.Odt = odt;
            model.NuevaObservacion = NuevaObservacion();
            return View(model);
        }

        // POST: ODTMantenimientoPreventivoController/CreateForEquipo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateForEquipo(MPViewModel vm)
        {
            if (vm.Odt.ChecklistId != 0)
            {
                vm.Odt.Checklist = db.ChecklistsMantenimientoPreventivo.Find(vm.Odt.ChecklistId);
                vm.Odt.FechaInicio = DateTime.Now;
                vm.Odt.UsuarioInicioId = 1; //TODO: hardcode
                vm.Odt.fechaCreacion = vm.Odt.FechaInicio;
                SaveNuevaObservacion(vm.NuevaObservacion, vm.Odt);
                db.ODTMantenimientosPreventivos.Add(vm.Odt);
                await db.SaveChangesAsync();

                return RedirectToAction("Details", new { id = vm.Odt.OrdenDeTrabajoId });
            }

            ViewBag.ChecklistId = new SelectList(db.ChecklistsMantenimientoPreventivo, "ChecklistMantenimientoPreventivoId", "Nombre", vm.Odt.ChecklistId);
            return View(vm);
        }

        public override ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrdenDeTrabajoMantenimientoPreventivo ordenDeTrabajo = db.ODTMantenimientosPreventivos.Find(id);

            if (ordenDeTrabajo == null)
            {
                return HttpNotFound();
            }
            ordenDeTrabajo.SolicitudesRespuestos = db.SolicitudesRepuestosServicios.Where(s => s.OrdenDeTrabajoId == id).ToList();
            ordenDeTrabajo.Equipo = db.Equipos.Find(ordenDeTrabajo.EquipoId);
            return View(ordenDeTrabajo);
        }

        [HttpGet]
        override public ActionResult EditGastos(int id)
        {
            var model = db.ODTMantenimientosPreventivos.Find(id);
            return View(model);
        }

        // POST: OrdenesDeTrabajo/EditGastos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditGastos(int ordenDeTrabajoId, IEnumerable<GastoOrdenDeTrabajo> gastos)
        {
            var orden = db.ODTMantenimientosPreventivos.Find(ordenDeTrabajoId);
            //gastos
            SaveGastos(gastos, orden.OrdenDeTrabajoId);

            db.Entry(orden).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Details", new { id = orden.OrdenDeTrabajoId });
        }

        // GET: OrdenesDeTrabajo/Close/id
        [HttpGet]
        override public ActionResult Close(int id)
        {
            var odt = db.ODTMantenimientosPreventivos.Find(id);
            var model = new MPViewModel();
            model.Odt = odt;
            model.NuevaObservacion = NuevaObservacion();
            return View(model);
        }

        // POST: OrdenesDeTrabajo/FillRepair
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Close(MPViewModel vm, IEnumerable<GastoOrdenDeTrabajo> gastos)
        {

            try
            {

                OrdenDeTrabajoMantenimientoPreventivo orden = await db.ODTMantenimientosPreventivos
                    .Include(o => o.SolicitudesRespuestos)
                    .Where(o => o.OrdenDeTrabajoId == vm.Odt.OrdenDeTrabajoId)
                    .SingleOrDefaultAsync();

                if (orden == null)
                {
                    return HttpNotFound();
                }
                
                orden.ChecklistCompleto = vm.Odt.ChecklistCompleto;
                orden.Estado = OrdenDeTrabajoEstado.Cerrada;
                orden.FechaCierre = DateTime.Now;
                orden.UsuarioCierreId = 1; //HARDCODE

                //gastos
                if (gastos != null && gastos.Count() > 0)
                {
                    SaveGastos(gastos, orden.OrdenDeTrabajoId);
                }

                //solicitudes
                CloseSolicitudesRepuestos(orden);

                //observaciones
                SaveNuevaObservacion(vm.NuevaObservacion, orden);

                db.Entry(orden).State = EntityState.Modified;
                await db.SaveChangesAsync();

            }
            catch (DbEntityValidationException e)
            {
                Debug.WriteLine(e.Data);
            }
            return RedirectToAction("Details", new { id = vm.Odt.OrdenDeTrabajoId });
        }
    }
}
