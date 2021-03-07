﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCharacters.Models;
using MovieCharacters.Models.DomainModels;

namespace MovieCharacters.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieCharacterDbContext _context;

        public MoviesController(MovieCharacterDbContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
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
        public async Task<IActionResult> CharacterToMovie(int id, List<int> characters)
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
            Movie movie = await _context.Movies.Include(m => m.Characters).FirstOrDefaultAsync(m => m.Id == id);
            if (characters == null)
            {
                return NotFound();
            }
            movie.Characters.Clear();
            foreach (int characterId in characters)
            {

                Character character = await _context.Characters.FindAsync(characterId);
                movie.Characters.Add(character);
            }
            await _context.SaveChangesAsync();
            return NoContent();

        }
    }
}
