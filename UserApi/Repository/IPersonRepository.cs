using UserApi.Models;

namespace UserApi.Repository;

public interface IPersonRepository
{
    Task<int> CreatePersonAsync(Person person);
    Task<Person> UpdatePersonAsync(Person person);
    Task DeletePersonAsync(int id);
    Task<IEnumerable<Person>> GetAllPersonActiveAsync();
    Task<Person> GetPersonByIdAsync(int id);
    Task<Person> GetPersonByEmailAndActiveAsync(string email);
}