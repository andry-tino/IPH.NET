﻿/// <summary>
/// NormalComparer.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH.Comparers
{
    using System;

    /// <summary>
    /// Compares 2 images.
    /// </summary>
    public class NormalComparer : IImagesComparer
    {
        private readonly IImagesComparer comparer;

        /// <summary>
        /// Initializes a new instance of the <see cref="NormalComparer"/> class.
        /// </summary>
        /// <param name="threshold"></param>
        public NormalComparer(IImagesComparer comparer)
        {
            if (comparer == null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            this.comparer = comparer;
        }

        /// <summary>
        /// Compares two images and returns the Hamming distance.
        /// </summary>
        /// <param name="image1"></param>
        /// <param name="image2"></param>
        /// <returns>A value indicating whether the two images are similar or not.</returns>
        public CompareResult Compare(Image image1, Image image2)
        {
            if (image1 == null)
            {
                throw new ArgumentNullException(nameof(image1));
            }

            if (image2 == null)
            {
                throw new ArgumentNullException(nameof(image2));
            }

            if (image1.Width != image2.Width || image1.Height != image2.Height)
            {
                throw new ArgumentException("Images must have the same size");
            }

            return this.comparer.Compare(image1, image2);
        }
    }
}
