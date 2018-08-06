using System;
using System.Collections.Generic;
using System.ComponentModel;
using Platform.Support.Reflection;
using Platform.Support;
using System.Drawing.Printing;
using System.IO;
using Platform.Presentation.Reports.Razor;
using System.Security;
using Microsoft.Win32;
using System.Linq;

namespace Platform.Presentation.Reports
{
    namespace Windows.Forms
    {
        public class RazorReportViewer : System.Windows.Forms.WebBrowser, INotifyPropertyChanged
        {
            public RazorReportViewer() : base()
            {
                InitializeComponent();

                if (!IsBrowserEmulationSet())
                    SetBrowserEmulationVersion();
            }

            private void InitializeComponent()
            {
                this.SuspendLayout();

                PropertyChanged += RazorReportViewer_PropertyChanged;
                DocumentCompleted += RazorReportViewer_DocumentCompleted;

                this.ResumeLayout(false);
            }

            #region Properties

            private int zoom;

            public int Zoom
            {
                get
                {
                    return zoom;
                }
                set
                {
                    if (zoom != value)
                    {
                        zoom = value;
                        OnPropertyChanged(new PropertyChangedEventArgs("Zoom"));
                    }
                }
            }

            private string template;

            public string Template
            {
                get
                {
                    return template;
                }
                set
                {
                    if (template != value)
                    {
                        template = value;
                        OnPropertyChanged(new PropertyChangedEventArgs("Template"));
                    }
                }
            }

            private PaperKind paperKind = PaperKind.Letter;

            public PaperKind PaperKind
            {
                get
                {
                    return paperKind;
                }
                set
                {
                    if (paperKind != value)
                    {
                        paperKind = value;
                        OnPropertyChanged(new PropertyChangedEventArgs("PaperKind"));
                    }
                }
            }

            //private Orientation paperLayout;
            //public Orientation PaperLayout { get { return paperLayout; } set { this.SetField(ref paperLayout, value); } }

            private object model;

            public object Model
            {
                get
                {
                    return model;
                }
                set
                {
                    if (model != value)
                    {
                        model = value;
                        OnPropertyChanged(new PropertyChangedEventArgs("Model"));
                    }
                }
            }

            //private PaperSize paperSize;
            //public PaperSize PaperSize { get { return paperSize; } set { this.SetField(ref paperSize, value); } }

            #endregion Properties

            public event PropertyChangedEventHandler PropertyChanged;

            protected void OnPropertyChanged(PropertyChangedEventArgs e)
            {
                PropertyChanged?.Invoke(this, e);
            }

            private void RazorReportViewer_PropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                if (!isDocumentCompleted)
                    return;

                if (e.PropertyName == "Zoom")
                    ajustView();
                else
                    Show(ViewBag);
            }

            public Dictionary<string, object> ViewBag { get; internal set; }

            public void Show(Dictionary<string, object> viewBag = null)
            {
                if (DesignMode)
                    return;

                isDocumentCompleted = false;

                ViewBag = viewBag;

                if (ViewBag == null)
                    ViewBag = new Dictionary<string, object>()
                    {
                        { "PaperKind", this.PaperKind }
                    };
                else if (!ViewBag.ContainsKey("PaperKind"))
                    ViewBag.Add("PaperKind", this.PaperKind);

                //if (AppDomain.CurrentDomain.IsDefaultAppDomain())
                //{
                //    // RazorEngine cannot clean up from the default appdomain...
                //    Console.WriteLine("Switching to second AppDomain, for RazorEngine...");
                //    AppDomainSetup adSetup = new AppDomainSetup
                //    {
                //        ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase
                //    };
                //    var current = AppDomain.CurrentDomain;
                //    // You only need to add strongnames when your appdomain is not a full trust environment.
                //    var strongNames = new StrongName[0];

                //    var domain = AppDomain.CreateDomain(
                //        AppDomain.CurrentDomain.FriendlyName, null,
                //        current.SetupInformation, new PermissionSet(PermissionState.Unrestricted),
                //        strongNames);
                //    var exitCode = domain.ExecuteAssembly(Assembly.GetEntryAssembly().Location);
                //    // RazorEngine will cleanup.
                //    AppDomain.Unload(domain);
                //    return;
                //}

                try
                {
                    var report = ReportBuilder<object>.Create(DateTime.Now.Ticks.ToString())
                        .WithTemplate(Template)
                        .WithViewBag(ViewBag)
                        .WithPrecompilation();

                    this.DocumentText = report.BuildReport(Model);
                }
                catch (Exception ex)
                {
                    ex.DebugThis();

                    var report = ReportBuilder<Exception>.Create(DateTime.Now.Ticks.ToString())
                       .WithTemplate(Presentation.Reports.Properties.Resources.Exception)
                       .WithViewBag(ViewBag)
                       .WithPrecompilation();

                    this.DocumentText = report.BuildReport(ex);
                }
            }

            private bool isDocumentCompleted = false;

            private void ajustView()
            {
                if (this.Document != null && this.Document.Body != null)
                    this.Document.Body.Style = $"zoom:{Zoom}%;";
            }

            private void RazorReportViewer_DocumentCompleted(object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
            {
                isDocumentCompleted = true;
                ajustView();
            }

            #region IE

            public enum BrowserEmulationVersion
            {
                Default = 0,
                Version7 = 7000,
                Version8 = 8000,
                Version8Standards = 8888,
                Version9 = 9000,
                Version9Standards = 9999,
                Version10 = 10000,
                Version10Standards = 10001,
                Version11 = 11000,
                Version11Edge = 11001
            }

            private const string InternetExplorerRootKey = @"Software\Microsoft\Internet Explorer";

            public static int GetInternetExplorerMajorVersion()
            {
                int result;

                result = 0;

                try
                {
                    RegistryKey key;

                    key = Registry.LocalMachine.OpenSubKey(InternetExplorerRootKey);

                    if (key != null)
                    {
                        object value;

                        value = key.GetValue("svcVersion", null) ?? key.GetValue("Version", null);

                        if (value != null)
                        {
                            string version;
                            int separator;

                            version = value.ToString();
                            separator = version.IndexOf('.');
                            if (separator != -1)
                            {
                                int.TryParse(version.Substring(0, separator), out result);
                            }
                        }
                    }
                }
                catch (SecurityException)
                {
                    // The user does not have the permissions required to read from the registry key.
                }
                catch (UnauthorizedAccessException)
                {
                    // The user does not have the necessary registry rights.
                }

                return result;
            }

            private const string BrowserEmulationKey = InternetExplorerRootKey + @"\Main\FeatureControl\FEATURE_BROWSER_EMULATION";

            public static BrowserEmulationVersion GetBrowserEmulationVersion()
            {
                BrowserEmulationVersion result;

                result = BrowserEmulationVersion.Default;

                try
                {
                    RegistryKey key;

                    key = Registry.CurrentUser.OpenSubKey(BrowserEmulationKey, true);
                    if (key != null)
                    {
                        string programName;
                        object value;

                        programName = Path.GetFileName(Environment.GetCommandLineArgs()[0]);
                        value = key.GetValue(programName, null);

                        if (value != null)
                        {
                            result = (BrowserEmulationVersion)Convert.ToInt32(value);
                        }
                    }
                }
                catch (SecurityException)
                {
                    // The user does not have the permissions required to read from the registry key.
                }
                catch (UnauthorizedAccessException)
                {
                    // The user does not have the necessary registry rights.
                }

                return result;
            }

            public static bool SetBrowserEmulationVersion(BrowserEmulationVersion browserEmulationVersion)
            {
                bool result;

                result = false;

                try
                {
                    RegistryKey key;

                    key = Registry.CurrentUser.OpenSubKey(BrowserEmulationKey, true);

                    if (key != null)
                    {
                        string programName;

                        programName = Path.GetFileName(Environment.GetCommandLineArgs()[0]);

                        if (browserEmulationVersion != BrowserEmulationVersion.Default)
                        {
                            // if it's a valid value, update or create the value
                            key.SetValue(programName, (int)browserEmulationVersion, RegistryValueKind.DWord);
                        }
                        else
                        {
                            // otherwise, remove the existing value
                            key.DeleteValue(programName, false);
                        }

                        result = true;
                    }
                }
                catch (SecurityException)
                {
                    // The user does not have the permissions required to read from the registry key.
                }
                catch (UnauthorizedAccessException)
                {
                    // The user does not have the necessary registry rights.
                }

                return result;
            }

            public static bool SetBrowserEmulationVersion()
            {
                int ieVersion;
                BrowserEmulationVersion emulationCode;

                ieVersion = GetInternetExplorerMajorVersion();

                if (ieVersion >= 11)
                {
                    emulationCode = BrowserEmulationVersion.Version11;
                }
                else
                {
                    switch (ieVersion)
                    {
                        case 10:
                            emulationCode = BrowserEmulationVersion.Version10;
                            break;

                        case 9:
                            emulationCode = BrowserEmulationVersion.Version9;
                            break;

                        case 8:
                            emulationCode = BrowserEmulationVersion.Version8;
                            break;

                        default:
                            emulationCode = BrowserEmulationVersion.Version7;
                            break;
                    }
                }

                return SetBrowserEmulationVersion(emulationCode);
            }

            public static bool IsBrowserEmulationSet()
            {
                return GetBrowserEmulationVersion() != BrowserEmulationVersion.Default;
            }

            #endregion IE
        }
    }
}