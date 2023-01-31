using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using MyAddressBook.Models;
using MyAddressBook.Services;
using Newtonsoft.Json;

namespace AddressBook.Services;

public class MenuServices
{
    private List<Contact> Contacts = new List<Contact>();
    private ObservableCollection<Contact> contacts;
    private readonly FileService file = new FileService();

    public MenuServices()
    {
        PopulateContactsList();
    }

    public void PopulateContactsList()
    {
        try
        {
            var items = JsonConvert.DeserializeObject<ObservableCollection<Contact>>(file.Read()); // skapa Json-fil
            if (items != null)
            {
                contacts = items;
            }
        }
        catch { }
    }

    // Välkomstmeny
    public void WelcomeMenu()
    {
        Console.Clear();
        Console.WriteLine("--Välkommen till adressboken!--");
        Console.WriteLine("1. Skapa en kontakt.");
        Console.WriteLine("2. Visa alla kontakter.");
        Console.WriteLine("3. Visa en specifik kontakt.");
        Console.WriteLine("4. Ta bort en specifik kontakt.");
        Console.WriteLine("Välj ett av alternativen ovan:");
        int option = Convert.ToInt32(Console.ReadLine());

        switch (option)
        {
            case 1:
                CreateOne();
                break;
            case 2:
                ShowAll();
                break;
            case 3:
                ShowOne();
                break;
            case 4:
                DeleteOne();
                break;
            default:
                Console.Clear();
                Console.WriteLine("Det är inget val. \n\nTryck på valfri tangent för att återgå till meny.");
                Console.ReadKey();
                break;
        }
    }

    //Skapa en kontakt
    private void CreateOne()
    {
        Console.Clear();
        Contact contact = new Contact();
        Console.WriteLine("Skapa en ny kontakt  \n ");

        
        Console.Write("Förnamn: ");
        contact.FirstName = Console.ReadLine() ?? "";
        Console.Write("Efternamn: ");
        contact.LastName = Console.ReadLine() ?? "";
        Console.Write("E-post: ");
        contact.Email = Console.ReadLine() ?? "";
        Console.Write("Telefonnummer: ");
        contact.PhoneNumber = Console.ReadLine() ?? "";
        Console.Write("Adress:: ");
        contact.Address = Console.ReadLine() ?? "";
       
        Contacts.Add(contact);
        Console.Clear();
        Console.WriteLine("Kontakt skapad.");
        Console.ReadKey();

        file.Save(JsonConvert.SerializeObject(Contacts)); // Sparar kontakten på fil
    }

    //Visa alla kontakter
    private void ShowAll()
    {
        Console.Clear();
        Console.WriteLine("Kontaktlista \n");

        if (Contacts.Count == 0)
        {
            Console.Clear();
            Console.WriteLine("Adressboken är tom");
        }
        else
        {
            foreach (Contact contact in Contacts)
            {
                Console.WriteLine($"Förnamn: {contact.FirstName}");
                Console.WriteLine($"Efternamn: {contact.LastName}");
                Console.WriteLine($"Email: {contact.Email} \n");
            }
        }
        Console.ReadKey();
    }

    //Visa en specifik kontakt
    private void ShowOne()
    {
        if (contacts.Count == 0)
        {
            Console.Clear();
            Console.WriteLine("Adressboken är tom!  \n Tryck på valfri tangent för att återgå till meny.");
        }
        else
        {
            Console.Clear();
            Console.Write("Förnamn på personen som du vill hitta: ");
            var findFirstName = Console.ReadLine().ToLower();
            Console.Write("Efternamn på personen som du vill hitta: ");
            var findLastName = Console.ReadLine().ToLower();
            foreach (Contact contact in Contacts)
            {
                var FullName = Contacts.FirstOrDefault(x => x.FirstName == findFirstName && x.LastName == findLastName);

                if (FullName != null)
                {
                    Console.Clear();
                    Console.WriteLine($"Förnamn: {findFirstName}");
                    Console.WriteLine($"Efternamn: {findLastName}");
                    Console.WriteLine($"Email: {contact.Email}");
                    Console.WriteLine($"Telefonnummer: {contact.PhoneNumber}");
                    Console.WriteLine($"Adress: {contact.Address}");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Det finns ingen kontakt med det namnet.");
                }
            }
        }
        Console.ReadKey();
    }

    //Ta bort en specifik kontakt
    private void DeleteOne()
    {

        if (Contacts.Count == 0)
        {
            Console.Clear();
            Console.WriteLine("Adressboken är tom.");
            Console.ReadKey();
        }
        else
        {
            Console.Clear();
            Console.Write("Förnamn på kontakten som du vill ta bort: ");
            var FirstName = Console.ReadLine();

            if (FirstName == null)
            {
                Console.Clear();
                Console.WriteLine("Det finns ingen kontakt med det namnet.");
                Console.ReadKey();
            }
            else
                Console.Write("Efternamn på kontakten som du vill ta bort: ");
            var LastName = Console.ReadLine();
            var contact = Contacts.FirstOrDefault(x => x.FirstName.ToLower() == FirstName.ToLower() && x.LastName.ToLower() == LastName.ToLower());

            if (contact == null)
            {
                Console.Clear();
                Console.WriteLine("There are no contact with the given name");
                Console.ReadKey();
            }

            if (contact != null)
            {
                Console.WriteLine($"Är du säkert på att du vill ta bort kontakten? {contact.FirstName} {contact.LastName}? Press Y for yes and N for no");
                var answer = Console.ReadLine().ToLower();
                if (answer == "y")
                {
                    Contacts.Remove(contact);
                    Console.Clear();
                    Console.WriteLine($"{contact.DisplayName} har tagits bort från din adressbok.");
                    Console.ReadKey();
                }
                else if (answer == "n")
                {
                    Console.Clear();
                    Console.WriteLine($"{contact.DisplayName} finns kvar i din adressbok.");
                    Console.ReadKey();
                }
            }
        }
        file.Save(JsonConvert.SerializeObject(Contacts)); //Uppdatera information på fil
    }
    
}

