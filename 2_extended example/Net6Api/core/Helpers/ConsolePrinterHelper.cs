namespace net6.core.Helpers
{
    public class ConsolePrinterHelper
    {
        public static void PrintWithColor(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
