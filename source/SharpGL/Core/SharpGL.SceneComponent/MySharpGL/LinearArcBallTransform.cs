using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// Linear arc ball only make linear transforms(rotation and scale).
    /// </summary>
    public class LinearArcBallTransform : IMouseLinearTransform
    {
        protected bool isCameraSet = false;
        public bool mouseDownFlag;
        protected float _angle;
        protected float _length, _radiusRadius;
        protected float[] _lastRotation = new float[16] { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 };
        protected Vertex _startPosition, _normalVector = new Vertex(0, 1, 0);
        protected int _width;
        protected int _height;
        protected Vertex _back;
        protected Vertex _up;
        protected Vertex _right;
        //protected mat4 currentRotation = mat4.identity();
        float _scale = 1.0f;
        IScientificCamera _camera;

        private Vertex GetArcBallPosition(int x, int y)
        {
            UpdateCameraAxis();
            float rx = (x - _width / 2) / _length;
            float ry = (_height / 2 - y) / _length;
            float zz = _radiusRadius - rx * rx - ry * ry;
            float rz = (zz > 0 ? (float)Math.Sqrt(zz) : 0);
            /*                                 | rx |
             * result = [ _right _up _back ] * | ry |
             *                                 | rz |
             */
            Vertex result = new Vertex(
                (rx * _right.X + ry * _up.X + rz * _back.X),
                (rx * _right.Y + ry * _up.Y + rz * _back.Y),
                (rx * _right.Z + ry * _up.Z + rz * _back.Z)
                );
            return result;
        }

        private void UpdateCameraAxis()
        {
            IViewCamera camera = this._camera;
            if (camera == null) { return; }

            _back = camera.Position - camera.Target;
            _back.Normalize();
            _right = camera.UpVector.VectorProduct(_back);
            _right.Normalize();
            _up = _back.VectorProduct(_right);
            _up.Normalize();
        }

        //public mat4 GetTransformMat4()
        //{
        //    mat4 rotation = GetRotation();
        //    mat4 scale = glm.scale(mat4.identity(), new vec3(Scale));
        //    mat4 translate = glm.translate(mat4.identity(), new vec3(Translate.X,
        //        Translate.Y, Translate.Z));
        //    //result = translate * rotation * scale;//rotate good
        //    //result = translate * scale * rotation;//rotate reversed
        //    //result = rotation * translate * scale;//rotate reversed
        //    //result = rotation * scale * translate;
        //    //result = scale * translate * rotation;
        //    mat4 result = scale * rotation * translate;//rotate good
        //    return result;
        //}

        //public mat4 GetRotation()
        //{
        //    return currentRotation;
        //}

        //private void UpdateRotation()
        //{
        //    float angle = (float)(_angle * Math.PI / 180.0f);
        //    mat4 rotation = glm.rotate(angle, new vec3(_normalVector.X, _normalVector.Y, _normalVector.Z));
        //    currentRotation = rotation * currentRotation;
        //}

        public float[] GetCurrentRotation()
        {
            return this._lastRotation.ToArray();
        }


        #region IMouseTransform 成员

        public void TransformMatrix(OpenGL gl)
        {
            if (!isCameraSet)
            { throw new Exception("Camera is not set by using SetCamera(..)"); }

            if (_angle != 0)
            {
                gl.PushMatrix();
                gl.LoadIdentity();
                gl.Rotate(2 * _angle, _normalVector.X, _normalVector.Y, _normalVector.Z);
                gl.MultMatrix(_lastRotation);
                gl.GetFloat(SharpGL.Enumerations.GetTarget.ModelviewMatix, _lastRotation);
                gl.PopMatrix();
                //UpdateRotation();
                System.Threading.Interlocked.Exchange(ref _angle, 0);
            }

            gl.MultMatrix(_lastRotation);
            gl.Scale(Scale, Scale, Scale);
        }

        #endregion

        #region IMouseRotation 成员

        public IScientificCamera Camera
        {
            get { return _camera; }
            set
            {
                _camera = value;
                isCameraSet = value != null;
            }
        }

        public void MouseUp(int x, int y)
        {
            mouseDownFlag = false;
        }

        public void MouseMove(int x, int y)
        {
            if (mouseDownFlag)
            {
                Vertex startPosition = this._startPosition;
                Vertex endPosition = GetArcBallPosition(x, y);
                double cosAngle = startPosition.ScalarProduct(endPosition) / (startPosition.Magnitude() * endPosition.Magnitude());
                if (cosAngle > 1) { cosAngle = 1; }
                else if (cosAngle < -1) { cosAngle = -1; }
                float angle = 1 * (float)(Math.Acos(cosAngle) / Math.PI * 180);
                System.Threading.Interlocked.Exchange(ref _angle, angle);
                this._normalVector = startPosition.VectorProduct(endPosition);
                this._startPosition = endPosition;
            }
        }

        public void SetBounds(int width, int height)
        {
            this._width = width;
            this._height = height;
            this._length = width > height ? width : height;
            float rx = (width / 2) / _length;
            float ry = (height / 2) / _length;
            this._radiusRadius = rx * rx + ry * ry;
        }

        public void MouseDown(int x, int y)
        {
            this._startPosition = GetArcBallPosition(x, y);

            mouseDownFlag = true;
        }

        public void ResetRotation()
        {
            this._lastRotation = new float[16] { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 };
            //this.currentRotation = mat4.identity();
        }

        #endregion

        #region IMouseScale 成员

        public float Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }

        #endregion

    }
}
