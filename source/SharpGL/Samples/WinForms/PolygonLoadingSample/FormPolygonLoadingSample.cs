using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.Linq;

using SharpGL;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph.Collections;
using SharpGL.SceneGraph.Primitives;
using SharpGL.Serialization;
using SharpGL.SceneGraph.Core;
using SharpGL.Enumerations;

namespace PolygonLoadingSample
{
    public partial class FormPolygonLoadingSample : Form
    {
        public FormPolygonLoadingSample()
        {
            InitializeComponent();

            //  Get the OpenGL object, for quick access.
            OpenGL gl = this.openGLControl1.OpenGL;      

            //  A bit of extra initialisation here, we have to enable textures.
            gl.Enable(OpenGL.GL_TEXTURE_2D);
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.databasen.se/hem/johan/raytracing/rayobj.htm");
        }

        private void openGLControl1_OpenGLDraw(object sender, RenderEventArgs e)
        {
            //  Get the OpenGL object, for quick access.
            var gl = this.openGLControl1.OpenGL;

            //  Clear and load the identity.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.LoadIdentity();

            //  View from a bit away the y axis and a few units above the ground.
            gl.LookAt(-10, 10, -10, 0, 0, 0, 0, 1, 0);

            //  Rotate the objects every cycle.
            gl.Rotate(rotate, 0.0f, 0.0f, 1.0f);

            //  Move the objects down a bit so that they fit in the screen better.
            gl.Translate(0, 0, -1);
            
            //  Draw every polygon in the collection.
            foreach (Polygon polygon in polygons)
            {
                polygon.PushObjectSpace(gl);
                polygon.Render(gl, SharpGL.SceneGraph.Core.RenderMode.Render);
                polygon.PopObjectSpace(gl);
            }
            //  Rotate a bit more each cycle.
            rotate += 1.0f;
        }


        float rotate = 0;

        //  A set of polygons to draw.
        List<Polygon> polygons = new List<Polygon>();

        //  The camera.
        SharpGL.SceneGraph.Cameras.PerspectiveCamera camera = new SharpGL.SceneGraph.Cameras.PerspectiveCamera();

        /// <summary>
        /// Handles the Click event of the importPolygonToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void importPolygonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //  Show a file open dialog.
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = SerializationEngine.Instance.Filter;
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                Scene scene = SerializationEngine.Instance.LoadScene(openDialog.FileName);
                if (scene != null)
                {
                    foreach (var polygon in scene.SceneContainer.Traverse<Polygon>())
                    {                        
                        //  Get the bounds of the polygon.
                        BoundingVolume boundingVolume = polygon.BoundingVolume;
                        float[] extent = new float[3];
                        polygon.BoundingVolume.GetBoundDimensions(out extent[0], out extent[1], out extent[2]);

                        //  Get the max extent.
                        float maxExtent = extent.Max();

                        //  Scale so that we are at most 10 units in size.
                        float scaleFactor = maxExtent > 10 ? 10.0f / maxExtent : 1;
                        polygon.Transformation.ScaleX = scaleFactor;
                        polygon.Transformation.ScaleY = scaleFactor;
                        polygon.Transformation.ScaleZ = scaleFactor;
                        polygon.Freeze(openGLControl1.OpenGL);
                        polygons.Add(polygon);
                    }
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the freezeAllToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void freezeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var poly in polygons)
                poly.Freeze(openGLControl1.OpenGL);
        }

        /// <summary>
        /// Handles the Click event of the unfreezeAllToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void unfreezeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var poly in polygons)
                poly.Unfreeze(openGLControl1.OpenGL);
        }

        private void openGLControl1_OpenGLInitialized(object sender, EventArgs e)
        {
            openGLControl1.OpenGL.PolygonMode(FaceMode.FrontAndBack, PolygonMode.Lines);
        }
        
        void WireframeToolStripMenuItemClick(object sender, EventArgs e)
        {
        	wireframeToolStripMenuItem.Checked = true;
        	solidToolStripMenuItem.Checked = false;
			lightedToolStripMenuItem.Checked = false;
        	openGLControl1.OpenGL.PolygonMode(FaceMode.FrontAndBack, PolygonMode.Lines);
        	openGLControl1.OpenGL.Disable(OpenGL.GL_LIGHTING);
        }
        
        void SolidToolStripMenuItemClick(object sender, EventArgs e)
        {
        	wireframeToolStripMenuItem.Checked = false;
        	solidToolStripMenuItem.Checked = true;
        	lightedToolStripMenuItem.Checked = false;
        	openGLControl1.OpenGL.PolygonMode(FaceMode.FrontAndBack, PolygonMode.Filled);        	
        	openGLControl1.OpenGL.Disable(OpenGL.GL_LIGHTING);
        }
        
        void LightedToolStripMenuItemClick(object sender, EventArgs e)
        {
        	wireframeToolStripMenuItem.Checked = false;
        	solidToolStripMenuItem.Checked = false;
        	lightedToolStripMenuItem.Checked = true;
        	openGLControl1.OpenGL.PolygonMode(FaceMode.FrontAndBack, PolygonMode.Filled);        	
        	openGLControl1.OpenGL.Enable(OpenGL.GL_LIGHTING);
        	openGLControl1.OpenGL.Enable(OpenGL.GL_LIGHT0);
        	openGLControl1.OpenGL.Enable(OpenGL.GL_COLOR_MATERIAL);
        	
        }
        
        void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
        	Close();
        }
        
        void ClearToolStripMenuItemClick(object sender, EventArgs e)
        {
        	polygons.Clear();
        }
    }
}