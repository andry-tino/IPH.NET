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
        public static double GetBrightness(double red, double green, double blue)
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
        public static double GetHue(double red, double green, double blue)
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

        /// <summary>
        /// Adds hue info to a <see cref="PixelColor"/>.
        /// </summary>
        /// <param name="pixel"></param>
        public static void AddHueInfo(this PixelColor pixel)
        {
            pixel.Hue = GetHue(pixel.Red, pixel.Green, pixel.Blue);
        }

        /// <summary>
        /// Adds brightness info to a <see cref="PixelColor"/>.
        /// </summary>
        /// <param name="pixel"></param>
        public static void AddBrightnessInfo(this PixelColor pixel)
        {
            pixel.Brightness = GetBrightness(pixel.Red, pixel.Green, pixel.Blue); // Corrected lightness
        }

        /// <summary>
        /// The pixel transparency;
        /// </summary>
        public static double PixelTransparency => 1;

        /// <summary>
        /// Copies a pixel.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="offset"></param>
        /// <param name="pixel"></param>
        public static void CopyPixel(ImageData stream, int offset, PixelColor pixel)
        {
            stream[offset] = pixel.Red;
            stream[offset + 1] = pixel.Green;
            stream[offset + 2] = pixel.Blue;
            stream[offset + 3] = pixel.Alpha * PixelTransparency;
        }

        /// <summary>
        /// Copies the gray channel.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="offset"></param>
        /// <param name="pixel"></param>
        public static void CopyGrayScalePixel(ImageData stream, int offset, PixelColor pixel)
        {
            stream[offset] = pixel.Brightness; // Red
            stream[offset + 1] = pixel.Brightness; // Green
            stream[offset + 2] = pixel.Brightness; // Blue
            stream[offset + 3] = pixel.Alpha * PixelTransparency; // Alpha
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="stream"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static bool GetPixelInfo(this PixelColor destination, ImageData stream, int offset)
        {
            if (stream.Length > offset && offset >= 0)
            {
                destination.Red = stream[offset];
                destination.Green = stream[offset + 1];
                destination.Blue = stream[offset + 2];
                destination.Alpha = stream[offset + 3];

                return true;
            }

            return false;
        }

        /// <summary>
        /// Loops.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="callback"></param>
        public static void Loop(int x, int y, Func<int, int, bool> callback)
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (!callback(i, j)) return;
                }
            }
        }
    }
}
