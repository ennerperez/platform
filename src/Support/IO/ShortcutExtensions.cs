using System.Diagnostics;

namespace Platform.Support.IO
{
    public static class ShortcutExtensions
    {
        public static string CreateShortcut(this ProcessStartInfo process, string targetPath, string name, string description = "")
        {
            return ShortcutHelper.CreateShortcut(process.FileName, targetPath, name, description, process.Arguments, process.WorkingDirectory);
        }
    }
}