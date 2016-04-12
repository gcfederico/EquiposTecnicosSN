﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true, ConvertEmptyStringToNull =true)]
        [DisplayName("Fecha de compra")]
        public DateTime? FechaCompra { get; set; }

        [DisplayName("Precio de compra")]
        public int? PrecioCompra { get; set; }

        [DisplayName("Valor restante")]
        public int? ValorRestante { get; set; }

        [DisplayName("Garantía/Contrato")]
        public GarantiaContrato EsGrantiaContrato { get;set; }

        [DisplayName("Fecha de fin de garantía")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true)]
        public DateTime? FechaFinGarantia { get; set; }

        [DisplayName("Notas de garantía")]
        public string NotasGarantia { get; set; }

        public int ProveedorId { get; set; }

        [Required]
        [ForeignKey("ProveedorId")]
        [DisplayName("Proveedor")]
        public virtual Proveedor Proveedor { get; set; }

    }
}

