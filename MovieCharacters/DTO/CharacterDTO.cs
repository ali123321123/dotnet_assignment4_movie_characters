using MovieCharacters.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharacters.DTO
{
    public class CharacterDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Alias { get; set; }
        public string Gender { get; set; }
        public string Picture { get; set; }

        public List<MoviesToCharacterDTO> Movies { get; set; }

    }
}
