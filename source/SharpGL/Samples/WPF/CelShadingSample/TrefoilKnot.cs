using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL.SceneGraph;
using SharpGL;
using System.Runtime.InteropServices;
using GlmNet;

namespace CelShadingSample
{
    //  Original Source: http://prideout.net/blog/?p=22

    /// <summary>
    /// The TrefoilKnot class creates geometry
    /// for a trefoil knot.
    /// </summary>
    public class TrefoilKnot
    {
        public void GenerateGeometry(OpenGL gl)
        {
            //  Generates the geometry for the trefoil knot.

            //  Create the vertices.
            vertices = new List<vec3>();

            //  Generate the vertices and normals.
            vertexAndNormalBuffer = CreateVertexNormalBuffer(gl);
            indexBuffer = CreateIndexBuffer(gl);
        }

        /// <summary>
        /// Evaluates the trefoil, providing the vertex at a given coordinate.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="t">The t.</param>
        /// <returns>The vertex at (s,t).</returns>
        private static vec3 EvaluateTrefoil(float s, float t)
        {
            const float TwoPi = (float)Math.PI * 2;

            float a = 0.5f;
            float b = 0.3f;
            float c = 0.5f;
            float d = 0.1f;
            float u = (1 - s) * 2 * TwoPi;
            float v = t * TwoPi;
            float r = (float)(a + b * Math.Cos(1.5f * u));
            float x = (float)(r * Math.Cos(u));
            float y = (float)(r * Math.Sin(u));
            float z = (float)(c * Math.Sin(1.5f * u));

            vec3 dv = new vec3();
            dv.x = (float) (-1.5f*b*Math.Sin(1.5f*u)*Math.Cos(u) - (a + b*Math.Cos(1.5f*u))*Math.Sin(u));
            dv.y = (float) (-1.5f*b*Math.Sin(1.5f*u)*Math.Sin(u) + (a + b*Math.Cos(1.5f*u))*Math.Cos(u)); 
            dv.z = (float) (1.5f*c*Math.Cos(1.5f*u));
            
            vec3 q = glm.normalize(dv);
            vec3 qvn = glm.normalize(new vec3(q.y, -q.x, 0.0f));
            vec3 ww =  glm.cross(q, qvn);

            vec3 range = new vec3();
            range.x = (float) (x + d*(qvn.x*Math.Cos(v) + ww.x*Math.Sin(v)));
            range.y = (float)(y + d * (qvn.y * Math.Cos(v) + ww.y * Math.Sin(v)));
            range.z = (float)(z + d * ww.z * Math.Sin(v));
            
            return range;
        }

        private uint CreateVertexNormalBuffer(OpenGL gl)
        {
            vec3[] verts = new vec3[VertexCount * 2];
            int count = 0;

            float ds = 1.0f / Slices;
            float dt = 1.0f / Stacks;

            // The upper bounds in these loops are tweaked to reduce the
            // chance of precision error causing an incorrect # of iterations.

            for (float s = 0; s < 1 - ds / 2; s += ds)
            {
                for (float t = 0; t < 1 - dt / 2; t += dt)
                {
                    const float E = 0.01f;
                    vec3 p = EvaluateTrefoil(s, t);
                    vec3 u = EvaluateTrefoil(s + E, t) - p;
                    vec3 v = EvaluateTrefoil(s, t + E) - p;
                    vec3 n = glm.normalize(glm.cross(u, v));
                    vertices.Add(p);
                    verts[count++] = p;
                    verts[count++] = n;
                }
            }
            
            
            uint[] buffers = new uint[1];
            gl.GenBuffers(1, buffers);
            uint handle = buffers[0];
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, handle);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, verts.SelectMany(v => v.to_array()).ToArray(), OpenGL.GL_STATIC_DRAW);
            
            return handle;
        }

        private uint CreateIndexBuffer(OpenGL gl)
        {
            indices = new ushort[IndexCount];
            int count = 0;

            ushort n = 0;
            for (ushort i = 0; i < Slices; i++)
            {
                for (ushort j = 0; j < Stacks; j++)
                {
                    indices[count++] = (ushort)(n + j);
                    indices[count++] = (ushort)(n + (j + 1) % Stacks);
                    indices[count++] = (ushort)((n + j + Stacks) % VertexCount);

                    indices[count++] = (ushort)((n + j + Stacks) % VertexCount);
                    indices[count++] = (ushort)((n + (j + 1) % Stacks) % VertexCount);
                    indices[count++] = (ushort)((n + (j + 1) % Stacks + Stacks) % VertexCount);
                }

                n += (ushort)Stacks;
            }
            
            uint[] buffers = new uint[1];
            gl.GenBuffers(1, buffers);
            uint handle = buffers[0];
            gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, handle);
            gl.BufferData(OpenGL.GL_ELEMENT_ARRAY_BUFFER, indices, OpenGL.GL_STATIC_DRAW);

            return handle;
        }

        private IList<vec3> vertices;

        public ushort[] indices;

        /// <summary>
        /// The number of slices.
        /// </summary>
        private uint slices = 128;

        /// <summary>
        /// The number of stacks.
        /// </summary>
        private uint stacks = 32;

        /// <summary>
        /// The vertex and normal buffer.
        /// </summary>
        private uint vertexAndNormalBuffer = 0;

        /// <summary>
        /// The index buffer.
        /// </summary>
        private uint indexBuffer = 0;

        /// <summary>
        /// Gets or sets the slices.
        /// </summary>
        /// <value>
        /// The slices.
        /// </value>
        public uint Slices
        {
            get { return slices; }
            set { slices = value; }
        }

        /// <summary>
        /// Gets or sets the stacks.
        /// </summary>
        /// <value>
        /// The stacks.
        /// </value>
        public uint Stacks
        {
            get { return stacks; }
            set { stacks = value; }
        }

        /// <summary>
        /// Gets the vertex count.
        /// </summary>
        /// <value>
        /// The vertex count.
        /// </value>
        public uint VertexCount
        {
            get {return Slices * Stacks;}
        }

        /// <summary>
        /// Gets the index count.
        /// </summary>
        /// <value>
        /// The index count.
        /// </value>
        public uint IndexCount
        {
            get { return VertexCount * 6; }
        }

        public uint VertexAndNormalBuffer
        {
            get { return vertexAndNormalBuffer; }
        }

        public uint IndexBuffer { get { return indexBuffer; } }

        /// <summary>
        /// Gets the vertices.
        /// </summary>
        /// <value>
        /// The vertices.
        /// </value>
        public IEnumerable<vec3> Vertices { get { return vertices; } } 
    }
}
