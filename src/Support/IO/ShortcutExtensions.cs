#if !PORTABLE

using System.Diagnostics;

namespace Platform.Support.IO
{
    public static class ShortcutExtensions
    {
        public static string CreateShortcut(this ProcessStartInfo process, string targetPath, string name, string description = "")
        {
            return Shortcut.Create(process.FileName, targetPath, name, description, process.Arguments, process.WorkingDirectory);
        }

        public static string CreateOnDesktop(this ProcessStartInfo process, string name = "", string description = "", string args = "", string workDir = "")
        {
            return Shortcut.CreateOnDesktop(process.FileName, name, description, process.Arguments, process.WorkingDirectory);
        }

        public static string CreateOnPrograms(this ProcessStartInfo process, string folder = "", string name = "", string description = "", string args = "", string workDir = "")
        {
            return Shortcut.CreateOnPrograms(process.FileName, name, description, process.Arguments, process.WorkingDirectory);
        }
    }
}

#endif