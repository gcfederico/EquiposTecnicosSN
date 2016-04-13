using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EquiposTecnicosSN.Entities
{
    [Table("OrdenesDeTrabajo")]
    public class OrdenDeTrabajo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrdenDeTrabajoId { get; set; }

        [ForeignKey("MantenimientoDeEquipo")]
        public int MantenimientoDeEquipoId { get; set; }

        public virtual MantenimientoEquipo MantenimientoDeEquipo { get; set; }

        [DisplayName("Diagnóstico")]
        public string Diagnostico { get; set; }

        [DisplayName("Resolución")]
        public string Resolucion { get; set; }

        public int? ProveedorId { get; set; }

        [DisplayName("Proveedor")]
        [ForeignKey("ProveedorId")]
        public virtual Proveedor Proveedor { get; set; }

        public Dictionary<string, int> Gastos { get; set; }

    }
}
