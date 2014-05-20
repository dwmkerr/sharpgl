using GlmNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGLHitTestSample
{
    /// <summary>
    /// Modelview x Projection matrix builder.
    /// </summary>
    public class ModelviewProjectionBuilder
    {
        #region fields
        mat4 _modelviewMatrix = mat4.identity(),
            _projectionMatrix = mat4.identity();

        float _height, _width, _near, _far, _fovRadians;

        #endregion fields

        #region properties
        public float Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public float Width
        {
            get { return _width; }
            set { _width = value; }
        }

        public float Near
        {
            get { return _near; }
            set { _near = value; }
        }

        public float Far
        {
            get { return _far; }
            set { _far = value; }
        }

        public float FovRadians
        {
            get { return _fovRadians; }
            set { _fovRadians = value; }
        }

        public mat4 ModelviewMatrix
        {
            get { return _modelviewMatrix; }
            set { _modelviewMatrix = value; }
        }

        public mat4 ProjectionMatrix
        {
            get { return _projectionMatrix; }
            set { _projectionMatrix = value; }
        }


        public float AngleX { get; set; }
        public float AngleY { get; set; }
        public float AngleZ { get; set; }
        public float TranslationX { get; set; }
        public float TranslationY { get; set; }
        public float TranslationZ { get; set; }

        #endregion properties

        #region events
        #endregion events

        #region constructors
        public ModelviewProjectionBuilder()
        {

        }
        #endregion constructors
        /// <summary>
        /// Slightly edited formula! 
        /// </summary>
        public void BuildPerspectiveProjection()
        {
            var a = Height / Width;
            var n = Near;
            var f = Far;
            var e = 1 / (float)Math.Tan(FovRadians / 2);


            var mat = new mat4(
                new vec4[]
                {
                    new vec4(e, 0, 0, 0),
                    new vec4(0, e/a, 0, 0),
                    new vec4(0, 0, -(2*f*n)/(f-n), -1),
                    new vec4(0, 0, -(f+n)/(f-n), 0),
                });

            ProjectionMatrix = mat;
        }

        /// <summary>
        /// Multiplies the Projection and modelview matrix into one.
        /// </summary>
        /// <returns></returns>
        public mat4 CombineToMvP()
        {
            return ProjectionMatrix * ModelviewMatrix;
        }
        public void BuildTurntableModelview(vec3 originPoint = new vec3())
        {
            var cosX = (float)Math.Cos(AngleX);
            var cosY = (float)Math.Cos(AngleY);
            var cosZ = (float)Math.Cos(AngleZ);
            var sinX = (float)Math.Sin(AngleX);
            var sinY = (float)Math.Sin(AngleY);
            var sinZ = (float)Math.Sin(AngleZ);

            mat4 rotX = new mat4(
                new vec4[]
                {
                    new vec4(1,0,0,0),
                    new vec4(0, cosX, -sinX, 0),
                    new vec4(0, sinX, cosX, 0),
                    new vec4(0,0,0,1)
                });
            mat4 rotY = new mat4(
                new vec4[]
                {
                    new vec4(cosY, 0, sinY, 0),
                    new vec4(0, 1, 0,0),
                    new vec4(-sinY, 0, cosY, 0),
                    new vec4(0,0,0,1)
                });


            var rotation = rotX * rotY;
            var translation = rotation * glm.translate(mat4.identity(), new vec3(TranslationX + originPoint.x, TranslationY + originPoint.y, TranslationZ + originPoint.z));
            var translation2 = glm.translate(translation, new vec3(-originPoint.x, -originPoint.y, -originPoint.z));


            ModelviewMatrix = translation2;
        }
    }
}
