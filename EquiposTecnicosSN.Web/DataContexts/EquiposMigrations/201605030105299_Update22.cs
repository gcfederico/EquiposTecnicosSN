namespace EquiposTecnicosSN.Web.DataContexts.EquiposMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update22 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UMDNS", "Codigo", c => c.String(nullable: false));
            DropColumn("dbo.UMDNS", "Code");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UMDNS", "Code", c => c.String(nullable: false));
            DropColumn("dbo.UMDNS", "Codigo");
        }
    }
}
