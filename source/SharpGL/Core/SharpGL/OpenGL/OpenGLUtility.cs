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

		#region Utility Functions

		/// <summary>
		/// This function transforms a windows point into an OpenGL point,
		/// which is measured from the bottom left of the screen.
		/// </summary>
		/// <param name="x">The x coord.</param>
		/// <param name="y">The y coord.</param>
		public void GDItoOpenGL(ref int x, ref int y)
		{
			//	Create an array that will be the viewport.
			var viewport = new int[4];
			
			//	Get the viewport, then convert the mouse point to an opengl point.
			GetInteger(GL_VIEWPORT, viewport);
			y = viewport[3] - y;
		}

        /// <summary>
        /// GDIs the coordinateto open GL coordinate.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public virtual void GDICoordinatetoOpenGLCoordinate(ref int x, ref int y)
        {
            //	Create an array that will be the viewport.
            var viewport = new int[4];

            //	Get the viewport, then convert the mouse point to an opengl point.
            glGetIntegerv(GL_VIEWPORT, viewport);
            y = viewport[3] - y;
        }


		#endregion

    }
}
