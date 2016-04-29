namespace EquiposTecnicosSN.Web.DataContexts.EquiposMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Equipos", "FabricanteId", "dbo.Fabricantes");
            DropForeignKey("dbo.Equipos", "MarcaId", "dbo.Marcas");
            DropForeignKey("dbo.Equipos", "ModeloId", "dbo.Modelos");
            DropIndex("dbo.Equipos", new[] { "FabricanteId" });
            DropIndex("dbo.Equipos", new[] { "MarcaId" });
            DropIndex("dbo.Equipos", new[] { "ModeloId" });
            CreateTable(
                "dbo.InformacionHardware",
                c => new
                    {
                        EquipoId = c.Int(nullable: false),
                        NumeroSerie = c.String(maxLength: 255),
                        FabricanteId = c.Int(nullable: false),
                        MarcaId = c.Int(nullable: false),
                        ModeloId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EquipoId)
                .ForeignKey("dbo.Equipos", t => t.EquipoId)
                .ForeignKey("dbo.Fabricantes", t => t.FabricanteId, cascadeDelete: true)
                .ForeignKey("dbo.Marcas", t => t.MarcaId, cascadeDelete: true)
                .ForeignKey("dbo.Modelos", t => t.ModeloId, cascadeDelete: true)
                .Index(t => t.EquipoId)
                .Index(t => t.FabricanteId)
                .Index(t => t.MarcaId)
                .Index(t => t.ModeloId);
            
            DropColumn("dbo.Equipos", "NumeroSerie");
            DropColumn("dbo.Equipos", "FabricanteId");
            DropColumn("dbo.Equipos", "MarcaId");
            DropColumn("dbo.Equipos", "ModeloId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Equipos", "ModeloId", c => c.Int(nullable: false));
            AddColumn("dbo.Equipos", "MarcaId", c => c.Int(nullable: false));
            AddColumn("dbo.Equipos", "FabricanteId", c => c.Int(nullable: false));
            AddColumn("dbo.Equipos", "NumeroSerie", c => c.String(maxLength: 255));
            DropForeignKey("dbo.InformacionHardware", "ModeloId", "dbo.Modelos");
            DropForeignKey("dbo.InformacionHardware", "MarcaId", "dbo.Marcas");
            DropForeignKey("dbo.InformacionHardware", "FabricanteId", "dbo.Fabricantes");
            DropForeignKey("dbo.InformacionHardware", "EquipoId", "dbo.Equipos");
            DropIndex("dbo.InformacionHardware", new[] { "ModeloId" });
            DropIndex("dbo.InformacionHardware", new[] { "MarcaId" });
            DropIndex("dbo.InformacionHardware", new[] { "FabricanteId" });
            DropIndex("dbo.InformacionHardware", new[] { "EquipoId" });
            DropTable("dbo.InformacionHardware");
            CreateIndex("dbo.Equipos", "ModeloId");
            CreateIndex("dbo.Equipos", "MarcaId");
            CreateIndex("dbo.Equipos", "FabricanteId");
            AddForeignKey("dbo.Equipos", "ModeloId", "dbo.Modelos", "ModeloId", cascadeDelete: true);
            AddForeignKey("dbo.Equipos", "MarcaId", "dbo.Marcas", "MarcaId", cascadeDelete: true);
            AddForeignKey("dbo.Equipos", "FabricanteId", "dbo.Fabricantes", "FabricanteId", cascadeDelete: true);
        }
    }
}
