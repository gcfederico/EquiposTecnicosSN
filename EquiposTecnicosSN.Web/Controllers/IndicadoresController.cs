using EquiposTecnicosSN.Entities.Equipos.Info;
using EquiposTecnicosSN.Web.DataContexts;
using EquiposTecnicosSN.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EquiposTecnicosSN.Web.Controllers
{
    [Authorize]
    public class IndicadoresController : Controller
    {
        private EquiposDbContext db = new EquiposDbContext();
        private IndicadoresService indicadoresSrv = new IndicadoresService();

        // GET: Indicadores
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetSectores()
        {

            var sectores = db.Sectores
                .Select(s => new
                {
                    nombre = s.Nombre,
                    id = s.SectorId
                });
            return Json(sectores, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ParetoPorSectoresJSON(string sectoresIds)
        {
            Dictionary<string, double> chartData = new Dictionary<string, double>();
            List<Entities.Equipos.Info.Sector> sectores = GetSectoresList(sectoresIds);

            var i = 10;
            foreach (var sector in sectores)
            {
                chartData.Add(sector.Nombre, indicadoresSrv.TiempoMedioDeReparacionPorSector(sector.SectorId));
                //chartData.Add(sector.Nombre, i);
                i--;
            }

            return Json(chartData, JsonRequestBehavior.AllowGet);
        }

        private List<Sector> GetSectoresList(string sectoresIds)
        {
            var noBrackets = sectoresIds.Substring(1, sectoresIds.Length - 2);
            string[] stringIds = noBrackets.Split(',');
            int[] idsSectores = Array.ConvertAll(stringIds, s => int.Parse(s));
            var sectores = db.Sectores.Where(s => idsSectores.Contains(s.SectorId)).ToList();
            return sectores;
        }
    }
}