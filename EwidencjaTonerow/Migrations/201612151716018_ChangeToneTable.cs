namespace EwidencjaTonerow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeToneTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Toner", "PrinterID", "dbo.Printer");
            DropIndex("dbo.Toner", new[] { "PrinterID" });
            CreateTable(
                "dbo.PrinterToner",
                c => new
                    {
                        PrinterID = c.Int(nullable: false),
                        TonerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PrinterID, t.TonerID })
                .ForeignKey("dbo.Printer", t => t.PrinterID, cascadeDelete: true)
                .ForeignKey("dbo.Toner", t => t.TonerID, cascadeDelete: true)
                .Index(t => t.PrinterID)
                .Index(t => t.TonerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PrinterToner", "TonerID", "dbo.Toner");
            DropForeignKey("dbo.PrinterToner", "PrinterID", "dbo.Printer");
            DropIndex("dbo.PrinterToner", new[] { "TonerID" });
            DropIndex("dbo.PrinterToner", new[] { "PrinterID" });
            DropTable("dbo.PrinterToner");
            CreateIndex("dbo.Toner", "PrinterID");
            AddForeignKey("dbo.Toner", "PrinterID", "dbo.Printer", "PrinterID");
        }
    }
}
