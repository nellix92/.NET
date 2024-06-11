namespace Test_fastendpoints.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Test_fastendpoints.Entities;

public class ApplicationDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Users;Trusted_Connection=True"); 
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options) 
    {

    }
    public DbSet<User> Users => Set<User>();
}
