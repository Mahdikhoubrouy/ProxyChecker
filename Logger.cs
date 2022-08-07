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
            Console.WriteLine("");
            string sep = "";
            for (int i = 0; i < (notes.First().Length + notes[^1].Length) + 10; i++) sep += "=";

            SepWriter(sep);

            AnsiConsole.Foreground = color;
            foreach(string note in notes)
            {
                Console.SetCursorPosition((Console.WindowWidth - note.Length) / 2, Console.CursorTop);
                AnsiConsole.WriteLine(note);
            }

            SepWriter(sep);

            Console.ResetColor();
            Console.SetCursorPosition(0, Console.CursorTop);

        }


        private static void SepWriter(string sep)
        {
            Console.SetCursorPosition((Console.WindowWidth - sep.Length) / 2, Console.CursorTop);
            AnsiConsole.Foreground = Color.DarkCyan;
            AnsiConsole.WriteLine(sep);
        }


    }
}
