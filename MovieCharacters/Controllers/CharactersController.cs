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
       public Task<ActionResult<CharacterDTO>> GetCharacter(int id)
       {
            return _characterService.GetCharacterByIdAsync(id);
         
       }
        
       // PUT: api/Characters/5
       // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
       [HttpPut("{id}")]
       public  Task<bool> PutCharacter(int id, CharacterDTO characterDTO)
       {

            return _characterService.UpdateCharacterAsync(id, characterDTO);
          
           }

        

       // POST: api/Characters
       // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
       [HttpPost]
       public  Task<bool> PostCharacter(CharacterDTO characterDTO)
       {

            return _characterService.PostCharacterAsync(characterDTO);

       }

        
       // DELETE: api/Characters/5
       [HttpDelete("{id}")]
       public Task<bool> DeleteCharacter(int id)
       {
            return _characterService.DeleteCharacter(id);
          
       }

     

    }
}
