namespace PrettyCats.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLogTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Logged = c.DateTime(nullable: false),
                        Level = c.String(maxLength: 50),
                        Message = c.String(),
                        Url = c.String(),
                        Logger = c.String(maxLength: 250),
                        CallSite = c.String(),
                        Exception = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Logs");
        }
    }
}
