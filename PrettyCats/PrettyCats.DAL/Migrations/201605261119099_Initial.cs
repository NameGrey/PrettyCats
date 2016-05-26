namespace PrettyCats.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DisplayPlaces",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PlaceOfDisplaying = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Owners",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Phone = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Pages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PetBreeds",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        RussianName = c.String(maxLength: 50),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Pets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        RussianName = c.String(maxLength: 55),
                        BreedID = c.Int(nullable: false),
                        BirthDate = c.DateTime(storeType: "date"),
                        UnderThePictureText = c.String(),
                        OwnerID = c.Int(nullable: false),
                        MotherID = c.Int(),
                        FatherID = c.Int(),
                        WhereDisplay = c.Int(nullable: false),
                        Status = c.String(maxLength: 50),
                        Color = c.String(maxLength: 50),
                        IsParent = c.Boolean(nullable: false),
                        Price = c.Int(),
                        IsInArchive = c.Boolean(nullable: false),
                        VideoUrl = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.DisplayPlaces", t => t.WhereDisplay, cascadeDelete: true)
                .ForeignKey("dbo.Pets", t => t.FatherID)
                .ForeignKey("dbo.Pets", t => t.MotherID)
                .ForeignKey("dbo.Owners", t => t.OwnerID)
                .ForeignKey("dbo.PetBreeds", t => t.BreedID)
                .Index(t => t.BreedID)
                .Index(t => t.OwnerID)
                .Index(t => t.MotherID)
                .Index(t => t.FatherID)
                .Index(t => t.WhereDisplay);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        ImageSmall = c.String(),
                        CssClass = c.String(maxLength: 50),
                        Order = c.Int(nullable: false),
                        IsMainPicture = c.Boolean(nullable: false),
                        PetID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Pets", t => t.PetID, cascadeDelete: true)
                .Index(t => t.PetID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pictures", "PetID", "dbo.Pets");
            DropForeignKey("dbo.Pets", "BreedID", "dbo.PetBreeds");
            DropForeignKey("dbo.Pets", "OwnerID", "dbo.Owners");
            DropForeignKey("dbo.Pets", "MotherID", "dbo.Pets");
            DropForeignKey("dbo.Pets", "FatherID", "dbo.Pets");
            DropForeignKey("dbo.Pets", "WhereDisplay", "dbo.DisplayPlaces");
            DropIndex("dbo.Pictures", new[] { "PetID" });
            DropIndex("dbo.Pets", new[] { "WhereDisplay" });
            DropIndex("dbo.Pets", new[] { "FatherID" });
            DropIndex("dbo.Pets", new[] { "MotherID" });
            DropIndex("dbo.Pets", new[] { "OwnerID" });
            DropIndex("dbo.Pets", new[] { "BreedID" });
            DropTable("dbo.Pictures");
            DropTable("dbo.Pets");
            DropTable("dbo.PetBreeds");
            DropTable("dbo.Pages");
            DropTable("dbo.Owners");
            DropTable("dbo.DisplayPlaces");
        }
    }
}
