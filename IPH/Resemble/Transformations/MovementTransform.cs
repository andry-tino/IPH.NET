/// <summary>
/// MovementTransform.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH.Resemble.Transformation
{
    using System;

    /// <summary>
    /// Movement transformation.
    /// </summary>
    public class MovementTransform : IPixelTransform
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MovementTransform"/> class.
        /// </summary>
        public MovementTransform()
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

            stream[offset] = ((d2.Red * (PixelColor.ErrorPixelColor.Red / 255)) + PixelColor.ErrorPixelColor.Red) / 2;
            stream[offset + 1] = ((d2.Green * (PixelColor.ErrorPixelColor.Green / 255)) + PixelColor.ErrorPixelColor.Green) / 2;
            stream[offset + 2] = ((d2.Blue * (PixelColor.ErrorPixelColor.Blue / 255)) + PixelColor.ErrorPixelColor.Blue) / 2;
            stream[offset + 3] = d2.Alpha;
        }
    }
}
