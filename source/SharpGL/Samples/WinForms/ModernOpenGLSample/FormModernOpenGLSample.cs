using System;
using System.Windows.Forms;
using SharpGL;

namespace ModernOpenGLSample
{
    /// <summary>
    /// A form to render the scene.
    /// </summary>
    public partial class FormModernOpenGLSample : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormModernOpenGLSample"/> class.
        /// </summary>
        public FormModernOpenGLSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the OpenGLInitialized event of the openGLControl1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void openGLControl1_OpenGLInitialized(object sender, EventArgs e)
        {
            //  Initialise the scene.
            scene.Initialise(openGLControl1.OpenGL, Width, Height);
        }

        /// <summary>
        /// Handles the OpenGLDraw event of the openGLControl1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="RenderEventArgs"/> instance containing the event data.</param>
        private void openGLControl1_OpenGLDraw(object sender, RenderEventArgs args)
        {
            //  Draw the scene.
            scene.Draw(openGLControl1.OpenGL);
        }

        /// <summary>
        /// The scene that we are rendering.
        /// </summary>
        private readonly Scene scene = new Scene();
    }
}
