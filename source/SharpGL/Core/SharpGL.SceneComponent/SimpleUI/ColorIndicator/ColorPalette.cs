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

        private float[] coords;

        /// <summary>
        /// protected default constructor
        /// </summary>
        private ColorPalette()
        {
        }

        public ColorPalette(String paletteName,GLColor[] colors,float[] coords)
        {
            if (colors == null||colors == null||colors.Length<2 || colors.Length < 2||colors.Length!=coords.Length)
            { 
                throw new ArgumentNullException("ColorPalette", "ColorPalette define error"); 
            }
            this.colors = colors;
            this.coords = coords;
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

        public float[] Coords
        {
            get
            {
                return this.coords;
            }
        }

        public GLColor[] Colors
        {
            get
            {
                return this.colors;
            }
        }

      

        public GLColor MapToColor(float x, float minValue, float maxValue){
            return ColorMapHelper.MapToColor(colors,this.coords, x, minValue, maxValue);
        }
    }

    public class ColorPaletteFactory
    {

        public static ColorPalette CreateRainbow()
        {
            GLColor[] colors = new GLColor[5];
            float[] coords = new float[5];
            coords[0] = 0.0f;
            colors[0] = System.Drawing.Color.FromArgb(255, 0, 22, 76);

            coords[1] = 0.25f;
            colors[1] = System.Drawing.Color.FromArgb(255, 0, 193, 136);

            coords[2] = 0.5f;
            colors[2] = System.Drawing.Color.FromArgb(255, 166, 255, 27);

            coords[3] = 0.75f;
            colors[3] = System.Drawing.Color.FromArgb(255, 255, 173, 0);

            coords[4] = 1.0f;
            colors[4] = System.Drawing.Color.FromArgb(255, 255, 8, 1);
            return new ColorPalette("Rainbow",colors,coords);
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

        public static GLColor MapToColor(GLColor[] colors,float[] coords, double value, double minValue, double maxValue)
        {
            if (colors.Length < 2)
                throw new ArgumentException("template colors size error");

            double d = maxValue - minValue;
            if (d < 0.0f)
                throw new ArgumentException("fault value range");

            if (value < minValue)
                value = minValue;
            if (value > maxValue)
                value = maxValue;

            if (d == 0.0d)
                return colors[0];

            double dx = value - minValue;

            double x = dx / d;
            if(x<=0.000000001d)
                return colors[0];

            bool find = false;
            double xi=0.0d, xi1=0.0d;
            GLColor yi=colors[0], yi1=colors[1];

            for (int i = 0; i < coords.Length - 1; i++)
            {
                xi = coords[i];
                xi1 = coords[i + 1];
                yi = colors[i];
                yi1 = colors[i + 1];
                if (x >= xi && x <= xi1)
                {
                    find = true;
                    break;
                }
            }
            if (!find)
                throw new ArgumentException("not found colors,template fault default not in[0,1] ?");


            double dxi = x - xi;
            
            GLColor color = new GLColor();
            double kr = LineSlope(yi1.R, yi.R, dxi);
            double kg = LineSlope(yi1.G, yi.G, dxi);
            double kb = LineSlope(yi1.B, yi.B, dxi);
            double r = yi.R + kr * (x - xi);
            double g = yi.G + kg * (x - xi);
            double b = yi.B + kb * (x - xi);

            double total = r + g + b;
            color.Set((float)(r / total), (float)(g / total), (float)(b / total));
            return color;

        }
    }
}
