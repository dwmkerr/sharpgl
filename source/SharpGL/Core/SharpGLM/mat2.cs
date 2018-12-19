using System;
using System.Linq;

namespace GlmNet
{
    /// <summary>
    /// Represents a 2x2 matrix.
    /// </summary>
    public struct mat2
    {
        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="mat2"/> struct.
        /// This matrix is the identity matrix scaled by <paramref name="scale"/>.
        /// </summary>
        /// <param name="scale">The scale.</param>
        public mat2(float scale)
        {
            cols = new[]
            {
                new vec2(scale, 0.0f),
                new vec2(0.0f, scale)
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="mat2"/> struct.
        /// The matrix is initialised with the <paramref name="cols"/>.
        /// </summary>
        /// <param name="cols">The colums of the matrix.</param>
        public mat2(vec2[] cols)
        {
            this.cols = new[]
            {
                cols[0],
                cols[1]
            };
        }

        public mat2(vec2 a, vec2 b)
        {
            this.cols = new[]
            {
                a, b
            };
        }

        public mat2(float a, float b, float c, float d)
        {
            this.cols = new[]
            {
                new vec2(a,b), new vec2(c,d)
            };
        }

        /// <summary>
        /// Creates an identity matrix.
        /// </summary>
        /// <returns>A new identity matrix.</returns>
        public static mat2 identity()
        {
            return new mat2
            {
                cols = new[] 
                {
                    new vec2(1,0),
                    new vec2(0,1)
                }
            };
        }

        #endregion

        #region Index Access

        /// <summary>
        /// Gets or sets the <see cref="vec2"/> column at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="vec2"/> column.
        /// </value>
        /// <param name="column">The column index.</param>
        /// <returns>The column at index <paramref name="column"/>.</returns>
        public vec2 this[int column]
        {
            get { return cols[column]; }
            set { cols[column] = value; }
        }

        /// <summary>
        /// Gets or sets the element at <paramref name="column"/> and <paramref name="row"/>.
        /// </summary>
        /// <value>
        /// The element at <paramref name="column"/> and <paramref name="row"/>.
        /// </value>
        /// <param name="column">The column index.</param>
        /// <param name="row">The row index.</param>
        /// <returns>
        /// The element at <paramref name="column"/> and <paramref name="row"/>.
        /// </returns>
        public float this[int column, int row]
        {
            get { return cols[column][row]; }
            set { cols[column][row] = value; }
        }

        #endregion

        #region Conversion

        /// <summary>
        /// Returns the matrix as a flat array of elements, column major.
        /// </summary>
        /// <returns></returns>
        public float[] to_array()
        {
            return cols.SelectMany(v => v.to_array()).ToArray();
        }

        #endregion

        #region Multiplication

        /// <summary>
        /// Multiplies the <paramref name="lhs"/> matrix by the <paramref name="rhs"/> vector.
        /// </summary>
        /// <param name="lhs">The LHS matrix.</param>
        /// <param name="rhs">The RHS vector.</param>
        /// <returns>The product of <paramref name="lhs"/> and <paramref name="rhs"/>.</returns>
        public static vec2 operator *(mat2 lhs, vec2 rhs)
        {
            return new vec2(
                lhs[0,0] * rhs[0] + lhs[1,0] * rhs[1], 
                lhs[0,1] * rhs[0] + lhs[1,1] * rhs[1]
            );
        }

        /// <summary>
        /// Multiplies the <paramref name="lhs"/> matrix by the <paramref name="rhs"/> matrix.
        /// </summary>
        /// <param name="lhs">The LHS matrix.</param>
        /// <param name="rhs">The RHS matrix.</param>
        /// <returns>The product of <paramref name="lhs"/> and <paramref name="rhs"/>.</returns>
        public static mat2 operator *(mat2 lhs, mat2 rhs)
        {
            return new mat2(new[]
            {
			    lhs[0][0] * rhs[0] + lhs[1][0] * rhs[1],
			    lhs[0][1] * rhs[0] + lhs[1][1] * rhs[1]
            });
        }

        public static mat2 operator * (mat2 lhs, float s)
        {
            return new mat2(new[]
            {
                lhs[0]*s,
                lhs[1]*s
            });
        }

        #endregion

        /// <summary>
        /// The columms of the matrix.
        /// </summary>
        private vec2[] cols;
    }
}