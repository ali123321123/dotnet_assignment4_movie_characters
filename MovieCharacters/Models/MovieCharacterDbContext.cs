using Microsoft.EntityFrameworkCore;
using MovieCharacters.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharacters.Models
{
    public class MovieCharacterDbContext : DbContext
    {

        // Tables
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Franchise> Franchises { get; set; }
        public DbSet<Character> Characters { get; set; }

        // Configure the service
        public MovieCharacterDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasData(new Movie { Id = 1, Title = "Inception", Genre = "Thriller", ReleaseYear = 2010, Director = "Christopher Nolan", Picture = "link", Trailer = "link", FranchiseId = null });
            modelBuilder.Entity<Character>().HasData(new Character { Id = 1, FirstName = "William Bradley", LastName = " Pitt", Alias = "Brad Pitt", Gender = "male", Picture = "link" });
        }
             

    }
    }

