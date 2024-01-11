using static System.Console;

namespace BibliotekTimFalk
{
  public class Program
  {
    // VARIABLER
    public static string menyVald;
    public static bool fortsättProgrammet = true;

    public static void Main()
    {
      BackgroundColor = ConsoleColor.Black;
      ForegroundColor = ConsoleColor.DarkGreen;

      WriteLine("[-] Välkommen till Biblioteket!");
      WriteLine("[-]");

      MenyVal();
    }

    public static void MenyVal()
    {
      do
      { //Alternativ för programmet
        WriteLine("[-] Välj något av alternativen nedan:");
        WriteLine("[-]");
        WriteLine("[-] 1. Lägg till ny bok i sortimentet.");
        WriteLine("[-] 2. Lägg till en ny lånetagare och deras lånade bok.");
        WriteLine("[-] 3. Lämna tillbaka en lånad bok.");
        WriteLine("[-] 4. Visa tillgängliga böcker.");
        WriteLine("[-] 5. Visa låntagare samt deras bok/böcker.");
        WriteLine("[-] 6. Avsluta programmet.");
        WriteLine("[-]");
        Write("[-] ");

        menyVald = ReadLine();
        Clear();

        VilketVal();
      } while (fortsättProgrammet);
    }

    public static void ValEtt() 
    { // Lägg till en ny bok i sortimentet
      Bok.LäggTillBok();
      Bibliotek.bokInformation.Add(Bok.helaBoken);
    }

    public static void ValTvå() 
    { // Lägg till en låntagare och låna en bok
      if (Bibliotek.bokInformation.Count > 0)
      {
        Låntagare.LäggTillLåntagare();
        Bibliotek.låntagareInformation.Add(Låntagare.helaInformationen);
      }
      else
      {
        WriteLine("[-] Det finns inga tillgängliga böcker inlagda i systemet, Lägg till en ny bok först.");
        WriteLine("[-]");
        Thread.Sleep(600);
      }
    }

    public static void ValTre()
    { // Lämna tillbaka en lånad bok 
      if (Bibliotek.låntagareInformation.Count > 0)
      {
        Låntagare.TaBortLånTagare();
      }
      else
      {
        WriteLine("[-] Det finns inga låntagare inlagda i systemet, Lägg till en ny låntagare först.");
        WriteLine("[-]");
        Thread.Sleep(600);
      }
    }

    public static void ValFyra()
    { // Visa tillgängliga böcker
      if (Bibliotek.bokInformation.Count > 0)
      {
        WriteLine("[-] Dessa böcker finns tillgängliga:");
        WriteLine("[-]");

        for (int currentIndex = 0; currentIndex < Bibliotek.bokInformation.Count; currentIndex++)
        {
          WriteLine($"[-] Bok {currentIndex + 1}: {Bibliotek.bokInformation[currentIndex]}");
        }

        WriteLine("[-]");
      }
      else
      {
        WriteLine("[-] Det finns inga tillgängliga böcker inlagda i systemet, Lägg till en ny bok först.");
        WriteLine("[-]");
        Thread.Sleep(600);
      }
    }

    public static void ValFem()
    { // visa lånetagare
      if (Bibliotek.låntagareInformation.Count > 0)
      {
        WriteLine("[-] Dessa låntagare finns inlagda i systemet:");
        WriteLine("[-]");

        for (int currentIndex = 0; currentIndex < Bibliotek.låntagareInformation.Count; currentIndex++)
        {
          WriteLine($"[-] Låntagare {currentIndex + 1}: {Bibliotek.låntagareInformation[currentIndex]}");
        }

        WriteLine("[-]");
      }
      else
      {
        WriteLine("[-] Det finns inga låntagare inlagda i systemet, Lägg till en ny låntagare först.");
        WriteLine("[-]");
        Thread.Sleep(600);
      }
    }

    public static void ValSex()
    { // avsluta programmet
      WriteLine("[-] Avslutar programmet...");
      Thread.Sleep(1200);
      fortsättProgrammet = false;
    }

    public static void VilketVal()
    {
      switch (menyVald)
      {
        case "1": // Lägg till en ny bok
          ValEtt();
          break;
        case "2": // Lägg till en ny låntagare och låna en bok
          ValTvå();
          break;
        case "3": // Lämna tillbaka en lånad bok (ta bort låntagare)
          ValTre();
          break;
        case "4": // Visa tillgängliga böcker
          ValFyra();
          break;
        case "5": // Visa Låntagare
          ValFem();
          break;
        case "6": // Avsluta programmet
          ValSex();
          break;
        default:
          WriteLine("[-] Felaktig inmatning, försök igen.");
          break;
      }
    }
  }

  public class Bok
  {
    public static string titel;
    public static string författare;
    public static string helaBoken;

    public static void LäggTillBok() // Funktion för att lägga till en bok i systemet
    {
      WriteLine("[-] Ange titeln av boken:");
      WriteLine("[-]");
      Write("[-] ");
      titel = ReadLine();

      WriteLine("[-]");
      WriteLine("[-] Ange författaren av boken:");
      WriteLine("[-]");
      Write("[-] ");
      författare = ReadLine();

      Clear();
      WriteLine("[-] Lägger till boken...");
      Thread.Sleep(1000);
      Clear();

      helaBoken = $"Titel: {titel}, Författare: {författare}.";
    }
  }

  public class Bibliotek
  { // Biblioteks klass för att lagra böcker och låntagare
    public static List<string> bokInformation = new List<string>();
    public static List<string> låntagareInformation = new List<string>();
  }
  
  public class Låntagare
  {
    public static string namn;
    public static int personnummer;
    public static string lånaBok;
    public static string taBortLåntagare;
    public static string låntagareInformation;
    public static int lånadBokIndex;
    public static int taBortLåntagareIndex;
    public static string helaInformationen;
    public static List<string> lånadeBöcker = new List<string>();

    public static void LäggTillLåntagare() // Lägger till en låntagare
    {
      WriteLine("[-] Ange förnamn och efternamn av låntagaren:");
      WriteLine("[-]");
      Write("[-] ");
      namn = ReadLine();

      WriteLine("[-]");
      WriteLine("[-] Ange låntagarens personnummer (Inga bindesstreck eller mellanslag):");
      WriteLine("[-]");
      Write("[-] ");
      personnummer = int.Parse(ReadLine());
      Clear();

      LäggTillLånadBok();
    }

    public static void LäggTillLånadBok() // Lägger till en bok att låna till låntagaren
    { // Låna en bok
      WriteLine("[-] Vilken av dessa böcker ska låntagaren låna?");
      WriteLine("[-]");

      for (int currentIndex = 0; currentIndex < Bibliotek.bokInformation.Count; currentIndex++)
      {
        WriteLine($"[-] Bok {currentIndex + 1}: {Bibliotek.bokInformation[currentIndex]}");
      }

      WriteLine("[-]");
      Write("[-] ");

      lånaBok = ReadLine();
      lånadBokIndex = int.Parse(lånaBok);

      Clear();
      WriteLine("[-] Lägger till låntagaren...");
      Thread.Sleep(1000);
      Clear();

      helaInformationen = $"Namn: {namn}, Personnummer: {personnummer}, Bok: {Bibliotek.bokInformation[lånadBokIndex - 1]}";

      lånadeBöcker.Add(Bibliotek.bokInformation[lånadBokIndex - 1]);
      Bibliotek.bokInformation.RemoveAt(lånadBokIndex - 1);
    }

    public static void TaBortLånTagare() // Tar bort en låntagare
    {
      WriteLine("[-] Dessa låntagare finns inlagda i systemet:");
      WriteLine("[-]");

      for (int currentIndex = 0; currentIndex < Bibliotek.låntagareInformation.Count; currentIndex++)
      {
        WriteLine($"[-] Låntagare {currentIndex + 1}: {Bibliotek.låntagareInformation[currentIndex]}");
      }

      WriteLine("[-]");
      WriteLine("[-] Vilken låntagare vill du ta bort från systemet?");
      WriteLine("[-]");
      Write("[-] ");

      taBortLåntagare = ReadLine();
      taBortLåntagareIndex = int.Parse(taBortLåntagare);

      Clear();
      WriteLine("[-] Tar bort låntagaren...");
      Thread.Sleep(1000);
      Clear();

      Bibliotek.bokInformation.Add(lånadeBöcker[taBortLåntagareIndex - 1]);
      Bibliotek.låntagareInformation.RemoveAt(lånadBokIndex - 1);
      lånadeBöcker.RemoveAt(lånadBokIndex - 1);
    }
  }
}