using System.ComponentModel.DataAnnotations;

namespace EquiposTecnicosSN.Entities.Mantenimiento
{
    public enum OrdenDeTrabajoEstado
    {
        Abierto = 1,
        Cerrado = 2,
        Cancelado = 3,
        [Display(Name ="A la espera de repuestos")]
        EsperaRepuesto = 4
    }
}