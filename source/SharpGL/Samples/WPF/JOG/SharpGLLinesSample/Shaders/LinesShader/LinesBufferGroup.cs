using GlmNet;
using SharpGL;
using SharpGL.SceneGraph.JOG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGLLinesSample
{
    public class LinesBufferGroup
    {
        #region fields
        private uint _usage = OpenGL.GL_STATIC_COPY;
        private uint _vboTarget = OpenGL.GL_ARRAY_BUFFER;
        private uint _iboTarget = OpenGL.GL_ELEMENT_ARRAY_BUFFER;

        #endregion fields

        #region properties
        public uint Vao { get; set; }
        public uint Ibo { get; set; }
        public uint Position { get; set; }
        public uint ColorValue { get; set; }


        public int IndicesCount { get; set; }
        public int VerticesCount { get; set; }

        public float LineWidth = 1;
        #endregion properties

        #region events
        #endregion events

        #region constructors
        public LinesBufferGroup(OpenGL gl)
        {
            CreateBufferIds(gl);
        }

        #endregion constructors

        private void CreateBufferIds(OpenGL gl)
        {
            var amount = 3;
            uint[] ids = new uint[amount];
            gl.GenBuffers(amount, ids);
            Position = ids[0];
            ColorValue = ids[1];
            Ibo = ids[2];
        }

        public void BufferData(OpenGL gl,
            uint[] indices, float[] vertices, ColorF colorValue)
        {
            if (indices != null)
                IndicesCount = indices.Length;
            else
                IndicesCount = 0;
            VerticesCount = vertices.Length;

            float[] colorsArr = new float[3];

            // Convert materials to float arrays.
            var newPos = 0;
            var argb = colorValue;
            for (int j = 0; j < 3; j++)
            {
                colorsArr[newPos] = argb[j+1];
                newPos++;
            }

            // Set buffer data.
            BufferData(gl, indices, vertices, colorsArr);

        }
        public void BufferData(OpenGL gl,
            uint[] indices, float[] vertices, float[] colors)
        {
            if (indices != null)
            {
                gl.BindBuffer(_iboTarget, Ibo);
                gl.BufferData(_iboTarget, indices, _usage);
            }

            gl.BindBuffer(_vboTarget, Position);
            gl.BufferData(_vboTarget, vertices, _usage);

            gl.BindBuffer(_vboTarget, ColorValue);
            gl.BufferData(_vboTarget, colors, _usage);

        }

        public void PrepareVAO(OpenGL gl, LinesProgram program)
        {
            var vertArrIds = new uint[1];
            gl.GenVertexArrays(1, vertArrIds);

            Vao = vertArrIds[0];
            gl.BindVertexArray(Vao);

            BindVBOs(gl, program);

            gl.EnableVertexAttribArray(0);
            gl.BindVertexArray(0);
        }

        public void BindVAO(OpenGL gl)
        {
            gl.BindVertexArray(Vao);
        }

        public void BindVBOs(OpenGL gl, LinesProgram program)
        {
            var attribPos = program.Attribs["Position"];
            gl.BindBuffer(_vboTarget, Position);
            gl.VertexAttribPointer(attribPos, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
            gl.EnableVertexAttribArray(attribPos);

            attribPos = program.Attribs["ColorValue"];
            gl.BindBuffer(_vboTarget, ColorValue);
            gl.VertexAttribPointer(attribPos, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
            gl.VertexAttribDivisor(attribPos, 1);
            gl.EnableVertexAttribArray(attribPos);

            if (IndicesCount > 0)
                gl.BindBuffer(_iboTarget, Ibo);

        }

    }
}
