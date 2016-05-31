﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Console
{
    class Program
    {
        static void Main(string[] args)
        {

            if (Platform.Support.Library.IsPortable())
                System.Console.WriteLine("Platform PCL");

            if (Platform.Support.Core.Library.IsPortable())
                System.Console.WriteLine("Platform PCL");

            if (Platform.Support.OS.Helpers.IsWindows())
                System.Console.WriteLine("Running Windows");

            if (Platform.Support.OS.Helpers.IsLinux())
                System.Console.WriteLine("Running Linux");


            System.Console.ReadKey();

        }
    }
}
