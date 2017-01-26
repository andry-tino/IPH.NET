/// <summary>
/// CompareResult.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH.Comparers
{
    using System;

    /// <summary>
    /// Information about the comparison result.
    /// </summary>
    public sealed class CompareResult
    {
        /// <summary>
        /// Gets or sets the hash on the first image.
        /// </summary>
        public IHash Hash1 { get; set; }

        /// <summary>
        /// Gets or sets the hash on the second image.
        /// </summary>
        public IHash Hash2 { get; set; }

        /// <summary>
        /// Gets or sets the string representation of the distance between the 2 images/hashes.
        /// </summary>
        public string DistanceRepresentation { get; set; }

        /// <summary>
        /// Gets or sets the comparison integer evaluation according to the <see cref="IComparable.CompareTo(object)"/> convention.
        /// </summary>
        public int IntegerResult { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the two images are similar or not.
        /// </summary>
        public bool Result { get; set; }
    }
}
