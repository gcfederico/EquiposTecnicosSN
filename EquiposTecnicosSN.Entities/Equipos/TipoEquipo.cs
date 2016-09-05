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
        [Display(Name = "Equipo de Climatización")]
        Climatizacion = 1,
        [Display(Name = "Equipo de Cirugía")]
        Cirugia = 2,
        [Display(Name = "Equipo de Endoscopía")]
        Endoscopia = 3,
        [Display(Name = "Equipamiento Edilicio")]
        Edilicio = 4,
        [Display(Name = "Equipo de Soporte de Vida")]
        SoporteDeVida = 5,
        [Display(Name = "Equipo de Gases Medicinales")]
        GasesMedicinales = 6,
        [Display(Name = "Equipo de Imágenes")]
        Imagenes = 7,
        [Display(Name = "Equipo de Luces")]
        Luces = 8,
        [Display(Name = "Equipo de Monitoreo")]
        Monitoreo = 9,
        [Display(Name = "Equipo de Informática")]
        Informatica = 10,
        [Display(Name = "Equipo de Odontología")]
        Odontologia = 11,
        [Display(Name = "Equipo de Pruebas de Diagnóstico")]
        PruebasDeDiagnostico = 12,
        [Display(Name = "Equipo de Rehabilitacion")]
        Rehabilitacion = 13,
        [Display(Name = "Equipo de Terapéutica")]
        Terapeutica = 14,
        [Display(Name = "Otros Equipos")]
        Otros = 15
    }
}
