using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SharpGL.SceneGraph.JOG
{
    /// <summary>
    /// 
    /// </summary>
    public class Material
    {
        #region fields
        static ColorF _defaultColor = new ColorF(0, 0, 0, 0);
        float shininess;
        ColorF _ambient, diffuse, specular, emission;
        #endregion fields

        #region properties
        public ColorF Ambient
        {
            get { return _ambient; }
            set { _ambient = value; }
        }

        public ColorF Diffuse
        {
            get { return diffuse; }
            set { diffuse = value; }
        }

        public ColorF Specular
        {
            get { return specular; }
            set { specular = value; }
        }

        public ColorF Emission
        {
            get { return emission; }
            set { emission = value; }
        }

        public float Shininess
        {
            get { return shininess; }
            set { shininess = value; }
        }
        #endregion properties

        #region constructors
        /// <summary>
        /// Default constructor. No users will be recorded.
        /// </summary>
        public Material()
        {
            Ambient = _defaultColor;
            Diffuse = _defaultColor;
            Specular = _defaultColor;
            Emission = _defaultColor;
        }
        public Material(ColorF ambient, ColorF diffuse, ColorF specular, ColorF emission, float shininess)
            : this()
        {
            Ambient = ambient == null? _defaultColor : ambient;
            Diffuse = diffuse == null? _defaultColor : diffuse;
            Specular = specular == null? _defaultColor : specular;
            Emission = emission == null? _defaultColor : emission;
            Shininess = shininess;
        }
        #endregion constructors


    }
}
