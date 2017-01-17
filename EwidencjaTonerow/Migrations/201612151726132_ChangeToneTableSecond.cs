namespace EwidencjaTonerow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeToneTableSecond : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Toner", "PrinterID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Toner", "PrinterID", c => c.Int());
        }
    }
}
