using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Mirza_Lorena_practic.Models;

namespace Mirza_Lorena_practic.Data
{
    public class DbInitializer
    {
        public static void Initialize(MovieShopContext context)
        {
            context.Database.EnsureCreated();
            if (context.Movies.Any())
            {
                return; // BD a fost creata anterior
            }
            var Movies = new Movie[]
            {
                 new Movie{Title="Baltagul",Director="Mihail Sadoveanu",Price=Decimal.Parse("22")},
                 new Movie{Title="Enigma Otiliei",Director="George Calinescu",Price=Decimal.Parse("18")},
                 new Movie{Title="Maytrei",Director="Mircea Eliade",Price=Decimal.Parse("27")}
            };
            foreach (Movie s in Movies)
            {
                context.Movies.Add(s);
            }
            context.SaveChanges();
            var customers = new Customer[]
            {

                 new Customer{CustomerID=1050,Name="Popescu Marcela",BirthDate=DateTime.Parse("1979-09-01")},
                new Customer{CustomerID=1045,Name="Mihailescu Cornel",BirthDate=DateTime.Parse("1969-07-08")},

 };
            foreach (Customer c in customers)
            {
                context.Customers.Add(c);
            }
            context.SaveChanges();
            var orders = new Order[]
            {
                 new Order{MovieID=1,CustomerID=1050},
                 new Order{MovieID=3,CustomerID=1045},
                 new Order{MovieID=1,CustomerID=1045},
                 new Order{MovieID=2,CustomerID=1050},
            };
            foreach (Order e in orders)
            {
                context.Orders.Add(e);
            }
            context.SaveChanges();
        }
    }
}
