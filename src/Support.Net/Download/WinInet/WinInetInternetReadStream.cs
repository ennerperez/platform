using Platform.Support.Windows;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace Platform.Support.Net.Download.WinInet
{
    internal class WinInetInternetReadStream : Stream
    {
        internal WinInetInternetReadStream(IntPtr inetFile)
        {
            this.helper = new Utilities();
            this.inetFile = inetFile;
            this.localBuffer = new byte[32768];
            this.contentLength = this.helper.GetContentLength(inetFile);
        }

        //internal WinInetInternetReadStream(IServiceProvider serviceProvider, IntPtr inetFile)
        //{
        //    this.serviceProvider = serviceProvider;
        //    this.logger = this.serviceProvider.GetService(false);
        //    this.helper = new DownloadHelpers(this.serviceProvider);
        //    this.inetFile = inetFile;
        //    this.localBuffer = new byte[32768];
        //    this.contentLength = this.helper.GetContentLength(inetFile);
        //}

        public override bool CanRead
        {
            get
            {
                return true;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return false;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return false;
            }
        }

        public override long Length
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public override long Position
        {
            get
            {
                throw new NotSupportedException();
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public override void Flush()
        {
            throw new NotSupportedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
                throw new ArgumentNullException("buffer");
            if (buffer.Length < offset + count)
                throw new ArgumentOutOfRangeException("count");
            int dwNumberOfBytesToRead = System.Math.Min(32768, count);
            int num;
            if (!Windows.WinInet.InternetReadFile(this.inetFile, this.localBuffer, dwNumberOfBytesToRead, out num))
                this.helper.ThrowGetLastErrorException("InternetReadFile");
            Array.Copy(this.localBuffer, 0, buffer, offset, num);
            return num;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                this.localBuffer = null;
            base.Dispose(disposing);
        }

        //private readonly IServiceProvider serviceProvider;

        //private readonly ILogger logger;

        private readonly Utilities helper;

        private readonly int contentLength;

        private readonly IntPtr inetFile;

        private byte[] localBuffer;
    }
}