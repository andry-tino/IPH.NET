/// <summary>
/// DifferenceBounds.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH.Resemble
{
    using System;

    /// <summary>
    /// Collects info about difference bounds.
    /// </summary>
    public class DifferenceBounds
    {
        /// <summary>
        /// The top bound.
        /// </summary>
        public int Top { get; set; }

        /// <summary>
        /// The bottom bound.
        /// </summary>
        public int Bottom { get; set; }

        /// <summary>
        /// The left bound.
        /// </summary>
        public int Left { get; set; }

        /// <summary>
        /// The right bound.
        /// </summary>
        public int Right { get; set; }

        /// <summary>
        /// Updates the values.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void UpdateBounds(int x, int y)
        {
            this.Left = Math.Min(x, this.Left);
            this.Right = Math.Max(x, this.Right);
            this.Top = Math.Min(y, this.Top);
            this.Bottom = Math.Max(y, this.Bottom);
        }
    }
}
