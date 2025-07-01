using UserApi.Models;

namespace UserApi.Repository;

public interface IPersonRepository
{
    Task<Person> CreatePersonAsync(Person person);
    Task<Person> UpdatePersonAsync(Person person);
    Task DeletePersonAsync(int id);
    Task<IEnumerable<Person>> GetAllPersonsAsync();
    Task<Person> GetPersonByIdAsync(int id);
}