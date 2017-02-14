/// <summary>
/// IPixelTransform.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH.Resemble.Checking
{
    using System;

    /// <summary>
    /// Abstracts checkers.
    /// </summary>
    public interface IChecker
    {
        /// <summary>
        /// A value indicating whether the check process was successful or not.
        /// </summary>
        bool Result { get; }
    }
}
