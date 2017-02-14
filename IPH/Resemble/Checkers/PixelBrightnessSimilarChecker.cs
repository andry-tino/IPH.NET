/// <summary>
/// ColorSimilarChecker.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH.Resemble.Checking
{
    using System;

    /// <summary>
    /// Checks whether pixel brightnesses are similar.
    /// </summary>
    public class PixelBrightnessSimilarChecker : IChecker
    {
        /// <summary>
        /// A value indicating whether the check process was successful or not.
        /// </summary>
        public bool Result { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PixelBrightnessSimilarChecker"/> class.
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        public PixelBrightnessSimilarChecker(PixelColorTolerance d1, PixelColorTolerance d2)
        {
            var alpha = new ColorSimilarChecker(d1.Alpha, d2.Alpha, PixelColorTolerance.Color.Alpha).Result;
            var brightness = new ColorSimilarChecker(d1.Brightness, d2.Brightness, PixelColorTolerance.Color.MinBright).Result;

            this.Result = alpha && brightness;
        }
    }
}
