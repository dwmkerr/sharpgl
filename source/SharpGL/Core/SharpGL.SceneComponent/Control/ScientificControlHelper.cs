using SharpGL.Enumerations;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Effects;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// Initialize <see cref="ScientificVisual3DControl"/>'s Scene and UIScene.
    /// </summary>
    internal class ScientificControlHelper
    {
        //internal static void InitializeUIScene(ScientificControl scientificVisual3DControl)
        //{
        //    if (scientificVisual3DControl == null) { return; }

        //    MyScene UIScene = new MyScene();
        //    SceneElement root = UIScene.SceneContainer;
        //    UIScene.IsClear = false;
        //    root.Name = "UI Scene's container";
        //    root.Children.Clear();
        //    root.Effects.Clear();
        //    UIScene.OpenGL = scientificVisual3DControl.OpenGL;
        //    scientificVisual3DControl.UIScene = UIScene;

        //    Initialize2DUI(scientificVisual3DControl);

        //    InitializeUISceneAttributes(root);

        //    SetSceneCameraToUICamera(scientificVisual3DControl);

        //    UIScene.RenderBoundingVolumes = false;
        //}
        //internal static void SetSceneCameraToUICamera(ScientificControl scientificVisual3DControl)
        //{
        //    ScientificCamera camera = scientificVisual3DControl.Scene.CurrentCamera;
        //    if (camera == null)
        //    { throw new Exception("this.Scene.CurrentCamera cannot be null."); }

        //    scientificVisual3DControl.UIScene.CurrentCamera = camera;
        //    scientificVisual3DControl.uiAxis.Camera = camera;
        //    scientificVisual3DControl.CameraRotation.Camera = camera;
        //}

        //private static OpenGLAttributesEffect InitializeUISceneAttributes(SceneElement parent)
        //{
        //    //  Create a set of scene attributes.
        //    OpenGLAttributesEffect sceneAttributes = new OpenGLAttributesEffect()
        //    {
        //        Name = "UI Scene Attributes"
        //    };

        //    //  Specify the scene attributes.
        //    sceneAttributes.EnableAttributes.EnableDepthTest = true;
        //    sceneAttributes.EnableAttributes.EnableNormalize = true;
        //    sceneAttributes.EnableAttributes.EnableLighting = false;
        //    sceneAttributes.EnableAttributes.EnableTexture2D = true;
        //    sceneAttributes.EnableAttributes.EnableBlend = true;
        //    sceneAttributes.ColorBufferAttributes.BlendingSourceFactor = BlendingSourceFactor.SourceAlpha;
        //    sceneAttributes.ColorBufferAttributes.BlendingDestinationFactor = BlendingDestinationFactor.OneMinusSourceAlpha;
        //    sceneAttributes.LightingAttributes.TwoSided = true;
        //    sceneAttributes.LightingAttributes.AmbientLight = new GLColor(1, 1, 1, 1);
        //    parent.AddEffect(sceneAttributes);

        //    return sceneAttributes;
        //}

        //private static void Initialize2DUI(ScientificControl scientificVisual3DControl)
        //{
        //    SceneElement parent = scientificVisual3DControl.UIScene.SceneContainer;
        //    SimpleUIAxis uiAxis = new SimpleUIAxis(
        //        AnchorStyles.Left | AnchorStyles.Bottom,
        //        new Padding(10, 0, 0, 20), new Size(40, 40)) { Name = "UI: Axis", };
        //    parent.AddChild(uiAxis);
        //    scientificVisual3DControl.uiAxis = uiAxis;

        //    SimpleUIColorIndicator uiColorIndicator = new SimpleUIColorIndicator(
        //        AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right,
        //        new Padding(80, 0, 80, 40), new Size(100, 15)) { Name = "UI: Color Indicator", };
        //    ColorIndicatorData rainbow = ColorIndicatorDataFactory.CreateRainbow();
        //    uiColorIndicator.Data = rainbow;
        //    parent.AddChild(uiColorIndicator);
        //    scientificVisual3DControl.uiColorIndicator = uiColorIndicator;

        //}

        internal static void InitializeScene(ScientificControl scientificVisual3DControl)
        {
            if (scientificVisual3DControl == null) { return; }

            MyScene scene = scientificVisual3DControl.Scene;
            SceneElement root = scene.SceneContainer;
            root.Name = string.Format("Scene's container in {0}.", scientificVisual3DControl.GetType().Name);
            root.Children.Clear();
            root.Effects.Clear();


            //InitializeSceneAttributes(root);

            //scene.RenderBoundingVolumes = false;

            //SceneElement scientificModelElementRoot = new SceneElement() 
            //{ Name = "Scientific Model Elements' root" };
            //scene.SceneContainer.AddChild(scientificModelElementRoot);
            //scientificVisual3DControl.scientificModelElementRoot = scientificModelElementRoot;
            ModelContainer container = new ModelContainer() { Name = "model space's container which contains models as children." };
            root.AddChild(container);
            scientificVisual3DControl.modelContainer = container;

            scientificVisual3DControl.CameraRotation = new SatelliteRotation();
            scientificVisual3DControl.CameraRotation.Camera = scientificVisual3DControl.Scene.CurrentCamera;
        }

        //private static OpenGLAttributesEffect InitializeSceneAttributes(SceneElement parent)
        //{
        //    //  Create a set of scene attributes.
        //    OpenGLAttributesEffect sceneAttributes = new OpenGLAttributesEffect()
        //    {
        //        Name = "Scene Attributes"
        //    };

        //    //  Specify the scene attributes.
        //    sceneAttributes.EnableAttributes.EnableDepthTest = true;
        //    sceneAttributes.EnableAttributes.EnableNormalize = true;
        //    sceneAttributes.EnableAttributes.EnableLighting = false;
        //    sceneAttributes.EnableAttributes.EnableTexture2D = true;
        //    sceneAttributes.EnableAttributes.EnableBlend = true;
        //    sceneAttributes.ColorBufferAttributes.BlendingSourceFactor = BlendingSourceFactor.SourceAlpha;
        //    sceneAttributes.ColorBufferAttributes.BlendingDestinationFactor = BlendingDestinationFactor.OneMinusSourceAlpha;
        //    sceneAttributes.LightingAttributes.TwoSided = true;
        //    sceneAttributes.LightingAttributes.AmbientLight = new GLColor(1, 1, 1, 1);
        //    parent.AddEffect(sceneAttributes);

        //    return sceneAttributes;
        //}
    }
}
