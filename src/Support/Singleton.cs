namespace Platform.Support
{
#if PORTABLE

    namespace Core
    {
#endif

        /// <summary>
        /// Implements singleton pattern
        /// </summary>
        /// <typeparam name="T">Class type</typeparam>
        public class Singleton<T> where T : class, new()
        {
            private static object _syncobj = new object();
            private static volatile T _instance = null;

            public static T Instance
            {
                get
                {
                    if (_instance == null)
                        lock (_syncobj)
                            if (_instance == null)
                                _instance = new T();
                    return _instance;
                }
            }

            public Singleton()
            { }
        }

#if PORTABLE
    }

#endif
}