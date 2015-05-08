using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SharpGL;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Effects;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// The ArcBall camera supports arcball projection, making it ideal for use with a mouse.
    /// <para>supports arcball rotation in a moving camera</para>
    /// </summary>
    public class MyArcBallEffect : Effect
    {
        /// <summary>
        ///  if null, please set arcBall.Camera property later.
        /// </summary>
        /// <param name="camera">if null, please set arcBall.Camera property later.</param>
        public MyArcBallEffect(LookAtCamera camera = null)
        {
            this.arcBall.Camera = camera;
        }

        /// <summary>
        /// Pushes the effect onto the specified parent element.
        /// </summary>
        /// <param name="gl">The OpenGL instance.</param>
        /// <param name="parentElement">The parent element.</param>
        public override void Push(OpenGL gl, SceneElement parentElement)
        {
            //  Push the stack.
            gl.PushMatrix();

            // Try to get the scene's camera.
            if (this.arcBall.Camera == null)
            {
                SceneContainer container = parentElement.TraverseToRootElement();
                if (container != null)
                {
                    Scene scene = container.ParentScene;
                    LookAtCamera camera = scene.CurrentCamera as LookAtCamera;
                    this.arcBall.Camera = camera;
                }
            }

            //  Perform the transformation.
            arcBall.TransformMatrix(gl);
        }

        /// <summary>
        /// Pops the specified parent element.
        /// </summary>
        /// <param name="gl">The OpenGL instance.</param>
        /// <param name="parentElement">The parent element.</param>
        public override void Pop(OpenGL gl, SceneElement parentElement)
        {
            //  Pop the stack.
            gl.PopMatrix();
        }

        /// <summary>
        /// The arcball.
        /// </summary>
        private MyArcBall arcBall = new MyArcBall();

        /// <summary>
        /// Gets or sets the linear transformation.
        /// </summary>
        /// <value>
        /// The linear transformation.
        /// </value>
        [Description("The ArcBall."), Category("Effect")]
        public MyArcBall ArcBall
        {
            get { return arcBall; }
            set { arcBall = value; }
        }
    }
}
