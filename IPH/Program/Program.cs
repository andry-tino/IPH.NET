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
            if (args.Length != 2)
            {
                Console.WriteLine("Incorrect usage: expected two parameters!");
                return;
            }

            string path1 = args[0];
            string path2 = args[1];

            Image image1 = null, image2 = null;

            try
            {
                image1 = new Image(path1);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(string.Format("Image {0} could not be loaded!", path1));
                return;
            }

            try
            {
                image2 = new Image(path2);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(string.Format("Image {0} could not be loaded!", path2));
                return;
            }

            PerceptualHasher PH = new PerceptualHasher();
            IHash hash1 = PH.CalculateHash(image1);
            IHash hash2 = PH.CalculateHash(image2);

            Console.WriteLine(string.Format("Hash for image-1 is: {0}", hash1.Representation));
            Console.WriteLine(string.Format("Hash for image-2 is: {0}", hash2.Representation));

            HammingDistanceCalculator hdc = new HammingDistanceCalculator(hash1, hash2);
            var d = hdc.Distance;

            Console.WriteLine(string.Format("Distance is: {0}", d));

            uint threshold = 2;
            var inThreshold = hdc.CompareTo(threshold);

            Console.WriteLine(string.Format("Threshold comparison set to: {0} - Response: {1}", 
                threshold, inThreshold <= 0 ? "OK" : "OOT (Out Of Threshold)"));
        }

        public static void Main2(string[] args)
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
