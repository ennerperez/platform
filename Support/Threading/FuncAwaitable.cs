#if NETFX_45

using Platform.Support.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Platform.Support.Extensions
{

    internal struct FuncAwaitable<TResult> : IAwaitable<TResult>
    {
        private readonly Func<TResult> function;

        public FuncAwaitable(Func<TResult> function)
        {
            this.function = function;
        }

        public IAwaiter<TResult> GetAwaiter()
        {
            return new FuncAwaiter<TResult>(this.function);
        }
    }
        
}

#endif