using Microsoft.AspNetCore.Mvc;
using MovieCharacters.DTO;
using MovieCharacters.Models.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieCharacters.Services
{
    public interface IMovieService
    {
        public Task<IEnumerable<MovieDTO>> GetMoviesAsync();
        public Task<ActionResult<MovieDTO>> GetMoviesByIdAsync(int id);
        public Task<IEnumerable<CharacterDTO>> GetCharactersForMovie(int id);
        public Task<bool> UpdateMovieAsync(int id, MovieDTO movieDTO);
        public Task<MovieDTO> PostMovieAsync(MovieDTO movieDTO);
        public Task<bool> DeleteMovie(int id);
        public Task<bool> AddCharacterToMovie(int id, List<int> characters);
        public Task<bool> UpdateCharactersForMovie(int id, List<int> characters);
    }
}
