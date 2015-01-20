using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using SharpGL;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Primitives;
using SharpGL.SceneGraph;

namespace CelShadingSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IList<double> _drawDurations;
        private IList<double> _fps;

        public MainWindow()
        {
            _drawDurations = new List<double>();
            _fps = new List<double>();

            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            var message = string.Format(CultureInfo.CurrentUICulture, "Average Draw Duration: {0} = {1} fps",
                _drawDurations.Average(), _fps.Average());
            MessageBox.Show(message);

            base.OnClosing(e);
        }

        private void OpenGLControl_OpenGLDraw(object sender, OpenGLEventArgs args)
        {
            var watch = new Stopwatch();
            watch.Start();

            //  Get the OpenGL instance.
            var gl = args.OpenGL;	

            //  Add a bit to theta (how much we're rotating the scene) and create the modelview
            //  and normal matrices.
            theta += 0.01f;
            scene.CreateModelviewAndNormalMatrix(theta);
            
            //  Clear the color and depth buffer.
            gl.ClearColor(0f, 0f, 0f, 1f);
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT | OpenGL.GL_STENCIL_BUFFER_BIT);
            
            //  Render the scene in either immediate or retained mode.
            switch (comboRenderMode.SelectedIndex)
            {
                case 0: 
                    {
                        scene.RenderRetainedMode(gl, checkBoxUseToonShader.IsChecked != null && checkBoxUseToonShader.IsChecked.Value); break;
                    }
                case 1:
                    {
                        axies.Render(gl, RenderMode.Design);
                        scene.RenderImmediateMode(gl); 
                        break;
                    }
                default: break;
            }

            watch.Stop();
            _drawDurations.Add(watch.Elapsed.TotalMilliseconds);

            var fps = 1000 / watch.Elapsed.TotalMilliseconds;
            _fps.Add(fps);

            var messageDuration = string.Format(CultureInfo.CurrentUICulture, "Duration (ms): {0}", watch.Elapsed.TotalMilliseconds);
            var messageFps = string.Format(CultureInfo.CurrentUICulture, "FpS: {0}", fps);

            gl.DrawText(10, 20, 1, 1, 1, "Times", 10, messageDuration);
            gl.DrawText(10, 10, 1, 1, 1, "Times", 10, messageFps);
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
