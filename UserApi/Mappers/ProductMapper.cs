using UserApi.Models;
using UserApi.Models.DTO;

namespace UserApi.Mappers;

public static class ProductMapper
{
    public static Product ToProduct(this ProductCreateRequest request)
    {
        return new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Category = request.Category!.Value, //Como o category pode ser nulo, deve adicionar o Enum!.Value
            Quantity = request.Quantity
        };
    }
}