using SharpGL;
using SharpGL.SceneGraph.Assets;
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GlmNet;
using SharpGLBase;
using SharpGLBase.Primitives;
using SharpGLBase.Shaders;

namespace SharpGLTest.Shapes
{
    /// <summary>
    /// A simple cube object
    /// </summary>
    public class SmoothShadedCube : Model3DBase
    {
        #region cube data
        readonly vec3[] _vertices = new vec3[]{
                new vec3(-1.0f, -1.0f, -1.0f),
                new vec3(1.0f, -1.0f, -1.0f),
                new vec3(1.0f,  1.0f, -1.0f),
                new vec3(-1.0f, 1.0f, -1.0f),
                new vec3(-1.0f, -1.0f,  1.0f),
                new vec3(1.0f, -1.0f,  1.0f),
                new vec3(1.0f,  1.0f,  1.0f),
                new vec3(-1.0f,  1.0f,  1.0f)
            };

        readonly ushort[] _indices = new ushort[]{
                0, 4, 5, 0, 5, 1,
                1, 5, 6, 1, 6, 2,
                2, 6, 7, 2, 7, 3,
                3, 7, 4, 3, 4, 0,
                4, 7, 6, 4, 6, 5,
                3, 0, 1, 3, 1, 2
        };
        #endregion cube data

        public SmoothShadedCube(OpenGL gl)
        {
            base.Init(_vertices, _indices, null, gl);
        }

        /// <summary>
        /// Renders the cube to the screen.
        /// </summary>
        /// <param name="gl"></param>
        /// <param name="renderMode"></param>
        public override void Render(OpenGL gl, RenderMode renderMode, ExtShaderProgram shader = null)
        {
            // Load our cube data ClockWise.
            gl.FrontFace(OpenGL.GL_CW);

            // Binds buffers.
            base.Bind();

            // Draw the elements.
            gl.DrawElements(GlDrawMode, Indices.Length, OpenGL.GL_UNSIGNED_SHORT, IntPtr.Zero);
        }
    }
}
