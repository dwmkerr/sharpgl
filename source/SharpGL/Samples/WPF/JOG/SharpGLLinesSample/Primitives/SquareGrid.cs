using GlmNet;
using SharpGL;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.JOG;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SharpGLLinesSample
{
    public class SquareGrid
    {
        #region fields
        private int _directionLineCount;
        private float _stepSize, _lineThickness = 1f;
        private List<vec3> _lines = new List<vec3>();
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

        public List<vec3> Lines
        {
            get { return _lines; }
            set { _lines = value; }
        }
        #endregion properties

        public SquareGrid()
            :this(10, 1f)
        { }

        public SquareGrid(int directionLineCount, float stepSize)
        {
            _directionLineCount = directionLineCount;
            _stepSize = stepSize;

            RecalculateShape();
        }


        /// <summary>
        /// Calculates the lines using the current grid properties.
        /// </summary>
        public void RecalculateShape()
        {
            _lines = new List<vec3>();
            var verts = new List<vec3>();

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

                }
            }

            Lines = verts;
        }
        public void RecalculateShape2()
        {
            List<vec3> verts = new List<vec3>();

            float min = -StepSize * DirectionLineCount;
            float max = StepSize * DirectionLineCount;

            // Vertical
            bool swap = false;
            for (float x = min; x <= max; x += StepSize)
            {
                if (swap)
                {
                    verts.Add(new vec3(x, 0, min));
                    verts.Add(new vec3(x, 0, max));
                }
                else
                {
                    verts.Add(new vec3(x, 0, max));
                    verts.Add(new vec3(x, 0, min));
                }

                swap = !swap;
            }

            // Horizontal

            Lines = verts;

        }
    }
}
