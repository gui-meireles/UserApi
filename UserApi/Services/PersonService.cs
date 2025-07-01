using UserApi.Mappers;
using UserApi.Models;
using UserApi.Models.DTO;
using UserApi.Repository;

namespace UserApi.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _repository;

    public PersonService(IPersonRepository repository)
    {
        _repository = repository;
    }

    public async Task<PersonResponse> CreatePersonAsync(PersonCreateRequest request)
    {
        var person = request.ToPerson();

        var existingEmail = await _repository.GetPersonByEmailAndActiveAsync(person.Email);

        if (existingEmail != null)
            throw new InvalidOperationException("Email already in use.");

        var created = await _repository.CreatePersonAsync(person);
        return created.ToPersonResponse();
    }

    public async Task<PersonResponse> GetPersonByIdAsync(int id)
    {
        var existing = await GetActivePersonByIdAsync(id);

        return existing?.ToPersonResponse();
    }

    public async Task<IEnumerable<PersonResponse>> GetAllPeopleAsync()
    {
        var people = await _repository.GetAllPersonActiveAsync();
        var activePeople = people.Where(p => p.Active);
        return activePeople.Select(p => p.ToPersonResponse());
    }

    public async Task<bool> UpdatePersonAsync(int id, PersonUpdateRequest request)
    {
        var updatedPerson = request.ToPerson(id);

        var existing = await GetActivePersonByIdAsync(id);

        if (existing == null) return false;

        var existingEmail = await _repository.GetPersonByEmailAndActiveAsync(updatedPerson.Email);

        if (existingEmail != null && existingEmail.Id != updatedPerson.Id)
            throw new InvalidOperationException("Email already in use.");

        await _repository.UpdatePersonAsync(updatedPerson);
        return true;
    }

    public async Task<bool> DeletePersonAsync(int id)
    {
        var existing = await GetActivePersonByIdAsync(id);
        if (existing == null) return false;

        await _repository.DeletePersonAsync(id);
        return true;
    }

    private async Task<Person> GetActivePersonByIdAsync(int id)
    {
        var person = await _repository.GetPersonByIdAsync(id);

        if (person == null || !person.Active) return null;

        return person;
    }
}