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
using SharpGL.Enumerations;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Primitives;

namespace DrawingMechanismsSample
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

        private void openGLCtrl_OpenGLDraw(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
            //  Get the OpenGL instance that's been passed to us.
            OpenGL gl = args.OpenGL;

            //  Clear the color and depth buffers.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            //  Reset the modelview matrix.
            gl.LoadIdentity();

            //  Move the geometry into a fairly central position.
            gl.Translate(0f, 0.0f, -6.0f);

            axies.Render(gl, RenderMode.Design);
            

            switch(this.drawingMechanismCombo.SelectedIndex)
            {
                case 0:
                    RenderVertices_Immediate(args.OpenGL);
                    break;
                case 1:
                    RenderVertices_VertexArray(args.OpenGL);
                    break;
            }
            
            //  Flush OpenGL.
            gl.Flush();
            /*
            //  Clear the color and depth buffers.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            //  Reset the modelview matrix.
            gl.LoadIdentity(); */
        }

        private void RenderVertices_Immediate(OpenGL gl)
        {
            gl.Color(1f, 0f, 0f);
            gl.Begin(OpenGL.GL_POINTS);
            for(uint i=0;i<vertices.Length; i++)
            {
                gl.Vertex(vertices[i]);
            }
            gl.End();
        }

        private void RenderVertices_VertexArray(OpenGL gl)
        {
            gl.EnableClientState(OpenGL.GL_VERTEX_ARRAY);
            gl.VertexPointer(3, 0, vertexArrayValues);
            gl.DrawArrays(OpenGL.GL_POINTS, 0, 2);//vertices.Length);
            gl.DisableClientState(OpenGL.GL_VERTEX_ARRAY);
        }

        private void openGLCtrl_OpenGLInitialized(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
            //  Create the vertices.
            vertices = GeometryGenerator.GenerateGeometry(Properties.Settings.Default.NumberOfVertices, 1f);

            //  Override with test values if needed.
            ProvideTestValues(vertices);

            vertexArrayValues = new float[vertices.Length * 3];
            uint counter = 0;
            for (uint i = 0; i < vertices.Length; i++)
            {
                vertexArrayValues[counter++] = vertices[i].X;
                vertexArrayValues[counter++] = vertices[i].Y;
                vertexArrayValues[counter++] = vertices[i].Z;
            }
            
            args.OpenGL.PointSize(3.0f);
        }

        private static void ProvideTestValues(Vertex[] vertices)
        {
            vertices[0].X = 1f;
            vertices[0].Y = 1f;
            vertices[0].Z = 1f;

            vertices[1].X = 0f;
            vertices[1].Y = 0f;
            vertices[1].Z = 0f;

            vertices[2].X = 0.5f;
            vertices[2].Y = 0.5f;
            vertices[2].Z = 0.5f;
        }

        private Axies axies = new Axies();
        private Vertex[] vertices = null;
        private float[] vertexArrayValues;
        private LookAtCamera camera = new LookAtCamera();
    }
}
