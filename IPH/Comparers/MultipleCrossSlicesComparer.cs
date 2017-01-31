/// <summary>
/// MultipleCrossSlicesComparer.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH.Comparers
{
    using System;
    using System.Linq;

    /// <summary>
    /// Slices the image inertical slices and compares eacof them.
    /// </summary>
    public class MultipleCrossSlicesComparer : IImagesComparer
    {
        private readonly uint threshold;
        private readonly int slicesCount;

        private IImagesComparer verticalSlicesComparer;
        private IImagesComparer horizontalSlicesComparer;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultipleCrossSlicesComparer"/> class.
        /// </summary>
        /// <param name="threshold"></param>
        public MultipleCrossSlicesComparer(uint threshold, int slicesCount)
        {
            this.threshold = threshold;
            this.slicesCount = slicesCount;

            this.verticalSlicesComparer = new MultipleVerticalSlicesComparer(this.threshold, this.slicesCount);
            this.horizontalSlicesComparer = new MultipleVerticalSlicesComparer(this.threshold, this.slicesCount);
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

            CompareResult vResults = this.verticalSlicesComparer.Compare(image1, image2);
            CompareResult hResults = this.horizontalSlicesComparer.Compare(image1, image2);

            // Flatten results
            // Vertical and horizontal will have the same values, we take one
            CompareResult results = vResults.Clone() as CompareResult;
            results.Result = vResults.Result && hResults.Result;

            Func<CompareResult, string, CompareResult> f = delegate (CompareResult result, string prefix) 
            {
                var clone = result.Clone() as CompareResult;
                clone.Description = $"{prefix} - {result.Description}";

                return clone;
            };

            results.DimensionalInfo = vResults.DimensionalInfo.Select(result => f(result, "V")).Concat(hResults.DimensionalInfo.Select(result => f(result, "H")));

            return results;
        }
    }
}
