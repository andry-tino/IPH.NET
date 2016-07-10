/// <summary>
/// PerceptualHasher.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH
{
    using System;
    using System.Collections.Generic;

    using MathNet.Numerics.LinearAlgebra;

    /// <summary>
    /// 
    /// </summary>
    public class PerceptualHasher : IHasher
    {
        private const int Size = 64;
        private const int SubSize = 8; // Must be < Size

        private const double RedLUMAFactor = 0.299;
        private const double GreenLUMAFactor = 0.587;
        private const double BlueLUMAFactor = 0.114;

        private readonly Image source;

        private Hash64Bit hash;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        public PerceptualHasher(Image image)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            this.source = image;
        }

        /// <summary>
        /// Gets the hash.
        /// </summary>
        public IHash Hash
        {
            get
            {
                if (this.hash == null)
                {
                    this.Compute(out this.hash);
                }

                return this.hash;
            }
        }

        private void Compute(out Hash64Bit hash)
        {
            // Generate a matrix of dimensions: Size x Size
            Matrix<RGBAColor> resizedMatrix = CreateRGBAMatrix(Size);

            // Get a copy of the image we need to compute the hash on
            // but resized to a default dimension Size
            // Attention: this will alter the proportions, but it is fine
            Image resizedImage = this.source.Resize(Size, Size);

            // Define quantities
            double[] row = new double[Size];
            List<double[]> rows = new List<double[]>(Size);

            // Calculate LUMA from RGB and DCT for each row
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    RGBAColor color = Color2RGBAColor(resizedImage.ColorAt(x, y));
                    row[x] = Math.Floor(
                        color.Red * RedLUMAFactor + 
                        color.Green * GreenLUMAFactor + 
                        color.Blue * BlueLUMAFactor);
                }
                rows[y] = DiscreteCosineTransform1Dimension(row);
            }

            // We need the resized matrix and image no more
            resizedMatrix = null;
            resizedImage.Dispose();
            resizedImage = null;

            // More quantities
            double[] col = new double[Size];
            List<double[]> matrix = new List<double[]>(Size);

            // Calculate the DCT for each column
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    col[y] = rows[y][x];
                }
                matrix[x] = DiscreteCosineTransform1Dimension(col);
            }

            // Extract top SubSize x SubSize pixels
            double[,] pixels = new double[SubSize, SubSize];
            for (int y = 0; y < SubSize; y++)
            {
                for (int x = 0; x < SubSize; x++)
                {
                    pixels[y, x] = matrix[y][x];
                }
            }
            double median = MedianFilter(pixels);

            // Calculate final hash
            ulong rawHash = 0x0;
            ulong one = 0x1;

            for (int i = 0; i < SubSize; i++)
            {
                for (int j = 0; j < SubSize; j++)
                {
                    double pixel = pixels[i, j];
                    if (pixel > median)
                    {
                        rawHash |= one;
                    }
                    one = one << 1;
                }
            }

            hash = new Hash64Bit(rawHash);
        }

        private static double[] DiscreteCosineTransform1Dimension(double[] vector)
        {
            return null;
        }

        private static double MedianFilter(double[,] matrix)
        {
            return 0;
        }

        #region Utilities

        private static Matrix<RGBAColor> CreateRGBAMatrix(int rowsNumber, int colsNumber)
        {
            var matrixBuilder = Matrix<RGBAColor>.Build;
            return matrixBuilder.Dense(rowsNumber, colsNumber);
        }

        private static Matrix<RGBAColor> CreateRGBAMatrix(int size)
        {
            return CreateRGBAMatrix(size, size);
        }

        private static RGBAColor Color2RGBAColor(System.Drawing.Color color)
        {
            return new RGBAColor(color.R, color.G, color.B, color.A);
        }

        #endregion

        #region Types

        private struct RGBAColor : IEquatable<RGBAColor>, IFormattable
        {
            private int r;
            private int g;
            private int b;
            private int a;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="red"></param>
            /// <param name="green"></param>
            /// <param name="blue"></param>
            /// <param name="alpha"></param>
            public RGBAColor(int red, int green, int blue, int alpha)
            {
                if (!ValidateChannel(red))
                {
                    throw new ArgumentException("Invalid color channel value!", nameof(red));
                }
                if (!ValidateChannel(green))
                {
                    throw new ArgumentException("Invalid color channel value!", nameof(red));
                }
                if (!ValidateChannel(blue))
                {
                    throw new ArgumentException("Invalid color channel value!", nameof(red));
                }
                if (!ValidateChannel(alpha))
                {
                    throw new ArgumentException("Invalid color channel value!", nameof(red));
                }

                this.r = red;
                this.g = green;
                this.b = blue;
                this.a = alpha;
            }

            /// <summary>
            /// Gets or sets the red channel.
            /// </summary>
            public int Red
            {
                get { return this.r; }
                set
                {
                    ValidateChannel(value);
                    this.r = value;
                }
            }

            /// <summary>
            /// Gets or sets the green channel.
            /// </summary>
            public int Green
            {
                get { return this.g; }
                set
                {
                    ValidateChannel(value);
                    this.g = value;
                }
            }

            /// <summary>
            /// Gets or sets the blue channel.
            /// </summary>
            public int Blue
            {
                get { return this.b; }
                set
                {
                    ValidateChannel(value);
                    this.b = value;
                }
            }

            /// <summary>
            /// Gets or sets the alpha channel.
            /// </summary>
            public int Alpha
            {
                get { return this.a; }
                set
                {
                    ValidateChannel(value);
                    this.a = value;
                }
            }

            /// <summary>
            /// Gets the null color.
            /// </summary>
            public RGBAColor Zero
            {
                get { return new RGBAColor(0, 0, 0, 0); }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="other"></param>
            /// <returns></returns>
            public bool Equals(RGBAColor other)
            {
                throw new NotImplementedException();
            }

            private static bool ValidateChannel(int value)
            {
                return value >= 0 && value <= 255;
            }

            public string ToString(string format, IFormatProvider formatProvider)
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
