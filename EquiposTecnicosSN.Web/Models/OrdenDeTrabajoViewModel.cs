using EquiposTecnicosSN.Entities.Mantenimientos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EquiposTecnicosSN.Web.Models
{
    public class OrdenDeTrabajoViewModel
    {

    }

    public class NewOrdenDeTrabajoViewModel
    {
        public OrdenDeTrabajo NewOrden { get; set; }

        public ICollection<GastoOrdenDeTrabajo> Gastos { get; set; }
    }
}