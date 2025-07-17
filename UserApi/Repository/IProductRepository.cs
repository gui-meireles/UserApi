using UserApi.Models;
using UserApi.Models.DTO;

namespace UserApi.Repository;

public interface IProductRepository
{
    Task<int> CreateProduct(Product product);
    Task<Product> UpdateProductAsync(Product product);
    Task DeleteProductAsync(int id);
    Task<Product> GetProductByIdAsync(int id);
    Task<Product> GetProductByNameAndActiveAsync(string name);
    Task<IEnumerable<Product>> GetAllProductsAsync();
}