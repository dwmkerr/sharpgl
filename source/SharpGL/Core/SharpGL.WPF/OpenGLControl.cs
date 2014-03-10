using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SharpGL.WPF
{
    /// <summary>
    /// The OpenGLControl
    /// </summary>
    public class OpenGLControl : Control
    {
        /// <summary>
        /// Initializes the <see cref="OpenGLControl"/> class.
        /// </summary>
        static OpenGLControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(OpenGLControl), new FrameworkPropertyMetadata(typeof(OpenGLControl)));
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            //  TODO: We must now draw the OpenGL scene to the drawing context.
        }
    }
}
