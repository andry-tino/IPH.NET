/// <summary>
/// ToolSet.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH.Resemble
{
    using System;

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
    }
}
