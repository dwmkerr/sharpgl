using System;
using System.ComponentModel;
using GlmNet;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Lighting;

namespace SharpGL.SceneGraph.Core
{
    /// <summary>
    /// The ArcBall camera supports arcball projection, making it ideal for use with a mouse.
    /// </summary>
    [Serializable()]
    public class ArcBall
    {
        private bool mouseDownFlag;
        private float _angle;
        private float _length, _radiusRadius;
        private float[] _lastRotation = new float[16] { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 };
        private Vertex _startPosition, _endPosition, _normalVector = new Vertex(0, 1, 0);
        int _width;
        int _height;

        public ArcBall(float eyex, float eyey, float eyez,
            float centerx, float centery, float centerz,
            float upx, float upy, float upz)
        {
            SetCamera(eyex, eyey, eyez, centerx, centery, centerz, upx, upy, upz);
        }
        public void SetBounds(int width, int height)
        {
            this._width = width; this._height = height;
            _length = width > height ? width : height;
            var rx = (width / 2) / _length;
            var ry = (height / 2) / _length;
            _radiusRadius = (float)(rx * rx + ry * ry);
        }

        public void MouseDown(int x, int y)
        {
            this._startPosition = GetArcBallPosition(x, y);

            mouseDownFlag = true;
        }

        private Vertex GetArcBallPosition(int x, int y)
        {
            var rx = (x - _width / 2) / _length;
            var ry = (_height / 2 - y) / _length;
            var zz = _radiusRadius - rx * rx - ry * ry;
            var rz = (zz > 0 ? Math.Sqrt(zz) : 0);
            var result = new Vertex(
                (float)(rx * _vectorRight.X + ry * _vectorUp.X + rz * _vectorCenterEye.X),
                (float)(rx * _vectorRight.Y + ry * _vectorUp.Y + rz * _vectorCenterEye.Y),
                (float)(rx * _vectorRight.Z + ry * _vectorUp.Z + rz * _vectorCenterEye.Z)
                );
            return result;
        }


        public void MouseMove(int x, int y)
        {
            if (mouseDownFlag)
            {
                this._endPosition = GetArcBallPosition(x, y);
                var cosAngle = _startPosition.ScalarProduct(_endPosition) / (_startPosition.Magnitude() * _endPosition.Magnitude());
                if (cosAngle > 1) { cosAngle = 1; }
                else if (cosAngle < -1) { cosAngle = -1; }
                var angle = 10 * (float)(Math.Acos(cosAngle) / Math.PI * 180);
                System.Threading.Interlocked.Exchange(ref _angle, angle);
                _normalVector = _startPosition.VectorProduct(_endPosition);
                _startPosition = _endPosition;
            }
        }

        public void MouseUp(int x, int y)
        {
            mouseDownFlag = false;
        }

        public mat4 GetTransformMat4()
        {
            if (_angle != 0)
            {
                var angle = (float)(_angle * Math.PI / 180.0f);
                var rotation = glm.rotate(angle, new vec3(_normalVector.X, _normalVector.Y, _normalVector.Z));
                rotation = rotation * originalRotation;
                originalRotation = rotation;
                System.Threading.Interlocked.Exchange(ref _angle, 0);
            }
            var scale = glm.scale(mat4.identity(), new vec3(Scale));
            var translate = glm.translate(mat4.identity(), new vec3(TranslateX,
                TranslateY, TranslateZ));
            //result = translate * originalRotation * scale;//rotate good
            //result = translate * scale * originalRotation;//rotate reversed
            //result = originalRotation * translate * scale;//rotate reversed
            //result = originalRotation * scale * translate;
            //result = scale * translate * originalRotation;
            var result = scale * originalRotation * translate;//rotate good
            return result;
        }
        public mat4 GetRotation()
        {
            var angle = (float)(_angle * Math.PI / 180.0f);
            var rotation = glm.rotate(angle, new vec3(_normalVector.X, _normalVector.Y, _normalVector.Z));
            rotation = rotation * originalRotation;
            originalRotation = rotation;
            System.Threading.Interlocked.Exchange(ref _angle, 0);
            return rotation;
        }
        public void TransformMatrix(OpenGL gl)
        {
            gl.PushMatrix();
            gl.LoadIdentity();
            gl.Rotate(2 * _angle, _normalVector.X, _normalVector.Y, _normalVector.Z);
            System.Threading.Interlocked.Exchange(ref _angle, 0);
            gl.MultMatrix(_lastRotation);
            gl.GetFloat(Enumerations.GetTarget.ModelviewMatix, _lastRotation);
            gl.PopMatrix();
            gl.Translate(_translateX, _translateY, _translateZ);
            gl.MultMatrix(_lastRotation);
            gl.Scale(Scale, Scale, Scale);
        }

        /// <summary>
        /// Default camera is at positive Z axis to look at negtive Z axis with up vector to positive Y axis.
        /// </summary>
        /// <param name="eyex"></param>
        /// <param name="eyey"></param>
        /// <param name="eyez"></param>
        /// <param name="centerx"></param>
        /// <param name="centery"></param>
        /// <param name="centerz"></param>
        /// <param name="upx"></param>
        /// <param name="upy"></param>
        /// <param name="upz"></param>
        public void SetCamera(float eyex, float eyey, float eyez,
            float centerx, float centery, float centerz,
            float upx, float upy, float upz)
        {
            _vectorCenterEye = new Vertex(eyex - centerx, eyey - centery, eyez - centerz);
            _vectorCenterEye.Normalize();
            _vectorUp = new Vertex(upx, upy, upz);
            _vectorRight = _vectorUp.VectorProduct(_vectorCenterEye);
            _vectorRight.Normalize();
            _vectorUp = _vectorCenterEye.VectorProduct(_vectorRight);
            _vectorUp.Normalize();
        }

        public void GoUp(float interval)
        {
            this._translateX += this._vectorUp.X * interval;
            this._translateY += this._vectorUp.Y * interval;
            this._translateZ += this._vectorUp.Z * interval;
        }
        public void GoDown(float interval)
        {
            this._translateX -= this._vectorUp.X * interval;
            this._translateY -= this._vectorUp.Y * interval;
            this._translateZ -= this._vectorUp.Z * interval;
        }
        public void GoLeft(float interval)
        {
            this._translateX -= this._vectorRight.X * interval;
            this._translateY -= this._vectorRight.Y * interval;
            this._translateZ -= this._vectorRight.Z * interval;
        }
        public void GoRight(float interval)
        {
            this._translateX += this._vectorRight.X * interval;
            this._translateY += this._vectorRight.Y * interval;
            this._translateZ += this._vectorRight.Z * interval;
        }

        float _translateZ;

        public float TranslateZ
        {
            get { return _translateZ; }
            set { _translateZ = value; }
        }
        float _translateY;

        public float TranslateY
        {
            get { return _translateY; }
            set { _translateY = value; }
        }
        float _translateX;

        public float TranslateX
        {
            get { return _translateX; }
            set { _translateX = value; }
        }

        float _scale = 1.0f;
        Vertex _vectorCenterEye;
        Vertex _vectorUp;
        Vertex _vectorRight;
        private mat4 originalRotation = mat4.identity();
        public float Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }


        public void SetTranslate(double x, double y, double z)
        {
            this._translateX = (float)x;
            this._translateY = (float)y;
            this._translateZ = (float)z;
        }

        public void GoFront(int interval)
        {
            this._translateX -= this._vectorCenterEye.X * interval;
            this._translateY -= this._vectorCenterEye.Y * interval;
            this._translateZ -= this._vectorCenterEye.Z * interval;
        }
        public void GoBack(int interval)
        {
            this._translateX += this._vectorCenterEye.X * interval;
            this._translateY += this._vectorCenterEye.Y * interval;
            this._translateZ += this._vectorCenterEye.Z * interval;
        }

        public void ResetRotation()
        {
            this._lastRotation = new float[16] { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 };
            this.originalRotation = mat4.identity();
        }
    }
}
