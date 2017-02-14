/// <summary>
/// ContrastingChecker.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH.Resemble.Checking
{
    using System;

    /// <summary>
    /// Checks whether there is contrast.
    /// </summary>
    public class ContrastingChecker : IChecker
    {
        /// <summary>
        /// A value indicating whether the check process was successful or not.
        /// </summary>
        public bool Result { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContrastingChecker"/> class.
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        public ContrastingChecker(PixelColor d1, PixelColor d2)
        {
            this.Result = Math.Abs(d1.Brightness - d2.Brightness) > PixelColor.Tolerance.MaximumBrightness;
        }
    }
}
