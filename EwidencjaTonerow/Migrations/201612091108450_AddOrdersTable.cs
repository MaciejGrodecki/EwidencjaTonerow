namespace EwidencjaTonerow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrdersTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        SendDate = c.DateTime(nullable: false),
                        DeliveryDate = c.DateTime(),
                        ChangeDate = c.DateTime(),
                        PrinterID = c.Int(nullable: false),
                        RoomID = c.Int(nullable: false),
                        TonerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Printer", t => t.PrinterID, cascadeDelete: true)
                .ForeignKey("dbo.Room", t => t.RoomID, cascadeDelete: true)
                .ForeignKey("dbo.Toner", t => t.TonerID, cascadeDelete: true)
                .Index(t => t.PrinterID)
                .Index(t => t.RoomID)
                .Index(t => t.TonerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Order", "TonerID", "dbo.Toner");
            DropForeignKey("dbo.Order", "RoomID", "dbo.Room");
            DropForeignKey("dbo.Order", "PrinterID", "dbo.Printer");
            DropIndex("dbo.Order", new[] { "TonerID" });
            DropIndex("dbo.Order", new[] { "RoomID" });
            DropIndex("dbo.Order", new[] { "PrinterID" });
            DropTable("dbo.Order");
        }
    }
}
