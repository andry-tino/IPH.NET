/// <summary>
/// IHash.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface abstracting hashes with common logic.
    /// </summary>
    public interface IHash
    {
        /// <summary>
        /// Gets the bytestream.
        /// </summary>
        IEnumerable<byte> Stream { get; }

        /// <summary>
        /// Gets the string representation of the hash.
        /// </summary>
        string Representation { get; }
    }
}
