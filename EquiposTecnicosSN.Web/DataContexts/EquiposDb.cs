using System.Data.Entity;
using EquiposTecnicosSN.Entities;
using System.Diagnostics;

namespace EquiposTecnicosSN.Web.DataContexts
{

    public class EquiposDbContext : DbContext
    {
        public EquiposDbContext()
            : base("DefaultConnection")
        {
            Database.Log = log => Debug.Write(log);
        }

        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<EquipoClimatizacion> EquiposDeClimatizacion { get; set; }
        public DbSet<EquipoRespirador> EquiposRespiradores { get; set; }
        public DbSet<Ubicacion> Ubicaciones { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<InformacionComercial> InformacionesComerciales { get; set; }
        public DbSet<UsuarioTecnico> UsuariosTecnicos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}