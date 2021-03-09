using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieCharacters.DTO;
using MovieCharacters.Models.DomainModels;
using MovieCharacters.Services;

namespace MovieCharacters.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {

        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
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
            return BadRequest();
        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MovieDTO>> PostMovie(MovieDTO movieDTO)
        {
            MovieDTO movie = await _movieService.PostMovieAsync(movieDTO);
            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }

        // DELETE: api/Movies/2
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovie(int id)
        {
            bool deleted = await _movieService.DeleteMovie(id);
            if (deleted)
            {
                return NoContent();
            }
            return BadRequest();
        }

        // GET api/movies/2/characters
        [HttpGet("{id}/characters")]
        public async Task<IEnumerable<CharacterDTO>> GetCharactersForMovie(int id)
        {
            return await _movieService.GetCharactersForMovie(id);
        }

        // POST api/movies/2/characters
        [HttpPost("{id}/characters")]
        public async Task<IActionResult> AddCharacterToMovie(int id, List<int> characters)
        {
            if (characters == null)
            {
                return BadRequest();
            }
            bool charactersAdded = await _movieService.AddCharacterToMovie(id, characters);
            if (charactersAdded)
            {
                return NoContent();
            }
            return BadRequest();
        }

        // PUT api/movies/2/characters
        // Add a list of character id's to a specific movie
        [HttpPut("{id}/characters")]
        public async Task<IActionResult> UpdateCharactersForMovie(int id, List<int> characters)
        {
            bool updatedCharacters = await _movieService.UpdateCharactersForMovie(id, characters);
            if(updatedCharacters)
            {
                return NoContent();
            }
            return BadRequest();
        }
    }
}
