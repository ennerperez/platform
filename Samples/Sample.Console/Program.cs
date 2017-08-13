using System;
using ListBox = Platform.Support.ConsoleEx.ListBox;

namespace Sample.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (Platform.Support.Library.IsPortable())
                System.Console.WriteLine("Platform PCL");

            if (Platform.Support.Core.Library.IsPortable())
                System.Console.WriteLine("Platform PCL");

            if (Platform.Support.OS.OSHelper.IsWindows())
                System.Console.WriteLine("Running Windows");

            if (Platform.Support.OS.OSHelper.IsLinux())
                System.Console.WriteLine("Running Linux");

            System.Console.ReadKey();

            string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            ListBox.WriteColorString("Choose Level using down and up arrow keys and press enter", 12, 20, ConsoleColor.Black, ConsoleColor.White);
            int choice = ListBox.ChooseListBoxItem(months, 34, 3, ConsoleColor.Blue, ConsoleColor.White);
            // do something with choice
            ListBox.WriteColorString("You chose " + months[choice - 1] + ". Press any key to exit", 21, 22, ConsoleColor.Black, ConsoleColor.White);

            System.Console.ReadKey();
        }
    }
}