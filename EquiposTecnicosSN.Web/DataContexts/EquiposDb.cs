using System.Data.Entity;
using EquiposTecnicosSN.Entities;
using System.Diagnostics;

namespace EquiposTecnicosSN.Web.DataContexts
{

    public class EquiposBaseDbContext : DbContext
    {
        public EquiposBaseDbContext()
            : base("DefaultConnection")
        {
            Database.Log = log => Debug.Write(log);
        }

        public DbSet<EquipoBase> Equipos { get; set; }
    }

    public class EquiposClimatizacionDb : EquiposBaseDbContext
    {
        public DbSet<EquipoClimatizacion> EquiposDeClimatizacion { get; set; }
    }
    
    public class EquiposRespiradorDb : EquiposBaseDbContext
    {
        public DbSet<EquipoRespirador> EquiposRespiradores { get; set; }
    }
}