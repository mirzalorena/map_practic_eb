using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mirza_Lorena_practic.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int MovieID { get; set; }

        public Customer Customer { get; set; }
        public Movie Movie { get; set; }
    }
}
