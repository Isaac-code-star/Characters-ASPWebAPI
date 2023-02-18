using AutoMapper;
using ASPWebAPI.Dtos.Character;

namespace ASPWebAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterResponseDto>();
            CreateMap<AddCharaterRequestDto, Character>();
        }
    }
}
