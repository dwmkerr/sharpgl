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
using SharpGL.SceneGraph.Shaders;
using SharpGL.SceneGraph;
using System.Runtime.InteropServices;
using SharpGL.Enumerations;
using Matrix = SharpGL.SceneGraph.Matrix;

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
        private uint attrPosition = 0;
        private uint attrNormal = 1;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenGLControl_OpenGLDraw(object sender, OpenGLEventArgs args)
        {
            //  Get the OpenGL instance.
            var gl = args.OpenGL;	

            //  Clear the color and depth buffer.
            gl.ClearColor(0f, 0f, 0f, 1f);
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            //  Draw the axies.
            axies.Render(gl, RenderMode.Design);

            //  Render the trefoil knot.
            if (checkBoxUseCelShader.IsChecked == true)
                DrawTrefoilBuffers(gl);
                //DrawTrefoilCelShaded(gl);
            else
                DrawTrefoilVertices(gl);
        }

        private void DrawTrefoilVertices(OpenGL gl)
        {
            //  Render each vertex.
            gl.Begin(BeginMode.Points);
            foreach(var vertex in trefoilKnot.Vertices)
                gl.Vertex(vertex);
            gl.End();
        }

        private void DrawTrefoilBuffers(OpenGL gl)
        {
            //  Bind the vertex and index buffer.
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, vertexBuffer);
            gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, indexBuffer);

            gl.EnableVertexAttribArray(attrPosition);
            gl.EnableVertexAttribArray(attrNormal);

            //  Draw the geometry, straight from the vertex buffer.
            gl.VertexAttribPointer(attrPosition, 3, OpenGL.GL_FLOAT, false, Marshal.SizeOf(typeof(Vertex)), IntPtr.Zero);
            int normalOffset = Marshal.SizeOf(typeof(Vertex));
            gl.VertexAttribPointer(attrNormal, 3, OpenGL.GL_FLOAT, false, Marshal.SizeOf(typeof(Vertex)), IntPtr.Add(new IntPtr(0), normalOffset));

            gl.DrawElements(OpenGL.GL_LINES, (int)trefoilKnot.IndexCount, OpenGL.GL_UNSIGNED_SHORT, IntPtr.Zero);

        }

        private void DrawTrefoilCelShaded(OpenGL gl)
        {
            //  Use the shader program.
            gl.UseProgram(shaderProgram.ProgramObject);

            //  Set the variables for the shader program.
            gl.Uniform3(toonUniforms.DiffuseMaterial, 0f, 0.75f, 0.75f);
            gl.Uniform3(toonUniforms.AmbientMaterial, 0.04f, 0.04f, 0.04f);
            gl.Uniform3(toonUniforms.SpecularMaterial, 0.5f, 0.5f, 0.5f);
            gl.Uniform1(toonUniforms.Shininess, 50f);

            //  Set the light position.
            gl.Uniform3(toonUniforms.LightPosition, 1, new float[4] { 0.25f, 0.25f, 1f, 0f });

            //  Set the matrices.
            gl.UniformMatrix4(toonUniforms.Projection, 1, false, projection.AsColumnMajorArrayFloat);
            gl.UniformMatrix4(toonUniforms.Modelview, 1, false, modelView.AsColumnMajorArrayFloat);
            gl.UniformMatrix3(toonUniforms.NormalMatrix, 1, false, normalMatrix.AsColumnMajorArrayFloat);

            //  Bind the vertex and index buffer.
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, vertexBuffer);
            gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, indexBuffer);

            gl.EnableVertexAttribArray(attrPosition);
            gl.EnableVertexAttribArray(attrNormal);

            //  Draw the geometry, straight from the vertex buffer.
            gl.VertexAttribPointer(attrPosition, 3, OpenGL.GL_FLOAT, false, Marshal.SizeOf(typeof(Vertex)), IntPtr.Zero);
            int normalOffset = Marshal.SizeOf(typeof(Vertex));
            gl.VertexAttribPointer(attrNormal, 3, OpenGL.GL_FLOAT, false, Marshal.SizeOf(typeof(Vertex)), IntPtr.Add(new IntPtr(0), normalOffset));

            gl.DrawElements(OpenGL.GL_TRIANGLES, (int)trefoilKnot.IndexCount, OpenGL.GL_UNSIGNED_SHORT, IntPtr.Zero);
        }

        private void OpenGLControl_OpenGLInitialized(object sender, OpenGLEventArgs args)
        {
            OpenGL gl = args.OpenGL;

            //  Configure the camera.
            camera.Position = new Vertex(10, 10, 10);
            camera.Target = new Vertex(0, 0, 0);

            //  Initialise the trefoil.
            trefoilKnot.GenerateGeometry(gl);

          /* RenderContext& rc = GlobalRenderContext;

    glswInit();
    glswSetPath("../", ".glsl");
    glswAddDirectiveToken("GL3", "#version 130");*/

            vertexBuffer = trefoilKnot.VertexAndNormalBuffer;
            indexBuffer = trefoilKnot.IndexBuffer;

            //  Create a shader program.
            shaderProgram.CreateInContext(gl);

            //  Create the vertex program.
            vertexShader.CreateInContext(gl);
            vertexShader.LoadSource("PerPixelLightingVertex.glsl");
            vertexShader.Compile();

            var compileStatus = vertexShader.CompileStatus;
            var info = vertexShader.InfoLog;

            //  Create the fragment program.
            fragmentShader.CreateInContext(gl); 
            fragmentShader.LoadSource("PerPixelLightingFragment.glsl");
            fragmentShader.Compile();

            //  Attach the shaders to the program.
            shaderProgram.AttachShader(vertexShader);
            shaderProgram.AttachShader(fragmentShader);
            shaderProgram.Link();
    /*
#if defined(__APPLE__)
    rc.ToonHandle = BuildProgram("Toon.Vertex.GL2", "Toon.Fragment.GL2");
#else
    rc.ToonHandle = BuildProgram("Toon.Vertex.GL3", "Toon.Fragment.GL3");
#endif*/

            toonUniforms.Projection = gl.GetUniformLocation(shaderProgram.ProgramObject, "Projection");
            toonUniforms.Modelview = gl.GetUniformLocation(shaderProgram.ProgramObject, "Modelview");
            toonUniforms.NormalMatrix = gl.GetUniformLocation(shaderProgram.ProgramObject, "NormalMatrix");
            toonUniforms.LightPosition = gl.GetUniformLocation(shaderProgram.ProgramObject, "LightPosition");
            toonUniforms.AmbientMaterial = gl.GetUniformLocation(shaderProgram.ProgramObject, "AmbientMaterial");
            toonUniforms.DiffuseMaterial = gl.GetUniformLocation(shaderProgram.ProgramObject, "DiffuseMaterial");
            toonUniforms.SpecularMaterial = gl.GetUniformLocation(shaderProgram.ProgramObject, "SpecularMaterial");
            toonUniforms.Shininess = gl.GetUniformLocation(shaderProgram.ProgramObject, "Shininess");

            gl.Enable(OpenGL.GL_DEPTH_TEST);
        }
        

        private float elapsedMilliseconds = 100;

        private float theta = 0;
        private float time = 0;
        private const float InitialPause = 0;
        private const bool LoopForever = true;

        private uint vertexBuffer = 0;
        private uint indexBuffer = 0;
        private ShaderUniforms toonUniforms = new ShaderUniforms();
        private ShaderProgram shaderProgram = new ShaderProgram();
        private VertexShader vertexShader = new VertexShader();
        private FragmentShader fragmentShader = new FragmentShader();
        private SharpGL.SceneGraph.Matrix modelView = new SharpGL.SceneGraph.Matrix(4, 4);
        private SharpGL.SceneGraph.Matrix projection = new SharpGL.SceneGraph.Matrix(4, 4);
        private SharpGL.SceneGraph.Matrix normalMatrix = new SharpGL.SceneGraph.Matrix(3, 3);

        private void OpenGLControl_Resized(object sender, OpenGLEventArgs args)
        {
            //  Get the OpenGL instance.
            var gl = args.OpenGL;

            //  Project.
            gl.MatrixMode(MatrixMode.Projection);
            gl.LoadIdentity();
            camera.TransformProjectionMatrix(args.OpenGL);
            gl.MatrixMode(MatrixMode.Modelview);

            modelView = gl.GetModelViewMatrix();
            projection = gl.GetProjectionMatrix();
            //  TODO: this should be to mat3
            normalMatrix = new Matrix(gl.GetModelViewMatrix());
            normalMatrix.FromOtherMatrix(gl.GetModelViewMatrix(), 3, 3);

            return;

    time += elapsedMilliseconds;
    if (time > InitialPause && (LoopForever || theta < 360))
    {
        theta += elapsedMilliseconds * 0.1f;
    }
            /*
    mat4 rotation = mat4::Rotate(theta, vec3(0, 1, 0));
    mat4 translation = mat4::Translate(0, 0, -7);

    rc.Modelview = rotation * translation;
    rc.NormalMatrix = rc.Modelview.ToMat3();

    rc.Projection = mat4::Frustum(-S, S, -H, H, 4, 10);
            */
    float S = 0.46f;
    float H = S * (float)openGlCtrl.ActualHeight / (float)openGlCtrl.ActualWidth;

            args.OpenGL.MatrixMode(MatrixMode.Projection);
            args.OpenGL.LoadIdentity();
            args.OpenGL.Frustum(-S, S, -H, H, 4, 10);
            args.OpenGL.MatrixMode(MatrixMode.Modelview);
        }

        /// <summary>
        /// The axies, which may be drawn.
        /// </summary>
        private readonly Axies axies = new Axies();

        /// <summary>
        /// A single trefoil knot instance we use as base geometry.
        /// </summary>
        private readonly TrefoilKnot trefoilKnot = new TrefoilKnot();

        /// <summary>
        /// The look-at camera.
        /// </summary>
        private readonly LookAtCamera camera = new LookAtCamera();
    }
}
