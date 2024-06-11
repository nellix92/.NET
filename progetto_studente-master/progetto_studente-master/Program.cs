using Microsoft.EntityFrameworkCore;
using progetto_studente.Migrations;

var ApplicationDBContext = new ApplicationDBContext();
string Input;
do
{
    Console.WriteLine("MENU");
    Console.WriteLine("-------------------------------------------------------------");
    Console.WriteLine("Scegli quale menu vuoi visualizzare:");
    Console.WriteLine("[S]tudente");
    Console.WriteLine("[C]orso");
    Console.WriteLine("[U]scita");
    Input = Console.ReadLine().ToUpper();
    switch (Input)
    {
        case "S":
            do
            {
                Studente studente = new Studente();
                Console.WriteLine("MENU");
                Console.WriteLine("-------------------------------------------------------------");
                Console.WriteLine("[A]ggiungere studente ");
                Console.WriteLine("[M]odifica studente");
                Console.WriteLine("[C]ancella studente");
                Console.WriteLine("[E]xit");
                Console.WriteLine("Scegli cosa vuoi fare?");
                Input = Console.ReadLine().ToUpper();
                switch (Input)
                {
                    case "A":
                        Console.WriteLine("Vuoi registrarti ad un corso? [Y]es o [N]o");
                        if (Console.ReadLine().ToUpper().Contains("Y"))
                        {
                            var Corsi = ApplicationDBContext.Corsi.ToList();
                            if (Corsi.Count == 0)
                            {
                                Console.WriteLine("Non ci sono ancora corsi registrati");
                                return;
                            }
                            Console.WriteLine("Elenco Corsi:");
                            foreach (var c in Corsi)
                            {
                                Console.WriteLine($"ID: {c.Id} Nome: {c.Nome}");
                            }
                            Console.WriteLine("Scegli il tuo corso di studi tramite ID:");
                            if (!int.TryParse(Console.ReadLine(), out int CorsoId))
                            {
                                Console.WriteLine("Input non valido.");
                                return;
                            }
                            var CorsoSelezionato = Corsi.FirstOrDefault(c => c.Id == CorsoId);
                            Console.WriteLine("Inserisci il nome dello studente:");
                            studente.Nome = Console.ReadLine();
                            while (int.TryParse(studente.Nome, out _) || string.IsNullOrEmpty(studente.Nome))
                            {
                                Console.WriteLine("Errore: Inserisci un nome valido:");
                                studente.Nome = Console.ReadLine();
                            }
                            Console.WriteLine("Inserisci il cognome dello studente: ");
                            studente.Cognome = Console.ReadLine();
                            while (int.TryParse(studente.Cognome, out _) || string.IsNullOrEmpty(studente.Cognome))
                            {
                                Console.WriteLine("Errore: Inserisci un cognome valido:");
                                studente.Cognome = Console.ReadLine();
                            }
                            CorsoSelezionato.Studenti.Add(studente);
                            ApplicationDBContext.SaveChanges();
                            Console.WriteLine("Studente aggiunto al corso con successo.");
                        }
                        break;
                    case "M":
                        var corsi = ApplicationDBContext.Corsi.Include(c => c.Studenti).ToList();
                        var corsiConStudenti = corsi.Where(c => c.Studenti.Any()).ToList();
                        if (corsiConStudenti.Count == 0)
                        {
                            Console.WriteLine("Non ci sono corsi con studenti registrati.");
                            return;
                        }
                        Console.WriteLine("Corsi disponibili:");
                        foreach (var corso in corsiConStudenti)
                        {
                            Console.WriteLine($"[{corso.Id}] {corso.Nome}");
                        }
                        Console.WriteLine("Scegli il corso in cui modificare lo studente:");
                        if (!int.TryParse(Console.ReadLine(), out int corsoId))
                        {
                            Console.WriteLine("Input non valido.");
                            return;
                        }
                        var corsoSelezionato = corsiConStudenti.FirstOrDefault(c => c.Id == corsoId);
                        if (corsoSelezionato == null)
                        {
                            Console.WriteLine("Corso non trovato.");
                            return;
                        }
                        var studentiNelCorso = corsoSelezionato.Studenti.ToList();
                        if (studentiNelCorso.Count == 0)
                        {
                            Console.WriteLine("Non ci sono studenti registrati per questo corso.");
                            return;
                        }
                        Console.WriteLine("Lista studenti:");
                        foreach (var s in studentiNelCorso)
                        {
                            Console.WriteLine($"[{s.Id}] {s.Nome} {s.Cognome}");
                        }
                        Console.WriteLine("Scegli l'ID dello studente da eliminare:");
                        if (!int.TryParse(Console.ReadLine(), out int studenteId))
                        {
                            Console.WriteLine("Input non valido.");
                            return;
                        }

                        var studenteSelezionato = studentiNelCorso.FirstOrDefault(s => s.Id == studenteId);
                        if (studenteSelezionato == null)
                        {
                            Console.WriteLine("Studente non trovato.");
                            return;
                        }

                        Console.WriteLine("Vuoi modificare il corso di appartenenza? [Y]es o [N]o");
                        if (Console.ReadLine().ToUpper().Contains("Y"))
                        {
                            Console.WriteLine("Lista corsi:");
                            foreach (var corso in corsi)
                            {
                                Console.WriteLine($"[{corso.Id}] {corso.Nome}");
                            }
                            Console.WriteLine("Scegli l'ID del corso:");


                            if (!int.TryParse(Console.ReadLine(), out int nuovoCorsoId))
                            {
                                Console.WriteLine("Input non valido.");
                                return;
                            }

                            var nuovoCorso = corsi.FirstOrDefault(c => c.Id == nuovoCorsoId);
                            if (nuovoCorso == null)
                            {
                                Console.WriteLine("Corso non trovato.");
                                return;
                            }

                            corsoSelezionato.Studenti.Remove(studenteSelezionato);

                            nuovoCorso.Studenti.Add(studenteSelezionato);
                        }

                        Console.WriteLine("Vuoi modificare il tuo nome e cognome? [Y]es / [N]o");
                        if (Console.ReadLine().ToUpper().Contains("Y"))
                        {
                            Console.WriteLine("Inserisci il nuovo nome dello studente:");
                            studenteSelezionato.Nome = Console.ReadLine();
                            while (String.IsNullOrWhiteSpace(studenteSelezionato.Nome))
                            {

                                Console.WriteLine("Non hai inserito il nome:");
                                studenteSelezionato.Nome = Console.ReadLine();
                            }
                            Console.WriteLine("Inserisci il nuovo cognome dello studente:");
                            studenteSelezionato.Cognome = Console.ReadLine();
                            while (String.IsNullOrWhiteSpace(studenteSelezionato.Cognome))
                            {
                                Console.WriteLine("Non hai inserito il cognome:");
                                studenteSelezionato.Cognome = Console.ReadLine();
                            }
                            ApplicationDBContext.SaveChanges();
                            Console.WriteLine("Studente modificato con successo.");
                        }
                        break;
                    case "C":
                        corsi = ApplicationDBContext.Corsi.Include(c => c.Studenti).ToList();
                        corsiConStudenti = corsi.Where(c => c.Studenti.Any()).ToList();
                        if (corsiConStudenti.Count == 0)
                        {
                            Console.WriteLine("Non ci sono corsi con studenti registrati.");
                            return;
                        }
                        Console.WriteLine("Corsi disponibili:");
                        foreach (var corso in corsiConStudenti)
                        {
                            Console.WriteLine($"[{corso.Id}] {corso.Nome}");
                        }
                        Console.WriteLine("Scegli il corso da cui cancellare lo studente:");
                        if (!int.TryParse(Console.ReadLine(), out int corsoIdCancella))
                        {
                            Console.WriteLine("Input non valido.");
                            return;
                        }

                        corsoSelezionato = corsiConStudenti.FirstOrDefault(c => c.Id == corsoIdCancella);
                        if (corsoSelezionato == null)
                        {
                            Console.WriteLine("Corso non trovato.");
                            return;
                        }

                        studentiNelCorso = corsoSelezionato.Studenti.ToList();
                        if (studentiNelCorso.Count == 0)
                        {
                            Console.WriteLine("Non ci sono studenti registrati per questo corso.");
                            return;
                        }

                        Console.WriteLine("Lista studenti:");
                        foreach (var s in studentiNelCorso)
                        {
                            Console.WriteLine($"[{s.Id}] {s.Nome} {s.Cognome}");
                        }
                        Console.WriteLine("Scegli lo studente da eliminare:");
                        if (!int.TryParse(Console.ReadLine(), out studenteId))
                        {
                            Console.WriteLine("Input non valido.");
                            return;
                        }

                        var studenteDaCancellare = studentiNelCorso.FirstOrDefault(s => s.Id == studenteId);
                        if (studenteDaCancellare == null)
                        {
                            Console.WriteLine("Studente non trovato.");
                            return;
                        }

                        corsoSelezionato.Studenti.Remove(studenteDaCancellare);

                        ApplicationDBContext.SaveChanges();
                        Console.WriteLine("Studente cancellato con successo.");
                        break;
                    case "E":
                        Console.WriteLine("uscita");
                        break;
                    default:
                        Console.WriteLine("Scegli una opzione valida");
                        break;
                }
                break;
            } while (Input != "E");
            break;
        case "C":
            do
            {
                Corso Corso = new Corso();
                Console.WriteLine("MENU");
                Console.WriteLine("-------------------------------------------------------------");
                Console.WriteLine("[A]ggiungere un nuovo corso");
                Console.WriteLine("[V]isualizza un corso");
                Console.WriteLine("[M]odifica un corso");
                Console.WriteLine("[D]elete un corso");
                Console.WriteLine("[E]xit");
                Console.WriteLine("Scegli cosa vuoi fare?");
                Input = Console.ReadLine().ToUpper();
                switch (Input)
                {
                    case "A":
                        Console.WriteLine("Inserisci il nome del corso");
                        Corso.Nome = Console.ReadLine();
                        while (String.IsNullOrWhiteSpace(Corso.Nome) || int.TryParse(Corso.Nome, out _))
                        {
                            Console.WriteLine("Non hai inserito il nome, inserisci il nome del corso:");
                            Corso.Nome = Console.ReadLine();
                        }
                        ApplicationDBContext.Add(Corso);
                        ApplicationDBContext.SaveChanges();
                        Console.WriteLine($"Corso aggiunto con successo: {Corso.Nome}");
                        break;
                    case "V":
                        var corsiVisualizzazione = ApplicationDBContext.Corsi.Include(c => c.Studenti).ToList();
                        if (corsiVisualizzazione.Count == 0)
                        {
                            Console.WriteLine("Non ci sono corsi registrati.");
                            break;
                        }
                        Console.WriteLine("Elenco corsi disponibili:");
                        foreach (var c in corsiVisualizzazione)
                        {
                            Console.WriteLine($"ID: {c.Id} Nome: {c.Nome}");
                        }
                        Console.WriteLine("Inserisci l'id del corso da visualizzare:");
                        if (!int.TryParse(Console.ReadLine(), out int IdCorso))
                        {
                            Console.WriteLine("Input non valido.");
                            break;
                        }
                        var corsoSelezionato = corsiVisualizzazione.FirstOrDefault(c => c.Id == IdCorso);
                        if (corsoSelezionato == null)
                        {
                            Console.WriteLine("Corso non trovato.");
                            break;
                        }
                        var studentiCorso = corsoSelezionato.Studenti.ToList();
                        if (studentiCorso.Count == 0)
                        {
                            Console.WriteLine("Non ci sono studenti registrati per questo corso.");
                            break;
                        }
                        Console.WriteLine($"Elenco degli studenti registrati al corso {corsoSelezionato.Nome}:");
                        foreach (var studente in studentiCorso)
                        {
                            Console.WriteLine($"[{studente.Id}] {studente.Nome} {studente.Cognome}");
                        }
                        Console.WriteLine($"ID: {corsoSelezionato.Id} Nome: {corsoSelezionato.Nome}");
                        break;
                    case "M":

                        var corsiModifica = ApplicationDBContext.Corsi.ToList();

                        if (corsiModifica.Count() == 0)
                        {
                            Console.WriteLine("Non ci sono ancora corsi registrati");
                        }
                        Console.WriteLine("Corsi disponibili:");
                        foreach (var c in corsiModifica)
                        {
                            Console.WriteLine($"ID: {c.Id} Nome: {c.Nome}");
                        }
                        Console.WriteLine("Inserisci l'id del corso da modificare:");
                        if (!int.TryParse(Console.ReadLine(), out int parsedId))
                        {
                            Console.WriteLine("Input non valido.");
                            break;
                        }
                        var corsoSelezionatoModifica = corsiModifica.FirstOrDefault(c => c.Id == parsedId);
                        if (corsoSelezionatoModifica == null)
                        {
                            Console.WriteLine("Corso non trovato.");
                            break;
                        }
                        Console.WriteLine($"Il nome del corso da modificare è: {corsoSelezionatoModifica.Nome}");
                        Console.WriteLine("Inserisci il nuovo nome per il corso:");
                        string nuovoNomeCorso = Console.ReadLine();
                        while (string.IsNullOrWhiteSpace(nuovoNomeCorso) || int.TryParse(nuovoNomeCorso, out _))
                        {
                            Console.WriteLine("Non hai inserito il nome, inserisci il nuovo nome:");
                            nuovoNomeCorso = Console.ReadLine();
                        }
                        corsoSelezionatoModifica.Nome = nuovoNomeCorso;
                        ApplicationDBContext.SaveChanges();
                        Console.WriteLine("Nome del corso modificato con successo.");
                        break;
                    case "D":
                        List<Corso> AllCourse = ApplicationDBContext.Corsi.ToList();
                        foreach (var c in AllCourse)
                        {
                            Console.WriteLine($"ID: {c.Id} Nome: {c.Nome}");
                        }
                        Console.WriteLine("Inserisci l'id del corso da eliminare:");
                        bool idValido = false;
                        while (!idValido)
                        {
                            if (int.TryParse(Console.ReadLine(), out parsedId))
                            {
                                IdCorso = parsedId;
                                if (AllCourse.Any(c => c.Id == IdCorso))
                                {
                                    Corso? corso = ApplicationDBContext.Corsi.Find(IdCorso);
                                    ApplicationDBContext.Corsi.Remove(corso);
                                    Console.WriteLine("Corso eliminato con successo.");
                                    ApplicationDBContext.SaveChanges();
                                    idValido = true;
                                }
                                else
                                {
                                    Console.Write("ID non valido. Inserisci di nuovo: ");
                                }
                            }
                            else
                            {
                                Console.Write("ID non valido. Inserisci di nuovo: ");
                            }
                        }
                        break;
                    case "E":
                        Console.WriteLine("Uscita");
                        break;
                }
            } while (Input != "E");
            break;
    }
} while (Input != "U");