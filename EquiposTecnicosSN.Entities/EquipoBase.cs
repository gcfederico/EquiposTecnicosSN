using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EquiposTecnicosSN.Entities
{
    
    [Table("EquiposBase")]
    public abstract class EquipoBase : EntidadBase
    {
        [Key]
        [Required]
        public int Id { get; set; }

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

        public DateTime fechaCompra { get; set; }

        public int numeroInventario { get; set; }

        public virtual Ubicacion Ubicacion { get; set; }

        public EstadoDeEquipo Estado { get; set; }
    }
}