using Platform.Support.IO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace Platform.Support.Net.Download.Bits
{
    [CLSCompliant(false)]
    internal class BitsJob : DisposableObject, IBackgroundCopyCallback
    {
        public BitsJob(string displayName, IBackgroundCopyJob job)
        {
            this.displayName = displayName;
            this.nativeJob = job;
        }

        //public BitsJob(IServiceProvider serviceProvider, string displayName, IBackgroundCopyJob job)
        //{
        //    this.serviceProvider = serviceProvider;
        //    this.displayName = displayName;
        //    this.nativeJob = job;
        //    this.log = this.serviceProvider.GetService(false);
        //}

        public static BitsJob CreateJob(IBackgroundCopyManager backgroundCopyManager, Uri uri, string filePath, string cookie)
        {
            IBackgroundCopyJob backgroundCopyJob = null;
            string text = "VsBitsDownloadJob - " + uri.GetHashCode();
            try
            {
#if NETFX_40 || NETFX_45
                Guid guid;
                backgroundCopyManager.CreateJob(text, BG_JOB_TYPE.BG_JOB_TYPE_DOWNLOAD, out guid, out backgroundCopyJob);
#else
                backgroundCopyManager.CreateJob(text, BG_JOB_TYPE.BG_JOB_TYPE_DOWNLOAD, out Guid guid, out backgroundCopyJob);
#endif
                backgroundCopyJob.SetNotifyFlags(11u);
                backgroundCopyJob.SetNoProgressTimeout(120u);
                backgroundCopyJob.SetPriority(BG_JOB_PRIORITY.BG_JOB_PRIORITY_FOREGROUND);
                backgroundCopyJob.SetProxySettings(BG_JOB_PROXY_USAGE.BG_JOB_PROXY_USAGE_AUTODETECT, null, null);
            }
            catch (COMException ex)
            {
                ex.DebugThis();
                //service.WriteVerbose("Failed to create job. {0}", new object[]
                //{
                //    ex.Message
                //});
                throw;
            }
            BitsJob bitsJob = new BitsJob(text, backgroundCopyJob);
            bitsJob.InitJob(uri.AbsoluteUri, filePath, cookie);
            return bitsJob;
        }

        //public static BitsJob CreateJob(IServiceProvider serviceProvider, IBackgroundCopyManager backgroundCopyManager, Uri uri, string filePath, string cookie)
        //{
        //    IBackgroundCopyJob backgroundCopyJob = null;
        //    ILogger service = serviceProvider.GetService(false);
        //    string text = "VsBitsDownloadJob - " + uri.GetHashCode();
        //    try
        //    {
        //        Guid guid;
        //        backgroundCopyManager.CreateJob(text, BG_JOB_TYPE.BG_JOB_TYPE_DOWNLOAD, out guid, out backgroundCopyJob);
        //        backgroundCopyJob.SetNotifyFlags(11u);
        //        backgroundCopyJob.SetNoProgressTimeout(120u);
        //        backgroundCopyJob.SetPriority(BG_JOB_PRIORITY.BG_JOB_PRIORITY_FOREGROUND);
        //        backgroundCopyJob.SetProxySettings(BG_JOB_PROXY_USAGE.BG_JOB_PROXY_USAGE_AUTODETECT, null, null);
        //    }
        //    catch (COMException ex)
        //    {
        //        service.WriteVerbose("Failed to create job. {0}", new object[]
        //        {
        //            ex.Message
        //        });
        //        throw;
        //    }
        //    BitsJob bitsJob = new BitsJob(serviceProvider, text, backgroundCopyJob);
        //    bitsJob.InitJob(uri.AbsoluteUri, filePath, cookie);
        //    return bitsJob;
        //}

        public void JobTransferred(IBackgroundCopyJob job)
        {
            try
            {
                this.UpdateProgress();
                this.UpdateJobState();
                this.CompleteOrCancel();
            }
            catch (Exception ex)
            {
                ex.DebugThis();
                //ILogger logger = this.log;
                //if (logger != null)
                //{
                //    logger.WriteVerbose("Failed to job transfer: {0}", new object[]
                //    {
                //        ex.Message
                //    });
                //}
            }
        }

        public void JobError(IBackgroundCopyJob job, IBackgroundCopyError error)
        {
            try
            {
                //ILogger logger = this.log;
                //if (logger != null)
                //{
                //    logger.WriteVerbose("Failed job: {0}", new object[]
                //    {
                //        this.displayName
                //    });
                //}
                this.UpdateJobState();
                BG_ERROR_CONTEXT errorContext = BG_ERROR_CONTEXT.BG_ERROR_CONTEXT_NONE;
                int returnCode = 0;
                this.Invoke(delegate
                {
                    error.GetError(out errorContext, out returnCode);
                }, "GetError", false);
                this.jobException = new IOException(string.Format(CultureInfo.CurrentCulture, "Strings.Error_BitsJobFailed_Arg2", new object[]
                {
                    errorContext,
                    returnCode
                }));
                this.CompleteOrCancel();
            }
            catch (Exception ex)
            {
                ex.DebugThis();
                //ILogger logger2 = this.log;
                //if (logger2 != null)
                //{
                //    logger2.WriteVerbose("Failed to handle job error: {0}", new object[]
                //    {
                //        ex.Message
                //    });
                //}
            }
        }

        public void JobModification(IBackgroundCopyJob job, uint reserved)
        {
            try
            {
                this.UpdateJobState();
                if (this.state == BG_JOB_STATE.BG_JOB_STATE_TRANSIENT_ERROR)
                {
                    this.resumeAttempts++;
                    if (this.resumeAttempts <= 10)
                    {
                        this.Resume();
                    }
                    else
                    {
                        //ILogger logger = this.log;
                        //if (logger != null)
                        //{
                        //    logger.WriteVerbose("Max resume attempts for job '{0}' exceeded. Canceling.", new object[]
                        //    {
                        //        this.displayName
                        //    });
                        //}
                        this.CompleteOrCancel();
                    }
                }
                else if (this.IsProgressingState(this.state))
                {
                    this.UpdateProgress();
                }
                else if (this.state == BG_JOB_STATE.BG_JOB_STATE_CANCELLED || this.state == BG_JOB_STATE.BG_JOB_STATE_ERROR)
                {
                    this.CompleteOrCancel();
                }
            }
            catch
            {
            }
        }

        public void Cancel()
        {
            //ILogger logger = this.log;
            //if (logger != null)
            //{
            //    logger.WriteVerbose("Canceling job {0}", new object[]
            //    {
            //        this.displayName
            //    });
            //}
            object obj = this.lockObj;
            lock (obj)
            {
                if (!this.isJobComplete)
                {
                    this.Invoke(delegate
                    {
                        this.nativeJob.Cancel();
                    }, "Bits Cancel", true);
                    this.jobException = new OperationCanceledException();
                    this.isJobComplete = true;
                }
            }
        }

        public void WaitForCompletion(ProgressUpdateCallback callback, CancellationToken cancellationToken)
        {
            try
            {
                this.UpdateJobState();
                while (this.IsProgressingState(this.state) || this.state == BG_JOB_STATE.BG_JOB_STATE_QUEUED || this.state == BG_JOB_STATE.BG_JOB_STATE_CONNECTING)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    Thread.Sleep(100);
                    this.UpdateJobState();
                    this.UpdateProgress();
                    if (callback != null && (this.state == BG_JOB_STATE.BG_JOB_STATE_TRANSFERRING || this.state == BG_JOB_STATE.BG_JOB_STATE_TRANSFERRED || this.state == BG_JOB_STATE.BG_JOB_STATE_ACKNOWLEDGED))
                    {
                        callback(new ProgressUpdateStatus((long)this.progress.BytesTransferred, (long)this.progress.BytesTotal, 0.0));
                    }
                }
            }
            finally
            {
                this.CompleteOrCancel();
            }
            if (this.jobException != null)
            {
                throw this.jobException;
            }
        }

        protected override void Dispose(bool isDisposing)
        {
            this.UpdateJobState();
            this.CompleteOrCancel();
            this.nativeJob = null;
            base.Dispose(isDisposing);
        }

        private void AddFile(string remoteUrl, string fileDestinationPath)
        {
            //ILogger logger = this.log;
            //if (logger != null)
            //{
            //    logger.WriteVerbose("Adding file '{0}': '{1}' to job '{2}'.", new object[]
            //    {
            //        remoteUrl,
            //        fileDestinationPath,
            //        this.displayName
            //    });
            //}
            this.Invoke(delegate
            {
                this.nativeJob.AddFile(remoteUrl, fileDestinationPath);
            }, "Bits AddJob", true);
        }

        private void CompleteOrCancel()
        {
            if (this.isJobComplete)
            {
                return;
            }
            object obj = this.lockObj;
            lock (obj)
            {
                if (!this.isJobComplete)
                {
                    if (this.state == BG_JOB_STATE.BG_JOB_STATE_TRANSFERRED)
                    {
                        //ILogger logger = this.log;
                        //if (logger != null)
                        //{
                        //    logger.WriteVerbose("Completing job '{0}'.", new object[]
                        //    {
                        //        this.displayName
                        //    });
                        //}
                        this.Invoke(delegate
                        {
                            this.nativeJob.Complete();
                        }, "Bits Complete", true);
                        while (this.state == BG_JOB_STATE.BG_JOB_STATE_TRANSFERRED)
                        {
                            Thread.Sleep(50);
                            this.UpdateJobState();
                        }
                    }
                    else
                    {
                        //ILogger logger2 = this.log;
                        //if (logger2 != null)
                        //{
                        //    logger2.WriteVerbose("Canceling job '{0}'.", new object[]
                        //    {
                        //        this.displayName
                        //    });
                        //}
                        this.Invoke(delegate
                        {
                            this.nativeJob.Cancel();
                        }, "Bits Cancel", true);
                    }
                    this.isJobComplete = true;
                }
            }
        }

        private void UpdateJobState()
        {
            IBackgroundCopyJob job = this.nativeJob;
            if (job != null)
            {
                this.Invoke(delegate
                {
                    job.GetState(out this.state);
                }, "GetState", true);
            }
        }

        private void UpdateProgress()
        {
            this.Invoke(delegate
            {
                this.nativeJob.GetProgress(out this.progress);
            }, "GetProgress", true);
        }

        private void Resume()
        {
            this.Invoke(delegate
            {
                this.nativeJob.Resume();
            }, "Bits Resume", true);
        }

        private bool IsProgressingState(BG_JOB_STATE state)
        {
            return state == BG_JOB_STATE.BG_JOB_STATE_CONNECTING || state == BG_JOB_STATE.BG_JOB_STATE_TRANSIENT_ERROR || state == BG_JOB_STATE.BG_JOB_STATE_TRANSFERRING;
        }

        private void Invoke(Action func, string displayName, bool throwOnFailure = true)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }
            try
            {
                func();
            }
            catch (Exception ex)
            {
                ex.DebugThis();
                //ILogger logger = this.log;
                //if (logger != null)
                //{
                //    logger.WriteVerbose("{0} failed. {1}", new object[]
                //    {
                //        displayName,
                //        ex
                //    });
                //}
                if (throwOnFailure)
                {
                    throw;
                }
            }
        }

        private void InitJob(string remoteUrl, string filePath, string cookie)
        {
            this.nativeJob.AddFile(remoteUrl, filePath);
            IBackgroundCopyJobHttpOptions backgroundCopyJobHttpOptions = this.nativeJob as IBackgroundCopyJobHttpOptions;
            if (backgroundCopyJobHttpOptions == null)
            {
                throw new InvalidOperationException("Strings.Error_BitsJobFailed");
            }
            BG_HTTP_SECURITY_FLAGS securityFlags = BG_HTTP_SECURITY_FLAGS.BG_HTTP_REDIRECT_POLICY_ALLOW_HTTPS_TO_HTTP;
            backgroundCopyJobHttpOptions.SetSecurityFlags(securityFlags);
            if (!string.IsNullOrEmpty(cookie))
            {
                cookie = "Cookie: " + cookie;
                backgroundCopyJobHttpOptions.SetCustomHeaders(cookie);
            }
            this.nativeJob.SetNotifyInterface(this);
            this.Resume();
        }

        private const int BitsEngineNoProgressTimeout = 120;

        private const int MaxResumeAttempts = 10;

        //private readonly IServiceProvider serviceProvider;
#pragma warning disable CS0169
        private readonly string displayName;
#pragma warning restore CS0169

        //private readonly ILogger log;

        private readonly object lockObj = new object();

        private IBackgroundCopyJob nativeJob;

        private Exception jobException;

        private _BG_JOB_PROGRESS progress;

        private BG_JOB_STATE state;

        private bool isJobComplete;

        private int resumeAttempts;
    }
}