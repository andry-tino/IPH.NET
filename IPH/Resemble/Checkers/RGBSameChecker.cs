/// <summary>
/// RGBSameChecker.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH.Resemble.Checking
{
    using System;

    /// <summary>
    /// Checks whether RGB channels are the same.
    /// </summary>
    public class RGBSameChecker : IChecker
    {
        /// <summary>
        /// A value indicating whether the check process was successful or not.
        /// </summary>
        public bool Result { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RGBSameChecker"/> class.
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        public RGBSameChecker(PixelColor d1, PixelColor d2)
        {
            this.Result = 
                d1.Red == d2.Red && 
                d1.Green == d2.Green && 
                d1.Blue == d2.Blue;
        }
    }
}
