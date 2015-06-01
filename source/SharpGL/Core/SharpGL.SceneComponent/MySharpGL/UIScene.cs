using SharpGL.SceneGraph.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// Use UIScene's Draw() method to draw UI elements after model's scene.Draw() is done.
    /// </summary>
    internal class UIScene : MyScene
    {
        public override void Draw(SceneGraph.Cameras.Camera camera = null)
        {
            var gl = OpenGL;
            if (gl == null) { return; }

            //  TODO: we must decide what to do about drawing - are 
            //  cameras completely outside of the responsibility of the scene?
            //  If no camera has been provided, use the current one.
            ScientificCamera currentCamera = this.CurrentCamera;
            currentCamera.Project(gl, ECameraType.Ortho);

            //gl.BindTexture(OpenGL.GL_TEXTURE_2D, 0);

            //  Render the root element, this will then render the whole
            //  of the scene tree.
            MyRenderElement(SceneContainer, gl, RenderMode.Design);

            //  TODO: Adding this code here re-enables textures- it should work without it but it
            //  doesn't, look into this.
            //gl.BindTexture(OpenGL.GL_TEXTURE_2D, 0);
            //gl.Enable(OpenGL.GL_TEXTURE_2D);

            gl.Flush();
        }

    }
}