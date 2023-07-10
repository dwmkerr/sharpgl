using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.VertexBuffers
{
    /// <summary>
    /// A VertexBufferArray is a logical grouping of VertexBuffers. Vertex Buffer Arrays
    /// allow us to use a set of vertex buffers for vertices, indicies, normals and so on,
    /// without having to use more complicated interleaved arrays.
    /// </summary>
    public class VertexBufferArray : IDisposable
    {
        private uint vertexArrayObject;
        public OpenGL OpenGL { get; private set; }
        /// <summary>
        /// Gets the vertex buffer array object.
        /// </summary>
        public uint VertexBufferArrayObject
        {
            get { return vertexArrayObject; }
        }
        public bool IsCreated => vertexArrayObject != 0;
        public void Create(OpenGL gl)
        {
            if (IsCreated) Delete();
            OpenGL = gl;
            //  Generate the vertex array.
            uint[] ids = new uint[1];
            gl.GenVertexArrays(1, ids);
            vertexArrayObject = ids[0];
        }

        public void Delete()
        {
            if (!IsCreated) return;
            OpenGL.DeleteVertexArrays(1, new uint[] { vertexArrayObject });
            vertexArrayObject = 0;
        }
        public void Delete(OpenGL gl)
        {
            if (!IsCreated) return;
            gl.DeleteVertexArrays(1, new uint[] { vertexArrayObject });
            vertexArrayObject = 0;
        }
        public void Bind()
        {
            if (!IsCreated) return;
            OpenGL.BindVertexArray(vertexArrayObject);
        }
        public void Bind(OpenGL gl)
        {
            if (!IsCreated) return;
            gl.BindVertexArray(vertexArrayObject);
        }
        public void Unbind()
        {
            OpenGL.BindVertexArray(0);
        }
        public void Unbind(OpenGL gl)
        {
            gl.BindVertexArray(0);
        }
        #region IDisposable
        private bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                Delete();
                disposedValue = true;
            }
        }
        ~VertexBufferArray()
        {
            Dispose(disposing: false);
        }
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
