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
 new Movie{Title="Baltagul",Director="MihailSadoveanu",Price=Decimal.Parse("22")},
 new Movie{Title="Enigma Otiliei",Director="GeorgeCalinescu",Price=Decimal.Parse("18")},
 new Movie{Title="Maytrei",Director="MirceaEliade",Price=Decimal.Parse("27")},
 new Movie{Title="Panza de paianjen",Director="CellaSerghi",Price=Decimal.Parse("45")},
 new Movie{Title="Fata de hartie",Director="GuillameMusso",Price=Decimal.Parse("38")},
 new Movie{Title="De veghe in lanul de secara",Director="J.D.Salinger",Price=Decimal.Parse("28")},
            };
            foreach (Movie b in Movies)
            {
                context.Movies.Add(b);
            }
            context.SaveChanges();
            var customers = new Customer[]
            {

 new Customer{CustomerID=1050,Name="PopescuMarcela",BirthDate=DateTime.Parse("1979-09-01")},
 new Customer{CustomerID=1045,Name="MihailescuCornel",BirthDate=DateTime.Parse("1969-07-08")},

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

 new Publisher{PublisherName="Humanitas",Adress="Str. Aviatorilor, nr. 40,Bucuresti"},
 new Publisher{PublisherName="Nemira",Adress="Str. Plopilor, nr. 35,Ploiesti"},
 new Publisher{PublisherName="Paralela 45",Adress="Str. Cascadelor, nr.22, Cluj-Napoca"},
            };
            foreach (Publisher p in publishers)
            {
                context.Publishers.Add(p);
            }
            context.SaveChanges();
            var PublishedMovies = new PublishedMovie[]
            {
 new PublishedMovie {
 MovieID = Movies.Single(c => c.Title == "Maytrei" ).ID,
 PublisherID = publishers.Single(i => i.PublisherName ==
"Humanitas").ID
 },
 new PublishedMovie {
 MovieID = Movies.Single(c => c.Title == "Enigma Otiliei" ).ID,
PublisherID = publishers.Single(i => i.PublisherName ==
"Humanitas").ID
 },
 new PublishedMovie {
 MovieID = Movies.Single(c => c.Title == "Baltagul" ).ID,
 PublisherID = publishers.Single(i => i.PublisherName ==
"Nemira").ID
 },
 new PublishedMovie {
 MovieID = Movies.Single(c => c.Title == "Fata de hartie" ).ID,
PublisherID = publishers.Single(i => i.PublisherName == "Paralela45").ID
 },
 new PublishedMovie {
 MovieID = Movies.Single(c => c.Title == "Panza de paianjen" ).ID,
PublisherID = publishers.Single(i => i.PublisherName == "Paralela45").ID
 },
 new PublishedMovie {
 MovieID = Movies.Single(c => c.Title == "De veghe in lanul desecara" ).ID,
 PublisherID = publishers.Single(i => i.PublisherName == "Paralela45").ID
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
