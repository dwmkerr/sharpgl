using SharpGL.Enumerations;
using System;

namespace SharpGL.VertexBuffers
{
    public class IndexBuffer : IDisposable
    {
        private uint bufferObject;
        /// <summary>
        /// Gets the index buffer object.
        /// </summary>
        public uint IndexBufferObject
        {
            get { return bufferObject; }
        }
        public OpenGL OpenGL { get; private set; }
        public BufferBindingTarget Target { get; private set; } = BufferBindingTarget.GL_ELEMENT_ARRAY_BUFFER;
        public BufferUsage Usage { get; private set; }
        public bool IsCreated => bufferObject != 0;
        public void Create(OpenGL gl)
        {
            if (IsCreated) Delete();
            OpenGL = gl;
            //  Generate the vertex array.
            uint[] ids = new uint[1];
            gl.GenBuffers(1, ids);
            bufferObject = ids[0];
        }

        public void Delete()
        {
            if (!IsCreated) return;
            OpenGL.DeleteBuffers(1, new uint[] { bufferObject });
            bufferObject = 0;
        }
        public void Delete(OpenGL gl)
        {
            if (!IsCreated) return;
            gl.DeleteBuffers(1, new uint[] { bufferObject });
            bufferObject = 0;
        }
        public void SetData( ushort[] rawData, BufferUsage usage = BufferUsage.GL_STATIC_DRAW)
        {
            if (!IsCreated) return;
            OpenGL.BufferData((uint)Target, rawData, (uint)usage);
            Usage = usage;
        }
        public void SetData(OpenGL gl, ushort[] rawData, BufferUsage usage = BufferUsage.GL_STATIC_DRAW)
        {
            if (!IsCreated) return;
            gl.BufferData((uint)Target, rawData, (uint)usage);
            Usage = usage;
        }
        public void Bind()
        {
            if (!IsCreated) return;
            OpenGL.BindBuffer((uint)Target, bufferObject);
        }
        public void Bind(OpenGL gl)
        {
            if (!IsCreated) return;
            gl.BindBuffer((uint)Target, bufferObject);
        }
        public void Unbind()
        {
            OpenGL.BindBuffer((uint)Target, 0);
        }
        public void Unbind(OpenGL gl)
        {
            gl.BindBuffer((uint)Target, 0);
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
        ~IndexBuffer()
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