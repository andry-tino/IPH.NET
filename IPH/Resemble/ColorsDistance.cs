/// <summary>
/// ColorsDistance.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH.Resemble
{
    using System;

    /// <summary>
    /// Calculated the distance (colors) between 2 <see cref="PixelColor"/> instances.
    /// </summary>
    public sealed class ColorsDistance
    {
        /// <summary>
        /// The distance
        /// </summary>
        public double Value { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorsDistance"/> class.
        /// </summary>
        public ColorsDistance(PixelColor color1, PixelColor color2)
        {
            if (color1 == null)
            {
                throw new ArgumentNullException(nameof(color1));
            }
            if (color2 == null)
            {
                throw new ArgumentNullException(nameof(color2));
            }

            this.Value = (
                Math.Abs(color1.Red - color2.Red) + 
                Math.Abs(color1.Green - color2.Green) +
                Math.Abs(color1.Blue - color2.Blue)) / 3;
        }
    }
}
