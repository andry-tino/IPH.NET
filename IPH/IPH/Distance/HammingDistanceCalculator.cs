/// <summary>
/// HammingDistanceCalculator.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH
{
    using System;

    /// <summary>
    /// Class implementing an Hamming distance calculator between two hashes.
    /// </summary>
    public class HammingDistanceCalculator : IDistanceCalculator
    {
        private readonly IHash hash1;
        private readonly IHash hash2;

        // Cached values
        private double distance;

        /// <summary>
        /// Initializes a new instance of the <see cref="HammingDistanceCalculator"/> class.
        /// </summary>
        /// <param name="hash1">The first <see cref="IHash"/> to use for comparison.</param>
        /// <param name="hash2">The second <see cref="IHash"/> to use for comparison.</param>
        public HammingDistanceCalculator(IHash hash1, IHash hash2)
        {
            if (hash1 == null)
            {
                throw new ArgumentNullException(nameof(hash1));
            }
            if (hash2 == null)
            {
                throw new ArgumentNullException(nameof(hash2));
            }

            this.hash1 = hash1;
            this.hash2 = hash2;

            this.distance = -1;
        }

        /// <summary>
        /// Gets the distance between the two hases.
        /// </summary>
        public double Distance
        {
            get
            {
                if (this.distance == -1)
                {
                    var h1 = this.hash1 as Hash64Bit;
                    var h2 = this.hash2 as Hash64Bit;

                    ulong v1 = h1.Value;
                    ulong v2 = h2.Value;

                    ulong x = v1 ^ v2;

                    ulong m1 = 0x5555555555555555UL;
                    ulong m2 = 0x3333333333333333UL;
                    ulong h01 = 0x0101010101010101UL;
                    ulong m4 = 0x0f0f0f0f0f0f0f0fUL;
                    x -= (x >> 1) & m1;
                    x = (x & m2) + ((x >> 2) & m2);
                    x = (x + (x >> 4)) & m4;

                    this.distance = (x * h01) >> 56;
                }

                return this.distance;
            }
        }
    }
}
