#if NETFX_45

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Support
{
#if PORTABLE

    namespace Core
    {
#endif

        namespace Threading
        {
            //public interface IAwaitable
            //{
            //    IAwaiter GetAwaiter();
            //}

            //public interface IAwaiter : INotifyCompletion // or ICriticalNotifyCompletion
            //{
            //    // INotifyCompletion has one method: void OnCompleted(Action continuation);

            //    // ICriticalNotifyCompletion implements INotifyCompletion,
            //    // also has this method: void UnsafeOnCompleted(Action continuation);

            //    bool IsCompleted { get; }

            //    void GetResult();
            //}

            public interface IAwaitable<out TResult>
            {
                IAwaiter<TResult> GetAwaiter();
            }

            public interface IAwaiter<out TResult> : INotifyCompletion // or ICriticalNotifyCompletion
            {
                bool IsCompleted { get; }

                TResult GetResult();
            }
        }
    }

#if PORTABLE
}

#endif

#endif