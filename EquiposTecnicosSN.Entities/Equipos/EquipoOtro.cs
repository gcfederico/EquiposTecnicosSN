﻿using System.ComponentModel.DataAnnotations.Schema;

namespace EquiposTecnicosSN.Entities.Equipos
{
    [Table("EquiposOtros")]
    public class EquipoOtro : Equipo
    {
        public override TipoEquipo Tipo()
        {
            return TipoEquipo.Otros;
        }
    }
}
