using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquiposTecnicosSN.Entities.Equipos
{
    public enum TipoEquipo
    {
        [Display(Name = "Equipo de Cirugía")]
        Climatizacion,
        [Display(Name = "Equipo de Cirugía")]
        Cirugia,
        [Display(Name = "Equipo de Endoscopía")]
        Endoscopia,
        [Display(Name = "Equipamiento Edilicio")]
        Edilicio,
        [Display(Name = "Equipo de Soporte de Vida")]
        SoporteDeVida,
        [Display(Name = "Equipo de Gases Medicinales")]
        GasesMedicinales,
        [Display(Name = "Equipo de Imágenes")]
        Imagenes,
        [Display(Name = "Equipo de Luces")]
        Luces,
        [Display(Name = "Equipo de Monitoreo")]
        Monitoreo,
        [Display(Name = "Equipo de Informática")]
        Informatica,
        [Display(Name = "Equipo de Odontología")]
        Odontologia,
        [Display(Name = "Equipo de Pruebas de Diagnóstico")]
        PruebasDeDiagnostico,
        [Display(Name = "Equipo de Rehabilitacion")]
        Rehabilitacion,
        [Display(Name = "Equipo de Terapéutica")]
        Terapeutica,
        [Display(Name = "Otros Equipos")]
        Otros
    }
}
