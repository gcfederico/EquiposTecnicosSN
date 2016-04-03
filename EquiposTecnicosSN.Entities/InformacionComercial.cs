using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquiposTecnicosSN.Entities
{
    [Table("InformacionComercial")]
    public class InformacionComercial
    {
        [Key, ForeignKey("Equipo")]
        public int EquipoId { get; set; }

        public virtual Equipo Equipo { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaCompra { get; set; }

        public int PrecioCompra { get; set; }

        public int ValorRestante { get; set; }

        public GarantiaContrato EsGrantiaContrato { get;set; }

        public DateTime FechaFinGarantia { get; set; }

        public string NotasGarantia { get; set; }

        public int ProveedorId { get; set; }

        [Required, ForeignKey("ProveedorId")]
        public virtual Proveedor Proveedor { get; set; }

        public InformacionComercial()
        {
            this.FechaCompra = DateTime.Now;
            this.FechaFinGarantia = DateTime.Now;
        }

    }
}

