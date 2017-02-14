/// <summary>
/// RGBSimilarChecker.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH.Resemble.Checking
{
    using System;

    /// <summary>
    /// Checks whether RGB channels are similar.
    /// </summary>
    public class RGBSimilarChecker : IChecker
    {
        /// <summary>
        /// A value indicating whether the check process was successful or not.
        /// </summary>
        public bool Result { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RGBSimilarChecker"/> class.
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        public RGBSimilarChecker(PixelColor d1, PixelColor d2)
        {
            this.Result = 
                new ColorSimilarChecker(d1.Red, d2.Red, PixelColorTolerance.Color.Red).Result && 
                new ColorSimilarChecker(d1.Green, d2.Green, PixelColorTolerance.Color.Green).Result && 
                new ColorSimilarChecker(d1.Blue, d2.Blue, PixelColorTolerance.Color.Blue).Result && 
                new ColorSimilarChecker(d1.Alpha, d2.Alpha, PixelColorTolerance.Color.Alpha).Result;
        }
    }
}
