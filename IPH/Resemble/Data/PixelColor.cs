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
        private static PixelColor targetPixel;
        private static PixelColor errorPixelColor;
        private static PixelColor tolerance;

        /// <summary>
        /// The red channel.
        /// Between 0 and 255.
        /// </summary>
        public double Red { get; set; }

        /// <summary>
        /// The green channel.
        /// Between 0 and 255.
        /// </summary>
        public double Green { get; set; }

        /// <summary>
        /// The blue channel.
        /// Between 0 and 255.
        /// </summary>
        public double Blue { get; set; }

        /// <summary>
        /// The alpha channel.
        /// Between 0 and 255.
        /// </summary>
        public double Alpha { get; set; }

        /// <summary>
        /// The minimum brightness.
        /// Between 0 and 255.
        /// </summary>
        public double MinimumBrightness { get; set; }

        /// <summary>
        /// The maximum brightness.
        /// Between 0 and 255.
        /// </summary>
        public double MaximumBrightness { get; set; }

        /// <summary>
        /// The brightness.
        /// Between 0 and 255.
        /// </summary>
        public double Brightness { get; set; }

        /// <summary>
        /// The brightness.
        /// Between 0 and 255.
        /// </summary>
        public double Hue { get; set; }

        /// <summary>
        /// The amount of white.
        /// Between 0 and 255.
        /// </summary>
        public double White { get; set; }

        /// <summary>
        /// The amount of black.
        /// Between 0 and 255.
        /// </summary>
        public double Black { get; set; }

        /// <summary>
        /// Gets a dimension basing on the input <see cref="Color"/>.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public double this[Color index]
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

        #region Configuration

        /// <summary>
        /// The singleton instance of error pixel color.
        /// </summary>
        public static PixelColor TargetPixel
        {
            get
            {
                if (targetPixel == null)
                {
                    targetPixel = new PixelColor()
                    {
                        Red = 0,
                        Green = 0,
                        Blue = 0,
                        Alpha = 0
                    };
                }

                return targetPixel;
            }
        }

        /// <summary>
        /// The singleton instance of error pixel color.
        /// </summary>
        public static PixelColor ErrorPixelColor
        {
            get
            {
                if (errorPixelColor == null)
                {
                    errorPixelColor = new PixelColor()
                    {
                        Red = 255,
                        Green = 0,
                        Blue = 255,
                        Alpha = 255
                    };
                }

                return errorPixelColor;
            }
        }

        /// <summary>
        /// The singleton instance of tolerance.
        /// </summary>
        public static PixelColor Tolerance
        {
            get
            {
                if (tolerance == null)
                {
                    tolerance = new PixelColor()
                    {
                        Red = 16,
                        Green = 16,
                        Blue = 16,
                        Alpha = 16,
                        MinimumBrightness = 16,
                        MaximumBrightness = 240
                    };
                }

                return tolerance;
            }
        }

        #endregion

        #region Types

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

        #endregion
    }
}
