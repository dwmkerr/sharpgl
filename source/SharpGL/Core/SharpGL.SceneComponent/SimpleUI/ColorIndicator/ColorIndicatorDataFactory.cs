using SharpGL.SceneComponent.SimpleUI.ColorIndicator;
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
            ColorPalette colorPalette = ColorPaletteFactory.CreateRainbow();
            return new ColorIndicatorData(colorPalette);
        }
    }
}
