﻿/// <summary>
/// ImagesComparer.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH.Resemble
{
    using System;
    using System.Diagnostics;
    using System.Drawing;

    using IPH.Resemble.Checking;
    using IPH.Resemble.Transformation;

    /// <summary>
    /// The algorithm taken from https://github.com/Huddle/Resemble.js/blob/master/resemble.js.
    /// </summary>
    public class ImagesComparer
    {
        private IPixelTransform errorPixelTransform;

        private AlgorithmData data;

        public ImagesComparer()
        {
            this.LargeImageThreshold = 1200;
            this.IgnoreAntialiasing = false;
            this.IgnoreColors = false;
        }

        /// <summary>
        /// 
        /// </summary>
        public int LargeImageThreshold { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating whether antialiasing should be ignored when comparing.
        /// </summary>
        public bool IgnoreAntialiasing { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating whether colors should be ignored when comparing.
        /// </summary>
        public bool IgnoreColors { get; set; }

        /// <summary>
        /// Gets or sets the error pixel transform to use.
        /// </summary>
        public IPixelTransform ErrorPixelTransform
        {
            get
            {
                if (this.errorPixelTransform == null)
                {
                    this.errorPixelTransform = new FlatTransform();
                }

                return this.errorPixelTransform;
            }

            set { this.errorPixelTransform = value; }
        }

        /// <summary>
        /// Compares two images.
        /// </summary>
        /// <param name="image1"></param>
        /// <param name="image2"></param>
        /// <returns></returns>
        public AlgorithmData Compare(Bitmap image1, Bitmap image2)
        {
            if (image1 == null)
            {
                throw new ArgumentNullException(nameof(image1));
            }

            if (image2 == null)
            {
                throw new ArgumentNullException(nameof(image2));
            }

            if (image1.Width != image2.Width || image1.Height != image2.Height)
            {
                throw new ArgumentException("Images must have the same size");
            }

            // Resetting
            this.data = new AlgorithmData();

            this.AnalyzeImages(new ImageData(image1), new ImageData(image2));

            return this.data;
        }

        private void AnalyzeImages(ImageData image1, ImageData image2)
        {
            // Assumption: both images have the same size
            var width = image1.Width;
            var height = image1.Height;

            var data1 = image1;
            var data2 = image2;

            var imgd = new Bitmap(width, height);
            var targetPix = new ImageData(imgd); // Attention, this copies, keep in sync image and stream

            var mismatchCount = 0;

            var diffBounds = new DifferenceBounds()
            {
                Top = height,
                Left = width,
                Bottom = 0,
                Right = 0
            };

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            int skip = 0;

            if (LargeImageThreshold > 0 && IgnoreAntialiasing && (width > LargeImageThreshold || height > LargeImageThreshold))
            {
                skip = 6;
            }

            var pixel1 = new PixelColor() { Red = 0, Green = 0, Blue = 0, Alpha = 0 };
            var pixel2 = new PixelColor() { Red = 0, Green = 0, Blue = 0, Alpha = 0 };

            ToolSet.Loop(width, height, (horizontalPos, verticalPos) =>
            {
                if (skip == 0) // Only skip if the image isn't small
                {
                    if (verticalPos % skip == 0 || horizontalPos % skip == 0)
                    {
                        return;
                    }
                }

                int offset = (verticalPos * width + horizontalPos) * 4;

                if (!ToolSet.GetPixelInfo(pixel1, data1, offset) || !ToolSet.GetPixelInfo(pixel2, data2, offset))
                {
                    return;
                }

                if (IgnoreColors)
                {
                    pixel1.AddBrightnessInfo();
                    pixel2.AddBrightnessInfo();

                    if (new PixelBrightnessSimilarChecker(pixel1, pixel2).Result)
                    {
                        ToolSet.CopyGrayScalePixel(targetPix, offset, pixel2);
                    }
                    else
                    {
                        this.errorPixelTransform.Transform(targetPix, offset, pixel1, pixel2);
                        mismatchCount++;
                        diffBounds.UpdateBounds(horizontalPos, verticalPos);
                    }
                    return;
                }

                if (new RGBSimilarChecker(pixel1, pixel2).Result)
                {
                    ToolSet.CopyPixel(targetPix, offset, pixel1/*, pixel2*/);

                }
                else
                {
                    pixel1.AddBrightnessInfo();
                    pixel2.AddBrightnessInfo();

                    bool isAntialiased = 
                        new PixelAntialiasedChecker(pixel1, data1, verticalPos, horizontalPos, width).Result || 
                        new PixelAntialiasedChecker(pixel2, data2, verticalPos, horizontalPos, width).Result;
                    
                    if (IgnoreAntialiasing && isAntialiased)
                    {
                        if (new PixelBrightnessSimilarChecker(pixel1, pixel2).Result)
                        {
                            ToolSet.CopyGrayScalePixel(targetPix, offset, pixel2);
                        }
                        else
                        {
                            this.errorPixelTransform.Transform(targetPix, offset, pixel1, pixel2);
                            mismatchCount++;
                            diffBounds.UpdateBounds(horizontalPos, verticalPos);
                        }
                    }
                    else
                    {
                        this.errorPixelTransform.Transform(targetPix, offset, pixel1, pixel2);
                        mismatchCount++;
                        diffBounds.UpdateBounds(horizontalPos, verticalPos);
                    }
                }
            });
            
            this.data.RawMisMatchPercentage = (mismatchCount / (height * width) * 100);
            this.data.MisMatchPercentage = this.data.RawMisMatchPercentage;
            this.data.DiffBounds = diffBounds;

            stopwatch.Stop();
            this.data.AnalysisTime = stopwatch.Elapsed;
        }
    }
}
