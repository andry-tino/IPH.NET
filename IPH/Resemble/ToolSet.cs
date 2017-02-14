/// <summary>
/// ToolSet.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH.Resemble
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Tools.
    /// </summary>
    public static class ToolSet
    {
        /// <summary>
        /// Gets the brightness of one pixel.
        /// </summary>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        /// <returns></returns>
        public static double GetBrightness(int red, int green, int blue)
        {
            return 0.3 * red + 0.59 * green + 0.11 * blue;
        }

        /// <summary>
        /// Gets the hue of one pixel.
        /// </summary>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        /// <returns></returns>
        public static double GetHue(int red, int green, int blue)
        {
            double r = red / 255;
            double g = green / 255;
            double b = blue / 255;

            double max = new[] { r, g, b }.Max();
            double min = new[] { r, g, b }.Min();

            double h = 0;

            if (max == min)
            {
                h = 0; // Achromatic
            }
            else
            {
                double d = max - min;

                if (max == r)
                {
                    h = (g - b) / d + (g < b ? 6 : 0);
                }
                else if (max == g)
                {
                    h = (b - r) / d + 2;
                }
                else if (max == b)
                {
                    h = (r - g) / d + 4;
                }

                h /= 6;
            }

            return h;
        }
    }
}
