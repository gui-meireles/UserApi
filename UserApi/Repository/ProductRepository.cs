using Dapper;
using Npgsql;
using UserApi.Data;
using UserApi.Models;

namespace UserApi.Repository;

public class ProductRepository : IProductRepository
{
    private readonly PersonContext _context;

    private readonly string _connectionString;

    public ProductRepository(IConfiguration config, PersonContext context)
    {
        _connectionString = config.GetConnectionString("default");
        _context = context;
    }

    private NpgsqlConnection GetConnection() => new NpgsqlConnection(_connectionString);

    public async Task<int> CreateProduct(Product product)
    {
        await using var connection = GetConnection();

        var createdProductId = await connection.ExecuteScalarAsync<int>
        ("INSERT INTO product (name, description, price, category, quantity, created_at, active) VALUES (@Name, @Description, @Price, @Category, @Quantity, @CreatedAt, @Active);select lastval();",
            product);

        return createdProductId;
    }

    public Task<Product> UpdateProductAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public Task DeleteProductAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetProductByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Product> GetProductByNameAndActiveAsync(string name)
    {
        await using var connection = GetConnection();

        return await connection.QueryFirstOrDefaultAsync<Product>(
            "SELECT * FROM product WHERE name = @Name and active = TRUE", new { Name = name }
        );
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        await using var connection = GetConnection();
        return await connection.QueryAsync<Product>("SELECT * FROM product");
    }
}