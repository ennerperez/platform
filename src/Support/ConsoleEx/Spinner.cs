#if !PORTABLE

using System;
using System.Threading;

namespace Platform.Support.ConsoleEx
{
    public class Spinner : IDisposable
    {
        private int counter;

        public void Turn()
        {
            counter++;
            switch (counter % 4)
            {
                case 0: System.Console.Write("/"); counter = 0; break;
                case 1: System.Console.Write("-"); break;
                case 2: System.Console.Write("\\"); break;
                case 3: System.Console.Write("|"); break;
            }
            Thread.Sleep(100);
            System.Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
        }

        private static bool busy = true;

        public static void StartSpinner()
        {
            var t = new ThreadStart(() =>
            {
                using (var spin = new Spinner())
                {
                    while (busy)
                    {
                        spin.Turn();
                    }
                }
            });

            t.Invoke();
        }

        public static void StopSpinner()
        {
            busy = false;
            Thread.CurrentThread.Join();
        }

        #region IDisposable

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // NOTE: Leave out the finalizer altogether if this class doesn't
        // own unmanaged resources itself, but leave the other methods
        // exactly as they are.
        ~Spinner()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    disposed = true;
                }
            }
        }

        #endregion IDisposable
    }
}

#endif