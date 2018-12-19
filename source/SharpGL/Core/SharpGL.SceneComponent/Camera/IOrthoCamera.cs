using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// Use this for ortho projection.
    /// <para>Typical usage: projection * view * model in GLSL.</para>
    /// </summary>
    public interface IOrthoCamera
    {
        /// <summary>
        /// Gets or sets the left relative to camera's position.
        /// </summary>
        /// <value>
        /// The left.
        /// </value>
        double Left { get; set; }

        /// <summary>
        /// Gets or sets the right relative to camera's position.
        /// </summary>
        /// <value>
        /// The right.
        /// </value>
        double Right { get; set; }

        /// <summary>
        /// Gets or sets the bottom relative to camera's position.
        /// </summary>
        /// <value>
        /// The bottom.
        /// </value>
        double Bottom { get; set; }

        /// <summary>
        /// Gets or sets the top relative to camera's position.
        /// </summary>
        /// <value>
        /// The top.
        /// </value>
        double Top { get; set; }

        /// <summary>
        /// Gets or sets the near.
        /// </summary>
        /// <value>
        /// The near.
        /// </value>
        double Near { get; set; }

        /// <summary>
        /// Gets or sets the far.
        /// </summary>
        /// <value>
        /// The far.
        /// </value>
        double Far { get; set; }
    }
}
