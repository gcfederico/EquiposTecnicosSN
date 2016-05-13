using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquiposTecnicosSN.Entities.Mantenimiento
{
    [Table("Repuestos")]
    public class Repuesto
    {
        [Key]
        public int RepuestoId { get; set; }

        [DisplayName("Código")]
        public string Codigo { get; set; }

        public string Nombre { get; set; }

        public Decimal Costo { get; set; }
    }
}
