using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mirza_Lorena_practic.Models
{
    public class Movie
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public decimal Price { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
