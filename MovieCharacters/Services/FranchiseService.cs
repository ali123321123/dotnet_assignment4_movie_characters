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
    public class FranchiseService : IFranchiseService
    {

        private readonly IMapper _mapper;
        private readonly MovieCharacterDbContext _context;

        public FranchiseService(MovieCharacterDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<bool> AddCharacterToFranchise(int id, List<int> characters)
        {
            try
            {
                Franchise franchise = await _context.Franchises.Include(f => f.Characters).FirstOrDefaultAsync(c => c.Id == id);
                foreach (int characterId in characters)

                    if (franchise.Characters.FirstOrDefault(c => c.Id == characterId) == null)
                    {

                        Character character = await _context.Characters.FindAsync(characterId);

                        if (character != null)
                        {
                            franchise.Characters.Add(character);
                        }
                    }
                
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AddMovieToFranchise(int id, List<int> movies)
        {
            try
            {
                Franchise franchise = await _context.Franchises.Include(f => f.Movies).FirstOrDefaultAsync(m => m.Id == id);
                foreach (int movieId in movies)

                    if (franchise.Movies.FirstOrDefault(m => m.Id == movieId) == null)
                    {

                        Movie movie = await _context.Movies.FindAsync(movieId);

                        if (movie != null)
                        {
                            franchise.Movies.Add(movie);
                        }
                    }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
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

        public async Task<IEnumerable<CharacterDTO>> GetCharactersForFranchise(int id)
        {
            Franchise franchise = await _context.Franchises.Include(f => f.Characters).FirstOrDefaultAsync(m => m.Id == id);

            IEnumerable<CharacterDTO> characterListDTO = _mapper.Map<IEnumerable<CharacterDTO>>(franchise.Characters);

            return characterListDTO;
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

        public async Task<IEnumerable<MovieDTO>> GetMoviesForFranchise(int id)
        {
            Franchise franchise = await _context.Franchises.Include(f => f.Movies).FirstOrDefaultAsync(m => m.Id == id);


            IEnumerable<MovieDTO> movieListDTO = _mapper.Map<IEnumerable<MovieDTO>>(franchise.Movies);

            return movieListDTO;
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


        public async Task<bool> UpdateCharactersForFranchise(int id, List<int> characters)
        {
            Franchise franchise = await _context.Franchises.Include(f => f.Characters).FirstOrDefaultAsync(c => c.Id == id);
            if (characters == null)
            {
                return false;
            }
            franchise.Characters.Clear();
            foreach (int characterId in characters)
            {
                Character character = await _context.Characters.FindAsync(characterId);
                franchise.Characters.Add(character);
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateMoviesForFranchise(int id, List<int> movies)
        {
            Franchise franchise = await _context.Franchises.Include(f => f.Movies).FirstOrDefaultAsync(m => m.Id == id);
            if (movies == null)
            {
                return false;
            }
            franchise.Movies.Clear();
            foreach (int movieId in movies)
            {
                Movie movie = await _context.Movies.FindAsync(movieId);
                franchise.Movies.Add(movie);
            }
            await _context.SaveChangesAsync();
            return true;
        }

        private bool FranchiseExists(int id)
        {
            return _context.Franchises.Any(e => e.Id == id);
        }
    }
}
