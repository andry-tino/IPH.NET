/// <summary>
/// PixelColor.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH.Resemble
{
    using System;

    /// <summary>
    /// Collects info about a pixel.
    /// </summary>
    public class PixelColor
    {
        /// <summary>
        /// The red channel.
        /// Between 0 and 255.
        /// </summary>
        public int Red { get; set; }

        /// <summary>
        /// The green channel.
        /// Between 0 and 255.
        /// </summary>
        public int Green { get; set; }

        /// <summary>
        /// The blue channel.
        /// Between 0 and 255.
        /// </summary>
        public int Blue { get; set; }

        /// <summary>
        /// The alpha channel.
        /// Between 0 and 255.
        /// </summary>
        public int Alpha { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PixelColor"/> class.
        /// </summary>
        public PixelColor()
        {
            this.Red = 255;
            this.Green = 0;
            this.Blue = 255;
            this.Alpha = 255;
        }
    }
}
