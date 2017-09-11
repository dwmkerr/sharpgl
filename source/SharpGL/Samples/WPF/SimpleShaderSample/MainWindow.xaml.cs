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
using SharpGL.SceneGraph.Primitives;
using SharpGL.SceneGraph.Shaders;
using SharpGL.SceneGraph;
using SharpGL.WPF;

namespace SimpleShaderSample
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

        private void OpenGLControl_OpenGLDraw(object sender, OpenGLRoutedEventArgs args)
        {
            OpenGL gl = args.OpenGL;	
            
            // Clear The Screen And The Depth Buffer
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            
            // Move Left And Into The Screen
            gl.LoadIdentity();
            gl.Translate(0.0f, 0.0f, -6.0f);

            program.Push(gl, null);
            gl.Rotate(rotation, 0.0f, 1.0f, 0.0f);

            Teapot tp = new Teapot();
            tp.Draw(gl, 14, 1, OpenGL.GL_FILL);

            rotation += 3.0f;
            program.Pop(gl, null);
        }

        float rotation = 0;

        private void OpenGLControl_OpenGLInitialized(object sender, OpenGLRoutedEventArgs args)
        {
            OpenGL gl = args.OpenGL;

            gl.Enable(OpenGL.GL_DEPTH_TEST);

            float[] global_ambient = new float[] { 0.5f, 0.5f, 0.5f, 1.0f };
            float[] light0pos = new float[] { 0.0f, 5.0f, 10.0f, 1.0f };
            float[] light0ambient = new float[] { 0.2f, 0.2f, 0.2f, 1.0f };
            float[] light0diffuse = new float[] { 0.3f, 0.3f, 0.3f, 1.0f };
            float[] light0specular = new float[] { 0.8f, 0.8f, 0.8f, 1.0f };

            float[] lmodel_ambient = new float[] { 0.2f, 0.2f, 0.2f, 1.0f };
            gl.LightModel(OpenGL.GL_LIGHT_MODEL_AMBIENT, lmodel_ambient);

            gl.LightModel(OpenGL.GL_LIGHT_MODEL_AMBIENT, global_ambient);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_POSITION, light0pos);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_AMBIENT, light0ambient);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_DIFFUSE, light0diffuse);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_SPECULAR, light0specular);
            gl.Enable(OpenGL.GL_LIGHTING);
            gl.Enable(OpenGL.GL_LIGHT0);

            gl.ShadeModel(OpenGL.GL_SMOOTH);		

            //  Create a vertex shader.
            VertexShader vertexShader = new VertexShader();
            vertexShader.CreateInContext(gl);
            vertexShader.SetSource(
                "void main()" + Environment.NewLine +
                "{" + Environment.NewLine +
                "gl_Position = ftransform();" + Environment.NewLine +
                "}" + Environment.NewLine);

            //  Create a fragment shader.
            FragmentShader fragmentShader = new FragmentShader();
            fragmentShader.CreateInContext(gl);
            fragmentShader.SetSource(
                "void main()" + Environment.NewLine +
                "{" + Environment.NewLine +
                "gl_FragColor = vec4(0.4,0.4,0.8,1.0);" + Environment.NewLine +
                "}" + Environment.NewLine);

            //  Compile them both.
            vertexShader.Compile();
            fragmentShader.Compile();

            //  Build a program.
            program.CreateInContext(gl);

            //  Attach the shaders.
            program.AttachShader(vertexShader);
            program.AttachShader(fragmentShader);
            program.Link();
        }

        ShaderProgram program = new ShaderProgram();
    }
}
