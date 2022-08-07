using System.Net;

namespace ProxyFinderAndChecker
{
    public class Proxy
    {

        public string CheckProxy(WebProxy proxy)
        {
            string data = "";
            using (var client = new HttpClient(handler: new HttpClientHandler() { Proxy = proxy }))
            {
                client.Timeout = TimeSpan.FromSeconds(5);
                data = client.GetStringAsync("http://ip-api.com/json").GetAwaiter().GetResult();
            }
            return data;
        }
    }


    public class CheckedProxyData
    {
        public string status { get; set; }
        public string country { get; set; }
        public string countryCode { get; set; }
        public string region { get; set; }
        public string regionName { get; set; }
        public string city { get; set; }
        public string zip { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string timezone { get; set; }
        public string isp { get; set; }
        public string org { get; set; }
        public string @as { get; set; }
        public string query { get; set; }
    }
}
