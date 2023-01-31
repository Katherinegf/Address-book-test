using MyAddressBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAddressBook.Services;

public class AddressBookService
{
    public List<Contact> ContactList { get; set; } = new List<Contact>();
}

