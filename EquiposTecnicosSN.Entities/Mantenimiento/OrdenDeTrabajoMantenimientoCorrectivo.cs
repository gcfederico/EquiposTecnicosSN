using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EquiposTecnicosSN.Entities.Mantenimiento
{
    [Table("OrdenesDeTrabajoMantenimientoCorrectivo")]
    public class OrdenDeTrabajoMantenimientoCorrectivo : OrdenDeTrabajo
    {        
        public bool EquipoParado { get; set; }
        
        [DisplayName("Descripción del problema")]
        [Required]
        [StringLength(500)]
        public string Descripcion { get; set; }

        [DisplayName("Diagnóstico")]
        [StringLength(500)]
        public string Diagnostico { get; set; }

        [DisplayName("Detalle de la reparación")]
        [StringLength(500)]
        public string DetalleReparacion { get; set; }

        [DisplayName("Causa raíz")]
        [StringLength(500)]
        public string CausaRaiz { get; set; }

        [DisplayName("Fecha de diagnóstico")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true)]
        public DateTime? FechaDiagnostico { get; set; }

        public int? UsuarioDiagnosticoId { get; set; }

        [DisplayName("Fecha de reparación")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true)]
        public DateTime? FechaReparacion { get; set; }

        public int? UsuarioReparacionId { get; set; }
        
        public bool Limpieza { get; set; }

        public bool VerificacionFuncionamiento { get; set; }
    }
}
