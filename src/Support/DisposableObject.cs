using System;

namespace Platform.Support
{
#if PORTABLE

    namespace Core
    {
#endif

    public abstract class DisposableObject : IDisposable
    {
        public bool IsDisposed { get; private set; }

        public void Dispose()
        {
            if (this.IsDisposed)
                return;
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            IsDisposed = true;
        }

        protected void VerifyNotDisposed()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().FullName);
        }
    }

#if PORTABLE
    }
#endif
}