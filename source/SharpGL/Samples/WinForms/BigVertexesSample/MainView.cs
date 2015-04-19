using SharpGL;
using SharpGL.Enumerations;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Effects;
using SharpGL.SceneGraph.Lighting;
using SharpGL.SceneGraph.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestVAO.Model;
using TestVAO.Visual;

namespace TestVAO
{
    public partial class MainView : Form
    {

        private ArcBallEffect arcBallEffect = new ArcBallEffect();

        public MainView()
        {
            InitializeComponent();
            
        }

        private int ToInt(TextBox tb)
        {
          int value= System.Convert.ToInt32(tb.Text);
          return value;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LookAtCamera camera = this.sceneControl1.Scene.CurrentCamera as LookAtCamera;
            
 
            this.sceneControl1.MouseWheel+=sceneControl1_MouseWheel;
            this.sceneControl1.MouseDown+=sceneControl1_MouseDown;
            this.sceneControl1.MouseMove+=sceneControl1_MouseMove;
            this.sceneControl1.MouseUp +=sceneControl1_MouseUp;
        }

        private void sceneControl1_MouseUp(object sender, MouseEventArgs e)
        {
            arcBallEffect.ArcBall.MouseUp(e.X, e.Y);
        }

        private void sceneControl1_MouseMove(object sender, MouseEventArgs e)
        {
            arcBallEffect.ArcBall.SetBounds(sceneControl1.Width, sceneControl1.Height);
            arcBallEffect.ArcBall.MouseMove(e.X, e.Y);
            this.sceneControl1.Invalidate();
        }

        private void sceneControl1_MouseDown(object sender, MouseEventArgs e)
        {
            arcBallEffect.ArcBall.SetBounds(sceneControl1.Width, sceneControl1.Height);
            arcBallEffect.ArcBall.MouseDown(e.X, e.Y);
        }

        private void sceneControl1_MouseWheel(object sender, MouseEventArgs e)
        {
            
        }



        private float ToFloat(TextBox tb)
        {
            float value = System.Convert.ToSingle(tb.Text);
            return value;
        }

        private void output(ColorVertexes particles)
        {
            System.Console.WriteLine(String.Format("Particle Count:{0}", particles.Size));
            long size = particles.Size;
            for (long i = 0; i < size; i++)
            {
                unsafe
                {
                    Point3D p = particles.Centers[i];
                    System.Console.WriteLine(String.Format("P({0},{1},{2})",p.x,p.y,p.z));
                }

            }

            System.Console.WriteLine(String.Format("Particle Color:{0}", particles.Size));
            for (long i = 0; i < size; i++)
            {
                unsafe
                {
                    Model.Color color = particles.Colors[i];
                    System.Console.WriteLine(String.Format("rgb({0},{1},{2})", color.red, color.green, color.blue));
                }
            }
        }


        private void InitializeModelScene(Scene scene, Rect3D rect3D, OpenGLControl sceneControl,ArcBallEffect arcBallEffect)
        {

            float centerX = rect3D.X + rect3D.Size.x / 2.0f;
            float centerY = rect3D.Y + rect3D.Size.y / 2.0f;
            float centerZ = rect3D.Z + rect3D.Size.z / 2.0f;


            //scene.CreateInContext(scenContor);

            Vertex center = new Vertex(centerX, centerY,centerZ);
            Vertex position = center + new Vertex(0.0f, 0.0f, 1.0f) * (rect3D.Size.z*2);
          
            Vertex PositionNear = center + new Vertex(0.0f, 0.0f, 1.0f) * (rect3D.Size.z*0.52f);
            //arcBallEffect.


            var lookAtCamera = new LookAtCamera()
            {
                Position = position,
                Target =  center,
                UpVector = new Vertex(0f, 1f, 0f),
                FieldOfView = 60,
                AspectRatio = 1.0f,
                Near = (PositionNear - center).Z,
                Far = float.MaxValue
            };
           

            scene.CurrentCamera = lookAtCamera;



            Vertex lightPosition = center;
            Light light1 = new Light()
            {
                Name = "Light 1",
                On = true,
                Position = lightPosition,
                GLCode = OpenGL.GL_LIGHT0
            };

            /*
            Light light2 = new Light()
            {
                Name = "Light 2",
                On = true,
                Position = center + new Vertex(1.0f,0.0f,0.0f)* rect3D.Size.x,
                GLCode = OpenGL.GL_LIGHT1
            };

            Light light3 = new Light()
            {
                Name = "Light 3",
                On = true,
                Position = center + new Vertex(0.0f,1.0f,0.0f)*rect3D.Size.y,
                GLCode = OpenGL.GL_LIGHT2
            };
            */

            var folder = new Folder() { Name = "Lights" };
            folder.AddChild(light1);
            //folder.AddChild(light2);
            //folder.AddChild(light3);
            scene.SceneContainer.AddChild(folder);

            //  Create a set of scene attributes.
            OpenGLAttributesEffect sceneAttributes = new OpenGLAttributesEffect()
            {
                Name = "Scene Attributes"
            };

            //  Specify the scene attributes.
            sceneAttributes.EnableAttributes.EnableDepthTest = true;
            sceneAttributes.EnableAttributes.EnableNormalize = true;
            sceneAttributes.EnableAttributes.EnableLighting = true;
            sceneAttributes.EnableAttributes.EnableTexture2D = true;
            sceneAttributes.EnableAttributes.EnableBlend = true;
            sceneAttributes.ColorBufferAttributes.BlendingSourceFactor = BlendingSourceFactor.SourceAlpha;
            sceneAttributes.ColorBufferAttributes.BlendingDestinationFactor = BlendingDestinationFactor.OneMinusSourceAlpha;
            sceneAttributes.LightingAttributes.TwoSided = true;
            scene.SceneContainer.AddEffect(sceneAttributes);

            sceneControl.OpenGL.SetDimensions(sceneControl.Width, sceneControl.Height);
            scene.Resize(sceneControl.Width, sceneControl.Height);
        }

        private void Create3DObject(object sender, EventArgs e)
        {
            try
            {
                int nx = this.ToInt(this.tbNX);
                int ny = this.ToInt(this.tbNY);
                int nz = this.ToInt(this.tbNZ);
                float radius = this.ToFloat(this.tbRadius);
                float minValue = this.ToFloat(this.tbRangeMin);
                float maxValue = this.ToFloat(this.tbRangeMax);
                if (minValue >= maxValue)
                    throw new ArgumentException("min value equal or equal to maxValue");

                
                //生成需要画的模型
                ColorVertexes colorVertexes  = ColorVertexesFactory.Create(nx, ny, nz, radius, minValue, maxValue);
               

                ColorVertexesElement visualElement = new ColorVertexesElement(colorVertexes);
               

                //output(particles);
                Rect3D rect3D = colorVertexes.Bounds;
                Scene scene = new Scene();
                this.arcBallEffect = new ArcBallEffect();
                //initialize Scene3D
                InitializeModelScene(scene, colorVertexes.Bounds, this.sceneControl1,this.arcBallEffect);
                this.sceneControl1.Scene = scene;
                scene.SceneContainer.AddChild(visualElement);
               
             
                visualElement.AddEffect(this.arcBallEffect);
                
                

                this.sceneControl1.Invalidate();
               
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
    }
}
