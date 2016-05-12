using EquiposTecnicosSN.Entities.Equipos.Info;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EquiposTecnicosSN.Entities.Equipos
{

    [Table("Equipos")]
    public abstract class Equipo
    {
        [Key]
        [Required]
        public int EquipoId { get; set; }

        [Required]
        [MaxLength(255)]
        [DisplayName("Nombre completo")]
        public string NombreCompleto { get; set; }

        [Required]
        [MaxLength(50)]
        public string UMDNS { get; set; }

        [DisplayName("Nº de matrícula")]
        public int? NumeroMatricula { get; set; }

        [Required]
        public int UbicacionId { get; set; }

        [ForeignKey("UbicacionId")]
        [DisplayName("Ubicación")]
        public virtual Ubicacion Ubicacion { get; set; }

        public EstadoDeEquipo Estado { get; set; }

        [ForeignKey("InformacionComercialId")]
        public virtual InformacionComercial InformacionComercial { get; set; }

        [ForeignKey("EquipoId")]
        public virtual InformacionHardware InformacionHardware { get; set; }

        public virtual ICollection<Mantenimiento.MantenimientoEquipo> HistorialDeMantenimientos { get; set; }

        public virtual ICollection<Traslado> Traslados { get; set; }

    }
}