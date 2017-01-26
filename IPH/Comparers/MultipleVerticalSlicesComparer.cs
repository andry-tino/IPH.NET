/// <summary>
/// MultipleVerticalSlicesComparer.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH.Comparers
{
    using System;

    /// <summary>
    /// Slices the image inertical slices and compares eacof them.
    /// </summary>
    public class MultipleVerticalSlicesComparer : MultipleSlicesComparer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NormalComparer"/> class.
        /// </summary>
        /// <param name="threshold"></param>
        public MultipleVerticalSlicesComparer(uint threshold, int slicesCount) 
            : base(threshold, slicesCount)
        {
        }

        protected override int RealSlicesCount(Image image) => (image.Width % this.SlicesCount > 0) ? this.SlicesCount + 1 : this.SlicesCount;

        protected override int SliceLength(Image image) => image.Width / this.SlicesCount;

        private static CompareResult AreSlicesSimilar(Image image1, Image image2, uint threshold) => new NormalComparer(threshold).Compare(image1, image2);
    }
}
