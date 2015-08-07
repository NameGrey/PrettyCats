namespace BookIt.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookingOffers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Category = c.Int(nullable: false),
                        IsInfinite = c.Boolean(nullable: false),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        OwnerID = c.Int(nullable: false),
                        BookingSubjectID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BookingSubjects", t => t.BookingSubjectID)
                .ForeignKey("dbo.People", t => t.OwnerID, cascadeDelete: true)
                .Index(t => t.OwnerID)
                .Index(t => t.BookingSubjectID);
            
            CreateTable(
                "dbo.BookingSubjects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Count = c.Int(nullable: false),
                        Category = c.Int(nullable: false),
                        OwnerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.People", t => t.OwnerID, cascadeDelete: true)
                .Index(t => t.OwnerID);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TimeSlots",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IsBusy = c.Boolean(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        OwnerID = c.Int(),
                        BookingOfferID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BookingOffers", t => t.BookingOfferID, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.OwnerID)
                .Index(t => t.OwnerID)
                .Index(t => t.BookingOfferID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimeSlots", "OwnerID", "dbo.People");
            DropForeignKey("dbo.TimeSlots", "BookingOfferID", "dbo.BookingOffers");
            DropForeignKey("dbo.BookingSubjects", "OwnerID", "dbo.People");
            DropForeignKey("dbo.BookingOffers", "OwnerID", "dbo.People");
            DropForeignKey("dbo.BookingOffers", "BookingSubjectID", "dbo.BookingSubjects");
            DropIndex("dbo.TimeSlots", new[] { "BookingOfferID" });
            DropIndex("dbo.TimeSlots", new[] { "OwnerID" });
            DropIndex("dbo.BookingSubjects", new[] { "OwnerID" });
            DropIndex("dbo.BookingOffers", new[] { "BookingSubjectID" });
            DropIndex("dbo.BookingOffers", new[] { "OwnerID" });
            DropTable("dbo.TimeSlots");
            DropTable("dbo.People");
            DropTable("dbo.BookingSubjects");
            DropTable("dbo.BookingOffers");
        }
    }
}
