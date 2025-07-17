using UserApi.Mappers;
using UserApi.Models.DTO;
using UserApi.Repository;

namespace UserApi.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    
    private readonly ILogger<ProductService> _logger;

    public ProductService(IProductRepository repository, ILogger<ProductService> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    
    public async Task<int> CreateProduct(ProductCreateRequest request)
    {
        _logger.LogInformation("Creating a new product with name: {Name}", request.Name);
        var product = request.ToProduct();
        
        var existingProduct = await _repository.GetProductByNameAndActiveAsync(product.Name);
        
        if (existingProduct is not null)
            throw new InvalidOperationException("Product name already exists.");

        var createdProductId = await _repository.CreateProduct(product);
        _logger.LogInformation("Created a new product with id: {Id}", createdProductId);
        return createdProductId;
    }
}