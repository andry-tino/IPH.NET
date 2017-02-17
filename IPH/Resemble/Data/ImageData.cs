/// <summary>
/// ImageData.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH.Resemble
{
    using System;
    using System.Drawing;

    /// <summary>
    /// Data stream for an image.
    /// </summary>
    public sealed class ImageData
    {
        private double[] stream;

        /// <summary>
        /// The distance
        /// </summary>
        public double this[int index]
        {
            get { return this.stream[index]; }
            set { this.stream[index] = value; }
        }

        /// <summary>
        /// The width of the image.
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// The height of the image.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageData"/> class.
        /// </summary>
        public ImageData(Bitmap image)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            this.Width = image.Width;
            this.Height = image.Height;

            this.stream = new double[4 * image.Width * image.Height];

            for (int r = 0, i = 0; r < image.Height; r++)
            {
                for (int c = 0; c < image.Width; c++)
                {
                    this.stream[i++] = image.GetPixel(c, r).R;
                    this.stream[i++] = image.GetPixel(c, r).G;
                    this.stream[i++] = image.GetPixel(c, r).B;
                    this.stream[i++] = image.GetPixel(c, r).A;
                }
            }
        }

        /// <summary>
        /// Gets the length of the stream.
        /// </summary>
        public int Length => this.stream.Length;
    }
}
