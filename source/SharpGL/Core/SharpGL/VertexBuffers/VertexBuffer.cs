using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.VertexBuffers
{
    public class VertexBuffer
    {
        public void Create(OpenGL gl)
        {
            //  Generate the vertex array.
            uint[] ids = new uint[1];
            gl.GenBuffers(1, ids);
            vertexBufferObject = ids[0];
        }

        public void SetData(OpenGL gl, uint attributeIndex, float[] rawData, bool isNormalised, int stride)
        {
            //  Set the data, specify its shape and assign it to a vertex attribute (so shaders can bind to it).
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, rawData, OpenGL.GL_STATIC_DRAW);
            gl.VertexAttribPointer(attributeIndex, stride, OpenGL.GL_FLOAT, isNormalised, 0, IntPtr.Zero);
            gl.EnableVertexAttribArray(attributeIndex);
        }

        public void Bind(OpenGL gl)
        {
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, vertexBufferObject);
        }

        public void Unbind(OpenGL gl)
        {
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
        }

        public bool IsCreated() { return vertexBufferObject != 0; }

        private uint vertexBufferObject;
    }

    public class IndexBuffer
    {
        public void Create(OpenGL gl)
        {
            //  Generate the vertex array.
            uint[] ids = new uint[1];
            gl.GenBuffers(1, ids);
            bufferObject = ids[0];
        }

        public void SetData(OpenGL gl, ushort[] rawData)
        {
            gl.BufferData(OpenGL.GL_ELEMENT_ARRAY_BUFFER, rawData, OpenGL.GL_STATIC_DRAW);
        }

        public void Bind(OpenGL gl)
        {
            gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, bufferObject);
        }

        public void Unbind(OpenGL gl)
        {
            gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, 0);
        }

        public bool IsCreated() { return bufferObject != 0; }

        private uint bufferObject;
    }
}
