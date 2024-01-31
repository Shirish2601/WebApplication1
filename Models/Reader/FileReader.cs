using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Reader
{
    public class FileReader : IDataReader
    {
        public void Read(string? path)
        {
            if (path is not null) 
            {
                string? file = string.Empty;
                string? currentLine = string.Empty;
                using (StreamReader sr = new StreamReader(path))
                {
                    currentLine = sr.ReadLine();
                    file += currentLine;
                    file += "\n";
                    while (currentLine != null)
                    {
                        currentLine = sr.ReadLine();
                        file += currentLine;
                        file += "\n";
                    }
                }
                Console.WriteLine(file);
            }
        }
    }
}
