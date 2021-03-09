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
    public class CharactersController : ControllerBase
    {
        
        private readonly ICharacterService _characterService;

        public CharactersController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        // GET: api/Characters
        [HttpGet]
        public Task<IEnumerable<CharacterDTO>> GetCharacters()
        {
            return _characterService.GetCharactersAsync();
        }
        
       // GET: api/Characters/5
       [HttpGet("{id}")]
       public async Task<ActionResult<CharacterDTO>> GetCharacter(int id)
       {
            try { return await _characterService.GetCharacterByIdAsync(id); }
            catch(Exception e)
            {
                return NotFound();
            }
                
       }
        
       // PUT: api/Characters/5
       // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
       [HttpPut("{id}")]
       public async Task<IActionResult>  PutCharacter(int id, CharacterDTO characterDTO)
       {
            if (id != characterDTO.Id)
            {
                return BadRequest();
            }
            bool updated = await  _characterService.UpdateCharacterAsync(id, characterDTO);
            if (updated)
            {
                return Ok(updated);
            }
            else
            {
                return NotFound();
            }

           }


       // POST: api/Characters
       // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
       [HttpPost]
       public async Task<IActionResult> PostCharacter(CharacterDTO characterDTO)
       {

          bool posted = await _characterService.PostCharacterAsync(characterDTO);
            if (posted)
            {
                return Ok(posted);
            }
            else
            {
                return BadRequest("Not a valid character");
            }

       }

        
       // DELETE: api/Characters/5
       [HttpDelete("{id}")]
       public async Task<ActionResult> DeleteCharacter(int id)
       {
           bool deleted =  await _characterService.DeleteCharacter(id);
            if (deleted)
            {
                return Ok(deleted);
            }
            else
            {
                return BadRequest("Couldn't delete this character");
            }
         
       }
    }
}
