#if NETFX_45

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Support.Threading
{
    public struct FuncAwaiter<TResult> : IAwaiter<TResult>
    {
        private readonly Task<TResult> task;

        public FuncAwaiter(Func<TResult> function)
        {
            this.task = new Task<TResult>(function);
            this.task.Start();
        }

        bool IAwaiter<TResult>.IsCompleted
        {
            get
            {
                return this.task.IsCompleted;
            }
        }

        TResult IAwaiter<TResult>.GetResult()
        {
            return this.task.Result;
        }

        void OnCompleted(Action continuation)
        {
            new Task(continuation).Start();
        }
    }
}

#endif