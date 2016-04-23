namespace EquiposTecnicosSN.Web.DataContexts.EquiposMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateInicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Equipos",
                c => new
                    {
                        EquipoId = c.Int(nullable: false, identity: true),
                        NombreCompleto = c.String(nullable: false, maxLength: 255),
                        UMDNS = c.String(nullable: false, maxLength: 50),
                        NumeroSerie = c.String(maxLength: 255),
                        Modelo = c.String(maxLength: 255),
                        NumeroInventario = c.Int(),
                        UbicacionId = c.Int(nullable: false),
                        Estado = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EquipoId)
                .ForeignKey("dbo.Ubicaciones", t => t.UbicacionId, cascadeDelete: true)
                .Index(t => t.UbicacionId);
            
            CreateTable(
                "dbo.Mantenimientos",
                c => new
                    {
                        MantenimientoId = c.Int(nullable: false, identity: true),
                        EquipoId = c.Int(nullable: false),
                        NumeroReferencia = c.Int(nullable: false),
                        Estado = c.Int(nullable: false),
                        Descripcion = c.String(),
                        FechaDeInicio = c.DateTime(nullable: false),
                        FechaDeFin = c.DateTime(),
                    })
                .PrimaryKey(t => t.MantenimientoId)
                .ForeignKey("dbo.Equipos", t => t.EquipoId, cascadeDelete: true)
                .Index(t => t.EquipoId);
            
            CreateTable(
                "dbo.OrdenesDeTrabajo",
                c => new
                    {
                        OrdenDeTrabajoId = c.Int(nullable: false, identity: true),
                        MantenimientoEquipoId = c.Int(nullable: false),
                        Diagnostico = c.String(),
                        Resolucion = c.String(),
                        ProveedorId = c.Int(),
                    })
                .PrimaryKey(t => t.OrdenDeTrabajoId)
                .ForeignKey("dbo.Mantenimientos", t => t.MantenimientoEquipoId, cascadeDelete: true)
                .ForeignKey("dbo.Proveedores", t => t.ProveedorId)
                .Index(t => t.MantenimientoEquipoId)
                .Index(t => t.ProveedorId);
            
            CreateTable(
                "dbo.Proveedores",
                c => new
                    {
                        ProveedorId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ProveedorId);
            
            CreateTable(
                "dbo.InformacionComercial",
                c => new
                    {
                        EquipoId = c.Int(nullable: false),
                        FechaCompra = c.DateTime(),
                        PrecioCompra = c.Int(),
                        ValorRestante = c.Int(),
                        EsGrantiaContrato = c.Int(nullable: false),
                        FechaFinGarantia = c.DateTime(),
                        NotasGarantia = c.String(),
                        ProveedorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EquipoId)
                .ForeignKey("dbo.Equipos", t => t.EquipoId)
                .ForeignKey("dbo.Proveedores", t => t.ProveedorId, cascadeDelete: true)
                .Index(t => t.EquipoId)
                .Index(t => t.ProveedorId);
            
            CreateTable(
                "dbo.Ubicaciones",
                c => new
                    {
                        UbicacionId = c.Int(nullable: false, identity: true),
                        NombreCompleto = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.UbicacionId);
            
            CreateTable(
                "dbo.Fabricantes",
                c => new
                    {
                        FabricanteId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.FabricanteId);
            
            CreateTable(
                "dbo.Marcas",
                c => new
                    {
                        MarcaId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        FabricanteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MarcaId)
                .ForeignKey("dbo.Fabricantes", t => t.FabricanteId, cascadeDelete: true)
                .Index(t => t.FabricanteId);
            
            CreateTable(
                "dbo.Modelos",
                c => new
                    {
                        ModeloId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        MarcaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ModeloId)
                .ForeignKey("dbo.Marcas", t => t.MarcaId, cascadeDelete: true)
                .Index(t => t.MarcaId);
            
            CreateTable(
                "dbo.SolicitudesUsuario",
                c => new
                    {
                        SolicitudUsuarioId = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        UbicacionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SolicitudUsuarioId)
                .ForeignKey("dbo.Ubicaciones", t => t.UbicacionId, cascadeDelete: true)
                .Index(t => t.UbicacionId);
            
            CreateTable(
                "dbo.Traslados",
                c => new
                    {
                        EquipoId = c.Int(nullable: false),
                        FechaTraslado = c.DateTime(nullable: false),
                        UbicacionOrigenId = c.Int(nullable: false),
                        UbicacionDestinoId = c.Int(nullable: false),
                        UbicacionDestino_UbicacionId = c.Int(),
                        UbicacionOrigen_UbicacionId = c.Int(),
                    })
                .PrimaryKey(t => t.EquipoId)
                .ForeignKey("dbo.Equipos", t => t.EquipoId)
                .ForeignKey("dbo.Ubicaciones", t => t.UbicacionDestino_UbicacionId)
                .ForeignKey("dbo.Ubicaciones", t => t.UbicacionOrigen_UbicacionId)
                .Index(t => t.EquipoId)
                .Index(t => t.UbicacionDestino_UbicacionId)
                .Index(t => t.UbicacionOrigen_UbicacionId);
            
            CreateTable(
                "dbo.EquiposClimatizacion",
                c => new
                    {
                        EquipoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EquipoId)
                .ForeignKey("dbo.Equipos", t => t.EquipoId)
                .Index(t => t.EquipoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EquiposClimatizacion", "EquipoId", "dbo.Equipos");
            DropForeignKey("dbo.Traslados", "UbicacionOrigen_UbicacionId", "dbo.Ubicaciones");
            DropForeignKey("dbo.Traslados", "UbicacionDestino_UbicacionId", "dbo.Ubicaciones");
            DropForeignKey("dbo.Traslados", "EquipoId", "dbo.Equipos");
            DropForeignKey("dbo.SolicitudesUsuario", "UbicacionId", "dbo.Ubicaciones");
            DropForeignKey("dbo.Modelos", "MarcaId", "dbo.Marcas");
            DropForeignKey("dbo.Marcas", "FabricanteId", "dbo.Fabricantes");
            DropForeignKey("dbo.Equipos", "UbicacionId", "dbo.Ubicaciones");
            DropForeignKey("dbo.OrdenesDeTrabajo", "ProveedorId", "dbo.Proveedores");
            DropForeignKey("dbo.InformacionComercial", "ProveedorId", "dbo.Proveedores");
            DropForeignKey("dbo.InformacionComercial", "EquipoId", "dbo.Equipos");
            DropForeignKey("dbo.OrdenesDeTrabajo", "MantenimientoEquipoId", "dbo.Mantenimientos");
            DropForeignKey("dbo.Mantenimientos", "EquipoId", "dbo.Equipos");
            DropIndex("dbo.EquiposClimatizacion", new[] { "EquipoId" });
            DropIndex("dbo.Traslados", new[] { "UbicacionOrigen_UbicacionId" });
            DropIndex("dbo.Traslados", new[] { "UbicacionDestino_UbicacionId" });
            DropIndex("dbo.Traslados", new[] { "EquipoId" });
            DropIndex("dbo.SolicitudesUsuario", new[] { "UbicacionId" });
            DropIndex("dbo.Modelos", new[] { "MarcaId" });
            DropIndex("dbo.Marcas", new[] { "FabricanteId" });
            DropIndex("dbo.InformacionComercial", new[] { "ProveedorId" });
            DropIndex("dbo.InformacionComercial", new[] { "EquipoId" });
            DropIndex("dbo.OrdenesDeTrabajo", new[] { "ProveedorId" });
            DropIndex("dbo.OrdenesDeTrabajo", new[] { "MantenimientoEquipoId" });
            DropIndex("dbo.Mantenimientos", new[] { "EquipoId" });
            DropIndex("dbo.Equipos", new[] { "UbicacionId" });
            DropTable("dbo.EquiposClimatizacion");
            DropTable("dbo.Traslados");
            DropTable("dbo.SolicitudesUsuario");
            DropTable("dbo.Modelos");
            DropTable("dbo.Marcas");
            DropTable("dbo.Fabricantes");
            DropTable("dbo.Ubicaciones");
            DropTable("dbo.InformacionComercial");
            DropTable("dbo.Proveedores");
            DropTable("dbo.OrdenesDeTrabajo");
            DropTable("dbo.Mantenimientos");
            DropTable("dbo.Equipos");
        }
    }
}
