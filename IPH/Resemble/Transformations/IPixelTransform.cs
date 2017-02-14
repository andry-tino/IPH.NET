/// <summary>
/// IPixelTransform.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH.Resemble.Transformation
{
    using System;

    /// <summary>
    /// Abstracts transformers.
    /// </summary>
    public interface IPixelTransform
    {
        /// <summary>
        /// Performs a transformation on the image.
        /// </summary>
        /// <param name="stream">The <see cref="ImageData"/> to transform.</param>
        /// <param name="offset">The offset in the stream.</param>
        /// <param name="d1">The first <see cref="PixelColor"/> to use.</param>
        /// <param name="d2">The second <see cref="PixelColor"/> to use.</param>
        void Transform(ImageData stream, int offset, PixelColor d1, PixelColor d2);
    }
}
