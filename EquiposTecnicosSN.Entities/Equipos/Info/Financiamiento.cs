using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquiposTecnicosSN.Entities.Equipos.Info
{
    public enum Financiamiento
    {
        Ninguno = 0,
        [Display(Name = "Fondos Provinciales")]
        FondosProvinciales = 1,
        [Display(Name = "Donación de Nación")]
        DonacionNacion = 2,
        [Display(Name = "Donación de Particular")]
        DonacionParticular = 3
    }
}
