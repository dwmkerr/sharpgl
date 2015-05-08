using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    class ColorIndicatorDataFactory
    {
        public static ColorIndicatorData CreateRainbow()
        {

            GLColor[] colors = new GLColor[5];
            colors[0] = System.Drawing.Color.FromArgb(255, 0, 22, 76);
            colors[1] = System.Drawing.Color.FromArgb(255, 0, 193, 136);
            colors[2] = System.Drawing.Color.FromArgb(255, 166, 255, 27);
            colors[3] = System.Drawing.Color.FromArgb(255, 255, 173, 0);
            colors[4] = System.Drawing.Color.FromArgb(255, 255, 8, 1);
            return new ColorIndicatorData(colors);
        }
    }
}
