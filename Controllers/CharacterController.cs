
using Microsoft.AspNetCore.Mvc;
using ASPWebAPI.Dtos.Character;
using Microsoft.AspNetCore.Authorization;

namespace ASPWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("GetAllCharacter"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterResponseDto>>>> GetAllCharacter()
        {
           return Ok(await _characterService.GetAllCharacter());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterResponseDto>>> GetSingle(int id)
        {
            var response = await _characterService.GetCharacterById(id);
            if (response.Data is null)
                return NotFound(response);
            return Ok(response);
        }

        [HttpPost("AddCharacter")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterResponseDto>>>> AddCharacter(AddCharaterRequestDto newCharacter)
        {
            return Ok(await _characterService.AddCharacter(newCharacter));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterResponseDto>>>> UpdateCharacter(UpdateCharaterRequestDto updateCharacter)
        {
            var response = await _characterService.UpdateCharacter(updateCharacter);
            if (response.Data is null)
                return NotFound(response);
            return Ok(response);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterResponseDto>>> DeleteCharacter(int id)
        {
            var response = await _characterService.DeleteCharacter(id);
            if (response.Data is null)
                return NotFound(response);
            return Ok(response);
        }

    }
}
