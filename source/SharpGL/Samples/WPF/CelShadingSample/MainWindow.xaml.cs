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
            gl.ClearColor(0f, 0f, 0f, 1f);
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT | OpenGL.GL_STENCIL_BUFFER_BIT);
            
            //  Render the scene in either immediate or retained mode.
            switch (comboRenderMode.SelectedIndex)
            {
                case 0: 
                    {
                        scene.RenderRetainedMode(gl, checkBoxUseToonShader.IsChecked.Value); break;
                    }
                case 1:
                    {
                        axies.Render(gl, RenderMode.Design);
                        scene.RenderImmediateMode(gl); 
                        break;
                    }
            }
        }
        
        private void OpenGLControl_OpenGLInitialized(object sender, OpenGLEventArgs args)
        {
            OpenGL gl = args.OpenGL;
            
            //  Initialise the scene.
            scene.Initialise(gl);
        }

        private void OpenGLControl_Resized(object sender, OpenGLEventArgs args)
        {
            //  Get the OpenGL instance.
            var gl = args.OpenGL;       
     
            //  Create the projection matrix for the screen size.
            scene.CreateProjectionMatrix(gl, (float)ActualWidth, (float)ActualHeight);
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
