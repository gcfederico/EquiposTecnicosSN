using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EquiposTecnicosSN.Entities
{
    
    [Table("Equipos")]
    public abstract class Equipo : EntidadBase
    {
        [Key]
        [Required]
        public int EquipoId { get; set; }

        [Required]
        [MaxLength(255)]
        public string NombreCompleto { get; set; }

        [Required]
        [MaxLength(50)]
        public string UMDNS { get; set; }

        [Required]
        public TipoDeEquipo Tipo { get; set; }

        [MaxLength(255)]
        public string NumeroSerie { get; set;  }

        [MaxLength(255)]
        public string Modelo { get; set; }

        public int NumeroInventario { get; set; }

        [Required]
        public int UbicacionId { get; set; }

        [ForeignKey("UbicacionId")]
        public virtual Ubicacion Ubicacion { get; set; }

        public EstadoDeEquipo Estado { get; set; }

        public virtual InformacionComercial InformacionComercial { get; set; }
    }
}