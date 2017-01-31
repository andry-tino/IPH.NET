/// <summary>
/// Program.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH.Program
{
    using System;
    using System.Diagnostics;

    using IPH.Comparers;

    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        private const uint threshold = 2;
        private const int slicesCount = 10;
        
        /// <summary>
        /// The entry point.
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

            TimeSpan elapsed;
            var result = ComputeResult(image1, image2, out elapsed);

            Console.WriteLine($"Operation completed in: {string.Format("{0:00}:{1:00}", elapsed.Seconds, elapsed.Milliseconds)}");
            Console.WriteLine();

            Console.WriteLine(string.Format("Hash for image-1 is: {0}", result.Hash1.Representation));
            Console.WriteLine(string.Format("Hash for image-2 is: {0}", result.Hash2.Representation));

            Console.WriteLine(string.Format("Distance is: {0}", result.Distance.StringRepresentation));

            Console.WriteLine(string.Format("Threshold comparison set to: {0} - Response: {1}", 
                threshold, result.Result ? "OK" : "OOT (Out Of Threshold)"));

            Console.WriteLine();
            Console.WriteLine("Result printout: " + result.ToString());

            if (result.DimensionalInfo != null)
            {
                Console.WriteLine();
                Console.WriteLine("Dimensions...");
                foreach (var dimension in result.DimensionalInfo)
                {
                    Console.WriteLine(dimension.ToString());
                }
            }
        }

        private static CompareResult ComputeResult(Image image1, Image image2, out TimeSpan elapsed)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var result = Comparer.Compare(image1, image2);

            stopwatch.Stop();
            elapsed = stopwatch.Elapsed;

            return result;
        }

        private static IImagesComparer Comparer
        {
            get
            {
#if MVS_COMPARER
                return new MultipleVerticalSlicesComparer(threshold, slicesCount);
#elif MHS_COMPARER
                return new MultipleHorizontalSlicesComparer(threshold, slicesCount);
#elif MCS_COMPARER
                return new MultipleCrossSlicesComparer(threshold, slicesCount);
#else
                return new NormalComparer(threshold);
#endif
            }
        }
    }
}
