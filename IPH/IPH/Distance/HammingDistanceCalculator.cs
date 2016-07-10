/// <summary>
/// HammingDistanceCalculator.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class HammingDistanceCalculator : IDistanceCalculator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hash1"></param>
        /// <param name="hash2"></param>
        public HammingDistanceCalculator(IHash hash1, IHash hash2)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public double Distance
        {
            get { return 0; }
        }
    }
}
