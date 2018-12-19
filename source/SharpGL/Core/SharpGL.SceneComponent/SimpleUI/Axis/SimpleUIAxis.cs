using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// Draw axis with arc ball rotation effect on viewport as an UI.
    /// </summary>
    public class SimpleUIAxis : SimpleUIRect, IMouseRotation
    {
        internal IMouseTransform mouseTransform = new ArcBallTransform();
        /// <summary>
        /// keeps axis' scale.
        /// </summary>
        private SceneGraph.Transformations.LinearTransformation axisTransform;

        /// <summary>
        /// Draw axis with arc ball rotation effect on viewport as an UI. 
        /// </summary>
        /// <param name="anchor">the edges of the viewport to which a SimpleUIAxis is bound and determines how it is resized with its parent.</param>
        /// <param name="margin">the space between viewport and SimpleUIAxis.</param>
        /// <param name="size">Stores width when <see cref="OpenGLUIRect.Anchor"/>.Left & <see cref="OpenGLUIRect.Anchor"/>.Right is <see cref="OpenGLUIRect.Anchor"/>.None.
        /// <para> and height when <see cref="OpenGLUIRect.Anchor"/>.Top & <see cref="OpenGLUIRect.Anchor"/>.Bottom is <see cref="OpenGLUIRect.Anchor"/>.None.</para></param>
        /// <param name="zNear"></param>
        /// <param name="zFar"></param>
        /// <param name="rectColor"></param>
        /// <param name="rightHandAxis"></param>
        public SimpleUIAxis(AnchorStyles anchor, Padding margin, System.Drawing.Size size, int zNear = -1000, int zFar = 1000, GLColor rectColor = null, bool rightHandAxis = true)
            : base(anchor, margin, size, zNear, zFar)
        {
            this.RightHandAxis = rightHandAxis;
            CylinderAxis axis = new CylinderAxis();
            LinearTransformationEffect axisTransform = new SharpGL.SceneGraph.Effects.LinearTransformationEffect();
            this.axisTransform = axisTransform.LinearTransformation;
            axis.AddEffect(axisTransform);
            base.AddChild(axis);
            this.RectColor = new GLColor(1, 1, 0, 1);// red(x axis) + green(y axis)
        }

        protected override void RenderModel(SimpleUIRectArgs args, OpenGL gl, SceneGraph.Core.RenderMode renderMode)
        {
            // Draw rectangle to show UI's scope.
            base.RenderModel(args, gl, renderMode);

            // ** / 2: half of width/height, 
            // ** / 3: CylinderAxis' length is 3.
            this.axisTransform.ScaleX = args.UIWidth / 2 / 3;
            this.axisTransform.ScaleY = args.UIHeight / 2 / 3;
            int max = Math.Max(args.UIWidth, args.UIHeight);
            this.axisTransform.ScaleZ = (this.RightHandAxis ? 1 : -1) * max / 2 / 3;
            //this.axisTransform.TranslateZ = base.zFar;// make sure UI shows in front of enything else.
            //var target2Position = base.Camera.Position - base.Camera.Target;
            //target2Position.Normalize();
            //target2Position *= 100;
            //this.axisTransform.TranslateX = base.Camera.Position.X + target2Position.X;
            //this.axisTransform.TranslateY = base.Camera.Position.Y + target2Position.Y;
            //this.axisTransform.TranslateZ = base.Camera.Position.Z + target2Position.Z;
        }

        public override void PushObjectSpace(OpenGL gl)
        {
            //base.PushObjectSpace(gl);

            this.args = new SimpleUIRectArgs();
            //int viewWidth;
            //int viewHeight;
            CalculateViewport(gl, args);

            //int UIWidth, UIHeight, left, bottom;
            CalculateCoords(args.viewWidth, args.viewHeight, args);

            gl.MatrixMode(SharpGL.Enumerations.MatrixMode.Projection);
            gl.PushMatrix();
            gl.LoadIdentity();
            gl.Ortho(args.left, args.right, args.bottom, args.top, zNear, zFar);

            IViewCamera camera = this.Camera;
            if (camera == null)
            {
                gl.LookAt(0, 0, 1, 0, 0, 0, 0, 1, 0);
                //throw new Exception("Camera not set!");
            }
            else
            {
                Vertex position = camera.Position;
                Vertex target = camera.Target;
                //position.Normalize();
                gl.LookAt(position.X, position.Y, position.Z,
                    target.X, target.Y, target.Z,
                    camera.UpVector.X, camera.UpVector.Y, camera.UpVector.Z);
            }

            gl.MatrixMode(SharpGL.Enumerations.MatrixMode.Modelview);
            gl.PushMatrix();

            if (mouseTransform.Camera != null)
            {
                //mouseTransform.TransformMatrix(gl);
            }

            var target2Position = base.Camera.Position - base.Camera.Target;
            target2Position.Normalize();
            target2Position *= 100;
            var x = base.Camera.Position.X + target2Position.X;
            var y = base.Camera.Position.Y + target2Position.Y;
            var z = base.Camera.Position.Z + target2Position.Z;
            gl.Translate(x, y, z);
        }



        #region IRotation 成员

        /// <summary>
        /// If Camera is null, this UI rectangle area will be drawn with an invoking
        /// <para>gl.LookAt(0, 0, 1, 0, 0, 0, 0, 1, 0);</para>
        /// <para>Otherwise, it uses gl.LookAt(Camera's (Position - Target), Target, UpVector);</para>
        /// </summary>
        public override IScientificCamera Camera
        {
            get
            {
                return base.Camera;
            }
            set
            {
                base.Camera = value;
                this.mouseTransform.Camera = value;
            }
        }

        public void MouseUp(int x, int y)
        {
            this.mouseTransform.MouseUp(x, y);
        }

        public void MouseMove(int x, int y)
        {
            this.mouseTransform.MouseMove(x, y);
        }

        public void SetBounds(int width, int height)
        {
            this.mouseTransform.SetBounds(width, height);
        }

        public void MouseDown(int x, int y)
        {
            this.mouseTransform.MouseDown(x, y);
        }

        public void ResetRotation()
        {
            this.mouseTransform.ResetRotation();
        }

        #endregion

        /// <summary>
        /// If true, draw right-hand axis. Otherwise, draw left-hand axis.
        /// </summary>
        public bool RightHandAxis { get; set; }
    }
}
