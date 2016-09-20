using EquiposTecnicosSN.Entities.Mantenimiento;
using EquiposTecnicosSN.Web.Models;
using Salud.Security.SSO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EquiposTecnicosSN.Web.Controllers
{
    public class ODTMantenimientoPreventivoController : ODTController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var model = new MPIndexViewModel
            {
                Search = new SearchOdtViewModel(),
                Proximas = odtsService.MPreventivosProximos(),
                Abiertas = odtsService.MPreventivosAbiertos(null)
            };

            return View(model);
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
        public ActionResult CreateForEquipo(MPViewModel vm)
        {
            if (vm.Odt.ChecklistId != 0)
            {
                vm.Odt.Checklist = db.ChecklistsMantenimientoPreventivo.Find(vm.Odt.ChecklistId);
                vm.Odt.FechaInicio = DateTime.Now;
                vm.Odt.UsuarioInicio = SSOHelper.CurrentIdentity.Fullname;
                vm.Odt.fechaCreacion = vm.Odt.FechaInicio;
                SaveNuevaObservacion(vm.NuevaObservacion, vm.Odt);
                db.ODTMantenimientosPreventivos.Add(vm.Odt);
                db.SaveChanges();

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
            ordenDeTrabajo.SolicitudesRespuestos = odtsService.BuscarSolicitudes(id);
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
        public ActionResult EditGastos(int ordenDeTrabajoId, IEnumerable<GastoOrdenDeTrabajo> gastos)
        {
            var orden = db.ODTMantenimientosPreventivos.Find(ordenDeTrabajoId);

            //gastos
            SaveGastos(gastos, orden.OrdenDeTrabajoId);

            //db.Entry(orden).State = EntityState.Modified;
            //db.SaveChanges();
            odtsService.Update(orden);
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
                orden.UsuarioCierre = SSOHelper.CurrentIdentity.Fullname;

                //gastos
                if (gastos != null && gastos.Count() > 0)
                {
                    SaveGastos(gastos, orden.OrdenDeTrabajoId);
                }

                //solicitudes
                CloseSolicitudesRepuestos(orden);

                //observaciones
                SaveNuevaObservacion(vm.NuevaObservacion, orden);

                //archivo
                if (vm.ChecklistCompletoFile != null && vm.ChecklistCompletoFile.ContentLength > 0)
                {
                    orden.ChecklistCompletoFileExtension = Path.GetExtension(vm.ChecklistCompletoFile.FileName);
                    orden.ChecklistCompletoContentType = vm.ChecklistCompletoFile.ContentType;

                    using (var reader = new BinaryReader(vm.ChecklistCompletoFile.InputStream))
                    {
                        orden.ChecklistCompletoContent = reader.ReadBytes(vm.ChecklistCompletoFile.ContentLength);
                    }
                }

                db.Entry(orden).State = EntityState.Modified;
                await db.SaveChangesAsync();

            }
            catch (DbEntityValidationException e)
            {
                Debug.WriteLine(e.Data);
            }
            return RedirectToAction("Details", new { id = vm.Odt.OrdenDeTrabajoId });
        }

        public FileResult DownloadChecklistCompleto(int odtId)
        {
            var orden = db.ODTMantenimientosPreventivos.Find(odtId);

            if (orden != null)
            {
                return File(orden.ChecklistCompletoContent, orden.ChecklistCompletoContentType, "ODT " + orden.NumeroReferencia + orden.ChecklistCompletoFileExtension);
            }

            return null;
        }


    }
}
