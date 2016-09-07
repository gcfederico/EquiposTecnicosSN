using EquiposTecnicosSN.Entities.Equipos;
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
                sumaTiemposEntreFallas += TiempoMedioEntreFallas(id, fechaInicio, fechaFin);
            }

            return sumaTiemposEntreFallas;
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
            var odts = db.OrdenesDeTrabajo
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

            return sumaTiemposEntreFallas / odts.Count;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="equipoId"></param>
        /// <returns></returns>
        public double TiempoMedioDeReparacion(int equipoId, DateTime fechaInicio, DateTime fechaFin)
        {
            var odts = db.OrdenesDeTrabajo
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

            return sumaTiemposOdts / odts.Count;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="equipoId"></param>
        /// <returns></returns>
        public double TiempoIndisponibilidad(int equipoId, DateTime fechaInicio, DateTime fechaFin)
        {
            var odts = db.OrdenesDeTrabajo
                .Where(odt => odt.EquipoId == equipoId)
                .Where(odt => odt.FechaCierre != null)
                .Where(odt => DateTime.Compare(odt.FechaInicio, fechaInicio) > 0)
                .Where(odt => DateTime.Compare(odt.FechaCierre.Value, fechaFin) < 0)
                .ToList();

            if (odts.Count == 0)
            {
                return 0;
            }

            var fechaCarga = db.Equipos.Find(equipoId).InformacionComercial.FechaCompra != null ? db.Equipos.Find(equipoId).InformacionComercial.FechaCompra : DateTime.MinValue;
            var tFuncionamientoEsperado = (fechaFin - fechaInicio).TotalMinutes;

            double sumaTiemposOdts = 0;
            foreach (var odt in odts)
            {
                sumaTiemposOdts += (odt.FechaCierre.Value - odt.FechaInicio).TotalMinutes;
            }

            return Math.Round((sumaTiemposOdts / tFuncionamientoEsperado) * 100, 2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sectorId"></param>
        /// <returns></returns>
        public double TiempoMedioDeReparacionPorSector(int sectorId, int? ubicacionId, DateTime fechaInicio, DateTime fechaFin)
        {
            var equipos = db.Equipos
                .Where(e => e.SectorId == sectorId)
                .Where(e => ubicacionId == null || e.UbicacionId == ubicacionId)
                .ToList();

            double tmrPorSector = 0;
            foreach(var equipo in equipos)
            {
                tmrPorSector += TiempoMedioDeReparacion(equipo.EquipoId, fechaInicio, fechaFin);
            }

            return tmrPorSector;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sectorId"></param>
        /// <param name="ubicacionId"></param>
        /// <returns></returns>
        public double TiempoMedioEntreFallasPorSector(int sectorId, int? ubicacionId, DateTime fechaInicio, DateTime fechaFin)
        {
            var equipos = db.Equipos
                .Where(e => e.SectorId == sectorId)
                .Where(e => ubicacionId == null || e.UbicacionId == ubicacionId)
                .ToList();

            double tmefPorSector = 0;
            foreach (var equipo in equipos)
            {
                tmefPorSector += TiempoMedioEntreFallas(equipo.EquipoId, fechaInicio, fechaFin);
            }

            return tmefPorSector;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sectorId"></param>
        /// <param name="ubicacionId"></param>
        /// <returns></returns>
        public double TiempoIndisponibilidadPorSector(int sectorId, int? ubicacionId, DateTime fechaInicio, DateTime fechaFin)
        {
            var equipos = db.Equipos
                .Where(e => e.SectorId == sectorId)
                .Where(e => ubicacionId == null || e.UbicacionId == ubicacionId)
                .ToList();

            double tmefPorSector = 0;
            foreach (var equipo in equipos)
            {
                tmefPorSector += TiempoIndisponibilidad(equipo.EquipoId, fechaInicio, fechaFin);
            }

            return tmefPorSector;
        }
    }
}