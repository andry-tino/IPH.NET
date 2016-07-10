/// <summary>
/// Image.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH
{
    using System;
    using System.Drawing;
    using System.IO;

    using NativeImage = System.Drawing.Bitmap;

    /// <summary>
    /// Wrapper around <see cref="System.Drawing.Image"/> and <see cref="Matrix{T}"/>.
    /// </summary>
    public class Image : IDisposable, ICloneable
    {
        private NativeImage image;

        /// <summary>
        /// Initializes a new instance of the <see cref="Image"/> class.
        /// </summary>
        /// <param name="path">Path to image.</param>
        public Image(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (File.Exists(path))
            {
                throw new ArgumentException("Specified image could not be found!", nameof(path));
            }

            this.image = new Bitmap(path);
        }

        /// <summary>
        /// Gets the width of the image.
        /// X axis.
        /// </summary>
        public int Width
        {
            get { return this.image.Width; }
        }

        /// <summary>
        /// Gets the height of the image.
        /// Y axis.
        /// </summary>
        public int Height
        {
            get { return this.image.Height; }
        }

        /// <summary>
        /// Gets the size of the image.
        /// All axis: multidimensional.
        /// </summary>
        public int Size
        {
            get { return this.Width * this.Height; }
        }

        /// <summary>
        /// Gets a new image, which is the current but resized to the specified dimension.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public Image Resize(int width, int height)
        {
            return null;
        }

        /// <summary>
        /// Disposes the class.
        /// </summary>
        public void Dispose()
        {
            this.image.Dispose();
        }

        /// <summary>
        /// Copies the image.
        /// </summary>
        /// <returns>Copied image.</returns>
        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
