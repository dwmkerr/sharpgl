using GlmNet;
using SharpGL;
using SharpGL.SceneGraph.JOG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGLHitTestSample
{
    public class NMHTBufferGroup
    {
        #region fields
        private uint _usage = OpenGL.GL_STATIC_COPY;
        private uint _vboTarget = OpenGL.GL_ARRAY_BUFFER;
        private uint _iboTarget = OpenGL.GL_ELEMENT_ARRAY_BUFFER;

        #endregion fields

        #region properties
        /// <summary>
        /// Binding for displaying the data.
        /// </summary>
        public uint VaoNM { get; set; }
        /// <summary>
        /// Binding for the HitTest
        /// </summary>
        public uint VaoHT { get; set; }
        public uint Ibo { get; set; }
        public uint HTColorId { get; set; }
        public uint Position { get; set; }
        public uint Normal { get; set; }
        public uint AmbientMaterial { get; set; }
        public uint DiffuseMaterial { get; set; }
        public uint SpecularMaterial { get; set; }
        public uint ShininessValue { get; set; }


        public int IndicesCount { get; set; }
        #endregion properties

        #region events
        #endregion events

        #region constructors
        public NMHTBufferGroup(OpenGL gl)
        {
            CreateBufferIds(gl);
        }

        #endregion constructors

        private void CreateBufferIds(OpenGL gl)
        {
            var amount = 8;
            uint[] ids = new uint[amount];
            gl.GenBuffers(amount, ids);
            Position = ids[0];
            Normal = ids[1];
            AmbientMaterial = ids[2];
            DiffuseMaterial = ids[3];
            SpecularMaterial = ids[4];
            ShininessValue = ids[5];
            HTColorId = ids[6];
            Ibo = ids[7];
        }

        public void BufferData(OpenGL gl,
            uint[] indices, float[] vertices, float[] normals, Material[] materials)
        {
            IndicesCount = indices.Length;

            var color3Size = 3 * materials.Length;
            float[] ambs = new float[color3Size],
                diffs = new float[color3Size],
                specs = new float[color3Size],
                shinis = new float[materials.Length];

            // Convert materials to float arrays.
            var newPos = 0;
            for (int i = 0; i < materials.Length; i++)
            {
                var mat = materials[i];
                for (int j = 0; j < 3; j++)
                {
                    ambs[newPos] = mat.Ambient[j+1];
                    diffs[newPos] = mat.Diffuse[j+1];
                    specs[newPos] = mat.Specular[j+1];
                    newPos++;
                }
                shinis[i] = mat.Shininess;
            }

            // Because an Index Buffer Object ID is unique, we'll use this to generate a hit test color.
            var htColor = new ColorF(Ibo);
            var htColorID = htColor.ToRGB();

            // Set buffer data.
            BufferData(gl, indices, vertices, normals,
                ambs, diffs, specs, shinis, htColorID);

        }
        public void BufferData(OpenGL gl,
            uint[] indices, float[] vertices, float[] normals, 
            float[] ambs, float[] diffs, float[] specs, float[] shinis,
            float[] htColor)
        {
            gl.BindBuffer(_iboTarget, Ibo);
            gl.BufferData(_iboTarget, indices, _usage);

            gl.BindBuffer(_vboTarget, Position);
            gl.BufferData(_vboTarget, vertices, _usage);

            gl.BindBuffer(_vboTarget, Normal);
            gl.BufferData(_vboTarget, normals, _usage);

            gl.BindBuffer(_vboTarget, AmbientMaterial);
            gl.BufferData(_vboTarget, ambs, _usage);

            gl.BindBuffer(_vboTarget, DiffuseMaterial);
            gl.BufferData(_vboTarget, diffs, _usage);

            gl.BindBuffer(_vboTarget, SpecularMaterial);
            gl.BufferData(_vboTarget, specs, _usage);

            gl.BindBuffer(_vboTarget, ShininessValue);
            gl.BufferData(_vboTarget, shinis, _usage);

            gl.BindBuffer(_vboTarget, HTColorId);
            gl.BufferData(_vboTarget, htColor, _usage);

        }

        public void PrepareNMVAO(OpenGL gl, NormalMaterialProgram program)
        {
            var vertArrIds = new uint[1];
            gl.GenVertexArrays(1, vertArrIds);

            VaoNM = vertArrIds[0];
            gl.BindVertexArray(VaoNM);

            BindNMVBOs(gl, program);

            gl.EnableVertexAttribArray(0);
            gl.BindVertexArray(0);
        }
        public void PrepareHTVAO(OpenGL gl, HitTestProgram program)
        {
            var vertArrIds = new uint[1];
            gl.GenVertexArrays(1, vertArrIds);

            VaoHT = vertArrIds[0];
            gl.BindVertexArray(VaoHT);

            BindHTVBOs(gl, program);

            gl.EnableVertexAttribArray(0);
            gl.BindVertexArray(0);
        }

        public void BindNMVAO(OpenGL gl)
        {
            gl.BindVertexArray(VaoNM);
        }
        public void BindHTVAO(OpenGL gl)
        {
            gl.BindVertexArray(VaoHT);
        }

        public void BindNMVBOs(OpenGL gl, NormalMaterialProgram program)
        {
            var attribPos = program.Attribs["Position"];
            gl.BindBuffer(_vboTarget, Position);
            gl.VertexAttribPointer(attribPos, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
            gl.EnableVertexAttribArray(attribPos);

            attribPos = program.Attribs["Normal"];
            gl.BindBuffer(_vboTarget, Normal);
            gl.VertexAttribPointer(attribPos, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
            gl.EnableVertexAttribArray(attribPos);

            attribPos = program.Attribs["AmbientMaterial"];
            gl.BindBuffer(_vboTarget, AmbientMaterial);
            gl.VertexAttribPointer(attribPos, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
            gl.VertexAttribDivisor(attribPos, 1);
            gl.EnableVertexAttribArray(attribPos);

            attribPos = program.Attribs["DiffuseMaterial"];
            gl.BindBuffer(_vboTarget, DiffuseMaterial);
            gl.VertexAttribPointer(attribPos, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
            gl.VertexAttribDivisor(attribPos, 1);
            gl.EnableVertexAttribArray(attribPos);

            attribPos = program.Attribs["SpecularMaterial"];
            gl.BindBuffer(_vboTarget, SpecularMaterial);
            gl.VertexAttribPointer(attribPos, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
            gl.VertexAttribDivisor(attribPos, 1);
            gl.EnableVertexAttribArray(attribPos);

            attribPos = program.Attribs["ShininessValue"];
            gl.BindBuffer(_vboTarget, ShininessValue);
            gl.VertexAttribPointer(attribPos, 1, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
            gl.VertexAttribDivisor(attribPos, 1);
            gl.EnableVertexAttribArray(attribPos);

            gl.BindBuffer(_iboTarget, Ibo);

        }

        public void BindHTVBOs(OpenGL gl, HitTestProgram program)
        {
            var attribPos = program.Attribs["Position"];
            gl.BindBuffer(_vboTarget, Position);
            gl.VertexAttribPointer(attribPos, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
            gl.EnableVertexAttribArray(attribPos);

            attribPos = program.Attribs["HTColorId"];
            gl.BindBuffer(_vboTarget, HTColorId);
            gl.VertexAttribPointer(attribPos, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
            gl.VertexAttribDivisor(attribPos, 1);
            gl.EnableVertexAttribArray(attribPos);

            gl.BindBuffer(_iboTarget, Ibo);
        }
    }
}
