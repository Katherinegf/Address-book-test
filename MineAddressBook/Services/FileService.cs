using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAddressBook.Services;

public class FileService
{
    private string FilePath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\contactConsole.json";
    public void Save(string filePath, string content)
    {
        using var sw = new StreamWriter(filePath);
        sw.WriteLine(content);
    }
    public string Read(string filePath)
    {
        try
        {
            using var sr = new StreamReader(filePath);
            return sr.ReadToEnd();
        }
        catch
        {
            return null!;
        }
    }

    internal void Save(string v)
    {
        throw new NotImplementedException();
    }
}
