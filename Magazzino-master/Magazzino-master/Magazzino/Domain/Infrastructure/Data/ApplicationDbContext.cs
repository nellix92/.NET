using Magazzino.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Magazzino.Domain.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    } 
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Order => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

}
