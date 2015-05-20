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
        #region Member Variables

        /// <summary>
        /// The current OpenGL instance.
        /// </summary>
        private static OpenGL currentOpenGLInstance;

        /// <summary>
        /// The render context provider.
        /// </summary>
        private IRenderContextProvider renderContextProvider;

        /// <summary>
        /// Set to true if we're inside glBegin.
        /// </summary>
        private bool insideGLBegin;

        /// <summary>
        /// The fontbitmaps object is used to allow easy rendering of text.
        /// </summary>
        private readonly FontBitmaps fontBitmaps = new FontBitmaps();

        /// <summary>
        /// The FontOutlines object is used to allow rendering of text.
        /// </summary>
        private readonly FontOutlines fontOutlines = new FontOutlines();

        #endregion

    }
}
