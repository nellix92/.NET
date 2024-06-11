using event_manager_data;
using Microsoft.EntityFrameworkCore;
using event_manager_wasm.Models;
namespace event_manager_backend.Infrastructure.Data;

public class EventManagerDbContext : DbContext
{
    public DbSet<Evento> Eventi { get; set; }
    public EventManagerDbContext(DbContextOptions options)
        : base(options) 
    {
       
    }
}
