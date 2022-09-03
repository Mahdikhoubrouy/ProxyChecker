using ProxyFinderAndChecker;
using Spectre.Console;

namespace ProxyChecker.ProxyService
{
    public class ResultRegistrar
    {
        public string PathSaveGood;
        static readonly object _ThreadLockGood = new object();
        static readonly object _ThreadLockBad = new object();
        public static int CounterGood { get; set; }
        public static int CounterBad { get; set; }

        public void RegisterGood(string data)
        {
            lock (_ThreadLockGood)
            {
                CounterGood++;
                Logger.Log("Good -> " + data, Color.Green);
            }

            FileWorker.WriteFile(data, PathSaveGood);
        }

        public void RegisterBad(string proxy)
        {
            lock (_ThreadLockBad)
            {
                CounterBad++;
                Logger.Log("Bad -> " + proxy, Color.Red);
            }
        }
    }


    public class RegistrarSetting
    {
        public string Path;
        public RegistrarSetting SetGoodPath(string path)
        {
            Path = path;
            return this;
        }

    }
}
