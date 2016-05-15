using EquiposTecnicosSN.Entities.Equipos;
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

        [DisplayName("Nº Referencia")]
        public string NumeroReferencia { get; set; }

        [ForeignKey("Equipo")]
        public int EquipoId { get; set; }

        public virtual Equipo Equipo { get; set; }

        public OrdenDeTrabajoPrioridad Prioridad { get; set; }

        public OrdenDeTrabajoEstado Estado { get; set; }

        [DisplayName("Fecha de incio")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true)]
        public DateTime FechaInicio { get; set; }

        public int UsuarioInicioId { get; set; }

        public bool EquipoParado { get; set; }

        public virtual ICollection<GastoOrdenDeTrabajo> Gastos { get; set; }

        [DisplayName("Descripción del problema")]
        public string Descripcion { get; set; }

        [DisplayName("Diagnóstico")]
        public string Diagnostico { get; set; }

        [DisplayName("Detalle de la reparación")]
        public string DetalleReparacion { get; set; }

        public ICollection<SolicitudRepuesto> SolicitudesRespuestos;

        [DisplayName("Causa raíz")]
        public string CausaRaiz { get; set; }

        [DisplayName("Fecha de diagnóstico")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true)]
        public DateTime? FechaDiagnostico { get; set; }

        public int? UsuarioDiagnosticoId { get; set; }

        [DisplayName("Fecha de reparación")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true)]
        public DateTime? FechaReparacion { get; set; }

        public int? UsuarioReparacionId { get; set; }

        [DisplayName("Fecha de cierre")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true)]
        public DateTime? FechaCierre { get; set; }

        public int? UsuarioCierreId { get; set; }

    }
}
