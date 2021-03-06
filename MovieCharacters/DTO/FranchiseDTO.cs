using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharacters.DTO
{
    public class FranchiseDTO
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<FranchiseMovieDTO> Movies { get; set; }
        public List<FranchiseCharacterDTO> Characters { get; set; }
    }
}
