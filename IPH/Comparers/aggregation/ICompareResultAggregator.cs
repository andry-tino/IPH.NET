/// <summary>
/// ICompareResultAggregator.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH.Comparers
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Abstraction of aggregation strategies.
    /// </summary>
    public interface ICompareResultAggregator
    {
        /// <summary>
        /// Generates a final <see cref="CompareResult"/> out of many.
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        CompareResult Aggregate(IEnumerable<CompareResult> results);
    }
}
