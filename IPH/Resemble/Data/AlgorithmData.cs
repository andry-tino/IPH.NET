/// <summary>
/// AlgorithmData.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH.Resemble
{
    using System;

    /// <summary>
    /// Collects info about difference bounds.
    /// </summary>
    public struct AlgorithmData
    {
        /// <summary>
        /// The top bound.
        /// </summary>
        public double RawMisMatchPercentage { get; set; }

        /// <summary>
        /// The percentage of mistmatch between the two images.
        /// </summary>
        public double MisMatchPercentage { get; set; }

        /// <summary>
        /// The diff bounds.
        /// </summary>
        public DifferenceBounds DiffBounds { get; set; }

        /// <summary>
        /// The time required for analysis.
        /// </summary>
        public TimeSpan AnalysisTime { get; set; }
    }
}
