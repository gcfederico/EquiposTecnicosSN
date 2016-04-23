using EquiposTecnicosSN.Entities.Otras;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EquiposTecnicosSN.Entities.Mantenimientos
{
    [Table("OrdenesDeTrabajo")]
    public class OrdenDeTrabajo
    {
        [Key]
        public int OrdenDeTrabajoId { get; set; }

        [ForeignKey("Mantenimiento")]
        public int MantenimientoId { get; set; }

        public virtual Mantenimiento Mantenimiento { get; set; }

        [DisplayName("Diagnóstico")]
        public string Diagnostico { get; set; }

        [DisplayName("Resolución")]
        public string Resolucion { get; set; }

        public int? ProveedorId { get; set; }

        [DisplayName("Proveedor")]
        [ForeignKey("ProveedorId")]
        public virtual Proveedor Proveedor { get; set; }

        public virtual ICollection<GastoOrdenDeTrabajo> Gastos { get; set; }
    }
}
