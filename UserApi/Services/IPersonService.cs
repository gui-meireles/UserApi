using UserApi.Models.DTO;

namespace UserApi.Services;

public interface IPersonService
{
    Task<int> CreatePersonAsync(PersonCreateRequest request);
    Task<PersonResponse> GetPersonByIdAsync(int id);
    Task<IEnumerable<PersonResponse>> GetAllPeopleAsync();
    Task<bool> UpdatePersonAsync(int id, PersonUpdateRequest request);
    Task<bool> DeletePersonAsync(int id);
}