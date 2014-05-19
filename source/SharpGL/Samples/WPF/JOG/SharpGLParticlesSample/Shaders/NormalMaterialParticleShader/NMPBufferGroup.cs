using GlmNet;
using SharpGL;
using SharpGL.SceneGraph.JOG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGLParticlesSample
{
    public class NMPBufferGroup
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
        public uint Normal { get; set; }
        public uint TCol1 { get; set; }
        public uint TCol2 { get; set; }
        public uint TCol3 { get; set; }
        public uint TCol4 { get; set; }
        public uint AmbientMaterial { get; set; }
        public uint DiffuseMaterial { get; set; }
        public uint SpecularMaterial { get; set; }
        public uint ShininessValue { get; set; }


        public int ParticleCount { get; set; }
        public int IndicesCount { get; set; }
        #endregion properties

        #region events
        #endregion events

        #region constructors
        public NMPBufferGroup(OpenGL gl)
        {
            CreateBufferIds(gl);
        }
        #endregion constructors

        private void CreateBufferIds(OpenGL gl)
        {
            var amount = 11;
            uint[] ids = new uint[amount];
            gl.GenBuffers(amount, ids);
            Position = ids[0];
            Normal = ids[1];
            TCol1 = ids[2];
            TCol2 = ids[3];
            TCol3 = ids[4];
            TCol4 = ids[5];
            AmbientMaterial = ids[6];
            DiffuseMaterial = ids[7];
            SpecularMaterial = ids[8];
            ShininessValue = ids[9];
            Ibo = ids[10];
        }

        public void BufferData(OpenGL gl,
            uint[] indices, float[] vertices, float[] normals, 
            mat4[] transformations, Material[] materials)
        {
            // Use the amount of transformations to decide how many particles of this object should be drawn.
            ParticleCount = transformations.Length;
            IndicesCount = indices.Length;

            var tColSize = 4 * transformations.Length;
            var color3Size = 3 * materials.Length;
            float[] tCol1 = new float[tColSize],
                tCol2 = new float[tColSize],
                tCol3 = new float[tColSize],
                tCol4 = new float[tColSize],
                ambs = new float[color3Size],
                diffs = new float[color3Size],
                specs = new float[color3Size],
                shinis = new float[materials.Length];

            // Convert transformations to float arrays.
            var newPos = 0;
            foreach (var trans in transformations)
            {
                for (int i = 0; i < 4; i++)
                {
                    tCol1[newPos] = trans[i][0];
                    tCol2[newPos] = trans[i][1];
                    tCol3[newPos] = trans[i][2];
                    tCol4[newPos] = trans[i][3];
                    newPos++;
                }
            }

            // Convert materials to float arrays.
            newPos = 0;
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

            // Set buffer data.
            BufferData(gl, indices, vertices, normals,
                tCol1, tCol2, tCol3, tCol4,
                ambs, diffs, specs, shinis);
        }
        public void BufferData(OpenGL gl,
            uint[] indices, float[] vertices, float[] normals, 
            float[] tCol1, float[] tCol2, float[] tCol3, float[] tCol4,
            float[] ambs, float[] diffs, float[] specs, float[] shinis)
        {
            gl.BindBuffer(_iboTarget, Ibo);
            gl.BufferData(_iboTarget, indices, _usage);

            gl.BindBuffer(_vboTarget, Position);
            gl.BufferData(_vboTarget, vertices, _usage);

            gl.BindBuffer(_vboTarget, Normal);
            gl.BufferData(_vboTarget, normals, _usage);

            gl.BindBuffer(_vboTarget, TCol1);
            gl.BufferData(_vboTarget, tCol1, _usage);

            gl.BindBuffer(_vboTarget, TCol2);
            gl.BufferData(_vboTarget, tCol2, _usage);

            gl.BindBuffer(_vboTarget, TCol3);
            gl.BufferData(_vboTarget, tCol3, _usage);

            gl.BindBuffer(_vboTarget, TCol4);
            gl.BufferData(_vboTarget, tCol4, _usage);

            gl.BindBuffer(_vboTarget, AmbientMaterial);
            gl.BufferData(_vboTarget, ambs, _usage);

            gl.BindBuffer(_vboTarget, DiffuseMaterial);
            gl.BufferData(_vboTarget, diffs, _usage);

            gl.BindBuffer(_vboTarget, SpecularMaterial);
            gl.BufferData(_vboTarget, specs, _usage);

            gl.BindBuffer(_vboTarget, ShininessValue);
            gl.BufferData(_vboTarget, shinis, _usage);
        
        }


        public void PrepareVAO(OpenGL gl, NormalMaterialParticleProgram program)
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

        public void BindVBOs(OpenGL gl, NormalMaterialParticleProgram program)
        {
            var attribPos = program.Attribs["Position"];
            gl.BindBuffer(_vboTarget, Position);
            gl.VertexAttribPointer(attribPos, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
            gl.EnableVertexAttribArray(attribPos);

            attribPos = program.Attribs["Normal"];
            gl.BindBuffer(_vboTarget, Normal);
            gl.VertexAttribPointer(attribPos, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
            gl.EnableVertexAttribArray(attribPos);

            attribPos = program.Attribs["TCol1"];
            gl.BindBuffer(_vboTarget, TCol1);
            gl.VertexAttribPointer(attribPos, 4, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
            gl.VertexAttribDivisor(attribPos, 1);
            gl.EnableVertexAttribArray(attribPos);

            attribPos = program.Attribs["TCol2"];
            gl.BindBuffer(_vboTarget, TCol2);
            gl.VertexAttribPointer(attribPos, 4, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
            gl.VertexAttribDivisor(attribPos, 1);
            gl.EnableVertexAttribArray(attribPos);

            attribPos = program.Attribs["TCol3"];
            gl.BindBuffer(_vboTarget, TCol3);
            gl.VertexAttribPointer(attribPos, 4, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
            gl.VertexAttribDivisor(attribPos, 1);
            gl.EnableVertexAttribArray(attribPos);

            attribPos = program.Attribs["TCol4"];
            gl.BindBuffer(_vboTarget, TCol4);
            gl.VertexAttribPointer(attribPos, 4, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
            gl.VertexAttribDivisor(attribPos, 1);
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

    }
}
