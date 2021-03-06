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
            // Add data to movies
            modelBuilder.Entity<Movie>().HasData(new Movie { Id = 1, Title = "Inception", Genre = "Sci-fi", ReleaseYear = 2010, Director = "Christopher Nolan", Picture = "link", Trailer = "link", FranchiseId = null });
            modelBuilder.Entity<Movie>().HasData(new Movie { Id = 2, Title = "Avengers", Genre = "Adventure", ReleaseYear = 2012, Director = "Russo Brothers", Picture = "link", Trailer = "link", FranchiseId = null });
            modelBuilder.Entity<Movie>().HasData(new Movie { Id = 3, Title = "Fast&Furious", Genre = "Actuin", ReleaseYear = 2001, Director = "Rob Cohen", Picture = "link", Trailer = "link", FranchiseId = null });
            modelBuilder.Entity<Movie>().HasData(new Movie { Id = 4, Title = "Titanic", Genre = "Romance", ReleaseYear = 1997, Director = "James Cameron", Picture = "link", Trailer = "link", FranchiseId = null });

            modelBuilder.Entity<Character>().HasData(new Character { Id = 1, FirstName = "Bradley", LastName = "Cooper", Alias = "Nip", Gender = "Male", Picture = "link" });
            modelBuilder.Entity<Character>().HasData(new Character { Id = 2,  FirstName = "Carl", LastName = "Lumbly", Alias = "Justice League Unlimited", Gender = "Male", Picture = "link" });
            modelBuilder.Entity<Character>().HasData(new Character { Id = 3, FirstName = "Jennifer", LastName = "Garner", Alias = "Felicity", Gender = "Female", Picture = "link" });
            modelBuilder.Entity<Character>().HasData(new Character { Id = 4, FirstName = "Lena", LastName = "Olin", Alias = "Fanny and Alexander", Gender = "Female", Picture = "link" });

            // Franchise
            modelBuilder.Entity<Franchise>().HasData(new Franchise { Id = 1, Name = "Batman", Description = "Franchise about batman and stuff"});
            modelBuilder.Entity<Franchise>().HasData(new Franchise { Id = 2, Name = "Lord of the Rings", Description = "One of the greatest stories in recent history."});
            modelBuilder.Entity<Franchise>().HasData(new Franchise { Id = 3, Name = "Hobbit", Description = "A series of events in a trilogy that only needed a single movie."});

        }
        
    }
}
