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
using Microsoft.Win32;
using SharpGL;
using SharpGL.Enumerations;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Primitives;

namespace ObjectLoadingSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenGLControl_OpenGLDraw(object sender, OpenGLEventArgs args)
        {
            //  Get the OpenGL instance.
            var gl = args.OpenGL;

            //  Add a bit to theta (how much we're rotating the scene) and create the modelview
            //  and normal matrices.
            theta += 0.01f;
            scene.CreateModelviewAndNormalMatrix(theta);

            //  Clear the color and depth buffer.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT | OpenGL.GL_STENCIL_BUFFER_BIT);

            //  Draw the axies.
            axies.Render(gl, RenderMode.Design);

            //  Render the scene in either immediate or retained mode.
            if(menuItemRetainedMode.IsChecked)
                scene.RenderRetainedMode(gl);
            else 
                scene.RenderImmediateMode(gl);
        }

        private void OpenGLControl_OpenGLInitialized(object sender, OpenGLEventArgs args)
        {
            OpenGL gl = args.OpenGL;

            //  Initialise the scene.
            scene.Initialise(gl);

            gl.Enable(OpenGL.GL_DEPTH_TEST);
        }

        private void OpenGLControl_Resized(object sender, OpenGLEventArgs args)
        {
            //  Get the OpenGL instance.
            var gl = args.OpenGL;

            //  Create a projection matrix for the scene with the screen size.
            scene.CreateProjectionMatrix((float)ActualWidth, (float)ActualHeight);

            //  When we do immediate mode drawing, OpenGL needs to know what our projection matrix
            //  is, so set it now.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            gl.MultMatrix(scene.ProjectionMatrix.to_array());
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }

        private void fileOpenItem_Click(object sender, RoutedEventArgs e)
        {
            //  Create a file open dialog.
            var fileOpenDialog = new OpenFileDialog();
            fileOpenDialog.Filter = "Wavefront Files (*.obj)|*.obj|All Files (*.*)|*.*";
            if(fileOpenDialog.ShowDialog(this) == true)
            {
                //  Get the path.
                var filePath = fileOpenDialog.FileName;

                //  Load the data into the scene.
                scene.Load(openGlCtrl.OpenGL, filePath);

                //  Auto scale.
                textBoxScale.Text = scene.SetScaleFactorAuto().ToString();
            }
        }

        /// <summary>
        /// The axies, which may be drawn.
        /// </summary>
        private readonly Axies axies = new Axies();

        private float theta = 0;

        /// <summary>
        /// The scene we're drawing.
        /// </summary>
        private readonly Scene scene = new Scene();
    }
}
