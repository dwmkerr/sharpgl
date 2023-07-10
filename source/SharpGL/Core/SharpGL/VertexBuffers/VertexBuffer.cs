using SharpGL.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SharpGL.VertexBuffers
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Very useful reference for management of VBOs and VBAs:
    /// http://stackoverflow.com/questions/8704801/glvertexattribpointer-clarification
    /// </remarks>
    public class VertexBuffer : IDisposable
    {
        private uint bufferObject;
        /// <summary>
        /// Gets the vertex buffer object.
        /// </summary>
        public uint VertexBufferObject
        {
            get { return bufferObject; }
        }
        public OpenGL OpenGL { get; private set; }
        public BufferBindingTarget Target { get; private set; } = BufferBindingTarget.GL_ARRAY_BUFFER;
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
        public void SetData(float[] rawData, BufferUsage usage = BufferUsage.GL_STATIC_DRAW)
        {
            if (!IsCreated) return;
            //  Set the data, specify its shape and assign it to a vertex attribute (so shaders can bind to it).
            OpenGL.BufferData((uint)Target, rawData, (uint)usage);
            Usage = usage;
        }
        public void SetData(OpenGL gl, uint attributeIndex, float[] rawData, bool isNormalised, int stride, BufferUsage usage = BufferUsage.GL_STATIC_DRAW)
        {
            if (!IsCreated) return;
            //  Set the data, specify its shape and assign it to a vertex attribute (so shaders can bind to it).
            gl.BufferData((uint)Target, rawData, (uint)usage);
            gl.VertexAttribPointer(attributeIndex, stride, OpenGL.GL_FLOAT, isNormalised, 0, IntPtr.Zero);
            gl.EnableVertexAttribArray(attributeIndex);
        }
        public void SetVertexAttribute(uint attributeIndex,int size ,int stride,bool isNormalised,int pointer = 0)
        {
            OpenGL.VertexAttribPointer(attributeIndex, size, OpenGL.GL_FLOAT, isNormalised, stride, (IntPtr)pointer);
        }
        public void EnableVertexAttribute(uint attributeIndex)
        {
            OpenGL.EnableVertexAttribArray(attributeIndex);
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
        ~VertexBuffer()
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
