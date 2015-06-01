using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// Render rectangles and lines of <see cref="SimpleUIColorIndicator"/>.
    /// </summary>
    internal class ColorIndicatorBar : SceneElement, IRenderable
    {
        private long lastModified = 0;

        private ColorIndicatorData data;

        public ColorIndicatorData Data
        {
            get { return data; }
            set { data = value; }
        }

        private unsafe void TryUpdate(ColorIndicatorData data)
        {
            if (data == null)
            {
                this.rectModel = null;
                this.verticalLines = null;
                this.horizontalLines = null;
                return;
            }

            if (data.LastModified == this.lastModified) { return; }

            // initialize rectangles with gradient color.
            GenerateRectangles(data);

            // initialize two horizontal white lines.
            GenerateHorizontalLines();

            // initialize vertical lines.
            GenerateVerticalLines(data);

            this.lastModified = data.LastModified;
        }

        unsafe private void GenerateVerticalLines(ColorIndicatorData data)
        {
            int blockCount = data.GetBlockCount();
            //int blockCount = data.BlockCount;
            int segmentCount = blockCount + 1;
            ScientificModel verticalLines = new ScientificModel(segmentCount * 2, Enumerations.BeginMode.Lines);
            float[] positions = verticalLines.Positions;
            for (int i = 0; i < segmentCount; i++)
            {
                if (i + 1 != segmentCount)
                {
                    if (data.MaxValue != data.MinValue)
                    {
                        positions[i * 2 * 3 + 0] = barWidth * (i * data.Step / (data.MaxValue - data.MinValue));
                    }
                    else
                    {
                        positions[i * 2 * 3 + 0] = barWidth * 0;
                    }
                }
                else
                {
                    positions[i * 2 * 3 + 0] = barWidth;
                }
                positions[i * 2 * 3 + 1] = -9;
                positions[i * 2 * 3 + 2] = 0;
                positions[i * 2 * 3 + 3 + 0] = positions[i * 2 * 3 + 0];
                positions[i * 2 * 3 + 3 + 1] = barHeight;
                positions[i * 2 * 3 + 3 + 2] = 0;
            }
            // move the vertical lines' center to (0, 0, 0)
            for (int i = 0; i < segmentCount * 2; i++)
            {
                positions[i * 3 + 0] -= barWidth / 2;
                positions[i * 3 + 1] -= barHeight / 2;
            }

            float[] colors = verticalLines.Colors;
            for (int i = 0; i < segmentCount * 2; i++)
            {
                colors[i * 3 + 0] = 1;
                colors[i * 3 + 1] = 1;
                colors[i * 3 + 2] = 1;
                //colors[i].red = byte.MaxValue / 2;
                //colors[i].green = byte.MaxValue / 2;
                //colors[i].blue = byte.MaxValue / 2;
            }

            this.verticalLines = verticalLines;
        }

        unsafe private void GenerateHorizontalLines()
        {
            int length = 4;
            ScientificModel horizontalLines = new ScientificModel(length, Enumerations.BeginMode.Lines);
            float[] positions = horizontalLines.Positions;
            //positions[0].X = 0; positions[0].Y = 0; positions[0].Z = 0;
            //positions[1].X = barWidth; positions[1].Y = 0; positions[1].Z = 0;
            //positions[2].X = 0; positions[2].Y = barHeight; positions[2].Z = 0;
            //positions[3].X = barWidth;
            //positions[3].Y = barHeight;
            //positions[3].Z = 0;
            positions[0 * 3 + 0] = 0; positions[0 * 3 + 1] = 0; positions[0 * 3 + 2] = 0;
            positions[1 * 3 + 0] = barWidth; positions[1 * 3 + 1] = 0; positions[1 * 3 + 2] = 0;
            positions[2 * 3 + 0] = 0; positions[2 * 3 + 1] = barHeight; positions[2 * 3 + 2] = 0;
            positions[3 * 3 + 0] = barWidth;
            positions[3 * 3 + 1] = barHeight;
            positions[3 * 3 + 2] = 0;
            // move the horizontal white lines' center to (0, 0, 0)
            for (int i = 0; i < length; i++)
            {
                positions[i * 3 + 0] -= barWidth / 2;
                positions[i * 3 + 1] -= barHeight / 2;
            }
            float[] colors = horizontalLines.Colors;
            for (int i = 0; i < length; i++)
            {
                colors[i * 3 + 0] = 1;
                colors[i * 3 + 1] = 1;
                colors[i * 3 + 2] = 1;
                //colors[i].red = byte.MaxValue / 2;
                //colors[i].green = byte.MaxValue / 2;
                //colors[i].blue = byte.MaxValue / 2;
            }

            this.horizontalLines = horizontalLines;
        }

        unsafe private void GenerateRectangles(ColorIndicatorData data)
        {
            int rectCount = data.ColorPalette.Colors.Length;
            ScientificModel rectModel = new ScientificModel(rectCount * 2, Enumerations.BeginMode.QuadStrip);
            float[] positions = rectModel.Positions;
            for (int i = 0; i < rectCount; i++)
            {
                positions[i * 2 * 3 + 0] = barWidth * data.ColorPalette.Coords[i];
                positions[i * 2 * 3 + 1] = 0;
                positions[i * 2 * 3 + 2] = 0;
                positions[i * 2 * 3 + 3 + 0] = positions[i * 2 * 3];
                positions[i * 2 * 3 + 3 + 1] = barHeight;
                positions[i * 2 * 3 + 3 + 2] = 0;
            }
            // move the rectangles' center to (0, 0, 0)
            for (int i = 0; i < rectCount * 2; i++)
            {
                positions[i * 3 + 0] -= barWidth / 2;
                positions[i * 3 + 1] -= barHeight / 2;
            }

            float[] colors = rectModel.Colors;
            for (int i = 0; i < rectCount; i++)
            {
                GLColor color = data.ColorPalette.Colors[i];
                colors[i * 2 * 3 + 0] = (color.R);
                colors[i * 2 * 3 + 1] = (color.G);
                colors[i * 2 * 3 + 2] = (color.B);
                colors[i * 2 * 3 + 3 + 0] = (color.R);
                colors[i * 2 * 3 + 3 + 1] = (color.G);
                colors[i * 2 * 3 + 3 + 2] = (color.B);
            }

            this.rectModel = rectModel;
        }

        public const int barWidth = 100;
        public const int barHeight = 30;

        private ScientificModel rectModel;

        private ScientificModel verticalLines;

        private ScientificModel horizontalLines;

        public void Render(OpenGL gl, RenderMode renderMode)
        {
            TryUpdate(this.data);

            ScientificModel rectModel = this.rectModel;
            ScientificModel verticalLines = this.verticalLines;
            ScientificModel horizontalLines = this.horizontalLines;


            if (rectModel != null)
            { rectModel.RenderLegacyOpenGL(gl, renderMode); }

            if (verticalLines != null)
            { verticalLines.RenderLegacyOpenGL(gl, renderMode); }

            if (horizontalLines != null)
            { horizontalLines.RenderLegacyOpenGL(gl, renderMode); }
        }

    }
}
