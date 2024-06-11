using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

public class ApplicationDBContext : DbContext 
{
    public ApplicationDBContext()
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ApplicationDB;Trusted_Connection=True");
        
    }
    public DbSet <Studente> Studenti => Set <Studente> ();
    public DbSet <Corso> Corsi => Set <Corso> ();
}