using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquiposTecnicosSN.Entities.Mantenimientos
{
    [Table("GastosOrdenesDeTrabajo")]
    public class GastoOrdenDeTrabajo
    {
        [Key]
        public int GastoOrdenDeTrabajoId { get; set; }

        [ForeignKey("OrdenDeTrabajo")]
        [Required]
        public int OrdenDeTrabajoId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Concepto { get; set; }

        [Required]
        public int Monto { get; set; }

        public virtual OrdenDeTrabajo OrdenDeTrabajo { get; set; }
    }
}
