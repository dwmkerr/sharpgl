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
        #region Properties

        /// <summary>
        /// Gets the render context provider.
        /// </summary>
        /// <value>The render context provider.</value>
        public IRenderContextProvider RenderContextProvider
        {
            get { return renderContextProvider; }
        }

        /// <summary>
        /// Gets the vendor.
        /// </summary>
        /// <value>The vendor.</value>
        public string Vendor
		{
            get { return GetString(GL_VENDOR); }
		}

        /// <summary>
        /// Gets the renderer.
        /// </summary>
        /// <value>The renderer.</value>
        public string Renderer
		{
            get { return GetString(GL_RENDERER); }
		}

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>The version.</value>
        public string Version
		{
            get { return GetString(GL_VERSION); }
		}

        /// <summary>
        /// Gets the extensions.
        /// </summary>
        /// <value>The extensions.</value>
        public string Extensions
		{
            get { return GetString(GL_EXTENSIONS); }
        }

        

        #endregion
    }
}
