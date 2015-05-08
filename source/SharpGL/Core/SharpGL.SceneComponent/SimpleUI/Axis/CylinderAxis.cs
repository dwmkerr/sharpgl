using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Assets;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Lighting;
using SharpGL.SceneGraph.Quadrics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// Cylinder-made axis.
    /// </summary>
    public class CylinderAxis : SceneElement, IRenderable
    {
        private bool initialized = false;

        public CylinderAxis()
        {
            this.Name = "Cylinder Axis";
        }

        private void InitAxis(SceneElement parent)
        {
            Light light1 = new Light()
            {
                Name = "Cylinder Axis Root: Light",
                On = true,
                Position = new Vertex(-9, -9, 11),
                GLCode = OpenGL.GL_LIGHT0
            };
            parent.AddChild(light1);

            DoInitAxis(light1);
        }

        const float factor = 2;

        private static void DoInitAxis(SceneElement parent)
        {
            // X轴
            Material red = new Material();
            red.Emission = Color.Red;
            red.Diffuse = Color.Red;

            Cylinder x1 = new Cylinder() { Name = "X1" };
            x1.BaseRadius = 0.1 * factor;
            x1.TopRadius = 0.1 * factor;
            x1.Height = 1.5 * factor;
            x1.Transformation.RotateY = 90f;
            x1.Material = red;
            parent.AddChild(x1);

            Cylinder x2 = new Cylinder() { Name = "X2" };
            x2.BaseRadius = 0.2 * factor;
            x2.TopRadius = 0 * factor;
            x2.Height = 0.4 * factor;
            x2.Transformation.TranslateX = 1.5f * factor;
            x2.Transformation.RotateY = 90f;
            x2.Material = red;
            parent.AddChild(x2);

            // Y轴
            Material green = new Material();
            green.Emission = Color.Green;
            green.Diffuse = Color.Green;

            Cylinder y1 = new Cylinder() { Name = "Y1" };
            y1.BaseRadius = 0.1 * factor;
            y1.TopRadius = 0.1 * factor;
            y1.Height = 1.5 * factor;
            y1.Transformation.RotateX = -90f;
            y1.Material = green;
            parent.AddChild(y1);

            Cylinder y2 = new Cylinder() { Name = "Y2" };
            y2.BaseRadius = 0.2 * factor;
            y2.TopRadius = 0 * factor;
            y2.Height = 0.4 * factor;
            y2.Transformation.TranslateY = 1.5f * factor;
            y2.Transformation.RotateX = -90f;
            y2.Material = green;
            parent.AddChild(y2);

            // Z轴
            Material blue = new Material();
            blue.Emission = Color.Blue;
            blue.Diffuse = Color.Blue;

            Cylinder z1 = new Cylinder() { Name = "Z1" };
            z1.BaseRadius = 0.1 * factor;
            z1.TopRadius = 0.1 * factor;
            z1.Height = 1.5 * factor;
            z1.Material = blue;
            parent.AddChild(z1);

            Cylinder z2 = new Cylinder() { Name = "Z2" };
            z2.BaseRadius = 0.2 * factor;
            z2.TopRadius = 0 * factor;
            z2.Height = 0.4 * factor;
            z2.Transformation.TranslateZ = 1.5f * factor;
            z2.Material = blue;
            parent.AddChild(z2);
        }

        #region IRenderable 成员

        public void Render(OpenGL gl, RenderMode renderMode)
        {
            if (!initialized)
            {
                // Cylinder needs an opengl context(OpenGL gl;) to generate inner cylinder. So this is the best place to initialize.
                InitAxis(this);
                initialized = true;
            }
        }

        #endregion
    }
}
