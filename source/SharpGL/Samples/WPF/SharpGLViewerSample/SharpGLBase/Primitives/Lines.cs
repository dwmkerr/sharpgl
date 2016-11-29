using GlmNet;
using SharpGL;
using SharpGL.Enumerations;
using SharpGL.SceneGraph.Assets;
using SharpGL.SceneGraph.Core;
using SharpGL.VertexBuffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGLBase.Primitives
{
    public class Lines
    {
        #region fields
        private uint _glDrawMode = SharpGL.OpenGL.GL_LINE;
        vec3[] _vertices, _normals;
        ushort[] _indices;
        Material _material;
        OpenGL _openGL;
        private VertexBuffer _vertexBuffer, _normalBuffer;
        private IndexBuffer _indexBuffer;
        float _lineWidth = 1f;
        #endregion fields


        #region properties
        public vec3[] Vertices
        {
            get { return _vertices; }
            set { _vertices = value; }
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

        public uint GlDrawMode
        {
            get { return _glDrawMode; }
            set { _glDrawMode = value; }
        }

        public Material Material
        {
            get { return _material; }
            set { _material = value; }
        }

        public OpenGL GL
        {
            get { return _openGL; }
            set { _openGL = value; }
        }

        public VertexBuffer VertexBuffer
        {
            get { return _vertexBuffer; }
            set { _vertexBuffer = value; }
        }

        public VertexBuffer NormalBuffer
        {
            get { return _normalBuffer; }
            set { _normalBuffer = value; }
        }
        public IndexBuffer IndexBuffer
        {
            get { return _indexBuffer; }
            set { _indexBuffer = value; }
        }

        public float LineWidth
        {
            get { return _lineWidth; }
            set { _lineWidth = value; }
        }

        #endregion properties

        public Lines(OpenGL gl, List<Tuple<vec3, vec3>> lines, Material material = null)
        {
            
            var verts = new vec3[lines.Count * 2];
            var normals = new vec3[lines.Count * 2];
            var indices = new ushort[lines.Count * 2];

            for (int i = 0; i < lines.Count; i++)
            {
                var i2 = i * 2;
                verts[i2] = lines[i].Item1;
                verts[i2 + 1] = lines[i].Item2;

                normals[i2] = new vec3(1, 1, 1);
                normals[i2 + 1] = new vec3(1, 1, 1);

                indices[i2] = (ushort)i2;
                indices[i2 + 1] = (ushort)(i2 + 1);
            }

            if (material != null)
                Material = material;

            _vertices = verts;
            _normals = normals;
            _indices = indices;
            GlDrawMode = OpenGL.GL_LINES;


            if (gl != null)
                GenerateGeometry(gl);
        }

        /// <summary>
        /// Generates the vertices, normals and indices and creates them for the OpenGL.
        /// This method has to be called once before drawing. 
        /// </summary>
        /// <param name="gl"></param>
        public void GenerateGeometry(OpenGL gl)
        {
            _openGL = gl;

            // Create the index data buffer.
            _indexBuffer = new IndexBuffer();
            _indexBuffer.Create(_openGL);

            // Create the vertex data buffer.
            _vertexBuffer = new VertexBuffer();
            _vertexBuffer.Create(_openGL);

            _normalBuffer = new VertexBuffer();
            _normalBuffer.Create(_openGL);
        }

        public void Render(OpenGL gl, RenderMode renderMode, Shaders.ExtShaderProgram shader = null)
        {

            // Binds buffers.
            Bind();

            // Sets the linewidth.
            gl.LineWidth(LineWidth);
            // Draw the elements.
            gl.DrawArrays(GlDrawMode, 0, Vertices.Length);
            //gl.DrawElements(GlDrawMode, Indices.Length, OpenGL.GL_UNSIGNED_SHORT, IntPtr.Zero);

            //gl.PopAttrib();
        }



        /// <summary>
        /// Calls VertexBuffer.Bind(gl), IndexBuffer.Bind(gl) and Material.Bind(gl). 
        /// </summary>
        /// <param name="gl">The OpenGL</param>
        public void Bind()
        {
            if (_openGL == null)
            {
                throw new ArgumentNullException("OpenGL parameter cannot be null. Call 'GenerateGeomerty(...)' before attempting to bind.");
            }

            // Bind the vertex, normal and index buffers.
            if (_vertexBuffer != null)
            {
                _vertexBuffer.Bind(_openGL);
                _vertexBuffer.SetData(_openGL, VertexAttributes.Position, Vertices.SelectMany(v => v.to_array()).ToArray(), false, 3);
            }

            if (_normalBuffer != null)
            {
                _normalBuffer.Bind(_openGL);
                _normalBuffer.SetData(_openGL, VertexAttributes.Normal, Normals.SelectMany(v => v.to_array()).ToArray(), false, 3);
            }

            if (_indexBuffer != null)
            {
                _indexBuffer.Bind(_openGL);
                _indexBuffer.SetData(_openGL, Indices);
            }

            if (Material != null)
                Material.Bind(_openGL);
        }
    }
}
