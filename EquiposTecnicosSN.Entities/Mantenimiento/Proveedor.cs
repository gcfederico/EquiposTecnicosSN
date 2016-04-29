using EquiposTecnicosSN.Entities.Equipos.Info;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EquiposTecnicosSN.Entities.Mantenimiento
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
