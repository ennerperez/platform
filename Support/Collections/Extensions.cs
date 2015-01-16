using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Support.Collections
{
    public static class Extensions
    {

        public static void RemoveAt<T>(ref T[] source, int index)
        {
            T[] aux = source;
            source = source.Where(c => Array.IndexOf(aux, c) != index).ToArray();
        }
        public static void Clear(ref Array source)
        {
            Array.Clear(source, 0, source.Length);
        }
        
        public static void Add<T>(ref T[] source, T item)
        {
            Array.Resize<T>(ref source, source.Length);
            source[source.Length - 1] = item;
        }
        
        public static int IndexOf<T>(this IEnumerable<T> obj, T value)
        {
            return IndexOf(obj, value, EqualityComparer<T>.Default);
        }
        public static int IndexOf<T>(this IEnumerable<T> obj, T value, IEqualityComparer<T> comparer)
        {
            int index = 0;
            foreach (T item in obj)
            {
                if (comparer.Equals(item, value))
                {
                    return index;
                }
                System.Math.Max(System.Threading.Interlocked.Increment(ref index), index - 1);
            }
            return -1;
        }
        public static string Join(this IEnumerable<string> source, string sep)
        {
            return string.Join(sep, source);
        }
        public static string Join(this string[] source, string sep)
        {
            return string.Join(sep, source);
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                System.Math.Max(System.Threading.Interlocked.Decrement(ref n), n + 1);
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

    }
}
