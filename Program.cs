using ProxyFinderAndChecker;
using Spectre.Console;
using System.Net;
using System.Text.Json;

#region ProxyFiles

string RunTimePath = Environment.CurrentDirectory;
string ProxyFolder = Path.Combine(RunTimePath, "Proxies");
string CheckedPath = Path.Combine(ProxyFolder, "CheckedProxy");
if (!Directory.Exists(ProxyFolder)) Directory.CreateDirectory(ProxyFolder);
if (!Directory.Exists(CheckedPath)) Directory.CreateDirectory(CheckedPath);
var ProxyListFiles = new List<string>();
var files = Directory.GetFiles(ProxyFolder, "*.txt");
foreach (var file in files)
{
    ProxyListFiles.Add(file.Split("Proxies")[1].Substring(1));
}

#endregion

#region About

Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine(@"    ____                           ________              __            
   / __ \_________  _  ____  __   / ____/ /_  ___  _____/ /_____  _____
  / /_/ / ___/ __ \| |/_/ / / /  / /   / __ \/ _ \/ ___/ //_/ _ \/ ___/
 / ____/ /  / /_/ />  </ /_/ /  / /___/ / / /  __/ /__/ ,< /  __/ /    
/_/   /_/   \____/_/|_|\__, /   \____/_/ /_/\___/\___/_/|_|\___/_/     
                      /____/                                           ");

Logger.Note(Color.Purple, "Coded By : MohmmadMahdi Khoubrouy", "Github : https://github.com/miticyber", "If you like it, please star the project on GitHub");


#endregion


#region Check Exists Proxy file in directory
if (ProxyListFiles.Count() <= 0)
{
    Logger.Log("ProxyList File Not Found In Folder Proxies", Color.Red);
    Logger.Note(Color.Cyan1, "For Working this app please first create ProxyList and Copy to Proxies Folder and then Run app");
    Logger.Note(Color.Khaki1, "Press enter for exit...");
    Console.ReadKey();
}
#endregion

#region main
else
{
    Console.WriteLine("");
    var selectedFile = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("What's your [green]ProxyList file[/]?")
        .PageSize(10)
        .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
        .AddChoices(ProxyListFiles));

    var readFile = FileWorker.ReadFile(Path.Combine(ProxyFolder, selectedFile));

    Logger.Log($"{readFile.Count()} Proxy Found !", Color.Green);

    Console.WriteLine("");
    var SelectedProxyType = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("What's your [blue]ProxyList Type[/]?")
        .PageSize(10)
        .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
        .AddChoices(new[]
        {
            "http","socks4","socks5"
        }));

    int success = 0;
    int failed = 0;
    foreach (var prox in readFile)
    {
        string port = prox.Split(":")[01];
        try
        {
            var proxy = new Proxy();
            string res = proxy.CheckProxy(new WebProxy($"{SelectedProxyType}://{prox}"));
            if (!string.IsNullOrEmpty(res))
            {
                var json = JsonSerializer.Deserialize<CheckedProxyData>(res);
                string data = $"{json.query}:{port},{json.country}";
                FileWorker.WriteFile(data, Path.Combine(CheckedPath, selectedFile));
                Logger.Log("Check Success : " + data, Color.Green);
                success++;
            }
        }
        catch
        {
            failed++;
            Logger.Log("Check Failed : " + prox, Color.Red);
        }
    }

    Logger.Note(Color.Aqua, "All proxies were checked ..", "Press enter to exit");
    Console.ReadKey();
}
#endregion