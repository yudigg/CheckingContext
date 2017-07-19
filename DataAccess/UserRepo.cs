using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace DataAccess
{
   public class UserRepo
    {
        private readonly string _connectionString;

        public UserRepo(string connStr)
        {
            _connectionString = connStr;
        }

        public void AddUser(string username, string password)
        {
            User user = new User();
            string salt = PasswordHelper.GenerateRandomSalt();
            string passwordHash = PasswordHelper.HashPassword(password, salt);
            user.PasswordHash = passwordHash;
            user.Salt = salt;
            user.UserName = username;
            user.Role = Role.Admin;
            using (var dc = new CheckingContext(_connectionString))
            {
                dc.Users.Add(user);
                dc.SaveChanges();
            }
        }

       public void IsUserInRole()
        {

        }

        public User GetUser(string username, string password)
        {
            using (var dc = new CheckingContext(_connectionString))
            {
                var user = dc.Users.FirstOrDefault(a => a.UserName == username);
                if (user == null)
                {
                    return null;
                }
                bool success = PasswordHelper.PasswordMatch(password, user.PasswordHash, user.Salt);
                return success ? user : null;
            }
        }

        public bool HasPendingOrder(int userID)
        {
            using (var dc = new CheckingContext(_connectionString))
            {
                var user = dc.Users.Find(userID);
                if (user == null)
                {
                    return false;
                }
                 return user.Orders.Count == 0 ? false: user.Orders.Last().OrderStatus != OrderStatus.Paid;              
            }
        }

     
    }
}
