using AddressBook.Services;
using MyAddressBook.Models;
using MyAddressBook.Readers;
using System.Runtime.CompilerServices;


var menuService = new MenuServices(new DefaultConsoleReader());

while(true)
{
    menuService.WelcomeMenu();
}

    //var contact = new Contact();




