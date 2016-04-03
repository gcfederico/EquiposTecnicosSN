using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EquiposTecnicosSN.Entities
{
    public enum TipoDeEquipo
    {
        [Display(Name = "Climatización")]
        Climatizacion,
        Respirador
    }
}