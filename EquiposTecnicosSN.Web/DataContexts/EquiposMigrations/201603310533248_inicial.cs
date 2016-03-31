namespace EquiposTecnicosSN.Web.DataContexts.EquiposMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EquiposBase",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NombreCompleto = c.String(nullable: false, maxLength: 255),
                        UMDNS = c.String(nullable: false, maxLength: 50),
                        Tipo = c.Int(nullable: false),
                        NumeroSerie = c.String(maxLength: 255),
                        Modelo = c.String(maxLength: 255),
                        fechaCompra = c.DateTime(nullable: false),
                        numeroInventario = c.Int(nullable: false),
                        Estado = c.Int(nullable: false),
                        Ubicacion_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ubicaciones", t => t.Ubicacion_Id)
                .Index(t => t.Ubicacion_Id);
            
            CreateTable(
                "dbo.Ubicaciones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NombreCompleto = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EquiposClimatizacion",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EquiposBase", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.EquiposRespirador",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EquiposBase", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EquiposRespirador", "Id", "dbo.EquiposBase");
            DropForeignKey("dbo.EquiposClimatizacion", "Id", "dbo.EquiposBase");
            DropForeignKey("dbo.EquiposBase", "Ubicacion_Id", "dbo.Ubicaciones");
            DropIndex("dbo.EquiposRespirador", new[] { "Id" });
            DropIndex("dbo.EquiposClimatizacion", new[] { "Id" });
            DropIndex("dbo.EquiposBase", new[] { "Ubicacion_Id" });
            DropTable("dbo.EquiposRespirador");
            DropTable("dbo.EquiposClimatizacion");
            DropTable("dbo.Ubicaciones");
            DropTable("dbo.EquiposBase");
        }
    }
}
