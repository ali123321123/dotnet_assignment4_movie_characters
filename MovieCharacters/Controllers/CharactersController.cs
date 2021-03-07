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

namespace MovieCharacters.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly MovieCharacterDbContext _context;

        public CharactersController(MovieCharacterDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Characters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterDTO>>> GetCharacters()
        {
            List<CharacterDTO> CharactersListDTO = new List<CharacterDTO>();
            List<Character> CharactersList = await _context.Characters.ToListAsync();

            foreach(Character character in CharactersList)
            {
                CharacterDTO characterDTO = _mapper.Map<CharacterDTO>(character);
                CharactersListDTO.Add(characterDTO);
            }

            return CharactersListDTO;
        }

        // GET: api/Characters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterDTO>> GetCharacter(int id)
        {
            var character = await _context.Characters.FindAsync(id);
            CharacterDTO characterDTO = _mapper.Map<CharacterDTO>(character);
            if (characterDTO == null)
            {
                return NotFound();
            }

            return characterDTO;
        }

        // PUT: api/Characters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCharacter(int id, CharacterDTO characterDTO)
        {
             if (id != characterDTO.Id)
                  {
                     return BadRequest();
                  }

        
                Character character = _mapper.Map<Character>(characterDTO);

                _context.Entry(character).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CharacterExists(id))
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
       

        // POST: api/Characters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Character>> PostCharacter(CharacterDTO characterDTO)
        {

            Character character = _mapper.Map<Character>(characterDTO);


            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCharacter", new { id = characterDTO.Id }, characterDTO);
        }

        // DELETE: api/Characters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            CharacterDTO characterDTO = new CharacterDTO { Id = id };
            Character character = _mapper.Map<Character>(characterDTO);
            var characterToDelete = await _context.Characters.FindAsync(characterDTO.id);
            if (characterToDelete == null)
            {
                return NotFound();
            }

            _context.Characters.Remove(characterToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CharacterExists(int id)
        {
            return _context.Characters.Any(e => e.Id == id);
        }
    }
}
