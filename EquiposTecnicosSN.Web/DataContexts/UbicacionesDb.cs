using EquiposTecnicosSN.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EquiposTecnicosSN.Web.DataContexts
{
    public class UbicacionesDb : EquiposBaseDbContext
    {
        public DbSet<Ubicacion> Ubicaciones { get; set; }
    }
}