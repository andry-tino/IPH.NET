/// <summary>
/// CompareResult.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH.Comparers
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information about the comparison result.
    /// </summary>
    public sealed class CompareResult : ICloneable
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

        /// <summary>
        /// Gets or sets the description for this result.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets more info.
        /// </summary>
        public IEnumerable<CompareResult> DimensionalInfo;

        public override string ToString()
        {
            return $"{this.Description + " " ?? string.Empty}H1={this.Hash1.Representation} H2={this.Hash2.Representation} D={this.DistanceRepresentation} - {this.Result}";
        }

        /// <summary>
        /// Shallow clones the results, without cloning the <see cref="DimensionalInfo"/>.
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new CompareResult()
            {
                Hash1 = this.Hash1,
                Hash2 = this.Hash2,
                DistanceRepresentation = this.DistanceRepresentation,
                IntegerResult = this.IntegerResult,
                Result = this.Result,
                Description = this.Description
            };
        }
    }
}
