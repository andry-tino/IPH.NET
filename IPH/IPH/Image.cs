/// <summary>
/// Image.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH
{
    using System;
    using System.Drawing;
    using System.IO;

    using MathNet.Numerics.LinearAlgebra;

    using NativeImage = System.Drawing.Bitmap;
    using RawImageMatrix = MathNet.Numerics.LinearAlgebra.Matrix<byte>;
    using NImageMatrix = MathNet.Numerics.LinearAlgebra.Matrix<double>;

    /// <summary>
    /// Wrapper around <see cref="System.Drawing.Image"/> and <see cref="Matrix{T}"/>.
    /// </summary>
    internal class Image : IDisposable, ICloneable
    {
        private RawImageMatrix matrixR; // Red
        private RawImageMatrix matrixG; // Green
        private RawImageMatrix matrixB; // Blue
        private NImageMatrix matrixN; // Single value

        private NativeImage image;

        /// <summary>
        /// Initializes a new instance of the <see cref="Image"/> class.
        /// </summary>
        public Image(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (File.Exists(path))
            {
                throw new ArgumentException("Specified image could not be found!", nameof(path));
            }

            this.image = new Bitmap(path);

            this.InitializeRGBMatrices();
            this.matrixN = null;
        }

        /// <summary>
        /// Gets the <see cref="NImageMatrix"/> associated to the image.
        /// </summary>
        public NImageMatrix Matrix
        {
            get
            {
                if (this.matrixN == null)
                {
                    this.matrixN = this.CreateMatrixN();
                }

                return this.matrixN;
            }
        }

        /// <summary>
        /// Gets the width of the image.
        /// </summary>
        public int Width
        {
            get { return this.image.Width; }
        }

        /// <summary>
        /// Gets the height of the image.
        /// </summary>
        public int Height
        {
            get { return this.image.Height; }
        }

        /// <summary>
        /// Disposes the class.
        /// </summary>
        public void Dispose()
        {
            this.image.Dispose();

            this.matrixR = null;
            this.matrixG = null;
            this.matrixB = null;
            this.matrixN = null;
        }

        /// <summary>
        /// Copies the image.
        /// </summary>
        /// <returns>Copied image.</returns>
        public object Clone()
        {
            throw new NotImplementedException();
        }

        private void InitializeRGBMatrices()
        {
            var matrixBuilder = RawImageMatrix.Build;

            this.matrixR = matrixBuilder.Dense(this.Width, this.Height);
            this.matrixG = matrixBuilder.Dense(this.Width, this.Height);
            this.matrixB = matrixBuilder.Dense(this.Width, this.Height);

            this.Image2Matrix(this.image, this.matrixR, ColorChannel.Red);
            this.Image2Matrix(this.image, this.matrixG, ColorChannel.Green);
            this.Image2Matrix(this.image, this.matrixB, ColorChannel.Blue);
        }

        private NImageMatrix CreateMatrixN()
        {
            // Precondition: RGB matrices are available and initialized
            return null;
        }

        #region Utilities

        private void Image2Matrix(NativeImage image, RawImageMatrix matrix, ColorChannel channel)
        {
            // Precondition: matrix already initialized
            for (int i = 0, wl = image.Width; i < wl; i++)
            {
                for (int j = 0, hl = image.Height; j < hl; j++)
                {
                    matrix[i, j] = image.GetPixel(i, j).R;
                }
            }
        }

        private static byte GetColorChannel(Color color, ColorChannel channel)
        {
            switch (channel)
            {
                case ColorChannel.Red: return color.R;
                case ColorChannel.Green: return color.G;
                case ColorChannel.Blue: return color.B;
                default:
                    throw new ArgumentException("Not recognized color channel!", nameof(channel));
            }
        }

        #endregion

        #region Types

        /// <summary>
        /// Color channels for image.
        /// </summary>
        private enum ColorChannel
        {
            Red,
            Green,
            Blue
        }

        #endregion
    }
}
