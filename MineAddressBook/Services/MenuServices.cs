using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using MyAddressBook.Models;
using MyAddressBook.Readers;
using MyAddressBook.Services;
using Newtonsoft.Json;

namespace AddressBook.Services;

public class MenuServices
{
    private readonly IConsoleReader _reader;
    public List<Contact> Contacts = new List<Contact>();
    //public ObservableCollection<Contact> contacts;
    private readonly FileService file = new FileService();

    public MenuServices(IConsoleReader reader, bool clearData = false)
    {
        if (clearData)
            file.Save(JsonConvert.SerializeObject(Contacts));
        PopulateContactsList();
        _reader = reader;
    }

    public void PopulateContactsList()
    {
        //try
        //{
            var items = JsonConvert.DeserializeObject<List<Contact>>(file.Read()); // skapa Json-fil
            if (items != null)
            {
                Contacts = items;
            }
        //}
        //catch { 
        //}
    }

    // Välkomstmeny
    public void WelcomeMenu()
    {
        _reader.Clear();
        _reader.WriteLine(InfoType.Display, "--Välkommen till adressboken!--");
        _reader.WriteLine(InfoType.Display, "1. Skapa en kontakt.");
        _reader.WriteLine(InfoType.Display, "2. Visa alla kontakter.");
        _reader.WriteLine(InfoType.Display, "3. Visa en specifik kontakt.");
        _reader.WriteLine(InfoType.Display, "4. Ta bort en specifik kontakt.");
        _reader.WriteLine(InfoType.Display, "Välj ett av alternativen ovan:");
        int option = Convert.ToInt32(_reader.ReadLine());  //Måste jag konvertera?

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
                _reader.Clear();
                _reader.WriteLine(InfoType.Display, "Det är inget val. \n\nTryck på valfri tangent för att återgå till meny.");
                _reader.ReadKey();
                break;
        }
    }

    //Skapa en ny kontakt
    public void CreateOne()
    {
        _reader.Clear();
        Contact contact = new Contact();
        _reader.WriteLine(InfoType.Display, "Skapa en ny kontakt  \n ");

        
        _reader.Write("Förnamn: ");
        contact.FirstName = _reader.ReadLine() ?? "";
        _reader.Write("Efternamn: ");
        contact.LastName = _reader.ReadLine() ?? "";
        _reader.Write("E-post: ");
        contact.Email = _reader.ReadLine() ?? "";
        _reader.Write("Telefonnummer: ");
        contact.PhoneNumber = _reader.ReadLine() ?? "";
        _reader.Write("Adress:: ");
        contact.Address = _reader.ReadLine() ?? "";
       
        Contacts.Add(contact);
        _reader.Clear();
        _reader.WriteLine(InfoType.Information, "Kontakt skapad.");
        _reader.ReadKey();

        file.Save(JsonConvert.SerializeObject(Contacts)); // Sparar kontakten på fil
    }

    //Visa alla kontakter
    public void ShowAll()
    {
        _reader.Clear();
        _reader.WriteLine(InfoType.Display, "Kontaktlista \n");

        if (Contacts.Count == 0)
        {
            _reader.Clear();
            _reader.WriteLine(InfoType.Information, "Adressboken är tom");
        }
        else
        {
            foreach (Contact contact in Contacts)
            {
                _reader.WriteLine(InfoType.Information, $"Förnamn: {contact.FirstName}");
                _reader.WriteLine(InfoType.Information, $"Efternamn: {contact.LastName}");
                _reader.WriteLine(InfoType.Information, $"Email: {contact.Email} \n");
            }
        }
        _reader.ReadKey();
    }

    //Visa en specifik kontakt
    public void ShowOne()
    {
        if (Contacts.Count == 0)
        {
            _reader.Clear();
            _reader.WriteLine(InfoType.Information, "Adressboken är tom!  \n Tryck på valfri tangent för att återgå till meny.");
        }
        else
        {
            _reader.Clear();
            _reader.Write("Förnamn på personen som du vill hitta: ");
            var findFirstName = _reader.ReadLine()?.ToLower()?.Trim();
            _reader.Write("Efternamn på personen som du vill hitta: ");
            var findLastName = _reader.ReadLine()?.ToLower()?.Trim();


            var contactOrNull = Contacts.FirstOrDefault(x => x.FirstName.ToLower().Trim() == findFirstName && x.LastName.ToLower().Trim() == findLastName);

            if (contactOrNull != null)
            {
                _reader.Clear();
                _reader.WriteLine(InfoType.Information, $"Förnamn: {findFirstName}");
                _reader.WriteLine(InfoType.Information, $"Efternamn: {findLastName}");
                _reader.WriteLine(InfoType.Information, $"Email: {contactOrNull.Email}");
                _reader.WriteLine(InfoType.Information, $"Telefonnummer: {contactOrNull.PhoneNumber}");
                _reader.WriteLine(InfoType.Information, $"Adress: {contactOrNull.Address}");
            } else
            {

                _reader.Clear();
                _reader.WriteLine(InfoType.Information, "Det finns ingen kontakt med det namnet.");
            }

            
        }
        _reader.ReadKey();
    }

    //Ta bort en specifik kontakt
    public void DeleteOne()
    {

        if (Contacts.Count == 0)
        {
            _reader.Clear();
            _reader.WriteLine(InfoType.Information, "Adressboken är tom.");
            _reader.ReadKey();
        }
        else
        {
            _reader.Clear();
            _reader.Write("Förnamn på kontakten som du vill ta bort: ");
            var FirstName = _reader.ReadLine();

            if (FirstName == null)
            {
                _reader.Clear();
                _reader.WriteLine(InfoType.Information, "Det finns ingen kontakt med det namnet.");
                _reader.ReadKey();
            }
            else
                _reader.Write("Efternamn på kontakten som du vill ta bort: ");
            var LastName = _reader.ReadLine();
            var contact = Contacts.FirstOrDefault(x => x.FirstName.ToLower() == FirstName.ToLower() && x.LastName.ToLower() == LastName.ToLower());

            if (contact == null)
            {
                _reader.Clear();
                _reader.WriteLine(InfoType.Information, "There are no contact with the given name");
                _reader.ReadKey();
            }

            if (contact != null)
            {
                _reader.WriteLine(InfoType.Display, $"Är du säkert på att du vill ta bort kontakten? {contact.FirstName} {contact.LastName}? Tryck Y för ja och N för nej");
                var answer = _reader.ReadLine().ToLower();
                if (answer == "y")
                {
                    Contacts.Remove(contact);
                    _reader.Clear();
                    _reader.WriteLine(InfoType.Information, $"{contact.DisplayName} har tagits bort från din adressbok.");
                    _reader.ReadKey();
                }
                else if (answer == "n")
                {
                    _reader.Clear();
                    _reader.WriteLine(InfoType.Information, $"{contact.DisplayName} finns kvar i din adressbok.");
                    _reader.ReadKey();
                }
            }
        }
        file.Save(JsonConvert.SerializeObject(Contacts)); //Uppdatera information på fil
    }
    
}

