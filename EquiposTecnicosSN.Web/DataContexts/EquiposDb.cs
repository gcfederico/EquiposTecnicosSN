using System.Data.Entity;
using System.Diagnostics;
using EquiposTecnicosSN.Entities.Equipos;
using EquiposTecnicosSN.Entities.Mantenimiento;
using EquiposTecnicosSN.Entities.Usuarios;
using EquiposTecnicosSN.Entities.Equipos.Info;
using System;
using System.Threading.Tasks;

namespace EquiposTecnicosSN.Web.DataContexts
{

    public class EquiposDbContext : DbContext
    {
        public EquiposDbContext()
            : base("EquiposTecnicosDbContext")
        {
            Database.Log = log => Debug.Write(log);
        }

        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<EquipoClimatizacion> EquiposDeClimatizacion { get; set; }
        public DbSet<Ubicacion> Ubicaciones { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<InformacionComercial> InformacionesComerciales { get; set; }
        public DbSet<MantenimientoEquipo> MantenimientosEquipo { get; set; }
        public DbSet<SolicitudUsuario> SolicitudesUsuarios { get; set; }
        public DbSet<OrdenDeTrabajo> OrdenesDeTrabajo { get; set; }
        public DbSet<Traslado> Traslados { get; set; }
        public DbSet<Fabricante> Fabricantes { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Modelo> Modelos { get; set; }
        public DbSet<GastoOrdenDeTrabajo> GastosOrdenesDeTrabajo { get; set; }
        public DbSet<InformacionHardware> InformacionesHardware { get; set; }
        public DbSet<Umdns> Umdns { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<InformacionComercial>().HasRequired<Equipo>(ic => ic.Equipo);
        }
    }
}