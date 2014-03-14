using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apex.MVVM;
using SharpGL.SceneGraph;
using SharpGL;
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph.Lighting;
using SharpGL.SceneGraph.Effects;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Primitives;
using SharpGL.SceneGraph.Helpers;
using SharpGL.Enumerations;
using SharpGL.Version;

namespace DataLoader
{
    public class MainViewModel : ViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        public MainViewModel()
        {
            loadDataCommand = new Command(DoLoadData, true);
            saveCommand = new Command(DoSaveCommand, true);
            rotateXCommand = new Command(
                () =>
                {
                    foreach (var polygon in Scene.SceneContainer.Traverse<Polygon>())
                        polygon.Transformation.RotateX += 90f;
                }, true);
            rotateYCommand = new Command(
                () =>
                {
                    foreach (var polygon in Scene.SceneContainer.Traverse<Polygon>())
                        polygon.Transformation.RotateY += 90f;
                }, true);
            rotateZCommand = new Command(
                () =>
                {
                    foreach (var polygon in Scene.SceneContainer.Traverse<Polygon>())
                        polygon.Transformation.RotateZ += 90f;
                }, true);
            CreateScene();
        }

        /// <summary>
        /// Creates the scene.
        /// </summary>
        private void CreateScene()
        {
            //  Create the perspective camera.
            
            var camera = new ArcBallCamera()
            {
                Name = "Look at camera",
                Position = new Vertex(-10, -10, 10)
            };
            Camera = camera;
            /*
            //  Create the look at camera.
            var camera = new LookAtCamera()
            {
                Name = "Look at camera",
                Position = new Vertex(-10, -10, 10),
                Target = new Vertex(0, 0, 0),
                UpVector = new Vertex(0, 0, 1)
            };*/
            Camera = camera;

            //  Create OpenGL.
            gl.Create(OpenGLVersion.OpenGL4_4, RenderContextType.DIBSection, 800, 600, 32, null);

            //   Create the scene object.
            Scene = new Scene() { OpenGL = gl };

            //  Initialise the scene.
            SceneHelper.InitialiseModelingScene(Scene);
            Scene.CurrentCamera = camera;

            //  Set the polygon mode.
            openGlAttributes = (Scene.SceneContainer.Effects[0] as OpenGLAttributesEffect);
            openGlAttributes.PolygonAttributes.PolygonMode = PolygonMode.Lines;

            //  Set the attributes as a scene level effect.
            Scene.SceneContainer.AddEffect(openGlAttributes);
        }
        
        private Command loadDataCommand;
        private Command saveCommand;
        private Command rotateXCommand;
        private Command rotateYCommand;
        private Command rotateZCommand;

        /// <summary>
        /// Does the load data.
        /// </summary>
        /// <param name="o">The o.</param>
        private void DoLoadData(object o)
        {
            //  Cast the data.
            Scene scene = o as Scene;

            //  Get the polygons (traverse and store, as we'll remove polygons
            //  from one scene as we add them to the other).
            var polygons = scene.SceneContainer.Traverse<Polygon>().ToList();

            //  Add each polygon.
            foreach (var polygon in polygons)
            {
                //  Get the bounds of the polygon.
                BoundingVolume boundingVolume = polygon.BoundingVolume;
                float[] extent = new float[3];
                polygon.BoundingVolume.GetBoundDimensions(out extent[0], out extent[1], out extent[2]);

                //  Get the max extent.
                float maxExtent = extent.Max();

                //  Scale so that we are at most 10 units in size.
                float scaleFactor = maxExtent > 10 ? 10.0f / maxExtent : 1;

                polygon.Parent.RemoveChild(polygon);
                polygon.Transformation.ScaleX = scaleFactor;
                polygon.Transformation.ScaleY = scaleFactor;
                polygon.Transformation.ScaleZ = scaleFactor;
                polygon.Freeze(gl);
                Scene.SceneContainer.AddChild(polygon);

                //  Add effects.
                polygon.AddEffect(new OpenGLAttributesEffect());
            }
        }

        private void DoSaveCommand(object param)
        {
            string path = (string)param;


            var sglSerializer = new SharpGL.Serialization.SharpGL.SharpGLXmlFormat();
            sglSerializer.SaveData(Scene, path);
        }

        public Command LoadDataCommand
        {
            get { return loadDataCommand; }
        }

        public Command RotateXCommand
        {
            get { return rotateXCommand; }
        }

        public Command RotateYCommand
        {
            get { return rotateYCommand; }
        }

        public Command RotateZCommand
        {
            get { return rotateZCommand; }
        }
        
        private NotifyingProperty SceneProperty =
          new NotifyingProperty("Scene", typeof(Scene), default(Scene));

        public Scene Scene
        {
            get { return (Scene)GetValue(SceneProperty); }
            set { SetValue(SceneProperty, value); }
        }

        
        private NotifyingProperty CameraProperty =
          new NotifyingProperty("Camera", typeof(Camera), default(Camera));

        public Camera Camera
        {
            get { return (Camera)GetValue(CameraProperty); }
            set { SetValue(CameraProperty, value); }
        }
                

        private OpenGL gl = new OpenGL();

        private OpenGLAttributesEffect openGlAttributes = new OpenGLAttributesEffect();

        
        private NotifyingProperty PolygonModeProperty =
          new NotifyingProperty("PolygonMode", typeof(PolygonMode), PolygonMode.Lines);

        public PolygonMode PolygonMode
        {
            get { return (PolygonMode)GetValue(PolygonModeProperty); }
            set 
            { 
                SetValue(PolygonModeProperty, value);
                openGlAttributes.PolygonAttributes.PolygonMode = PolygonMode;
            }
        }

        public Command SaveCommand
        {
            get { return saveCommand; }
        }
    }
}
