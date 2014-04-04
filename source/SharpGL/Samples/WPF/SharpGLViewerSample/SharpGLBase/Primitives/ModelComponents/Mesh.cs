using GlmNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGLBase.Primitives.ModelComponents
{
    public class Mesh
    {
        #region fields
        Face[] _faces;
        vec3[] _vertices, _normals;
        ushort[] _indices;
        Vertex[] _allVertices;
        Dictionary<uint, Vertex> _idxForVertex;
        #endregion fields

        #region properties
        public Face[] Faces
        {
            get { return _faces; }
            set { _faces = value; }
        }
        public vec3[] Vertices
        {
            get { return _vertices; }
            set 
            { 
                _vertices = value;
                UpdateFaceVertices();
            }
        }

        public vec3[] Normals
        {
            get { return _normals; }
            set { _normals = value; }
        }
        public ushort[] Indices
        {
            get { return _indices; }
            set { _indices = value; }
        }
        #endregion properties

        #region constructors
        public Mesh()
        {

        }
        public Mesh(Face[] faces)
        {
            _faces = faces;
        }

        public Mesh(ushort[] indices, vec3[] vertices, vec3[] normals)
        {
            var n = 3;
            var faces = new List<Face>();

            _indices = indices;
            _normals = normals;
            _vertices = vertices;

            _idxForVertex = new Dictionary<uint,Vertex>();
            _allVertices = new Vertex[vertices.Length];

            for (int i = 0; i < indices.Length; i+=n)
            {
                var idx0 = indices[i];
                var idx1 = indices[i + 1];
                var idx2 = indices[i + 2];

                #region getVertex(...) function
                Func<ushort, Vertex> getVertex = idx =>
                {
                    if (_idxForVertex.ContainsKey(idx))
                    {
                        return _idxForVertex[idx];
                    }
                    else
                    {
                        var vert = new Vertex(idx,
                            normals == null ?
                            new vec3() 
                            : normals[idx], vertices[idx]);
                        _idxForVertex[idx] = vert;

                        _allVertices[idx] = vert;
                        return vert;
                    }
                };
                #endregion getVertex(...) function


                Vertex v0 = getVertex(idx0);
                Vertex v1 = getVertex(idx1);
                Vertex v2 = getVertex(idx2);


                faces.Add(new Face(new Vertex[]{v0,v1,v2}));

            }

            _faces = faces.ToArray();



            if (_normals == null)
            {
                CalculateNormals();
            }
        }

        #endregion constructors

        public void CalculateNormals()
        {
            //// Set indices to null.
            //foreach (var face in _faces)
            //{
            //    foreach (var vertex in face.Vertices)
            //    {
            //        vertex.Index = null;
            //    }
            //}

            //ushort nxtIdx = 0;
            //var vertices = new List<vec3>();
            var normals = new vec3[_allVertices.Length];
            //var indices = new List<ushort>();
            //foreach (var face in _faces)
            //{
            //    foreach (var vertex in face.Vertices)
            //    {
            //        if (vertex.Index == null)
            //        {
            //            //vertex.Index = nxtIdx++; // Set vertex and increase nxtIdx
            //            //vertices.Add(vertex.Vec3);
            //            normals.Add(vertex.Normal);
            //        }
            //        //indices.Add(vertex.Index.Value);
            //    }
            //}

            foreach (var face in _faces)
            {
                face.CalculateNormal();
            }

            foreach (var vert in _allVertices)
            {
                vert.CalculateVertexNormal();
                normals[vert.Index.Value] = vert.Normal;
            }

            //Vertices = vertices.ToArray();
            Normals = normals;//.ToArray();
            //Indices = indices.ToArray();

        }

        public void UpdateFaceVertices()
        {
            foreach (var idx in _indices)
            {
                Vertex vert = _idxForVertex[idx];
                vec3 vec3Vert = _vertices[idx];

                vert.Vec3 = vec3Vert;
            }
        }
    }
}
