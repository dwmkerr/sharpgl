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
    class ScientificCamera : SharpGL.SceneGraph.Cameras.Camera
    {
        public ScientificCamera(ECameraType cameraType = ECameraType.Perspecitive)
        {
            Name = "Scientific Camera";
            this.FieldOfView = 60f;
            this.AspectRatio = 1f;
            this.Near = 0.5f;
            this.Far = 40f;
            this.Target = new Vertex(0, 0, 0);
            this.UpVector = new Vertex(0, 1, 0);
            this.Position = new Vertex(0, 0, 0);
            this.Left = -100;
            this.Right = 100;
            this.Bottom = -100;
            this.Top = 100;
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
                    gl.Perspective(FieldOfView, AspectRatio, Near, Far);
                    break;
                case ECameraType.Ortho:
                    gl.Ortho(Left, Right, Bottom, Top, Near, Far);
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
        /// Gets or sets the field of view.
        /// </summary>
        /// <value>
        /// The field of view.
        /// </value>
        [Description("The angle of the lense of the camera (60 degrees = human eye)."), Category("Camera (Perspective)")]
        public double FieldOfView { get; set; }

        /// <summary>
        /// Gets or sets the near.
        /// </summary>
        /// <value>
        /// The near.
        /// </value>
        [Description("The near clipping distance."), Category("Camera (Perspective)")]
        public double Near { get; set; }

        /// <summary>
        /// Gets or sets the far.
        /// </summary>
        /// <value>
        /// The far.
        /// </value>
        [Description("The left clipping distance."), Category("Camera (Perspective)")]
        public double Far { get; set; }

        /// <summary>
        /// Gets or sets the far.
        /// </summary>
        /// <value>
        /// The far.
        /// </value>
        [Description("The far clipping distance."), Category("Camera (Ortho)")]
        public double Left { get; set; }

        /// <summary>
        /// Gets or sets the far.
        /// </summary>
        /// <value>
        /// The far.
        /// </value>
        [Description("The far clipping distance."), Category("Camera (Ortho)")]
        public double Right { get; set; }

        /// <summary>
        /// Gets or sets the far.
        /// </summary>
        /// <value>
        /// The far.
        /// </value>
        [Description("The far clipping distance."), Category("Camera (Ortho)")]
        public double Bottom { get; set; }

        /// <summary>
        /// Gets or sets the far.
        /// </summary>
        /// <value>
        /// The far.
        /// </value>
        [Description("The far clipping distance."), Category("Camera (Ortho)")]
        public double Top { get; set; }

        /// <summary>
        /// camera's perspective type.
        /// </summary>
        public ECameraType CameraType { get; set; }
    }
}
