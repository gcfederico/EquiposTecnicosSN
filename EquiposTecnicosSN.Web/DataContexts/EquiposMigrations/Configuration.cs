namespace EquiposTecnicosSN.Web.DataContexts.EquiposMigrations
{
    using Entities.Equipos.Info;
    using Entities.Mantenimiento;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<EquiposTecnicosSN.Web.DataContexts.EquiposDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
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

            var fabricanteDemo = new Fabricante { Nombre = "Fabricante Demo" };
            var marcaDemo = new Marca { Nombre = "Marca Demo", Fabricante = fabricanteDemo };
            var modeloDemo = new Modelo { Nombre = "Modelo Demo", Marca = marcaDemo };

            context.Fabricantes.AddOrUpdate(f => f.Nombre, fabricanteDemo);
            context.Marcas.AddOrUpdate(f => f.Nombre, marcaDemo);
            context.Modelos.AddOrUpdate(f => f.Nombre, modeloDemo);

            context.Ubicaciones.AddOrUpdate(
                u => u.Nombre,
                new Ubicacion { Nombre = "Hospital Zapala" },
                new Ubicacion { Nombre = "Hospital Neuquén" }
                );


            context.Proveedores.AddOrUpdate(
                u => u.Nombre,
                new Proveedor { Nombre = "3M" },
                new Proveedor { Nombre = "Obras Públicas" }
                );

            var ejemplosUMDNS = new[]
            {
                new Umdns { Codigo = "10-003", NombreCompleto ="Fajas, Abdominales"},
                new Umdns { Codigo = "10-014", NombreCompleto ="Kits para Acupuntura"},
                new Umdns { Codigo = "10-024", NombreCompleto ="Adaptómetros"},
                new Umdns { Codigo = "10-025", NombreCompleto ="Adenótomos"},
                new Umdns { Codigo = "10-026", NombreCompleto ="Tiras Adhesivas"},
                new Umdns { Codigo = "10-028", NombreCompleto ="Tiras Adhesivas, Multipropósito, Hipoalergénicas"},
                new Umdns { Codigo = "10-029", NombreCompleto ="Tiras Adhesivas, Multipropósito, Impermeables"},
                new Umdns { Codigo = "10-030", NombreCompleto ="Cintas, Adhesivas"},
                new Umdns { Codigo = "10-033", NombreCompleto ="Removedores de Adhesivos"},
                new Umdns { Codigo = "10-034", NombreCompleto ="Adhesivos"},
                new Umdns { Codigo = "10-035", NombreCompleto ="Adhesivos, en Aerosol"},
                new Umdns { Codigo = "10-036", NombreCompleto ="Adhesivos, Líquidos"},
                new Umdns { Codigo = "10-037", NombreCompleto ="Taburetes, Ajustables"},
                new Umdns { Codigo = "10-045", NombreCompleto ="Aireadores, de Oxido de Etileno"},
                new Umdns { Codigo = "10-046", NombreCompleto ="Generadores de Aerosol"},
                new Umdns { Codigo = "10-051", NombreCompleto ="Muestreadores, para Ambiente, del Aire"},
                new Umdns { Codigo = "10-053", NombreCompleto ="Vías Aéreas Artificiales"},
                new Umdns { Codigo = "10-056", NombreCompleto ="Vías Aéreas Artificiales, con Obturación Esofágica"},
                new Umdns { Codigo = "10-057", NombreCompleto ="Vías Aéreas Artificiales, Nasofaríngeas"},
                new Umdns { Codigo = "10-058", NombreCompleto ="Desobstructores de Vías Aéreas, para Emergencias"},
                new Umdns { Codigo = "10-059", NombreCompleto ="Vías Aéreas Artificiales, Orofaríngeas"},
                new Umdns { Codigo = "10-077", NombreCompleto ="Aleaciones, Dentales"},
                new Umdns { Codigo = "10-082", NombreCompleto ="Amalgamadores"},
                new Umdns { Codigo = "10-085", NombreCompleto ="Ambulancias"},
                new Umdns { Codigo = "10-088", NombreCompleto ="Amnioscopios"},
                new Umdns { Codigo = "10-089", NombreCompleto ="Amniótomos"},
                new Umdns { Codigo = "10-098", NombreCompleto ="Abridores de Ampollas"},
                new Umdns { Codigo = "10-123", NombreCompleto ="Adaptadores para Circuitos Respiratorios Externos"},
                new Umdns { Codigo = "10-124", NombreCompleto ="Kits para Anestesia"},
                new Umdns { Codigo = "10-125", NombreCompleto ="Kits para Anestesia, Plexo Braquial"},
                new Umdns { Codigo = "10-126", NombreCompleto ="Kits para Anestesia, Caudal"},
                new Umdns { Codigo = "10-127", NombreCompleto ="Kits para Anestesia, Epidural"},
                new Umdns { Codigo = "10-128", NombreCompleto ="Kits para Anestesia, Glosofaríngea"},
                new Umdns { Codigo = "10-129", NombreCompleto ="Kits para Anestesia, Paracervical"},
                new Umdns { Codigo = "10-130", NombreCompleto ="Kits para Anestesia, Pudendal"},
                new Umdns { Codigo = "10-131", NombreCompleto ="Kits para Anestesia, Espinal"},
                new Umdns { Codigo = "10-134", NombreCompleto ="Unidades de Anestesia"},
                new Umdns { Codigo = "10-139", NombreCompleto ="Circuitos Respiratorios Externos, para Anestesia"},
                new Umdns { Codigo = "10-140", NombreCompleto ="Absorbedores para Unidades de Anestesia, de Dióxido de Carbono"},
                new Umdns { Codigo = "10-142", NombreCompleto ="Expulsadores de Gas para las Unidades de Anestesia"},
                new Umdns { Codigo = "10-144", NombreCompleto ="Vaporizadores para las Unidades de Anestesia"},
                new Umdns { Codigo = "10-145", NombreCompleto ="Ventiladores, para Unidades de Anestesia"},
                new Umdns { Codigo = "10-148", NombreCompleto ="Anestesiómetros"},
                new Umdns { Codigo = "10-149", NombreCompleto ="Taburetes, Ajustables, para Anestesiología"},
                new Umdns { Codigo = "10-152", NombreCompleto ="Angiótomos"},
                new Umdns { Codigo = "10-153", NombreCompleto ="Bandas Elásticas, para Tobillos"},
                new Umdns { Codigo = "10-155", NombreCompleto ="Tobilleras"},
                new Umdns { Codigo = "10-156", NombreCompleto ="Anoscopios"},
                new Umdns { Codigo = "10-164", NombreCompleto ="Apexcardiógrafos"},
                new Umdns { Codigo = "10-168", NombreCompleto ="Tonómetros Oftálmicos, Aplanación"},
                new Umdns { Codigo = "10-171", NombreCompleto ="Jarras, para Aplicadores"},
                new Umdns { Codigo = "10-172", NombreCompleto ="Aplicadores"},
                new Umdns { Codigo = "10-174", NombreCompleto ="Aplicadores, de Presillas"},
                new Umdns { Codigo = "10-175", NombreCompleto ="Aplicadores, para los Ojos"},
                new Umdns { Codigo = "10-176", NombreCompleto ="Aplicadores, Proctoscópicos"},
                new Umdns { Codigo = "10-177", NombreCompleto ="Aplicadores, Traqueales"},
                new Umdns { Codigo = "10-182", NombreCompleto ="Baños, para Brazos"},
                new Umdns { Codigo = "10-183", NombreCompleto ="Cubiertas, para Tablillas de Brazos"},
                new Umdns { Codigo = "10-184", NombreCompleto ="Tableros, para Brazos"},
                new Umdns { Codigo = "10-185", NombreCompleto ="Reposabrazos"},
                new Umdns { Codigo = "10-190", NombreCompleto ="Simuladores, Cardiacos, para Electrocardiografía, de Arritmia"},
                new Umdns { Codigo = "10-198", NombreCompleto ="Artroscopios"},
                new Umdns { Codigo = "10-199", NombreCompleto ="Articuladores"},
                new Umdns { Codigo = "10-200", NombreCompleto ="Articuladores, para Yeso"},
                new Umdns { Codigo = "10-201", NombreCompleto ="Articuladores, Dentales"},
                new Umdns { Codigo = "10-204", NombreCompleto ="Laringes, Artificiales"},
                new Umdns { Codigo = "10-208", NombreCompleto ="Aspiradores"},
                new Umdns { Codigo = "10-211", NombreCompleto ="Recipientes, para Colección de Aspirador"},
                new Umdns { Codigo = "10-212", NombreCompleto ="Aspiradores, Dentales"},
                new Umdns { Codigo = "10-214", NombreCompleto ="Aspiradores, Pediátricos"},
                new Umdns { Codigo = "10-215", NombreCompleto ="Aspiradores, de Bajo Volumen"},
                new Umdns { Codigo = "10-216", NombreCompleto ="Aspiradores, Nasales"},
                new Umdns { Codigo = "10-217", NombreCompleto ="Aspiradores, Quirúrgicos"},
                new Umdns { Codigo = "10-218", NombreCompleto ="Aspiradores, Torácicos"},
                new Umdns { Codigo = "10-219", NombreCompleto ="Aspiradores, Traqueales"},
                new Umdns { Codigo = "10-222", NombreCompleto ="Aspiradores, Uterinos"},
                new Umdns { Codigo = "10-223", NombreCompleto ="Aspiradores, para Heridas"},
                new Umdns { Codigo = "10-225", NombreCompleto ="Atmocauterios"},
                new Umdns { Codigo = "10-226", NombreCompleto ="Atomizadores"},
                new Umdns { Codigo = "10-228", NombreCompleto ="Audiómetros"}
            };


            context.Umdns.AddOrUpdate(
            u => u.Codigo,
                ejemplosUMDNS
            );
        }
    }
}
