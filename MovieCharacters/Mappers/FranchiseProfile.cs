using AutoMapper;
using MovieCharacters.DTO;
using MovieCharacters.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharacters.Mappers
{
    public class FranchiseProfile : Profile
    {
        public FranchiseProfile()
        {
            CreateMap<Franchise, FranchiseDTO>();
            CreateMap<Movie, FranchiseMovieDTO>().ForMember(dto => dto.Id, m => m.MapFrom(z => z.Id));
            CreateMap<Character, FranchiseCharacterDTO>().ForMember(dto => dto.Id, c => c.MapFrom(f => f.Id));
            CreateMap<FranchiseMovieDTO, Movie>();
            CreateMap<FranchiseCharacterDTO, Character>();
            CreateMap<FranchiseDTO, Franchise>();
        }

    }
}
