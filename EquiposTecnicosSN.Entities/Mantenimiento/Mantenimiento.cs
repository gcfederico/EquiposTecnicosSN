using EquiposTecnicosSN.Entities.Equipos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EquiposTecnicosSN.Entities.Mantenimiento
{
    [Table("Mantenimientos")]
    public class Mantenimiento
    {
        [Key]
        public int MantenimientoId { get; set; }

        [ForeignKey("Equipo")]
        public int EquipoId { get; set; }

        public virtual Equipo Equipo { get; set; }

        [Required]
        [DisplayName("Nº de referencia")]
        public int NumeroReferencia { get; set; }

        public MantenimientoEstado Estado { get; set; }

        [DisplayName("Descripción")]
        public string Descripcion { get; set; }

        [DisplayName("Fecha de inicio")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true)]
        public DateTime FechaDeInicio { get; set; }

        [DisplayName("Fecha de fin")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true)]
        public DateTime? FechaDeFin { get; set; }

        public virtual ICollection<OrdenDeTrabajo> OrdenesDeTrabajo { get; set; }

        public MantenimientoTipo Tipo { get; set; }

        public Mantenimiento()
        {
            this.FechaDeInicio = DateTime.Now;
            this.Estado = MantenimientoEstado.Abierto;
        }

    }

}
