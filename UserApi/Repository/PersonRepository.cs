using Dapper;
using Npgsql;
using UserApi.Models;

namespace UserApi.Repository;

public class PersonRepository : IPersonRepository
{
    private readonly string _connectionString;

    private readonly IConfiguration _config;

    public PersonRepository(IConfiguration config)
    {
        _config = config;
        _connectionString = _config.GetConnectionString("default");
    }
    
    private NpgsqlConnection GetConnection() => new NpgsqlConnection(_connectionString);

    public async Task<Person> CreatePersonAsync(Person person)
    {
        await using var connection = GetConnection();

        var createdId = await connection.ExecuteScalarAsync<int>
        ("INSERT INTO person (name, email, created_at, active) VALUES (@Name, @Email, @CreatedAt, @Active);select lastval();",
            person);

        person.Id = createdId;
        return person;
    }

    public async Task<Person> UpdatePersonAsync(Person person)
    {
        await using var connection = GetConnection();

        await connection.ExecuteAsync("UPDATE person SET name = @name, email = @email WHERE id = @id", person);
        return person;
    }

    public async Task DeletePersonAsync(int id)
    {
        await using var connection = GetConnection();
        await connection.ExecuteAsync("UPDATE person SET active = FALSE WHERE id = @id", new { id });
    }

    public async Task<IEnumerable<Person>> GetAllPersonActiveAsync()
    {
        await using var connection = GetConnection();
        return await connection.QueryAsync<Person>("SELECT * FROM person");
    }

    public async Task<Person> GetPersonByIdAsync(int id)
    {
        await using var connection = GetConnection();
        return await connection.QueryFirstOrDefaultAsync<Person>(
            "SELECT * FROM person WHERE id = @id", new { id });
    }

    public async Task<Person> GetPersonByEmailAndActiveAsync(string email)
    {
        await using var connection = GetConnection();
        return await connection.QueryFirstOrDefaultAsync<Person>(
            "SELECT * FROM person WHERE email = @Email  and active = TRUE", new { Email = email });
    }
}