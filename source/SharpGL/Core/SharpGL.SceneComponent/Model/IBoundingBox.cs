using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// Specify a cuboid that marks a model's edges.
    /// </summary>
    public interface IBoundingBox
    {
        /// <summary>
        /// Maximum position of this cuboid.
        /// </summary>
        Vertex MaxPosition { get; }

        /// <summary>
        /// Minimum position of this cuboid.
        /// </summary>
        Vertex MinPosition { get; }

        /// <summary>
        /// Get center position of this cuboid.
        /// </summary>
        /// <param name="x">x position.</param>
        /// <param name="y">y position.</param>
        /// <param name="z">z position.</param>
        void GetCenter(out float x, out float y, out float z);

        /// <summary>
        /// Gets the bound dimensions.
        /// </summary>
        /// <param name="x">The x size.</param>
        /// <param name="y">The y size.</param>
        /// <param name="z">The z size.</param>
        void GetBoundDimensions(out float xSize, out float ySize, out float zSize);

        /// <summary>
        /// Render to the provided instance of OpenGL.
        /// </summary>
        /// <param name="gl">The OpenGL instance.</param>
        /// <param name="renderMode">The render mode.</param>
        void Render(OpenGL gl, RenderMode renderMode);

        /// <summary>
        /// Only way to set bounding box'es values.
        /// </summary>
        /// <param name="minX"></param>
        /// <param name="minY"></param>
        /// <param name="minZ"></param>
        /// <param name="maxX"></param>
        /// <param name="maxY"></param>
        /// <param name="maxZ"></param>
        void Set(float minX, float minY, float minZ, float maxX, float maxY, float maxZ);
    }
}
