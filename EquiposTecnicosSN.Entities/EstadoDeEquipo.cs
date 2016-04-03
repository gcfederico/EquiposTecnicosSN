using System.ComponentModel.DataAnnotations;

namespace EquiposTecnicosSN.Entities
{
    public enum EstadoDeEquipo
    {
        Funcional,
        [Display(Name="No Funcional")]
        NoFuncional,
        [Display(Name = "Funciona Pero Requiere Reparacion")]
        FuncionalRequiereReparacion,
        [Display(Name = "No Funciona. Requiere Reparacion")]
        NoFuncionalRequiereReparacion
    }
}
