using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAddressBook.Readers
{
    public class TestConsoleReader : IConsoleReader
    {
        private readonly List<string> outputLines = new();
        public IReadOnlyCollection<string> InfoOutputLines { get { return outputLines; } }
        private readonly Queue<string> inputQueue = new();

        public Queue<ConsoleKeyInfo> readKeyQueue = new();

        public void EnqueueKey(ConsoleKeyInfo key)
        {
            readKeyQueue.Enqueue(key);
        }

        public void EnqueueInput(string line)
        {
            inputQueue.Enqueue(line);
        }

        public void Clear()
        {
            
        }

        public ConsoleKeyInfo ReadKey()
        {
            // 
            return new ConsoleKeyInfo(' ', ConsoleKey.Spacebar, false, false, false);
        }

        public string? ReadLine()
        {
            return inputQueue.Dequeue();
        }

        public void Write(string str)
        {
            // should only be used for input. Might be relevant to store/document somehow
        }

        public void WriteLine(InfoType type, string line)
        {
            if(type == InfoType.Information)
                outputLines.Add(line);
        }
    }
}
