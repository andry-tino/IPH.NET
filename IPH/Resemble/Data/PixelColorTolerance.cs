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
        private static PixelColorTolerance tolerance;

        /// <summary>
        /// The minimum brightness.
        /// Between 0 and 255.
        /// </summary>
        public int MinimumBrightness { get; set; }

        /// <summary>
        /// The maximum brightness.
        /// Between 0 and 255.
        /// </summary>
        public int MaximumBrightness { get; set; }

        /// <summary>
        /// The brightness.
        /// Between 0 and 255.
        /// </summary>
        public int Brightness { get; set; }

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

        /// <summary>
        /// Gets a dimension basing on the input <see cref="Color"/>.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int this[Color index]
        {
            get
            {
                switch (index)
                {
                    case Color.Red: return this.Red;
                    case Color.Green: return this.Green;
                    case Color.Blue: return this.Blue;
                    case Color.Alpha: return this.Red;
                    case Color.Bright: return this.Brightness;
                    case Color.MinBright: return this.MinimumBrightness;
                    case Color.MaxBright: return this.MaximumBrightness;
                    default: throw new InvalidOperationException("Unrecognized color");
                }
            }
        }

        public static PixelColorTolerance Tolerance
        {
            get
            {
                if (tolerance == null)
                {
                    tolerance = new PixelColorTolerance();
                }

                return tolerance;
            }
        }

        /// <summary>
        /// The colors in order to index tolerance values.
        /// </summary>
        public enum Color
        {
            Red,
            Green,
            Blue,
            Alpha,
            Bright,
            MinBright,
            MaxBright
        }
    }
}
