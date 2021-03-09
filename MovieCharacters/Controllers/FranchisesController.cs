using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class FranchisesController : ControllerBase
    {
        private readonly IFranchiseService _service;

        public FranchisesController(IFranchiseService service)
        {
            _service = service;
        }

        // GET: api/Franchises
        [HttpGet]
        public Task<IEnumerable<FranchiseDTO>> GetFranchises()
        {
            return _service.GetFranchisesAsync();
        }

        // GET: api/Franchises/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FranchiseDTO>> GetFranchise(int id)
        {

            var franchise = await _service.GetFranchiseByIdAsync(id);
            if (franchise == null)
            {
                return NotFound();
            }

            return franchise;
        }

        // PUT: api/Franchises/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFranchise(int id, FranchiseDTO franchiseDTO)
        {

            if (id != franchiseDTO.Id)
            {
                return BadRequest();
            }

            bool update = await _service.UpdateFranchiseAsync(id, franchiseDTO);

            if(!update)
            {
                return NotFound();
            }
            return NoContent();
        }

        // POST: api/Franchises
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public Task<bool> PostFranchise(FranchiseDTO franchiseDTO)
        {
            // consider change here

            return _service.PostFranchiseAsync(franchiseDTO);
        }

        // DELETE: api/Franchises/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFranchise(int id)
        {
            bool success = await _service.DeleteFranchise(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        // POST api/franchises/2/characters
        [HttpPost("{id}/characters")]
        public async Task<IActionResult> AddCharacterToFranchise(int id, List<int> characters)
        {
            if (characters == null)
            {
                return BadRequest();
            }
            bool charactersAdded = await _service.AddCharacterToFranchise(id, characters);
            if (charactersAdded)
            {
                return NoContent();
            }
            return NotFound();
        }

        // POST api/franchises/2/movies
        [HttpPost("{id}/movies")]
        public async Task<IActionResult> AddMovieToFranchise(int id, List<int> movies)
        {
            if (movies == null)
            {
                return BadRequest();
            }
            bool charactersAdded = await _service.AddMovieToFranchise(id, movies);
            if (charactersAdded)
            {
                return NoContent();
            }
            return NotFound();
        }

        // GET api/franchises/2/characters
        [HttpGet("{id}/characters")]
        public async Task<IEnumerable<CharacterDTO>> GetCharactersForFranchise(int id)
        {
            return await _service.GetCharactersForFranchise(id);
        }

        // GET api/franchises/2/movies
        [HttpGet("{id}/movies")]
        public async Task<IEnumerable<MovieDTO>> GetMoviesForFranchise(int id)
        {
            return await _service.GetMoviesForFranchise(id);
        }

        // PUT api/franchies/2/characters
        [HttpPut("{id}/characters")]
        public async Task<IActionResult> UpdateCharactersForFranchise(int id, List<int> characters)
        {
            bool updatedCharacters = await _service.UpdateCharactersForFranchise(id, characters);
            if (updatedCharacters)
            {
                return NoContent();
            }
            return NotFound();
        }

        // PUT api/franchies/2/characters
        [HttpPut("{id}/movies")]
        public async Task<IActionResult> UpdateMoviesForFranchise(int id, List<int> characters)
        {
            bool updatedCharacters = await _service.UpdateMoviesForFranchise(id, characters);
            if (updatedCharacters)
            {
                return NoContent();
            }
            return NotFound();
        }


    }
}
