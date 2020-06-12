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
using SharpGL.WPF;

namespace TwoDSample
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
        
        private void openGLControl1_OpenGLDraw(object sender, OpenGLRoutedEventArgs args)
        {
            //  If there aren't any shapes, create them.
            if (!shapes.Any())
                CreateShapes();

            //  Get the OpenGL instance.
            var gl = args.OpenGL;

            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            gl.PointSize(2.0f);

            foreach(var shape in shapes)
            {
                gl.Color(shape.Red, shape.Green, shape.Blue);

                gl.Begin(BeginMode.LineLoop);
                shape.Points.ForEach(sp => gl.Vertex(sp.Position));
                gl.End();
            }

            Tick();
        }

        private void Tick()
        {
            shapes.SelectMany(s => s.Points).ToList().ForEach(sp => ApplyVelocity(ref sp.Position, ref sp.Velocity));
        }

        private void ApplyVelocity(ref Vertex position, ref Vertex velocity)
        {
            float newX = position.X + velocity.X;
            if (newX < 0)
            {
                position.X = -newX;
                velocity.X *= -1;
            }
            else if (newX >  openGLControl1.ActualWidth)
            {
                position.X -= (newX - (float) openGLControl1.ActualWidth);
                velocity.X *= -1;
            }
            else
            {
                position.X = newX;
            }


            float newy = position.Y + velocity.Y;
            if (newy < 0)
            {
                position.Y = -newy;
                velocity.Y *= -1;
            }
            else if (newy > openGLControl1.ActualHeight)
            {
                position.Y -= (newy - (float) openGLControl1.ActualHeight);
                velocity.Y *= -1;
            }
            else
            {
                position.Y = newy;
            }
        }

        private void CreateShapes()
        {
            //  Create some shapes...
            int shapeCount = random.Next(2, 5);
            for(int i = 0; i < shapeCount; i++)
            {
                //  Create the shape.
                var shape = new CrazyShape{Red = RandomFloat(), Green = RandomFloat(),Blue = RandomFloat()};

                //  Create the points.
                int pointCount = random.Next(3, 6);
                for(int j=0; j < pointCount; j++)
                    shape.Points.Add(new ShapePoint
                                         {
                                             Position = new Vertex(RandomFloat()*(float) openGLControl1.ActualWidth,
                                                 RandomFloat()*(float) openGLControl1.ActualHeight, 0),
                                             Velocity = new Vertex(RandomFloat(1f, 10f), RandomFloat(1f, 10f), 0)
                                         });

                //  Add the shape.
                shapes.Add(shape);
            }
        }

        private float RandomFloat(float min = 0, float max = 1)
        {
            return (float) random.NextDouble()*(max - min) + min;
        }

        public double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble()*(maximum - minimum) + minimum;
        }

        private void openGLControl1_Resized(object sender, OpenGLRoutedEventArgs args)
        {
            //  Get the OpenGL instance.
            var gl = args.OpenGL;

            //  Create an orthographic projection.
            gl.MatrixMode(MatrixMode.Projection);
            gl.LoadIdentity();
            gl.Ortho(0, openGLControl1.ActualWidth, openGLControl1.ActualHeight, 0, -10, 10);

            //  Back to the modelview.
            gl.MatrixMode(MatrixMode.Modelview);
        }

        private readonly Random random = new Random();

        private readonly List<CrazyShape> shapes = new List<CrazyShape>();


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //  Clear the shapes - they'll be recreated next draw.
            shapes.Clear();
        }
    }

    public class CrazyShape
    {
        public List<ShapePoint> Points = new List<ShapePoint>();
        public float Red;
        public float Green;
        public float Blue;
    }

    public class ShapePoint
    {
        public Vertex Position;
        public Vertex Velocity;
    }   

}