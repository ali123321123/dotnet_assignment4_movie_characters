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
        public Task<bool> PostFranchiseAsync(FranchiseDTO franchiseDTO);
        public Task<bool> DeleteFranchise(int id);

    }
}
