using GlmNet;
using SharpGL;
using SharpGL.SceneGraph.Assets;
using SharpGL.SceneGraph.Core;
using SharpGLBase.Shaders;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SharpGLBase.Primitives
{
    public class SquareGridVBO
    {
        #region fields
        private int _directionLineCount;
        private float _stepSize, _lineThickness = 1f;
        private List<Tuple<vec3, vec3>> _lines = null;
        private Lines _gridLines = null;
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
            get { return _gridLines.Material; }
            set { _gridLines.Material = value; }
        }
        #endregion properties

        public SquareGridVBO(OpenGL gl)
            :this(gl, 10, 1f)
        { }

        public SquareGridVBO(OpenGL gl, int directionLineCount, float stepSize)
        {
            _directionLineCount = directionLineCount;
            _stepSize = stepSize;

            RecalculateShape();

            _gridLines = new Lines(gl, _lines);

            Material = new Material();
            Material.Diffuse = Color.FromArgb(255, 146, 134, 188); // Purple.
            Material.Shininess = 1f;
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

            shader.UseProgram(gl, () =>
            {
                shader.ApplyMaterial(gl, Material);

                Render(gl, renderMode);
            });
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

            _gridLines.Render(gl, renderMode, null);
        }

        /// <summary>
        /// Calculates the lines using the current grid properties.
        /// </summary>
        public void RecalculateShape()
        {
            _lines = new List<Tuple<vec3, vec3>>();
            List<vec3> verts = new List<vec3>();

            float min = -StepSize * DirectionLineCount;
            float max = StepSize * DirectionLineCount;
            for (float x = min; x <= max; x += StepSize)
            {
                for (float z = min; z <= max; z += StepSize)
                {
                    vec3 v1 = new vec3(x, 0.0f, min);
                    vec3 v2 = new vec3(x, 0.0f, max);
                    vec3 v3 = new vec3(min, 0.0f, z);
                    vec3 v4 = new vec3(max, 0.0f, z);

                    verts.AddRange(new vec3[] { v1, v2, v3, v4 });
                    

                    _lines.Add(
                        new Tuple<vec3, vec3>(v1, v2));

                    _lines.Add(
                        new Tuple<vec3, vec3>(v3,v4));
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
