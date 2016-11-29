using GlmNet;
using SharpGL;
using SharpGL.Enumerations;
using SharpGL.SceneGraph.Assets;
using SharpGL.SceneGraph.Core;
using SharpGLBase.Primitives;
using SharpGLBase.Scene;
using SharpGLBase.Shaders;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using SharpGLBase.Extensions;

namespace SharpGLTest
{
    public class MyScene : OpenGLScene
    {
        #region fields
        AxisVBO _axis;
        SquareGridVBO _grid;
        vec3 _lightPosition = new vec3(10, 10, 10);
        Dictionary<Material, List<Model3DBase>> _materialForObjects = new Dictionary<Material, List<Model3DBase>>();
        Color _verticalGradientBackgroundColorTop = Color.FromArgb(146, 134, 188); // Light Purple
        Color _verticalGradientBackgroundColorBottom = Color.FromArgb(0, 0, 0); // Black
        bool _drawEdgesOnly = false;
        #endregion fields

        #region properties
        /// <summary>
        /// Top color of the gradient background.
        /// </summary>
        public Color VerticalGradientBackgroundColorTop
        {
            get { return _verticalGradientBackgroundColorTop; }
            set { _verticalGradientBackgroundColorTop = value; }
        }
        /// <summary>
        /// Bottom color of the gradient background.
        /// </summary>
        public Color VerticalGradientBackgroundColorBottom
        {
            get { return _verticalGradientBackgroundColorBottom; }
            set { _verticalGradientBackgroundColorBottom = value; }
        }
        /// <summary>
        /// Objects grouped by material. Add an object here and it will be presented to the scene.
        /// </summary>
        public Dictionary<Material, List<Model3DBase>> MaterialForObjects
        {
            get { return _materialForObjects; }
            set { _materialForObjects = value; }
        }
        /// <summary>
        /// Position of the lightpoint.
        /// </summary>
        public vec3 LightPosition
        {
            get { return _lightPosition; }
            set { _lightPosition = value; }
        }
        /// <summary>
        /// If true => models will be rendered in Polygon LineMode.
        /// </summary>
        public bool DrawEdgesOnly
        {
            get { return _drawEdgesOnly; }
            set { _drawEdgesOnly = value; }
        }
        #endregion properties


        public MyScene()
        {
        }

        public override void OpenGLInitialized(SharpGL.OpenGL gl)
        {
            base.OpenGLInitialized(gl);

            // Initialize the grid and axis.
            _axis = new AxisVBO(gl);
            _grid = new SquareGridVBO(gl);

            // Set the shader.
            CurrentShader = Shaders.LoadToonShader(gl);
        }

        public override void Draw(SharpGL.OpenGL gl)
        {
            // Disable dithering ( = smooth color transition). Should have a slight performance impact. 
            gl.Disable(OpenGL.GL_DITHER);

            // Clear the viewport.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            // Add gradient background.
            SetVerticalGradientBackground(gl, VerticalGradientBackgroundColorTop, VerticalGradientBackgroundColorBottom);

            // Use the shader program.
            CurrentShader.UseProgram(gl, () =>
            {
                if (DrawEdgesOnly)
                {
                    // Push the polygon attributes and set line mode.
                    gl.PushAttrib(OpenGL.GL_POLYGON_BIT);
                    gl.PolygonMode(FaceMode.FrontAndBack, PolygonMode.Lines);
                }

                CurrentShader.ApplyMVPNMatrices(gl, ModelView, Projection, Normal);

                // Set the light.
                CurrentShader.SetLight(gl, _lightPosition, false);

                // Render all objects.
                foreach (var key in MaterialForObjects.Keys)
                {
                    var objects = MaterialForObjects[key];
                    RenderObjectsFromMaterial(gl, key, CurrentShader, objects);
                }

                // Add axis.
                _axis.Render(gl, RenderMode.Design, CurrentShader);

                // Add grid.
                _grid.Render(gl, RenderMode.Design, CurrentShader);


                // Undo draw edges only.
                if (DrawEdgesOnly)
                {
                    // Pop the attributes, restoring all polygon state.
                    gl.PopAttrib();
                }
            });
        }

        public override void ViewResized(SharpGL.OpenGL gl, double actualWidth, double actualHeight)
        {
            base.ViewResized(gl, actualWidth, actualHeight);
        }

        /// <summary>
        /// Sets the background color, using a gradient existing from 2 colors
        /// </summary>
        /// <param name="gl"></param>
        private static void SetVerticalGradientBackground(OpenGL gl, Color colorTop, Color colorBot)
        {
            float topRed = colorTop.R / 255.0f;
            float topGreen = colorTop.G / 255.0f;
            float topBlue = colorTop.B / 255.0f;
            float botRed = colorBot.R / 255.0f;
            float botGreen = colorBot.G / 255.0f;
            float botBlue = colorBot.B / 255.0f;

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

        /// <summary>
        /// Renders all objects in MaterialForObjects[materialKey] to the screen, using the given parameters. 
        /// </summary>
        /// <param name="gl">The OpenGL</param>
        /// <param name="material">The material that will be used for the objects.</param>
        /// <param name="shader">The shader that will be used for this material.</param>
        /// <param name="objects">The objects to be drawn.</param>
        /// <param name="drawEdgesOnly">True: draw only edges, false: draw faces.</param>
        public void RenderObjectsFromMaterial(OpenGL gl, Material material, ExtShaderProgram shader, IEnumerable<Model3DBase> objects)
        {
            if (objects == null)
                return;

            //  Set the variables for the shader program.
            shader.ApplyMaterial(gl, material);

            // Bind the vertex buffer arrays.
            foreach (var item in objects)
            {
                // If the object has it's own material, then use this. Else use the material-parameter.
                if (item.Material != null)
                {
                    shader.ApplyMaterial(gl, item.Material);
                    item.Render(gl, RenderMode.Render);
                    shader.ApplyMaterial(gl, material);
                }
                else
                {
                    item.Render(gl, RenderMode.Render);
                }
            }
        }
    }
}
