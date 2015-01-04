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

        private void OpenGLControl_OpenGLDraw(object sender, OpenGLEventArgs args)
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

            rotation += 1.0f;
            program.Pop(gl, null);
        }

        float rotation = 0;

        private void OpenGLControl_OpenGLInitialized(object sender, OpenGLEventArgs args)
        {
            OpenGL gl = args.OpenGL;

            gl.Enable(OpenGL.GL_DEPTH_TEST);

            float[] global_ambient = new float[] { 0.5f, 0.5f, 0.5f, 1.0f };
            float[] light0pos = new float[] { 0.5f, 0.75f, 0.75f, 1.0f };
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
                "varying vec3 normal;" + Environment.NewLine +

                "void main()" + Environment.NewLine +
                "{" + Environment.NewLine +
                    "normal = gl_NormalMatrix * gl_Normal;" + Environment.NewLine +
                    "gl_Position = ftransform();" + Environment.NewLine +
                "}" + Environment.NewLine
            );

            //  Create a fragment shader.
            FragmentShader fragmentShader = new FragmentShader();
            fragmentShader.CreateInContext(gl);
            fragmentShader.SetSource(
                "varying vec3 normal;" + Environment.NewLine +

                "void main()" + Environment.NewLine +
                "{" + Environment.NewLine +
                    "float intensity;" + Environment.NewLine +
                    "vec4 color;" + Environment.NewLine +
                    "vec3 n = normalize(normal);" + Environment.NewLine +
                    "intensity = dot(vec3(gl_LightSource[0].position), n);" + Environment.NewLine +

                    "if (intensity > 0.95)" + Environment.NewLine +
                        "color = vec4(0.5, 0.5, 1.0, 1.0);" + Environment.NewLine +
                    "else if (intensity > 0.5)" + Environment.NewLine +
                        "color = vec4(0.3, 0.3, 0.6, 1.0);" + Environment.NewLine +
                    "else if (intensity > 0.25)" + Environment.NewLine +
                        "color = vec4(0.2, 0.2, 0.4, 1.0);" + Environment.NewLine +
                    "else" + Environment.NewLine +
                        "color = vec4(0.1, 0.1, 0.2, 1.0);" + Environment.NewLine +
                    "gl_FragColor = color;" + Environment.NewLine +
                "}" + Environment.NewLine
            );

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
