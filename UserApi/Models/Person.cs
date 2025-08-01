﻿using System.ComponentModel.DataAnnotations.Schema;

namespace UserApi.Models;

[Table("person")]
public class Person
{

    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("email")]
    public string Email { get; set; } = string.Empty;
    
    [Column("created_at")]
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    [Column("active")]
    public bool Active { get; init; } = true;
}