namespace EwidencjaTonerow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTonerTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Toner", "PrinterID", "dbo.Printer");
            DropIndex("dbo.Toner", new[] { "PrinterID" });
            AlterColumn("dbo.Toner", "PrinterID", c => c.Int());
            CreateIndex("dbo.Toner", "PrinterID");
            AddForeignKey("dbo.Toner", "PrinterID", "dbo.Printer", "PrinterID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Toner", "PrinterID", "dbo.Printer");
            DropIndex("dbo.Toner", new[] { "PrinterID" });
            AlterColumn("dbo.Toner", "PrinterID", c => c.Int(nullable: false));
            CreateIndex("dbo.Toner", "PrinterID");
            AddForeignKey("dbo.Toner", "PrinterID", "dbo.Printer", "PrinterID", cascadeDelete: true);
        }
    }
}
