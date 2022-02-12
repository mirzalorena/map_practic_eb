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
 new Movie{Title="Pulp Fiction",Director="Quentin Tarantino",Price=Decimal.Parse("22")},
 new Movie{Title="The Guard",Director="John Michael McDonagh",Price=Decimal.Parse("18")},
 new Movie{Title="Moonrise Kingdom",Director="Wes Anderson",Price=Decimal.Parse("27")},
 new Movie{Title="Gravity",Director="Alfonso Cuarón",Price=Decimal.Parse("45")},
 new Movie{Title="Snowpiercer",Director="Joon-ho Bong",Price=Decimal.Parse("38")},
 new Movie{Title="Birdman",Director="Alejandro González Iñárritu",Price=Decimal.Parse("28")},
            };
            foreach (Movie b in Movies)
            {
                context.Movies.Add(b);
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
 new Order{MovieID=1,CustomerID=1050,OrderDate=DateTime.Parse("02-25-2020")},
 new Order{MovieID=3,CustomerID=1045,OrderDate=DateTime.Parse("09-28-2020")},
 new Order{MovieID=1,CustomerID=1045,OrderDate=DateTime.Parse("10-28-2020")},
 new Order{MovieID=2,CustomerID=1050,OrderDate=DateTime.Parse("09-28-2020")},
 new Order{MovieID=4,CustomerID=1050,OrderDate=DateTime.Parse("09-28-2020")},
 new Order{MovieID=6,CustomerID=1050,OrderDate=DateTime.Parse("10-28-2020")},
 };
            foreach (Order e in orders)
            {
                context.Orders.Add(e);
            }
            context.SaveChanges();
            var publishers = new Publisher[]
            {

 new Publisher{PublisherName="Warner Bros",Adress="Str. Aviatorilor, nr. 40,Bucuresti"},
 new Publisher{PublisherName="Sony Pictures Motion Picture Group",Adress="Str. Plopilor, nr. 35,Ploiesti"},
 new Publisher{PublisherName="Universal Pictures",Adress="Str. Cascadelor, nr.22, Cluj-Napoca"},
            };
            foreach (Publisher p in publishers)
            {
                context.Publishers.Add(p);
            }
            context.SaveChanges();
            var PublishedMovies = new PublishedMovie[]
            {
 new PublishedMovie {
 MovieID = Movies.Single(c => c.Title == "Gravity" ).ID,
 PublisherID = publishers.Single(i => i.PublisherName ==
"Warner Bros").ID
 },
 new PublishedMovie {
 MovieID = Movies.Single(c => c.Title == "Snowpiercer" ).ID,
PublisherID = publishers.Single(i => i.PublisherName ==
"Universal Pictures").ID
 },
 new PublishedMovie {
 MovieID = Movies.Single(c => c.Title == "Birdman" ).ID,
 PublisherID = publishers.Single(i => i.PublisherName ==
"Sony Pictures Motion Picture Group").ID
 },
 new PublishedMovie {
 MovieID = Movies.Single(c => c.Title == "Pulp Fiction" ).ID,
PublisherID = publishers.Single(i => i.PublisherName == "Universal Pictures").ID
 },
 new PublishedMovie {
 MovieID = Movies.Single(c => c.Title == "The Guard" ).ID,
PublisherID = publishers.Single(i => i.PublisherName == "Warner Bros").ID
 },
 new PublishedMovie {
 MovieID = Movies.Single(c => c.Title == "Moonrise Kingdom" ).ID,
 PublisherID = publishers.Single(i => i.PublisherName == "Universal Picture").ID
 },
            };
            foreach (PublishedMovie pb in PublishedMovies)
            {
                context.PublishedMovies.Add(pb);
            }
            context.SaveChanges();
        }
    }
}
