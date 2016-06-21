using EquiposTecnicosSN.Entities.Equipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EquiposTecnicosSN.Web.CustomExtensions
{
    public static class EquiposExtension
    {
        private static String EquiposClimatizacionController = "EquiposClimatizacion";
        private static String EquiposBaseController = "EquiposBase";

        public static String WebController(this Equipo equipo)
        {
            if (equipo is EquipoClimatizacion)
            {
                return EquiposClimatizacionController;
            }

            return EquiposBaseController;
        }
    }
}