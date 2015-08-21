namespace BookIt.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMigration : DbMigration
    {
        public override void Up()
        {
			DropForeignKey("dbo.TimeSlots", "OwnerID", "dbo.People");
			DropForeignKey("dbo.BookingSubjects", "OwnerID", "dbo.People");
			DropForeignKey("dbo.BookingOffers", "OwnerID", "dbo.People");

            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        RoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Roles", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.BookingOffers", "CategoryID", c => c.Int(nullable: false));
            AddColumn("dbo.BookingSubjects", "Capacity", c => c.Int(nullable: false));
            AddColumn("dbo.BookingSubjects", "CategoryID", c => c.Int(nullable: false));
            AddColumn("dbo.TimeSlots", "IsOccupied", c => c.Boolean(nullable: false));
            CreateIndex("dbo.BookingOffers", "CategoryID");
            CreateIndex("dbo.BookingSubjects", "CategoryID");
            AddForeignKey("dbo.BookingSubjects", "CategoryID", "dbo.Categories", "ID", cascadeDelete: true);
            AddForeignKey("dbo.BookingOffers", "CategoryID", "dbo.Categories", "ID", cascadeDelete: true);
			DropColumn("dbo.BookingOffers", "Category");
            DropColumn("dbo.BookingSubjects", "Count");
            DropColumn("dbo.BookingSubjects", "Category");
            DropColumn("dbo.TimeSlots", "IsBusy");
            DropTable("dbo.People");
        }
        
        public override void Down()
        {
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
            
            AddColumn("dbo.TimeSlots", "IsBusy", c => c.Boolean(nullable: false));
            AddColumn("dbo.BookingSubjects", "Category", c => c.Int(nullable: false));
            AddColumn("dbo.BookingSubjects", "Count", c => c.Int(nullable: false));
            AddColumn("dbo.BookingOffers", "Category", c => c.Int(nullable: false));
            DropForeignKey("dbo.BookingOffers", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Users", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.BookingSubjects", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Users", new[] { "RoleID" });
            DropIndex("dbo.BookingSubjects", new[] { "CategoryID" });
            DropIndex("dbo.BookingOffers", new[] { "CategoryID" });
            DropColumn("dbo.TimeSlots", "IsOccupied");
            DropColumn("dbo.BookingSubjects", "CategoryID");
            DropColumn("dbo.BookingSubjects", "Capacity");
            DropColumn("dbo.BookingOffers", "CategoryID");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Categories");
        }
    }
}
