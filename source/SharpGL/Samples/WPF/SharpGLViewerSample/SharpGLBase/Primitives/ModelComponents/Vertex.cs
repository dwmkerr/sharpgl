using GlmNet;
using SharpGLBase.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGLBase.Primitives.ModelComponents
{
    public class Vertex
    {
        #region fields
        ushort? _index = null;
        List<Face> _parentFaces = new List<Face>();
        vec3 _vertex;
        vec3 _normal;
        #endregion fields


        #region properties
        public ushort? Index
        {
            get { return _index; }
            set { _index = value; }
        }
        public vec3 Normal
        {
            get { return _normal; }
            set { _normal = value; }
        }

        public vec3 Vec3
        {
            get { return _vertex; }
            set { _vertex = value; }
        }

        public List<Face> ParentFaces
        {
            get { return _parentFaces; }
            set { _parentFaces = value; }
        }
        #endregion properties

        public Vertex() { }

        public Vertex(ushort index, vec3 normal, vec3 vertex)
        {
            _index = index;
            _normal = normal;
            _vertex = vertex;
        }


        public void CalculateVertexNormal()
        {
            vec3 normalSum = new vec3();
            foreach (var plane in _parentFaces)
            {
                normalSum += plane.Normal;
            }

            Normal = glm.normalize(normalSum);
        }

        public override string ToString()
        {
            return _vertex.ToValueString();
        }
    }
}
