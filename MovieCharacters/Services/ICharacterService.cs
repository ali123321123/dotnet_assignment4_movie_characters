using Microsoft.AspNetCore.Mvc;
using MovieCharacters.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharacters.Services
{
    public interface ICharacterService
    {
        public Task <IEnumerable<CharacterDTO>> GetCharactersAsync();
        public Task <ActionResult<CharacterDTO>> GetCharacterByIdAsync(int id);
        public  Task<bool> UpdateCharacterAsync(int id, CharacterDTO characterDTO);
        public Task<bool> PostCharacterAsync(CharacterDTO characterDTO);
        public Task<bool> DeleteCharacter(int id);

    }
}
