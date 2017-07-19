using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Statement
    {
        public int StatementID{ get; set; }

        public int FileName { get; set; }

        public int OrderID { get; set; }

        public virtual Order Order { get; set; }
    }
}
