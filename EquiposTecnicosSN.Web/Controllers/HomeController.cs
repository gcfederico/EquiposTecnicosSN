using EquiposTecnicosSN.Entities.Equipos;
using EquiposTecnicosSN.Entities.Equipos.Info;
using EquiposTecnicosSN.Web.DataContexts;
using EquiposTecnicosSN.Web.Models;
using EquiposTecnicosSN.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        public ActionResult Index(BuscarEquipoViewModel vm)
        {

            ViewBag.UbicacionId = new SelectList(db.Ubicaciones.OrderBy(u => u.Nombre), "UbicacionId", "Nombre");
            ViewBag.SectorId = new SelectList(db.Sectores.OrderBy(u => u.Nombre), "SectorId", "Nombre");
            return View(new BuscarEquipoViewModel());
        }


        public ActionResult SearchEquipos(string buscarNombreCompleto, string buscarUMDNS, int? UbicacionId, int? SectorId, int? Estado, int? NumeroMatricula)
        {
            var result = db.Equipos
                .Where(e => buscarNombreCompleto.Equals("") || e.NombreCompleto.Equals(buscarNombreCompleto))
                .Where(e => buscarUMDNS.Equals("") || e.UMDNS.Equals(buscarUMDNS))
                .Where(e => UbicacionId == null || e.UbicacionId == UbicacionId)
                .Where(e => SectorId == null || e.SectorId == SectorId)
                .Where(e => NumeroMatricula == null || e.NumeroMatricula.Equals(NumeroMatricula))
                .ToList();

            if (Estado != 0)
            {
                EstadoDeEquipo estadoFiltro = (EstadoDeEquipo)Estado;
                result = result.Where(e => e.Estado.Equals(estadoFiltro)).ToList();
            }
            
            return PartialView("_SearchEquipos", result);
        }

        
    }
}