namespace EquiposTecnicosSN.Web.DataContexts.EquiposMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MantenimientosEquipo", "EquipoId", "dbo.Equipos");
            DropIndex("dbo.MantenimientosEquipo", new[] { "EquipoId" });
            DropTable("dbo.MantenimientosEquipo");
        }
    }
}
