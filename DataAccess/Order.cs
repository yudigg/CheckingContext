using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{

    public enum OrderStatus
    {
        None,
        HasIssue,
        Finished,
        Paid
    }

    public class Order
    {
        public int OrderID { get; set; }

        public int UserID { get; set; }

        public virtual User User { get; set; }

        public OrderStatus? OrderStatus { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<Statement> Statements { get; set; }
    }
}
