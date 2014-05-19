using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneGraph.JOG
{
    public class ColorF
    {
        #region fields
        float[] _argb = new float[4];
        #endregion fields

        #region properties
        public float A
        {
            get { return this[0]; }
            set { this[0] = value; }
        }

        public float R
        {
            get { return this[1]; }
            set { this[1] = value; }
        }

        public float G
        {
            get { return this[2]; }
            set { this[2] = value; }
        }

        public float B
        {
            get { return this[3]; }
            set { this[3] = value; }
        }

        /// <summary>
        /// Gets or sets a color by index (0 = Alpha, 1 = Red, 2 = Green, 3 = Blue)
        /// </summary>
        /// <param name="it"></param>
        /// <returns></returns>
        public float this[int i]
        {
            get
            {
                return _argb[i];
            }
            set
            {
                _argb[i] = value;
            }
        }

        public float[] Argb
        {
            get { return _argb; }
            set { _argb = value; }
        }
        #endregion properties

        #region constructors
        public ColorF()
        { }

        /// <summary>
        /// Converts uint to ColorF. Second byte = blue, Thirth byte = green, Last byte = red.
        /// </summary>
        /// <param name="colorRGB"></param>
        public ColorF(uint colorRGB)
        {
            // Get the integer ID
            var i = colorRGB;

            int b = (int)(i >> 16) & 0xFF;
            int g = (int)(i >> 8) & 0xFF;
            int r = (int)i & 0xFF;

            IntToFloatColor(255, r, g, b);
        }

        /// <summary>
        /// Converts uint to ColorF. 3th and 4th byte = blue, 5th and 6th byte = green, 7th and last byte = red.
        /// </summary>
        /// <param name="colorRGB"></param>
        public ColorF(ulong colorRGB)
        {
            // Get the integer ID
            var i = colorRGB;

            int b = (int)(i >> 32) & 0xFF;
            int g = (int)(i >> 16) & 0xFF;
            int r = (int)i & 0xFF;

            IntToFloatColor(255, r, g, b);
        }

        public ColorF(int a, int r, int g, int b)
        {
            IntToFloatColor(a, r, g, b);
        }

        public ColorF(float a, float r, float g, float b)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }

        public ColorF(System.Drawing.Color color)
            : this(color.A, color.R, color.G, color.B)
        { }


        #endregion constructors

        private void IntToFloatColor(int a, int r, int g, int b)
        {
            A = a / 255.0f;
            R = r / 255.0f;
            G = g / 255.0f;
            B = b / 255.0f;
        }

        public uint ToUint()
        {
            // Get color id from pixel data.
            return (uint)(R * 255 + G * 65025 + B * 16581375); // r * 255 + g * 255² + b * 255³.
        }
    }
}
