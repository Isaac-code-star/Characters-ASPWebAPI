using ASPWebAPI.Dtos.Character;
using AutoMapper;
using ASPWebAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ASPWebAPI.Services.CharacterServices
{
    public class CharacterService : ICharacterService
    {

        private static List<Character> characters = new List<Character>
        {
            new Character(),
            new Character{Id = 1, Name = "Isaac"},
            

        };

        //Dependency injection for Automapper
        private readonly IMapper _mapper;
        //Dependency injection for database sqlServer
        private readonly DataContext _dataContext;

        //contrustor of injections
        public CharacterService(IMapper mapper, DataContext dataContext)
        {
            _mapper = mapper;
            _dataContext = dataContext;
        }

        //Add charaters method
        public async Task<ServiceResponse<List<GetCharacterResponseDto>>> AddCharacter(AddCharaterRequestDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResponseDto>>();   
            var charater = _mapper.Map<Character>(newCharacter);
            //charater.Id = _dataContext.Characters.Max(c => c.Id) + 1;
            _dataContext.Characters.Add(charater);
            await _dataContext.SaveChangesAsync();

            serviceResponse.Data = _dataContext.Characters.Select(c => _mapper.Map<GetCharacterResponseDto>(c)).ToList();

            return serviceResponse;
        }


        public async Task<ServiceResponse<List<GetCharacterResponseDto>>> GetAllCharacter()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResponseDto>>();
            var dbCharacter = await _dataContext.Characters.ToListAsync();

            try
            {
                var character = dbCharacter.Select(character => _mapper.Map<GetCharacterResponseDto>(character)).ToList();

                if (character is null)
                    throw new Exception($"No characters available");
                serviceResponse.Data = character;
                serviceResponse.Success = true;
                serviceResponse.Message = "Successfull";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterResponseDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterResponseDto>();
            try
            {
                var dbCharater = await _dataContext.Characters.FirstOrDefaultAsync(c => c.Id == id);
                if (dbCharater is null)
                    throw new Exception($"ID {id} not found"); 

                serviceResponse.Data = _mapper.Map<GetCharacterResponseDto>(dbCharater);
                serviceResponse.Success = true;
                serviceResponse.Message = $"ID {id} found";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterResponseDto>> UpdateCharacter(UpdateCharaterRequestDto updateCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterResponseDto>();

            try
            {
                var dbCharacter = await _dataContext.Characters.FirstOrDefaultAsync(c => c.Id == updateCharacter.Id);
                if (dbCharacter is null)
                {
                    throw new Exception($"Character with Id '{updateCharacter.Id}' not found");
                }
                dbCharacter.Name = updateCharacter.Name;
                dbCharacter.HitPoints = updateCharacter.HitPoints;
                dbCharacter.Strength = updateCharacter.Strength;
                dbCharacter.Defense = updateCharacter.Defense;
                dbCharacter.Intelligence = updateCharacter.Intelligence;
                dbCharacter.Class = updateCharacter.Class;
                _dataContext.Characters.Update(dbCharacter);
                await _dataContext.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetCharacterResponseDto>(dbCharacter);
                serviceResponse.Success = true;
                serviceResponse.Message = $"Characters with Id {updateCharacter.Id} updated successfully";
            }
            catch (Exception ex) {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterResponseDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResponseDto>>();

            try
            {
                var character = await _dataContext.Characters.FirstOrDefaultAsync(c => c.Id == id);
                if (character is null)
                {
                    throw new Exception($"Character with Id '{id}' not found");
                }

                _dataContext.Characters.Remove(character);
                await _dataContext.SaveChangesAsync();

                serviceResponse.Data = _dataContext.Characters.Select(c => _mapper.Map<GetCharacterResponseDto>(c)).ToList();
                serviceResponse.Success = true;
                serviceResponse.Message = $"Character with {id} removed successfully";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}
