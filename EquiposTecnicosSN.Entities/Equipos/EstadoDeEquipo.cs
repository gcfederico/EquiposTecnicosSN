using System.ComponentModel.DataAnnotations;

namespace EquiposTecnicosSN.Entities.Equipos
{
    public enum EstadoDeEquipo
    {
        Funcional = 1,
        [Display(Name="No Funcional")]
        NoFuncional = 2,
        [Display(Name = "Funciona Pero Requiere Reparacion")]
        FuncionalRequiereReparacion = 3,
        [Display(Name = "No Funciona. Requiere Reparacion")]
        NoFuncionalRequiereReparacion = 4
    }
}
