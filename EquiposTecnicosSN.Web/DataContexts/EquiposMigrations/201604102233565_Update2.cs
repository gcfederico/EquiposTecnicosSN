namespace EquiposTecnicosSN.Web.DataContexts.EquiposMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Equipos", "NumeroInventario", c => c.Int());
            AlterColumn("dbo.InformacionComercial", "FechaCompra", c => c.DateTime());
            AlterColumn("dbo.InformacionComercial", "PrecioCompra", c => c.Int());
            AlterColumn("dbo.InformacionComercial", "ValorRestante", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.InformacionComercial", "ValorRestante", c => c.Int(nullable: false));
            AlterColumn("dbo.InformacionComercial", "PrecioCompra", c => c.Int(nullable: false));
            AlterColumn("dbo.InformacionComercial", "FechaCompra", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Equipos", "NumeroInventario", c => c.Int(nullable: false));
        }
    }
}
