/// <summary>
/// HammingDistanceCalculator.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH
{
    using System;
    using System.Linq;

    /// <summary>
    /// Class implementing an Hamming distance calculator between two hashes.
    /// </summary>
    public class HammingDistanceCalculator : IDistanceCalculator<uint>
    {
        private readonly IHash hash1;
        private readonly IHash hash2;

        // Cached values
        private bool distanceCalculated;
        private uint distance;

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

            if (!ValidateHashes(hash1, hash2))
            {
                throw new ArgumentException("Hashes must be the same type!");
            }

            this.hash1 = hash1;
            this.hash2 = hash2;

            this.distanceCalculated = false;
        }

        /// <summary>
        /// Gets the distance between the two hases.
        /// </summary>
        public uint Distance
        {
            get
            {
                if (!this.distanceCalculated)
                {
                    this.distance = this.GetDistance();
                    this.distanceCalculated = true;
                }

                return this.distance;
            }
        }

        /// <summary>
        /// Compares the distance with a provided value.
        /// </summary>
        /// <param name="other">The distance to compare to.</param>
        /// <returns>A positive number whether it is higher than other, a negative if less than other, 0 if equal.</returns>
        public int CompareTo(uint other)
        {
            return (int)(this.Distance - other);
        }

        private static bool ValidateHashes(IHash hash1, IHash hash2)
        {
            Type hash1Type = hash1.GetType();
            Type hash2Type = hash2.GetType();

            int hash1Length = hash1.Stream.Count();
            int hash2Length = hash2.Stream.Count();

            return hash1Type.Equals(hash2Type) && hash1Length == hash2Length;
        }

        private uint GetDistance()
        {
            uint distance = 0;

            for (int i = 0, l = this.hash1.Stream.Count(); i < l; i++)
            {
                uint b1 = this.hash1.Stream.ElementAt(i);
                uint b2 = this.hash2.Stream.ElementAt(i);

                uint x = b1 ^ b2;

                while (x != 0)
                {
                    distance++;
                    x &= x - 1;
                }
            }

            return distance;
        }
    }
}
