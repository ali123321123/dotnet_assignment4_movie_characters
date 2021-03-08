using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCharacters.DTO;
using MovieCharacters.Models;
using MovieCharacters.Models.DomainModels;
using MovieCharacters.Services;

namespace MovieCharacters.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {

        private readonly MovieCharacterDbContext _context;
        private readonly IMovieService _movieService;

        public MoviesController(MovieCharacterDbContext context, IMovieService movieService)
        {
            _context = context;
            _movieService = movieService;

        }

        // GET: api/Movies
        [HttpGet]
        public Task<IEnumerable<MovieDTO>> GetMovies()
        {
            return _movieService.GetMoviesAsync();
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDTO>> GetMovie(int id)
        {
            try
            {
                return await _movieService.GetMoviesByIdAsync(id);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, MovieDTO movieDTO)
        {
            if (id != movieDTO.Id)
            {
                return BadRequest();
            }
            
            bool updated = await _movieService.UpdateMovieAsync(id, movieDTO);
            if(updated)
            {
                return NoContent();
            }
            return NotFound();
        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(MovieDTO movieDTO)
        {
            int movieId = await _movieService.PostMovieAsync(movieDTO);
            return CreatedAtAction("GetMovie", new { id = movieId }, movieId);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovie(int id)
        {
            bool deleted = await _movieService.DeleteMovie(id);
            if (deleted)
            {
                return NoContent();
            }
            return NotFound();
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }

        [HttpGet("{id}/characters")]
        public async Task<ActionResult<IEnumerable<Character>>> GetCharactersForMovie(int id)
        {
            Movie movie = await _context.Movies.Include(m => m.Characters).FirstOrDefaultAsync(m => m.Id == id);
            foreach (Character character in movie.Characters)
            {
                movie.Characters = null;
            }
            return movie.Characters.ToList();
        }

        [HttpPost("{id}/characters")]
        public async Task<IActionResult> AddCharacterToMovie(int id, List<int> characters)
        {
            Movie movie = await _context.Movies.Include(m => m.Characters).FirstOrDefaultAsync(m => m.Id == id);
            if(characters == null)
            {
                return NotFound();
            }
            foreach (int characterId in characters)
            {
                if(movie.Characters.FirstOrDefault(m => m.Id == characterId) == null)
                {
                    Character character = await _context.Characters.FindAsync(characterId);
                    if(character != null)
                    {
                        movie.Characters.Add(character);
                    }
                }
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}/characters")]
        public async Task<IActionResult> UpdateCharactersForMovie(int id, List<int> characters)
        {
            bool updatedCharacters = await _movieService.UpdateCharactersForMovie(id, characters);
            if(updatedCharacters)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
