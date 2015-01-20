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
using SharpGL.SceneGraph.Effects;
using SharpGL.SceneGraph.Primitives;
using SharpGL.SceneGraph.Transformations;
using SharpGL.SceneGraph.Quadrics;

namespace SceneSample
{
    public partial class FormSceneSample : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormSceneSample"/> class.
        /// </summary>
        public FormSceneSample()
        {
            InitializeComponent();

            sceneControl1.MouseDown += new MouseEventHandler(FormSceneSample_MouseDown);
            sceneControl1.MouseMove += new MouseEventHandler(FormSceneSample_MouseMove);
            sceneControl1.MouseUp += new MouseEventHandler(sceneControl1_MouseUp);

            // Add some design-time primitives.
            sceneControl1.Scene.SceneContainer.AddChild(new Grid());
            sceneControl1.Scene.SceneContainer.AddChild(new Axies());

            // Create a light.
            Light light = new Light()
            {
                On = true,
                Position = new Vertex(-3, 10, 3),
                GLCode = OpenGL.GL_LIGHT0
            };

            // Add the light.
            sceneControl1.Scene.SceneContainer.AddChild(light);

            // Create the torso.
            Cube torso = new Cube()
            {
                Transformation = new LinearTransformation()
                {
                    TranslateZ = 4.0f,
                    ScaleX = 1.0f, ScaleY = 1.25f, ScaleZ = 1.25f
                },
                Name = "Torso"
            };

            torso.AddEffect(arcBallEffect);

            // Create the head.
            Cube head = new Cube()
            {
                Transformation = new LinearTransformation()
                {
                    TranslateZ = 1.5f,
                    ScaleX = 0.4f,
                    ScaleY = 0.4f,
                    ScaleZ = 0.4f
                },
                Name = "Left Arm"
            };

            // Create the arms.
            Cube rightArm = new Cube()
            {
                Transformation = new LinearTransformation()
                {
                    TranslateY = -1.3f, TranslateZ = -0.5f,
                    ScaleX = 0.2f, ScaleY = 0.2f
                },
                Name = "Right Arm"
            };

            Cube leftArm = new Cube()
            {
                Transformation = new LinearTransformation()
                {
                    TranslateX = -1.0f, TranslateY = 1.3f, TranslateZ = -0.1f,
                    RotateY = 45.0f,
                    ScaleX = 0.2f, ScaleY = 0.2f
                },
                Name = "Left Leg"
            };

            // Create the legs.
            Cube rightLeg = new Cube()
            {
                Transformation = new LinearTransformation()
                {
                    TranslateY = -0.7f, TranslateZ = -1.3f,
                    ScaleX = 0.2f, ScaleY = 0.2f, ScaleZ = 2f
                },
                Name = "Right Leg"
            };

            Cube leftLeg = new Cube()
            {
                Transformation = new LinearTransformation()
                {
                    TranslateY = 0.7f, TranslateZ = -1.3f,
                    ScaleX = 0.2f, ScaleY = 0.2f, ScaleZ = 2f
                },
                Name = "Left Leg"
            };

            //Sphere head = new Sphere()
            //{
            //    Transformation = new LinearTransformation()
            //    {
            //        TranslateZ = 0.5f
            //    },
            //    Name = "Head"
            //};

            torso.AddChild(head);
            torso.AddChild(rightArm);
            torso.AddChild(leftArm);
            torso.AddChild(rightLeg);
            torso.AddChild(leftLeg);

            //  Add torso to scene.
            sceneControl1.Scene.SceneContainer.AddChild(torso);

            //  Add the root element to the scene tree view control.
            AddElementToTree(sceneControl1.Scene.SceneContainer, treeView1.Nodes);
        }

        void sceneControl1_MouseUp(object sender, MouseEventArgs e)
        {
            arcBallEffect.ArcBall.MouseUp(e.X, e.Y);
        }

        void FormSceneSample_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                arcBallEffect.ArcBall.MouseMove(e.X, e.Y);
            }
        }

        void FormSceneSample_MouseDown(object sender, MouseEventArgs e)
        {
            arcBallEffect.ArcBall.SetBounds(sceneControl1.Width, sceneControl1.Height);
            arcBallEffect.ArcBall.MouseDown(e.X, e.Y);
        }
        private ArcBallEffect arcBallEffect = new ArcBallEffect();
        /// <summary>
        /// Adds the element to tree.
        /// </summary>
        /// <param name="sceneElement">The scene element.</param>
        /// <param name="nodes">The nodes.</param>
        private void AddElementToTree(SceneElement sceneElement, TreeNodeCollection nodes)
        {
            //  Add the element.
            TreeNode newNode = new TreeNode() 
            { 
                Text = sceneElement.Name, 
                Tag = sceneElement 
            };
            nodes.Add(newNode);

            //  Add each child.
            foreach (var element in sceneElement.Children)
                AddElementToTree(element, newNode.Nodes);
        }

        /// <summary>
        /// Handles the AfterSelect event of the treeView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.TreeViewEventArgs"/> instance containing the event data.</param>
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SelectedSceneElement = e.Node.Tag as SceneElement;
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
    }
}