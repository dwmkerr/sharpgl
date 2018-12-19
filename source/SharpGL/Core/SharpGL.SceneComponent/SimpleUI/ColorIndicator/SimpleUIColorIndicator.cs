using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// Draw color indicator on viewport as an UI.
    /// </summary>
    public class SimpleUIColorIndicator : SimpleUIRect
    {
        private SceneGraph.Transformations.LinearTransformation colorBarTransform;
        private ColorIndicatorData data;
        private ColorIndicatorBar colorBar;
        private ColorIndicatorNumber colorNumber;

        public ColorIndicatorData Data
        {
            get { return data; }
            set
            {
                data = value;
                this.colorBar.Data = value;
                this.colorNumber.Data = value;
            }
        }

        /// <summary>
        /// Draw color indicator on viewport as an UI. 
        /// </summary>
        /// <param name="anchor">the edges of the viewport to which a SimpleUIColorIndicator is bound and determines how it is resized with its parent.</param>
        /// <param name="margin">the space between viewport and SimpleUIColorIndicator.</param>
        /// <param name="size">Stores width when <see cref="OpenGLUIRect.Anchor"/>.Left & <see cref="OpenGLUIRect.Anchor"/>.Right is <see cref="OpenGLUIRect.Anchor"/>.None.
        /// <para> and height when <see cref="OpenGLUIRect.Anchor"/>.Top & <see cref="OpenGLUIRect.Anchor"/>.Bottom is <see cref="OpenGLUIRect.Anchor"/>.None.</para></param>
        /// <param name="zNear"></param>
        /// <param name="zFar"></param>
        /// <param name="rectColor"></param>
        public SimpleUIColorIndicator(AnchorStyles anchor, Padding margin, System.Drawing.Size size, int zNear = -1000, int zFar = 1000, GLColor rectColor = null)
            : base(anchor, margin, size, zNear, zFar)
        {
            {
                this.colorBar = new ColorIndicatorBar() { Name = "color indicator's bar" };
                var colorBarTransform = new SharpGL.SceneGraph.Effects.LinearTransformationEffect();
                colorBar.AddEffect(colorBarTransform);
                base.AddChild(colorBar);
                this.colorBarTransform = colorBarTransform.LinearTransformation;
            }

            {
                this.colorNumber = new ColorIndicatorNumber() { Name = "color indicator's number" };
                base.AddChild(colorNumber);
            }
        }

        protected override void RenderModel(SimpleUIRectArgs args, OpenGL gl, SceneGraph.Core.RenderMode renderMode)
        {
            // Draw rectangle to show UI's scope.
            base.RenderModel(args, gl, renderMode);

            this.colorBarTransform.ScaleX = (float)args.UIWidth / (float)ColorIndicatorBar.barWidth;
            this.colorBarTransform.ScaleY = (float)args.UIHeight / (float)ColorIndicatorBar.barHeight;
            //this.colorBarTransform.ScaleZ = 1;// This is not needed.
            this.colorBarTransform.TranslateZ = -base.zNear;// make sure UI shows in front of enything else.

            this.colorNumber.CurrentArgs = args;
        }
    }
}
