/// <summary>
/// MaxDistanceAggregator.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH.Comparers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Aggregates by returning the max distance result.
    /// </summary>
    public class MaxDistanceAggregator : ICompareResultAggregator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MaxDistanceAggregator"/> class.
        /// </summary>
        /// <param name="threshold"></param>
        public MaxDistanceAggregator()
        {
        }

        /// <summary>
        /// Generates a final <see cref="CompareResult"/> out of many.
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        public CompareResult Aggregate(IEnumerable<CompareResult> results) => results.Aggregate(
            (result1, result2) => result1.Distance.CompareTo(result2.Distance) >= 0 
                ? result1 
                : result2);
    }
}
