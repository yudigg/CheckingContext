namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedSalt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "PasswordHash", c => c.String());
            AddColumn("dbo.Users", "Salt", c => c.String());
            DropColumn("dbo.Users", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Password", c => c.String());
            DropColumn("dbo.Users", "Salt");
            DropColumn("dbo.Users", "PasswordHash");
        }
    }
}
