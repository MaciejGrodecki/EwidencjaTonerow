namespace EwidencjaTonerow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStorehouseTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Storehouse",
                c => new
                    {
                        StorehouseID = c.Int(nullable: false, identity: true),
                        PrinterID = c.Int(nullable: false),
                        TonerID = c.Int(nullable: false),
                        RoomID = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StorehouseID)
                .ForeignKey("dbo.Printer", t => t.PrinterID, cascadeDelete: true)
                .ForeignKey("dbo.Room", t => t.RoomID, cascadeDelete: true)
                .ForeignKey("dbo.Toner", t => t.TonerID, cascadeDelete: true)
                .Index(t => t.PrinterID)
                .Index(t => t.TonerID)
                .Index(t => t.RoomID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Storehouse", "TonerID", "dbo.Toner");
            DropForeignKey("dbo.Storehouse", "RoomID", "dbo.Room");
            DropForeignKey("dbo.Storehouse", "PrinterID", "dbo.Printer");
            DropIndex("dbo.Storehouse", new[] { "RoomID" });
            DropIndex("dbo.Storehouse", new[] { "TonerID" });
            DropIndex("dbo.Storehouse", new[] { "PrinterID" });
            DropTable("dbo.Storehouse");
        }
    }
}
