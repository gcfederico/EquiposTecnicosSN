using EquiposTecnicosSN.Entities.Equipos;
using EquiposTecnicosSN.Entities.Equipos.Info;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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

    public class BuscarEquipoViewModel
    {
        [StringLength(255)]
        [DisplayName("Nombre completo")]
        public string NombreCompleto { get; set; }

        [StringLength(50)]
        public string UMDNS { get; set; }

        [DisplayName("Nº de matrícula")]
        public int? NumeroMatricula { get; set; }

        public int UbicacionId { get; set; }

        [DisplayName("Ubicación")]
        public virtual Ubicacion Ubicacion { get; set; }

        public int SectorId { get; set; }

        public virtual Sector Sector { get; set; }

        public EstadoDeEquipo Estado { get; set; }

        public List<Equipo> Equipos { get; set; }
    }
}