using GlmNet;
using SharpGL;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.JOG;
using SharpGL.Shaders;
using SharpGL.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGLParticlesSample
{
    public class GLController
    {
        #region fields
        NormalMaterialParticleProgram _nmpProgram;
        mat4 _projectionMatrix = mat4.identity(),
            _modelviewMatrix = mat4.identity();
        mat3 _normalMatrix = mat3.identity();


        ModelviewProjectionBuilder _mvpBuilder = new ModelviewProjectionBuilder();
        #endregion fields

        #region properties
        public NormalMaterialParticleProgram NmpProgram
        {
            get { return _nmpProgram; }
            set { _nmpProgram = value; }
        }
        /// <summary>
        /// The matrix responsable for deforming the projection.
        /// </summary>
        public mat4 ProjectionMatrix
        {
            get { return _projectionMatrix; }
            set
            {
                _projectionMatrix = value;
                NmpProgram.Projection = value;
            }
        }
        /// <summary>
        /// The projection matrix, responsable for transforming objects in the world.
        /// </summary>
        public mat4 ModelviewMatrix
        {
            get { return _modelviewMatrix; }
            set
            {
                _modelviewMatrix = value;
                NmpProgram.Modelview = value;
            }
        }

        /// <summary>
        /// A normal matrix, influences lighting reflection etc.
        /// </summary>
        public mat3 NormalMatrix
        {
            get { return _normalMatrix; }
            set
            {
                _normalMatrix = value;
                NmpProgram.NormalMatrix = value;
            }
        }

        public OpenGL GL { get { return SceneControl.Gl; } }
        public OpenGLControlJOG SceneControl { get; set; }

        public ModelviewProjectionBuilder MvpBuilder
        {
            get { return _mvpBuilder; }
            set { _mvpBuilder = value; }
        }
        #endregion properties
        
        #region events
        #endregion events

        #region constructors
        #endregion constructors
        public void Init(object sender, OpenGLEventArgs args)
        {
            SceneControl = sender as OpenGLControlJOG;

            // Set up the view.
            MvpBuilder.FovRadians = (float)Math.PI / 2f; // Set FOV to 90°
            MvpBuilder.Far = 100f;
            MvpBuilder.Near = 0.01f;
            MvpBuilder.Width = (int)SceneControl.ActualWidth;
            MvpBuilder.Height = (int)SceneControl.ActualHeight;

            MvpBuilder.TranslationZ = -10;

            MvpBuilder.BuildPerspectiveProjection();
            MvpBuilder.BuildTurntableModelview();


            // Create a shader program.
            NmpProgram = new NormalMaterialParticleProgram(GL);
            ProjectionMatrix = MvpBuilder.ProjectionMatrix;
            ModelviewMatrix = MvpBuilder.ModelviewMatrix;
            NormalMatrix = mat3.identity();
            NmpProgram.LightPosition = new vec3(5, 10, 15);


            AddData(GL);

            GL.Enable(OpenGL.GL_DEPTH_TEST);

            GL.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
        }
        public void Draw(object sender, OpenGLEventArgs args)
        {
            GL.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);


            // Add gradient background.
            SetVerticalGradientBackground(GL, new ColorF(255, 146, 134, 188), new ColorF(1f, 0, 1, 0));

            NmpProgram.BindAll(GL);
        }
        public void Resized(object sender, OpenGLEventArgs args)
        {
            var control = sender as OpenGLControlJOG;

            MvpBuilder.Width = (int)control.ActualWidth;
            MvpBuilder.Height = (int)control.ActualHeight;

            MvpBuilder.BuildPerspectiveProjection();

            ProjectionMatrix = MvpBuilder.ProjectionMatrix;
        }

        private void AddData(OpenGL gl)
        {
            NMPBufferGroup group = new NMPBufferGroup(gl);
            
            var verts = MyTrefoilKnot.Vertices.SelectMany(x=>x.to_array()).ToArray();
            var normals = MyTrefoilKnot.Normals.SelectMany(x => x.to_array()).ToArray();

            group.BufferData(gl, MyTrefoilKnot.Indices, verts, normals, new mat4[] { mat4.identity() },
                new Material[] { new Material(new ColorF(255, 150, 50, 50), new ColorF(255,10,100,10), new ColorF(255, 225, 225, 225), null, 100f)});
            group.PrepareVAO(gl, NmpProgram);

            NmpProgram.AddBufferGroup(group);
            NMPBufferGroup groupCube = new NMPBufferGroup(gl);

            var vertsCube = FlatShadedCube.Vertices.SelectMany(x => x.to_array()).ToArray();
            var normalsCube = FlatShadedCube.Normals.SelectMany(x => x.to_array()).ToArray();


            groupCube.BufferData(gl, FlatShadedCube.Indices, vertsCube, normalsCube, 
                new mat4[] 
                { 
                    mat4.identity(), 
                    glm.translate(mat4.identity(), new vec3(1,2,3)) 
                },
                new Material[] 
                { 
                    new Material(new ColorF(1f, 0.3f, 1, 0), new ColorF(1f, 1f, 0.5f, 0), new ColorF(1f, 1, 0, 1), null, 100f),
                    new Material(new ColorF(1f, 0, 1, 0.3f), new ColorF(1f, 1f, 0.5f, 1), new ColorF(1f, 1, 0.3f, 0.3f), null, 100f) 
                });
            groupCube.PrepareVAO(gl, NmpProgram);

            NmpProgram.AddBufferGroup(groupCube);
        }

        public void RefreshModelview()
        {
            MvpBuilder.BuildTurntableModelview();
            ModelviewMatrix = MvpBuilder.ModelviewMatrix;
        }
        public void RefreshProjection()
        {
            MvpBuilder.BuildPerspectiveProjection();
            ProjectionMatrix = MvpBuilder.ProjectionMatrix;
        }

        /// <summary>
        /// Sets the background color, using a gradient existing from 2 colors
        /// </summary>
        /// <param name="gl"></param>
        private static void SetVerticalGradientBackground(OpenGL gl, ColorF colorTop, ColorF colorBot)
        {
            float topRed = colorTop.R;// / 255.0f;
            float topGreen = colorTop.G;// / 255.0f;
            float topBlue = colorTop.B;// / 255.0f;
            float botRed = colorBot.R;// / 255.0f;
            float botGreen = colorBot.G;// / 255.0f;
            float botBlue = colorBot.B;// / 255.0f;

            gl.Begin(OpenGL.GL_QUADS);

            //bottom color
            gl.Color(botRed, botGreen, botBlue);
            gl.Vertex(-1.0, -1.0);
            gl.Vertex(1.0, -1.0);

            //top color
            gl.Color(topRed, topGreen, topBlue);
            gl.Vertex(1.0, 1.0);
            gl.Vertex(-1.0, 1.0);

            gl.End();
        }

    }
}
