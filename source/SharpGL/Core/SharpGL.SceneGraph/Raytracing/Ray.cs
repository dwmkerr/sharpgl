using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL.SceneGraph.Core;

namespace SharpGL.SceneGraph.Raytracing
{
    /// <summary>
    /// A Ray.
    /// </summary>
    public class Ray
    {
        /// <summary>
        /// The light.
        /// </summary>
        public GLColor light = new GLColor(0, 0, 0, 0);

        /// <summary>
        /// The origin.
        /// </summary>
        public System.Numerics.Vector3 origin = new System.Numerics.Vector3();

        /// <summary>
        /// The direction.
        /// </summary>
        public System.Numerics.Vector3 direction = new System.Numerics.Vector3();
    }
}
