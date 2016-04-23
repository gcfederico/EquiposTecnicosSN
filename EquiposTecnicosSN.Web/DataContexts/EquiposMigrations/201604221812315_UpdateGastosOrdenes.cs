namespace EquiposTecnicosSN.Web.DataContexts.EquiposMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateGastosOrdenes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GastosOrdenesDeTrabajo",
                c => new
                    {
                        OrdenDeTrabajoId = c.Int(nullable: false),
                        Concepto = c.String(nullable: false, maxLength: 100),
                        Monto = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrdenDeTrabajoId)
                .ForeignKey("dbo.OrdenesDeTrabajo", t => t.OrdenDeTrabajoId)
                .Index(t => t.OrdenDeTrabajoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GastosOrdenesDeTrabajo", "OrdenDeTrabajoId", "dbo.OrdenesDeTrabajo");
            DropIndex("dbo.GastosOrdenesDeTrabajo", new[] { "OrdenDeTrabajoId" });
            DropTable("dbo.GastosOrdenesDeTrabajo");
        }
    }
}
