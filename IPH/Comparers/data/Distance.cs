/// <summary>
/// Distance.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH.Comparers
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Generalizes possible distances in order to have comparison capabilities.
    /// </summary>
    public class Distance : IComparable<Distance>
    {
        private readonly DistanceUnderlyingType type;

        private readonly uint uintDistance;

        /// <summary>
        /// Initializes a new instance of the <see cref="Distance"/> class.
        /// </summary>
        /// <param name="uintDistance"></param>
        public Distance(uint uintDistance)
        {
            this.uintDistance = uintDistance;
            this.type = DistanceUnderlyingType.UInt;
        }

        /// <summary>
        /// Gets the string representation of the distance.
        /// </summary>
        public string StringRepresentation
        {
            get
            {
                switch (this.type)
                {
                    case DistanceUnderlyingType.UInt:
                        return this.uintDistance.ToString();
                    default:
                        throw new InvalidOperationException("Not recognized type");
                }
            }
        }

        /// <summary>
        /// Compares to another <see cref="Distance"/>.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Distance other)
        {
            switch (this.type)
            {
                case DistanceUnderlyingType.UInt:
                    return this.uintDistance.CompareTo(other);
                default:
                    throw new InvalidOperationException("Not recognized type");
            }
        }

        #region Types

        /// <summary>
        /// All possible types of distances.
        /// </summary>
        private enum DistanceUnderlyingType
        {
            UInt
        }

        #endregion
    }
}
