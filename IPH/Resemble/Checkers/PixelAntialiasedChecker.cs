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
        /// <param name="horizontalPosition"></param>
        /// <param name="width"></param>
        public PixelAntialiasedChecker(PixelColor sourcePixel, ImageData imageStream, int verticalPosition, int horizontalPosition, int width)
        {
            int offset;
            int distance = 1;
            int i, j;
            int hasHighContrastSibling = 0;
            int hasSiblingWithDifferentHue = 0;
            int hasEquivalentSibling = 0;

            sourcePixel.AddHueInfo();

            for (i = distance * -1; i <= distance; i++)
            {
                for (j = distance * -1; j <= distance; j++)
                {

                    if (i == 0 && j == 0)
                    {
                        // Ignore source pixel
                    }
                    else
                    {

                        offset = ((verticalPosition + j) * width + (horizontalPosition + i)) * 4;

                        if (!PixelColor.TargetPixel.GetPixelInfo(imageStream, offset))
                        {
                            continue;
                        }

                        PixelColor.TargetPixel.AddBrightnessInfo();
                        PixelColor.TargetPixel.AddHueInfo();

                        if (new ContrastingChecker(sourcePixel, PixelColor.TargetPixel).Result)
                        {
                            hasHighContrastSibling++;
                        }

                        if (new RGBSameChecker(sourcePixel, PixelColor.TargetPixel).Result)
                        {
                            hasEquivalentSibling++;
                        }

                        if (Math.Abs(PixelColor.TargetPixel.Hue - sourcePixel.Hue) > 0.3)
                        {
                            hasSiblingWithDifferentHue++;
                        }

                        if (hasSiblingWithDifferentHue > 1 || hasHighContrastSibling > 1)
                        {
                            this.Result = true;
                            return;
                        }
                    }
                }
            }

            if (hasEquivalentSibling < 2)
            {
                this.Result = true;
                return;
            }

            this.Result = false;
        }
    }
}
