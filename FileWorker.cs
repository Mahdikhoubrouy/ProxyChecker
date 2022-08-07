using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyFinderAndChecker
{
    public class FileWorker
    {
        public static string[] ReadFile(string path)
        {
            return File.ReadAllLines(path);
        }
        public static void WriteFile(string text, string path)
        {
            if(!File.Exists(path)) File.Create(path);
            using (StreamWriter writer = new StreamWriter(path,true))
            {
                writer.WriteLine(text);
            };
        }
    }
}
