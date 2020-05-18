using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SharpGL.SceneGraph.Effects;
using SharpGL.SceneGraph.Primitives;
using SharpGL.Serialization.Wavefront;

namespace DuckySample
{
    public partial class MainForm : Form
    {
        private readonly ArcBallEffect arcBallEffect = new ArcBallEffect();

        public MainForm()
        {
            InitializeComponent();
            InitializeScene();
        }

        private void InitializeScene()
        {
            sceneControl.MouseDown += new MouseEventHandler(sceneControl_MouseDown);
            sceneControl.MouseMove += new MouseEventHandler(sceneControl_MouseMove);
            sceneControl.MouseUp += new MouseEventHandler(sceneControl_MouseUp);

            //  Add some design-time primitives.
            sceneControl.Scene.SceneContainer.AddChild(new Grid());
            sceneControl.Scene.SceneContainer.AddChild(new Axies());

            // Let's load ducky
            var obj = new ObjFileFormat();
            var objScene = obj.LoadData(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "ducky.obj"));

            // Add the materials to the scene
            foreach (var asset in objScene.Assets)
                sceneControl.Scene.Assets.Add(asset);

            // Get the polygons
            var polygons = objScene.SceneContainer.Traverse<Polygon>().ToList();

            // Add each polygon (There is only one in ducky.obj)
            foreach (var polygon in polygons)
            {
                polygon.Name = "Ducky";
                polygon.Transformation.RotateX = 90f; // So that Ducky appears in the right orientation

                //  Get the bounds of the polygon.
                var boundingVolume = polygon.BoundingVolume;
                var extent = new float[3];
                polygon.BoundingVolume.GetBoundDimensions(out extent[0], out extent[1], out extent[2]);

                // Get the max extent.
                var maxExtent = extent.Max();

                // Scale so that we are at most 10 units in size.
                var scaleFactor = maxExtent > 10 ? 10.0f / maxExtent : 1;

                polygon.Parent.RemoveChild(polygon);
                polygon.Transformation.ScaleX = scaleFactor;
                polygon.Transformation.ScaleY = scaleFactor;
                polygon.Transformation.ScaleZ = scaleFactor;
                polygon.Freeze(sceneControl.OpenGL);
                sceneControl.Scene.SceneContainer.AddChild(polygon);

                // Add effects.
                polygon.AddEffect(new OpenGLAttributesEffect());
                polygon.AddEffect(arcBallEffect);
            }
        }

        private void sceneControl_MouseDown(object sender, MouseEventArgs e)
        {
            arcBallEffect.ArcBall.SetBounds(sceneControl.Width, sceneControl.Height);
            arcBallEffect.ArcBall.MouseDown(e.X, e.Y);
        }

        private void sceneControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                arcBallEffect.ArcBall.MouseMove(e.X, e.Y);
        }

        private void sceneControl_MouseUp(object sender, MouseEventArgs e)
        {
            arcBallEffect.ArcBall.MouseUp(e.X, e.Y);
        }
    }
}
