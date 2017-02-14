﻿/// <summary>
/// FlatTransform.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH.Resemble.Transformation
{
    using System;

    /// <summary>
    /// Flat transformation.
    /// </summary>
    public class FlatTransform : IPixelTransform
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FlatTransform"/> class.
        /// </summary>
        public FlatTransform()
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

            stream[offset] = errorPixelColor.Red;
            stream[offset + 1] = errorPixelColor.Green;
            stream[offset + 2] = errorPixelColor.Blue;
            stream[offset + 3] = errorPixelColor.Alpha;
        }
    }
}
