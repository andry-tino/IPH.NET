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
    /// Implementation of perceptual hash as per <see href="http://phash.org/"/>.
    /// </summary>
    /// <remarks>
    /// The implementation has been validated by comparing with 
    /// algorithm <see href="https://github.com/jenssegers/imagehash/blob/master/src/Implementations/PerceptualHash.php"/>.
    /// </remarks>
    public class PerceptualHasher : IHasher
    {
        private const int Size = 64;
        private const int SubSize = 8; // Must be < Size

        private const double RedLUMAFactor = 0.299d;
        private const double GreenLUMAFactor = 0.587d;
        private const double BlueLUMAFactor = 0.114d;

        private Hash64Bit hash;

        /// <summary>
        /// Initializes a new instance of the <see cref="PerceptualHasher"/> class.
        /// </summary>
        public PerceptualHasher()
        {
        }

        /// <summary>
        /// Calculates the perceptual hash of an <see cref="Image"/>.
        /// </summary>
        /// <param name="image">The <see cref="Image"/> to hash.</param>
        /// <returns>The perceptual hash of the input <see cref="Image"/>.</returns>
        public IHash CalculateHash(Image image)
        {
            this.Compute(out this.hash, image);
            return this.hash;
        }

        private void Compute(out Hash64Bit hash, Image source)
        {
            // Get a copy of the image we need to compute the hash on
            // but resized to a default dimension Size
            // Attention: this will alter the proportions, but it is fine
            Image resizedImage = source.Resize(Size, Size);

            // Define quantities
            double[] row = new double[Size];
            double[][] rows = new double[Size][];

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
            resizedImage.Dispose();
            resizedImage = null;

            // More quantities
            double[] col = new double[Size];
            double[][] matrix = new double[Size][];

            // Calculate the DCT for each column
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    col[y] = rows[y][x];
                }
                matrix[x] = DiscreteCosineTransform1Dimension(col);
            }

            // Extract top SubSize x SubSize pixels into a linear array
            double[] pixels = new double[SubSize * SubSize];
            for (int y = 0, i = 0; y < SubSize; y++)
            {
                for (int x = 0; x < SubSize; x++)
                {
                    pixels[i++] = matrix[y][x];
                }
            }

            // Calculate median
            double median = MedianFilter(pixels);

            // Calculate final hash
            ulong rawHash = 0;
            ulong one = 1;

            for (int i = 0; i < pixels.Length; i++)
            {
                double pixel = pixels[i];
                if (pixel > median)
                {
                    rawHash |= one;
                }
                one = one << 1;
            }

            hash = new Hash64Bit(rawHash);
        }

        private static double[] DiscreteCosineTransform1Dimension(double[] vector)
        {
            double[] transformed = new double[vector.Length];
            int size = transformed.Length;
            double sizeD = size;

            for (int i = 0; i < size; i++)
            {
                double sum = 0;
                for (int j = 0; j < size; j++)
                {
                    sum += vector[j] * Math.Cos((double)i * Math.PI * (((double)j + 0.5d) / sizeD));
                }

                sum *= Math.Sqrt(2d / sizeD);

                if (i == 0)
                {
                    sum *= 1d / Math.Sqrt(2d);
                }

                transformed[i] = sum;
            }

            return transformed;
        }

        private static double MedianFilter(double[] array)
        {
            // Copy the array as we do not want to act on the original
            double[] clonedArray = new double[array.Length];
            for (int i = 0; i < clonedArray.Length; i++)
            {
                clonedArray[i] = array[i];
            }

            Array.Sort(clonedArray);

            int middle = (int)Math.Floor(clonedArray.Length / 2d);

            if (clonedArray.Length % 2 != 0)
            {
                return clonedArray[middle];
            }

            double low = clonedArray[middle];
            double high = clonedArray[middle + 1];
            return (low + high) / 2d;
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

        /// <summary>
        /// 
        /// </summary>
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
