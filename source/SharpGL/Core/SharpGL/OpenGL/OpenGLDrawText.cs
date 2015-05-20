using System;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.InteropServices;
using SharpGL.RenderContextProviders;
using SharpGL.Version;

namespace SharpGL
{
	/// <summary>
	/// The OpenGL class wraps Suns OpenGL 3D library.
    /// </summary>
	public partial class OpenGL
	{        
     
        /// <summary>
        /// Draws the text.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="r">The r.</param>
        /// <param name="g">The g.</param>
        /// <param name="b">The b.</param>
        /// <param name="faceName">Name of the face.</param>
        /// <param name="fontSize">Size of the font.</param>
        /// <param name="text">The text.</param>
        public void DrawText(int x, int y, float r, float g, float b, 
            string faceName, float fontSize, string text)
        {
            //  Use the font bitmaps object to render the text.
            fontBitmaps.DrawText(this, x, y, r, g, b, faceName, fontSize, text);
        }

        /// <summary>
        /// Draws 3D text.
        /// </summary>
        /// <param name="faceName">Name of the face.</param>
        /// <param name="fontSize">Size of the font.</param>
        /// <param name="deviation">The deviation.</param>
        /// <param name="extrusion">The extrusion.</param>
        /// <param name="text">The text.</param>
        public void DrawText3D(string faceName, float fontSize, float deviation, float extrusion, string text)
        {
            //  Use the font outlines object to render the text.
            fontOutlines.DrawText(this, faceName, fontSize, deviation, extrusion, text); 
        }

    }
}
