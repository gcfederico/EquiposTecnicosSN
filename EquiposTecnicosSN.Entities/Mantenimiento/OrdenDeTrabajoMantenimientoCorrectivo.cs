using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EquiposTecnicosSN.Entities.Mantenimiento
{
    /// <summary>
    /// Clase que representa una orden de trabajo para mantenimiento correctivo.
    /// </summary>
    [Table("OrdenesDeTrabajoMantenimientoCorrectivo")]
    public class OrdenDeTrabajoMantenimientoCorrectivo : OrdenDeTrabajo
    {        
        /// <summary>
        /// Descripción del problema que dispara la orden de trabajo.
        /// </summary>
        [DisplayName("Descripción del problema")]
        [Required(ErrorMessage = "La descripción del problema es requerida.")]
        [StringLength(500)]
        public string Descripcion { get; set; }
        /// <summary>
        /// Diagnóstico del problema.
        /// </summary>
        [DisplayName("Diagnóstico")]
        [StringLength(500)]
        public string Diagnostico { get; set; }
        /// <summary>
        /// Fecha en la que se realiza el diagnóstico del problema.
        /// </summary>
        [DisplayName("Fecha de diagnóstico")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true)]
        public DateTime? FechaDiagnostico { get; set; }
        /// <summary>
        /// Usuario que realiza el diagnóstico del problema.
        /// </summary>
        public string UsuarioDiagnostico { get; set; }
        /// <summary>
        /// Detalle de la reparación.
        /// </summary>
        [DisplayName("Detalle de la reparación")]
        [StringLength(500)]
        public string DetalleReparacion { get; set; }
        /// <summary>
        /// Fecha en la que se realiza la reparación.
        /// </summary>
        [DisplayName("Fecha de reparación")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true)]
        public DateTime? FechaReparacion { get; set; }
        /// <summary>
        /// Usuario que realiza la reparación.
        /// </summary>
        public string UsuarioReparacion { get; set; }
        /// <summary>
        /// Cauza raíz que generó el problema. Se completa al moento del cierre de la orden de trabajo.
        /// </summary>
        [DisplayName("Causa raíz")]
        [StringLength(500)]
        public string CausaRaiz { get; set; }
        /// <summary>
        /// True si se realiza una limpieza del equipo luego de la reparaciónal momento del cierre de la orden de trabajo.
        /// </summary>
        public bool Limpieza { get; set; }
        /// <summary>
        /// True si el equipo queda funcionando al momento del cierre de la orden de trabajo.
        /// </summary>
        public bool VerificacionFuncionamiento { get; set; }
        /// <summary>
        /// True si el equipo se encuentra parado en el momento en que se crea la orden de trabajo.
        /// </summary>
        public bool EquipoParado { get; set; }
    }
}
