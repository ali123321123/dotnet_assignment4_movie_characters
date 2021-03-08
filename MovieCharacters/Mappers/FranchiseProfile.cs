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
            CreateMap<FranchiseDTO, Franchise>();
        }

    }
}
