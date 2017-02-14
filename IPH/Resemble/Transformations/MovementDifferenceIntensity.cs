/// <summary>
/// MovementDifferenceIntensity.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH.Resemble.Transformation
{
    using System;

    /// <summary>
    /// Movement difference intensity transformation.
    /// </summary>
    public class MovementDifferenceIntensity : IPixelTransform
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MovementDifferenceIntensity"/> class.
        /// </summary>
        public MovementDifferenceIntensity()
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

            var errorPixelColor = new PixelColor();

            double ratio = new ColorsDistance(d1, d2).Value / 255 * 0.8;

            stream[offset] = ((1 - ratio) * (d2.Red * (errorPixelColor.Red / 255)) + ratio * errorPixelColor.Red);
            stream[offset + 1] = ((1 - ratio) * (d2.Green * (errorPixelColor.Green / 255)) + ratio * errorPixelColor.Green);
            stream[offset + 2] = ((1 - ratio) * (d2.Blue * (errorPixelColor.Blue / 255)) + ratio * errorPixelColor.Blue);
            stream[offset + 3] = d2.Alpha;
        }
    }
}
