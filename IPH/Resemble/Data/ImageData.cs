﻿/// <summary>
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
        /// Initializes a new instance of the <see cref="ImageData"/> class.
        /// </summary>
        public ImageData(Bitmap image)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            this.stream = new double[4 * image.Width * image.Height];

            for (int r = 0, i = 0; r < image.Height; r++)
            {
                for (int c = 0; r < image.Width; c++)
                {
                    this.stream[i] = image.GetPixel(r, c).R;
                    this.stream[++i] = image.GetPixel(r, c).G;
                    this.stream[++i] = image.GetPixel(r, c).B;
                    this.stream[++i] = image.GetPixel(r, c).A;
                }
            }
        }
    }
}
