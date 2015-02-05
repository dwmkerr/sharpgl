using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ReSharper disable CheckNamespace
namespace SharpGL
// ReSharper restore CheckNamespace
{
// ReSharper disable InconsistentNaming

    /// <summary>
    /// Internally used only. The OpenGLAPI attribute is used to decorate functions
    /// which are wrappers around OpenGL APIs. Metadata around the APIs can be used in the
    /// future for API discovery.
    /// </summary>
    internal class OpenGLAPIAttribute : Attribute
// ReSharper restore InconsistentNaming
    {
        /// <summary>
        /// Gets a valye indicating whether the API is actually a SharpGL helper.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the API is a SharpGL helper (i.e. not available outside of SharpGL); otherwise, <c>false</c>.
        /// </value>
        public bool SharpGL { get; set; }
    }
}
