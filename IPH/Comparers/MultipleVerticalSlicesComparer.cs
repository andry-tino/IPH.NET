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
        /// Initializes a new instance of the <see cref="MultipleVerticalSlicesComparer"/> class.
        /// </summary>
        /// <param name="comparer"></param>
        /// <param name="slicesCount"></param>
        public MultipleVerticalSlicesComparer(IImagesComparer comparer, int slicesCount) 
            : base(comparer, slicesCount)
        {
        }

        protected override int RealSlicesCount(Image image) => (image.Width % this.SlicesCount > 0) ? this.SlicesCount + 1 : this.SlicesCount;

        protected override int SliceLength(Image image) => image.Width / this.SlicesCount;
    }
}
