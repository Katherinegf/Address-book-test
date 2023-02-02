using MyAddressBook.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAddressBook.Services;

public class AddressBookService
{
    private List<Contact> _contacts;
    public ReadOnlyCollection<Contact> Contacts => new ReadOnlyCollection<Contact>(_contacts);

    public AddressBookService(List<Contact> contacts)
    {
        _contacts = contacts;
    }

    public void AddContact(Contact contact)
    {
        _contacts.Add(contact);
    }

    public bool RemoveContact(string firstname)
    {
        int originalSize = _contacts.Count;
        _contacts = _contacts.Where(c => c.FirstName.ToLower().Trim() != firstname.ToLower().Trim()).ToList();
        int newSize = _contacts.Count;

        return originalSize != newSize;
    }

    public bool RemoveContact(string firstname, string lastname)
    {
        int originalSize = _contacts.Count;
        _contacts = _contacts.Where(c => c.FirstName.ToLower().Trim() != firstname.ToLower().Trim() || c.LastName.ToLower().Trim() != lastname.ToLower().Trim()).ToList();
        int newSize = _contacts.Count;

        return originalSize != newSize;
    }

    public Contact? FindContact(string firstname)
    {
        return _contacts.FirstOrDefault(c => c.FirstName.ToLower().Trim() == firstname.ToLower().Trim());
    }

    public Contact? FindContact(string firstname, string lastname)
    {
        return _contacts.FirstOrDefault(c => c.FirstName.ToLower().Trim() == firstname.ToLower().Trim() && c.LastName.ToLower().Trim() == lastname.ToLower().Trim());
    }

}

