using System.ComponentModel.DataAnnotations.Schema;
using UserApi.Enum;

namespace UserApi.Models;

[Table("product")]
public class Product
{
    [Column("id")]
    public int Id { get; set; }
    
    [Column("name")]
    public string Name { get; set; }
    
    [Column("description")]
    public string Description { get; set; }
    
    [Column("category")]
    public CategoryEnum Category { get; set; }
    
    [Column("price")]
    public decimal Price { get; set; }
    
    [Column("quantity")]
    public int Quantity { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("active")]
    public bool Active { get; set; } = true;
}