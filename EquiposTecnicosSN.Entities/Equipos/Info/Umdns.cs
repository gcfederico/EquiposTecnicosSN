using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquiposTecnicosSN.Entities.Equipos.Info
{
    [Table(name: "UMDNS")]
    public class Umdns
    {
        [Key]
        public int UmdnsId { get; set; }

        [Required]
        public string Codigo { get; set; }

        [Required]
        public string NombreCompleto { get; set; }
    }
}
