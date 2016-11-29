using GlmNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGLBase.Primitives.ModelComponents
{
    public class Face
    {
        #region fields
        Vertex[] _vertices;
        vec3 _normal;
        #endregion fields

        #region properties
        public Vertex[] Vertices
        {
            get { return _vertices; }
        }

        public vec3 Normal
        {
            get { return _normal; }
            set { _normal = value; }
        }
        #endregion properties


        public Face(Vertex[] vertices)
        {
            _vertices = vertices;
            foreach (var vertex in vertices)
            {
                vertex.ParentFaces.Add(this); // Establish many-to-many relationship.   
            }
        }
        public void CalculateNormal()
        {
            var v1 = _vertices[0];
            var v2 = _vertices[1];
            var v3 = _vertices[2];
            var edge1 = v2.Vec3-v1.Vec3;
            var edge2 = v3.Vec3-v1.Vec3;
            var normal = glm.cross(edge1, edge2);

            //for (int i = 0; i < _vertices.Length - 1; i++)
            //{
            //    vec3 curVert = _vertices[i].Vec3;
            //    vec3 nextVert = _vertices[(i + 1) % _vertices.Length].Vec3;

            //    normal.x += (curVert.y - nextVert.y) * (curVert.z + nextVert.z);
            //    normal.y += (curVert.z - nextVert.z) * (curVert.x + nextVert.x);
            //    normal.z += (curVert.x - nextVert.x) * (curVert.y + nextVert.y);
            //}

            
            if (normal.x == 0 && normal.y == 0 && normal.z ==0)
            {
                Normal = new vec3(0, 0, 0);
            }
            else
            {
                normal.z = -normal.z;
                normal.x = -normal.x;
                normal.y = -normal.y;
                Normal = glm.normalize(normal);
            }
        }
    }
}
