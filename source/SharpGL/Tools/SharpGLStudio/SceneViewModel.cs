using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apex.MVVM;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Lighting;
using SharpGL;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Cameras;
using System.Collections.ObjectModel;
using SharpGL.Version;

namespace SharpGLStudio
{
    public class SceneViewModel : ViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SceneViewModel"/> class.
        /// </summary>
        public SceneViewModel()
        {
            //  Create the scene.    
            CreateScene();

            ribbonViewModel.Scene = Scene;
        }

        /// <summary>
        /// Creates the scene.
        /// </summary>
        private void CreateScene()
        {
            //  Create OpenGL.
            gl.Create(OpenGLVersion.OpenGL4_4, RenderContextType.DIBSection, 800, 600, 32, null);

            //   Create the scene object.
            Scene = new Scene() { OpenGL = gl };

            //  Initialise the scene object.
            SharpGL.SceneGraph.Helpers.SceneHelper.InitialiseModelingScene(Scene);

            //  Create a sphere.
            SharpGL.SceneGraph.Quadrics.Sphere sphere = new SharpGL.SceneGraph.Quadrics.Sphere();

            //  Add it.
            Scene.SceneContainer.AddChild(sphere);

            //  Create the arcball camera.
            var camera = new ArcBallCamera()
            {
                Position = new Vertex(-10, -10, 10)
            };
            Camera = camera;
            Scene.CurrentCamera = camera;
        }

        /// <summary>
        /// The main OpenGL instance.
        /// </summary>
        private OpenGL gl = new OpenGL();
        
        private NotifyingProperty SceneProperty =
          new NotifyingProperty("Scene", typeof(Scene), default(Scene));

        public Scene Scene
        {
            get { return (Scene)GetValue(SceneProperty); }
            set { SetValue(SceneProperty, value); }
        }

        
        private NotifyingProperty SelectedSceneElementProperty =
          new NotifyingProperty("SelectedSceneElement", typeof(SceneElement), default(SceneElement));

        public SceneElement SelectedSceneElement
        {
            get { return (SceneElement)GetValue(SelectedSceneElementProperty); }
            set { SetValue(SelectedSceneElementProperty, value); }
        }
                

        private RibbonViewModel ribbonViewModel = new RibbonViewModel();

        public RibbonViewModel RibbonViewModel
        {
            get { return ribbonViewModel; }
        }

        
        private NotifyingProperty CameraProperty =
          new NotifyingProperty("Camera", typeof(Camera), default(Camera));

        public Camera Camera
        {
            get { return (Camera)GetValue(CameraProperty); }
            set { SetValue(CameraProperty, value); }
        }

        /// <summary>
        /// The selected elements.
        /// </summary>
        private ObservableCollection<SceneElement> selectedElements
            = new ObservableCollection<SceneElement>();

        /// <summary>
        /// Gets the selected elements.
        /// </summary>
        public ObservableCollection<SceneElement> SelectedElements
        {
            get { return selectedElements; }
        }
    }
}
