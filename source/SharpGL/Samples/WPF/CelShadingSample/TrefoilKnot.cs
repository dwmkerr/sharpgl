using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL.SceneGraph;
using SharpGL;
using System.Runtime.InteropServices;

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
            vertices = new List<Vertex>();

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
        private static Vertex EvaluateTrefoil(float s, float t)
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

            var dv = new Vertex
            {
                X = (float) (-1.5f*b*Math.Sin(1.5f*u)*Math.Cos(u) - (a + b*Math.Cos(1.5f*u))*Math.Sin(u)), 
                Y = (float) (-1.5f*b*Math.Sin(1.5f*u)*Math.Sin(u) + (a + b*Math.Cos(1.5f*u))*Math.Cos(u)), 
                Z = (float) (1.5f*c*Math.Cos(1.5f*u))
            };

            Vertex q = new Vertex(dv);
            q.Normalize();
            Vertex qvn = new Vertex(q.Y, -q.X, 0);
            qvn.Normalize();
            Vertex ww = q.VectorProduct(qvn);

            Vertex range = new Vertex()
            {
                X = (float) (x + d*(qvn.X*Math.Cos(v) + ww.X*Math.Sin(v))),
                Y = (float)(y + d * (qvn.Y * Math.Cos(v) + ww.Y * Math.Sin(v))),
                Z = (float)(z + d * ww.Z * Math.Sin(v))
            };

            return range;
        }

        private uint CreateVertexNormalBuffer(OpenGL gl)
        {
            Vertex[] verts = new Vertex[VertexCount * 2];
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
                    Vertex p = EvaluateTrefoil(s, t);
                    Vertex u = EvaluateTrefoil(s + E, t) - p;
                    Vertex v = EvaluateTrefoil(s, t + E) - p;
                    Vertex n = u.VectorProduct(v);
                    n.Normalize();
                    vertices.Add(p);
                    verts[count++] = p;
                    verts[count++] = new Vertex(0, 0, 0);//n);
                }
            }
            
            //  Pin the data.
            GCHandle vertsHandle = GCHandle.Alloc(verts, GCHandleType.Pinned);
            IntPtr vertsPtr = vertsHandle.AddrOfPinnedObject();
            var size = Marshal.SizeOf(typeof(Vertex)) * VertexCount;

            uint[] buffers = new uint[1];
            gl.GenBuffers(1, buffers);
            uint handle = buffers[0];
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, handle);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, (int)size, vertsPtr, OpenGL.GL_STATIC_DRAW);

            //  Free the data.
            vertsHandle.Free();

            return handle;
        }

        private uint CreateIndexBuffer(OpenGL gl)
        {
            ushort[] inds = new ushort[IndexCount];
            int count = 0;

            ushort n = 0;
            for (ushort i = 0; i < Slices; i++)
            {
                for (ushort j = 0; j < Stacks; j++)
                {
                    inds[count++] = (ushort)(n + j);
                    inds[count++] = (ushort)(n + (j + 1) % Stacks);
                    inds[count++] = (ushort)((n + j + Stacks) % VertexCount);

                    inds[count++] = (ushort)((n + j + Stacks) % VertexCount);
                    inds[count++] = (ushort)((n + (j + 1) % Stacks) % VertexCount);
                    inds[count++] = (ushort)((n + (j + 1) % Stacks + Stacks) % VertexCount);
                }

                n += (ushort)Stacks;
            }
            
            //  Pin the data.
            GCHandle indsHandle = GCHandle.Alloc(inds, GCHandleType.Pinned);
            IntPtr indsPtr = indsHandle.AddrOfPinnedObject();
            var size = Marshal.SizeOf(typeof(ushort)) * VertexCount;

            uint[] buffers = new uint[1];
            gl.GenBuffers(1, buffers);
            uint handle = buffers[0];
            gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, handle);
            gl.BufferData(OpenGL.GL_ELEMENT_ARRAY_BUFFER, (int)size, indsPtr, OpenGL.GL_STATIC_DRAW);

            //  Free the data.
            indsHandle.Free();

            return handle;
        }

        private IList<Vertex> vertices; 

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
        public IEnumerable<Vertex> Vertices { get { return vertices; } } 
    }
}
