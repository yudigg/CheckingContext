using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
   public class FileRepo
    {
        private readonly string _connectionString;

        public FileRepo(string connStr)
        {
            _connectionString = connStr;
        }

        public void AddFile(Image image)
        {
            
            using (var dc = new CheckingContext(_connectionString))
            {
                dc.Images.Add(image);
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


    }
}
