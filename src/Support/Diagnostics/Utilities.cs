#if !PORTABLE

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace Platform.Support.Diagnostics
{
    public static partial class Utilities
    {
        public static bool IsProcessRunning(string processName)
        {
            Process[] processesByName = Process.GetProcessesByName(processName);
            int currentId = Process.GetCurrentProcess().Id;
            return processesByName.Length != 0 && processesByName.Any((Process p) => p.Id != currentId);
        }

        public static void KillProcess(string folderName, int waitTime)
        {
            foreach (Process process in Process.GetProcesses())
            {
                try
                {
                    string text;
                    if (process == null)
                    {
                        text = null;
                    }
                    else
                    {
                        ProcessModule mainModule = process.MainModule;
                        text = (mainModule?.FileName);
                    }
                    string text2 = text;
                    if (text2 != null && text2.ToLowerInvariant().Contains(folderName.ToLowerInvariant()))
                    {
                        Console.WriteLine(text2);
                        while (!process.HasExited)
                        {
                            process.Kill();
                            Thread.Sleep(100);
                        }
                    }
                }
                catch
                {
                }
            }
            Thread.Sleep(waitTime);
        }
    }
}

#endif