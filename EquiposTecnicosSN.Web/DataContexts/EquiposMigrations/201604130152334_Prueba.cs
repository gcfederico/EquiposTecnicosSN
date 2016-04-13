namespace EquiposTecnicosSN.Web.DataContexts.EquiposMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Prueba : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrdenesDeTrabajo",
                c => new
                    {
                        MantenimientoDeEquipoId = c.Int(nullable: false),
                        OrdenDeTrabajoId = c.Int(nullable: false),
                        Diagnostico = c.String(),
                        Resolucion = c.String(),
                        ProveedorId = c.Int(),
                    })
                .PrimaryKey(t => t.MantenimientoDeEquipoId)
                .ForeignKey("dbo.MantenimientosEquipo", t => t.MantenimientoDeEquipoId)
                .ForeignKey("dbo.Proveedores", t => t.ProveedorId)
                .Index(t => t.MantenimientoDeEquipoId)
                .Index(t => t.ProveedorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrdenesDeTrabajo", "ProveedorId", "dbo.Proveedores");
            DropForeignKey("dbo.OrdenesDeTrabajo", "MantenimientoDeEquipoId", "dbo.MantenimientosEquipo");
            DropIndex("dbo.OrdenesDeTrabajo", new[] { "ProveedorId" });
            DropIndex("dbo.OrdenesDeTrabajo", new[] { "MantenimientoDeEquipoId" });
            DropTable("dbo.OrdenesDeTrabajo");
        }
    }
}
