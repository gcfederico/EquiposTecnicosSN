using System;
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
        /// <param name="equipoId"></param>
        /// <returns></returns>
        private double TiempoMedioEntreFallas(int equipoId)
        {
            var odtsMc = db.ODTMantenimientosCorrectivos.Where(odt => odt.EquipoId == equipoId && odt.FechaCierre != null).ToList();

            if (odtsMc.Count == 0)
            {
                return 0;
            }

            double sumaTiemposEntreFallas = 0;
            for (int i = 0; i < odtsMc.Count - 1; i++)
            {
                var current = odtsMc[i];                    
                var next = odtsMc[i + 1];

                sumaTiemposEntreFallas += (next.FechaInicio - current.FechaInicio).TotalDays;
            }

            return sumaTiemposEntreFallas / odtsMc.Count;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="equipoId"></param>
        /// <returns></returns>
        private double TiempoMedioDeReparacion(int equipoId)
        {
            var odtsMc = db.ODTMantenimientosCorrectivos.Where(odt => odt.EquipoId == equipoId && odt.FechaCierre != null).ToList();

            if (odtsMc.Count == 0)
            {
                return 0;
            }

            double sumaTiemposOdts = 0;
            foreach(var odt in odtsMc)
            {
                sumaTiemposOdts += (odt.FechaCierre.Value - odt.FechaInicio).TotalDays;
            }

            return sumaTiemposOdts / odtsMc.Count;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="equipoId"></param>
        /// <returns></returns>
        private double TiempoIndisponibilidad(int equipoId)
        {
            var odtsMc = db.ODTMantenimientosCorrectivos.Where(odt => odt.EquipoId == equipoId && odt.FechaCierre != null).ToList();

            if (odtsMc.Count == 0)
            {
                return 0;
            }

            var fechaCarga = db.Equipos.Find(equipoId).InformacionComercial.FechaCompra != null ? db.Equipos.Find(equipoId).InformacionComercial.FechaCompra : DateTime.MinValue;
            var tFuncionamientoEsperado = (DateTime.Now - fechaCarga.Value).TotalDays;

            double sumaTiemposOdts = 0;
            foreach (var odt in odtsMc)
            {
                sumaTiemposOdts += (odt.FechaCierre.Value - odt.FechaInicio).TotalDays;
            }

            return sumaTiemposOdts / tFuncionamientoEsperado;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sectorId"></param>
        /// <returns></returns>
        public double TiempoMedioDeReparacionPorSector(int sectorId, int? ubicacionId)
        {
            var equipos = db.Equipos
                .Where(e => e.SectorId == sectorId)
                .Where(e => ubicacionId == null || e.UbicacionId == ubicacionId)
                .ToList();

            double tmrPorSector = 0;
            foreach(var equipo in equipos)
            {
                tmrPorSector += TiempoMedioDeReparacion(equipo.EquipoId);
            }

            return tmrPorSector;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sectorId"></param>
        /// <param name="ubicacionId"></param>
        /// <returns></returns>
        public double TiempoMedioEntreFallasPorSector(int sectorId, int? ubicacionId)
        {
            var equipos = db.Equipos
                .Where(e => e.SectorId == sectorId)
                .Where(e => ubicacionId == null || e.UbicacionId == ubicacionId)
                .ToList();

            double tmefPorSector = 0;
            foreach (var equipo in equipos)
            {
                tmefPorSector += TiempoMedioEntreFallas(equipo.EquipoId);
            }

            return tmefPorSector;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sectorId"></param>
        /// <param name="ubicacionId"></param>
        /// <returns></returns>
        public double TiempoIndisponibilidadPorSector(int sectorId, int? ubicacionId)
        {
            var equipos = db.Equipos
                .Where(e => e.SectorId == sectorId)
                .Where(e => ubicacionId == null || e.UbicacionId == ubicacionId)
                .ToList();

            double tmefPorSector = 0;
            foreach (var equipo in equipos)
            {
                tmefPorSector += TiempoIndisponibilidad(equipo.EquipoId);
            }

            return tmefPorSector;
        }
    }
}