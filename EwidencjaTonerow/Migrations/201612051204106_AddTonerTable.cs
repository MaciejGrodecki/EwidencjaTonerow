namespace EwidencjaTonerow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTonerTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Toner",
                c => new
                    {
                        TonerID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        PrinterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TonerID)
                .ForeignKey("dbo.Printer", t => t.PrinterID, cascadeDelete: true)
                .Index(t => t.PrinterID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Toner", "PrinterID", "dbo.Printer");
            DropIndex("dbo.Toner", new[] { "PrinterID" });
            DropTable("dbo.Toner");
        }
    }
}
