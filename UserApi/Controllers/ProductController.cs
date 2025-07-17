using Microsoft.AspNetCore.Mvc;
using UserApi.Models.DTO;
using UserApi.Services;

namespace UserApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductCreateRequest request)
    {
        try
        {
            var productIdCreated = await _productService.CreateProduct(request);
            return Created(string.Empty, new { Id = productIdCreated });
        }
        catch (InvalidOperationException ex)
        {
            return StatusCode(422, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}