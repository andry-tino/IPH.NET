/// <summary>
/// ColorSimilarChecker.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH.Resemble.Checking
{
    using System;

    /// <summary>
    /// Checks whether colors are similar.
    /// </summary>
    public class ColorSimilarChecker : IChecker
    {
        /// <summary>
        /// A value indicating whether the check process was successful or not.
        /// </summary>
        public bool Result { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorSimilarChecker"/> class.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="color"></param>
        public ColorSimilarChecker(int a, int b, PixelColorTolerance.Color color)
        {
            int absDiff = Math.Abs(a - b);

            if (a == b)
            {
                this.Result = true;
            }
            else if (absDiff < new PixelColorTolerance()[color])
            {
                this.Result = true;
            }
            else
            {
                this.Result = false;
            }
        }
    }
}
