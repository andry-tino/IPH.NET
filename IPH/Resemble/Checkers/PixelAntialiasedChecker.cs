/// <summary>
/// PixelAntialiasedChecker.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH.Resemble.Checking
{
    using System;

    /// <summary>
    /// Checks whether a pixel is antialiased.
    /// </summary>
    public class PixelAntialiasedChecker : IChecker
    {
        /// <summary>
        /// A value indicating whether the check process was successful or not.
        /// </summary>
        public bool Result { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PixelAntialiasedChecker"/> class.
        /// </summary>
        /// <param name="sourcePixel"></param>
        /// <param name="imageStream"></param>
        /// <param name="verticalPosition"></param>
        /// <param name="horixontalPosition"></param>
        /// <param name="width"></param>
        public PixelAntialiasedChecker(PixelColor sourcePixel, ImageData imageStream, int verticalPosition, int horixontalPosition, int width)
        {
            int offset;
            int distance = 1;
            int i, j;
            int hasHighContrastSibling = 0;
            int hasSiblingWithDifferentHue = 0;
            int hasEquivalentSibling = 0;


        }
    }
}
