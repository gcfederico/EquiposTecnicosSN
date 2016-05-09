using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using EquiposTecnicosSN.Entities.Equipos;
using EquiposTecnicosSN.Web.DataContexts;


namespace EquiposTecnicosSN.Web.Services
{
    public class EquiposService
    {
        private EquiposDbContext equiposDb = new EquiposDbContext();
        private IdentityDb identityDb = new IdentityDb();

        public List<EquipoClimatizacion> EquiposClimatizacionDeUbicacion(int ubicacionId)
        {
            var equiposC = equiposDb.EquiposDeClimatizacion.
                Include(e => e.InformacionComercial).
                Include(e => e.Ubicacion).
                Include(e => e.HistorialDeMantenimientos);

            if (ubicacionId != 0)
            {
                equiposC = equiposC.Where(e => e.UbicacionId == ubicacionId);
            }

            return equiposC.ToList();
        }
    }
}