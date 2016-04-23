namespace EquiposTecnicosSN.Web.DataContexts.EquiposMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateGastosOrdenes2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GastosOrdenesDeTrabajo", "OrdenDeTrabajoId", "dbo.OrdenesDeTrabajo");
            DropPrimaryKey("dbo.GastosOrdenesDeTrabajo");
            AddColumn("dbo.GastosOrdenesDeTrabajo", "GastoOrdenDeTrabajoId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.GastosOrdenesDeTrabajo", "GastoOrdenDeTrabajoId");
            AddForeignKey("dbo.GastosOrdenesDeTrabajo", "OrdenDeTrabajoId", "dbo.OrdenesDeTrabajo", "OrdenDeTrabajoId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GastosOrdenesDeTrabajo", "OrdenDeTrabajoId", "dbo.OrdenesDeTrabajo");
            DropPrimaryKey("dbo.GastosOrdenesDeTrabajo");
            DropColumn("dbo.GastosOrdenesDeTrabajo", "GastoOrdenDeTrabajoId");
            AddPrimaryKey("dbo.GastosOrdenesDeTrabajo", "OrdenDeTrabajoId");
            AddForeignKey("dbo.GastosOrdenesDeTrabajo", "OrdenDeTrabajoId", "dbo.OrdenesDeTrabajo", "OrdenDeTrabajoId");
        }
    }
}
