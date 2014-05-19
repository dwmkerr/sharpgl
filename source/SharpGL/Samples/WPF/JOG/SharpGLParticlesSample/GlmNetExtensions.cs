using GlmNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGLParticlesSample
{
    /// <summary>
    /// Provides matrix and vector extentions primarely for the GlmNET library. 
    /// </summary>
    public static class GlmNetExtensions
    {
        /// <summary>
        /// Deep copy of the mat4 object
        /// </summary>
        /// <param name="mat">The matrix</param>
        /// <returns>Deep copy of the input matrix.</returns>
        public static mat4 DeepCopy(this mat4 mat)
        {
            return new mat4(
                new vec4[]{
                    mat[0].DeepCopy(),
                    mat[1].DeepCopy(),
                    mat[2].DeepCopy(),
                    mat[3].DeepCopy(),
                });
        }

        /// <summary>
        /// Deep copy of a vec4 object
        /// </summary>
        /// <param name="v">The vector</param>
        /// <returns>Deep copy of the input vector.</returns>
        public static vec4 DeepCopy(this vec4 v)
        {
            return new vec4(v.x, v.y, v.z, v.w);
        }
        
        /// <summary>
        /// Copies everything except the z-value from the vector v into a new vec3.
        /// </summary>
        /// <param name="v">input vector</param>
        /// <returns></returns>
        public static vec3 ToVec3(this vec4 v)
        {
            return new vec3(v.x, v.y, v.z);
        }

        /// <summary>
        /// Puts the values from the matrix in a readable 4 line string, where each line defines 1 vector.
        /// BEWARE: if m contains null vectors, it will throw an exception. This exception is being caught here, but this might create performance issues.
        /// If exception is caught, this method will return an empty string( = "").
        /// </summary>
        /// <param name="m">The matrix.</param>
        /// <param name="round">Used for calling Math.Round(m[i][j], round)</param>
        /// <returns>A readable 4 line string, where each line defines 1 vector </returns>
        public static string ToValueString(this mat4 m, int round = 3)
        {
            var txt = "";
            try
            {
                for (int i = 0; i < 4; i++)
                {
                    var arr = m.to_array();
                    var mi = m[i];
                    txt += mi.ToValueString(round);
                    txt += "\n";
                }
            }
            catch (Exception) 
            {
                txt = "";
            }
            return txt;

        }

        /// <summary>
        /// Create a string from the values in the vec4.
        /// </summary>
        /// <param name="v">The vector.</param>
        /// <param name="round">Used for calling Math.Round(v[j], round).</param>
        /// <returns>A string from the values in the vec4.</returns>
        public static string ToValueString(this vec4 v, int round = 3)
        {
            string txt = "";
            for (int j = 0; j < 4; j++)
            {
                txt += Math.Round(v[j], round) + "\t";
            }

            return txt;
        }

        /// <summary>
        /// Create a string from the values in the vec3.
        /// </summary>
        /// <param name="v">The vector.</param>
        /// <param name="round">Used for calling Math.Round(v[j], round).</param>
        /// <returns>A string from the values in the vec3.</returns>
        public static string ToValueString(this vec3 v, int round = 3)
        {
            string txt = "";
            for (int j = 0; j < 3; j++)
            {
                txt += Math.Round(v[j], round) + "\t";
            }

            return txt;
        }

        /// <summary>
        /// Transposes the mat4.
        /// </summary>
        /// <param name="m">The 4x4 matrix.</param>
        /// <returns>The Transpose of the matrix.</returns>
        public static mat4 Transpose(this mat4 m)
        {
            vec4[] vecs = new vec4[4];

            for (int i = 0; i < vecs.Length; i++)
            {
                vecs[i] = new vec4();
                for (int j = 0; j < vecs.Length; j++)
                {
                    vecs[i][j] = m[j][i];
                }
            }

            return new mat4(vecs);
        }

        /// <summary>
        /// Concatenates every value in this matrix with it's corresponding value in the other one.
        /// </summary>
        /// <param name="m">This matrix</param>
        /// <param name="m2">Other matrix</param>
        /// <returns>The concatenated result.</returns>
        public static mat4 Concat(this mat4 m, mat4 m2)
        {
            vec4[] vecs = new vec4[4];

            for (int i = 0; i < vecs.Length; i++)
            {
                vecs[i] = new vec4();
                for (int j = 0; j < vecs.Length; j++)
                {
                    vecs[i][j] = m[i][j] + m2[i][j];
                }
            }

            return new mat4(vecs);
        }

        /// <summary>
        /// Not sure if this one works 100%, but might be more performant (if it's ever needed).
        /// Creates the Inverse of the matrix.
        /// </summary>
        /// <param name="mat">A 4x4 matrix.</param>
        /// <returns>The inversed matrix.</returns>
        public static mat4 Inverse2(this mat4 mat)
        {
            int n = 4;
            float[,] a = new float[n,n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    a[i,j] = mat[j][i];
                }
            }

            var s0 = a[0, 0] * a[1, 1] - a[1, 0] * a[0, 1];
            var s1 = a[0, 0] * a[1, 2] - a[1, 0] * a[0, 2];
            var s2 = a[0, 0] * a[1, 3] - a[1, 0] * a[0, 3];
            var s3 = a[0, 1] * a[1, 2] - a[1, 1] * a[0, 2];
            var s4 = a[0, 1] * a[1, 3] - a[1, 1] * a[0, 3];
            var s5 = a[0, 2] * a[1, 3] - a[1, 2] * a[0, 3];

            var c5 = a[2, 2] * a[3, 3] - a[3, 2] * a[2, 3];
            var c4 = a[2, 1] * a[3, 3] - a[3, 1] * a[2, 3];
            var c3 = a[2, 1] * a[3, 2] - a[3, 1] * a[2, 2];
            var c2 = a[2, 0] * a[3, 3] - a[3, 0] * a[2, 3];
            var c1 = a[2, 0] * a[3, 2] - a[3, 0] * a[2, 2];
            var c0 = a[2, 0] * a[3, 1] - a[3, 0] * a[2, 1];

            // Should check for 0 determinant
            var invdet = 1.0 / (s0 * c5 - s1 * c4 + s2 * c3 + s3 * c2 - s4 * c1 + s5 * c0);

            var b = mat4.identity();

            b[0, 0] = (float)((a[1, 1] * c5 - a[1, 2] * c4 + a[1, 3] * c3) * invdet);
            b[0, 1] = (float)((-a[0, 1] * c5 + a[0, 2] * c4 - a[0, 3] * c3) * invdet);
            b[0, 2] = (float)((a[3, 1] * s5 - a[3, 2] * s4 + a[3, 3] * s3) * invdet);
            b[0, 3] = (float)((-a[2, 1] * s5 + a[2, 2] * s4 - a[2, 3] * s3) * invdet);

            b[1, 0] = (float)((-a[1, 0] * c5 + a[1, 2] * c2 - a[1, 3] * c1) * invdet);
            b[1, 1] = (float)((a[0, 0] * c5 - a[0, 2] * c2 + a[0, 3] * c1) * invdet);
            b[1, 2] = (float)((-a[3, 0] * s5 + a[3, 2] * s2 - a[3, 3] * s1) * invdet);
            b[1, 3] = (float)((a[2, 0] * s5 - a[2, 2] * s2 + a[2, 3] * s1) * invdet);

            b[2, 0] = (float)((a[1, 0] * c4 - a[1, 1] * c2 + a[1, 3] * c0) * invdet);
            b[2, 1] = (float)((-a[0, 0] * c4 + a[0, 1] * c2 - a[0, 3] * c0) * invdet);
            b[2, 2] = (float)((a[3, 0] * s4 - a[3, 1] * s2 + a[3, 3] * s0) * invdet);
            b[2, 3] = (float)((-a[2, 0] * s4 + a[2, 1] * s2 - a[2, 3] * s0) * invdet);

            b[3, 0] = (float)((-a[1, 0] * c3 + a[1, 1] * c1 - a[1, 2] * c0) * invdet);
            b[3, 1] = (float)((a[0, 0] * c3 - a[0, 1] * c1 + a[0, 2] * c0) * invdet);
            b[3, 2] = (float)((-a[3, 0] * s3 + a[3, 1] * s1 - a[3, 2] * s0) * invdet);
            b[3, 3] = (float)((a[2, 0] * s3 - a[2, 1] * s1 + a[2, 2] * s0) * invdet);

            return b;
        }

        /// <summary>
        /// Creates the Inverse of the matrix.
        /// </summary>
        /// <param name="mat">A 4x4 matrix.</param>
        /// <returns>The inversed matrix.</returns>
        public static mat4 Inverse(this mat4 a)
        {
            int n = 4;
            float[][] arrA = new float[n][];
            float[][] arrInverse;
            mat4 inverse = mat4.identity();

            for (int i = 0; i < n; i++)
            {
                arrA[i] = new float[n];
                for (int j = 0; j < n; j++)
                {
                    arrA[i][j] = a[j][i];
                }
            }

            var d = Determinant(arrA, n);
            if (d != 0)
            {
                arrInverse = Cofactor(arrA, n);

                //float[][] to mat4
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        inverse[i, j] = arrInverse[i][j];
                    }
                }

                //test if result == I
                var res = a * inverse;


                return inverse;
            }
            else
            {
                throw new Exception("Matrix can't be inverted, determinant == 0.");
            }
        }

        /// <summary>
        /// For calculating Determinant of the Matrix.
        /// </summary>
        /// <param name="a">The matrix.</param>
        /// <param name="k">The order of the matrix (k = 3 => assuming a matrix of size 3x3)</param>
        /// <returns>The determinant.</returns>
        public static float Determinant(float[][] a, int k)
        {
            float s=1,det=0;
            float[][] b = new float[k][];


            for (int idx = 0; idx < b.Length; idx++)
			{
			    b[idx] = new float[k];
			}

            int m,n,c;
            if (k==1)
            {
                return (a[0][0]);
            }
            else
            {
                det=0;
                for (c=0;c<k;c++)
                {
                    m=0;
                    n=0;
                    for (int i = 0; i < k; i++)
                    {
                        for (int j = 0; j < k; j++)
                        {
                            b[i][j]=0;
                            if (i != 0 && j != c)
                            {
                                b[m][n]=a[i][j];
                                if (n<(k-2))
                                    n++;
                                else
                                {
                                    n=0;
                                    m++;
                                }
                            }
                        }
                    }
                    det=det + s * (a[0][c] * Determinant(b,k-1));
                    s=-1 * s;
                }
            }
 
            return (det);
        }

        /// <summary>
        /// Calculates the Cofactor of a matrix of the order f.
        /// </summary>
        /// <param name="a">The matrix.</param>
        /// <param name="f">The order of the matrix (f = 3 => assuming a matrix of size 3x3)</param>
        /// <returns>The cofactor.</returns>
        public static float[][] Cofactor(float[][] a, int f)
        {
            var b = new float[f][];
            var fac = new float[f][];

            for (int i = 0; i < f; i++)
            {
                b[i] = new float[f];
                fac[i] = new float[f];
            }


            int m,n;
            for (int q = 0; q < f; q++)
            {
                for (int p = 0; p < f; p++)
                {
                    m=0;
                    n=0;
                    for (int i = 0; i < f; i++)
                    {
                        for (int j = 0; j < f; j++)
                        {
                            if (i != q && j != p)
                            {
                                b[m][n]=a[i][j];
                                if (n<(f-2))
                                    n++;
                                else
                                {
                                    n=0;
                                    m++;
                                }
                            }
                        }
                    }
                    fac[q][p] = (float)Math.Pow(-1, q + p) * Determinant(b, f - 1);
                }
            }
            return Transpose(a, fac, f);
        }
        
        /// <summary>
        /// Finding the transpose of a matrix.
        /// </summary>
        /// <param name="a">The matrix</param>
        /// <param name="fac">The cofactor.</param>
        /// <param name="r">The order of the matrix (r = 3 => assuming a matrix of size 3x3)</param>
        /// <returns>The transpose.</returns>
        public static float[][] Transpose(float[][] a, float[][] fac, int r)
        {
            float[][] b = new float[r][],
                inverse = new float[r][];
            float d;

            for (int i = 0; i < r; i++)
            {
                b[i] = new float[r];
                inverse[i] = new float[r];
            }

            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < r; j++) 
                {
                    b[i][j]=fac[j][i];
                }
            }
            d = Determinant(a, r);
            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < r; j++)
                {
                    inverse[i][j] = b[i][j] / d;
                }
            }
            //The inverse of matrix is :
            return inverse;
        }

        public static vec3 Substract(this vec3 v1, vec3 v2)
        {
            return new vec3(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        }

        /// <summary>
        /// Multiplies a 4x1 vector with a 4x4 transformation matrix.
        /// </summary>
        /// <param name="vec">The 4x1 vector.</param>
        /// <param name="mat">The 4x4 matrix.</param>
        /// <returns></returns>
        public static vec4 Multiply (this vec4 vec, mat4 mat)
        {
            var pos = new vec4();
            for (int i = 0; i < 4; i++)
            {
                float newPosVal = 0.0f;
                for (int j = 0; j < 4; j++)
                {
                    newPosVal += vec[j] * mat[j][i];
                }
                pos[i] = newPosVal;
            }

            return pos;
        }
    }
}
