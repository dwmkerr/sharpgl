using GlmNet;
using SharpGL;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.JOG;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SharpGLLinesSample
{
    public class Axis
    {

        #region fields
        #endregion fields

        #region properties
        public Tuple<vec3, vec3> LineX { get; set; }
        public Tuple<vec3, vec3> LineY { get; set; }
        public Tuple<vec3, vec3> LineZ { get; set; }
        #endregion properties

        #region events
        #endregion events

        #region constructors
        public Axis(float axisLength)
        {
            LineX = new Tuple<vec3, vec3>(new vec3(0, 0, 0), new vec3(axisLength, 0, 0));
            LineY = new Tuple<vec3, vec3>(new vec3(0, 0, 0), new vec3(0, axisLength, 0));
            LineZ = new Tuple<vec3, vec3>(new vec3(0, 0, 0), new vec3(0, 0, axisLength));
        }
        #endregion constructors
    }
}
