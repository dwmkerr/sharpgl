using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// projects in perspective view or ortho view.
    /// </summary>
    public class ScientificCamera : SharpGL.SceneGraph.Cameras.Camera,
        IScientificCamera,
        IPerspectiveViewCamera, IOrthoViewCamera, 
        IViewCamera, IPerspectiveCamera, IOrthoCamera
    {
        static int count = 0;
        public override string ToString()
        {
            return string.Format("{0}/{1}", Name, count);
            //return base.ToString();
        }
        public ScientificCamera(ECameraType cameraType = ECameraType.Perspecitive)
        {
            Name = "Scientific Camera: " + count++;
            IPerspectiveCamera perspectiveCamera = this;
            perspectiveCamera.FieldOfView = 60f;
            perspectiveCamera.AspectRatio = 1f;
            perspectiveCamera.Near = 0.01;
            perspectiveCamera.Far = 1000;

            IOrthoCamera orthoCamera = this;
            orthoCamera.Left = -100;
            orthoCamera.Right = 100;
            orthoCamera.Bottom = -100;
            orthoCamera.Top = 100;
            orthoCamera.Near = -1000;
            orthoCamera.Far = 1000;

            this.Target = new Vertex(0, 0, 0);
            this.UpVector = new Vertex(0, 1, 0);
            this.Position = new Vertex(0, 0, 0);

            this.CameraType = cameraType;
        }

        public override void Project(OpenGL gl)
        {
            //	Load the projection identity matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();

            //	Perform the projection.
            TransformProjectionMatrix(gl);

            ////	Get the matrix.
            //float[] matrix = new float[16];
            //gl.GetFloat(OpenGL.GL_PROJECTION_MATRIX, matrix);
            //for (int i = 0; i < 4; i++)
            //    for (int j = 0; j < 4; j++)
            //        projectionMatrix[i, j] = matrix[(i * 4) + j];

            //	Back to the modelview matrix.
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }
        /// <summary>
        /// This is the class' main function, to override this function and perform a 
        /// perspective transformation.
        /// </summary>
        public override void TransformProjectionMatrix(OpenGL gl)
        {
            //  Perform the look at transformation.
            switch (CameraType)
            {
                case ECameraType.Perspecitive:
                    IPerspectiveCamera perspectiveCamera = this;
                    gl.Perspective(perspectiveCamera.FieldOfView, perspectiveCamera.AspectRatio, perspectiveCamera.Near, perspectiveCamera.Far);
                    break;
                case ECameraType.Ortho:
                    IOrthoCamera orthoCamera = this;
                    gl.Ortho(orthoCamera.Left, orthoCamera.Right, orthoCamera.Bottom, orthoCamera.Top, orthoCamera.Near, orthoCamera.Far);
                    break;
                default:
                    break;
            }
            gl.LookAt((double)Position.X, (double)Position.Y, (double)Position.Z,
                (double)Target.X, (double)Target.Y, (double)Target.Z,
                (double)UpVector.X, (double)UpVector.Y, (double)UpVector.Z);
        }

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        /// <value>
        /// The target.
        /// </value>
        [Description("The target of the camera (the point it's looking at)"), Category("Camera")]
        public Vertex Target { get; set; }

        /// <summary>
        /// Gets or sets up vector.
        /// </summary>
        /// <value>
        /// Up vector.
        /// </value>
        [Description("The up direction, relative to camera. (Controls tilt)."), Category("Camera")]
        public Vertex UpVector { get; set; }

        /// <summary>
        /// camera's perspective type.
        /// </summary>
        public ECameraType CameraType { get; set; }

        #region IPerspectiveCamera 成员

        double IPerspectiveCamera.FieldOfView { get; set; }

        double IPerspectiveCamera.AspectRatio
        {
            get { return base.AspectRatio; }
            set { base.AspectRatio = value; }
        }

        double IPerspectiveCamera.Near { get; set; }

        double IPerspectiveCamera.Far { get; set; }

        #endregion

        #region IOrthoCamera 成员

        double IOrthoCamera.Left { get; set; }

        double IOrthoCamera.Right { get; set; }

        double IOrthoCamera.Bottom { get; set; }

        double IOrthoCamera.Top { get; set; }

        double IOrthoCamera.Near { get; set; }

        double IOrthoCamera.Far { get; set; }

        #endregion

        public void Scale(int delta)
        {
            ScientificCamera camera = this;
            if (camera.CameraType == ECameraType.Perspecitive)
            {
                var target2Position = camera.Position - camera.Target;
                if (target2Position.Magnitude () < 0.01)
                {
                    target2Position.Normalize();
                    target2Position.X *= 0.01f;
                    target2Position.Y *= 0.01f;
                    target2Position.Z *= 0.01f;
                }
                var scaledTarget2Position = target2Position * (1 - delta * 0.001f);
                camera.Position = camera.Target + scaledTarget2Position;
                double lengthDiff = scaledTarget2Position.Magnitude() - target2Position.Magnitude();
                // Increase ortho camera's Near/Far property in case the camera's position changes too much.
                IPerspectiveCamera perspectiveCamera = camera;
                perspectiveCamera.Far += lengthDiff;
                //perspectiveCamera.Near += lengthDiff;
                IOrthoCamera orthoCamera = camera;
                orthoCamera.Far += lengthDiff;
                orthoCamera.Near += lengthDiff;
            }
            else if (camera.CameraType == ECameraType.Ortho)
            {
                IOrthoCamera orthoCamera = camera;
                double distanceX = orthoCamera.Right - orthoCamera.Left;
                double distanceY = orthoCamera.Top - orthoCamera.Bottom;
                double centerX = (orthoCamera.Left + orthoCamera.Right) / 2;
                double centerY = (orthoCamera.Bottom + orthoCamera.Top) / 2;
                orthoCamera.Left = centerX - distanceX * (1 - delta * 0.001) / 2;
                orthoCamera.Right = centerX + distanceX * (1 - delta * 0.001) / 2;
                orthoCamera.Bottom = centerY - distanceY * (1 - delta * 0.001) / 2;
                orthoCamera.Top = centerX + distanceY * (1 - delta * 0.001) / 2;
            }
        }
    }
}
