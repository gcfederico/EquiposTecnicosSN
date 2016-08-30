using EquiposTecnicosSN.Entities.Mantenimiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EquiposTecnicosSN.Web.Services
{
    public class ODTsService : BaseService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<OrdenDeTrabajoMantenimientoCorrectivo> EmergenciasAbiertas()
        {
            var ordenesDeTrabajoEmergencia = db.ODTMantenimientosCorrectivos
                .Where(odt => odt.Estado == OrdenDeTrabajoEstado.Abierta || odt.Estado == OrdenDeTrabajoEstado.EsperaRepuesto)
                .Where(odt => odt.Prioridad == OrdenDeTrabajoPrioridad.Emergencia)
                .OrderBy(odt => odt.FechaInicio);

            return ordenesDeTrabajoEmergencia.ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int CorrectivosAbiertosCount()
        {
            return db.ODTMantenimientosCorrectivos
                .Where(odt => odt.Estado == OrdenDeTrabajoEstado.Abierta || odt.Estado == OrdenDeTrabajoEstado.EsperaRepuesto)
                .Count();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int PreventivosAbiertosCount()
        {
            return db.ODTMantenimientosPreventivos
               .Where(odt => odt.Estado == OrdenDeTrabajoEstado.Abierta || odt.Estado == OrdenDeTrabajoEstado.EsperaRepuesto)
               .Count();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<OrdenDeTrabajoMantenimientoPreventivo> ProximosPreventivos()
        {
            var proximos = db.ODTMantenimientosPreventivos
                .Where(odt => odt.FechaInicio >= DateTime.Now)
                .OrderBy(odt => odt.FechaInicio);

            return proximos.ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="odtId"></param>
        /// <returns></returns>
        public ICollection<SolicitudRepuestoServicio> BuscarSolicitudes(int? odtId)
        {
            return db.SolicitudesRepuestosServicios.Where(s => s.OrdenDeTrabajoId == odtId).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="odtId"></param>
        /// <returns></returns>
        public OrdenDeTrabajoMantenimientoPreventivo BuscarMPreventivo(int odtId)
        {
            return db.ODTMantenimientosPreventivos.Find(odtId);
        }

        public ICollection<GastoOrdenDeTrabajo> BuscarGastos(int ordenDeTrabajoId)
        {
            return db.GastosOrdenesDeTrabajo.Where(g => g.OrdenDeTrabajoId == ordenDeTrabajoId).ToList();
        }
    }
}