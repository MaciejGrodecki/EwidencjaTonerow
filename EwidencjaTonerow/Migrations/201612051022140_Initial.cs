namespace EwidencjaTonerow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Printer",
                c => new
                    {
                        PrinterID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PrinterID);
            
            CreateTable(
                "dbo.Room",
                c => new
                    {
                        RoomID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.RoomID);
            
            CreateTable(
                "dbo.PrinterRoom",
                c => new
                    {
                        PrinterID = c.Int(nullable: false),
                        RoomID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PrinterID, t.RoomID })
                .ForeignKey("dbo.Printer", t => t.PrinterID, cascadeDelete: true)
                .ForeignKey("dbo.Room", t => t.RoomID, cascadeDelete: true)
                .Index(t => t.PrinterID)
                .Index(t => t.RoomID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PrinterRoom", "RoomID", "dbo.Room");
            DropForeignKey("dbo.PrinterRoom", "PrinterID", "dbo.Printer");
            DropIndex("dbo.PrinterRoom", new[] { "RoomID" });
            DropIndex("dbo.PrinterRoom", new[] { "PrinterID" });
            DropTable("dbo.PrinterRoom");
            DropTable("dbo.Room");
            DropTable("dbo.Printer");
        }
    }
}
