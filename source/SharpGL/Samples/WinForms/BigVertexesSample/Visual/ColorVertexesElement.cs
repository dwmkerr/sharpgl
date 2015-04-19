using SharpGL;
using SharpGL.SceneGraph.Core;
using SharpGL.VertexBuffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestVAO.Model;

namespace TestVAO.Visual
{


    public class ColorVertexesElement : SceneElement,IRenderable,IDisposable
    {
        private ColorVertexes _colorVertexes;

        
        public ColorVertexesElement(ColorVertexes particles)
        {
            this._colorVertexes = particles;
        }


        public void Render(SharpGL.OpenGL gl, RenderMode renderMode)
        {
           

            if (this._colorVertexes.Size <= 0)
                return;

            unsafe
            {
                gl.Enable(OpenGL.GL_DEPTH_TEST);
                gl.Enable(0X8861);
                
                gl.EnableClientState(OpenGL.GL_VERTEX_ARRAY);
                gl.EnableClientState(OpenGL.GL_COLOR_ARRAY);

                gl.VertexPointer(3, OpenGL.GL_FLOAT, 0, (IntPtr)this._colorVertexes.Centers);
                gl.ColorPointer(3, OpenGL.GL_BYTE, 0, (IntPtr)this._colorVertexes.Colors);

                gl.DrawArrays(OpenGL.GL_POINTS, 0, this._colorVertexes.Size);

                gl.DisableClientState(OpenGL.GL_VERTEX_ARRAY);
                gl.DisableClientState(OpenGL.GL_COLOR_ARRAY);
            }
        }

        public void Dispose()
        {
          
           
           
        }

        protected void Dispose(bool disposing)
        {
        }
    }
}
