using AutoMapper;
using MovieCharacters.DTO;
using MovieCharacters.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharacters.ModelMapper
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieDTO>();
            CreateMap<Character, MovieCharacterDTO>().ForMember(dto => dto.Id, c => c.MapFrom(c => c.Id));
            CreateMap<MovieDTO, Movie>();
            CreateMap<MovieCharacterDTO, Character>();
        }

    }
}
