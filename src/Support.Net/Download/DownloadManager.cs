using Platform.Support.IO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Platform.Support.Net.Download
{
    public class DownloadManager : DisposableObject, IDownloadManager
    {
        public DownloadManager()
        {
            this.AddDownloadEngine(new WebClientEngine());
            this.AddDownloadEngine(new BitsEngine());
            this.AddDownloadEngine(new WinInetEngine());
            this.AddDownloadEngine(new FileDownloadEngine());
            this.DefaultEngines = from e in this.allEngines
                                  select e.Name;
            this.SleepDurationBetweenRetries = DownloadManager.DefaultSleepDuration;
        }

        //public DownloadManager(IServiceProvider serviceProvider)
        //{
        //    this.serviceProvider = serviceProvider;
        //    this.logger = serviceProvider.GetService<ILogger>(false);
        //    this.telemetry = serviceProvider.GetService(false);
        //    ISettingsService service = serviceProvider.GetService(false);
        //    if (service == null || !service.NoWeb)
        //    {
        //        this.AddDownloadEngine(new WebClientEngine(this.serviceProvider));
        //        this.AddDownloadEngine(new BitsEngine(this.serviceProvider));
        //        this.AddDownloadEngine(new WinInetEngine(this.serviceProvider));
        //    }
        //    this.AddDownloadEngine(new FileDownloadEngine(this.serviceProvider));
        //    this.DefaultEngines = from e in this.allEngines
        //                          select e.Name;
        //    this.SleepDurationBetweenRetries = DownloadManager.DefaultSleepDuration;
        //}

        public int MaxDownloads
        {
            get
            {
                return this.maxDownloads;
            }
            set
            {
                if (value < 1 || value > 2147483647)
                {
                    throw new InvalidOperationException("Invalid value for MaxDownloads");
                }
                this.maxDownloads = value;
            }
        }

        public IEnumerable<string> DefaultEngines
        {
            get
            {
                return from e in this.defaultEngines
                       select e.Name;
            }
            set
            {
                IEnumerable<IDownloadEngine> preferredEngines = this.GetPreferredEngines(this.allEngines, value);
                this.defaultEngines.Clear();
                this.defaultEngines.AddRange(preferredEngines);
            }
        }

        public IEnumerable<string> AllEngines
        {
            get
            {
                return from e in this.allEngines
                       select e.Name;
            }
        }

        internal int SleepDurationBetweenRetries { get; set; }

        public Task<DownloadSummary> Download(Uri uri, Stream outputStream, ProgressUpdateCallback progress, CancellationToken cancellationToken, DownloadContext downloadContext = null, bool verifySignature = false)
        {
            //ILogger logger = this.logger;
            //if (logger != null)
            //{
            //    logger.WriteVerbose("Download requested: {0}", new object[]
            //    {
            //        uri.AbsoluteUri
            //    });
            //}
            if (outputStream == null)
                throw new ArgumentNullException("outputStream");
            if (!outputStream.CanWrite)
                throw new InvalidOperationException("Strings.Error_InvalidOutputStream_CannotWrite");
            if (!uri.IsFile && !uri.IsUnc)
            {
                if (!string.Equals(uri.Scheme, "http", StringComparison.OrdinalIgnoreCase) && !string.Equals(uri.Scheme, "https", StringComparison.OrdinalIgnoreCase) && !string.Equals(uri.Scheme, "ftp", StringComparison.OrdinalIgnoreCase))
                {
                    ArgumentException ex = new ArgumentException(string.Format(CultureInfo.CurrentCulture, "Strings.Error_UnSupportedUriScheme_Arg1", new object[]
                    {
                        uri.Scheme
                    }));
                    //ILogger logger2 = this.logger;
                    //if (logger2 != null)
                    //{
                    //    logger2.WriteVerbose("Uri scheme '{0}' is not supported. {1}", new object[]
                    //    {
                    //        uri.Scheme,
                    //        ex.Message
                    //    });
                    //}
                    throw ex;
                }
                if (uri.AbsoluteUri.Length < 7)
                {
                    ArgumentException ex2 = new ArgumentException(string.Format(CultureInfo.CurrentCulture, "Strings.Error_InvalidUri_Arg1", new object[]
                    {
                        uri.AbsoluteUri
                    }));
                    //ILogger logger3 = this.logger;
                    //if (logger3 != null)
                    //{
                    //    logger3.WriteVerbose("The Uri is too short: {0}; {1}", new object[]
                    //    {
                    //        uri.AbsoluteUri,
                    //        ex2.Message
                    //    });
                    //}
                    throw ex2;
                }
            }
            Task<DownloadSummary> result;
            try
            {
                IDownloadEngine[] engines = this.GetSuitableEngines(this.defaultEngines, uri);
                result = Task.Factory.StartNew<DownloadSummary>(() => this.DownloadWithRetry(engines, uri, outputStream, progress, cancellationToken, downloadContext, verifySignature), cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            }
            catch (Exception ex3)
            {
                ex3.DebugThis();
                //ILogger logger4 = this.logger;
                //if (logger4 != null)
                //{
                //    logger4.WriteVerbose("Unable to get download engine: {0}", new object[]
                //    {
                //        ex3.Message
                //    });
                //}
                throw;
            }
            return result;
        }

        internal void RemoveAllEngines()
        {
            this.allEngines.Clear();
            this.defaultEngines.Clear();
        }

        internal void AddDownloadEngine(IDownloadEngine engine)
        {
            if (engine == null)
            {
                throw new ArgumentNullException("engine");
            }
            if (this.allEngines.Any((IDownloadEngine e) => string.Equals(e.Name, engine.Name, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException(string.Format("Engine {0} already exists.", engine.Name));
            }
            this.allEngines.Add(engine);
            this.defaultEngines.Add(engine);
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
        }

        private IEnumerable<IDownloadEngine> GetPreferredEngines(IEnumerable<IDownloadEngine> engines, IEnumerable<string> engineOrder)
        {
            if (engineOrder == null)
            {
                throw new ArgumentNullException("Invalid engine preference.");
            }
            if (engineOrder.Count<string>() > engines.Count<IDownloadEngine>())
            {
                throw new ArgumentException("Default engines can't be more than all available engines.");
            }
            List<IDownloadEngine> list = new List<IDownloadEngine>();
            using (IEnumerator<string> enumerator = engineOrder.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    string name = enumerator.Current;
                    IDownloadEngine downloadEngine = engines.FirstOrDefault((IDownloadEngine e) => string.Equals(e.Name, name, StringComparison.OrdinalIgnoreCase));
                    if (downloadEngine == null)
                        throw new InvalidOperationException(string.Format("Engine {0} not found among registered engines.", name));
                    list.Add(downloadEngine);
                }
            }
            return list;
        }

        private IDownloadEngine[] GetSuitableEngines(IEnumerable<IDownloadEngine> downloadEngines, Uri uri)
        {
            DownloadSource source = (uri.IsFile || uri.IsUnc) ? DownloadSource.File : DownloadSource.Internet;
            IDownloadEngine[] array = (from e in downloadEngines
                                       where e.IsSupported(source)
                                       select e).ToArray<IDownloadEngine>();
            if (array.Length == 0)
            {
                //ILogger logger = this.logger;
                //if (logger != null)
                //{
                //    logger.WriteVerbose("Unable to select suitable download engine.", new object[0]);
                //}
                throw new NoSuitableEngineException("Strings.Error_NoSuitableEngineFound");
            }
            return array;
        }

        private DownloadSummary DownloadWithRetry(IDownloadEngine[] engines, Uri uri, Stream outputStream, ProgressUpdateCallback progress, CancellationToken cancellationToken, DownloadContext downloadContext = null, bool verifySignature = false)
        {
            //Dictionary<string, object> dictionary = new Dictionary<string, object>
            //{
            //    {
            //        TelemetryConstants.DOWNLOADURLPROPERTY,
            //        uri
            //    }
            //};
            //if (((downloadContext != null) ? downloadContext.PackageIdentity : null) != null)
            //{
            //    ITelemetry telemetry = this.telemetry;
            //    if (telemetry != null)
            //    {
            //        telemetry.GetContextProperties((downloadContext != null) ? downloadContext.PackageIdentity : null, dictionary);
            //    }
            //}
            //if (!string.IsNullOrEmpty((downloadContext != null) ? downloadContext.TelemetryParentCorrelation : null))
            //{
            //    dictionary.Add(TelemetryConstants.PARENTSPROPERTY, (downloadContext != null) ? downloadContext.TelemetryParentCorrelation : null);
            //}
            //if (!string.IsNullOrEmpty((downloadContext != null) ? downloadContext.TelemetryAncestorWorkloads : null))
            //{
            //    dictionary.Add(TelemetryConstants.ANCESTORWORKLOADSPROPERTY, (downloadContext != null) ? downloadContext.TelemetryAncestorWorkloads : null);
            //}
            //dictionary.Add(TelemetryConstants.ENGINEOPERATIONTYPE, "Download-Package");
            DownloadSummary downloadSummary = new DownloadSummary();
            //ITelemetry telemetry2 = this.telemetry;
            //using (ITelemetryOperation telemetryOperation = (telemetry2 != null) ? telemetry2.StartOperation(TelemetryConstants.PACKAGEOPERATIONEVENT, dictionary, false, false) : null)
            //{
            DateTime now = DateTime.Now;
            string text = string.Empty;
            List<DownloadFailureInformation> list = new List<DownloadFailureInformation>();
            for (int i = 0; i < engines.Length; i++)
            {
                DateTime now2 = DateTime.Now;
                long position = outputStream.Position;
                long length = outputStream.Length;
                IDownloadEngine engine = engines[i];
                text = text + engine.Name + ";";
                try
                {
                    //ILogger logger = this.logger;
                    //if (logger != null)
                    //{
                    //    logger.WriteVerbose("Attempting download '{0}' using engine '{1}'", new object[]
                    //    {
                    //        uri.AbsoluteUri,
                    //        engine.Name
                    //    });
                    //}
                    downloadSummary = engine.Download(uri, outputStream, delegate (ProgressUpdateStatus status)
                    {
                        ProgressUpdateCallback progress2 = progress;
                        if (progress2 == null)
                        {
                            return;
                        }
                        progress2(new ProgressUpdateStatus(engine.Name, status.BytesRead, status.TotalBytes, status.BitRate));
                    }, cancellationToken, downloadContext);
                    if (outputStream.Length == 0L)
                    {
                        //string resourceId = "Error_InvalidSignatureOnDownloadEmptyFile_Arg1";
                        //InvalidSignatureException ex = new InvalidSignatureException(VerificationResult.InvalidSignature, Strings.ResourceManager, resourceId, new object[]
                        //{
                        //    uri
                        //})
                        //{
                        //    ShouldLog = false
                        //};
                        //ILogger logger2 = this.logger;
                        //if (logger2 != null)
                        //{
                        //    logger2.WriteError(ex, ex.Message, new object[0]);
                        //}
                        //Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                        //string faultlocationproperty = TelemetryConstants.FAULTLOCATIONPROPERTY;
                        //dictionary2[faultlocationproperty] = "PostDownloadEngineDownload";
                        //string faulttypeproperty = TelemetryConstants.FAULTTYPEPROPERTY;
                        //dictionary2[faulttypeproperty] = "EmptyFile";
                        //string enginepackagefailuretype = TelemetryConstants.ENGINEPACKAGEFAILURETYPE;
                        //dictionary2[enginepackagefailuretype] = "download-failure";
                        //Dictionary<string, object> properties = dictionary2;
                        //ITelemetry telemetry3 = this.telemetry;
                        //if (telemetry3 != null)
                        //{
                        //    telemetry3.WriteFault(TelemetryConstants.PACKAGEFAILUREEVENT, string.Format("An empty file was downloaded from {0}", engine.Name), properties, null, null, false, null, false, null, false);
                        //}
                        var ex = new InvalidDataException("Invalid signature on download an empty file");
                        throw ex;
                    }
                    if (verifySignature)
                    {
                        //if (this.verifier == null)
                        //{
                        //    this.verifier = this.serviceProvider.GetService(true);
                        //}
                        //VerificationContext verificationContext = new VerificationContext
                        //{
                        //    PackageIdentity = ((downloadContext != null) ? downloadContext.PackageIdentity : null),
                        //    TelemetryParentCorrelation = ((downloadContext != null) ? downloadContext.TelemetryParentCorrelation : null),
                        //    TelemetryAncestorWorkloads = ((downloadContext != null) ? downloadContext.TelemetryAncestorWorkloads : null),
                        //    Sha256 = ((downloadContext != null) ? downloadContext.Sha256 : null)
                        //};
                        //VerificationInformation verificationInformation = this.verifier.Verify(outputStream, null, verificationContext);
                        //if (verificationInformation == null || verificationInformation.Result > VerificationResult.Success)
                        //{
                        //    VerificationResult result = (verificationInformation != null) ? verificationInformation.Result : VerificationResult.Exception;
                        //    InvalidSignatureException ex2 = InvalidSignatureException.TryCreate(verificationInformation);
                        //    if (ex2 == null)
                        //    {
                        //        string resourceId2 = "Error_InvalidSignatureOnDownload_Arg1";
                        //        ex2 = new InvalidSignatureException(result, Strings.ResourceManager, resourceId2, new object[]
                        //        {
                        //            uri
                        //        });
                        //    }
                        //    ex2.ShouldLog = false;
                        //ILogger logger3 = this.logger;
                        //if (logger3 != null)
                        //{
                        //    logger3.WriteError(ex2, ex2.Message, new object[0]);
                        //}
                        var ex2 = new NotImplementedException("Invalid signature on download");
                        throw ex2;
                        //}
                    }
                    //ILogger logger4 = this.logger;
                    //if (logger4 != null)
                    //{
                    //    logger4.WriteMessage("Download of '{0}' succeeded using engine '{1}'", new object[]
                    //    {
                    //        uri.AbsoluteUri,
                    //        engine.Name
                    //    });
                    //}
                    //TimeSpan timeSpan = DateTime.Now - now;
                    //if (telemetryOperation != null)
                    //{
                    //    telemetryOperation.Properties.Add(TelemetryConstants.DOWNLOADSIZEPROPERTY, downloadSummary.DownloadedSize);
                    //    telemetryOperation.Properties.Add(string.Format("{0}{1}MS", TelemetryConstants.DOWNLOADENGINETIME, engine.Name), downloadSummary.DownloadTime.TotalMilliseconds);
                    //    telemetryOperation.Properties.Add(TelemetryConstants.DOWNLOADTIMEPROPERTY, timeSpan.TotalMilliseconds);
                    //    telemetryOperation.Properties.Add(TelemetryConstants.DOWNLOADENGINESUSED, text);
                    //    telemetryOperation.Properties.Add(TelemetryConstants.DOWNLOADPROXYSETTING, downloadSummary.ProxyResolution);
                    //    telemetryOperation.Properties.Add(TelemetryConstants.DOWNLOADPFINALURI, downloadSummary.FinalUri);
                    //    telemetryOperation.RecordSuccess("Success");
                    //}
                    downloadSummary.DownloadEngine = engine.Name;
                    return downloadSummary;
                }
                catch (OperationCanceledException)
                {
                    //if (telemetryOperation != null)
                    //{
                    //    TimeSpan timeSpan2 = DateTime.Now - now;
                    //    TimeSpan timeSpan3 = DateTime.Now - now2;
                    //    telemetryOperation.Properties.Add(string.Format("{0}{1}MS", TelemetryConstants.DOWNLOADENGINETIME, engine.Name), timeSpan3.TotalMilliseconds);
                    //    telemetryOperation.Properties.Add(TelemetryConstants.DOWNLOADTIMEPROPERTY, timeSpan2.TotalMilliseconds);
                    //    telemetryOperation.RecordCancel(string.Format("User canceled during package {0}", "download"));
                    //}
                    throw;
                }
                catch (Exception ex3)
                {
                    list.Add(new DownloadFailureInformation
                    {
                        Exception = ex3,
                        Engine = engine.Name
                    });
                    //ILogger logger5 = this.logger;
                    //if (logger5 != null)
                    //{
                    //    logger5.WriteVerbose("Download failed using {0} engine. {1}", new object[]
                    //    {
                    //        engine.Name,
                    //        ex3
                    //    });
                    //}
                    //if (telemetryOperation != null)
                    //{
                    //    TimeSpan timeSpan4 = DateTime.Now - now2;
                    //    telemetryOperation.Properties.Add(string.Format("{0}{1}MS", TelemetryConstants.DOWNLOADENGINETIME, engine.Name), timeSpan4.TotalMilliseconds);
                    //}
                    //if (i == engines.Length - 1)
                    //{
                    //    TimeSpan timeSpan5 = DateTime.Now - now;
                    //    if (telemetryOperation != null)
                    //    {
                    //        telemetryOperation.Properties.Add(TelemetryConstants.DOWNLOADTIMEPROPERTY, timeSpan5.TotalMilliseconds);
                    //        telemetryOperation.RecordFailure("Payload failed to download.", "Payload failed to download.");
                    //    }
                    //    throw new DownloadFailureException(list)
                    //    {
                    //        ShouldLog = false
                    //    };
                    //}
                    outputStream.SetLength(length);
                    outputStream.Seek(position, SeekOrigin.Begin);
                    int num = (i + 1) * this.SleepDurationBetweenRetries;
                    //ILogger logger6 = this.logger;
                    //if (logger6 != null)
                    //{
                    //    logger6.WriteVerbose(string.Format("Sleeping {0} milliseconds before retrying download.", num), new object[0]);
                    //}
                    Thread.Sleep(num);
                }
            }
            //}
            return null;
        }

        private static readonly int DefaultSleepDuration = 5000;

        //private readonly IServiceProvider serviceProvider;

        //private readonly ILogger logger;

        //private readonly ITelemetry telemetry;

        private readonly List<IDownloadEngine> allEngines = new List<IDownloadEngine>();

        private readonly List<IDownloadEngine> defaultEngines = new List<IDownloadEngine>();

        private int maxDownloads = 100;

        //private ISignatureVerifierManager verifier;
    }
}