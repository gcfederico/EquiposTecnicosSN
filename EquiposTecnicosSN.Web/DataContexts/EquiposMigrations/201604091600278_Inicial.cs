namespace EquiposTecnicosSN.Web.DataContexts.EquiposMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inicial : DbMigration
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
                        NumeroInventario = c.Int(nullable: false),
                        UbicacionId = c.Int(nullable: false),
                        Estado = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EquipoId)
                .ForeignKey("dbo.Ubicaciones", t => t.UbicacionId, cascadeDelete: true)
                .Index(t => t.UbicacionId);
            
            CreateTable(
                "dbo.MantenimientosEquipo",
                c => new
                    {
                        MantenimientoEquipoId = c.Int(nullable: false, identity: true),
                        EquipoId = c.Int(nullable: false),
                        NumeroReferencia = c.Int(nullable: false),
                        Estado = c.Int(nullable: false),
                        Descripcion = c.String(),
                        FechaDeInicio = c.DateTime(nullable: false),
                        FechaDeFin = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MantenimientoEquipoId)
                .ForeignKey("dbo.Equipos", t => t.EquipoId, cascadeDelete: true)
                .Index(t => t.EquipoId);
            
            CreateTable(
                "dbo.InformacionComercial",
                c => new
                    {
                        EquipoId = c.Int(nullable: false),
                        FechaCompra = c.DateTime(nullable: false),
                        PrecioCompra = c.Int(nullable: false),
                        ValorRestante = c.Int(nullable: false),
                        EsGrantiaContrato = c.Int(nullable: false),
                        FechaFinGarantia = c.DateTime(nullable: false),
                        NotasGarantia = c.String(),
                        ProveedorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EquipoId)
                .ForeignKey("dbo.Equipos", t => t.EquipoId)
                .ForeignKey("dbo.Proveedores", t => t.ProveedorId, cascadeDelete: true)
                .Index(t => t.EquipoId)
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
                "dbo.Ubicaciones",
                c => new
                    {
                        UbicacionId = c.Int(nullable: false, identity: true),
                        NombreCompleto = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.UbicacionId);
            
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
                "dbo.EquiposClimatizacion",
                c => new
                    {
                        EquipoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EquipoId)
                .ForeignKey("dbo.Equipos", t => t.EquipoId)
                .Index(t => t.EquipoId);
            
            CreateTable(
                "dbo.EquiposRespirador",
                c => new
                    {
                        EquipoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EquipoId)
                .ForeignKey("dbo.Equipos", t => t.EquipoId)
                .Index(t => t.EquipoId);
            
            CreateTable(
                "dbo.EquiposCirugia",
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
            DropForeignKey("dbo.EquiposCirugia", "EquipoId", "dbo.Equipos");
            DropForeignKey("dbo.EquiposRespirador", "EquipoId", "dbo.Equipos");
            DropForeignKey("dbo.EquiposClimatizacion", "EquipoId", "dbo.Equipos");
            DropForeignKey("dbo.SolicitudesUsuario", "UbicacionId", "dbo.Ubicaciones");
            DropForeignKey("dbo.Equipos", "UbicacionId", "dbo.Ubicaciones");
            DropForeignKey("dbo.InformacionComercial", "ProveedorId", "dbo.Proveedores");
            DropForeignKey("dbo.InformacionComercial", "EquipoId", "dbo.Equipos");
            DropForeignKey("dbo.MantenimientosEquipo", "EquipoId", "dbo.Equipos");
            DropIndex("dbo.EquiposCirugia", new[] { "EquipoId" });
            DropIndex("dbo.EquiposRespirador", new[] { "EquipoId" });
            DropIndex("dbo.EquiposClimatizacion", new[] { "EquipoId" });
            DropIndex("dbo.SolicitudesUsuario", new[] { "UbicacionId" });
            DropIndex("dbo.InformacionComercial", new[] { "ProveedorId" });
            DropIndex("dbo.InformacionComercial", new[] { "EquipoId" });
            DropIndex("dbo.MantenimientosEquipo", new[] { "EquipoId" });
            DropIndex("dbo.Equipos", new[] { "UbicacionId" });
            DropTable("dbo.EquiposCirugia");
            DropTable("dbo.EquiposRespirador");
            DropTable("dbo.EquiposClimatizacion");
            DropTable("dbo.SolicitudesUsuario");
            DropTable("dbo.Ubicaciones");
            DropTable("dbo.Proveedores");
            DropTable("dbo.InformacionComercial");
            DropTable("dbo.MantenimientosEquipo");
            DropTable("dbo.Equipos");
        }
    }
}
