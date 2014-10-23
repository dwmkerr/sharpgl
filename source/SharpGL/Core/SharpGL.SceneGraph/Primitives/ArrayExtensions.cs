using System;
using System.Runtime.InteropServices;

namespace SharpGL.SceneGraph.Primitives
{
    /// <summary>
    /// Extensions for Array type.
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Flattens the specified array.
        /// </summary>
        /// <typeparam name="T">The array type.</typeparam>
        /// <param name="array">The array.</param>
        /// <returns>The flattened array.</returns>
        public static T[] Flatten<T>(this T[,,] array)
            where T : struct
        {
            int size = Marshal.SizeOf(array[0, 0, 0]);
            int totalSize = Buffer.ByteLength(array);
            T[] result = new T[totalSize / size];
            Buffer.BlockCopy(array, 0, result, 0, totalSize);
            return result;
        }
    }
}
