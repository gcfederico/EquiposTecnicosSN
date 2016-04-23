namespace EquiposTecnicosSN.Web.DataContexts.EquiposMigrations
{
    using Entities;
    using Entities.Otras;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EquiposTecnicosSN.Web.DataContexts.EquiposDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"DataContexts\EquiposMigrations";
        }

        protected override void Seed(EquiposTecnicosSN.Web.DataContexts.EquiposDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Ubicaciones.AddOrUpdate(
                u => u.NombreCompleto,
                new Ubicacion { NombreCompleto = "Hospital Zapala" },
                new Ubicacion { NombreCompleto = "Hospital Neuquén" }
                );


            context.Proveedores.AddOrUpdate(
                u => u.Nombre,
                new Proveedor { Nombre = "3M" },
                new Proveedor { Nombre = "Obras Públicas" }
                );
        }
    }
}
