using GlmNet;
using SharpGLBase.Shaders;
using SharpGL;
using SharpGL.SceneGraph.Assets;
using SharpGL.SceneGraph.Core;
using SharpGL.Shaders;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SharpGLBase.Primitives
{
    /// <summary>
    /// Draws an axis in the center of the world.
    /// </summary>
    public class Axis : IRenderable
    {
        #region fields
        private float _axisLength = 4, 
            _lineThickness = 2;
        private Material _materialX, _materialY, _materialZ;
        private List<Tuple<vec3, vec3>> _linesX, _linesY, _linesZ;
        #endregion fields

        #region properties
        /// <summary>
        /// The length of each axis.
        /// </summary>
        public float AxisLength
        {
            get { return _axisLength; }
            set { _axisLength = value; }
        }
        /// <summary>
        /// The thickness of each axis.
        /// </summary>
        public float LineThickness
        {
            get { return _lineThickness; }
            set { _lineThickness = value; }
        }
        /// <summary>
        /// The material of the the X-axis
        /// </summary>
        public Material MaterialX
        {
            get { return _materialX; }
            set { _materialX = value; }
        }
        /// <summary>
        /// The material of the the Y-axis
        /// </summary>
        public Material MaterialY
        {
            get { return _materialY; }
            set { _materialY = value; }
        }

        /// <summary>
        /// The material of the the Z-axis
        /// </summary>
        public Material MaterialZ
        {
            get { return _materialZ; }
            set { _materialZ = value; }
        }
        #endregion properties

        #region constructors
        public Axis()
        {
            _materialX = new Material();
            _materialX.Ambient = Color.Red;
            _materialX.Shininess = 1f;

            _materialY = new Material();
            _materialY.Ambient = Color.Green;
            _materialY.Shininess = 1f;

            _materialZ = new Material();
            _materialZ.Ambient = Color.Blue;
            _materialZ.Shininess = 1f;

            RecalculateShape();
        }

        public Axis(Material matX, Material matY, Material matZ)
        {
            _materialX = matX;
            _materialY = matY;
            _materialY = matZ;

            RecalculateShape();
        }
        #endregion constructors


        /// <summary>
        /// Renders the axis to the gl if it's in DesignMode.
        /// </summary>
        /// <param name="gl">The gl</param>
        /// <param name="renderMode">The rendermode</param>
        /// <param name="shader">Pass the shader so the material color(s) can be applied locally.</param>
        public void Render(OpenGL gl, RenderMode renderMode, ExtShaderProgram shader)
        {
            // Ensure that we're in design mode (we don't want axis during render)
            if (renderMode != RenderMode.Design)
                return;

            ValidateBeforeRender();

            LoadAttributes(gl);

            shader.ApplyMaterial(gl, MaterialX);
            gl.Begin(OpenGL.GL_LINES);
            PrimitivesGlobal.RenderLines(gl, _linesX);
            gl.End();

            shader.ApplyMaterial(gl, MaterialZ);
            gl.Begin(OpenGL.GL_LINES);
            PrimitivesGlobal.RenderLines(gl, _linesZ);
            gl.End();

            shader.ApplyMaterial(gl, MaterialY);
            gl.Begin(OpenGL.GL_LINES);
            PrimitivesGlobal.RenderLines(gl, _linesY);
            gl.End();

            // Undo last attribute changes
            PopAttributes(gl);
        }

        /// <summary>
        /// Renders the axis to the gl if it's in DesignMode.
        /// </summary>
        /// <param name="gl">The gl</param>
        /// <param name="renderMode">The rendermode</param>
        public virtual void Render(OpenGL gl, RenderMode renderMode)
        {
            // Ensure that we're in design mode (we don't want axis during render)
            if (renderMode != RenderMode.Design)
                return;

            ValidateBeforeRender();

            LoadAttributes(gl);
            // Draw the lines.
            // TODO: Inefficient, consumes a lot of resources.
            gl.Begin(OpenGL.GL_LINES);
            PrimitivesGlobal.RenderLines(gl, _linesX);
            PrimitivesGlobal.RenderLines(gl, _linesY);
            PrimitivesGlobal.RenderLines(gl, _linesZ);
            gl.End();

            PopAttributes(gl);
        }

        /// <summary>
        /// Calculates the lines using the current axis properties.
        /// </summary>
        public virtual void RecalculateShape()
        {
            _linesX = new List<Tuple<vec3, vec3>>();
            _linesY = new List<Tuple<vec3, vec3>>();
            _linesZ = new List<Tuple<vec3, vec3>>();

            _linesX.Add(
                new Tuple<vec3, vec3>(
                    new vec3(0, 0, 0),
                    new vec3(_axisLength,0,0))
                );

            _linesY.Add(
                new Tuple<vec3, vec3>(
                    new vec3(0, 0, 0),
                    new vec3(0, _axisLength, 0))
                );

            _linesZ.Add(
                new Tuple<vec3, vec3>(
                    new vec3(0, 0, 0),
                    new vec3(0, 0, _axisLength))
                );
        }

        /// <summary>
        /// Pushes and sets some attributes.
        /// </summary>
        /// <param name="gl">The GL.</param>
        public virtual void LoadAttributes(OpenGL gl)
        {
            //  Push all attributes, disable lighting and depth testing.
            gl.PushAttrib(OpenGL.GL_CURRENT_BIT | OpenGL.GL_ENABLE_BIT |
                OpenGL.GL_LINE_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            gl.DepthFunc(OpenGL.GL_ALWAYS);
            gl.LineWidth(_lineThickness);
        }

        /// <summary>
        /// Pops last attributes.
        /// </summary>
        /// <param name="gl">The GL.</param>
        public virtual void PopAttributes(OpenGL gl)
        {
            gl.PopAttrib();
        }

        /// <summary>
        /// Ensures that all required properties are acceptable.
        /// </summary>
        private void ValidateBeforeRender()
        {
            if (_linesX == null || _linesY == null || _linesZ == null)
            {
                throw new Exception("Axis aren't calculated.");
            }
        }
    }
}
