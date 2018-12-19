using SharpGL.SceneComponent.SimpleUI.ColorIndicator;
using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// Contains color palette, step, min value, max value etc.
    /// <para>Works as model while <see cref="SimpleUIColorIndicator"/> works as view.</para>
    /// </summary>
    public class ColorIndicatorData
    {
        private long lastModified = 0;

        /// <summary>
        /// Get last time(in ticks) when this object's property is modified.
        /// </summary>
        internal long LastModified
        {
            get { return lastModified; }
        }

        ColorPalette colorPalette;

        /// <summary>
        /// Get or set color palette.
        /// </summary>
        public ColorPalette ColorPalette
        {
            get { return colorPalette; }
            set
            {
                if (value != colorPalette)
                {
                    colorPalette = value;
                    lastModified = DateTime.Now.Ticks;
                }
            }
        }

        public ColorIndicatorData(ColorPalette colorPalette)
        {
            this.ColorPalette = colorPalette;
        }

        private float step = 10.0f;
        /// <summary>
        /// Get or set distance for one block of color indicator's rectangle bar.
        /// </summary>
        public float Step
        {
            get { return step; }
            set
            {
                if (step != value)
                {
                    step = value;
                    lastModified = DateTime.Now.Ticks;
                }
            }
        }

        private string quantityLabel = string.Empty;
        /// <summary>
        /// Get or set label for the quantity.
        /// </summary>
        public string QuantityLabel
        {
            get { return quantityLabel; }
            set
            {
                if (quantityLabel != value)
                {
                    quantityLabel = value;
                    lastModified = DateTime.Now.Ticks;
                }
            }
        }


        private float minValue;
        /// <summary>
        /// Get or set minimum value.
        /// </summary>
        public float MinValue
        {
            get { return minValue; }
            set
            {
                if (minValue != value)
                {
                    minValue = value;
                    lastModified = DateTime.Now.Ticks;
                }
            }
        }

        private float maxValue;
        /// <summary>
        /// Get or set maximum value.
        /// </summary>
        public float MaxValue
        {
            get { return maxValue; }
            set
            {
                if (maxValue != value)
                {
                    maxValue = value;
                    lastModified = DateTime.Now.Ticks;
                }
            }
        }

        /// <summary>
        /// Get mapping color of specified value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public GLColor MapToColor(float value)
        {
            //return this.colorPalette.MapToColor(value, this.MinValue, this.MaxValue);
            return this.MapToColor(value, this.colorPalette.Colors, this.colorPalette.Coords, this.minValue, this.maxValue);
        }

        /// <summary>
        /// Get block count at current maxVluae, minValue and step.
        /// <para>Return -1 if step == 0</para>
        /// <para>Return 1 if maxValue == minValue</para>
        /// </summary>
        /// <returns></returns>
        public int GetBlockCount()
        {
            if (this.step == 0) { return -1; }

            int blockCount = (int)Math.Ceiling((this.MaxValue - this.MinValue) / this.Step);
            if (blockCount == 0) { blockCount = 1; }

            return blockCount;
        }

        GLColor MapToColor(float value, GLColor[] colors, float[] coords, float minValue, float maxValue)
        {
            if (colors == null || colors.Length < 2)
            { throw new ArgumentNullException("colors", "colors' count must greater than 1."); }
            if (coords == null || coords.Length != colors.Length)
            { throw new ArgumentException("coords must have same count of elements with colors."); }

            float difference = maxValue - minValue;
            if (difference < 0.0f)
            { throw new ArgumentException("fault value range"); }

            if (difference == 0f)
            { return colors[0]; }

            if (value <= minValue) { return colors[0]; }
            if (value >= maxValue) { return colors[colors.Length - 1]; }

            float progress = (value - minValue) / difference;
            int leftIndex = -1;
            for (int i = 1; i < coords.Length; i++)
            {
                if (progress < coords[i])
                {
                    leftIndex = i - 1;
                    break;
                }
            }

            if (leftIndex == -1)
            {
                Debug.WriteLine("MapToColor: progress[{0}] not found between minValue[{1}] and maxValue[{2}]! Coords (should be in [0, 1]) are: [{3}]",
                    progress, minValue, maxValue,
                    ArrayHelper.PrintArray(coords));
                return colors[colors.Length - 1];
            }

            float leftValue = minValue + difference * coords[leftIndex];
            float rightValue = minValue + difference * coords[leftIndex + 1];
            float blockWidth = rightValue - leftValue;

            GLColor leftColor = colors[leftIndex];
            GLColor rightColor = colors[leftIndex + 1];

            GLColor color = leftColor * ((value - leftValue) / blockWidth)
                + rightColor * ((rightValue - value) / blockWidth);

            return color;
        }

     
        public override string ToString()
        {
            return string.Format("{0},min:{1}, max:{2}, step:{3}", colorPalette, minValue, maxValue, step);
        }
    }
}
