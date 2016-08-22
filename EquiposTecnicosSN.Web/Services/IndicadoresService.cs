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
        public double TiempoMedioEntreFallas(int equipoId)
        {
            var odtsMp = equiposDb.ODTMantenimientosCorrectivos.Where(odt => odt.EquipoId == equipoId && odt.FechaCierre != null).ToList();

            if (odtsMp.Count == 0)
            {
                return 0;
            }

            double sumaTiemposEntreFallas = 0;
            for (int i = 0; i < odtsMp.Count; i++)
            {
                var current = odtsMp[i];                    
                var next = odtsMp[i + 1];

                sumaTiemposEntreFallas += (next.FechaInicio - current.FechaInicio).TotalDays;
            }

            return sumaTiemposEntreFallas / odtsMp.Count;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="equipoId"></param>
        /// <returns></returns>
        public double TiempoMedioDeReparacion(int equipoId)
        {
            var odtsMp = equiposDb.ODTMantenimientosCorrectivos.Where(odt => odt.EquipoId == equipoId && odt.FechaCierre != null).ToList();

            if (odtsMp.Count == 0)
            {
                return 0;
            }

            double sumaTiemposOdts = 0;
            foreach(var odt in odtsMp)
            {
                sumaTiemposOdts += (odt.FechaCierre.Value - odt.FechaInicio).TotalDays;
            }

            return sumaTiemposOdts / odtsMp.Count;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sectorId"></param>
        /// <returns></returns>
        public double TiempoMedioDeReparacionPorSector(int sectorId)
        {
            var equipos = equiposDb.Equipos.Where(e => e.SectorId == sectorId).ToList();

            double tmrPorSector = 0;
            foreach(var equipo in equipos)
            {
                tmrPorSector += TiempoMedioDeReparacion(equipo.EquipoId);
            }

            return tmrPorSector;
        }
    }
}