using Microsoft.AspNetCore.Mvc;
using MovieCharacters.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharacters.Services
{
    public interface IFranchiseService
    {
        public Task<IEnumerable<FranchiseDTO>> GetFranchisesAsync();
        public Task<ActionResult<FranchiseDTO>> GetFranchiseByIdAsync(int id);
        public Task<bool> UpdateFranchiseAsync(int id, FranchiseDTO franchiseDTO);
        public Task<FranchiseDTO> PostFranchiseAsync(FranchiseDTO franchiseDTO);
        public Task<bool> DeleteFranchise(int id);
        public Task<IEnumerable<CharacterDTO>> GetCharactersForFranchise(int id);
        public Task<IEnumerable<MovieDTO>> GetMoviesForFranchise(int id);
        public Task<bool> UpdateMoviesForFranchise(int id, List<int> movies);
        public Task<bool> UpdateCharactersForFranchise(int id, List<int> movies);
        public Task<bool> AddMovieToFranchise(int id, List<int> movies);
        public Task<bool> AddCharacterToFranchise(int id, List<int> characters);
        
        

    }
}
