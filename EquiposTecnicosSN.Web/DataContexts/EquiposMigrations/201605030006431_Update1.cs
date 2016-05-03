namespace EquiposTecnicosSN.Web.DataContexts.EquiposMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ubicaciones", "Nombre", c => c.String(nullable: false, maxLength: 255));
            DropColumn("dbo.Ubicaciones", "NombreCompleto");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ubicaciones", "NombreCompleto", c => c.String(nullable: false, maxLength: 255));
            DropColumn("dbo.Ubicaciones", "Nombre");
        }
    }
}
