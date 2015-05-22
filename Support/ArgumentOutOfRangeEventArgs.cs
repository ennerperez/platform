using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Support
{

    public delegate void ArgumentOutOfRangeEventHandler<T>(object sender, ArgumentOutOfRangeEventArgs<T> e);
    public class ArgumentOutOfRangeEventArgs<T> : EventArgs
    {

        public ArgumentOutOfRangeEventArgs() : base() { }

        public ArgumentOutOfRangeEventArgs(T value, T min, T max)
            : this()
        {
            this.Value = value;
            this.Min = min;
            this.Max = max;
        }

        public T Value { get; set; }
        public T Min { get; set; }
        public T Max { get; set; }
        
    }


    public delegate void ArgumentOutOfRangeEventHandler(object sender, ArgumentOutOfRangeEventArgs<Object> e);
    public class ArgumentOutOfRangeEventArgs : ArgumentOutOfRangeEventArgs<Object>
    {

        public ArgumentOutOfRangeEventArgs() : base() { }

        public ArgumentOutOfRangeEventArgs(Object value, Object min, Object max) : base(value, min, max) { }

    }

}
