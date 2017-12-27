#if !PORTABLE

using System;
using System.Security.Cryptography;

namespace Platform.Support.Security.Cryptography
{
    public class CRC32 : SHA1
    {
        public const UInt32 DefaultPolynomial = 0xedb88320u;

        public const UInt32 DefaultSeed = 0xffffffffu;
        private UInt32 _hash;
        private UInt32 _seed;
        private UInt32[] _table;

        private static UInt32[] defaultTable;

        public CRC32()
        {
            _table = InitializeTable(DefaultPolynomial);
            _seed = DefaultSeed;
            _hash = _seed;
        }

        public CRC32(UInt32 polynomial, UInt32 seed)
        {
            _table = InitializeTable(polynomial);
            this._seed = seed;
            _hash = _seed;
        }

        public override void Initialize()
        {
        }

        protected override void HashCore(byte[] buffer, int start, int length)
        {
            _hash = CalculateHash(_table, _hash, buffer, start, length);
        }

        protected override byte[] HashFinal()
        {
            byte[] hashBuffer = UInt32ToBigEndianBytes(_hash);
            this.HashValue = hashBuffer;
            return hashBuffer;
        }

        public override int HashSize
        {
            get { return 32; }
        }

        public static UInt32 Compute(byte[] buffer)
        {
            return CalculateHash(InitializeTable(DefaultPolynomial), DefaultSeed, buffer, 0, buffer.Length);
        }

        public static UInt32 Compute(UInt32 seed, byte[] buffer)
        {
            return CalculateHash(InitializeTable(DefaultPolynomial), seed, buffer, 0, buffer.Length);
        }

        public static UInt32 Compute(UInt32 polynomial, UInt32 seed, byte[] buffer)
        {
            return CalculateHash(InitializeTable(polynomial), seed, buffer, 0, buffer.Length);
        }

        private static UInt32[] InitializeTable(UInt32 polynomial)
        {
            if (polynomial == DefaultPolynomial && defaultTable != null)
            {
                return defaultTable;
            }

            UInt32[] createTable = new UInt32[256];
            int i = 0;
            while (i < 256)
            {
                UInt32 entry = (UInt32)i;
                int j = 0;
                while (j < 8)
                {
                    if ((entry & 1) == 1)
                    {
                        entry = (entry >> 1) ^ polynomial;
                    }
                    else
                    {
                        entry = entry >> 1;
                    }
                    System.Math.Max(System.Threading.Interlocked.Increment(ref j), j - 1);
                }
                createTable[i] = entry;
                System.Math.Max(System.Threading.Interlocked.Increment(ref i), i - 1);
            }

            if (polynomial == DefaultPolynomial)
            {
                defaultTable = createTable;
            }

            return createTable;
        }

        private static UInt32 CalculateHash(UInt32[] table, UInt32 seed, byte[] buffer, int start, int size)
        {
            UInt32 crc = seed;
            int i = start;
            while (i < size)
            {
                crc = (crc >> 8) ^ table[buffer[i] ^ crc & 0xff];

                System.Math.Max(System.Threading.Interlocked.Increment(ref i), i - 1);
            }
            return crc;
        }

        private static new byte[] ComputeHash(System.IO.Stream source)
        {
            var algoritm = CRC32.Create();
            return algoritm.ComputeHash(source);
        }

        private byte[] UInt32ToBigEndianBytes(UInt32 x)
        {
            return new byte[] {
                (byte)((x >> 24) & 0xff),
                (byte)((x >> 16) & 0xff),
                (byte)((x >> 8) & 0xff),
                (byte)(x & 0xff)
            };
        }

        public static string Get(string source)
        {
            return Get(new System.IO.FileInfo(source));
        }

        public static string Get(System.IO.Stream source)
        {
            String hash = String.Empty;
            foreach (byte b in CRC32.ComputeHash(source))
                hash += b.ToString("x2").ToLower();

            return hash;
        }

        public static string Get(System.IO.FileInfo source)
        {
            String hash = String.Empty;
            if (System.IO.File.Exists(source.FullName))
            {
                try
                {
                    using (System.IO.FileStream fs = System.IO.File.Open(source.FullName, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read))
                        foreach (byte b in CRC32.ComputeHash(fs))
                            hash += b.ToString("x2").ToLower();
                }
                catch (Exception ex)
                {
                    ex.DebugThis();
                }
            }

            return hash;
        }
    }
}

#endif