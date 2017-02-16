/// <summary>
/// CSSResembleBasedComparer.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH.Comparers
{
    using System;

    /// <summary>
    /// Algorithm based on CSS Resemble.
    /// </summary>
    public class CSSResembleBasedComparer : IImagesComparer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSSResembleBasedComparer"/> class.
        /// </summary>
        public CSSResembleBasedComparer()
        {
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

            var Resemble = new Resemble.ImagesComparer();
            var result = Resemble.Compare(image1.UnderlyingBitmap, image2.UnderlyingBitmap);

            return new CompareResult()
            {
                DifferencePercentage = result.MisMatchPercentage
            };
        }
    }
}
