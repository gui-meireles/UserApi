using UserApi.Models.DTO;

namespace UserApi.Services;

public interface IProductService
{
    Task<int> CreateProduct(ProductCreateRequest request);
}