namespace EquiposTecnicosSN.Web.DataContexts.EquiposMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateGastosOrdenes3 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.OrdenesDeTrabajo", name: "MantenimientoEquipoId", newName: "MantenimientoId");
            RenameIndex(table: "dbo.OrdenesDeTrabajo", name: "IX_MantenimientoEquipoId", newName: "IX_MantenimientoId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.OrdenesDeTrabajo", name: "IX_MantenimientoId", newName: "IX_MantenimientoEquipoId");
            RenameColumn(table: "dbo.OrdenesDeTrabajo", name: "MantenimientoId", newName: "MantenimientoEquipoId");
        }
    }
}
