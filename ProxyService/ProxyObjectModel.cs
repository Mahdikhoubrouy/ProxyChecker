namespace ProxyChecker.ProxyService
{
    public class ProxyObjectModel
    {

        public string Url { get; set; }

        public string ProxyType { get; set; }

        public string Port { get; set; }

        public List<Thread> Threads { get; set; }

        public ResultRegistrar Registerar { get; set; }

        public ProxyObjectModel(List<Thread> ThreadList, string url, string proxytype, ResultRegistrar registerar)
        {
            Threads = ThreadList;
            Url = url;
            Port = Url.Split(":")[1];
            ProxyType = proxytype;
            Registerar = registerar;
        }
    }


}