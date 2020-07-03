using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace SharpGL.WPF
{
    /// <summary>
    /// RoutedEvent arguments for OpenGL events.
    /// </summary>
    public class OpenGLRoutedEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGLRoutedEventArgs"/> class.
        /// </summary>
        /// <param name="routedEvent">The routed event.</param>
        /// <param name="gl">The OpenGL instance.</param>
        public OpenGLRoutedEventArgs(RoutedEvent routedEvent, OpenGL gl)
            : base(routedEvent)
        {
            OpenGL = gl;
        }

        /// <summary>
        /// Gets or sets the OpenGL instance.
        /// </summary>
        /// <value>The the OpenGL instance.</value>
        public OpenGL OpenGL { get; private set; } = null;
    }

    /// <summary>
    /// The OpenGL Event Handler delegate.
    /// </summary>
    public delegate void OpenGLRoutedEventHandler(object sender, OpenGLRoutedEventArgs args);
}