using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// Use thsi for perspective/ortho view matrix.
    /// <para>Typical usage: projection * view * model in GLSL.</para>
    /// </summary>
    public interface IScientificCamera : IPerspectiveViewCamera, IOrthoViewCamera, IViewCamera, IPerspectiveCamera, IOrthoCamera
    {
        /// <summary>
        /// camera's perspective type.
        /// </summary>
        ECameraType CameraType { get; set; }
    }
}
