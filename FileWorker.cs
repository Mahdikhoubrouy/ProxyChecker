namespace ProxyFinderAndChecker
{
    public class FileWorker
    {
        static readonly object _ThreadLock = new object();

        public static string[] ReadFile(string path)
        {
            return File.ReadAllLines(path);
        }
        public static void WriteFile(string text, string path)
        {
            lock (_ThreadLock)
            {
                try
                {
                    if (!File.Exists(path)) File.Create(path).Close();
                    using (StreamWriter writer = new StreamWriter(path, true))
                    {
                        writer.WriteLine(text);
                    };
                }
                catch
                {

                }
            }
        }
    }
}
