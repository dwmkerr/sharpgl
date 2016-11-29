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
    public class AxisVBO
    {
        
        #region fields
        private float _axisLength = 4, 
            _lineThickness = 2;
        private Lines _lineX, _lineY, _lineZ;
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
            get { return _lineX.Material; }
            set { _lineX.Material = value; }
        }
        /// <summary>
        /// The material of the the Y-axis
        /// </summary>
        public Material MaterialY
        {
            get { return _lineY.Material; }
            set { _lineY.Material = value; }
        }

        /// <summary>
        /// The material of the the Z-axis
        /// </summary>
        public Material MaterialZ
        {
            get { return _lineZ.Material; }
            set { _lineZ.Material = value; }
        }
        #endregion properties

        #region constructors
        public AxisVBO(OpenGL gl)
        {
            RecalculateShape(gl);

            _lineX.Material = new Material();
            _lineX.Material.Diffuse = Color.Red;

            _lineY.Material = new Material();
            _lineY.Material.Diffuse = Color.Green;
            _lineY.Material.Shininess = 0.1f;

            _lineZ.Material = new Material();
            _lineZ.Material.Diffuse = Color.Blue;
            _lineZ.Material.Shininess = 0.1f;

            var lineWidth = 5f;

            _lineX.LineWidth = lineWidth;
            _lineY.LineWidth = lineWidth;
            _lineZ.LineWidth = lineWidth;
        }

        public AxisVBO(OpenGL gl, Material matX, Material matY, Material matZ, float lineWidth)
        {
            RecalculateShape(gl);

            _lineX.Material = matX;
            _lineY.Material = matY;
            _lineZ.Material = matZ;

            _lineX.LineWidth = lineWidth;
            _lineY.LineWidth = lineWidth;
            _lineZ.LineWidth = lineWidth;
        }
        #endregion constructors


        public void Render(OpenGL gl, RenderMode renderMode, ExtShaderProgram shader)
        {
            // Ensure that we're in design mode (we don't want axis during render)
            if (renderMode != RenderMode.Design)
                return;

            ValidateBeforeRender();

            //gl.DepthFunc(OpenGL.GL_ALWAYS);

            shader.UseProgram(gl, () =>
            {
                shader.ApplyMaterial(gl, MaterialX);
                _lineX.Render(gl, renderMode, shader);
                
                shader.ApplyMaterial(gl, MaterialY);
                _lineY.Render(gl, renderMode, shader);

                shader.ApplyMaterial(gl, MaterialZ);
                _lineZ.Render(gl, renderMode, shader);
            });

            //gl.DepthFunc(OpenGL.GL_LESS);
        }



        /// <summary>
        /// Calculates the lines using the current axis properties.
        /// </summary>
        public virtual void RecalculateShape(OpenGL gl)
        {
            var riseAxisValue = 0f;// .0001f; // Axis didn't combine well with 

            var lineX = new List<Tuple<vec3, vec3>>();
            var lineY = new List<Tuple<vec3, vec3>>();
            var lineZ = new List<Tuple<vec3, vec3>>();

            lineX.Add(
                new Tuple<vec3, vec3>(
                    new vec3(0, riseAxisValue, 0),
                    new vec3(_axisLength, riseAxisValue, 0))
                );

            lineY.Add(
                new Tuple<vec3, vec3>(
                    new vec3(0, riseAxisValue, 0),
                    new vec3(0, _axisLength, 0))
                );

            lineZ.Add(
                new Tuple<vec3, vec3>(
                    new vec3(0, riseAxisValue, 0),
                    new vec3(0, riseAxisValue, _axisLength))
                );


            _lineX = new Lines(gl, lineX, null);
            _lineY = new Lines(gl, lineY, null);
            _lineZ = new Lines(gl, lineZ, null);
        }

        /// <summary>
        /// Ensures that all required properties are acceptable.
        /// </summary>
        private void ValidateBeforeRender()
        {
            if (_lineX == null || _lineY == null || _lineZ == null)
            {
                throw new Exception("Axis aren't calculated.");
            }
        }
    }
}
