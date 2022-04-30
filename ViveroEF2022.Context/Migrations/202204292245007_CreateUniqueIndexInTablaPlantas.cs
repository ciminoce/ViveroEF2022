namespace ViveroEF2022.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateUniqueIndexInTablaPlantas : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Plantas", "Descripcion", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Plantas", new[] { "Descripcion" });
        }
    }
}
