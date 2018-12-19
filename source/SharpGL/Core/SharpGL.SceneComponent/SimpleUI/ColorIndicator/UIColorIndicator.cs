using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SharpGL.SceneComponent.SimpleUI.ColorIndicator
{
    public struct QuantityRange
    {
        public float minValue;
        public float maxValue;
    }

    public class UIColorIndicator : SimpleUIRect
    {



        private QuantityRange range = new QuantityRange() { minValue = 0.0f, maxValue = 100.0f };

        /// <summary>
        /// (0.0,Inifinity]
        /// </summary>
        private float step = 10.0f;

        /// <summary>
        /// 
        /// </summary>
        private ColorPalette colorPalette = ColorPaletteFactory.CreateRainbow();


        public UIColorIndicator(AnchorStyles anchor, Padding margin, System.Drawing.Size size, int zNear = -1000, int zFar = 1000, GLColor rectColor = null)
            : base(anchor, margin, size, zNear, zFar, rectColor)
        {

        }


        /// <summary>
        /// 
        /// </summary>
        public String QuantityLabel
        {
            get;
            set;
        }


        public QuantityRange QuantityRange
        {
            get
            {
                return this.range;
            }
            set
            {
                this.range = value;
            }
        }

        public float Step
        {
            get
            {
                return this.step;
            }
            set
            {
                this.step = value;
            }
        }

        public ColorPalette ColorPalette
        {
            get
            {
                return this.colorPalette;
            }
            set
            {
                this.colorPalette = value;
            }
        }

        public GLColor MapToColor(float value)
        {
           return this.colorPalette.MapToColor(value, this.QuantityRange.minValue, this.QuantityRange.maxValue);
        }

        protected override void RenderModel(SimpleUIRectArgs args, OpenGL gl, SceneGraph.Core.RenderMode renderMode)
        {
            base.RenderModel(args, gl, renderMode);
        }

       
    }
}
