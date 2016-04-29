using EquiposTecnicosSN.Entities.Equipos;
using EquiposTecnicosSN.Entities.Equipos.Info;
using System.Collections.Generic;
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