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
                Movie movie = await _context.Movies.Include(m => m.Characters).FirstOrDefaultAsync(m => m.Id == id);
                foreach (int characterId in characters)
                {
                    if (movie.Characters.FirstOrDefault(m => m.Id == characterId) == null)
                    {
                        Character character = await _context.Characters.FindAsync(characterId);
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
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return false;
            }
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CharacterDTO>> GetCharactersForMovie(int id)
        {
            Movie movie = await _context.Movies.Include(m => m.Characters).FirstOrDefaultAsync(m => m.Id == id);

            IEnumerable<CharacterDTO> characterListDTO = _mapper.Map<IEnumerable<CharacterDTO>>(movie.Characters);

            return characterListDTO;
        }

        public async Task<IEnumerable<MovieDTO>> GetMoviesAsync()
        {
            IEnumerable<Movie> MovieList = await _context.Movies.Include(m => m.Characters).ToListAsync();

            IEnumerable<MovieDTO> MovieListDTO = _mapper.Map<IEnumerable<MovieDTO>>(MovieList);

            foreach(MovieDTO movie in MovieListDTO)
            {
                movie.Characters = _mapper.Map<List<MovieCharacterDTO>>(movie.Characters);
            }
            return MovieListDTO;
        }

        public async Task<ActionResult<MovieDTO>> GetMoviesByIdAsync(int id)
        {
            var movie = await _context.Movies.Include(m => m.Characters).FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                throw new Exception();
            }
            MovieDTO movieDTO = _mapper.Map<MovieDTO>(movie);
            List<MovieCharacterDTO> characterListDTO = _mapper.Map<List<MovieCharacterDTO>>(movie.Characters);
            movieDTO.Characters = characterListDTO;

            return movieDTO;
        }

        public async Task<int> PostMovieAsync(MovieDTO movieDTO)
        {
            Movie movie = _mapper.Map<Movie>(movieDTO);
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return movie.Id;
        }

        public async Task<bool> UpdateCharactersForMovie(int id, List<int> characters)
        {
            Movie movie = await _context.Movies.Include(m => m.Characters).FirstOrDefaultAsync(m => m.Id == id);
            if (characters == null)
            {
                return false;
            }
            movie.Characters.Clear();
            foreach (int characterId in characters)
            {

                Character character = await _context.Characters.FindAsync(characterId);
                movie.Characters.Add(character);
            }
            await _context.SaveChangesAsync();
            return true;


        }

        public async Task<bool> UpdateMovieAsync(int id, MovieDTO movieDTO)
        {
            Movie movie = _mapper.Map<Movie>(movieDTO);
            try
            {
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
