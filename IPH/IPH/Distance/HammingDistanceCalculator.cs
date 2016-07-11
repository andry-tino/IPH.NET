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
        private IHash hash1;
        private IHash hash2;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hash1"></param>
        /// <param name="hash2"></param>
        public HammingDistanceCalculator(IHash hash1, IHash hash2)
        {
            this.hash1 = hash1;
            this.hash2 = hash2;
        }

        /// <summary>
        /// 
        /// </summary>
        public double Distance
        {
            get
            {
                var h1 = this.hash1 as Hash64Bit;
                var h2 = this.hash2 as Hash64Bit;

                ulong v1 = h1.Value;
                ulong v2 = h2.Value;

                ulong x = v1 ^ v2;

                ulong m1 = 0x5555555555555555UL;
                ulong m2 = 0x3333333333333333UL;
                ulong h01 = 0x0101010101010101UL;
                ulong m4 = 0x0f0f0f0f0f0f0f0fUL;
                x -= (x >> 1) & m1;
                x = (x & m2) + ((x >> 2) & m2);
                x = (x + (x >> 4)) & m4;

                return (x * h01) >> 56;
            }
        }
    }
}
