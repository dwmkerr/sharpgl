using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SharpGL.SceneComponent
{
    internal class ColorIndicatorNumber : SceneElement, IRenderable
    {
        public ColorIndicatorData Data { get; set; }

        //private System.Drawing.Font font = new System.Drawing.Font("Courier New", fontSize);
        const float fontSize = 12f;

        public void Render(OpenGL gl, RenderMode renderMode)
        {
            SimpleUIRectArgs lastArgs = this.CurrentArgs;
            if (lastArgs == null) { return; }
            ColorIndicatorData data = this.Data;
            if (data == null) { return; }

            int blockWidth = lastArgs.UIWidth / (data.Colors.Length - 1);
            //draw numbers
            for (int i = 0; i < data.Colors.Length; i++)
            {
                string value = (data.minValue * (double)(data.Colors.Length - 1 - i) / (data.Colors.Length - 1)
                    + data.maxValue * (double)i / (data.Colors.Length - 1)).ToString();
                double valueLength = 100.0 * value.Length / fontSize;
                double x = -(double)lastArgs.UIWidth / 2 - lastArgs.left + i * blockWidth - valueLength / 2;
                double y = -(double)lastArgs.UIHeight / 2 - lastArgs.bottom - 14;
                gl.DrawText((int)x, (int)y, 1, 1, 1, "Courier New", fontSize, value);
            }
        }

        public SimpleUIRectArgs CurrentArgs { get; set; }
    }
}
