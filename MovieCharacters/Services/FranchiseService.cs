using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCharacters.DTO;
using MovieCharacters.Models;
using MovieCharacters.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharacters.Services
{
    public class FranchiseService : IFranchiseService
    {

        private readonly IMapper _mapper;
        private readonly MovieCharacterDbContext _context;

        public FranchiseService(MovieCharacterDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<bool> AddCharacterToFranchise(int id, List<int> characters)
        {
            try
            {

                Franchise franchise = await _context.Franchises.Include(f => f.Characters).FirstOrDefaultAsync(c => c.Id == id);
                foreach (int characterId in characters)

                    // Check if character exists in franchise
                    if (franchise.Characters.FirstOrDefault(c => c.Id == characterId) == null)
                    {
                        // Get character by id
                        Character character = await _context.Characters.FindAsync(characterId);

                        // Check if character is found, and add
                        if (character != null)
                        {
                            franchise.Characters.Add(character);
                        }
                    }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AddMovieToFranchise(int id, List<int> movies)
        {
            try
            {
                Franchise franchise = await _context.Franchises.Include(f => f.Movies).FirstOrDefaultAsync(m => m.Id == id);
                foreach (int movieId in movies)
                    // Check if movie exists
                    if (franchise.Movies.FirstOrDefault(m => m.Id == movieId) == null)
                    {
                        // Get movie by id
                        Movie movie = await _context.Movies.FindAsync(movieId);


                        // Add if movie is found
                        if (movie != null)
                        {
                            franchise.Movies.Add(movie);
                        }
                    }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteFranchise(int id)
        {

            FranchiseDTO franchiseDTO = new FranchiseDTO { Id = id };
            Franchise franchise = _mapper.Map<Franchise>(franchiseDTO);
            if (franchise == null)
            {
                return false;
            }
            // Delete franchise
            _context.Franchises.Remove(franchise);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<CharacterDTO>> GetCharactersForFranchise(int id)
        {
            // Find character by id and get characters for franchise
            Franchise franchise = await _context.Franchises.Include(f => f.Characters).FirstOrDefaultAsync(m => m.Id == id);

            // Map characters in franchise to CharacterDTO
            IEnumerable<CharacterDTO> characterListDTO = _mapper.Map<IEnumerable<CharacterDTO>>(franchise.Characters);

            return characterListDTO;
        }

        public async Task<ActionResult<FranchiseDTO>> GetFranchiseByIdAsync(int id)
        {
            // Get franchise by id
            var franchise = await _context.Franchises.FindAsync(id);
            FranchiseDTO franchiseDTO = _mapper.Map<FranchiseDTO>(franchise);

            // Check if franchise exists
            if (franchiseDTO == null)
            {
                return null;
            }

            return franchiseDTO;
        }

        public async Task<IEnumerable<FranchiseDTO>> GetFranchisesAsync()
        {
            // Get movies and characters in franchise
            IEnumerable<Franchise> FranchiseList = await _context.Franchises.Include(m => m.Movies)
                                                                            .Include(c => c.Characters)
                                                                            .ToListAsync();

            IEnumerable<FranchiseDTO> FranchiseListDTO = _mapper.Map<IEnumerable<FranchiseDTO>>(FranchiseList);

            foreach (FranchiseDTO franchise in FranchiseListDTO)
            {
                // Map all characters and movies in franchise to FranchiseDTO to avoid overposting
                franchise.Characters = _mapper.Map<List<FranchiseCharacterDTO>>(franchise.Characters);
                franchise.Movies = _mapper.Map<List<FranchiseMovieDTO>>(franchise.Movies);
            }
            return FranchiseListDTO;
        }

        public async Task<IEnumerable<MovieDTO>> GetMoviesForFranchise(int id)
        {
            // Find movies for franchise by id
            Franchise franchise = await _context.Franchises.Include(f => f.Movies).FirstOrDefaultAsync(m => m.Id == id);

            IEnumerable<MovieDTO> movieListDTO = _mapper.Map<IEnumerable<MovieDTO>>(franchise.Movies);

            return movieListDTO;
        }

        public async Task<FranchiseDTO> PostFranchiseAsync(FranchiseDTO franchiseDTO)
        {
            // Map to franchise
            Franchise franchise = _mapper.Map<Franchise>(franchiseDTO);

            franchise.Characters.Clear();
            franchise.Movies.Clear();
            // Checks if the franchiseDTO is null
            if (franchiseDTO.Characters != null)
            {
                foreach (FranchiseCharacterDTO character in franchiseDTO.Characters)
                {
                    if (character.Id != 0)
                    {
                        // Finds the character with the id and adds it to the movie
                        Character characters = await _context.Characters.FindAsync(character.Id);
                        franchise.Characters.Add(characters);
                    }
                }
            }

            // For movie
            if (franchiseDTO.Movies != null)
            {
                foreach (FranchiseMovieDTO movie in franchiseDTO.Movies)
                {
                    if (movie.Id != 0)
                    {
                        // Finds the movie with the id and adds it to the movie
                        Movie movies = await _context.Movies.FindAsync(movie.Id);
                        franchise.Movies.Add(movies);
                    }
                }
            }


            // Add franchise to database
            _context.Franchises.Add(franchise);
            await _context.SaveChangesAsync();

            return franchiseDTO;
        }



        public async Task<bool> UpdateFranchiseAsync(int id, FranchiseDTO franchiseDTO)
        {
            // Check if franchise exists
            if (!FranchiseExists(id))
            {
                return false;
            }

            // Map franchiseDTO to franchise
            Franchise franchise = _mapper.Map<Franchise>(franchiseDTO);

            _context.Entry(franchise).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FranchiseExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }


        public async Task<bool> UpdateCharactersForFranchise(int id, List<int> characters)
        {
            // Find franchise by id
            Franchise franchise = await _context.Franchises.Include(f => f.Characters).FirstOrDefaultAsync(c => c.Id == id);

            // Check if franchise has characters to update
            if (characters == null)
            {
                return false;
            }
            franchise.Characters.Clear();

            // Go over characters and update
            foreach (int characterId in characters)
            {
                Character character = await _context.Characters.FindAsync(characterId);
                franchise.Characters.Add(character);
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateMoviesForFranchise(int id, List<int> movies)
        {

            /// Find movie for franchise by id
            Franchise franchise = await _context.Franchises.Include(f => f.Movies).FirstOrDefaultAsync(m => m.Id == id);
            
            // Check if franchise has movie
            if (movies == null)
            {
                return false;
            }
            franchise.Movies.Clear();

            // Go over movies and update
            foreach (int movieId in movies)
            {
                Movie movie = await _context.Movies.FindAsync(movieId);
                franchise.Movies.Add(movie);
            }
            await _context.SaveChangesAsync();
            return true;
        }

        private bool FranchiseExists(int id)
        {
            // Check if franchise exists
            return _context.Franchises.Any(e => e.Id == id);
        }
    }
}
