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
using SharpGL;
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Primitives;
using SharpGL.SceneGraph;
using System.Runtime.InteropServices;
using SharpGL.Enumerations;
using GlmNet;
using SharpGL.Shaders;
using System.Reflection;
using System.IO;

namespace CelShadingSample
{
    public struct ShaderUniforms
    {
        public int Projection;
        public int Modelview;
        public int NormalMatrix;
        public int LightPosition;
        public int AmbientMaterial;
        public int DiffuseMaterial;
        public int SpecularMaterial;
        public int Shininess;
    };

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
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            //  Draw the axies.
            axies.Render(gl, RenderMode.Design);

            //  Render the scene in either immediate or retained mode.
            switch (comboRenderMode.SelectedIndex)
            {
                case 0: scene.RenderImmediateMode(gl); break;
                case 1: scene.RenderRetainedMode(gl); break;
                default: break;
            }
        }
        
        private void OpenGLControl_OpenGLInitialized(object sender, OpenGLEventArgs args)
        {
            OpenGL gl = args.OpenGL;
            
            //  Initialise the scene.
            scene.Initialise(gl);

            gl.Enable(OpenGL.GL_DEPTH_TEST);
            gl.PolygonMode(FaceMode.FrontAndBack, PolygonMode.Lines);
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
                

        private float elapsedMilliseconds = 100;

        private float theta = 0;
        private float time = 0;
        private const float InitialPause = 0;
        private const bool LoopForever = true;

        private uint vertexBuffer = 0;
        private uint indexBuffer = 0;
        private ShaderUniforms toonUniforms = new ShaderUniforms();
        private ShaderProgram shaderProgram;


        /// <summary>
        /// The scene we're drawing.
        /// </summary>
        private Scene scene = new Scene();
    }
}
