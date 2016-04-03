using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquiposTecnicosSN.Entities
{
    [Table("Proveedores")]
    public class Proveedor
    {
        [Key]
        [Required]
        public int ProveedorId { get; set; }

        [Required]
        public string Nombre { get; set; }

        public virtual ICollection<InformacionComercial> InfoEquipos { get; set; }
    }
}
