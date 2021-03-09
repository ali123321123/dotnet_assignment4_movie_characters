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
            CreateMap<Franchise, FranchiseMovieDTO>().ForMember(dto => dto.Id, c => c.MapFrom(c => c.Id));
            CreateMap<Franchise, FranchiseCharacterDTO>().ForMember(dto => dto.Id, c => c.MapFrom(c => c.Id));
            CreateMap<FranchiseDTO, Franchise>();
        }

    }
}
