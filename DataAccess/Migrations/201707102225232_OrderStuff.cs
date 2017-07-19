namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderStuff : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        OrderStatus = c.Int(),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImageID = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        OrderID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ImageID)
                .ForeignKey("dbo.Orders", t => t.OrderID, cascadeDelete: true)
                .Index(t => t.OrderID);
            
            CreateTable(
                "dbo.Statements",
                c => new
                    {
                        StatementID = c.Int(nullable: false, identity: true),
                        FileName = c.Int(nullable: false),
                        OrderID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StatementID)
                .ForeignKey("dbo.Orders", t => t.OrderID, cascadeDelete: true)
                .Index(t => t.OrderID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "UserID", "dbo.Users");
            DropForeignKey("dbo.Statements", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.Images", "OrderID", "dbo.Orders");
            DropIndex("dbo.Statements", new[] { "OrderID" });
            DropIndex("dbo.Images", new[] { "OrderID" });
            DropIndex("dbo.Orders", new[] { "UserID" });
            DropTable("dbo.Statements");
            DropTable("dbo.Images");
            DropTable("dbo.Orders");
        }
    }
}
