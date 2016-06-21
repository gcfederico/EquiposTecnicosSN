using System.ComponentModel.DataAnnotations;

namespace EquiposTecnicosSN.Entities.Mantenimiento
{
    public enum OrdenDeTrabajoEstado
    {
        Abierta = 0,
        [Display(Name ="A la espera de repuestos")]
        EsperaRepuesto = 1,
        Cerrada = 2,
        Cancelada = 3
    }
}