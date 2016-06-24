namespace PrettyCats.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PetBreeds", "ShortName", c => c.String(maxLength: 50));
            AddColumn("dbo.PetBreeds", "FullName", c => c.String());
            AddColumn("dbo.PetBreeds", "LinkPage", c => c.String());
            AddColumn("dbo.PetBreeds", "PicturePath", c => c.String());
            DropColumn("dbo.PetBreeds", "RussianName");
            DropColumn("dbo.PetBreeds", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PetBreeds", "Description", c => c.String());
            AddColumn("dbo.PetBreeds", "RussianName", c => c.String(maxLength: 50));
            DropColumn("dbo.PetBreeds", "PicturePath");
            DropColumn("dbo.PetBreeds", "LinkPage");
            DropColumn("dbo.PetBreeds", "FullName");
            DropColumn("dbo.PetBreeds", "ShortName");
        }
    }
}
