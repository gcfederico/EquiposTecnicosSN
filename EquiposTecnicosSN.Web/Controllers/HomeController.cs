using EquiposTecnicosSN.Entities.Equipos;
using EquiposTecnicosSN.Entities.Mantenimiento;
using EquiposTecnicosSN.Web.DataContexts;
using EquiposTecnicosSN.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace EquiposTecnicosSN.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private EquiposDbContext db = new EquiposDbContext();


        // POST: EquiposBase/EquiposDeUsuarioCount
        [HttpPost]
        public JsonResult EquiposDeUsuarioCount()
        {
            //Get ubicacion de Usuario
            var count = db.Equipos.Count();
            return Json(count);
        }

        public ActionResult Index()
        {

            ViewBag.UbicacionId = new SelectList(db.Ubicaciones.OrderBy(u => u.Nombre), "UbicacionId", "Nombre");
            ViewBag.SectorId = new SelectList(db.Sectores.OrderBy(u => u.Nombre), "SectorId", "Nombre");
            return View(new HomeViewModel());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buscarNombreCompleto"></param>
        /// <param name="buscarUMDNS"></param>
        /// <param name="UbicacionId"></param>
        /// <param name="SectorId"></param>
        /// <param name="Estado"></param>
        /// <param name="NumeroMatricula"></param>
        /// <returns></returns>
        public ActionResult SearchEquipos(string buscarNombreCompleto, string buscarUMDNS, int? UbicacionId, int? SectorId, int? EstadoEquipo, int? NumeroMatricula)
        {
            var result = db.Equipos
                .Where(e => buscarNombreCompleto.Equals("") || e.NombreCompleto.Equals(buscarNombreCompleto))
                .Where(e => buscarUMDNS.Equals("") || e.UMDNS.Equals(buscarUMDNS))
                .Where(e => UbicacionId == null || e.UbicacionId == UbicacionId)
                .Where(e => SectorId == null || e.SectorId == SectorId)
                .Where(e => NumeroMatricula == null || e.NumeroMatricula.Equals(NumeroMatricula))
                .ToList();

            if (EstadoEquipo != 0)
            {
                EstadoDeEquipo estadoFiltro = (EstadoDeEquipo)EstadoEquipo;
                result = result.Where(e => e.Estado.Equals(estadoFiltro)).ToList();
            }
            
            return PartialView("_SearchEquipos", result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buscarNumeroReferencia"></param>
        /// <param name="EstadoODT"></param>
        /// <returns></returns>
        public ActionResult SearchODT(string buscarNumeroReferencia, int? EstadoODT)
        {
            var result = db.OrdenesDeTrabajo
                .Where(odt => buscarNumeroReferencia.Equals("") || odt.NumeroReferencia.Contains(buscarNumeroReferencia))
                .ToList();

            if (EstadoODT != 0)
            {
                OrdenDeTrabajoEstado estadoFiltro = (OrdenDeTrabajoEstado) EstadoODT;
                result = result.Where(odt => odt.Estado.Equals(estadoFiltro)).ToList();
            }

            return PartialView("_SearchODTs", result);
        }

    }
}