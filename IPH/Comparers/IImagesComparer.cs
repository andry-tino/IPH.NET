/// <summary>
/// IImagesComparer.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH.Comparers
{
    using System;

    /// <summary>
    /// Abstraction of comparing algorithms.
    /// </summary>
    public interface IImagesComparer
    {
        /// <summary>
        /// Compares two images and returns the Hamming distance.
        /// </summary>
        /// <param name="image1"></param>
        /// <param name="image2"></param>
        /// <returns>A value indicating whether the two images are similar or not.</returns>
        CompareResult Compare(Image image1, Image image2);
    }
}
