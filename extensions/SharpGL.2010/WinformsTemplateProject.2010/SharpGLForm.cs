using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpGL;

namespace $safeprojectname$
{
    /// <summary>
    /// The main form class.
    /// </summary>
    public partial class SharpGLForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SharpGLForm"/> class.
        /// </summary>
        public SharpGLForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the OpenGLDraw event of the openGLControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RenderEventArgs"/> instance containing the event data.</param>
        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs e)
        {
            //  Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;

            //  Clear the color and depth buffer.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            //  Load the identity matrix.
            gl.LoadIdentity();

            //  Rotate around the Y axis.
            gl.Rotate(rotation, 0.0f, 1.0f, 0.0f);

            //  Draw a coloured pyramid.
            gl.Begin(OpenGL.GL_TRIANGLES);
                gl.Color(1.0f, 0.0f, 0.0f);			
                gl.Vertex(0.0f, 1.0f, 0.0f);		
                gl.Color(0.0f, 1.0f, 0.0f);			
                gl.Vertex(-1.0f, -1.0f, 1.0f);		
                gl.Color(0.0f, 0.0f, 1.0f);			
                gl.Vertex(1.0f, -1.0f, 1.0f);	
                gl.Color(1.0f, 0.0f, 0.0f);			
                gl.Vertex(0.0f, 1.0f, 0.0f);		
                gl.Color(0.0f, 0.0f, 1.0f);			
                gl.Vertex(1.0f, -1.0f, 1.0f);		
                gl.Color(0.0f, 1.0f, 0.0f);			
                gl.Vertex(1.0f, -1.0f, -1.0f);		
                gl.Color(1.0f, 0.0f, 0.0f);			
                gl.Vertex(0.0f, 1.0f, 0.0f);		
                gl.Color(0.0f, 1.0f, 0.0f);			
                gl.Vertex(1.0f, -1.0f, -1.0f);		
                gl.Color(0.0f, 0.0f, 1.0f);			
                gl.Vertex(-1.0f, -1.0f, -1.0f);		
                gl.Color(1.0f, 0.0f, 0.0f);			
                gl.Vertex(0.0f, 1.0f, 0.0f);		
                gl.Color(0.0f, 0.0f, 1.0f);			
                gl.Vertex(-1.0f, -1.0f, -1.0f);		
                gl.Color(0.0f, 1.0f, 0.0f);			
                gl.Vertex(-1.0f, -1.0f, 1.0f);		
            gl.End();	
			
		    //  Nudge the rotation.
            rotation += 3.0f;
        }



        /// <summary>
        /// Handles the OpenGLInitialized event of the openGLControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            //  TODO: Initialise OpenGL here.

            //  Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;

            //  Set the clear color.
            gl.ClearColor(0, 0, 0, 0);
        }

        /// <summary>
        /// Handles the Resized event of the openGLControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void openGLControl_Resized(object sender, EventArgs e)
        {
            //  TODO: Set the projection matrix here.

            //  Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;

            //  Set the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);

            //  Load the identity.
            gl.LoadIdentity();

            //  Create a perspective transformation.
            gl.Perspective(60.0f, (double)Width / (double)Height, 0.01, 100.0);

            //  Use the 'look at' helper function to position and aim the camera.
            gl.LookAt(-5, 5, -5, 0, 0, 0, 0, 1, 0);

            //  Set the modelview matrix.
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }

        /// <summary>
        /// The current rotation.
        /// </summary>
        private float rotation = 0.0f;
    }
}
