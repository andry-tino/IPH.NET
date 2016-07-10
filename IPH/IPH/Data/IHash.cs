/// <summary>
/// IHash.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public interface IHash
    {
        /// <summary>
        /// 
        /// </summary>
        IEnumerable<byte> Stream { get; }
    }
}
