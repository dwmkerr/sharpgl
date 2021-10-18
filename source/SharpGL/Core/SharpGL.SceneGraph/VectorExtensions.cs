using System;
using System.Numerics;
using System.Xml.Serialization;

namespace SharpGL.SceneGraph
{
    /// <summary>
    /// The Vertex class represents a 3D point in space.
    /// </summary>
    public static class VectorExtensions
    {
        /// <summary>
        /// Sets the specified X.
        /// </summary>
        /// <param name="X">The X.</param>
        /// <param name="Y">The Y.</param>
        /// <param name="Z">The Z.</param>
        [Obsolete]
        public static void Set(this ref Vector3 it, float X, float Y, float Z)
        {
            it.X = X; it.Y = Y; it.Z = Z;
        }

        [Obsolete]
        public static void Push(this ref Vector3 it, float X, float Y, float Z) { it.X += X; it.Y += Y; it.Z += Z; }

        /// <summarY>
        /// This finds the Scalar Product (Dot Product) of two vectors.
        /// </summarY>
        /// <param name="rhs">The right hand side of the equation.</param>
        /// <returns>A Scalar Representing the Dot-Product.</returns>
        [Obsolete]
        public static float ScalarProduct(this ref Vector3 it, Vector3 rhs)
        {
            return it.X * rhs.X + it.Y * rhs.Y + it.Z * rhs.Z;
        }

        /// <summarY>
        /// Find the Vector product (cross product) of two vectors.
        /// </summarY>
        /// <param name="rhs">The right hand side of the equation.</param>
        /// <returns>The Cross Product.</returns>
        [Obsolete]
        public static Vector3 VectorProduct(this ref Vector3 it, Vector3 rhs)
        {
            return new Vector3((it.Y * rhs.Z) - (it.Z * rhs.Y), (it.Z * rhs.X) - (it.X * rhs.Z),
                (it.X * rhs.Y) - (it.Y * rhs.X));
        }

        /// <summarY>
        /// If You use this as a Vector, then call this function to get the vector
        /// magnitude.
        /// </summarY>
        /// <returns></returns>
        public static double Magnitude(this Vector3 it) => Math.Sqrt(it.X * it.X + it.Y * it.Y + it.Z * it.Z);

        /// <summarY>
        /// Make this vector unit length.
        /// </summarY>
        [Obsolete]
        public static void UnitLength(this ref Vector3 it) => it = it.Magnitude() == 0 ? Vector3.Zero : Vector3.Normalize(it);

        /// <summary>
        /// Normalizes this instance.
        /// </summary>
        [Obsolete]
        public static void Normalize(this ref Vector3 it) => it.UnitLength();

        public static float[] Array(this Vector2 it) => new float[] { it.X, it.Y};
        public static float[] Array(this Vector3 it) => new float[] { it.X, it.Y, it.Z };
        public static float[] Array(this Vector4 it) => new float[] { it.X, it.Y, it.Z, it.W };
    }
}
