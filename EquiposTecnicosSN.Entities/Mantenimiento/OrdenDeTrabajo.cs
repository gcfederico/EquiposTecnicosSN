using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EquiposTecnicosSN.Entities.Mantenimiento
{
    [Table("OrdenesDeTrabajo")]
    public class OrdenDeTrabajo
    {
        [Key]
        public int OrdenDeTrabajoId { get; set; }

        [ForeignKey("Mantenimiento")]
        public int MantenimientoId { get; set; }

        public virtual MantenimientoEquipo Mantenimiento { get; set; }

        [DisplayName("Diagnóstico")]
        public string Diagnostico { get; set; }

        [DisplayName("Resolución")]
        public string Resolucion { get; set; }

        public int? ProveedorId { get; set; }

        [DisplayName("Fecha de creación")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true)]
        public DateTime? FechaCreacion { get; set; }

        [DisplayName("Proveedor")]
        [ForeignKey("ProveedorId")]
        public virtual Proveedor Proveedor { get; set; }

        public virtual ICollection<GastoOrdenDeTrabajo> Gastos { get; set; }
    }
}
