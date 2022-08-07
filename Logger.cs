using Spectre.Console;

namespace ProxyFinderAndChecker
{
    public static class Logger
    {
        public static void Log(string text, Color color)
        {
            AnsiConsole.Foreground = Color.Orange1;
            AnsiConsole.Write("LOG : ");
            AnsiConsole.Foreground = color;
            AnsiConsole.WriteLine(text);

        }

        public static void Note(Color color,params string[] notes)
        {
            AnsiConsole.WriteLine("");

            AnsiConsole.Foreground = color;
            foreach(string note in notes)
            {
                AnsiConsole.WriteLine("- "+note);
            }

            AnsiConsole.WriteLine("");

            Console.ResetColor();

        }




    }
}
