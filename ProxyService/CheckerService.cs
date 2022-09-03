using ProxyFinderAndChecker;
using System.Net;
using System.Text.Json;

namespace ProxyChecker.ProxyService
{
    public class CheckerService
    {
        private List<Thread> ThreadList { get; set; } = new List<Thread>();


        private static void ProxyChecker(object data)
        {
            var Proxy = (ProxyObjectModel)data;

            var proxyService = new Proxy();
            string res = proxyService.CheckProxy(new WebProxy($"{Proxy.ProxyType}://{Proxy.Url}"));
            if (res != "Error")
            {
                var json = JsonSerializer.Deserialize<CheckedProxyData>(res);
                Proxy.Registerar.RegisterGood($"{json.query}:{Proxy.Port},{json.country}");
            }
            else
            {
                Proxy.Registerar.RegisterBad(Proxy.Url);
            }

        }


        public int GetThreadAlive()
        {
            return ThreadList.Where(x=>x.IsAlive == true).Select(x=>x).ToList().Count ;
        }

        public int GetGoodCount()
        {
            return ResultRegistrar.CounterGood;
        }
        public int GetBadCount()
        {
            return ResultRegistrar.CounterBad;
        }

        public void StartCheckProxyService(int BotThread, string[] Proxies, string Type, RegistrarSetting registrarSetting)
        {
            foreach (var proxy in Proxies)
            {
                if (ThreadList.Where(x=>x.IsAlive == true).Select(x=>x).Count() <= BotThread)
                {
                    Thread th = new Thread(new ParameterizedThreadStart(ProxyChecker));
                    th.IsBackground = true;
                    th.Start(
                        new ProxyObjectModel(ThreadList, proxy, Type,
                        new ResultRegistrar()
                        { PathSaveGood = registrarSetting.Path })); ;

                    ThreadList.Add(th);
                    ThreadList.Where(x => x.IsAlive == false).Select(x => x).ToList().ForEach(t =>
                    {
                        ThreadList.Remove(t);
                    });
                }
                else
                {
                    while (true)
                    {
                        Thread.Sleep(100);
                        if (ThreadList.Where(x => x.IsAlive == true).Select(x => x).Count() <= BotThread)
                        {
                            break;
                        }
                    }
                }
            }

        }

    }

}
