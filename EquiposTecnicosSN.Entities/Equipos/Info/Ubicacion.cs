using EquiposTecnicosSN.Entities.Equipos;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EquiposTecnicosSN.Entities.Equipos.Info
{
    [Table("Ubicaciones")]
    public class Ubicacion
    {
        [Key]
        [Required]
        public int UbicacionId { get; set; }

        [Required]
        [MaxLength(255)]
        [DisplayName("Nombre completo")]
        public string NombreCompleto { get; set; }

        public virtual ICollection<Equipo> EquiposTecnicos { get; set; }
        
        public Ubicacion()
        {
            EquiposTecnicos = new LinkedList<Equipo>();
        }
    }
}
