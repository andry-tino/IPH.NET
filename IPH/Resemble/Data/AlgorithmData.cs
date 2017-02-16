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
    public class AlgorithmData
    {
        private PixelColor pixel;

        /// <summary>
        /// The red channel.
        /// </summary>
        public double Red
        {
            get { return this.pixel.Red; }
            set { this.pixel.Red = value; }
        }
        /// <summary>
        /// The green channel.
        /// </summary>
        public double Green
        {
            get { return this.pixel.Green; }
            set { this.pixel.Green = value; }
        }
        /// <summary>
        /// The blue channel.
        /// </summary>
        public double Blue
        {
            get { return this.pixel.Blue; }
            set { this.pixel.Blue = value; }
        }
        /// <summary>
        /// The alpha channel.
        /// </summary>
        public double Alpha
        {
            get { return this.pixel.Alpha; }
            set { this.pixel.Alpha = value; }
        }
        /// <summary>
        /// The brightness.
        /// </summary>
        public double Brightness
        {
            get { return this.pixel.Brightness; }
            set { this.pixel.Brightness = value; }
        }
        /// <summary>
        /// The white channel.
        /// </summary>
        public double White
        {
            get { return this.pixel.White; }
            set { this.pixel.White = value; }
        }
        /// <summary>
        /// The black channel.
        /// </summary>
        public double Black
        {
            get { return this.pixel.Black; }
            set { this.pixel.Black = value; }
        }

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
