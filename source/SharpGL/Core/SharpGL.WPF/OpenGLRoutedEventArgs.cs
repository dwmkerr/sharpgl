using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace SharpGL.WPF
{
    public class OpenGLRoutedEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGLRoutedEventArgs"/> class.
        /// </summary>
        /// <param name="gl">The gl.</param>
        public OpenGLRoutedEventArgs(RoutedEvent routedEvent, OpenGL gl)
            : base(routedEvent)
        {
            OpenGL = gl;
        }

        /// <summary>
        /// The OpenGL instance.
        /// </summary>
        private OpenGL gl = null;

        /// <summary>
        /// Gets or sets the open GL.
        /// </summary>
        /// <value>The open GL.</value>
        public OpenGL OpenGL
        {
            get { return gl; }
            private set { gl = value; }
        }
    }

    /// <summary>
    /// The OpenGL Event Handler delegate.
    /// </summary>
    public delegate void OpenGLRoutedEventHandler(object sender, OpenGLRoutedEventArgs args);
}
