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
        private readonly FranchiseService _service;

        public FranchisesController(FranchiseService service)
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
        public async Task<ActionResult<FranchiseDTO>> PostFranchise(FranchiseDTO franchiseDTO)
        {
            // consider change here
            bool success = await _service.PostFranchiseAsync(franchiseDTO);

            return CreatedAtAction("GetFranchise", new { id = franchiseDTO.Id }, franchiseDTO);
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


    }
}
