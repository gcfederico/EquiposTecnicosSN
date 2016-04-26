namespace EquiposTecnicosSN.Web.DataContexts.EquiposMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.InformacionComercial", "ProveedorId", "dbo.Proveedores");
            DropIndex("dbo.InformacionComercial", new[] { "ProveedorId" });
            AlterColumn("dbo.InformacionComercial", "ProveedorId", c => c.Int());
            CreateIndex("dbo.InformacionComercial", "ProveedorId");
            AddForeignKey("dbo.InformacionComercial", "ProveedorId", "dbo.Proveedores", "ProveedorId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InformacionComercial", "ProveedorId", "dbo.Proveedores");
            DropIndex("dbo.InformacionComercial", new[] { "ProveedorId" });
            AlterColumn("dbo.InformacionComercial", "ProveedorId", c => c.Int(nullable: false));
            CreateIndex("dbo.InformacionComercial", "ProveedorId");
            AddForeignKey("dbo.InformacionComercial", "ProveedorId", "dbo.Proveedores", "ProveedorId", cascadeDelete: true);
        }
    }
}
