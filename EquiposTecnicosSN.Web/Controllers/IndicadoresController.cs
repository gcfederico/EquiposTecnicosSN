using EquiposTecnicosSN.Entities.Equipos.Info;
using EquiposTecnicosSN.Web.DataContexts;
using EquiposTecnicosSN.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EquiposTecnicosSN.Web.Controllers
{
    [Authorize]
    public class IndicadoresController : Controller
    {
        private EquiposDbContext db = new EquiposDbContext();
        private IndicadoresService indicadoresSrv = new IndicadoresService();

        /// <summary>
        /// Acción Web Index.
        /// </summary>
        /// <returns>View Model</returns>
        public ActionResult Index()
        {
            ViewBag.UbicacionId = new SelectList(db.Ubicaciones.OrderBy(u => u.Nombre), "UbicacionId", "Nombre");
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

        /// <summary>
        /// Calcula los tiempos medios de reparación de los equipos para los sectores y ubicación pasadas como parámetros.
        /// </summary>
        /// <param name="sectoresIds">String con los ids de los sectores a buscar separados por coma.</param>
        /// <param name="ubicacionId">Id de la ubicación seleccionada por el usuario.</param>
        /// <returns>Devuelve un diccionario en formato JSON con los tiempos medios de reparación de los equipos.</returns>
        public JsonResult ParetoTMRData(string sectoresIds, int? ubicacionId)
        {
            Dictionary<string, double> chartData = new Dictionary<string, double>();
            List<Sector> sectores = GetSectoresList(sectoresIds);

            foreach (var sector in sectores)
            {
                chartData.Add(sector.Nombre, indicadoresSrv.TiempoMedioDeReparacionPorSector(sector.SectorId, ubicacionId));
            }

            var orderedData = chartData.OrderByDescending(x => x.Value).ToDictionary(r => r.Key,r => r.Value);

            return Json(orderedData, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Calcula los tiempos de medios entre fallas de los equipos para los sectores y ubicación pasadas como parámetros.
        /// </summary>
        /// <param name="sectoresIds">String con los ids de los sectores a buscar separados por coma.</param>
        /// <param name="ubicacionId">Id de la ubicación seleccionada por el usuario.</param>
        /// <returns>Devuelve un diccionario en formato JSON con los tiempos medios entre fallas de los equipos.</returns>
        public JsonResult ParetoTMEFData(string sectoresIds, int? ubicacionId)
        {
            Dictionary<string, double> chartData = new Dictionary<string, double>();
            List<Sector> sectores = GetSectoresList(sectoresIds);

            foreach (var sector in sectores)
            {
                chartData.Add(sector.Nombre, indicadoresSrv.TiempoMedioEntreFallasPorSector(sector.SectorId, ubicacionId));
            }

            var orderedData = chartData.OrderByDescending(x => x.Value).ToDictionary(r => r.Key, r => r.Value);

            return Json(orderedData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Calcula los tiempos de indisponibolidad de los equipos para los sectores y ubicación pasadas como parámetros.
        /// </summary>
        /// <param name="sectoresIds">String con los ids de los sectores a buscar separados por coma.</param>
        /// <param name="ubicacionId">Id de la ubicación seleccionada por el usuario.</param>
        /// <returns>Devuelve un diccionario en formato JSON con los tiempos de indisponibilidad de los equipos.</returns>
        public JsonResult ParetoTIData(string sectoresIds, int? ubicacionId)
        {
            Dictionary<string, double> chartData = new Dictionary<string, double>();
            List<Sector> sectores = GetSectoresList(sectoresIds);

            foreach (var sector in sectores)
            {
                chartData.Add(sector.Nombre, indicadoresSrv.TiempoIndisponibilidadPorSector(sector.SectorId, ubicacionId));
            }

            var orderedData = chartData.OrderByDescending(x => x.Value).ToDictionary(r => r.Key, r => r.Value);

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