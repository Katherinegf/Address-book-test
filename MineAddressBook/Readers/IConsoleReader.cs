using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAddressBook.Readers
{
    public interface IConsoleReader
    {
        string? ReadLine();
        void Write(string str);
        void WriteLine(InfoType type, string line);
        void Clear();
        ConsoleKeyInfo ReadKey();
    }
}
