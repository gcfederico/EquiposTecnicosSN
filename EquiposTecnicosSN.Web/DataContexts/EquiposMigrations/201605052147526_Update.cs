namespace EquiposTecnicosSN.Web.DataContexts.EquiposMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.InformacionComercial", "EsGrantiaContrato", c => c.Int());
            AlterColumn("dbo.InformacionComercial", "Financiamiento", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.InformacionComercial", "Financiamiento", c => c.Int(nullable: false));
            AlterColumn("dbo.InformacionComercial", "EsGrantiaContrato", c => c.Int(nullable: false));
        }
    }
}
