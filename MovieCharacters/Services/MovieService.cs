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
    public class MovieService : IMovieService
    {
        private readonly MovieCharacterDbContext _context;
        private readonly IMapper _mapper;

        public MovieService(IMapper mapper, MovieCharacterDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<bool> AddCharacterToMovie(int id, List<int> characters)
        {
            try
            {
                // Get movie from movieTable and also gets the movie characters on movieId
                Movie movie = await _context.Movies.Include(m => m.Characters).FirstOrDefaultAsync(m => m.Id == id);
                foreach (int characterId in characters)
                {
                    // check if character exist in movie
                    if (movie.Characters.FirstOrDefault(m => m.Id == characterId) == null)
                    {
                        // Get character by id
                        Character character = await _context.Characters.FindAsync(characterId);
                        // Add character to movie if found
                        if (character != null)
                        {
                            movie.Characters.Add(character);
                        }
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

        public async Task<bool> DeleteMovie(int id)
        {
            // Find movie by id
            Movie movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                // If not found return false
                return false;
            }
            // If found remove movie 
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CharacterDTO>> GetCharactersForMovie(int id)
        {
            // Find movie by id and get characters for that movie
            Movie movie = await _context.Movies.Include(m => m.Characters).FirstOrDefaultAsync(m => m.Id == id);

            // Map characters in movie to CharacterDTO
            IEnumerable<CharacterDTO> characterListDTO = _mapper.Map<IEnumerable<CharacterDTO>>(movie.Characters);

            return characterListDTO;
        }

        public async Task<IEnumerable<MovieDTO>> GetMoviesAsync()
        {
            // Get movies and characters in movie
            IEnumerable<Movie> MovieList = await _context.Movies.Include(m => m.Characters).ToListAsync();

            // Map movies to movieDTO
            IEnumerable<MovieDTO> MovieListDTO = _mapper.Map<IEnumerable<MovieDTO>>(MovieList);

            foreach(MovieDTO movie in MovieListDTO)
            {
                // Map all characters in movie to moviecharacterDTO to avoid overposting
                movie.Characters = _mapper.Map<List<MovieCharacterDTO>>(movie.Characters);
            }
            return MovieListDTO;
        }

        public async Task<ActionResult<MovieDTO>> GetMoviesByIdAsync(int id)
        {
            // Get movies by id and characters in movie
            Movie movie = await _context.Movies.Include(m => m.Characters).FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                throw new Exception();
            }
            // Map movies to movieDTO
            MovieDTO movieDTO = _mapper.Map<MovieDTO>(movie);
            // 
            List<MovieCharacterDTO> characterListDTO = _mapper.Map<List<MovieCharacterDTO>>(movie.Characters);
            movieDTO.Characters = characterListDTO;

            return movieDTO;
        }

        public async Task<MovieDTO> PostMovieAsync(MovieDTO movieDTO)
        {
            // Map movieDTO to Movie
            Movie movie = _mapper.Map<Movie>(movieDTO);
            // Add movie to database
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            // Map the newly created movie to movieDTO
            MovieDTO movieResponeDTO = _mapper.Map<MovieDTO>(movie);

            return movieResponeDTO;
        }

        public async Task<bool> UpdateCharactersForMovie(int id, List<int> characters)
        {
            // Get movie by id and characters in movie
            Movie movie = await _context.Movies.Include(m => m.Characters).FirstOrDefaultAsync(m => m.Id == id);
            if (characters == null)
            {
                // If not found return false
                return false;
            }
            // Clear the characters in the movie
            movie.Characters.Clear();
            foreach (int characterId in characters)
            {
                // Find characters by id and add to movie
                Character character = await _context.Characters.FindAsync(characterId);
                movie.Characters.Add(character);
            }
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> UpdateMovieAsync(int id, MovieDTO movieDTO)
        {
            // Map from movieDTO to Movie
            Movie movie = _mapper.Map<Movie>(movieDTO);
            try
            {
                // Updates the movie
                _context.Entry(movie).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
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

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
