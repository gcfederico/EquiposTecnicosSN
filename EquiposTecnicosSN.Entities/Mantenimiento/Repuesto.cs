using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EquiposTecnicosSN.Entities.Mantenimiento
{
    [Table("Repuestos")]
    public class Repuesto
    {
        [Key]
        public int RepuestoId { get; set; }

        [DisplayName("Código")]
        public string Codigo { get; set; }

        [StringLength(255)]
        public string Nombre { get; set; }

        [ForeignKey("Proveedor")]
        public int ProveedorId { get; set; }

        public virtual Proveedor Proveedor { get; set; }

        public Decimal Costo { get; set; }
    }
}
