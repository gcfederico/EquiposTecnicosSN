using EquiposTecnicosSN.Entities.Mantenimiento;
using EquiposTecnicosSN.Web.CustomExtensions;
using EquiposTecnicosSN.Web.DataContexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EquiposTecnicosSN.Web.Controllers
{
    public class SolicitudesRepuestoServicioController : Controller
    {
        private EquiposDbContext db = new EquiposDbContext();

        // GET: SolicitudesRepuestoServicio/DetailsSolicitud
        [HttpGet]
        public ActionResult DetailsSolicitud(int id)
        {
            var solicitud = db.SolicitudesRepuestosServicios.Find(id);
            return PartialView("_DetailSolicitudRepuestoServicioContent", solicitud);
        }

        // GET: */OrderReplacementService/id
        [HttpGet]
        public ActionResult OrderReplacementService(int id)
        {
            ViewBag.ProveedorId = new SelectList(db.Proveedores, "ProveedorId", "Nombre");
            var odt = db.OrdenesDeTrabajo.Find(id);
            var model = new SolicitudRepuestoServicio
            {
                OrdenDeTrabajoId = id,
                OrdenDeTrabajo = odt,
                FechaInicio = DateTime.Now,
                Repuesto = new Repuesto(),
            };
            return View(model);
        }

        // POST: */OrderReplacementService
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
                    return RedirectToAction("Details", orden.WebController() ,new { id = solicitud.OrdenDeTrabajoId });
                }
            }
            ViewBag.ProveedorId = new SelectList(db.Proveedores, "ProveedorId", "Nombre", solicitud.ProveedorId);
            return View(solicitud);
        }


        // GET: */DetailsSolicitudRepuestoServicio/id
        [HttpGet]
        public ActionResult Details(int id)
        {
            var srs = db.SolicitudesRepuestosServicios.Find(id);
            return View(srs);
        }

        // POST: */Close
        [HttpPost]
        public async Task<JsonResult> Close(int solicitudId)
        {
            var sRespuestoServicio = db.SolicitudesRepuestosServicios.Find(solicitudId);
            sRespuestoServicio.FechaCierre = DateTime.Now;            
            db.Entry(sRespuestoServicio).State = EntityState.Modified;

            var solicitudesAbiertas = await db.SolicitudesRepuestosServicios.Where(s => 
                s.OrdenDeTrabajoId == sRespuestoServicio.OrdenDeTrabajoId &&
                s.FechaInicio == null)
                .ToListAsync();

            if (solicitudesAbiertas.Count == 0)
            {
                var orden = db.OrdenesDeTrabajo.Find(sRespuestoServicio.OrdenDeTrabajoId);
                orden.Estado = OrdenDeTrabajoEstado.Abierta;
                db.Entry(orden).State = EntityState.Modified;
            }

            await db.SaveChangesAsync();

            return Json(new
            {
                result = "success",
                solicitudId = solicitudId,
                updateEstado = solicitudesAbiertas.Count == 0
            });
        }

    }
}