//#define SANDBOX

using System;
using ListBox = Platform.Support.ConsoleEx.ListBox;

#if SANDBOX

using Sandbox;

#else

using Platform.Support.Reflection;

#endif

using System.Linq;

namespace Platform.Samples
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (Platform.Support.Library.IsPortable())
                System.Console.WriteLine("Platform PCL");

            if (typeof(Core.Models.Model1).IsSingleton())
                System.Console.WriteLine("Model1 is Singleton");

#if SANDBOX

            // Insert sandbox code here

            Singleton<Core.Models.Model2>.Instance.Name = "Instance 1";
            System.Console.WriteLine($"{Singleton<Core.Models.Model2>.Id}:{Singleton<Core.Models.Model2>.Instance.Name}");

            var instance2 = Singleton<Core.Models.Model2>.Instance;
            instance2.Name = "Instance 2";

            System.Console.WriteLine($"{Singleton<Core.Models.Model2>.Id}:{Singleton<Core.Models.Model2>.Instance.Name}");
            System.Console.WriteLine($"{Singleton<Core.Models.Model2>.Id}:{instance2.Name}");

            var instance3 = Singleton<Core.Models.Person>.Instance;
            instance3.Name = "Enner Pérez";

            System.Console.WriteLine($"{Singleton<Core.Models.Model2>.Id}:{Singleton<Core.Models.Model2>.Instance.Name}");
            System.Console.WriteLine($"{Singleton<Core.Models.Person>.Id}:{instance3.Name}");
            System.Console.WriteLine($"{Singleton<Core.Models.Model2>.Id}:{instance2.Name}");

            var instance4 = typeof(Core.Models.Model2).Of();

#endif

            if (Platform.Support.OS.Environment.IsWindows())
                System.Console.WriteLine("Running Windows");
            else if (Platform.Support.OS.Environment.IsLinux())
                System.Console.WriteLine("Running Linux");
            else
                System.Console.WriteLine("Running Unknown OS");

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

#if SANDBOX

namespace Sandbox
{
    // Insert sandbox clases here
    /// <summary>
    /// Implements singleton pattern
    /// </summary>
    /// <typeparam name="T">Class type</typeparam>
    public class Singleton<T> : Singleton where T : class, new()
    {
        public new static T Instance
        {
            get
            {
                return (T)base.Instance;
            }
        }

        protected Singleton() : base(typeof(T))
        {
        }
    }

    public class Singleton
    {
        internal static object _syncobj = new object();
        internal static volatile object _instance = null;

        private static Type internalType;

        public static object Instance
        {
            get
            {
                if (_instance == null)
                    lock (_syncobj)
                        if (_instance == null)
                            _instance = Activator.CreateInstance(internalType);
                return _instance;
            }
        }

        private static Guid id = Guid.NewGuid();
        public static Guid Id { get { return id; } }

        protected Singleton(Type type)
        {
            internalType = type;
        }

        internal static object GetInstance(Type type)
        {
            return Instance;
        }
    }

    public static class Extensions
    {
        public static T Of<T>(this Type type) where T : class, new()
        {
            return (T)Singleton.GetInstance(type);
        }

        public static object Of(this Type type)
        {
            return Singleton.GetInstance(type);
        }

        public static bool IsSingleton(this Type type)
        {
#if !PORTABLE || PROFILE_328
            var props = type.GetProperties(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            var instance = props.Any(p => p.Name == "Instance");

            var ctors = type.GetMembers(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).OfType<System.Reflection.ConstructorInfo>();
            var ctor = ctors.Where(m => m.IsFamily).Any();
#else
                    var props = type.GetRuntimeProperties();
                    var instance = props.Any(p => p.Name == "Instance");

                    var ctors = type.GetRuntimeMethods().Where(m => m.IsConstructor);
                    var ctor = ctors.Where(m => m.IsFamily).Any();
#endif
            return instance && ctor;
        }

        public static bool IsSingleton(this object obj)
        {
            return obj.GetType().IsSingleton();
        }
    }
}

#endif