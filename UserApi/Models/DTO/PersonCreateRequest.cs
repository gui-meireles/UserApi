using System.ComponentModel.DataAnnotations;

namespace UserApi.Models.DTO;

public class PersonCreateRequest
{
    [Required]
    [MinLength(5)]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    [MinLength(5)]
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;
}