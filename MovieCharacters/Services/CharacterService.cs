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
            // Find Character by id
            var characterToDelete = await _context.Characters.FindAsync(id);
            if (characterToDelete == null)
            {
                // If not found return false
                return false;
            }
            // If found remove Character
            _context.Characters.Remove(characterToDelete);
            await _context.SaveChangesAsync();

            return true;
        }



        public async Task<IEnumerable<CharacterDTO>> GetCharactersAsync()
        {
            // Get characters  and movies to every character
            IEnumerable<Character> CharactersList = await _context.Characters.Include(c => c.Movies).ToListAsync();

            // Map Characters  to CharacterDTO
            IEnumerable<CharacterDTO> charactersListDTO = _mapper.Map<IEnumerable<CharacterDTO>>(CharactersList);

            foreach(CharacterDTO character in charactersListDTO)
            {
                // Map all movies to character  to MoviesToCharacterDTO to avoid overposting
                character.Movies = _mapper.Map<List<MoviesToCharacterDTO>>(character.Movies);
            }

            return charactersListDTO;
        }



        public async Task<bool> PostCharacterAsync(CharacterDTO characterDTO)
        {
            // Map characterDTO to character
            Character character = _mapper.Map<Character>(characterDTO);
            // Clear the movies to  character because we need to map the characters with the id to to movie and not just id.
            character.Movies.Clear();
            // Checks if the characterDTO is null
            if (characterDTO.Movies != null)
            {
                // Mapping from MoviesToCharacterDTO to movies
                foreach (MoviesToCharacterDTO movie in characterDTO.Movies)
                {
                    if(movie.Id != 0)
                    {
                        // Finds the movie with the id and adds it to the character
                        Movie movies = await _context.Movies.FindAsync(movie.Id);
                          character.Movies.Add(movies);
                    }
                }
            }
            // Add character to database
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateCharacterAsync(int id, CharacterDTO characterDTO)
        {
            // Map from characterDTO to character
            Character character = _mapper.Map<Character>(characterDTO);


            // Updates the character
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

        public async Task<bool> AddMoviesToCharacter(int id, List<int> movies)
        {
            try
            {
                // Get character from CharacterTable and also gets the  character movies on characteridId
                Character character = await _context.Characters.Include(m => m.Movies).FirstOrDefaultAsync(m => m.Id == id);
                foreach (int movieId in movies)
                {
                    // check if movie exist to character
                    if (character.Movies.FirstOrDefault(m => m.Id == movieId) == null)
                    {
                        // Get Movie by id
                        Movie movie = await _context.Movies.FindAsync(movieId);
                        // Add Movie to character if found
                        if (movie != null)
                        {
                            character.Movies.Add(movie);
                        }
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

        public async Task<IEnumerable<MovieDTO>> GetMoviesForCharacter(int id)
        {
            // Find character by id and get movies  for that character
            Character character = await _context.Characters.Include(m => m.Movies).FirstOrDefaultAsync(m => m.Id == id);

            // Map movies in movie to movieDTO
            IEnumerable<MovieDTO> movieListDTO = _mapper.Map<IEnumerable<MovieDTO>>(character.Movies);

            return movieListDTO;
        }
    }
}
