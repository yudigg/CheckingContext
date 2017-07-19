namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnumRole : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Role_RoleId", "dbo.Roles");
            DropIndex("dbo.Users", new[] { "Role_RoleId" });
            AddColumn("dbo.Users", "Role", c => c.Int());
            DropColumn("dbo.Users", "RoleId");
            DropColumn("dbo.Users", "Role_RoleId");
            DropTable("dbo.Roles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Short(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            AddColumn("dbo.Users", "Role_RoleId", c => c.Short());
            AddColumn("dbo.Users", "RoleId", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "Role");
            CreateIndex("dbo.Users", "Role_RoleId");
            AddForeignKey("dbo.Users", "Role_RoleId", "dbo.Roles", "RoleId");
        }
    }
}
