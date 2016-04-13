namespace EquiposTecnicosSN.Web.DataContexts.EquiposMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MantenimientosEquipo", "FechaDeFin", c => c.DateTime());
            AlterColumn("dbo.InformacionComercial", "FechaFinGarantia", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.InformacionComercial", "FechaFinGarantia", c => c.DateTime(nullable: false));
            AlterColumn("dbo.MantenimientosEquipo", "FechaDeFin", c => c.DateTime(nullable: false));
        }
    }
}
