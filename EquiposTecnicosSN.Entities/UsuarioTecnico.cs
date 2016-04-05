using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquiposTecnicosSN.Entities
{
    [Table("UsuariosTecnicos")]
    public class UsuarioTecnico
    {
        [Key]
        [Required]
        public int UsuarioTecnicoId { get; set; }

        [Required]
        public string Email { get; set; }

        public int UbicacionId { get; set; }

        [ForeignKey("UbicacionId")]
        [DisplayName("Ubicación")]
        public virtual Ubicacion Ubicacion { get; set; }

        public int ApplicationUserId { get; set; }
    }
}
