#if DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;

#if !NETFX_45
namespace System
{

    using System.Runtime.InteropServices;

#pragma warning disable 0436
    internal interface IProgress<T>
    {
        void Report(T value);
    }

    public enum FileType : uint
    {
        FILE_TYPE_UNKNOWN = 0x0000,
        FILE_TYPE_DISK = 0x0001,
        FILE_TYPE_CHAR = 0x0002,
        FILE_TYPE_PIPE = 0x0003,
        FILE_TYPE_REMOTE = 0x8000,
    }

    public enum STDHandle : uint
    {
        STD_INPUT_HANDLE = unchecked((uint)-10),
        STD_OUTPUT_HANDLE = unchecked((uint)-11),
        STD_ERROR_HANDLE = unchecked((uint)-12),
    }

    internal static class Helpers
    {

        [DllImport("Kernel32.dll")]
        static public extern UIntPtr GetStdHandle(STDHandle stdHandle);
        [DllImport("Kernel32.dll")]
        static public extern FileType GetFileType(UIntPtr hFile);

        static public bool IsOutputRedirected()
        {
            UIntPtr hOutput = GetStdHandle(STDHandle.STD_OUTPUT_HANDLE);
            FileType fileType = (FileType)GetFileType(hOutput);
            if (fileType == FileType.FILE_TYPE_CHAR)
                return false;
            return true;
        }

    }

}

#endif

namespace Platform.Support.ConsoleEx
{

    public class Spinner : IDisposable
    {
        private int counter;
                
        public void Dispose()
        {
        }

        public void Turn()
        {
            counter++;
            switch (counter % 4)
            {
                case 0: Console.Write("/"); counter = 0; break;
                case 1: Console.Write("-"); break;
                case 2: Console.Write("\\"); break;
                case 3: Console.Write("|"); break;
            }
            Thread.Sleep(100);
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
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

    }

    /// <summary>
    /// An ASCII progress bar
    /// Author: DanielSWolf
    /// Ref: https://gist.github.com/DanielSWolf/0ab6a96899cc5377bf54
    /// </summary>
    public class ProgressBar : IDisposable, IProgress<double>
    {
        private const int blockCount = 10;
        private readonly TimeSpan animationInterval = TimeSpan.FromSeconds(1.0 / 8);
        private const string animation = @"|/-\";

        private readonly Timer timer;

        private double currentProgress = 0;
        private string currentText = string.Empty;
        private bool disposed = false;
        private int animationIndex = 0;

        public ProgressBar()
        {
            timer = new Timer(TimerHandler);

            // A progress bar is only for temporary display in a console window.
            // If the console output is redirected to a file, draw nothing.
            // Otherwise, we'll end up with a lot of garbage in the target file.
#if !NETFX_45
            if (!System.Helpers.IsOutputRedirected())
#else
            if (!Console.IsOutputRedirected)
#endif
                ResetTimer();
        }

        public void Report(double value)
        {
            // Make sure value is in [0..1] range
            value = Math.Max(0, Math.Min(1, value));
            Interlocked.Exchange(ref currentProgress, value);
        }

        private void TimerHandler(object state)
        {
            lock (timer)
            {
                if (disposed) return;

                int progressBlockCount = (int)(currentProgress * blockCount);
                int percent = (int)(currentProgress * 100);
                string text = string.Format("[{0}{1}] {2,3}% {3}",
                    new string('#', progressBlockCount), new string('-', blockCount - progressBlockCount),
                    percent,
                    animation[animationIndex++ % animation.Length]);
                UpdateText(text);

                ResetTimer();
            }
        }

        private void UpdateText(string text)
        {
            // Get length of common portion
            int commonPrefixLength = 0;
            int commonLength = Math.Min(currentText.Length, text.Length);
            while (commonPrefixLength < commonLength && text[commonPrefixLength] == currentText[commonPrefixLength])
            {
                commonPrefixLength++;
            }

            // Backtrack to the first differing character
            StringBuilder outputBuilder = new StringBuilder();
            outputBuilder.Append('\b', currentText.Length - commonPrefixLength);

            // Output new suffix
            outputBuilder.Append(text.Substring(commonPrefixLength));

            // If the new text is shorter than the old one: delete overlapping characters
            int overlapCount = currentText.Length - text.Length;
            if (overlapCount > 0)
            {
                outputBuilder.Append(' ', overlapCount);
                outputBuilder.Append('\b', overlapCount);
            }

            Console.Write(outputBuilder);
            currentText = text;
        }

        private void ResetTimer()
        {
            timer.Change(animationInterval, TimeSpan.FromMilliseconds(-1));
        }

#if DEBUG

        public static void DrawProgressBar(long progress, long total)
        {
            //draw empty progress bar
            Console.CursorLeft = 0;
            Console.Write("["); //start
            Console.CursorLeft = 32;
            Console.Write("]"); //end
            Console.CursorLeft = 1;
            float onechunk = 30.0f / total;

            //draw filled part
            int position = 1;
            for (int i = 0; i < onechunk * progress; i++)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.CursorLeft = position++;
                Console.Write(" ");
            }

            //draw unfilled part
            for (int i = position; i <= 31; i++)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.CursorLeft = position++;
                Console.Write(" ");
            }

            //draw totals
            Console.CursorLeft = 35;
            Console.BackgroundColor = ConsoleColor.Black;
            //Console.Write(progress.ToString() + " of " + total.ToString() + "    "); //blanks at the end remove any excess
        }

#endif

        public void Dispose()
        {
            lock (timer)
            {
                disposed = true;
                UpdateText(string.Empty);
            }
        }

    }
}

#endif