using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;

using SharpGL;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph.Collections;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Lighting;
using SharpGL.SceneGraph.Primitives;

namespace HitTestSample
{
    public partial class FormHitTestSample : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormHitTestSample"/> class.
        /// </summary>
        public FormHitTestSample()
        {
            InitializeComponent();

            //  Create a sphere.
            SharpGL.SceneGraph.Quadrics.Sphere sphere = new SharpGL.SceneGraph.Quadrics.Sphere();
            sphere.Transformation.TranslateX = 2;
            sphere.Transformation.TranslateY = 2;

            //  Create a cone.
            SharpGL.SceneGraph.Quadrics.Cylinder cone = new SharpGL.SceneGraph.Quadrics.Cylinder() { Name = "Cone" };
            cone.BaseRadius = 1.5;
            cone.TopRadius = 0;
            cone.Height = 2;
            cone.Transformation.TranslateX = -2;
            cone.Transformation.TranslateY = -2;

            //  Create a cylinder.
            SharpGL.SceneGraph.Quadrics.Cylinder cylinder = new SharpGL.SceneGraph.Quadrics.Cylinder() { Name = "Cylinder" };
            cylinder.BaseRadius = 1.5;
            cylinder.TopRadius = 1.5;
            cylinder.Height = 2;
            cylinder.Transformation.TranslateX = -2;
            cylinder.Transformation.TranslateY = 2;

            //  Create a cube.
            Cube cube = new Cube();
            cube.Transformation.TranslateX = 2;
            cube.Transformation.TranslateY = -2;
            cube.Transformation.RotateZ = 45f;

            //  Add them.
            sceneControl1.Scene.SceneContainer.AddChild(sphere);
            sceneControl1.Scene.SceneContainer.AddChild(cube);
            sceneControl1.Scene.SceneContainer.AddChild(cone);
            sceneControl1.Scene.SceneContainer.AddChild(cylinder);
        }

        /// <summary>
        /// Called when [selected scene element changed].
        /// </summary>
        private void OnSelectedSceneElementChanged()
        {
            propertyGrid1.SelectedObject = SelectedSceneElement;
        }

        /// <summary>
        /// The selected scene element.
        /// </summary>
        private SceneElement selectedSceneElement = null;

        /// <summary>
        /// Gets or sets the selected scene element.
        /// </summary>
        /// <value>
        /// The selected scene element.
        /// </value>
        public SceneElement SelectedSceneElement
        {
            get { return selectedSceneElement; }
            set
            {
                selectedSceneElement = value;
                OnSelectedSceneElementChanged();
            }
        }

        private void toolStripButtonShowBoundingVolumes_Click(object sender, EventArgs e)
        {
            sceneControl1.Scene.RenderBoundingVolumes = !sceneControl1.Scene.RenderBoundingVolumes;
            toolStripButtonShowBoundingVolumes.Checked = !toolStripButtonShowBoundingVolumes.Checked;
        }

        private void sceneControl1_MouseClick(object sender, MouseEventArgs e)
        {
            //  Do a hit test.
            SelectedSceneElement = null;
            listBox1.Items.Clear();
            var itemsHit = sceneControl1.Scene.DoHitTest(e.X, e.Y);
            foreach (var item in itemsHit)
                listBox1.Items.Add(item);
            if (listBox1.Items.Count >0)
            {
                listBox1.SetSelected(0, true);
               // listBox1_SelectedIndexChanged(this, null);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedSceneElement = listBox1.SelectedItem as SceneElement;
        }
    }
}