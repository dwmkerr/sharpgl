using GlmNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGLBase.Extensions;

namespace SharpGLBase.Scene
{
    public enum RotationMethod { TurnTableYZ, TurnTableXZ, TurnTableXY, Simple }
    /// <summary>
    /// Contains the rotation, scaling and translation matrices, together with the result produced from multiplying them.
    /// Override RecalculateResultMatrix(...) in order to multiply the result matrix to your vectors/matrices whenever a transformation has been done.
    /// </summary>
    public abstract class TransformableBase
    {
        #region fields
        private mat4 _translationMatrix = mat4.identity();
        private mat4 _rotationMatrix = mat4.identity();
        private mat4 _scalingMatrix = mat4.identity();
        private mat4 _resultMatrix = mat4.identity();
        private RotationMethod _rotationMethod = RotationMethod.Simple;
        private vec3 _rotatedRadians = new vec3(0, 0, 0);

        #endregion fields

        #region properties
        /// <summary>
        /// Contains the result of all transformations.
        /// </summary>
        public mat4 ResultMatrix
        {
            get { return _resultMatrix; }
            set { _resultMatrix = value; }
        }
        /// <summary>
        /// Represents all translations.
        /// </summary>
        public mat4 TranslationMatrix
        {
            get { return _translationMatrix; }
            set { _translationMatrix = value; }
        }
        /// <summary>
        /// Represents all rotations.
        /// </summary>
        public mat4 RotationMatrix
        {
            get { return _rotationMatrix; }
            set { _rotationMatrix = value; }
        }
        /// <summary>
        /// Represents all scalings.
        /// </summary>
        public mat4 ScalingMatrix
        {
            get { return _scalingMatrix; }
            set { _scalingMatrix = value; }
        }
        /// <summary>
        /// Defines the way of rotating, TurnTable is usually more intuitive for camera control. TrackBall provides more freedom.
        /// </summary>
        public RotationMethod RotationMethod
        {
            get { return _rotationMethod; }
            set { _rotationMethod = value; }
        }
        /// <summary>
        /// Gets the value of rotation (radians) in each direction.
        /// </summary>
        public vec3 RotatedRadians
        {
            get { return _rotatedRadians; }
        }
        #endregion properties

        #region absolute rotation
        /// <summary>
        /// Rotates by angleRadians in the direction(s) v and forces a recalculation of the result matrix.
        /// </summary>
        /// <param name="angleRadians">The angle of the rotation in radians.</param>
        /// <param name="v">The vector that represents the axis around which should be rotated.</param>
        public void RotateAbsolute(float angleRadians, vec3 v)
        {
            if (angleRadians == 0)
                return;

            mat4 tempRotationMatrix = _rotationMatrix;
            switch (_rotationMethod)
            {
                case RotationMethod.TurnTableXY:
                    tempRotationMatrix = GlmNet.glm.rotate(tempRotationMatrix, -_rotatedRadians.x, new vec3(1, 0, 0));
                    tempRotationMatrix = GlmNet.glm.rotate(tempRotationMatrix, -_rotatedRadians.y, new vec3(0, 1, 0));
                    tempRotationMatrix = GlmNet.glm.rotate(tempRotationMatrix, angleRadians, v);
                    tempRotationMatrix = GlmNet.glm.rotate(tempRotationMatrix, _rotatedRadians.y, new vec3(0, 1, 0));
                    tempRotationMatrix = GlmNet.glm.rotate(tempRotationMatrix, _rotatedRadians.x, new vec3(1, 0, 0));
                    break;
                case RotationMethod.TurnTableXZ: 
                    tempRotationMatrix = GlmNet.glm.rotate(tempRotationMatrix, -_rotatedRadians.x, new vec3(1, 0, 0));
                    tempRotationMatrix = GlmNet.glm.rotate(tempRotationMatrix, -_rotatedRadians.z, new vec3(0, 0, 1));
                    tempRotationMatrix = GlmNet.glm.rotate(tempRotationMatrix, angleRadians, v);
                    tempRotationMatrix = GlmNet.glm.rotate(tempRotationMatrix, _rotatedRadians.z, new vec3(0, 0, 1));
                    tempRotationMatrix = GlmNet.glm.rotate(tempRotationMatrix, _rotatedRadians.x, new vec3(1, 0, 0));
                    break;
                case RotationMethod.TurnTableYZ: 
                    tempRotationMatrix = GlmNet.glm.rotate(tempRotationMatrix, -_rotatedRadians.y, new vec3(0, 1, 0));
                    tempRotationMatrix = GlmNet.glm.rotate(tempRotationMatrix, -_rotatedRadians.z, new vec3(0, 0, 1));
                    tempRotationMatrix = GlmNet.glm.rotate(tempRotationMatrix, angleRadians, v);
                    tempRotationMatrix = GlmNet.glm.rotate(tempRotationMatrix, _rotatedRadians.z, new vec3(0, 0, 1));
                    tempRotationMatrix = GlmNet.glm.rotate(tempRotationMatrix, _rotatedRadians.y, new vec3(0, 1, 0));
                    break;
                default:
                    tempRotationMatrix = GlmNet.glm.rotate(tempRotationMatrix, angleRadians, v);
                    break;
            }

            _rotationMatrix = tempRotationMatrix;


            _rotatedRadians.x += v.x * angleRadians;
            _rotatedRadians.y += v.y * angleRadians;
            _rotatedRadians.z += v.z * angleRadians;


            RecalculateResultMatrix();
        }
        /// <summary>
        /// Calls RotateAbsolute(angleRadians, new vec3(1, 0, 0));
        /// </summary>
        /// <param name="angleRadians"></param>
        public void RotateAbsoluteX(float angleRadians)
        {
            RotateAbsolute(angleRadians, new vec3(1, 0, 0));
        }
        /// <summary>
        /// Calls RotateAbsolute(angleRadians, new vec3(0, 1, 0));
        /// </summary>
        /// <param name="angleRadians"></param>
        public void RotateAbsoluteY(float angleRadians)
        {
            RotateAbsolute(angleRadians, new vec3(0, 1, 0));
        }
        
        /// <summary>
        /// RotateAbsolute(angleRadians, new vec3(0, 0, 1));
        /// </summary>
        /// <param name="angleRadians"></param>
        public void RotateAbsoluteZ(float angleRadians)
        {
            RotateAbsolute(angleRadians, new vec3(0, 0, 1));
        }

        #endregion absolute rotation

        #region absolute translation
        /// <summary>
        /// Translates by the values contained in 'vec'.
        /// </summary>
        /// <param name="vec">Translation vector.</param>
        public void TranslateAbsolute(vec3 vec)
        {
            _translationMatrix = GlmNet.glm.translate(_translationMatrix, vec);
            RecalculateResultMatrix();
        }
        /// <summary>
        /// Calls TranslateAbsolute(new vec3(distance, 0, 0));
        /// </summary>
        /// <param name="distance">The distance.</param>
        public void TranslateAbsoluteX(float distance)
        {
            TranslateAbsolute(new vec3(distance, 0, 0));
        }
        /// <summary>
        /// Calls TranslateAbsolute(new vec3(0, distance, 0));
        /// </summary>
        /// <param name="distance">The distance.</param>
        public void TranslateAbsoluteY(float distance)
        {
            TranslateAbsolute(new vec3(0, distance, 0));
        }
        /// <summary>
        /// Calls TranslateAbsolute(new vec3(0, 0, distance));
        /// </summary>
        /// <param name="distance">The distance.</param>
        public void TranslateAbsoluteZ(float distance)
        {
            TranslateAbsolute(new vec3(0, 0, distance));
        }
        #endregion absolute translation

        #region relative translation
        public void TranslateOnRotationMatrix(vec3 vec)
        {
            ////var tMatrix = GlmNet.glm.translate(mat4.identity(), vec);
            ////var invRotMatrix = _rotationMatrix.Inverse();
            ////var resMat = (tMatrix * _rotationMatrix).Transpose(); // 
            ////var translateVector = new vec3(resMat[3].x, resMat[3].y, resMat[3].z);


            //float largestRot;
            //float totRot = RotatedRadians.x + RotatedRadians.y + RotatedRadians.z;
            ////if (RotatedRadians.x > RotatedRadians.y)
            ////{
            ////    if (RotatedRadians.x > RotatedRadians.z)
            ////    {
            ////        // x is largest
            ////        largestRot = RotatedRadians.x;
            ////    }
            ////    else 
            ////    {
            ////        // z is largest
            ////        largestRot = RotatedRadians.z;
            ////    }
            ////}
            ////else {
            ////    if (RotatedRadians.y > RotatedRadians.z)
            ////    {
            ////        // y is largest
            ////        largestRot = RotatedRadians.y;
            ////    }
            ////    else 
            ////    {
            ////        // z is largest
            ////        largestRot = RotatedRadians.z;
            ////    }
            ////}

            //var totMoveDistance = (float)Math.Sqrt(vec.x * vec.x + vec.y * vec.y + vec.z * vec.z);

            //float addX;
            //float addY = 0;
            //float addZ;
            //if (totRot == 0)
            //{
            //    addX = vec.x;
            //    addY = vec.y;
            //    addZ = vec.z;
            //}
            //else
            //{
            //    var scaleX = RotatedRadians.x / totRot;
            //    var scaleY = RotatedRadians.y / totRot;
            //    var scaleZ = RotatedRadians.z / totRot;

            //    addX = vec.x * scaleX;
            //    //addY = vec.x * scaleY;
            //    addZ = vec.x * scaleZ;
            //}
            //vec3 translateVector = new vec3(addX, addY, addZ);




            ////ResMatDeleteMeLater = resMat;
            ////TranslationVecDeleteMeLater = vec;
            ////TranslateAbsolute(translateVector);

            //var t = _translationMatrix[3];
            //t.x += translateVector.x;
            //t.y += translateVector.y;
            //t.z += translateVector.z;
            //_translationMatrix[3] = t;

            //RecalculateResultMatrix();
        }
        #endregion relative translation

        #region absolute scaling
        /// <summary>
        /// Scales by the values contained in 'vec'.
        /// </summary>
        /// <param name="vec">Scaling vector.</param>
        public void Scale(vec3 vec)
        {
            _scalingMatrix = GlmNet.glm.scale(_scalingMatrix, vec);
            RecalculateResultMatrix();
        }
        /// <summary>
        /// Calls Scale(new vec3(scale, 1, 1));
        /// </summary>
        /// <param name="scale"></param>
        public void ScaleX(float scale)
        {
            Scale(new vec3(scale, 1, 1));
        }
        /// <summary>
        /// Calls Scale(new vec3(1, scale, 1));
        /// </summary>
        /// <param name="scale"></param>
        public void ScaleY(float scale)
        {
            Scale(new vec3(1, scale, 1));
        }
        /// <summary>
        /// Calls Scale(new vec3(1, 1, scale));
        /// </summary>
        /// <param name="scale"></param>
        public void ScaleZ(float scale)
        {
            Scale(new vec3(1, 1, scale));
        }
        #endregion absolute scaling

        /// <summary>
        /// Multiplies all transformations into the ResultMatrix in folowing sequence 'ResultMatrix = TranslationMatrix * RotationMatrix * ScalingMatrix'. 
        /// </summary>
        public virtual void RecalculateResultMatrix()
        {
            _resultMatrix = _translationMatrix * _rotationMatrix * _scalingMatrix;
        }
    }
}
