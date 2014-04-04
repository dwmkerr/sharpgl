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
    /// Draws a grid in the center of the world.
    /// </summary>
    public class SquareGrid : IRenderable, IHasMaterial
    {
        #region fields
        private int _directionLineCount;
        private float _stepSize, _lineThickness = 0.001f;
        private Material _material;
        private List<Tuple<vec3, vec3>> _lines = null;
        #endregion fields

        #region properties
        /// <summary>
        /// Gets or sets the amount of lines in each direction.
        /// </summary>
        public int DirectionLineCount
        {
            get { return _directionLineCount; }
            set { _directionLineCount = value; }
        }

        /// <summary>
        /// Distance between 2 lines.
        /// </summary>
        public float StepSize
        {
            get { return _stepSize; }
            set { _stepSize = value; }
        }
        public float LineThickness
        {
            get { return _lineThickness; }
            set { _lineThickness = value; }
        }

        /// <summary>
        /// The applied material for the grid.
        /// </summary>
        public Material Material
        {
            get { return _material; }
            set { _material = value; }
        }
        #endregion properties

        public SquareGrid()
            :this(10, 1f)
        { }

        public SquareGrid(int directionLineCount, float stepSize)
        {
            _directionLineCount = directionLineCount;
            _stepSize = stepSize;

            _material = new Material();
            _material.Ambient = Color.FromArgb(255, 146, 134, 188); // Purple.
            _material.Shininess = 1f;

            RecalculateShape();
        }

        /// <summary>
        /// Renders the grid to the gl if it's in DesignMode.
        /// </summary>
        /// <param name="gl">The gl</param>
        /// <param name="renderMode">The rendermode</param>
        /// <param name="shader">Pass the shader so the material color(s) can be applied locally.</param>
        public void Render(OpenGL gl, RenderMode renderMode, ExtShaderProgram shader)
        {
            if (renderMode != RenderMode.Design)
                return;

            shader.ApplyMaterial(gl, _material);

            Render(gl, renderMode);
        }

        /// <summary>
        /// Renders the grid to the gl if it's in DesignMode.
        /// </summary>
        /// <param name="gl">The gl</param>
        /// <param name="renderMode">The rendermode</param>
        public void Render(OpenGL gl, RenderMode renderMode)
        {
            // Ensure that we're in design mode (we don't want axis during render)
            if (renderMode != RenderMode.Design)
                return;


            ValidateBeforeRender();

            // Draw the lines.
            // TODO: Inefficient, consumes a lot of resources.
            gl.LineWidth(_lineThickness);
            gl.Begin(OpenGL.GL_LINES);
            PrimitivesGlobal.RenderLines(gl, _lines);
            gl.End();
        }

        /// <summary>
        /// Calculates the lines using the current grid properties.
        /// </summary>
        public void RecalculateShape()
        {
            _lines = new List<Tuple<vec3, vec3>>();

            float min = -StepSize * DirectionLineCount;
            float max = StepSize * DirectionLineCount;
            for (float x = min; x <= max; x += StepSize)
            {
                for (float z = min; z <= max; z += StepSize)
                {
                    _lines.Add(
                        new Tuple<vec3, vec3>(
                            new vec3(x, 0.0f, min), 
                            new vec3(x, 0.0f, max))
                        );

                    _lines.Add(
                        new Tuple<vec3, vec3>(
                            new vec3(min, 0.0f, z),
                            new vec3(max, 0.0f, z))
                        );
                }
            }
        }

        /// <summary>
        /// Ensures that all required properties are acceptable.
        /// </summary>
        private void ValidateBeforeRender()
        {
            if (_lines == null)
            {
                throw new Exception("Grid lines aren't calculated.");
            }
        }
    }
}
