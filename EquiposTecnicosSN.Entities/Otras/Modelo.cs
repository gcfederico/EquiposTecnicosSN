using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EquiposTecnicosSN.Entities.Otras
{
    [Table("Modelos")]
    public class Modelo
    {
        [Key]
        public int ModeloId { get; set; }

        [Required]
        public string Nombre { get; set; }

        public int MarcaId { get; set; }

        [ForeignKey("MarcaId")]
        public virtual Marca Marca { get; set; }
    }
}