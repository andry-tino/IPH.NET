/// <summary>
/// MultipleOrizontalSlicesComparer.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH.Comparers
{
    using System;

    /// <summary>
    /// Slices the image inertical slices and compares eacof them.
    /// </summary>
    public class MultipleHorizontalSlicesComparer : MultipleSlicesComparer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultipleHorizontalSlicesComparer"/> class.
        /// </summary>
        /// <param name="threshold"></param>
        public MultipleHorizontalSlicesComparer(uint threshold, int slicesCount)
            : base(threshold, slicesCount)
        {
        }

        protected override int RealSlicesCount(Image image) => (image.Height % this.SlicesCount > 0) ? this.SlicesCount + 1 : this.SlicesCount;

        protected override int SliceLength(Image image) => image.Height / this.SlicesCount;
    }
}
