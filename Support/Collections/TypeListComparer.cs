using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Support
{
#if PORTABLE
    namespace Core
    {
#endif
        namespace Collections
        {
            public class TypeListComparer<T> : IEqualityComparer<IEnumerable<T>> where T : class
            {
                public bool Equals(IEnumerable<T> x, IEnumerable<T> y)
                {
                    return x.SetEqual(y);
                }
                public int GetHashCode(IEnumerable<T> obj)
                {
                    if (obj == null)
                        throw new ArgumentNullException("obj");

                    int? num = obj.Aggregate(null, (int? current, T o) => new int?((!current.HasValue) ? o.GetHashCode() : (current.Value | o.GetHashCode())));
                    int? num2 = num;
                    if (!num2.HasValue)
                        return 0;

                    return num2.GetValueOrDefault();
                }
            }
        }
#if PORTABLE
    }
#endif
}
