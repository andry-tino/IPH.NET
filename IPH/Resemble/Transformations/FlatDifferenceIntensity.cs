/// <summary>
/// FlatDifferenceIntensity.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH.Resemble.Transformation
{
    using System;

    /// <summary>
    /// Flat difference intensity transformation.
    /// </summary>
    public class FlatDifferenceIntensity : IPixelTransform
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FlatDifferenceIntensity"/> class.
        /// </summary>
        public FlatDifferenceIntensity()
        {
        }

        /// <summary>
        /// Performs a transformation on the image.
        /// </summary>
        /// <param name="stream">The <see cref="ImageData"/> to transform.</param>
        /// <param name="offset">The offset in the stream.</param>
        /// <param name="d1">The first <see cref="PixelColor"/> to use.</param>
        /// <param name="d2">The second <see cref="PixelColor"/> to use.</param>
        public void Transform(ImageData stream, int offset, PixelColor d1, PixelColor d2)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (offset < 0)
            {
                throw new ArgumentException(nameof(offset));
            }

            if (d1 == null)
            {
                throw new ArgumentNullException(nameof(d1));
            }

            if (d2 == null)
            {
                throw new ArgumentNullException(nameof(d2));
            }

            stream[offset] = PixelColor.ErrorPixelColor.Red;
            stream[offset + 1] = PixelColor.ErrorPixelColor.Green;
            stream[offset + 2] = PixelColor.ErrorPixelColor.Blue;
            stream[offset + 3] = new ColorsDistance(d1, d2).Value;
        }
    }
}
