using System;

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

        public static UInt64 ReverseBytes(UInt64 value)
        {
            return (value & 0x00000000000000FFUL) << 56 | (value & 0x000000000000FF00UL) << 40 |
                (value & 0x0000000000FF0000UL) << 24 | (value & 0x00000000FF000000UL) << 8 |
                (value & 0x000000FF00000000UL) >> 8 | (value & 0x0000FF0000000000UL) >> 24 |
                (value & 0x00FF000000000000UL) >> 40 | (value & 0xFF00000000000000UL) >> 56;
        }
    }

#if PORTABLE
    }

#endif
}