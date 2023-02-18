using ASPWebAPI.Dtos.Character;

namespace ASPWebAPI.Services.CharacterServices
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterResponseDto>>> AddCharacter(AddCharaterRequestDto newCharacter);
        Task<ServiceResponse<GetCharacterResponseDto>> GetCharacterById(int id);
        Task<ServiceResponse<List<GetCharacterResponseDto>>> GetAllCharacter();
        Task<ServiceResponse<GetCharacterResponseDto>> UpdateCharacter(UpdateCharaterRequestDto updateCharacter);
        Task<ServiceResponse<List<GetCharacterResponseDto>>> DeleteCharacter(int id);

    }
}
