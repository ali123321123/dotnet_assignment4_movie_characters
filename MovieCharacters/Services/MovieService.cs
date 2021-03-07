using MovieCharacters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharacters.Services
{
    public class MovieService
    {
        private readonly MovieCharacterDbContext _context;

        public MovieService(MovieCharacterDbContext context)
        {
            _context = context;
        }

    }
}
