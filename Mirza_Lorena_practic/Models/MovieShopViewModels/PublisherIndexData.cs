using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mirza_Lorena_practic.Models.MovieShopViewModels
{
    public class PublisherIndexData
    {
        public IEnumerable<Publisher> Publishers { get; set; }
        public IEnumerable<Movie> Movies { get; set; }
        public IEnumerable<Order> Orders
        {
            get; set;
        }
    }
}
