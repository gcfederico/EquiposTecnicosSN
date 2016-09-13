using EquiposTecnicosSN.Entities.Equipos;
using EquiposTecnicosSN.Entities.Mantenimiento;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EquiposTecnicosSN.Web.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class IndicadoresService : BaseService
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="umdns"></param>
        /// <param name="ubicacionId"></param>
        /// <returns></returns>
        public double TiempoIndisponibilidadEquipos(string umdns, int ubicacionId, DateTime fechaInicio, DateTime fechaFin)
        {
            var equiposIds = db.Equipos
                .Where(e => umdns == "" || e.UMDNS == umdns)
                .Where(e => ubicacionId == 0 || e.UbicacionId == ubicacionId)
                .Select(e => e.EquipoId)
                .ToList();

            double sumaTiemposEntreFallas = 0;
            foreach (var id in equiposIds)
            {
                sumaTiemposEntreFallas += TiempoIndisponibilidad(id, fechaInicio, fechaFin);
            }

            return sumaTiemposEntreFallas;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="equipoId"></param>
        /// <returns></returns>
        public double TiempoIndisponibilidad(int equipoId, DateTime fechaInicio, DateTime fechaFin)
        {

            var odtsMC = db.ODTMantenimientosCorrectivos
                .Where(odt => odt.EquipoId == equipoId)
                .Where(odt => odt.EquipoParado)
                .Where(odt => odt.FechaCierre != null)
                .Where(odt => DateTime.Compare(odt.FechaInicio, fechaInicio) > 0)
                .Where(odt => DateTime.Compare(odt.FechaCierre.Value, fechaFin) < 0)
                .ToList();

            var odtsMP = db.ODTMantenimientosPreventivos
                .Where(odt => odt.EquipoId == equipoId)
                .Where(odt => odt.FechaCierre != null)
                .Where(odt => DateTime.Compare(odt.FechaInicio, fechaInicio) > 0)
                .Where(odt => DateTime.Compare(odt.FechaCierre.Value, fechaFin) < 0)
                .ToList();

            var totalCount = odtsMC.Count + odtsMP.Count;

            if (totalCount == 0)
            {
                return 0;
            }

            var fechaCarga = db.Equipos.Find(equipoId).InformacionComercial.FechaCompra != null ? db.Equipos.Find(equipoId).InformacionComercial.FechaCompra : DateTime.MinValue;
            var tFuncionamientoEsperado = (fechaFin - fechaInicio).TotalMinutes;

            double sumaTiemposOdts = 0;
            foreach (var odt in odtsMC)
            {
                sumaTiemposOdts += (odt.FechaCierre.Value - odt.FechaInicio).TotalMinutes;
            }

            foreach (var odt in odtsMP)
            {
                sumaTiemposOdts += (odt.FechaCierre.Value - odt.FechaInicio).TotalMinutes;
            }

            return Math.Round((sumaTiemposOdts / tFuncionamientoEsperado) * 100, 4);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="umdns"></param>
        /// <param name="ubicacionId"></param>
        /// <returns></returns>
        public double TiempoMedioEntreFallasEquipos(string umdns, int ubicacionId, DateTime fechaInicio, DateTime fechaFin)
        {
            var equiposIds = db.Equipos
                .Where(e => umdns == "" || e.UMDNS == umdns)
                .Where(e => ubicacionId == 0 || e.UbicacionId == ubicacionId)
                .Select(e => e.EquipoId)
                .ToList();

            double sumaTiemposEntreFallas = 0;
            foreach (var id in equiposIds)
            {
                sumaTiemposEntreFallas += TiempoMedioEntreFallas(id, fechaInicio, fechaFin);
            }

            return sumaTiemposEntreFallas;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="equipoId"></param>
        /// <returns></returns>
        public double TiempoMedioEntreFallas(int equipoId, DateTime fechaInicio, DateTime fechaFin)
        {
            var odts = db.ODTMantenimientosCorrectivos
                .Where(odt => odt.EquipoId == equipoId)
                .Where(odt => odt.FechaCierre != null)
                .Where(odt => DateTime.Compare(odt.FechaInicio, fechaInicio) > 0)
                .Where(odt => DateTime.Compare(odt.FechaCierre.Value, fechaFin) < 0)
                .ToList();

            if (odts.Count == 0)
            {
                return 0;
            }

            double sumaTiemposEntreFallas = 0;
            for (int i = 0; i < odts.Count - 1; i++)
            {
                var current = odts[i];                    
                var next = odts[i + 1];

                sumaTiemposEntreFallas += (next.FechaInicio - current.FechaInicio).TotalDays;
            }

            return Math.Round((sumaTiemposEntreFallas / odts.Count) * 100, 4);
        }

        /// <summary>
        /// Devuelve el tiempo medio de reparación de un equipo entre las fechas indicadas.
        /// </summary>
        /// <param name="equipoId">ID del equipo.</param>
        /// <param name="fechaInicio">Fecha de inicio del período.</param>
        /// <param name="fechaFin">Fecha de fin del período.</param>
        /// <returns>Porcentaje con dos decimales.</returns>
        public double TiempoMedioDeReparacion(int equipoId, DateTime fechaInicio, DateTime fechaFin)
        {
            var odts = db.ODTMantenimientosCorrectivos
                .Where(odt => odt.EquipoId == equipoId)
                .Where(odt => odt.FechaCierre != null)
                .Where(odt => DateTime.Compare(odt.FechaInicio, fechaInicio) > 0)
                .Where(odt => DateTime.Compare(odt.FechaCierre.Value, fechaFin) < 0)
                .ToList();

            if (odts.Count == 0)
            {
                return 0;
            }

            double sumaTiemposOdts = 0;
            foreach(var odt in odts)
            {
                sumaTiemposOdts += (odt.FechaCierre.Value - odt.FechaInicio).TotalDays;
            }

            return Math.Round((sumaTiemposOdts / odts.Count) * 100, 4);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ubicacionId"></param>
        /// <param name="sectorId"></param>
        /// <param name="fechaInicioDT"></param>
        /// <param name="fechaFinDT"></param>
        /// <returns></returns>
        public Dictionary<string, double> ParetoChartDataTI(int? ubicacionId, int? sectorId, DateTime fechaInicioDT, DateTime fechaFinDT)
        {

            Dictionary<string, double> chartData = new Dictionary<string, double>();

            var equipos = db.Equipos
                .Where(e => sectorId == null || e.SectorId == sectorId)
                .Where(e => ubicacionId == null || e.UbicacionId == ubicacionId)
                .ToList();

            foreach (var equipo in equipos)
            {
                var label = equipo.NombreCompleto 
                    + (ubicacionId == null ? " - " + equipo.Ubicacion.Nombre : "") 
                    + (sectorId == null ? " - " + equipo.Sector.Nombre : "");
                chartData.Add(label, TiempoIndisponibilidad(equipo.EquipoId, fechaInicioDT, fechaFinDT));
            }

            return chartData.OrderByDescending(x => x.Value).ToDictionary(r => r.Key, r => r.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ubicacionId"></param>
        /// <param name="sectorId"></param>
        /// <param name="fechaInicioDT"></param>
        /// <param name="fechaFinDT"></param>
        /// <returns></returns>
        public Dictionary<string, double> ParetoChartDataTMR(int? ubicacionId, int? sectorId, DateTime fechaInicioDT, DateTime fechaFinDT)
        {

            Dictionary<string, double> chartData = new Dictionary<string, double>();

            var equipos = db.Equipos
                .Where(e => sectorId == null || e.SectorId == sectorId)
                .Where(e => ubicacionId == null || e.UbicacionId == ubicacionId)
                .ToList();

            foreach (var equipo in equipos)
            {
                var label = equipo.NombreCompleto
                    + (ubicacionId == null ? " - " + equipo.Ubicacion.Nombre : "")
                    + (sectorId == null ? " - " + equipo.Sector.Nombre : "");
                chartData.Add(label, TiempoMedioDeReparacion(equipo.EquipoId, fechaInicioDT, fechaFinDT));
            }

            return chartData.OrderByDescending(x => x.Value).ToDictionary(r => r.Key, r => r.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ubicacionId"></param>
        /// <param name="sectorId"></param>
        /// <param name="fechaInicioDT"></param>
        /// <param name="fechaFinDT"></param>
        /// <returns></returns>
        public Dictionary<string, double> ParetoChartDataTMEF(int? ubicacionId, int? sectorId, DateTime fechaInicioDT, DateTime fechaFinDT)
        {

            Dictionary<string, double> chartData = new Dictionary<string, double>();

            var equipos = db.Equipos
                .Where(e => sectorId == null || e.SectorId == sectorId)
                .Where(e => ubicacionId == null || e.UbicacionId == ubicacionId)
                .ToList();

            foreach (var equipo in equipos)
            {
                var label = equipo.NombreCompleto
                    + (ubicacionId == null ? " - " + equipo.Ubicacion.Nombre : "")
                    + (sectorId == null ? " - " + equipo.Sector.Nombre : "");
                chartData.Add(label, TiempoMedioEntreFallas(equipo.EquipoId, fechaInicioDT, fechaFinDT));
            }

            return chartData.OrderByDescending(x => x.Value).ToDictionary(r => r.Key, r => r.Value);
        }


    }
}