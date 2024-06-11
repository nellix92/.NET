using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

public class Corso
{
    public string Nome { get; set; }
    public List<Studente> Studenti { get; set; }
    public int Id { get; set; }
    public Corso()
    {
        Studenti = new List<Studente>();
    }        
}