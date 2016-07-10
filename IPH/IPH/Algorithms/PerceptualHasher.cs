/// <summary>
/// PerceptualHasher.cs
/// Andrea Tino - 2016
/// </summary>

namespace IPH
{
    using System;

    using MathNet.Numerics.LinearAlgebra;

    /// <summary>
    /// 
    /// </summary>
    public class PerceptualHasher : IHasher
    {
        private const int Size = 64;

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
            Matrix<RGBAColor> resized = CreateRGBAMatrix(Size);

            hash = null;
        }

        #region Utilities

        private Matrix<RGBAColor> CreateRGBAMatrix(int size)
        {
            var matrixBuilder = Matrix<RGBAColor>.Build;
            return matrixBuilder.Dense(size, size);
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
