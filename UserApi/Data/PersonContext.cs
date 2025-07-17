using Microsoft.EntityFrameworkCore;
using UserApi.Models;

namespace UserApi.Data;

public class PersonContext : DbContext
{
    public PersonContext(DbContextOptions<PersonContext> options) : base(options) { }

    public DbSet<Person> People { get; set; }
    
    public DbSet<Product> Product { get; set; }
}