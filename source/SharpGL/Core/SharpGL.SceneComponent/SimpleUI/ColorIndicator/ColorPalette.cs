using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent.SimpleUI.ColorIndicator
{

    public class ColorPalette
    {
        /// <summary>
        /// meaningful name for the Palette, eg.rainbow,blackwhite, and so on.
        /// </summary>
        private String name;

        /// <summary>
        /// reference colors 
        /// </summary>
        private GLColor[] colors;

        /// <summary>
        /// protected default constructor
        /// </summary>
        private ColorPalette()
        {
        }

        public ColorPalette(String paletteName,GLColor[] colors)
        {
            if (colors == null || colors.Length < 2)
            { 
                throw new ArgumentNullException("colors", "colors' length must greater than 1."); 
            }
            this.colors = colors;
            this.name = paletteName;
        }
        
        /// <summary>
        /// Color Pallete Name
        /// </summary>
        public String Name{
           get{
               return this.name;
           }
        }

        public GLColor MapToColor(float x, float minValue, float maxValue){
            return ColorMapHelper.MapToColor(colors, x, minValue, maxValue);
        }
    }

    public class ColorPaletteFactory
    {

        public static ColorPalette CreateRainbow()
        {
            GLColor[] colors = new GLColor[5];
            colors[0] = System.Drawing.Color.FromArgb(255, 0, 22, 76);
            colors[1] = System.Drawing.Color.FromArgb(255, 0, 193, 136);
            colors[2] = System.Drawing.Color.FromArgb(255, 166, 255, 27);
            colors[3] = System.Drawing.Color.FromArgb(255, 255, 173, 0);
            colors[4] = System.Drawing.Color.FromArgb(255, 255, 8, 1);
            return new ColorPalette("Rainbow",colors);
        }

        public static IList<ColorPalette> LoadColorPalettes(String filePath)
        {
            throw new NotImplementedException("Not Implementation");
        }
    }

    public class ColorMapHelper
    {


        /// <summary>
        /// 如果两点重合，则斜率为NAN.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <returns></returns>
        private static double LineSlope(double y2, double y1, double dx)
        {
            return (y2 - y1) / dx;
        }

        public static GLColor MapToColor(GLColor[] template, double x, double minValue, double maxValue)
        {
            if (template.Length < 2)
                throw new ArgumentException("template colors size error");

            double d = maxValue - minValue;
            if (d < 0.0f)
                throw new ArgumentException("fault value range");

            if (x < minValue)
                x = minValue;
            if (x > maxValue)
                x = maxValue;

            if (d == 0.0d)
                return template[0];

            double step = d / template.Length;

            double d0 = x - minValue;
            int minIndex = (int)Math.Floor(d0 / step);


            double x0 = minIndex * step;
            double x1 = (minIndex + 1) * step;

            GLColor y0 = template[minIndex];
            GLColor y1 = template[minIndex + 1];

            GLColor color = new GLColor();
            double kr = LineSlope(y1.R, y0.R, step);
            double kg = LineSlope(y1.G, y0.G, step);
            double kb = LineSlope(y1.B, y0.B, step);
            double r = y0.R + kr * (x - x0);
            double g = y0.G + kg * (x - x0);
            double b = y0.B + kb * (x - x0);

            double total = r + g + b;
            color.Set((float)(r / total), (float)(g / total), (float)(b / total));
            return color;

        }
    }
}
