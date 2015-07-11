using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
#if !PORTABLE
using System.Data;
using Platform.Support.Reflection;
#endif

namespace Platform.Support.Collections
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

#if !PORTABLE
        
        public static ICollection<T> Clone<T>(this ICollection<T> items) where T : ICloneable
        {
            ICollection<T> _return = Activator.CreateInstance<ICollection<T>>();
            foreach (T item in items)
            {
                _return.Add(item.Clone<T>());
            }
            return _return;
        }

        public static DataTable ToDataTable<T>(this ICollection<T> items)
        {
            return items.AsEnumerable<T>().ToDataTable<T>();
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                tb.Columns.Add(prop.Name, prop.PropertyType);
            }

            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (var i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }

        public static DataTable ToLinkedDataTable<T>(this ICollection<T> items, string propertyName = null)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                tb.Columns.Add(prop.Name, prop.PropertyType);
            }

            if (propertyName == null) propertyName = typeof(T).ToString();

            tb.Columns.Add(propertyName, typeof(T));

            foreach (var item in items)
            {
                var values = new object[props.Length+1];
                for (var i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                values[props.Length] = item;

                tb.Rows.Add(values);
            }

            return tb;
        }

#endif


        public static void AddRange<T>(this ICollection<T> target, IEnumerable<T> items)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }
            foreach (T current in items)
            {
                target.Add(current);
            }
        }
        public static TValue GetOrCreateValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> createValueCallback)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary");
            }
            if (!dictionary.ContainsKey(key))
            {
                lock (dictionary)
                {
                    if (!dictionary.ContainsKey(key))
                    {
                        dictionary[key] = createValueCallback();
                    }
                }
            }
            return dictionary[key];
        }
        public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            return source.ToDictionary((KeyValuePair<TKey, TValue> m) => m.Key, (KeyValuePair<TKey, TValue> m) => m.Value);
        }
        public static bool Empty<T>(this IEnumerable<T> source)
        {
            return !source.Any<T>();
        }
        public static bool SetEqual<T>(this IEnumerable<T> x, IEnumerable<T> y)
        {
            if (x == null)
            {
                throw new ArgumentNullException("x");
            }
            if (y == null)
            {
                throw new ArgumentNullException("y");
            }
            List<T> list = x.ToList<T>();
            List<T> list2 = y.ToList<T>();
            if (list.Count<T>() != list2.Count<T>())
            {
                return false;
            }
            foreach (T current in list2)
            {
                if (!list.Contains(current))
                {
                    return false;
                }
                list.Remove(current);
            }
            return list.Empty<T>();
        }

    }
}
