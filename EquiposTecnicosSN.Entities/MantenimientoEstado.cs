using System.ComponentModel.DataAnnotations;

namespace EquiposTecnicosSN.Entities
{
    public enum MantenimientoEstado
    {
        Abierto = 1,
        Cerrado = 2,
        Cancelado = 3,
        [Display(Name ="A la espera de repuestos")]
        EsperaRepuesto = 4
    }
}