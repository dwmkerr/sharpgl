using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// This helps to get last vertex's id of picked primitive.
    /// </summary>
    public static class IColorCodedPickingHelper
    {
        /// <summary>
        /// Returns last vertex's id of picked geometry if the geometry represented by <paramref name="stageVertexID"/> belongs to this <paramref name="element"/> instance.
        /// <para>Returns false if <paramref name="stageVertexID"/> the primitive is in some other element.</para>
        /// </summary>
        /// <param name="element"></param>
        /// <param name="stageVertexID"></param>
        /// <param name="lastVertexID"></param>
        /// <returns></returns>
        public static bool GetLastVertexIDOfPickedGeometry(this IColorCodedPicking element, uint stageVertexID, out uint lastVertexID)
        {
            lastVertexID = uint.MaxValue;
            bool result = false;

            if (element != null)
            {
                if (stageVertexID < element.PickingBaseID) // ID is in some previous element.
                { return false; }

                uint vertexCount = element.GetVertexCount();
                uint id = stageVertexID - element.PickingBaseID;
                if (id < vertexCount)
                {
                    lastVertexID = id;
                    result = true;
                }
                else // ID is in some subsequent element.
                {
                    result = false;
                }
            }

            return result;
        }

        /// <summary>
        /// Get the primitive of <paramref name="element"/> according to vertex's id.
        /// <para>Returns <code>null</code> if <paramref name="element"/> is null or <paramref name="stageVertexID"/> is not in the range of this <paramref name="element"/>.</para>
        /// <para>Note: the <paramref name="stageVertexID"/> Refers to the last vertex that constructs the primitive. And it's unique in scene's all elements.</para>
        /// <para>Note: The result's positions property is not set up as there will be different kinds of storage mode for positions(float[], IntPtr, etc). You have to initialize the positions property and fill correct position information afterwards.</para>
        /// </summary>
        /// <typeparam name="T">Sub type of <see cref="IPickedGeometry"/></typeparam>
        /// <param name="element">the scene's element that contains the primitive.</param>
        /// <param name="mode">specifies what type of primitive it is.</param>
        /// <param name="stageVertexID">Refers to the last vertex that constructs the primitive. And it's unique in scene's all elements.</param>
        /// <returns></returns>
        public static T TryPick<T>(
            this IColorCodedPicking element, Enumerations.BeginMode mode, uint stageVertexID)
            where T : IPickedGeometry, new()
        {
            T pickedGeometry = default(T);

            if (element != null)
            {
                uint lastVertexID;
                if (element.GetLastVertexIDOfPickedGeometry(stageVertexID, out lastVertexID))
                {
                    pickedGeometry = new T();

                    pickedGeometry.GeometryType = mode.ToGeometryType();
                    pickedGeometry.StageVertexID = stageVertexID;
                    pickedGeometry.From = element;
                }
            }

            return pickedGeometry;
        }

        /// <summary>
        /// Get the primitive of <paramref name="element"/> according to vertex's id.
        /// <para>Returns <code>null</code> if <paramref name="element"/> is null or <paramref name="stageVertexID"/> does not belong to any of this <paramref name="element"/>'s vertices.</para>
        /// <para>Note: the <paramref name="stageVertexID"/> refers to the last vertex that constructs the primitive. And it's unique in scene's all elements.</para>
        /// </summary>
        /// <typeparam name="T">Subclass of <see cref="IPickedGeometry"/></typeparam>
        /// <param name="element">the scene's element that contains the primitive.</param>
        /// <param name="mode">specifies what type of primitive it is.</param>
        /// <param name="stageVertexID">Refers to the last vertex that constructs the primitive. And it's unique in scene's all elements.</param>
        /// <param name="positions">element's vertices' position array.</param>
        /// <returns></returns>
        public static T TryPick<T>(
            this IColorCodedPicking element, Enumerations.BeginMode mode, uint stageVertexID, float[] positions)
            where T : IPickedGeometry, new()
        {
            if (positions == null) { throw new ArgumentNullException("positions"); }

            T pickedGeometry = element.TryPick<T>(mode, stageVertexID);

            // Fill primitive's positions and colors. This maybe changes much more than lines above in second dev.
            if (pickedGeometry != null)
            {
                uint lastVertexID;
                if (element.GetLastVertexIDOfPickedGeometry(stageVertexID, out lastVertexID))
                {
                    int vertexCount = pickedGeometry.GeometryType.GetVertexCount();
                    if (vertexCount == -1) { vertexCount = positions.Length / 3; }
                    float[] geometryPositions = new float[vertexCount * 3];
                    uint i = lastVertexID * 3 + 2;
                    for (int j = (geometryPositions.Length - 1); j >= 0; i--, j--)
                    {
                        if (i == uint.MaxValue)// This is when mode is GL_LINE_LOOP.
                        { i = (uint)positions.Length - 1; }
                        geometryPositions[j] = positions[i];
                    }

                    pickedGeometry.positions = geometryPositions;
                }
            }

            return pickedGeometry;
        }


        /// <summary>
        /// Get geometry's index(start from 0) according to <paramref name="lastVertexID"/> and <paramref name="mode"/>.
        /// <para>Returns false if failed.</para>
        /// </summary>
        /// <param name="element"></param>
        /// <param name="mode"></param>
        /// <param name="lastVertexID">Refers to the last vertex that constructs the primitive.
        /// <para>Ranges from 0 to (<paramref name="element"/>'s vertices' count - 1).</para></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool GetGeometryIndex(this IColorCodedPicking element, SharpGL.Enumerations.BeginMode mode, uint lastVertexID, out uint index)
        {
            index = uint.MaxValue;
            if (element == null) { return false; }

            uint vertexCount = element.GetVertexCount();

            if (lastVertexID < vertexCount)
            {
                switch (mode)
                {
                    case SharpGL.Enumerations.BeginMode.Points:
                        // vertexID should range from 0 to vertexCount - 1.
                        index = lastVertexID;
                        break;
                    case SharpGL.Enumerations.BeginMode.Lines:
                        // vertexID should range from 0 to vertexCount - 1.
                        index = lastVertexID / 2;
                        break;
                    case SharpGL.Enumerations.BeginMode.LineLoop:
                        // vertexID should range from 0 to vertexCount.
                        if (lastVertexID == 0) // This is the last primitive.
                        { index = vertexCount - 1; }
                        else
                        { index = lastVertexID - 1; }
                        break;
                    case SharpGL.Enumerations.BeginMode.LineStrip:
                        index = lastVertexID - 1;// If lastVertexID is 0, this returns -1.
                        break;
                    case SharpGL.Enumerations.BeginMode.Triangles:
                        index = lastVertexID / 3;
                        break;
                    case SharpGL.Enumerations.BeginMode.TriangleString:
                        index = lastVertexID - 2;// if lastVertexID is 0 or 1, this returns -2 or -1.
                        break;
                    case SharpGL.Enumerations.BeginMode.TriangleFan:
                        index = lastVertexID - 2;// if lastVertexID is 0 or 1, this returns -2 or -1.
                        break;
                    case SharpGL.Enumerations.BeginMode.Quads:
                        index = lastVertexID / 4;
                        break;
                    case SharpGL.Enumerations.BeginMode.QuadStrip:
                        index = lastVertexID / 2 - 1;// If lastVertexID is 0 or 1, this returns -1.
                        break;
                    case SharpGL.Enumerations.BeginMode.Polygon:
                        index = 0;
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }

            return true;
        }

        /// <summary>
        /// Get geometry's count according to specified <paramref name="mode"/>.
        /// <para>Returns false if the <paramref name="element"/> is null.</para>
        /// </summary>
        /// <param name="element"></param>
        /// <param name="mode"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static bool GetGeometryCount(this IColorCodedPicking element, SharpGL.Enumerations.BeginMode mode, out uint count)
        {
            bool result = false;
            count = uint.MaxValue;

            if (element != null)
            {
                uint vertexCount = element.GetVertexCount();

                switch (mode)
                {
                    case SharpGL.Enumerations.BeginMode.Points:
                        count = vertexCount;
                        break;
                    case SharpGL.Enumerations.BeginMode.Lines:
                        count = vertexCount / 2;
                        break;
                    case SharpGL.Enumerations.BeginMode.LineLoop:
                        count = vertexCount;
                        break;
                    case SharpGL.Enumerations.BeginMode.LineStrip:
                        count = vertexCount - 1;
                        break;
                    case SharpGL.Enumerations.BeginMode.Triangles:
                        count = vertexCount / 3;
                        break;
                    case SharpGL.Enumerations.BeginMode.TriangleString:
                        count = vertexCount - 2;
                        break;
                    case SharpGL.Enumerations.BeginMode.TriangleFan:
                        count = vertexCount - 2;
                        break;
                    case SharpGL.Enumerations.BeginMode.Quads:
                        count = vertexCount / 4;
                        break;
                    case SharpGL.Enumerations.BeginMode.QuadStrip:
                        count = vertexCount / 2 - 1;
                        break;
                    case SharpGL.Enumerations.BeginMode.Polygon:
                        count = 1;
                        break;
                    default:
                        throw new NotImplementedException();
                }

                result = true;
            }

            return result;
        }
    }
}
