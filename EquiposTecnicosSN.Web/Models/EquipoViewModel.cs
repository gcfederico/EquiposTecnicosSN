using EquiposTecnicosSN.Entities;
using EquiposTecnicosSN.Entities.Comercial;
using EquiposTecnicosSN.Entities.Equipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EquiposTecnicosSN.Web.Models
{
    public class EquipoViewModel
    {
        // Solamente usados en EquipoClimatizacionViewModel
        public IEnumerable<SelectListItem> Ubicaciones { get; set; }
        public int UbicacionSelectedId { get; set; }

        // Generales
        public InformacionComercial InformacionComercial { get; set; }
    }

    public class EquipocClimatizacionViewModel : EquipoViewModel
    {
        public EquipoClimatizacion Equipo { get; set; }
        public IEnumerable<SelectListItem> UbicacionId { get; set; }
    }
}