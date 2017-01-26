/// <summary>
/// Image.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    
    /// <summary>
    /// Class describing the image which can be provided as input of hashing algorithms 
    /// implementing the <see cref="IHasher"/> interface.
    /// </summary>
    public class Image : IDisposable, ICloneable
    {
        private Bitmap image;

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

            if (!File.Exists(path))
            {
                throw new ArgumentException("Specified image could not be found!", nameof(path));
            }

            this.image = new Bitmap(path);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Image"/> class.
        /// </summary>
        /// <param name="bitmap"></param>
        private Image(Bitmap bitmap)
        {
            if (bitmap == null)
            {
                throw new ArgumentNullException(nameof(bitmap));
            }

            this.image = bitmap;
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
        /// <remarks>
        /// This performs a bicubic, high quality,  composition and high quality smoothing transformation.
        /// </remarks>
        public Image Resize(int width, int height)
        {
            Bitmap resizedBitmap = ResizeBitmap(this.image, width, height);
            return new Image(resizedBitmap);
        }

        /// <summary>
        /// Slices the image from the specified x coordinate for the specified width.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="width"></param>
        /// <returns>A vertical slice.</returns>
        public Image VerticalSlice(int x, int width)
        {
            if (x < 0)
            {
                throw new ArgumentException(nameof(x), "Coordinate cannot be negative");
            }
            if (x > this.image.Width)
            {
                throw new ArgumentException(nameof(x), "Coordinate exceeds image boundaries");
            }

            if (width < 0)
            {
                throw new ArgumentException(nameof(width), "Width cannot be negative");
            }
            if (width > this.image.Width)
            {
                throw new ArgumentException(nameof(width), "Width exceeds image boundaries");
            }

            // Note: Coordinates are 0 based!

            int y = 0;
            int w = width;
            int h = this.image.Height;

            // Adjusting rectangle
            int rectx2 = x + w;
            if (rectx2 > this.image.Width)
            {
                w = this.image.Width - x;
            }

            var rect = new Rectangle(x, y, w, h);

            return new Image(this.image.Clone(rect, this.image.PixelFormat));
        }

        /// <summary>
        /// Extract one pixel at a specific coordinate.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Color ColorAt(int x, int y)
        {
            return this.image.GetPixel(x, y);
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

        #region Utilities
        
        private static Bitmap ResizeBitmap(Bitmap source, int width, int height)
        {
            var destinationRectangle = new Rectangle(0, 0, width, height);
            var destinationBitmap = new Bitmap(width, height);

            destinationBitmap.SetResolution(source.HorizontalResolution, source.VerticalResolution);

            using (var graphics = Graphics.FromImage(destinationBitmap))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(source, destinationRectangle, 0, 0, source.Width, source.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destinationBitmap;
        }

        #endregion
    }
}
