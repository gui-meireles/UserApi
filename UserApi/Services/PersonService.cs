using UserApi.Mappers;
using UserApi.Models;
using UserApi.Models.DTO;
using UserApi.Repository;

namespace UserApi.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _repository;
    
    private readonly ILogger<PersonService> _logger;

    public PersonService(IPersonRepository repository, ILogger<PersonService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<int> CreatePersonAsync(PersonCreateRequest request)
    {
        _logger.LogInformation("Iniciando criação da pessoa com email: {Email}", request.Email);
        var person = request.ToPerson();

        var existingEmail = await _repository.GetPersonByEmailAndActiveAsync(person.Email);

        if (existingEmail is not null)
            throw new InvalidOperationException("Email already in use.");

        var createdId = await _repository.CreatePersonAsync(person);
        _logger.LogInformation("Pessoa criada com ID: {Id} e Email: {Email}", createdId,  person.Email);
        return createdId;
    }

    public async Task<PersonResponse> GetPersonByIdAsync(int id)
    {
        var personPersisted = await GetActivePersonByIdAsync(id);

        return personPersisted?.ToPersonResponse();
    }

    public async Task<List<PersonResponse>> GetAllPeopleAsync()
    {
        var peoplePersisted = await _repository.GetAllPeopleAsync();
        
        return peoplePersisted
            .Where(p => p.Active)
            .Select(person => person.ToPersonResponse())
            .ToList();
    }

    public async Task<bool> UpdatePersonAsync(int id, PersonUpdateRequest request)
    {
        var personPersisted = await GetActivePersonByIdAsync(id);

        if (personPersisted is null) return false;

        var personWithTheSameEmail = await _repository.GetPersonByEmailAndActiveAsync(request.Email);

        if (personWithTheSameEmail is not null && personWithTheSameEmail.Id != id)
            throw new InvalidOperationException("Email already in use.");

        personPersisted.ToUpdate(request);
        
        await _repository.UpdatePersonAsync(personPersisted);
        return true;
    }

    public async Task<bool> DeletePersonAsync(int id)
    {
        var personPersisted = await GetActivePersonByIdAsync(id);
        if (personPersisted is null) return false;

        await _repository.DeletePersonAsync(id);
        return true;
    }

    private async Task<Person> GetActivePersonByIdAsync(int id)
    {
        var personPersisted = await _repository.GetPersonByIdAsync(id);

        if (personPersisted is null || !personPersisted.Active) return null;

        return personPersisted;
    }
}