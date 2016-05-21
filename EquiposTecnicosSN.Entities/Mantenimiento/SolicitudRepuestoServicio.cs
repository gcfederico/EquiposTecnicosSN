using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EquiposTecnicosSN.Entities.Mantenimiento
{
    [Table("SolicitudesRepuestosServicios")]
    public class SolicitudRepuestoServicio
    {
        [Key]
        public int SolicitudRepuestoServicioId { get; set; }

        [ForeignKey("OrdenDeTrabajo")]
        public int OrdenDeTrabajoId { get; set; }

        public virtual OrdenDeTrabajo OrdenDeTrabajo { get; set; }

        [DisplayName("Fecha de inicio")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true)]
        public DateTime FechaInicio { get; set; }

        public string Comentarios { get; set; }

        [DisplayName("Fecha de cierre")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true)]
        public DateTime? FechaCierre { get; set; }

        [ForeignKey("Proveedor")]
        public int? ProveedorId { get; set; }

        [DisplayName("Cantidad")]
        public int CantidadRepuesto { get; set; }

        public virtual Proveedor Proveedor { get; set; }

        [ForeignKey("Repuesto")]
        public int RepuestoId { get; set; }

        public virtual Repuesto Repuesto { get; set; }

        public int? UsuarioSolicitudId { get; set; }
    }
}