﻿/// <summary>
/// MultipleSlicesComparer.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH.Comparers
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Slices the image inertical slices and compares eacof them.
    /// </summary>
    public abstract class MultipleSlicesComparer: IImagesComparer
    {
        private readonly IImagesComparer comparer;
        private readonly int slicesCount;

        private ICompareResultAggregator aggregator;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultipleSlicesComparer"/> class.
        /// </summary>
        /// <param name="comparer"></param>
        /// <param name="slicesCount"></param>
        public MultipleSlicesComparer(IImagesComparer comparer, int slicesCount)
        {
            if (comparer == null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            if (slicesCount <= 0)
            {
                throw new ArgumentException(nameof(slicesCount), "Number of slices cannot 0 or negative");
            }

            this.comparer = comparer;
            this.slicesCount = slicesCount;
        }

        /// <summary>
        /// Gets or sets the aggregator.
        /// </summary>
        public ICompareResultAggregator Aggregator
        {
            get
            {
                if (this.aggregator == null)
                {
                    this.aggregator = new MaxDistanceAggregator();
                }

                return this.aggregator;
            }

            set { this.aggregator = value; }
        }
        
        /// <summary>
        /// Compares two images and returns the Hamming distance.
        /// </summary>
        /// <param name="image1"></param>
        /// <param name="image2"></param>
        /// <returns>A value indicating whether the two images are similar or not.</returns>
        public CompareResult Compare(Image image1, Image image2)
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
                throw new Exception("Images must have the same size");
            }

            // Calculating the number of slices
            int realSlicesCount = this.RealSlicesCount(image1);
            int sliceLength = this.SliceLength(image1);

            // Calculating result on whole images
            var normalResult = this.AreSlicesSimilar(image1, image2);

#if DEBUG
            string seed = string.Format("#{0:X6}", new Random().Next(0x1000000));
#endif

            // Collecting results
            var results = new List<CompareResult>();
            for (int i = 0; i < realSlicesCount; i++)
            {
                int x = i * sliceLength;

                Image slice1 = image1.VerticalSlice(x, sliceLength);
                Image slice2 = image2.VerticalSlice(x, sliceLength);

                var result = this.AreSlicesSimilar(slice1, slice2);
                result.Description = $"Slice #{i + 1}";

                results.Add(result);

#if DEBUG

                if (Environment.GetEnvironmentVariable("IPH_DBGFOLDER") != null)
                {
                    if (!System.IO.Directory.Exists(Environment.GetEnvironmentVariable("IPH_DBGFOLDER")))
                    {
                        System.IO.Directory.CreateDirectory(Environment.GetEnvironmentVariable("IPH_DBGFOLDER"));
                    }
                    
                    slice1.SaveTo(System.IO.Path.Combine(Environment.GetEnvironmentVariable("IPH_DBGFOLDER"), $"Img{seed}_S1-{i + 1}.png"));
                    slice2.SaveTo(System.IO.Path.Combine(Environment.GetEnvironmentVariable("IPH_DBGFOLDER"), $"Img{seed}_S2-{i + 1}.png"));
                }

#endif
            }

            // We return always quantities relative to the whole image comparison, but Result will be influenced by slices

            foreach (var result in results)
            {
                if (!result.Result)
                {
                    return new CompareResult()
                    {
                        Hash1 = normalResult.Hash1,
                        Hash2 = normalResult.Hash2,
                        Distance = normalResult.Distance,
                        IntegerResult = normalResult.IntegerResult,
                        Result = false,
                        DimensionalInfo = results,
                        Description = "Failure detected in one or more slices"
                    };
                }
            }

            return new CompareResult()
            {
                Hash1 = normalResult.Hash1,
                Hash2 = normalResult.Hash2,
                Distance = normalResult.Distance,
                IntegerResult = normalResult.IntegerResult,
                Result = normalResult.Result,
                DimensionalInfo = results
            };
        }

        protected int SlicesCount => this.slicesCount;

        protected abstract int RealSlicesCount(Image image);

        protected abstract int SliceLength(Image image);

        private CompareResult AreSlicesSimilar(Image image1, Image image2) => this.comparer.Compare(image1, image2);
    }
}
