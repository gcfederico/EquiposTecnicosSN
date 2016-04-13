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
    [Table("SolicitudesUsuario")]
    public class SolicitudUsuario
    {
        [Key]
        public int SolicitudUsuarioId { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [DisplayName("Ubicación")]
        public int UbicacionId { get; set; }

        public virtual Ubicacion Ubicacion { get; set; }
    }
}
