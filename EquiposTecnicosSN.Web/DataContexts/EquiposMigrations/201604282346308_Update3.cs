namespace EquiposTecnicosSN.Web.DataContexts.EquiposMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InformacionComercial", "Financiamiento", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InformacionComercial", "Financiamiento");
        }
    }
}
