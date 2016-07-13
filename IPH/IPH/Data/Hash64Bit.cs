/// <summary>
/// Hash64Bit.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A 64 bit hash.
    /// </summary>
    public class Hash64Bit : IHash
    {
        private readonly ulong hash; // 64 bits (8 bytes) unsigned int

        // Cached values
        private IEnumerable<byte> stream;

        /// <summary>
        /// Initializes a new instance of the <see cref="Hash64Bit"/> class.
        /// </summary>
        public Hash64Bit(ulong value)
        {
            this.hash = value;
        }

        /// <summary>
        /// Gets the bytestream of the hash.
        /// </summary>
        public IEnumerable<byte> Stream
        {
            get
            {
                if (this.stream == null)
                {
                    this.stream = BitConverter.GetBytes(this.hash);
                }

                return this.stream;
            }
        }

        /// <summary>
        /// TODO: to remove.
        /// </summary>
        public ulong Value
        {
            get { return this.hash; }
        }

        /// <summary>
        /// Gets the string representation of the hash.
        /// </summary>
        public string Representation
        {
            get { return this.hash.ToString(); }
        }
    }
}
