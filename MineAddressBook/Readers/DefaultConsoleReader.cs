using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAddressBook.Readers
{
    internal class DefaultConsoleReader : IConsoleReader
    {
        public void Clear()
        {
            Console.Clear();
        }

        public ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey();
        }

        public string? ReadLine()
        {
            return Console.ReadLine();
        }

        public void Write(string str)
        {
            Console.Write(str);
        }

        public void WriteLine(InfoType type, string line)
        {
            Console.WriteLine(line);
        }
    }
}
