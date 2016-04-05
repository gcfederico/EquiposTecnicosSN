namespace EquiposTecnicosSN.Web.DataContexts.EquiposMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EquiposCirugia",
                c => new
                    {
                        EquipoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EquipoId)
                .ForeignKey("dbo.Equipos", t => t.EquipoId)
                .Index(t => t.EquipoId);
            
            DropColumn("dbo.Equipos", "Tipo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Equipos", "Tipo", c => c.Int(nullable: false));
            DropForeignKey("dbo.EquiposCirugia", "EquipoId", "dbo.Equipos");
            DropIndex("dbo.EquiposCirugia", new[] { "EquipoId" });
            DropTable("dbo.EquiposCirugia");
        }
    }
}
