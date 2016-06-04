using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Platform.Support.Security.Cryptography.Encryption
{
    /// <summary>
    /// Hash functions are fundamental to modern cryptography. These functions map binary 
    /// strings of an arbitrary length to small binary strings of a fixed length, known as 
    /// hash values. A cryptographic hash function has the property that it is computationally
    /// infeasible to find two distinct inputs that hash to the same value. Hash functions 
    /// are commonly used with digital signatures and for data integrity.
    /// </summary>
    public class Hash
    {

        /// <summary>
        /// Type of hash; some are security oriented, others are fast and simple
        /// </summary>
        public enum Provider
        {
            /// <summary>
            /// Cyclic Redundancy Check provider, 32-bit
            /// </summary>
            CRC32,
            /// <summary>
            /// Secure Hashing Algorithm provider, SHA-1 variant, 160-bit
            /// </summary>
            SHA1,
            /// <summary>
            /// Secure Hashing Algorithm provider, SHA-2 variant, 256-bit
            /// </summary>
            SHA256,
            /// <summary>
            /// Secure Hashing Algorithm provider, SHA-2 variant, 384-bit
            /// </summary>
            SHA384,
            /// <summary>
            /// Secure Hashing Algorithm provider, SHA-2 variant, 512-bit
            /// </summary>
            SHA512,
            /// <summary>
            /// Message Digest algorithm 5, 128-bit
            /// </summary>
            MD5
        }

        private HashAlgorithm _Hash;

        private Data _HashValue = new Data();
        private Hash()
        {
        }

        /// <summary>
        /// Instantiate a new hash of the specified type
        /// </summary>
        public Hash(Provider p)
        {
            switch (p)
            {
                case Provider.CRC32:
                    _Hash = new CRC32();
                    break;
                case Provider.MD5:
                    _Hash = new MD5CryptoServiceProvider();
                    break;
                case Provider.SHA1:
                    _Hash = new SHA1Managed();
                    break;
                case Provider.SHA256:
                    _Hash = new SHA256Managed();
                    break;
                case Provider.SHA384:
                    _Hash = new SHA384Managed();
                    break;
                case Provider.SHA512:
                    _Hash = new SHA512Managed();
                    break;
            }
        }

        /// <summary>
        /// Returns the previously calculated hash
        /// </summary>
        public Data Value
        {
            get { return _HashValue; }
        }

        /// <summary>
        /// Calculates hash on a stream of arbitrary length
        /// </summary>
        public Data Calculate(ref System.IO.Stream s)
        {
            _HashValue.Bytes = _Hash.ComputeHash(s);
            return _HashValue;
        }

        /// <summary>
        /// Calculates hash for fixed length <see cref="Data"/>
        /// </summary>
        public Data Calculate(Data d)
        {
            return CalculatePrivate(d.Bytes);
        }

        /// <summary>
        /// Calculates hash for a string with a prefixed salt value. 
        /// A "salt" is random data prefixed to every hashed value to prevent 
        /// common dictionary attacks.
        /// </summary>
        public Data Calculate(Data d, Data salt)
        {
            byte[] nb = new byte[d.Bytes.Length + salt.Bytes.Length - 1];
            salt.Bytes.CopyTo(nb, 0);
            d.Bytes.CopyTo(nb, salt.Bytes.Length);
            return CalculatePrivate(nb);
        }

        /// <summary>
        /// Calculates hash for an array of bytes
        /// </summary>
        private Data CalculatePrivate(byte[] b)
        {
            _HashValue.Bytes = _Hash.ComputeHash(b);
            return _HashValue;
        }

    }
}
