using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EquiposTecnicosSN.Entities.Equipos
{
    [Table("EquipamientosEdilicios")]
    public class EquipamientoEdilicio : Equipo
    {
        public override string Tipo()
        {
            return "Equipamiento Edilicio";
        }
    }
}
