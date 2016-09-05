using EquiposTecnicosSN.Entities.Equipos.Info;
using EquiposTecnicosSN.Entities.Mantenimiento;
using System;
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
        [StringLength(255)]
        [DisplayName("Nombre completo")]
        public string NombreCompleto { get; set; }

        [Required]
        [StringLength(50)]
        public string UMDNS { get; set; }

        [DisplayName("Nº de matrícula")]
        public int? NumeroMatricula { get; set; }

        [Required]
        public int UbicacionId { get; set; }

        [ForeignKey("UbicacionId")]
        [DisplayName("Ubicación")]
        public virtual Ubicacion Ubicacion { get; set; }

        [Required]
        public int SectorId { get; set; }

        [ForeignKey("SectorId")]
        public virtual Sector Sector { get; set; }

        [Required]
        public EstadoDeEquipo Estado { get; set; }

        [ForeignKey("InformacionComercialId")]
        public virtual InformacionComercial InformacionComercial { get; set; }

        [ForeignKey("EquipoId")]
        public virtual InformacionHardware InformacionHardware { get; set; }

        public virtual ICollection<OrdenDeTrabajo> OrdenesDeTrabajo { get; set; }

        public virtual ICollection<Traslado> Traslados { get; set; }

        public abstract TipoEquipo Tipo();
    }
}