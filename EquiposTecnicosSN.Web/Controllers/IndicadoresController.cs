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

        /// <summary>
        /// Devuelve la lista de todos los sectores.
        /// </summary>
        /// <returns>Lista de Sectores JSON.</returns>
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
            List<Sector> sectores = GetSectoresList(sectoresIds);

            foreach (var sector in sectores)
            {
                chartData.Add(sector.Nombre, indicadoresSrv.TiempoMedioDeReparacionPorSector(sector.SectorId));
            }

            var orderedData = chartData.OrderByDescending(x => x.Value).ToDictionary(r => r.Key,r => r.Value);

            return Json(orderedData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Devuelve una lista de objetos de tipo Serctor a partir de un string con todos los ids separados por coma.
        /// </summary>
        /// <param name="sectoresIds">String con los ids de los sectores a buscar separados por coma.</param>
        /// <returns>Lista de objetos de tipo Sector.</returns>
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