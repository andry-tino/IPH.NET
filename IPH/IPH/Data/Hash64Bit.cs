/// <summary>
/// Hash64Bit.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public class Hash64Bit : IHash
    {
        private ulong hash;

        /// <summary>
        /// 
        /// </summary>
        public Hash64Bit(ulong value)
        {
            this.hash = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<byte> Stream
        {
            get { return null; }
        }

        /// <summary>
        /// To remove.
        /// </summary>
        public ulong Value
        {
            get { return this.hash; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Representation
        {
            get { return this.hash.ToString(); }
        }
    }
}
