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
    /// Initialize <see cref="ScientificVisual3DControl"/>'s UIScene.
    /// </summary>
    internal class ScientificVisual3DControlHelper
    {
        internal static void InitializeUIScene(ScientificVisual3DControl scientificVisual3DControl)
        {
            if (scientificVisual3DControl == null) { return; }

            MyScene UIScene = new MyScene();
            SceneElement root = UIScene.SceneContainer;
            UIScene.IsClear = false;
            root.Name = "UI Scene's container";
            root.Children.Clear();
            root.Effects.Clear();
            UIScene.OpenGL = scientificVisual3DControl.OpenGL;
            scientificVisual3DControl.UIScene = UIScene;

            Initialize2DUI(scientificVisual3DControl);

            InitializeUISceneAttributes(root);

            scientificVisual3DControl.SetSceneCameraToUICamera();

            UIScene.RenderBoundingVolumes = false;
        }

        private static OpenGLAttributesEffect InitializeUISceneAttributes(SceneElement parent)
        {
            //  Create a set of scene attributes.
            OpenGLAttributesEffect sceneAttributes = new OpenGLAttributesEffect()
            {
                Name = "UI Scene Attributes"
            };

            //  Specify the scene attributes.
            sceneAttributes.EnableAttributes.EnableDepthTest = true;
            sceneAttributes.EnableAttributes.EnableNormalize = true;
            sceneAttributes.EnableAttributes.EnableLighting = false;
            sceneAttributes.EnableAttributes.EnableTexture2D = true;
            sceneAttributes.EnableAttributes.EnableBlend = true;
            sceneAttributes.ColorBufferAttributes.BlendingSourceFactor = BlendingSourceFactor.SourceAlpha;
            sceneAttributes.ColorBufferAttributes.BlendingDestinationFactor = BlendingDestinationFactor.OneMinusSourceAlpha;
            sceneAttributes.LightingAttributes.TwoSided = true;
            sceneAttributes.LightingAttributes.AmbientLight = new GLColor(1, 1, 1, 1);
            parent.AddEffect(sceneAttributes);

            return sceneAttributes;
        }

        private static void Initialize2DUI(ScientificVisual3DControl scientificVisual3DControl)
        {
            SceneElement parent = scientificVisual3DControl.UIScene.SceneContainer;
            SimpleUIAxis uiAxis = new SimpleUIAxis(
                AnchorStyles.Left | AnchorStyles.Bottom,
                new Padding(10, 0, 0, 20), new Size(40, 40)) { Name = "UI: Axis", };
            parent.AddChild(uiAxis);
            scientificVisual3DControl.uiAxis = uiAxis;

            SimpleUIColorIndicator uiColorIndicator = new SimpleUIColorIndicator(
                AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right,
                new Padding(10 + 40 + 10, 0, 40, 40), new Size(100, 15)) { Name = "UI: Color Indicator", };
            ColorIndicatorData rainbow = ColorIndicatorDataFactory.CreateRainbow();
            uiColorIndicator.Data = rainbow;
            parent.AddChild(uiColorIndicator);
            scientificVisual3DControl.uiColorIndicator = uiColorIndicator;

            scientificVisual3DControl.CameraRotation = new CameraRotation();
        }

        internal static void InitializeScene(ScientificVisual3DControl scientificVisual3DControl)
        {
            if (scientificVisual3DControl == null) { return; }

            MyScene scene = scientificVisual3DControl.Scene;
            scene.SceneContainer.Name = "Scene's container";
            scene.SceneContainer.Children.Clear();
            scene.SceneContainer.Effects.Clear();

            SceneElement root = scene.SceneContainer;

            InitializeSceneAttributes(root);

            scene.RenderBoundingVolumes = false;

            ScientificModelElement element = new ScientificModelElement();
            MyArcBallEffect arcBallEffect = new MyArcBallEffect();
            element.AddEffect(arcBallEffect);
            element.modelTranslation = arcBallEffect.ArcBall;
            root.AddChild(element);
            scientificVisual3DControl.SetModelElement(element);
        }

        private static OpenGLAttributesEffect InitializeSceneAttributes(SceneElement parent)
        {
            //  Create a set of scene attributes.
            OpenGLAttributesEffect sceneAttributes = new OpenGLAttributesEffect()
            {
                Name = "Scene Attributes"
            };

            //  Specify the scene attributes.
            sceneAttributes.EnableAttributes.EnableDepthTest = true;
            sceneAttributes.EnableAttributes.EnableNormalize = true;
            sceneAttributes.EnableAttributes.EnableLighting = false;
            sceneAttributes.EnableAttributes.EnableTexture2D = true;
            sceneAttributes.EnableAttributes.EnableBlend = true;
            sceneAttributes.ColorBufferAttributes.BlendingSourceFactor = BlendingSourceFactor.SourceAlpha;
            sceneAttributes.ColorBufferAttributes.BlendingDestinationFactor = BlendingDestinationFactor.OneMinusSourceAlpha;
            sceneAttributes.LightingAttributes.TwoSided = true;
            sceneAttributes.LightingAttributes.AmbientLight = new GLColor(1, 1, 1, 1);
            parent.AddEffect(sceneAttributes);

            return sceneAttributes;
        }
    }
}
