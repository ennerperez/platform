using Platform.Support.Reflection;
using System;
using System.Diagnostics;
using System.Reflection;

namespace Platform.Support.Windows
{
    public static class UAC
    {
        public static void RestartElevated()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                UseShellExecute = true,
                WorkingDirectory = Environment.CurrentDirectory,
                FileName = Assembly.GetEntryAssembly().ExecutablePath(),
                Verb = "runas",
                Arguments = Environment.CommandLine
            };

            try
            {
                global::System.Diagnostics.Process p = global::System.Diagnostics.Process.Start(startInfo);
            }
            catch
            {
                return;
            }

            System.Environment.Exit(0);
        }

        public static void RestartNonElevated()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                UseShellExecute = false,
                WorkingDirectory = Environment.CurrentDirectory,
                FileName = Assembly.GetEntryAssembly().ExecutablePath()
            };

            try
            {
                global::System.Diagnostics.Process p = global::System.Diagnostics.Process.Start(startInfo);
            }
            catch
            {
                return;
            }

            System.Environment.Exit(0);
        }

        public static void StartElevated(string varExecutablePath, ProcessWindowStyle varWindowStyle, string varArguments = "", bool varWait = true, bool varCreateNoWindow = false)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            {
                startInfo.UseShellExecute = true;
                startInfo.Verb = "runas";
                startInfo.Arguments = varArguments;
                startInfo.WindowStyle = varWindowStyle;
                startInfo.FileName = varExecutablePath;
                startInfo.CreateNoWindow = varCreateNoWindow;
                startInfo.ErrorDialog = true;
            }

            try
            {
                global::System.Diagnostics.Process p = global::System.Diagnostics.Process.Start(startInfo);
                if (varWait)
                {
                    p.WaitForExit();
                }
            }
            catch
            {
            }
        }
    }
}