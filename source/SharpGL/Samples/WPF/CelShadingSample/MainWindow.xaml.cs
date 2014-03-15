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
                //DrawTrefoilBuffers(gl);
                DrawTrefoilCelShaded(gl);
            else
                DrawTrefoilVertices(gl);
        }

        private void DrawTrefoilVertices(OpenGL gl)
        {
            //  Render each vertex.
          /*  gl.Begin(BeginMode.Points);
            foreach (var vertex in trefoilKnot.Vertices)
                gl.Vertex(vertex);
            gl.End();
            */
            //  OR  render each  triangle.
            var vertices = trefoilKnot.Vertices.ToList();
            gl.Begin(BeginMode.Triangles);
            foreach (var index in trefoilKnot.indices)
                gl.Vertex(vertices[index].x, vertices[index].y, vertices[index].z);
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

         //   gl.DrawElements(OpenGL.GL_TRIANGLES, 9/*(int)trefoilKnot.IndexCount*/, OpenGL.GL_UNSIGNED_SHORT, IntPtr.Zero);
            gl.DrawArrays(OpenGL.GL_TRIANGLES, 0, 50); // Draw our square  

        }

        private void DrawTrefoilCelShaded(OpenGL gl)
        {
            //  Use the shader program.
            shaderProgram.Bind(gl);

            //  Set the variables for the shader program.
            gl.Uniform3(toonUniforms.DiffuseMaterial, 0f, 0.75f, 0.75f);
            gl.Uniform3(toonUniforms.AmbientMaterial, 0.04f, 0.04f, 0.04f);
            gl.Uniform3(toonUniforms.SpecularMaterial, 0.5f, 0.5f, 0.5f);
            gl.Uniform1(toonUniforms.Shininess, 50f);

            //  Set the light position.
            gl.Uniform3(toonUniforms.LightPosition, 1, new float[4] { 0.25f, 0.25f, 1f, 0f });

            //  Set the matrices.
            gl.UniformMatrix4(toonUniforms.Projection, 1, false, projection.to_array());
            gl.UniformMatrix4(toonUniforms.Modelview, 1, false, modelView.to_array());
            gl.UniformMatrix3(toonUniforms.NormalMatrix, 1, false, normalMatrix.to_array());

            //  Bind the vertex and index buffer.
            DrawTrefoilBuffers(gl);

            shaderProgram.Unbind(gl);
        }

        private void OpenGLControl_OpenGLInitialized(object sender, OpenGLEventArgs args)
        {
            OpenGL gl = args.OpenGL;

            //  Configure the camera.
            camera.Position = new Vertex(2, 2, 2);
            camera.Target = new Vertex(0, 0, 0);

            //  Initialise the trefoil.
            trefoilKnot.GenerateGeometry(gl);

            vertexBuffer = trefoilKnot.VertexAndNormalBuffer;
            indexBuffer = trefoilKnot.IndexBuffer;
            
            //  Create the shader program.
            CreateShader(gl);           

            gl.Enable(OpenGL.GL_DEPTH_TEST);
            gl.PolygonMode(FaceMode.FrontAndBack, PolygonMode.Lines);
        }

        private void CreateShader(OpenGL gl)
        {
            //  Load the shader source.
            var vertexShaderSource = LoadManifestResourceTextFile("PerPixelLightingVertex.glsl");
            var fragmentShaderSource = LoadManifestResourceTextFile("PerPixelLightingFragment.glsl");

            //  Create the shader.
            shaderProgram = new ShaderProgram();
            shaderProgram.Create(gl, vertexShaderSource, fragmentShaderSource);
            
            //  Now that we've created the shader, get all of the uniform locations.
            toonUniforms.Projection = shaderProgram.GetUniformLocation(gl, "Projection");
            toonUniforms.Modelview = shaderProgram.GetUniformLocation(gl, "Modelview");
            toonUniforms.NormalMatrix = shaderProgram.GetUniformLocation(gl, "NormalMatrix");
            toonUniforms.LightPosition = shaderProgram.GetUniformLocation(gl, "LightPosition");
            toonUniforms.AmbientMaterial = shaderProgram.GetUniformLocation(gl, "AmbientMaterial");
            toonUniforms.DiffuseMaterial = shaderProgram.GetUniformLocation(gl, "DiffuseMaterial");
            toonUniforms.SpecularMaterial = shaderProgram.GetUniformLocation(gl, "SpecularMaterial");
            toonUniforms.Shininess = shaderProgram.GetUniformLocation(gl, "Shininess");
        }

        private string LoadManifestResourceTextFile(string textFileName)
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(string.Format("CelShadingSample.{0}", textFileName)))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private void OpenGLControl_Resized(object sender, OpenGLEventArgs args)
        {
            //  Get the OpenGL instance.
            var gl = args.OpenGL;

            float theta = 0.0f;

            mat4 rotation = glm.rotate(mat4.identity(), theta, new vec3(0, 1, 0));
            mat4 translation = glm.translate(mat4.identity(), new vec3(0, 0, -7));

            modelView = rotation * translation;
            normalMatrix = modelView.to_mat3();

    float S = 0.46f;
    float H = S * (float)ActualHeight / (float)ActualWidth;
    projection = glm.pfrustum(-S, S, -H, H, 4, 10);
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





        private float elapsedMilliseconds = 100;

        private float theta = 0;
        private float time = 0;
        private const float InitialPause = 0;
        private const bool LoopForever = true;

        private uint vertexBuffer = 0;
        private uint indexBuffer = 0;
        private ShaderUniforms toonUniforms = new ShaderUniforms();
        private ShaderProgram shaderProgram;

        //  We have our own matrices for this sample - no need for OpenGL matrix functionality.
        private mat4 modelView = mat4.identity();
        private mat4 projection = mat4.identity();
        private mat3 normalMatrix = mat3.identity();
    }
}
