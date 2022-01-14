using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mirza_Lorena_practic.Models
{
    public class PublishedMovie
    {
        public int PublisherID { get; set; }
        public int MovieID { get; set; }
        public Publisher Publisher { get; set; }
        public Movie Movie { get; set; }
    }
}
