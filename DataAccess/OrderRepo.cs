using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
   public class OrderRepo
    {
        private string _connectionString;

        public OrderRepo(string Connstr)
        {
            _connectionString = Connstr;
        }

        public Order GetOrder(int orderId)
        {
            Order o = null;
            using(CheckingContext db = new CheckingContext(_connectionString))
            {
              o = db.Orders.Find(orderId);
            }
            return o;
        }

        public List<string> GetImagePaths(int orderId)
        {
            List<string> paths = new List<string>();
            
            using (CheckingContext db = new CheckingContext(_connectionString))
            {
              Order order = db.Orders.Find(orderId);
                foreach (var i in order.Images)
                {
                    string s = Path.GetFullPath(i.FileName);
                    paths.Add(s);
                    // FileStream stream = new FileStream(s, FileMode.Open);
                }
            }
            return paths;
        }
    }
}
