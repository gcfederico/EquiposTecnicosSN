using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EquiposTecnicosSN.Entities.Mantenimiento
{
    [Table("SolicitudesRepuestos")]
    public class SolicitudRepuesto
    {
        [Key]
        public int SolicitudRepuestoId { get; set; }

        [DisplayName("Fecha de solicitud")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true)]
        public DateTime FechaSolicitud { get; set; }

        public string Comentarios { get; set; }

        [ForeignKey("Repuesto")]
        public int RepuestoId { get; set; }

        public virtual Repuesto Repuesto { get; set; }
    }
}