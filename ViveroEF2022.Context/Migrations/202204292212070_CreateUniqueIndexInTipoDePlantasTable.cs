namespace ViveroEF2022.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateUniqueIndexInTipoDePlantasTable : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.TiposDePlantas", "Descripcion", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.TiposDePlantas", new[] { "Descripcion" });
        }
    }
}
