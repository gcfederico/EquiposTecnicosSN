namespace EquiposTecnicosSN.Web.DataContexts.EquiposMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update21 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UMDNS",
                c => new
                    {
                        UmdnsId = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        NombreCompleto = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UmdnsId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UMDNS");
        }
    }
}
