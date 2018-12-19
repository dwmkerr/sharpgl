using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// Rotates a camera on a sphere, whose center is camera's Target.
    /// <para>Just like a satellite moves around a fixed star.</para>
    /// </summary>
    public class SatelliteRotation : IMouseRotation
    {
        private Point downPosition = new Point();
        private Size bound = new Size();
        public bool mouseDownFlag = false;
        private float horizontalRotationFactor = 4;
        private float verticalRotationFactor = 4;
        private SharpGL.SceneGraph.Vertex up;
        private SharpGL.SceneGraph.Vertex back;
        private SharpGL.SceneGraph.Vertex right;

        /// <summary>
        /// Rotates a camera on a sphere, whose center is camera's Target.
        /// <para>Just like a satellite moves around a fixed star.</para> 
        /// </summary>
        /// <param name="camera"></param>
        public SatelliteRotation(IScientificCamera camera = null)
        {
            this.Camera = camera;
        }


        public override string ToString()
        {
            return string.Format("back:{0}|{3:0.00},up:{1}|{4:0.00},right:{2}|{5:0.00}",
                FormatVertex(back), FormatVertex(up), FormatVertex(right), back.Magnitude(), up.Magnitude(), right.Magnitude());
            //return base.ToString();
        }

        private string FormatVertex(Vertex v)
        {
            return string.Format("{0:0.00},{1:0.00},{2:0.00}",
                v.X, v.Y, v.Z);
        }


        #region IRotation 成员

        private IScientificCamera originalCamera;

        public IScientificCamera Camera { get; set; }

        public void MouseUp(int x, int y)
        {
            this.mouseDownFlag = false;
        }

        public void MouseMove(int x, int y)
        {
            if (this.mouseDownFlag)
            {
                IViewCamera camera = this.Camera;
                if (camera == null) { return; }

                Vertex back = this.back;
                Vertex right = this.right;
                Vertex up = this.up;
                Size bound = this.bound;
                Point downPosition = this.downPosition;
                {
                    float deltaX = -horizontalRotationFactor * (x - downPosition.X) / bound.Width;
                    float cos = (float)Math.Cos(deltaX);
                    float sin = (float)Math.Sin(deltaX);
                    Vertex newBack = new Vertex(
                        back.X * cos + right.X * sin,
                        back.Y * cos + right.Y * sin,
                        back.Z * cos + right.Z * sin);
                    back = newBack;
                    right = up.VectorProduct(back);
                    back.Normalize();
                    right.Normalize();
                }
                {
                    float deltaY = verticalRotationFactor * (y - downPosition.Y) / bound.Height;
                    float cos = (float)Math.Cos(deltaY);
                    float sin = (float)Math.Sin(deltaY);
                    Vertex newBack = new Vertex(
                        back.X * cos + up.X * sin,
                        back.Y * cos + up.Y * sin,
                        back.Z * cos + up.Z * sin);
                    back = newBack;
                    up = back.VectorProduct(right);
                    back.Normalize();
                    up.Normalize();
                }

                camera.Position = camera.Target +
                    back * (float)((camera.Position - camera.Target).Magnitude());
                camera.UpVector = up;
                this.back = back;
                this.right = right;
                this.up = up;
                this.downPosition.X = x;
                this.downPosition.Y = y;
            }
        }

        public void SetBounds(int width, int height)
        {
            this.bound.Width = width;
            this.bound.Height = height;
        }

        public void MouseDown(int x, int y)
        {
            this.downPosition.X = x;
            this.downPosition.Y = y;
            this.mouseDownFlag = true;
            PrepareCamera();
        }

        private void PrepareCamera()
        {
            var camera = this.Camera;
            if (camera != null)
            {
                Vertex back = camera.Position - camera.Target;
                Vertex right = Camera.UpVector.VectorProduct(back);
                Vertex up = back.VectorProduct(right);
                back.Normalize();
                right.Normalize();
                up.Normalize();

                this.back = back;
                this.right = right;
                this.up = up;

                if (this.originalCamera == null)
                { this.originalCamera = new ScientificCamera(); }
                this.originalCamera.Position = camera.Position;
                this.originalCamera.UpVector = camera.UpVector;
            }
        }

        public void ResetRotation()
        {
            IViewCamera camera = this.Camera;
            if (camera == null) { return; }
            IViewCamera originalCamera = this.originalCamera;
            if (originalCamera == null) { return; }

            camera.Position = originalCamera.Position;
            camera.UpVector = originalCamera.UpVector;
        }

        #endregion
    }
}
