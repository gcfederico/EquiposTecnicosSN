using EquiposTecnicosSN.Entities.Mantenimiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EquiposTecnicosSN.Web.CustomExtensions
{
    public static class ODTExtension
    {
        private static String OdtMCorrectivoController = "ODTMantenimientoCorrectivo";
        private static String OdtMPreventivoController = "ODTMantenimientoPreventivo";
        private static String OdtBaseController = "OrdenesDeTrabajo";

        public static String WebController(this OrdenDeTrabajo odt)
        {

            if (odt is OrdenDeTrabajoMantenimientoCorrectivo)
            {
                return OdtMCorrectivoController;
            }

            if (odt is OrdenDeTrabajoMantenimientoPreventivo)
            {
                return OdtMPreventivoController;
            }

            return OdtBaseController;
        }
    }
}