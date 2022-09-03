using ProxyChecker.ProxyService;
using ProxyChecker.Settings;
using ProxyFinderAndChecker;
using Spectre.Console;
using System.Diagnostics;
#region Configurations

string RunTimePath = Environment.CurrentDirectory;
string ProxyFolder = Path.Combine(RunTimePath, "Proxies");
string CheckedPath = Path.Combine(ProxyFolder, "CheckedProxy");
if (!Directory.Exists(ProxyFolder)) Directory.CreateDirectory(ProxyFolder);
if (!Directory.Exists(CheckedPath)) Directory.CreateDirectory(CheckedPath);
SettingFileConfigure.CreateSettingFile();
var ProxyListFiles = new List<string>();
var files = Directory.GetFiles(ProxyFolder, "*.txt");
foreach (var file in files)
{
    ProxyListFiles.Add(file.Split("Proxies")[1].Substring(1));
}

#endregion

#region About

Console.WriteLine("");
Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine(@"    ____                           ________              __            
   / __ \_________  _  ____  __   / ____/ /_  ___  _____/ /_____  _____
  / /_/ / ___/ __ \| |/_/ / / /  / /   / __ \/ _ \/ ___/ //_/ _ \/ ___/
 / ____/ /  / /_/ />  </ /_/ /  / /___/ / / /  __/ /__/ ,< /  __/ /    
/_/   /_/   \____/_/|_|\__, /   \____/_/ /_/\___/\___/_/|_|\___/_/     
                      /____/                                           ");

Logger.Note(Color.Purple, "Coded By : MohmmadMahdi Khoubrouy", "Github : https://github.com/miticyber", "If you like it, please star the project on GitHub");


Logger.Note(Color.Orange1, $"Current Timeout Value : {SettingFileConfigure.GetTimeOut()} Seconds ", $"Current BotThread Value : {SettingFileConfigure.GetBotThread()}");

#endregion


#region Main Menu

var SectionSelected = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("[green]Please select the section you want :backhand_index_pointing_down:[/]")
        .PageSize(10)
        .HighlightStyle(Style.Plain.Foreground(Color.Red3_1))
        .MoreChoicesText("[grey](Move up and down to reveal more Section)[/]")
        .AddChoices(new[] {
            "Proxy Checker","Setting","Exit"
        })); ;



if (SectionSelected == "Proxy Checker")
{
    #region Proxy Section
    if (ProxyListFiles.Count() <= 0)
    {
        Logger.Log("ProxyList File Not Found In Folder Proxies", Color.Red);
        Logger.Note(Color.Cyan1, "For Working this app please first create ProxyList", "Copy to Proxies Folder and then Run app");
        Logger.Note(Color.Khaki1, "Press enter for exit...");
        Console.ReadKey();
    }
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




        var ProxyCheckerService = new CheckerService();
        ProxyCheckerService.StartCheckProxyService(SettingFileConfigure.GetBotThread(), readFile, SelectedProxyType,
            new RegistrarSetting()
            .SetGoodPath(Path.Combine(CheckedPath, selectedFile)));

        while (true)
        {
            Thread.Sleep(1000);
            if (ProxyCheckerService.GetThreadAlive() <= 0)
            {
                Logger.Note(Color.Aqua, "All proxies were checked ..", $"Good = {ProxyCheckerService.GetGoodCount()} ", $"Bad = {ProxyCheckerService.GetBadCount()}", "Press enter to exit");
                Console.ReadKey();
                break;
            }
        }

    }
    #endregion
}
else if (SectionSelected == "Setting")
{
    #region Setting Section
    var SettingItem = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("[green]Please select the section you want [/]")
        .PageSize(10)
        .HighlightStyle(Style.Plain.Foreground(Color.Red3_1))
        .MoreChoicesText("[grey](Move up and down to reveal more Section)[/]")
        .AddChoices(new[] {
            "BotThread","TimeOut","Exit"
        })); ;


    if (SettingItem == "BotThread")
    {
        #region BotThread Section
        Logger.Note(Color.Aqua, $"Current BotThread value: {SettingFileConfigure.GetBotThread()}");
        var BotThreadNumber = AnsiConsole.Ask<int>(" [royalblue1]What is your desired new[/] [red]number[/] for [green]Bot Thread[/] ?");
        if (BotThreadNumber > 0)
        {
            SettingFileConfigure.SetBotThread(BotThreadNumber);

            AnsiConsole.MarkupLine("[italic]The new value was [green]successfully[/] set[/]");
        }
        else
        {
            AnsiConsole.MarkupLine("[bold][red]Input cannot be less than zero or zero[/][/]");
            AnsiConsole.MarkupLine("[italic]The new value was [red]failed[/] set[/]");
        }

        #endregion
    }
    else if (SettingItem == "TimeOut")
    {
        Logger.Note(Color.Aqua, $"Current TimeOut value: {SettingFileConfigure.GetTimeOut()}");
        var TimeOutValue = AnsiConsole.Ask<int>(" [bold][green]What is the amount of[/] [red]timeout[/] [green]based on[/] [red]seconds[/] [/] ?");

        if (TimeOutValue > 0)
        {
            SettingFileConfigure.SetTimeOut(TimeOutValue);

            AnsiConsole.MarkupLine("[italic]The new value was [green]successfully[/] set[/]");
        }
        else
        {
            AnsiConsole.MarkupLine("[bold][red]Input cannot be less than zero or zero[/][/]");
            AnsiConsole.MarkupLine("[italic]The new value was [red]failed[/] set[/]");
        }
    }
    else if (SettingItem == "Exit")
    {
        #region Exit Section
        AnsiConsole.MarkupLine("[italic][aqua] Ok GoodBye .. [/][/]");
        Thread.Sleep(1500);
        Environment.Exit(0);
        #endregion
    }


    #endregion
}
else if (SectionSelected == "Exit")
{
    #region Exit Section
    AnsiConsole.MarkupLine("[italic][aqua] Ok GoodBye .. [/][/]");
    Thread.Sleep(1500);
    Environment.Exit(0);
    #endregion 
}

#endregion




Console.ReadKey();