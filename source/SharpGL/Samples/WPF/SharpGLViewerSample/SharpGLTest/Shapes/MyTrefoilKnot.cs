using GlmNet;
using SharpGLBase;
using SharpGLBase.Primitives;
using SharpGLBase.Primitives.ModelComponents;
using SharpGL;
using SharpGL.SceneGraph.Assets;
using SharpGL.VertexBuffers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using SharpGLBase.Shaders;

namespace SharpGLTest.Shapes
{
    /// <summary>
    /// The TrefoilKnot class creates geometry
    /// for a trefoil knot.
    /// </summary>
    public class MyTrefoilKnot: Model3DBase
    {
        #region fields
        /// The number of slices and stacks.
        private const uint _slices = 128;
        private const uint _stacks = 32;

        private static vec3[] _vertices;
        private static vec3[] _normals;
        private static ushort[] _indices;
        #endregion fields

        #region constructor
        static MyTrefoilKnot()
        {
            CreateIndexBuffer();
            CreateVertexNormalBuffer();
        }

        public MyTrefoilKnot(OpenGL gl)
        {
            Init(_vertices, _indices, _normals, gl);
            //base.BaseVertices = _vertices;
            //base.Normals = _normals;
            //base.Indices = _indices;
            //GenerateGeometry(gl);
        }

        #endregion constructor

        #region calculate trefoil knot
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
            dv.x = (float)(-1.5f * b * Math.Sin(1.5f * u) * Math.Cos(u) - (a + b * Math.Cos(1.5f * u)) * Math.Sin(u));
            dv.y = (float)(-1.5f * b * Math.Sin(1.5f * u) * Math.Sin(u) + (a + b * Math.Cos(1.5f * u)) * Math.Cos(u));
            dv.z = (float)(1.5f * c * Math.Cos(1.5f * u));

            vec3 q = glm.normalize(dv);
            vec3 qvn = glm.normalize(new vec3(q.y, -q.x, 0.0f));
            vec3 ww = glm.cross(q, qvn);

            vec3 range = new vec3();
            range.x = (float)(x + d * (qvn.x * Math.Cos(v) + ww.x * Math.Sin(v)));
            range.y = (float)(y + d * (qvn.y * Math.Cos(v) + ww.y * Math.Sin(v)));
            range.z = (float)(z + d * ww.z * Math.Sin(v));

            return range;
        }

        private static void CreateVertexNormalBuffer()
        {
            var vertexCount = _slices * _stacks;

            _vertices = new vec3[vertexCount];
            _normals = new vec3[vertexCount];

            int count = 0;

            float ds = 1.0f / _slices;
            float dt = 1.0f / _stacks;

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
                    _vertices[count] = p;
                    _normals[count] = n;
                    count++;
                }
            }
        }

        private static void CreateIndexBuffer()
        {
            const uint vertexCount = _slices * _stacks;
            const uint indexCount = vertexCount * 6;
            _indices = new ushort[indexCount];
            int count = 0;

            ushort n = 0;
            for (ushort i = 0; i < _slices; i++)
            {
                for (ushort j = 0; j < _stacks; j++)
                {
                    _indices[count++] = (ushort)(n + j);
                    _indices[count++] = (ushort)(n + (j + 1) % _stacks);
                    _indices[count++] = (ushort)((n + j + _stacks) % vertexCount);

                    _indices[count++] = (ushort)((n + j + _stacks) % vertexCount);
                    _indices[count++] = (ushort)((n + (j + 1) % _stacks) % vertexCount);
                    _indices[count++] = (ushort)((n + (j + 1) % _stacks + _stacks) % vertexCount);
                }

                n += (ushort)_stacks;
            }
        }
        #endregion calculate trefoil knot

        public override void Render(OpenGL gl, SharpGL.SceneGraph.Core.RenderMode renderMode, ExtShaderProgram shader = null)
        {
            if (base.Visible)
            {
                OpenGL.FrontFace(OpenGL.GL_CW);

                base.Bind();

                //  Draw the elements.
                OpenGL.DrawElements(GlDrawMode, Indices.Length, OpenGL.GL_UNSIGNED_SHORT, IntPtr.Zero);
            }
        }
    }
}
