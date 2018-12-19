using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// Use thsi for view matrix.
    /// <para>Typical usage: projection * view * model in GLSL.</para>
    /// </summary>
    public interface IViewCamera
    {
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        [Description("The position of the camera"), Category("Camera")]
        Vertex Position { get; set; }

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        /// <value>
        /// The target.
        /// </value>
        [Description("The target of the camera (the point it's looking at)"), Category("Camera")]
        Vertex Target { get; set; }

        /// <summary>
        /// Gets or sets up vector.
        /// </summary>
        /// <value>
        /// Up vector.
        /// </value>
        [Description("The up direction, relative to camera. (Controls tilt)."), Category("Camera")]
        Vertex UpVector { get; set; }
    }
}
