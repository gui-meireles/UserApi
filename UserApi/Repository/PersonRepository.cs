using Dapper;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using UserApi.Data;
using UserApi.Models;

namespace UserApi.Repository;

public class PersonRepository : IPersonRepository
{
    private readonly PersonContext _context;
    
    private readonly string _connectionString;

    public PersonRepository(IConfiguration config, PersonContext context)
    {
        _connectionString = config.GetConnectionString("default");
        _context = context;
    }

    private NpgsqlConnection GetConnection() => new NpgsqlConnection(_connectionString);

    public async Task<int> CreatePersonAsync(Person person)
    {
        await using var connection = GetConnection();

        var createdId = await connection.ExecuteScalarAsync<int>
        ("INSERT INTO person (name, email, created_at, active) VALUES (@Name, @Email, @CreatedAt, @Active);select lastval();",
            person);

        return createdId;
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

    public async Task<IEnumerable<Person>> GetAllPeopleAsync()
    {
        await using var connection = GetConnection();
        return await connection.QueryAsync<Person>(@"
    SELECT 
        id,
        name,
        email,
        created_at AS CreatedAt,
        active
    FROM person"
        );
    }

    public async Task<Person> GetPersonByIdAsync(int id)
    {
        return await _context.People
            .AsNoTracking()  // Caso utilize o objeto retornado para atualizá-lo, remova essa linha
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Person> GetPersonByEmailAndActiveAsync(string email)
    {
        await using var connection = GetConnection();
        return await connection.QueryFirstOrDefaultAsync<Person>(
            "SELECT * FROM person WHERE email = @Email  and active = TRUE", new { Email = email });
    }
}