using SharpGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ModernOpenGLSample
{
    public partial class FormModernOpenGLControlDemo : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormModernOpenGLControlDemo"/> class.
        /// </summary>
        public FormModernOpenGLControlDemo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the OpenGLInitialized event of the openGLControl1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            //  Initialise the scene.
            this.sceneElement.Initialise(openGLControl.OpenGL, 
                openGLControl.Width, openGLControl.Height);
        }

        /// <summary>
        /// Handles the OpenGLDraw event of the openGLControl1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="RenderEventArgs"/> instance containing the event data.</param>
        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs args)
        {
            //  Draw the scene.
            this.sceneElement.Draw(openGLControl.OpenGL);
        }

        /// <summary>
        /// The scene that we are rendering.
        /// </summary>
        private readonly ModernOpenGLControlSceneElement sceneElement = new ModernOpenGLControlSceneElement();

    }
}
