namespace Platform.Support
{
#if PORTABLE

    namespace Core
    {
#endif

        public static class Library
        {
#if PORTABLE
            private const bool isPortable = true;
#else
        private const bool isPortable = false;
#endif

            public static bool IsPortable()
            {
                return isPortable;
            }
        }

#if PORTABLE
    }

#endif
}