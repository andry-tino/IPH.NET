/// <summary>
/// IHasher.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH
{
    using System;

    /// <summary>
    /// Interface describing algorithms providing logic for hashing images.
    /// </summary>
    public interface IHasher
    {
        /// <summary>
        /// Calculates the hash of an <see cref="Image"/>.
        /// </summary>
        /// <param name="image">The <see cref="Image"/> to hash.</param>
        /// <returns>The hash of the input <see cref="Image"/>.</returns>
        IHash CalculateHash(Image image);
    }
}
