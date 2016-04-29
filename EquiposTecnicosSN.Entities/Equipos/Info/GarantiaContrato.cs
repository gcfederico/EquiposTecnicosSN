using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquiposTecnicosSN.Entities.Equipos.Info
{
    public enum GarantiaContrato
    {
        Ninguna = 0,
        [Display(Name="Garantía")]
        Garantia = 1,
        Contrato = 2
    }
}
