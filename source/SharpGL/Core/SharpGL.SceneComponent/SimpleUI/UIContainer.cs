using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent.SimpleUI
{
    /// <summary>
    /// 
    /// </summary>
    public class UIContainer: SceneElement, IRenderable, IOrthoInfo
    {
        public ScientificCamera Camera { get; set; }
        public double zNear = -1000;
        public double zFar = 1000;

        #region IViewportInfo 成员

        SceneGraph.Vertex IOrthoInfo.LeftBottom { get; set; }

        SceneGraph.Vertex IOrthoInfo.RightBottom { get; set; }

        SceneGraph.Vertex IOrthoInfo.LeftTop { get; set; }

        SceneGraph.Vertex IOrthoInfo.RightTop { get; set; }

        float IOrthoInfo.Width { get; set; }

        float IOrthoInfo.Height { get; set; }

        #endregion

        #region IRenderable 成员

        void IRenderable.Render(OpenGL gl, RenderMode renderMode)
        {
            ScientificCamera camera = this.Camera;
            if (camera == null) { return; }

            IOrthoCamera orthoCamera = camera;
            double width = orthoCamera.Right - orthoCamera.Left;
            double height = orthoCamera.Top - orthoCamera.Bottom;

            Vertex back = camera.Position - camera.Target;
            Vertex right = camera.UpVector.VectorProduct(back);
            Vertex up = back.VectorProduct(right);
            back.Normalize();
            right.Normalize();
            up.Normalize();
            right.X *= (float)(height / 2);
            right.Y *= (float)(height / 2);
            right.Z *= (float)(height / 2);
            up.X *= (float)(width / 2);
            up.Y *= (float)(width / 2);
            up.Z *= (float)(width / 2);

            IOrthoInfo viewportInfo = this;
            viewportInfo.LeftBottom = camera.Position - right - up;
            viewportInfo.LeftTop = camera.Position - right + up;
            viewportInfo.RightBottom = camera.Position + right - up;
            viewportInfo.RightTop = camera.Position + right + up;
            viewportInfo.Width = (float)width;
            viewportInfo.Height = (float)height;

            gl.MatrixMode(SharpGL.Enumerations.MatrixMode.Projection);
            gl.LoadIdentity();
            gl.Ortho(orthoCamera.Left, orthoCamera.Right, orthoCamera.Bottom, orthoCamera.Top, orthoCamera.Near, orthoCamera.Far);
            gl.LookAt(
                (double)camera.Position.X, (double)camera.Position.Y, (double)camera.Position.Z,
                (double)camera.Target.X, (double)camera.Target.Y, (double)camera.Target.Z,
                (double)camera.UpVector.X, (double)camera.UpVector.Y, (double)camera.UpVector.Z);
            gl.MatrixMode(Enumerations.MatrixMode.Modelview);
        }

        #endregion
    }
}
