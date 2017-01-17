namespace EwidencjaTonerow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AllowNullFieldsInStorehouseTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Storehouse", "RoomID", "dbo.Room");
            DropForeignKey("dbo.Storehouse", "TonerID", "dbo.Toner");
            DropIndex("dbo.Storehouse", new[] { "TonerID" });
            DropIndex("dbo.Storehouse", new[] { "RoomID" });
            AlterColumn("dbo.Storehouse", "TonerID", c => c.Int());
            AlterColumn("dbo.Storehouse", "RoomID", c => c.Int());
            CreateIndex("dbo.Storehouse", "TonerID");
            CreateIndex("dbo.Storehouse", "RoomID");
            AddForeignKey("dbo.Storehouse", "RoomID", "dbo.Room", "RoomID");
            AddForeignKey("dbo.Storehouse", "TonerID", "dbo.Toner", "TonerID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Storehouse", "TonerID", "dbo.Toner");
            DropForeignKey("dbo.Storehouse", "RoomID", "dbo.Room");
            DropIndex("dbo.Storehouse", new[] { "RoomID" });
            DropIndex("dbo.Storehouse", new[] { "TonerID" });
            AlterColumn("dbo.Storehouse", "RoomID", c => c.Int(nullable: false));
            AlterColumn("dbo.Storehouse", "TonerID", c => c.Int(nullable: false));
            CreateIndex("dbo.Storehouse", "RoomID");
            CreateIndex("dbo.Storehouse", "TonerID");
            AddForeignKey("dbo.Storehouse", "TonerID", "dbo.Toner", "TonerID", cascadeDelete: true);
            AddForeignKey("dbo.Storehouse", "RoomID", "dbo.Room", "RoomID", cascadeDelete: true);
        }
    }
}
