using AutoMapper;
using MovieCharacters.DTO;
using MovieCharacters.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharacters
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            CreateMap<Character, CharacterDTO>();
            CreateMap<CharacterDTO, Character>();
            CreateMap<Movie, MoviesToCharacterDTO>().ForMember(dto => dto.Id, c => c.MapFrom(c => c.Id));
            CreateMap<MoviesToCharacterDTO, Movie>();
        }

    }
}
