using System.ComponentModel.DataAnnotations;
using UserApi.Enum;

namespace UserApi.Models.DTO;

public class ProductCreateRequest
{
    [Required]
    [MinLength(5)]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [MinLength(5)]
    [MaxLength(250)]
    public string Description { get; set; } = string.Empty;
    
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que 0.")]
    public decimal Price { get; set; }
    
    [Required(ErrorMessage = "A categoria é obrigatória.")]
    [EnumDataType(typeof(CategoryEnum), ErrorMessage = "Categoria inválida.")]
    public CategoryEnum? Category { get; set; }  //Com a '?' o Enum se torna nullable

    [Range(0, int.MaxValue, ErrorMessage = "A quantidade em estoque não pode ser negativa.")]
    public int Quantity { get; set; } = 0;
}