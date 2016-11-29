using GlmNet;
using SharpGLBase.Scene;
using SharpGLBase.Extensions;
using SharpGL;
using SharpGL.SceneGraph.Assets;
using SharpGL.SceneGraph.Core;
using SharpGL.VertexBuffers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SharpGLBase.Primitives.ModelComponents;
using SharpGLBase.Shaders;

namespace SharpGLBase.Primitives
{
    public abstract class Model3DBase : TransformableBase, IHasMaterial, IDisposable
    {
        #region fields
        //private ushort[] _indices;
        private vec3[] _baseVertices;//, _transformedVertices, _normals;
        private VertexBuffer _vertexBuffer, _normalBuffer;
        private IndexBuffer _indexBuffer;
        private OpenGL _openGL;
        private bool _visible = true;
        private bool _autoCalculateNormals = true;
        private Material _material = new Material(){ Ambient = Color.Red};
        private Mesh _mesh;
        private uint _glDrawMode = OpenGL.GL_TRIANGLES;
        private int _bufferStride = 3;
        #endregion fields
        
        #region properties
        /// <summary>
        /// The material that has to be applied to this model.
        /// </summary>
        public Material Material
        {
            get;
            set;
        }
        /// <summary>
        /// This indices for this model. Used for pointing to a position in the Normals- and Vertices arrays.
        /// </summary>
        public ushort[] Indices
        {
            get { return _mesh.Indices; }
            set { _mesh.Indices = value; }
        }
        /// <summary>
        /// The normals for this model. Primarily used for lighting calculations by telling the GL in which direction the surface is facing.
        /// </summary>
        public vec3[] Normals
        {
            get { return _mesh.Normals; }
            set { _mesh.Normals = value; }
        }
        /// <summary>
        /// The vertices for this model. 
        /// </summary>
        public vec3[] BaseVertices
        {
            get { return _baseVertices; }
            set { _baseVertices = value; }
        }
        /// <summary>
        /// The vertices for this model after applying the transformations to it.
        /// </summary>
        public vec3[] TransformedVertices
        {
            get { return _mesh.Vertices == null ? _baseVertices : _mesh.Vertices; }
            set { _mesh.Vertices = value; }
        }
        /// <summary>
        /// The normal buffer for binding with the GL.
        /// </summary>
        public VertexBuffer NormalBuffer
        {
            get { return _normalBuffer; }
            set { _normalBuffer = value; }
        }
        /// <summary>
        /// The vertex buffer for binding with the GL.
        /// </summary>
        public VertexBuffer VertexBuffer
        {
            get { return _vertexBuffer; }
            set { _vertexBuffer = value; }
        }
        /// <summary>
        /// The index buffer for binding with the GL.
        /// </summary>
        public IndexBuffer IndexBuffer
        {
            get { return _indexBuffer; }
            set { _indexBuffer = value; }
        }
        /// <summary>
        /// The opengl instance.
        /// </summary>
        public OpenGL OpenGL
        {
            get { return _openGL; }
        }
        /// <summary>
        /// Checks whether this model should be drawn during the next draw-call. Implement this in your Render-method.
        /// </summary>
        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }
        /// <summary>
        /// Checks if the normals can be automatically calculated if they're unavailable or after a transformation.
        /// </summary>
        public bool AutoCalculateNormals
        {
            get { return _autoCalculateNormals; }
            set { _autoCalculateNormals = value; }
        }
        /// <summary>
        /// The mode that this model should be drawn with (GL_LINES, GL_TRIANGLES, GL_QUADS,...).
        /// </summary>
        public uint GlDrawMode
        {
            get { return _glDrawMode; }
            set { _glDrawMode = value; }
        }
        /// <summary>
        /// The stride between 
        /// </summary>
        public int BufferStride
        {
            get { return _bufferStride; }
            set { _bufferStride = value; }
        }
        #endregion properties

        #region abstract methods
        /// <summary>
        /// Draws the model to the OpenGL. GenerateGeometry(...) has to be called at least once before Draw(...).
        /// </summary>
        /// <param name="gl"></param>
        /// <param name="grid"></param>
        /// <param name="scale"></param>
        /// <param name="type"></param>
        //public abstract void Draw();
        public abstract void Render(OpenGL gl, RenderMode renderMode, ExtShaderProgram shader = null);

        #endregion abstract methods

        /// <summary>
        /// Call this method to prepare the object for being displayed or fill -BaseVertices, -Indices, -Normals manually.
        /// Also provide the OpenGL to automatically call GenerateGeometry.
        /// </summary>
        /// <param name="vertices">The BaseVertices</param>
        /// <param name="indices">The Indices</param>
        /// <param name="normals">The Normals</param>
        /// <param name="gl">The OpenGL</param>
        public void Init(vec3[] vertices, ushort[] indices, vec3[] normals = null, OpenGL gl = null)
        {
            _baseVertices = vertices;
            //_indices = indices;


            _mesh = new Mesh(indices, _baseVertices, normals);
            //_normals = normals;

            if (gl != null)
            {
                GenerateGeometry(gl);
            }
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

            if (_mesh.Normals != null)
            {
                _normalBuffer = new VertexBuffer();
                _normalBuffer.Create(_openGL);
            }
            else if (_autoCalculateNormals)
            {
                CalculateNormals();
                _normalBuffer = new VertexBuffer();
                _normalBuffer.Create(_openGL);
            }
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
                _vertexBuffer.SetData(_openGL, VertexAttributes.Position, TransformedVertices.SelectMany(v => v.to_array()).ToArray(), false, _bufferStride);
            }

            if (_normalBuffer != null)
            {
                _normalBuffer.Bind(_openGL);
                _normalBuffer.SetData(_openGL, VertexAttributes.Normal, _mesh.Normals.SelectMany(v => v.to_array()).ToArray(), false, _bufferStride);
            }

            if (_indexBuffer != null)
            {
                _indexBuffer.Bind(_openGL);
                _indexBuffer.SetData(_openGL, _mesh.Indices);
            }

            if (Material != null)
                Material.Bind(_openGL);
        }

        /// <summary>
        /// When the creation of the new transformation matrix is finished, this matrix must to be applied to every vertex.
        /// </summary>
        public override void RecalculateResultMatrix()
        {
            base.RecalculateResultMatrix();

            vec3[] vertices = new vec3[BaseVertices.Length];
            // Apply transformations to each vertex
            for (int baseVertIdx = 0; baseVertIdx < BaseVertices.Length; baseVertIdx++)
			{
			    vec3 vec3Vert = BaseVertices[baseVertIdx];
                vec4 vec4Vert = new vec4(vec3Vert.x, vec3Vert.y, vec3Vert.z, 1);

                // vec4 res = base.ResultMatrix * vec4Vert; // Apparently not supported in GlmNET, so we do it manually.
                vec4 res = new vec4();
                for (int i = 0; i < 4; i++) // i = collumn for mat4
                {
                    float val = 0;
                    for (int j = 0; j < 4; j++) // j = row for mat4 and col for vec4
                    {
                        val += vec4Vert[j] * base.ResultMatrix[j][i];
                    }
                    res[i] = val;
                }
                vertices[baseVertIdx] = new vec3(res[0], res[1], res[2]);
            }

            TransformedVertices = vertices;
            //Vertices changed, so normals need to be recalculated
            CalculateNormals();
        }

        /// <summary>
        /// Recalculates the normals.
        /// NOTE: this method is virtual.
        /// </summary>
        public virtual void CalculateNormals()
        {
            _mesh.CalculateNormals();

        }


        /// <summary>
        /// Cleans up OpenGL memory when this element is no longer needed.
        /// </summary>
        public void Dispose()
        {
            if (_openGL == null)
                return;

            List<uint> buffersToBeRemoved = new List<uint>();

            if (_indexBuffer != null)
                buffersToBeRemoved.Add(_indexBuffer.IndexBufferObject);
            if (_vertexBuffer != null)
                buffersToBeRemoved.Add(_vertexBuffer.VertexBufferObject);
            if (_normalBuffer != null)
                buffersToBeRemoved.Add(_normalBuffer.VertexBufferObject);

            _openGL.DeleteBuffers(buffersToBeRemoved.Count, buffersToBeRemoved.ToArray());
        }

        /// <summary>
        /// Generates and draws the model from scratch for the given GL. Do NOT use this method for each draw-call. 
        /// This method should only be used for drawing a model once.
        /// </summary>
        /// <param name="gl"></param>
        /// <param name="model"></param>
        public static void GenerateAndDrawOnce(OpenGL gl, Model3DBase model)
        {
            var verts = model.TransformedVertices.SelectMany(v => v.to_array()).ToArray();
            var normals = model.Normals.SelectMany(v => v.to_array()).ToArray();
            var indices = model.Indices;
            var drawMode = model.GlDrawMode;


            // Create the index data buffer.
            var indexBuffer = new IndexBuffer();
            indexBuffer.Create(gl);

            // Create the vertex data buffer.
            var vertexBuffer = new VertexBuffer();
            vertexBuffer.Create(gl);

            // Create the normal data buffer.
            var normalBuffer = new VertexBuffer();
            normalBuffer.Create(gl);


            // Bind the vertex, normal and index buffers.
            vertexBuffer.Bind(gl);
            vertexBuffer.SetData(gl, VertexAttributes.Position, verts, false, 3);

            normalBuffer.Bind(gl);
            normalBuffer.SetData(gl, VertexAttributes.Normal, normals, false, 3);

            indexBuffer.Bind(gl);
            indexBuffer.SetData(gl, indices);

            gl.FrontFace(OpenGL.GL_CW);

            // Draw the elements.
            gl.DrawElements(drawMode, indices.Length, OpenGL.GL_UNSIGNED_SHORT, IntPtr.Zero);


            // Clean up
            List<uint> buffersToBeRemoved = new List<uint>();

            if (indexBuffer != null)
                buffersToBeRemoved.Add(indexBuffer.IndexBufferObject);
            if (vertexBuffer != null)
                buffersToBeRemoved.Add(vertexBuffer.VertexBufferObject);
            if (normalBuffer != null)
                buffersToBeRemoved.Add(normalBuffer.VertexBufferObject);

            gl.DeleteBuffers(buffersToBeRemoved.Count, buffersToBeRemoved.ToArray());
        }


        /// <summary>
        /// Converts an (uint)model.VertexBuffer.VertexBufferObject to a ARGB color.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A float[4] containing RGBA values.</returns>
        public Color GenerateColorFromId()
        {
            if (VertexBuffer == null)
            {
                return Color.Black;
            }

            // Get the integer ID
            var i = (int)VertexBuffer.VertexBufferObject;

            int b = (i >> 16) & 0xFF;
            int g = (i >> 8) & 0xFF;
            int r = i & 0xFF;

            return Color.FromArgb(255, r, g, b);
        }
    }
}
