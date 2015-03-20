using System;
using System.ComponentModel;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Lighting;

namespace SharpGL.SceneGraph.Cameras
{
	/// <summary>
	/// The ArcBall camera supports arcball projection, making it ideal for use with a mouse.
	/// </summary>
	[Serializable()]
	public class ArcBallCamera : PerspectiveCamera
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="PerspectiveCamera"/> class.
        /// </summary>
        public ArcBallCamera(float eyex, float eyey, float eyez,
            float centerx, float centery, float centerz,
            float upx, float upy, float upz)
        {
            Name = "Camera (ArcBall)";
            this.arcBall = new ArcBall(eyex, eyey, eyez, centerx, centery, centerz, upx, upy, upz);
            this.Position = new Vertex(eyex, eyey, eyez);
            this.Target = new Vertex(centerx, centery, centerz);
            this.Up = new Vertex(upx, upy, upz);
        }

        /// <summary>
        /// This is the class' main function, to override this function and perform a 
        /// perspective transformation.
        /// </summary>
        public override void TransformProjectionMatrix(OpenGL gl)
        {
            int[] viewport = new int[4];
            gl.GetInteger(OpenGL.GL_VIEWPORT, viewport);

            //  Perform the perspective transformation.
            arcBall.SetBounds(viewport[2], viewport[3]);
            gl.Perspective(FieldOfView, AspectRatio, Near, Far);
            Vertex target = this.Target;
            Vertex upVector = this.Up;

            //  Perform the look at transformation.
            gl.LookAt((double)Position.X, (double)Position.Y, (double)Position.Z,
                (double)target.X, (double)target.Y, (double)target.Z,
                (double)upVector.X, (double)upVector.Y, (double)upVector.Z);

            arcBall.TransformMatrix(gl);
        }

        /// <summary>
        /// The arcball used for rotating.
        /// </summary>
        private ArcBall arcBall;// = new ArcBall();
        private Vertex Target;
        private Vertex Up;

        /// <summary>
        /// Gets the arc ball.
        /// </summary>
        public ArcBall ArcBall
        {
            get { return arcBall; }
        }
	}
}
