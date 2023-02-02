using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAddressBook.Services;

public class FileService
{
    public string FilePath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\contactConsole.json";
    public void Save(string filePath, string content)
    {
        using var sw = new StreamWriter(filePath);
        sw.WriteLine(content);
    }
    private string Read(string filePath)
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

    public void Save(string v)
    {
        File.WriteAllTextAsync(FilePath, v).Wait();
    }

    public string Read()
    {
        return Read(FilePath);
    }
}
