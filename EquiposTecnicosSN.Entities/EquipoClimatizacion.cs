﻿using System.ComponentModel.DataAnnotations.Schema;

namespace EquiposTecnicosSN.Entities
{
    [Table("EquiposClimatizacion")]
    public class EquipoClimatizacion : Equipo
    {
        public EquipoClimatizacion() : base()
        {
            this.Tipo = TipoDeEquipo.Climatizacion;
        }
    }
}