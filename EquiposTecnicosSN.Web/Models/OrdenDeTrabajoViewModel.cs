using EquiposTecnicosSN.Entities.Mantenimiento;
using System.Collections.Generic;
using System.ComponentModel;
using System;

namespace EquiposTecnicosSN.Web.Models
{
    public class OrdenDeTrabajoViewModel
    {

    }

    public class NewOrdenDeTrabajoViewModel
    {
        public OrdenDeTrabajoMantenimientoCorrectivo NewOrden { get; set; }

        public ICollection<GastoOrdenDeTrabajo> Gastos { get; set; }
    }

    public abstract class MViewModel
    {
        [DisplayName("Nueva Observación")]
        public ObservacionOrdenDeTrabajo NuevaObservacion { get; set; }
    }

    public class MCViewModel : MViewModel
    {

        public  OrdenDeTrabajoMantenimientoCorrectivo Odt { get; set; }

        //[DisplayName("Nueva Observación")]
        //public ObservacionOrdenDeTrabajo NuevaObservacion { get; set; }
    }

    public class MPViewModel : MViewModel
    {

        public  OrdenDeTrabajoMantenimientoPreventivo Odt { get; set; }

        //[DisplayName("Nueva Observación")]
        //public ObservacionOrdenDeTrabajo NuevaObservacion { get; set; }
    }
}