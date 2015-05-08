using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// interface for the model shown in <see cref="ScientificVisual3DControl"/>.
    /// </summary>
    public interface IScientificModel
    {
        /// <summary>
        /// Move from (0, 0, 0) to Translate, and you are at model's center in world coordiante.
        /// </summary>
        Vertex Translate { get; set; }

        /// <summary>
        /// Render this model.
        /// </summary>
        /// <param name="gl"></param>
        /// <param name="renderMode"></param>
        void Render(SharpGL.OpenGL gl, RenderMode renderMode);

        /// <summary>
        /// Adjust camera's position, target, etc according to model.
        /// </summary>
        /// <param name="gl"></param>
        /// <param name="camera"></param>
        void AdjustCamera(SharpGL.OpenGL gl, SharpGL.SceneGraph.Cameras.Camera camera);
    }
}
