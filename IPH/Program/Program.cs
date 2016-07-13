/// <summary>
/// Program.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH.Program
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            Image image = new Image(@"C:\Users\antino\Desktop\Shared\Phone_A0_Standard\Baseline\IE11\25001.png");
            Image image1 = new Image(@"C:\Users\antino\Desktop\Shared\Phone_A0_Standard\Output\IE11\25001.png");

            PerceptualHasher PH = new PerceptualHasher();

            IHash hash = PH.CalculateHash(image);
            IHash hash1 = PH.CalculateHash(image1);

            HammingDistanceCalculator hdc = new HammingDistanceCalculator(hash, hash1);
            var d = hdc.Distance;

            var inThreshold = hdc.CompareTo(2);

            Console.WriteLine(hash.Representation);
        }
    }
}
