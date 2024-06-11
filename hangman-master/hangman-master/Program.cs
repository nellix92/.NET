using System.Reflection.Metadata;
using System.Text.Json;
using System.IO;

Console.Write("Inserisci il tuo username: ");
string Nome = Console.ReadLine();
Utente user = Utente.LoadUtente(Nome,$"{Nome}.json");
int CountPartitaVinta = 0;
int CountPartitaPersa = 0;
int PartiteGiocate = 0;
// Utente user = new Utente(Nome, CountPartitaVinta, CountPartitaPersa);

Console.Write($"Benvenuto {Nome}");
Impiccato impiccato = new Impiccato(user);
impiccato.Start();
user.SaveUtente($"{Nome}.json");


public class Utente
{

    public string Nome { get; set; }
    public int CountPartitaVinta { get; set; }
    public int CountPartitaPersa { get; set; }
    public int PartiteGiocate { get; set; }

    public Utente(string nome, int countPartitaVinta, int countPartitaPersa)
    {
        Nome = nome;
        CountPartitaVinta = countPartitaVinta;
        CountPartitaPersa = countPartitaPersa;
    }

    public void AggiornaCountPartitaVinta()
    {
        CountPartitaVinta++;
    }

    public void AggiornaCountPartitaPersa()
    {
        CountPartitaPersa++;
    }

    public static Utente LoadUtente(string nome, string fileName)
    {
        
        string filePath = $"{fileName}.json";
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<Utente>(json);
        }
        else
        {
            return new Utente(nome, 0, 0);
        }
    }

    public void SaveUtente(string fileName)
    {

        string filePath = $"{fileName}.json";
        string json = JsonSerializer.Serialize(this);
        File.WriteAllText(filePath, json);
    }
    public void Statistiche()
    {
        Console.WriteLine($"Statistiche dell'utente {Nome}:");
        Console.WriteLine($"Partite giocate: {CountPartitaVinta+CountPartitaPersa}");
        Console.WriteLine($"Partite vinte: {CountPartitaVinta}");
        Console.WriteLine($"Partite perse: {CountPartitaPersa}");
    }
}

public class Impiccato
{

    public static string[] parole = new string[] { "casa", "albero", "bicicletta", "cane", "gatto", "sole", "mare", "montagna", "libro", "computer" };

    public static Random random = new Random();
    public Utente User { get; set; }
    public string ParolaNascosta { get; set; }
    public string ParolaTrovata { get; set; }
    public int Tentativi { get; set; }
    public List <char> lettereUsate = new List<char>();
    
    public Impiccato(Utente user)
    {
        User = user;
    }

    public void Start()
    {
        Console.WriteLine("Benvenuto al gioco dell'impiccato");
        while (true)
        {
            ParolaNascosta = parole[random.Next(parole.Length)];
            ParolaTrovata = new string('_', ParolaNascosta.Length);
            Tentativi = 6;

            PlayRound();

            Console.WriteLine("Vuoi giocare ancora? (y/n)");
            string response = Console.ReadLine();
            if (response.ToLower() != "y")
                break;
        }

        Console.WriteLine("Grazie per aver giocato!");
    }

    public void PlayRound()
    {
        Console.WriteLine($"Tentativi rimasti: {Tentativi}");
        Console.WriteLine($"Parola da trovare: {ParolaTrovata}");

        while (Tentativi > 0 && ParolaTrovata != ParolaNascosta)
        {
            char guess = Partita();
            if (ParolaNascosta.Contains(guess))
            {
                Console.WriteLine("Corretto!");
                UpdateGuessedWord(guess);
            }
            else
            {
                Console.WriteLine("Sbagliato!");
                Tentativi--;
            }

            Console.WriteLine($"Tentativi Rimasti: {Tentativi}");
            switch (Tentativi)
            {
                case 5:
                    Console.WriteLine("O");
                break;
                case 4:
                    Console.WriteLine(" O");
                    Console.WriteLine("/");
                break;
                case 3:
                    Console.WriteLine(" O");
                    Console.WriteLine("/\\");
                break;
                case 2:
                    Console.WriteLine(" O");
                    Console.WriteLine("/\\");
                    Console.WriteLine(" |");
                break;
                case 1:
                    Console.WriteLine(" O");
                    Console.WriteLine("/\\");
                    Console.WriteLine(" |");
                    Console.WriteLine("/");
                break;
                case 0:
                    Console.WriteLine(" O");
                    Console.WriteLine("/\\");
                    Console.WriteLine("|");
                    Console.WriteLine("/\\");
                break;
            }            
            Console.WriteLine($"Parola da trovare è: {ParolaTrovata}");
        }

        if (ParolaTrovata == ParolaNascosta)
        {
            Console.WriteLine("Congratulazioni! Hai indovinato la parola");
            User.AggiornaCountPartitaVinta();
        }
        else
        {
            Console.WriteLine("Game over!");
            Console.WriteLine($"La parola da trovare era: {ParolaNascosta}");
            User.AggiornaCountPartitaPersa();
        }

        User.PartiteGiocate++;
        User.Statistiche();
    }


    public char Partita()
    {
        char lettera;
        bool isValid = false;
        string inputValido;
        do
        {
            Console.Write("Inserisci una lettera: ");     
            inputValido = Console.ReadLine();      
            if (char.TryParse(inputValido, out lettera) && !char.IsNumber(lettera)&& !lettereUsate.Contains(lettera)&& inputValido!=" ")
            {
                isValid = true;
            }
            else
            {
                Console.WriteLine("ERRORE:Inserisci un singolo carattere non vuoto e non ancora inserito!!!");
            }
        } while (!isValid);
        lettereUsate.Add(lettera);
        return lettera;
    }

    private void UpdateGuessedWord(char guess)
    {
        char[] nuovaParola = ParolaTrovata.ToCharArray();
        for (int i = 0; i < ParolaNascosta.Length; i++)
        {
            if (ParolaNascosta[i] == guess)
                nuovaParola[i] = guess;
        }
        ParolaTrovata = new string(nuovaParola);
    }
}