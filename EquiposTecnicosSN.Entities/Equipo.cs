using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EquiposTecnicosSN.Entities
{
    
    [Table("Equipos")]
    public abstract class Equipo
    {
        [Key]
        [Required]
        public int EquipoId { get; set; }

        [Required]
        [MaxLength(255)]
        [DisplayName("Nombre completo")]
        public string NombreCompleto { get; set; }

        [Required]
        [MaxLength(50)]
        public string UMDNS { get; set; }

        [MaxLength(255)]
        [DisplayName("Nº de serie")]
        public string NumeroSerie { get; set;  }

        [MaxLength(255)]
        public string Modelo { get; set; }

        [DisplayName("Nº de inventario")]
        public int NumeroInventario { get; set; }

        [Required]
        public int UbicacionId { get; set; }

        [ForeignKey("UbicacionId")]
        [DisplayName("Ubicación")]
        public virtual Ubicacion Ubicacion { get; set; }

        public EstadoDeEquipo Estado { get; set; }

        public virtual InformacionComercial InformacionComercial { get; set; }

        public virtual ICollection<MantenimientoEquipo> HistorialDeMantenimientos { get; set; }
    }
}