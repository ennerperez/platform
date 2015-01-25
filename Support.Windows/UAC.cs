using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Support.Windows
{
    public class UAC
    {

        public static void RestartElevated()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = true;
            startInfo.WorkingDirectory = Environment.CurrentDirectory;
            startInfo.FileName = Support.Helpers.ExecutablePath();
            startInfo.Verb = "runas";
            startInfo.Arguments = Environment.CommandLine;


            try
            {
                global::System.Diagnostics.Process p = global::System.Diagnostics.Process.Start(startInfo);


            }
            catch //(Exception ex)
            {
                return;
                //If cancelled, do nothing

            }

            System.Environment.Exit(0);

        }

        public static void RestartNonElevated()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();

            startInfo.UseShellExecute = false;

            startInfo.WorkingDirectory = Environment.CurrentDirectory;

            startInfo.FileName = Support.Helpers.ExecutablePath();

            //startInfo.Verb = "runas"


            try
            {
                global::System.Diagnostics.Process p = global::System.Diagnostics.Process.Start(startInfo);


            }
            catch //(Exception ex)
            {
                return;
                //If cancelled, do nothing

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
            catch //(Exception ex)
            {
                //My.Application.Log.WriteException(ex);
                //If cancelled, do nothing
            }

        }

    }
}
