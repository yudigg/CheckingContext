using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
   public class CheckingContext: DbContext
    {
        public CheckingContext(): base("name=CheckbookBuddyContext")
        {

        }
        public CheckingContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CheckingContext, DataAccess.Migrations.Configuration>("CheckbookBuddyContext"));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Order> Orders { get; set; }


        public void AddUser(User user)
        {
            Users.Add(user);
            SaveChanges();
        }

        public User GetUser(string userName)
        {
            var user = Users.SingleOrDefault(u => u.UserName == userName);
            return user;
        }

        public User GetUser(string userName, string password)
        {
            var user = Users.SingleOrDefault(u => u.UserName ==
                           userName && u.PasswordHash == password);
            return user;
        }

     
    }
}
