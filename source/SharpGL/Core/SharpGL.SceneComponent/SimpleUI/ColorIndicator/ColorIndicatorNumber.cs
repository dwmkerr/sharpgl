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

            int blockCount = data.GetBlockCount();
            if (blockCount <= 0) { return; }

            GLColor[] colors = data.ColorPalette.Colors;
            int blockWidth = 0;
            if (data.MaxValue - data.MinValue == 0)
            {
                blockWidth = lastArgs.UIWidth;
            }
            else
            {
                blockWidth = (int)(lastArgs.UIWidth * (data.Step / (data.MaxValue - data.MinValue)));
            }
            //draw numbers
            for (int i = 0; i <= blockCount; i++)
            {
                string value = null;
                if (i == blockCount)
                { value = data.MaxValue.ToString(); }
                else
                { value = (data.MinValue + data.Step * i).ToString(); }
                double valueLength = 100.0 * value.Length / fontSize;
                double x = 0;
                if (i == blockCount)
                { x = -(double)lastArgs.UIWidth / 2 - lastArgs.left + lastArgs.UIWidth - valueLength / 2; }
                else
                { x = -(double)lastArgs.UIWidth / 2 - lastArgs.left + i * blockWidth - valueLength / 2; }
                double y = -(double)lastArgs.UIHeight / 2 - lastArgs.bottom - 14;
                gl.DrawText((int)x, (int)y, 1, 1, 1, "Courier New", fontSize, value);
            }
        }

        public SimpleUIRectArgs CurrentArgs { get; set; }
    }
}
