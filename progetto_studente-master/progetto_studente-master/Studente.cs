public class Studente
{
    public string Nome { get; set; }
    public string Cognome { get; set; }
    public int Id { get; set; }
    public int CorsoId { get; set; }
    public Corso Corso { get; set; }
}