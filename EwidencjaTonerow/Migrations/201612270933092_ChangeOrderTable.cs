namespace EwidencjaTonerow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeOrderTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Order", "PrinterID", "dbo.Printer");
            DropForeignKey("dbo.Order", "RoomID", "dbo.Room");
            DropForeignKey("dbo.Order", "TonerID", "dbo.Toner");
            DropIndex("dbo.Order", new[] { "PrinterID" });
            DropIndex("dbo.Order", new[] { "RoomID" });
            DropIndex("dbo.Order", new[] { "TonerID" });
            AddColumn("dbo.Order", "StorehouseID", c => c.Int(nullable: false));
            CreateIndex("dbo.Order", "StorehouseID");
            AddForeignKey("dbo.Order", "StorehouseID", "dbo.Storehouse", "StorehouseID", cascadeDelete: true);
            DropColumn("dbo.Order", "PrinterID");
            DropColumn("dbo.Order", "RoomID");
            DropColumn("dbo.Order", "TonerID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Order", "TonerID", c => c.Int(nullable: false));
            AddColumn("dbo.Order", "RoomID", c => c.Int(nullable: false));
            AddColumn("dbo.Order", "PrinterID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Order", "StorehouseID", "dbo.Storehouse");
            DropIndex("dbo.Order", new[] { "StorehouseID" });
            DropColumn("dbo.Order", "StorehouseID");
            CreateIndex("dbo.Order", "TonerID");
            CreateIndex("dbo.Order", "RoomID");
            CreateIndex("dbo.Order", "PrinterID");
            AddForeignKey("dbo.Order", "TonerID", "dbo.Toner", "TonerID", cascadeDelete: true);
            AddForeignKey("dbo.Order", "RoomID", "dbo.Room", "RoomID", cascadeDelete: true);
            AddForeignKey("dbo.Order", "PrinterID", "dbo.Printer", "PrinterID", cascadeDelete: true);
        }
    }
}
