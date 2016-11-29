using GlmNet;
using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGLBase.Primitives
{
    public static class PrimitivesGlobal
    {
        public static void RenderLines(OpenGL gl, List<Tuple<vec3, vec3>> lines)
        {
            foreach (var item in lines)
            {
                vec3 v1 = item.Item1;
                vec3 v2 = item.Item2;
                gl.Vertex(v1.x, v1.y, v1.z);
                gl.Vertex(v2.x, v2.y, v2.z);
            }
        }
    }
}
