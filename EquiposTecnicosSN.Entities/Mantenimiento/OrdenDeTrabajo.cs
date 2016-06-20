using EquiposTecnicosSN.Entities.Equipos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EquiposTecnicosSN.Entities.Mantenimiento
{
    [Table("OrdenesDeTrabajo")]
    public abstract class OrdenDeTrabajo
    {
        [Key]
        [Required]
        public int OrdenDeTrabajoId { get; set; }

        [DisplayName("Nº Referencia")]
        [Required]
        public string NumeroReferencia { get; set; }
        
        [Required]
        public int EquipoId { get; set; }

        [ForeignKey("EquipoId")]
        [Column("Equipo")]
        public virtual Equipo Equipo { get; set; }

        [DisplayName("Fecha de incio")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true)]
        [Required]
        public DateTime FechaInicio { get; set; }

        [Required]
        public int UsuarioInicioId { get; set; }

        public virtual ICollection<GastoOrdenDeTrabajo> Gastos { get; set; }

        public virtual ICollection<SolicitudRepuestoServicio> SolicitudesRespuestos { get; set; }

        public string Observaciones { get; set; }

        [DisplayName("Fecha de cierre")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true)]
        public DateTime? FechaCierre { get; set; }

        public int? UsuarioCierreId { get; set; }

        public OrdenDeTrabajoPrioridad Prioridad { get; set; }

        public OrdenDeTrabajoEstado Estado { get; set; }
    }
}
