namespace EquiposTecnicosSN.Web.DataContexts.EquiposMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrdenesDeTrabajo", "FechaCreacion", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrdenesDeTrabajo", "FechaCreacion");
        }
    }
}
