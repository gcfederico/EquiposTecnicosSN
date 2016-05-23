using System.ComponentModel.DataAnnotations;

namespace EquiposTecnicosSN.Entities.Mantenimiento
{
    public enum OrdenDeTrabajoEstado
    {
        Abierto = 0,
        [Display(Name ="A la espera de repuestos")]
        EsperaRepuesto = 1,
        Cerrada = 2,
        Cancelado = 3
    }
}