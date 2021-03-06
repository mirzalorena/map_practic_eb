using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Mirza_Lorena_practic.Models;

namespace Mirza_Lorena_practic.Data
{
    public class MovieShopContext:DbContext
    {
        public MovieShopContext(DbContextOptions<MovieShopContext> options) :
       base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Movie> Movies { get; set; }

        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<PublishedMovie> PublishedMovies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Movie>().ToTable("Movie");

            modelBuilder.Entity<Publisher>().ToTable("Publisher");
            modelBuilder.Entity<PublishedMovie>().ToTable("PublishedMovie");
            modelBuilder.Entity<PublishedMovie>()
            .HasKey(c => new { c.MovieID, c.PublisherID });
        }
    }
}
