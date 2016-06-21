namespace EquiposTecnicosSN.Web.DataContexts.EquiposMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateInicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChecklistsMantenimientoPreventivo",
                c => new
                    {
                        ChecklistMantenimientoPreventivoId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 255),
                        ContentType = c.String(nullable: false, maxLength: 100),
                        Content = c.Binary(nullable: false),
                        FileExtension = c.String(nullable: false, maxLength: 5),
                    })
                .PrimaryKey(t => t.ChecklistMantenimientoPreventivoId);
            
            CreateTable(
                "dbo.Equipos",
                c => new
                    {
                        EquipoId = c.Int(nullable: false, identity: true),
                        NombreCompleto = c.String(nullable: false, maxLength: 255),
                        UMDNS = c.String(nullable: false, maxLength: 50),
                        NumeroMatricula = c.Int(),
                        UbicacionId = c.Int(nullable: false),
                        Estado = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EquipoId)
                .ForeignKey("dbo.Ubicaciones", t => t.UbicacionId, cascadeDelete: false)
                .Index(t => t.UbicacionId);
            
            CreateTable(
                "dbo.InformacionComercial",
                c => new
                    {
                        EquipoId = c.Int(nullable: false),
                        FechaCompra = c.DateTime(),
                        PrecioCompra = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorRestante = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EsGrantiaContrato = c.Int(),
                        FechaFinGarantia = c.DateTime(),
                        NotasGarantia = c.String(maxLength: 150),
                        ProveedorId = c.Int(),
                        Financiamiento = c.Int(),
                    })
                .PrimaryKey(t => t.EquipoId)
                .ForeignKey("dbo.Equipos", t => t.EquipoId)
                .ForeignKey("dbo.Proveedores", t => t.ProveedorId)
                .Index(t => t.EquipoId)
                .Index(t => t.ProveedorId);
            
            CreateTable(
                "dbo.Proveedores",
                c => new
                    {
                        ProveedorId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        Tipo = c.Int(nullable: false),
                        Direccion = c.String(maxLength: 255),
                        Telefono = c.String(maxLength: 50),
                        Website = c.String(maxLength: 255),
                        Email = c.String(maxLength: 50),
                        Servicios = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.ProveedorId);
            
            CreateTable(
                "dbo.InformacionHardware",
                c => new
                    {
                        EquipoId = c.Int(nullable: false),
                        NumeroSerie = c.String(maxLength: 255),
                        FabricanteId = c.Int(nullable: false),
                        MarcaId = c.Int(nullable: false),
                        ModeloId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EquipoId)
                .ForeignKey("dbo.Equipos", t => t.EquipoId)
                .ForeignKey("dbo.Fabricantes", t => t.FabricanteId, cascadeDelete: false)
                .ForeignKey("dbo.Marcas", t => t.MarcaId, cascadeDelete: false)
                .ForeignKey("dbo.Modelos", t => t.ModeloId, cascadeDelete: false)
                .Index(t => t.EquipoId)
                .Index(t => t.FabricanteId)
                .Index(t => t.MarcaId)
                .Index(t => t.ModeloId);
            
            CreateTable(
                "dbo.Fabricantes",
                c => new
                    {
                        FabricanteId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.FabricanteId);
            
            CreateTable(
                "dbo.Marcas",
                c => new
                    {
                        MarcaId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 150),
                        FabricanteId = c.Int(),
                    })
                .PrimaryKey(t => t.MarcaId)
                .ForeignKey("dbo.Fabricantes", t => t.FabricanteId)
                .Index(t => t.FabricanteId);
            
            CreateTable(
                "dbo.Modelos",
                c => new
                    {
                        ModeloId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 150),
                        MarcaId = c.Int(),
                    })
                .PrimaryKey(t => t.ModeloId)
                .ForeignKey("dbo.Marcas", t => t.MarcaId)
                .Index(t => t.MarcaId);
            
            CreateTable(
                "dbo.OrdenesDeTrabajo",
                c => new
                    {
                        OrdenDeTrabajoId = c.Int(nullable: false, identity: true),
                        NumeroReferencia = c.String(nullable: false, maxLength: 20),
                        EquipoId = c.Int(nullable: false),
                        FechaInicio = c.DateTime(nullable: false),
                        UsuarioInicioId = c.Int(nullable: false),
                        Observaciones = c.String(maxLength: 500),
                        FechaCierre = c.DateTime(),
                        UsuarioCierreId = c.Int(),
                        Prioridad = c.Int(nullable: false),
                        Estado = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrdenDeTrabajoId)
                .ForeignKey("dbo.Equipos", t => t.EquipoId, cascadeDelete: true)
                .Index(t => t.EquipoId);
            
            CreateTable(
                "dbo.GastosOrdenesDeTrabajo",
                c => new
                    {
                        GastoOrdenDeTrabajoId = c.Int(nullable: false, identity: true),
                        OrdenDeTrabajoId = c.Int(nullable: false),
                        Concepto = c.String(nullable: false, maxLength: 100),
                        Monto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SolicitudRepuestoServicioId = c.Int(),
                    })
                .PrimaryKey(t => t.GastoOrdenDeTrabajoId)
                .ForeignKey("dbo.OrdenesDeTrabajo", t => t.OrdenDeTrabajoId, cascadeDelete: true)
                .ForeignKey("dbo.SolicitudesRepuestosServicios", t => t.SolicitudRepuestoServicioId)
                .Index(t => t.OrdenDeTrabajoId)
                .Index(t => t.SolicitudRepuestoServicioId);
            
            CreateTable(
                "dbo.SolicitudesRepuestosServicios",
                c => new
                    {
                        SolicitudRepuestoServicioId = c.Int(nullable: false, identity: true),
                        OrdenDeTrabajoId = c.Int(nullable: false),
                        FechaInicio = c.DateTime(nullable: false),
                        Comentarios = c.String(maxLength: 500),
                        FechaCierre = c.DateTime(),
                        ProveedorId = c.Int(),
                        CantidadRepuesto = c.Int(nullable: false),
                        RepuestoId = c.Int(nullable: false),
                        UsuarioSolicitudId = c.Int(),
                    })
                .PrimaryKey(t => t.SolicitudRepuestoServicioId)
                .ForeignKey("dbo.OrdenesDeTrabajo", t => t.OrdenDeTrabajoId, cascadeDelete: true)
                .ForeignKey("dbo.Proveedores", t => t.ProveedorId)
                .ForeignKey("dbo.Repuestos", t => t.RepuestoId, cascadeDelete: true)
                .Index(t => t.OrdenDeTrabajoId)
                .Index(t => t.ProveedorId)
                .Index(t => t.RepuestoId);
            
            CreateTable(
                "dbo.Repuestos",
                c => new
                    {
                        RepuestoId = c.Int(nullable: false, identity: true),
                        Codigo = c.String(),
                        Nombre = c.String(maxLength: 255),
                        ProveedorId = c.Int(nullable: false),
                        Costo = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.RepuestoId)
                .ForeignKey("dbo.Proveedores", t => t.ProveedorId, cascadeDelete: true)
                .Index(t => t.ProveedorId);
            
            CreateTable(
                "dbo.Traslados",
                c => new
                    {
                        TrasladoId = c.Int(nullable: false, identity: true),
                        EquipoId = c.Int(nullable: false),
                        FechaTraslado = c.DateTime(nullable: false),
                        UbicacionOrigenId = c.Int(nullable: false),
                        UbicacionDestinoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TrasladoId)
                .ForeignKey("dbo.Equipos", t => t.EquipoId, cascadeDelete: true)
                .ForeignKey("dbo.Ubicaciones", t => t.UbicacionDestinoId, cascadeDelete: false)
                .ForeignKey("dbo.Ubicaciones", t => t.UbicacionOrigenId, cascadeDelete: false)
                .Index(t => t.EquipoId)
                .Index(t => t.UbicacionOrigenId)
                .Index(t => t.UbicacionDestinoId);
            
            CreateTable(
                "dbo.Ubicaciones",
                c => new
                    {
                        UbicacionId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 150),
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
                "dbo.StockRepuestos",
                c => new
                    {
                        StockRepuestoId = c.Int(nullable: false, identity: true),
                        CantidadDisponible = c.Int(nullable: false),
                        RepuestoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StockRepuestoId)
                .ForeignKey("dbo.Repuestos", t => t.RepuestoId, cascadeDelete: true)
                .Index(t => t.RepuestoId);
            
            CreateTable(
                "dbo.UMDNS",
                c => new
                    {
                        UmdnsId = c.Int(nullable: false, identity: true),
                        Codigo = c.String(nullable: false),
                        NombreCompleto = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UmdnsId);
            
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
                "dbo.OrdenesDeTrabajoMantenimientoCorrectivo",
                c => new
                    {
                        OrdenDeTrabajoId = c.Int(nullable: false),
                        EquipoParado = c.Boolean(nullable: false),
                        Descripcion = c.String(nullable: false, maxLength: 500),
                        Diagnostico = c.String(maxLength: 500),
                        DetalleReparacion = c.String(maxLength: 500),
                        CausaRaiz = c.String(maxLength: 500),
                        FechaDiagnostico = c.DateTime(),
                        UsuarioDiagnosticoId = c.Int(),
                        FechaReparacion = c.DateTime(),
                        UsuarioReparacionId = c.Int(),
                        Limpieza = c.Boolean(nullable: false),
                        VerificacionFuncionamiento = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.OrdenDeTrabajoId)
                .ForeignKey("dbo.OrdenesDeTrabajo", t => t.OrdenDeTrabajoId)
                .Index(t => t.OrdenDeTrabajoId);
            
            CreateTable(
                "dbo.OrdenesDeTrabajoMantenimientoPreventivo",
                c => new
                    {
                        OrdenDeTrabajoId = c.Int(nullable: false),
                        ChecklistId = c.Int(nullable: false),
                        fechaCreacion = c.DateTime(nullable: false),
                        UsuarioCreacionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrdenDeTrabajoId)
                .ForeignKey("dbo.OrdenesDeTrabajo", t => t.OrdenDeTrabajoId)
                .ForeignKey("dbo.ChecklistsMantenimientoPreventivo", t => t.ChecklistId, cascadeDelete: true)
                .Index(t => t.OrdenDeTrabajoId)
                .Index(t => t.ChecklistId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrdenesDeTrabajoMantenimientoPreventivo", "ChecklistId", "dbo.ChecklistsMantenimientoPreventivo");
            DropForeignKey("dbo.OrdenesDeTrabajoMantenimientoPreventivo", "OrdenDeTrabajoId", "dbo.OrdenesDeTrabajo");
            DropForeignKey("dbo.OrdenesDeTrabajoMantenimientoCorrectivo", "OrdenDeTrabajoId", "dbo.OrdenesDeTrabajo");
            DropForeignKey("dbo.EquiposClimatizacion", "EquipoId", "dbo.Equipos");
            DropForeignKey("dbo.StockRepuestos", "RepuestoId", "dbo.Repuestos");
            DropForeignKey("dbo.SolicitudesUsuario", "UbicacionId", "dbo.Ubicaciones");
            DropForeignKey("dbo.Traslados", "UbicacionOrigenId", "dbo.Ubicaciones");
            DropForeignKey("dbo.Traslados", "UbicacionDestinoId", "dbo.Ubicaciones");
            DropForeignKey("dbo.Equipos", "UbicacionId", "dbo.Ubicaciones");
            DropForeignKey("dbo.Traslados", "EquipoId", "dbo.Equipos");
            DropForeignKey("dbo.GastosOrdenesDeTrabajo", "SolicitudRepuestoServicioId", "dbo.SolicitudesRepuestosServicios");
            DropForeignKey("dbo.SolicitudesRepuestosServicios", "RepuestoId", "dbo.Repuestos");
            DropForeignKey("dbo.Repuestos", "ProveedorId", "dbo.Proveedores");
            DropForeignKey("dbo.SolicitudesRepuestosServicios", "ProveedorId", "dbo.Proveedores");
            DropForeignKey("dbo.SolicitudesRepuestosServicios", "OrdenDeTrabajoId", "dbo.OrdenesDeTrabajo");
            DropForeignKey("dbo.GastosOrdenesDeTrabajo", "OrdenDeTrabajoId", "dbo.OrdenesDeTrabajo");
            DropForeignKey("dbo.OrdenesDeTrabajo", "EquipoId", "dbo.Equipos");
            DropForeignKey("dbo.InformacionHardware", "ModeloId", "dbo.Modelos");
            DropForeignKey("dbo.InformacionHardware", "MarcaId", "dbo.Marcas");
            DropForeignKey("dbo.InformacionHardware", "FabricanteId", "dbo.Fabricantes");
            DropForeignKey("dbo.Modelos", "MarcaId", "dbo.Marcas");
            DropForeignKey("dbo.Marcas", "FabricanteId", "dbo.Fabricantes");
            DropForeignKey("dbo.InformacionHardware", "EquipoId", "dbo.Equipos");
            DropForeignKey("dbo.InformacionComercial", "ProveedorId", "dbo.Proveedores");
            DropForeignKey("dbo.InformacionComercial", "EquipoId", "dbo.Equipos");
            DropIndex("dbo.OrdenesDeTrabajoMantenimientoPreventivo", new[] { "ChecklistId" });
            DropIndex("dbo.OrdenesDeTrabajoMantenimientoPreventivo", new[] { "OrdenDeTrabajoId" });
            DropIndex("dbo.OrdenesDeTrabajoMantenimientoCorrectivo", new[] { "OrdenDeTrabajoId" });
            DropIndex("dbo.EquiposClimatizacion", new[] { "EquipoId" });
            DropIndex("dbo.StockRepuestos", new[] { "RepuestoId" });
            DropIndex("dbo.SolicitudesUsuario", new[] { "UbicacionId" });
            DropIndex("dbo.Traslados", new[] { "UbicacionDestinoId" });
            DropIndex("dbo.Traslados", new[] { "UbicacionOrigenId" });
            DropIndex("dbo.Traslados", new[] { "EquipoId" });
            DropIndex("dbo.Repuestos", new[] { "ProveedorId" });
            DropIndex("dbo.SolicitudesRepuestosServicios", new[] { "RepuestoId" });
            DropIndex("dbo.SolicitudesRepuestosServicios", new[] { "ProveedorId" });
            DropIndex("dbo.SolicitudesRepuestosServicios", new[] { "OrdenDeTrabajoId" });
            DropIndex("dbo.GastosOrdenesDeTrabajo", new[] { "SolicitudRepuestoServicioId" });
            DropIndex("dbo.GastosOrdenesDeTrabajo", new[] { "OrdenDeTrabajoId" });
            DropIndex("dbo.OrdenesDeTrabajo", new[] { "EquipoId" });
            DropIndex("dbo.Modelos", new[] { "MarcaId" });
            DropIndex("dbo.Marcas", new[] { "FabricanteId" });
            DropIndex("dbo.InformacionHardware", new[] { "ModeloId" });
            DropIndex("dbo.InformacionHardware", new[] { "MarcaId" });
            DropIndex("dbo.InformacionHardware", new[] { "FabricanteId" });
            DropIndex("dbo.InformacionHardware", new[] { "EquipoId" });
            DropIndex("dbo.InformacionComercial", new[] { "ProveedorId" });
            DropIndex("dbo.InformacionComercial", new[] { "EquipoId" });
            DropIndex("dbo.Equipos", new[] { "UbicacionId" });
            DropTable("dbo.OrdenesDeTrabajoMantenimientoPreventivo");
            DropTable("dbo.OrdenesDeTrabajoMantenimientoCorrectivo");
            DropTable("dbo.EquiposClimatizacion");
            DropTable("dbo.UMDNS");
            DropTable("dbo.StockRepuestos");
            DropTable("dbo.SolicitudesUsuario");
            DropTable("dbo.Ubicaciones");
            DropTable("dbo.Traslados");
            DropTable("dbo.Repuestos");
            DropTable("dbo.SolicitudesRepuestosServicios");
            DropTable("dbo.GastosOrdenesDeTrabajo");
            DropTable("dbo.OrdenesDeTrabajo");
            DropTable("dbo.Modelos");
            DropTable("dbo.Marcas");
            DropTable("dbo.Fabricantes");
            DropTable("dbo.InformacionHardware");
            DropTable("dbo.Proveedores");
            DropTable("dbo.InformacionComercial");
            DropTable("dbo.Equipos");
            DropTable("dbo.ChecklistsMantenimientoPreventivo");
        }
    }
}
