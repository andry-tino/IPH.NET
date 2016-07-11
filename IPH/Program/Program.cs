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
            PerceptualHasher PH = new PerceptualHasher(image);

            IHash hash = PH.Hash;
            Console.WriteLine(hash.Representation);
        }
    }
}
