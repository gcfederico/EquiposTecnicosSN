namespace EquiposTecnicosSN.Web.DataContexts.EquiposMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Prueba2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrdenesDeTrabajo", "MantenimientoDeEquipoId", "dbo.MantenimientosEquipo");
            DropPrimaryKey("dbo.OrdenesDeTrabajo");
            AlterColumn("dbo.OrdenesDeTrabajo", "OrdenDeTrabajoId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.OrdenesDeTrabajo", "OrdenDeTrabajoId");
            AddForeignKey("dbo.OrdenesDeTrabajo", "MantenimientoDeEquipoId", "dbo.MantenimientosEquipo", "MantenimientoEquipoId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrdenesDeTrabajo", "MantenimientoDeEquipoId", "dbo.MantenimientosEquipo");
            DropPrimaryKey("dbo.OrdenesDeTrabajo");
            AlterColumn("dbo.OrdenesDeTrabajo", "OrdenDeTrabajoId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.OrdenesDeTrabajo", "MantenimientoDeEquipoId");
            AddForeignKey("dbo.OrdenesDeTrabajo", "MantenimientoDeEquipoId", "dbo.MantenimientosEquipo", "MantenimientoEquipoId");
        }
    }
}
