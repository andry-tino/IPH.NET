/// <summary>
/// NormalComparer.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH.Comparers
{
    using System;

    /// <summary>
    /// Abstraction of comparing algorithms.
    /// </summary>
    public class NormalComparer : IImagesComparer
    {
        private readonly uint threshold;

        /// <summary>
        /// Initializes a new instance of the <see cref="NormalComparer"/> class.
        /// </summary>
        /// <param name="threshold"></param>
        public NormalComparer(uint threshold)
        {
            this.threshold = threshold;
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

            PerceptualHasher PH = new PerceptualHasher();
            IHash hash1 = PH.CalculateHash(image1);
            IHash hash2 = PH.CalculateHash(image2);

            HammingDistanceCalculator hdc = new HammingDistanceCalculator(hash1, hash2);
            var d = hdc.Distance;

            var inThreshold = hdc.CompareTo(threshold);

            return new CompareResult()
            {
                Hash1 = hash1,
                Hash2 = hash2,
                DistanceRepresentation = d.ToString(),
                IntegerResult = inThreshold,
                Result = inThreshold <= 0
            };
        }
    }
}
