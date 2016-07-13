/// <summary>
/// IDistanceCalculator.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH
{
    using System;

    /// <summary>
    /// Interface abstracting common logic to algorithms used to compare hashes
    /// </summary>
    public interface IDistanceCalculator
    {
        /// <summary>
        /// Gets the distance between two <see cref="IHash"/>.
        /// </summary>
        double Distance { get; }
    }
}
