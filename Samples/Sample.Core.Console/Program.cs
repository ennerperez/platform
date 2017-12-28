using System;
using ListBox = Platform.Support.ConsoleEx.ListBox;
using Platform.Support.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Core
{
    public class Demostrative //: Platform.Support.Core.Singleton<Demostrative>
    {
        public string Name { get; set; }
        private static Demostrative instance;

        protected Demostrative()
        {
        }

        public static Demostrative Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Demostrative();
                }
                return instance;
            }
        }
    }
}

namespace Sample.Core.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            System.Console.WriteLine("Platform NETCore");

            if (Sample.Core.Demostrative.Instance.IsSingleton())
                System.Console.WriteLine("Demostrative is Singleton");

            if (Platform.Support.OS.Environment.IsWindows())
                System.Console.WriteLine("Running Windows");

            if (Platform.Support.OS.Environment.IsLinux())
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