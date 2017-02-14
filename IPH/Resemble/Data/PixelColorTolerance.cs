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
    public class PixelColorTolerance : PixelColor
    {
        /// <summary>
        /// The red channel.
        /// Between 0 and 255.
        /// </summary>
        public int MinimumBrightness { get; set; }

        /// <summary>
        /// The green channel.
        /// Between 0 and 255.
        /// </summary>
        public int MaximumBrightness { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PixelColorTolerance"/> class.
        /// </summary>
        public PixelColorTolerance()
        {
            this.Red = 16;
            this.Green = 16;
            this.Blue = 16;
            this.Alpha = 16;
            this.MinimumBrightness = 16;
            this.MaximumBrightness = 240;
        }
    }
}
