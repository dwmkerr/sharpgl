using GlmNet;
using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGLBase.Scene
{
    /// <summary>
    /// This class contains all functionality related to the projection matrix.
    /// </summary>
    public class Projection
    {
        #region fields
        mat4 _projectionMatrix = mat4.identity();
        float _left, _right, _bottom, _top, _nearVal = 2f, _farVal = -1.2f;
        vec3 _translationVector = new vec3();

        #endregion fields

        #region properties
        /// <summary>
        /// The Projection Matrix.
        /// </summary>
        public mat4 ProjectionMatrix
        {
            get { return _projectionMatrix; }
            set { _projectionMatrix = value; }
        }
        
        /// <summary>
        /// The Left.
        /// Used in GlmNET.glm.frustum(...);
        /// </summary>
        public float Left
        {
            get { return _left; }
            set { _left = value; }
        }

        /// <summary>
        /// The Right.
        /// Used in GlmNET.glm.frustum(...);
        /// </summary>
        public float Right
        {
            get { return _right; }
            set { _right = value; }
        }

        /// <summary>
        /// The Bottom.
        /// Used in GlmNET.glm.frustum(...);
        /// </summary>
        public float Bottom
        {
            get { return _bottom; }
            set { _bottom = value; }
        }

        /// <summary>
        /// The Top.
        /// Used in GlmNET.glm.frustum(...);
        /// </summary>
        public float Top
        {
            get { return _top; }
            set { _top = value; }
        }

        /// <summary>
        /// The Near Val.
        /// Used in GlmNET.glm.frustum(...);
        /// </summary>
        public float NearVal
        {
            get { return _nearVal; }
            set { _nearVal = value; }
        }

        /// <summary>
        /// The Far val.
        /// Used in GlmNET.glm.frustum(...);
        /// </summary>
        public float FarVal
        {
            get { return _farVal; }
            set { _farVal = value; }
        }


        #endregion properties


        /// <summary>
        /// Resets the frustum using the current values for NearVal and FarVal.
        /// </summary>
        /// <param name="screenWidth">Width of the screen.</param>
        /// <param name="screenHeight">Height of the screen.</param>
        public void SetFrustum(float screenWidth, float screenHeight)
        {
            SetFrustum(screenWidth, screenHeight, _nearVal, _farVal);
        }

        /// <summary>
        /// Resets the frustum.
        /// </summary>
        /// <param name="screenWidth">Width of the screen.</param>
        /// <param name="screenHeight">Height of the screen.</param>
        /// <param name="nearVal"></param>
        /// <param name="farVal"></param>
        public void SetFrustum(float screenWidth, float screenHeight, float nearVal = 1, float farVal = 0)
        {
            float scale = 1 / screenWidth;
            screenWidth *= scale;
            screenHeight *= scale;

            Left = -screenWidth;
            Right = -Left;
            Bottom = -screenHeight;
            Top = -Bottom;
            NearVal = nearVal;
            FarVal = farVal;
            CalculateFrustum();
        }

        /// <summary>
        /// Recalculate the frustum from the available properties.
        /// </summary>
        public void CalculateFrustum()
        {
            _projectionMatrix = glm.frustum(Left, Right , Bottom , Top , NearVal, FarVal);

            glm.translate(_projectionMatrix, _translationVector);
        }

        /// <summary>
        /// Changes the z values from the TranslationVector, resulting in a zoom effect.
        /// </summary>
        /// <param name="distance">Zooming distance (positive => zoom in, negative => zoom out)</param>
        public void Zoom(float distance)
        {
            _translationVector.z += distance;
            CalculateFrustum();
        }

        /// <summary>
        /// Converts ProjectionMatrix to a float[].
        /// </summary>
        /// <returns>A float[] containing all the values from the initial matrix.</returns>
        public float[] ToArray()
        {
            return _projectionMatrix.to_array();
        }
    }
}
