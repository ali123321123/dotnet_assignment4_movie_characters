using AutoMapper;
using Microsoft.AspNetCore.Http;
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
    public class FranchiseService : IFranchiseService
    {

        private readonly IMapper _mapper;
        private readonly MovieCharacterDbContext _context;

        public FranchiseService(MovieCharacterDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<bool> DeleteFranchise(int id)
        {
            FranchiseDTO franchiseDTO = new FranchiseDTO { Id = id };
            Franchise franchise = _mapper.Map<Franchise>(franchiseDTO);
            if (franchise == null)
            {
                return false;
            }

            _context.Franchises.Remove(franchise);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ActionResult<FranchiseDTO>> GetFranchiseByIdAsync(int id)
        {
            var franchise = await _context.Franchises.FindAsync(id);
            FranchiseDTO franchiseDTO = _mapper.Map<FranchiseDTO>(franchise);
            if (franchiseDTO == null)
            {
                return null;
            }

            return franchiseDTO;
        }

        public async Task<IEnumerable<FranchiseDTO>> GetFranchisesAsync()
        {
            IEnumerable<Franchise> FranchiseList = await _context.Franchises.Include(c => c.Movies).ToListAsync();
            IEnumerable<FranchiseDTO> FranchiseListDTO = _mapper.Map<IEnumerable<FranchiseDTO>>(FranchiseList);

            return FranchiseListDTO;
        }

        public async Task<bool> PostFranchiseAsync(FranchiseDTO franchiseDTO)
        {
            Franchise franchise = _mapper.Map<Franchise>(franchiseDTO);

            _context.Franchises.Add(franchise);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateFranchiseAsync(int id, FranchiseDTO franchiseDTO)
        {
            if (!FranchiseExists(id))
            {
                return false;
            }


            Franchise franchise = _mapper.Map<Franchise>(franchiseDTO);

            _context.Entry(franchise).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FranchiseExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }
        private bool FranchiseExists(int id)
        {
            return _context.Franchises.Any(e => e.Id == id);
        }
    }
}
