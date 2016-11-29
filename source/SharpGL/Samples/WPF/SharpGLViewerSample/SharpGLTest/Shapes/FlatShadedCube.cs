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
    public class FlatShadedCube : Model3DBase
    {
        #region cube data
        readonly vec3[] _vertices = new vec3[]{ 
            new vec3(0.0f,0.0f,0.0f), new vec3(1.0f,0.0f,0.0f), new vec3(1.0f,1.0f,0.0f), new vec3(0.0f,1.0f,0.0f),
            new vec3(0.0f,0.0f,1.0f), new vec3(1.0f,0.0f,1.0f), new vec3(1.0f,1.0f,1.0f), new vec3(0.0f,1.0f,1.0f), 
            new vec3(0.0f,0.0f,0.0f), new vec3(0.0f,0.0f,1.0f), new vec3(0.0f,1.0f,1.0f), new vec3(0.0f,1.0f,0.0f), 
            new vec3(1.0f,0.0f,0.0f), new vec3(1.0f,0.0f,1.0f), new vec3(1.0f,1.0f,1.0f), new vec3(1.0f,1.0f,0.0f), 
            new vec3(0.0f,0.0f,0.0f), new vec3(1.0f,0.0f,0.0f), new vec3(1.0f,0.0f,1.0f), new vec3(0.0f,0.0f,1.0f), 
            new vec3(0.0f,1.0f,0.0f), new vec3(1.0f,1.0f,0.0f), new vec3(1.0f,1.0f,1.0f), new vec3(0.0f,1.0f,1.0f)
            };
        //readonly vec3[] _normals = new vec3[]{
        //    new vec3(0,0,-1),new vec3(0,0,-1),new vec3(0,0,-1),new vec3(0,0,-1),
        //    new vec3(0,0,1),new vec3(0,0,1),new vec3(0,0,1),new vec3(0,0,1),
        //    new vec3(-1,0,0),new vec3(-1,0,0),new vec3(-1,0,0),new vec3(-1,0,0),
        //    new vec3(1,0,0),new vec3(1,0,0),new vec3(1,0,0),new vec3(1,0,0),
        //    new vec3(0,-1,0),new vec3(0,-1,0),new vec3(0,-1,0),new vec3(0,-1,0),
        //    new vec3(0,1,0),new vec3(0,1,0),new vec3(0,1,0),new vec3(0,1,0),
        //};
        readonly ushort[] _indices = new ushort[]{
                1, 2,0,2,3,0,
                4,6,5, 4,7,6,
                8,10,9, 8,11,10,
                13,14,12, 14,15,12,
                16,18,17, 16,19,18,
                21,22,20, 22,23,20,
        };
        #endregion cube data

        public FlatShadedCube(OpenGL gl)
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
            gl.DrawElements(OpenGL.GL_TRIANGLES, Indices.Length, OpenGL.GL_UNSIGNED_SHORT, IntPtr.Zero);
        }
    }
}
