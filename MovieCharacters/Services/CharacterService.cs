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
    public class CharacterService : ICharacterService
    {

        private readonly IMapper _mapper;

        private readonly MovieCharacterDbContext _context;

        public CharacterService(IMapper mapper, MovieCharacterDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<bool> DeleteCharacter(int id)
        {
            CharacterDTO characterDTO = new CharacterDTO { Id = id };
            Character character = _mapper.Map<Character>(characterDTO);
            var characterToDelete = await _context.Characters.FindAsync(characterDTO.Id);
            if (characterToDelete == null)
            {
                return false;
            }

            _context.Characters.Remove(characterToDelete);
            await _context.SaveChangesAsync();

            return true;
        }



        public async Task<IEnumerable<CharacterDTO>> GetCharactersAsync()
        {
            IEnumerable<Character> CharactersList = await _context.Characters.Include(c => c.Movies).ToListAsync();


            IEnumerable<CharacterDTO> charactersListDTO = _mapper.Map<IEnumerable<CharacterDTO>>(CharactersList);

            return charactersListDTO;
        }



        public async Task<bool> PostCharacterAsync(CharacterDTO characterDTO)
        {

            Character character = _mapper.Map<Character>(characterDTO);


            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateCharacterAsync(int id, CharacterDTO characterDTO)
        {
           
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
                    throw new NotImplementedException();
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async Task<ActionResult<CharacterDTO>> GetCharacterByIdAsync(int id)
        {
            var character = await _context.Characters.FindAsync(id);
            CharacterDTO characterDTO = _mapper.Map<CharacterDTO>(character);
            if (characterDTO == null)
            {
                throw new NotImplementedException();
            }

            return characterDTO;
        }
        private bool CharacterExists(int id)
        {
            return _context.Characters.Any(e => e.Id == id);
        }
    }
}
