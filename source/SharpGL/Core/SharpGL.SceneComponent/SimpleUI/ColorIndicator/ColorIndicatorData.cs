using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// Contains gradient colors and max, min values.
    /// </summary>
    public class ColorIndicatorData
    {
        private GLColor[] colors;

        public GLColor[] Colors
        {
            get { return colors; }
            set
            {
                if (value == null || value.Length < 2)
                { throw new ArgumentNullException("colors", "colors' count must greater than 1."); }
                colors = value;
            }
        }

        public ColorIndicatorData(GLColor[] colors, int minValue = 0, int maxValue = 0)
        {
            if (colors == null || colors.Length < 2)
            { throw new ArgumentNullException("colors", "colors' count must greater than 1."); }
            if (maxValue < minValue)
            { throw new Exception("minValue must less than or equal to maxValue."); }

            this.Colors = colors;
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        public GLColor MapToColor(float value)
        {
            GLColor result = MapToColor(value, this.minValue, this.maxValue);
            return result;
        }

        public GLColor MapToColor(float value, float minValue, float maxValue)
        {
            GLColor[] colors = this.Colors;
            if (colors == null || colors.Length < 2)
            { throw new ArgumentNullException("colors", "colors' count must greater than 1."); }

            float difference = maxValue - minValue;
            if (difference < 0.0f)
            { throw new ArgumentException("fault value range"); }

            if (difference == 0f)
            { return colors[0]; }

            if (value <= minValue) { return colors[0]; }
            if (value >= maxValue) { return colors[colors.Length - 1]; }

            float step = difference / (colors.Length - 1);

            int leftIndex = (int)Math.Floor(value - minValue / step);

            float leftValue = leftIndex * step;
            float rightValue = leftValue + step;

            GLColor leftColor = colors[leftIndex];
            GLColor rightColor = colors[leftIndex + 1];

            GLColor color = new GLColor();
            color = leftColor * ((value - leftValue) / step)
                + rightColor * ((rightValue - value) / step);

            return color;
        }

        public override string ToString()
        {
            GLColor[] colors = this.Colors;
            if (colors == null)
            {
                return string.Format("0 colors. value:{0} ~ {1}", minValue, maxValue);
            }
            else
            {
                return string.Format("{0} colors. Value:{1} ~ {2}", colors.Length, minValue, maxValue);
            }
        }


        public float minValue { get; set; }

        public float maxValue { get; set; }
    }
}
