﻿using Microsoft.EntityFrameworkCore;
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
    }
}
