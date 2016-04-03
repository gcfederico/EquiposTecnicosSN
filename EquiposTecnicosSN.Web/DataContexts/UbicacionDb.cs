using EquiposTecnicosSN.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EquiposTecnicosSN.Web.DataContexts
{
    public class UbicacionDb : EquiposDbContext
    {
        public DbSet<Ubicacion> UbicacionesList { get; set; }
    }
}