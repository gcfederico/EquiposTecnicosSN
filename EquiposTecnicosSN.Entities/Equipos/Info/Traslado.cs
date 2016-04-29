using EquiposTecnicosSN.Entities.Equipos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquiposTecnicosSN.Entities.Equipos.Info
{
    [Table("Traslados")]
    public class Traslado
    {
        [Key]
        [ForeignKey("Equipo")]
        [Required]
        [DisplayName("Equipo")]
        public int EquipoId { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true)]
        [DisplayName("Fecha de traslado")]
        public DateTime FechaTraslado { get; set; }

        [Required]
        public int UbicacionOrigenId { get; set; }

        [Required]
        public int UbicacionDestinoId { get; set; }

        public virtual Equipo Equipo { get; set; }

        [DisplayName("Ubicación de origen")]
        public virtual Ubicacion UbicacionOrigen { get; set; }

        [DisplayName("Ubicación de destino")]
        public virtual Ubicacion UbicacionDestino { get; set; }
    }
}
