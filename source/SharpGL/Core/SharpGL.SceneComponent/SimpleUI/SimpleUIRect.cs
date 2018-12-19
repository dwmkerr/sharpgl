using SharpGL.RenderContextProviders;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// Draw a rectangle on OpenGL control like a <see cref="Windows.Forms.Control"/> drawn on a <see cref="windows.Forms.Form"/>.
    /// Set its properties(Anchor, Margin, Size, etc) to adjust its behaviour.
    /// </summary>
    public class SimpleUIRect : SceneElement, IRenderable, IHasObjectSpace
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="anchor">the edges of the viewport to which a SimpleUIRect is bound and determines how it is resized with its parent.
        /// <para>something like AnchorStyles.Left | AnchorStyles.Bottom.</para></param>
        /// <param name="margin">the space between viewport and SimpleRect.</param>
        /// <param name="size">Stores width when <see cref="OpenGLUIRect.Anchor"/>.Left & <see cref="OpenGLUIRect.Anchor"/>.Right is <see cref="OpenGLUIRect.Anchor"/>.None.
        /// <para> and height when <see cref="OpenGLUIRect.Anchor"/>.Top & <see cref="OpenGLUIRect.Anchor"/>.Bottom is <see cref="OpenGLUIRect.Anchor"/>.None.</para></param>
        /// <param name="zNear"></param>
        /// <param name="zFar"></param>
        /// <param name="rectColor">default color is red.</param>
        public SimpleUIRect(AnchorStyles anchor, Padding margin, System.Drawing.Size size, int zNear = -1000, int zFar = 1000, GLColor rectColor = null)
        {
            this.Anchor = anchor;
            this.Margin = margin;
            this.Size = size;
            this.zNear = zNear;
            this.zFar = zFar;
            if (rectColor == null)
            { this.RectColor = new GLColor(1, 0, 0, 1); }
            else
            { this.RectColor = new GLColor(1, 0, 0, 1); }

            this.RenderBound = true;
        }

        #region IRenderable 成员

        public void Render(OpenGL gl, RenderMode renderMode)
        {
            //if (renderMode == RenderMode.HitTest) { return; }

            RenderModel(args, gl, renderMode);
        }

        protected void CalculateViewport(OpenGL gl, SimpleUIRectArgs args)
        {
            IRenderContextProvider rcp = gl.RenderContextProvider;
            Debug.Assert(rcp != null, "The gl.RenderContextProvider is null!");

            if (rcp != null)
            {
                args.viewWidth = rcp.Width;
                args.viewHeight = rcp.Height;
            }
            else
            {
                int[] viewport = new int[4];
                gl.GetInteger(OpenGL.GL_VIEWPORT, viewport);
                args.viewWidth = viewport[2];
                args.viewHeight = viewport[3];
            }
        }

        protected void CalculateCoords(int viewWidth, int viewHeight, SimpleUIRectArgs args)
        {
            if ((Anchor & leftRightAnchor) == leftRightAnchor)
            {
                args.UIWidth = viewWidth - Margin.Left - Margin.Right;
                if (args.UIWidth < 0) { args.UIWidth = 0; }
            }
            else
            {
                args.UIWidth = this.Size.Width;
            }

            if ((Anchor & topBottomAnchor) == topBottomAnchor)
            {
                args.UIHeight = viewHeight - Margin.Top - Margin.Bottom;
                if (args.UIHeight < 0) { args.UIHeight = 0; }
            }
            else
            {
                args.UIHeight = this.Size.Height;
            }

            if ((Anchor & leftRightAnchor) == AnchorStyles.None)
            {
                args.left = -(args.UIWidth / 2
                    + (viewWidth - args.UIWidth) * ((double)Margin.Left / (double)(Margin.Left + Margin.Right)));
            }
            else if ((Anchor & leftRightAnchor) == AnchorStyles.Left)
            {
                args.left = -(args.UIWidth / 2 + Margin.Left);
            }
            else if ((Anchor & leftRightAnchor) == AnchorStyles.Right)
            {
                args.left = -(viewWidth - args.UIWidth / 2 - Margin.Right);
            }
            else // if ((Anchor & leftRightAnchor) == leftRightAnchor)
            {
                args.left = -(args.UIWidth / 2 + Margin.Left);
            }

            if ((Anchor & topBottomAnchor) == AnchorStyles.None)
            {
                args.bottom = -viewHeight / 2;
                args.bottom = -(args.UIHeight / 2
                    + (viewHeight - args.UIHeight) * ((double)Margin.Bottom / (double)(Margin.Bottom + Margin.Top)));
            }
            else if ((Anchor & topBottomAnchor) == AnchorStyles.Bottom)
            {
                args.bottom = -(args.UIHeight / 2 + Margin.Bottom);
            }
            else if ((Anchor & topBottomAnchor) == AnchorStyles.Top)
            {
                args.bottom = -(viewHeight - args.UIHeight / 2 - Margin.Top);
            }
            else // if ((Anchor & topBottomAnchor) == topBottomAnchor)
            {
                args.bottom = -(args.UIHeight / 2 + Margin.Bottom);
            }
        }


        #endregion

        /// <summary>
        /// render UI model at axis's center(0, 0, 0) in <paramref name="UIWidth"/> and <paramref name="UIHeight"/>.
        /// <para>The <see cref="SimpleUIRect.RenderMode()"/> only draws a rectangle to show the UI's scope.</para>
        /// </summary>
        /// <param name="UIWidth"></param>
        /// <param name="UIHeight"></param>
        /// <param name="gl"></param>
        /// <param name="renderMode"></param>
        protected virtual void RenderModel(SimpleUIRectArgs args, OpenGL gl, RenderMode renderMode)
        {
            if (this.RenderBound)
            {
                gl.Begin(Enumerations.BeginMode.LineLoop);
                gl.Color(RectColor);
                gl.Vertex(-args.UIWidth / 2, -args.UIHeight / 2, 0);
                gl.Vertex(args.UIWidth / 2, -args.UIHeight / 2, 0);
                gl.Vertex(args.UIWidth / 2, args.UIHeight / 2, 0);
                gl.Vertex(-args.UIWidth / 2, args.UIHeight / 2, 0);
                gl.End();
            }
        }

        /// <summary>
        /// leftRightAnchor = (AnchorStyles.Left | AnchorStyles.Right); 
        /// </summary>
        protected const AnchorStyles leftRightAnchor = (AnchorStyles.Left | AnchorStyles.Right);

        /// <summary>
        /// topBottomAnchor = (AnchorStyles.Top | AnchorStyles.Bottom);
        /// </summary>
        protected const AnchorStyles topBottomAnchor = (AnchorStyles.Top | AnchorStyles.Bottom);

        //protected int viewWidth;
        //protected int viewHeight;
        //protected int UIWidth;
        //protected int UIHeight;
        //protected int left;
        //protected int bottom;
        protected SimpleUIRectArgs args = new SimpleUIRectArgs();

        /// <summary>
        /// if Camera is null, this UI rectangle area will be drawn with an invoking
        /// <para>gl.LookAt(0, 0, 1, 0, 0, 0, 0, 1, 0);</para>
        /// <para>otherwise, it uses gl.LookAt(Camera's (Position - Target), Target, UpVector);</para>
        /// </summary>
        public virtual IScientificCamera Camera { get; set; }

        /// <summary>
        /// the edges of the OpenGLControl to which a SimpleUIRect is bound and determines how it is resized with its parent.
        /// <para>something like AnchorStyles.Left | AnchorStyles.Bottom.</para>
        /// </summary>
        public System.Windows.Forms.AnchorStyles Anchor { get; set; }
        
        /// <summary>
        /// Gets or sets the space between viewport and SimpleRect.
        /// </summary>
        public System.Windows.Forms.Padding Margin { get; set; }

        ///// <summary>
        ///// Left bottom point's location on view port.
        ///// <para>This works when <see cref="OpenGLUIRect.Anchor"/>.Left & <see cref="OpenGLUIRect.Anchor"/>.Right is <see cref="OpenGLUIRect.Anchor"/>.None.
        ///// or <see cref="OpenGLUIRect.Anchor"/>.Top & <see cref="OpenGLUIRect.Anchor"/>.Bottom is <see cref="OpenGLUIRect.Anchor"/>.None.</para>
        ///// </summary>
        //public System.Drawing.Point Location { get; set; }

        /// <summary>
        /// Stores width when <see cref="OpenGLUIRect.Anchor"/>.Left & <see cref="OpenGLUIRect.Anchor"/>.Right is <see cref="OpenGLUIRect.Anchor"/>.None.
        /// <para> and height when <see cref="OpenGLUIRect.Anchor"/>.Top & <see cref="OpenGLUIRect.Anchor"/>.Bottom is <see cref="OpenGLUIRect.Anchor"/>.None.</para>
        /// </summary>
        public System.Drawing.Size Size { get; set; }

        public int zNear { get; set; }

        public int zFar { get; set; }

        public GLColor RectColor { get; set; }

        public bool RenderBound { get; set; }

        #region IHasObjectSpace 成员

        /// <summary>
        /// Prepare projection matrix.
        /// </summary>
        /// <param name="gl"></param>
        public virtual void PushObjectSpace(OpenGL gl)
        {
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
                Vertex position = camera.Position - camera.Target;
                position.Normalize();
                gl.LookAt(position.X, position.Y, position.Z,
                    0, 0, 0,
                    camera.UpVector.X, camera.UpVector.Y, camera.UpVector.Z);
            }

            gl.MatrixMode(SharpGL.Enumerations.MatrixMode.Modelview);
            gl.PushMatrix();
        }

        public virtual void PopObjectSpace(OpenGL gl)
        {
            gl.MatrixMode(SharpGL.Enumerations.MatrixMode.Projection);
            gl.PopMatrix();

            gl.MatrixMode(SharpGL.Enumerations.MatrixMode.Modelview);
            gl.PopMatrix();
        }

        /// <summary>
        /// This is not used.
        /// </summary>
        public SceneGraph.Transformations.LinearTransformation Transformation
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

    }
}
