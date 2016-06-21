using EquiposTecnicosSN.Entities.Mantenimiento;
using System.Collections.Generic;

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
}