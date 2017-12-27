using System;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Threading;

namespace Platform.Support.Windows
{
    public partial class ServiceLoader : ServiceBase
    {
        public ServiceLoader(string name, string commandPath, string workingPath = "")
        {
            ServiceName = name;

            if (commandPath.StartsWith("~/") || !Path.IsPathRooted(commandPath))
                CommandPath = Path.Combine(new FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).Directory.FullName, commandPath.Replace("~/", string.Empty));
            else
                CommandPath = commandPath;
            WorkingPath = workingPath;

            //InitializeComponent();
        }

        public bool AutoRestart { get; set; } = true;
        public bool Visible { get; set; } = true;
        public string WorkingPath { get; set; }

        public string CommandPath { get; private set; }
        public string[] CommandArgs { get; private set; }

        private uint ProcessId = 0;
        private int WaitInterval = 1000;
        private bool StopService = false;
        private EventLog EventLogger;

        private void WriteLog(string s, EventLogEntryType logtype = EventLogEntryType.Information)
        {
            if (!Environment.UserInteractive)
            {
                Trace.WriteLine(this.GetType().Name + "." + s);
                EventLogger.WriteEntry(s, logtype);
            }
            else
            {
                switch (logtype)
                {
                    case EventLogEntryType.Error:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        break;

                    case EventLogEntryType.Warning:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        break;

                    case EventLogEntryType.Information:
                        Console.ForegroundColor = ConsoleColor.Gray;
                        break;

                    case EventLogEntryType.SuccessAudit:
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        break;

                    case EventLogEntryType.FailureAudit:
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        break;

                    default:
                        break;
                }
                Console.WriteLine(s);
                Console.ResetColor();
            }
        }

        private int GetProcId(string commandArgs)
        {
            Process[] p = new Process[0];
            var i = 1;
            var processname = System.IO.Path.GetFileNameWithoutExtension(commandArgs);
            while (p.Length == 0 && i <= 5)
            {
                //WriteLog($"OnStart GetProcessesByName i={i} processname={processname}");
                p = Process.GetProcessesByName(processname);
                Thread.Sleep(1000);
                i += 1;
            }
            //WriteLog($"OnStart p.Length={p.Length}");
            if (p.Length == 0)
            {
                throw new Exception($"Command {commandArgs} has not started. Check the Windows Application Log");
            }
            else if (p.Length > 1)
            {
                throw new Exception($"More than one process found named {commandArgs}.");
            }
            return p[0].Id;
        }

        private void MonitorProcess()
        {
            var checkProcess = true;
            var i = 0;
            var lastTime = System.DateTime.MinValue;
            while (checkProcess && !StopService)
            {
                //Log every hour
                var currentTime = DateTime.Now;
                if ((lastTime - currentTime).TotalHours >= 1)
                {
                    WriteLog($"Monitoring process {ProcessId}");
                    lastTime = currentTime;
                }
                //Check if process exists
                Process process1 = null;
                try
                {
                    process1 = Process.GetProcessById((int)ProcessId);
                    Thread.Sleep(WaitInterval);
                }
                catch (Exception ex)
                {
                    WriteLog(ex.Message);
                    checkProcess = false;
                }
                checkProcess = process1 != null && !process1.HasExited;
                i += 1;
            }
            //Process does not exist, stop the service / restart process
            WriteLog($"Process {ProcessId} has exited, stopping monitoring.");
            ProcessId = 0;
            if (!StopService)
                if (AutoRestart) OnStart(CommandArgs); else Stop();
        }

        protected override void OnStart(string[] args)
        {
            CommandArgs = args;

            try
            {
                EventLogger = new EventLog() { Source = ServiceName, Log = "Application" };

                var commandFile = new FileInfo(CommandPath);
                var path = string.IsNullOrEmpty(WorkingPath) ? commandFile.Directory.FullName : WorkingPath;

                if (!commandFile.Exists)
                    throw new FileNotFoundException($"Invalid command path. '{CommandPath}' not found.");

                var result = false;
                if (Environment.UserInteractive)
                {
                    var process = new Process
                    {
                        StartInfo = new ProcessStartInfo(commandFile.FullName, string.Join(", ", CommandArgs))
                    };

                    result = process.Start();
                    ProcessId = (uint)process.Id;
                }
                else
                    result = Advapi32.StartProcessAsCurrentUser(commandFile.FullName, out ProcessId, string.Join(", ", CommandArgs), path, Visible);

                if (!result) throw new Exception("Starting process failed");

                var monitor = new Thread(MonitorProcess);
                monitor.Start();
                WriteLog($"Starting process {ProcessId}");
            }
            catch (Exception ex)
            {
                WriteLog($"Starting process error: {ex.ToString()}", EventLogEntryType.Error);
                Stop();
                throw;
            }
        }

        protected override void OnStop()
        {
            //WriteLog("OnStop begin");
            StopService = true;
            if (ProcessId > 0)
            {
                Process p = null;
                try
                {
                    //WriteLog($"OnStop GetProcessById {_ProcID}");
                    p = Process.GetProcessById(Convert.ToInt32(ProcessId));
                }
                catch (Exception ex)
                {
                    WriteLog($"Process {ProcessId} probably not running. {ex.Message}");
                }
                if (p != null)
                {
                    try
                    {
                        while (!p.HasExited)
                        {
                            WriteLog($"Killing process {ProcessId} and waiting {WaitInterval} milliseconds.");
                            p.Kill();
                            p.WaitForExit(WaitInterval);
                        }
                        WriteLog($"Process {ProcessId} killed successfully.");
                    }
                    catch (Exception ex)
                    {
                        WriteLog($"Process {ProcessId} possibly not killed, might have to kill it manually. {ex.ToString()}");
                    }
                }
            }
            //WriteLog("OnStop end");
        }
    }
}