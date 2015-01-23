using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmNet;

namespace SharpGL.Tests.BasicShaders
{
    internal class TorusGeometry
    {
        public TorusGeometry(float outerRadius, float innerRadius, uint segments, uint sides)
        {

            vertices = new vec3[(segments + 1) * (sides + 1)];
            vec3 up = new vec3(0, 1, 0);
            float _2pi = (float)Math.PI * 2f;
            for (int seg = 0; seg <= segments; seg++)
            {
                int currSeg = seg == segments ? 0 : seg;

                float t1 = (float)currSeg / segments * _2pi;
                vec3 r1 = new vec3((float)Math.Cos(t1) * outerRadius, 0f, (float)Math.Sin(t1) * outerRadius);

                for (int side = 0; side <= sides; side++)
                {
                    int currSide = side == sides ? 0 : side;

                    vec3 normale = glm.cross(r1, up);
                    float t2 = (float)currSide / sides * _2pi;

                    var rotation = glm.rotate(mat4.identity(), -t1*_2pi/360.0f, up);
                    vec4 r2q = rotation * new vec4((float)Math.Sin(t2) * innerRadius, (float)Math.Cos(t2) * innerRadius, 0, 1);
                    vec3 r2 = new vec3(r2q.x, r2q.y, r2q.z);
                    vertices[side + seg * (sides + 1)] = r1 + r2;
                }
            }


            normals = new vec3[vertices.Length];
            for (int seg = 0; seg <= segments; seg++)
            {
                int currSeg = seg == segments ? 0 : seg;

                float t1 = (float)currSeg / segments * _2pi;
                vec3 r1 = new vec3((float)Math.Cos(t1) * outerRadius, 0f, (float)Math.Sin(t1) * outerRadius);

                for (int side = 0; side <= sides; side++)
                {
                    normals[side + seg * (sides + 1)] = glm.normalize(vertices[side + seg * (sides + 1)] - r1);
                }
            }



            int nbFaces = vertices.Length;
            int nbTriangles = nbFaces * 2;
            int nbIndexes = nbTriangles * 3;
            triangles = new uint[nbIndexes];

            int i = 0;
            for (uint seg = 0; seg <= segments; seg++)
            {
                for (uint side = 0; side <= sides - 1; side++)
                {
                    uint current = side + seg * (sides + 1);
                    uint next = side + (seg < (segments) ? (seg + 1) * (sides + 1) : 0);

                    if (i < triangles.Length - 6)
                    {
                        triangles[i++] = current;
                        triangles[i++] = next;
                        triangles[i++] = next + 1;

                        triangles[i++] = current;
                        triangles[i++] = next + 1;
                        triangles[i++] = current + 1;
                    }
                }
            }
        }

        public readonly vec3[] vertices;
        public readonly vec3[] normals;
        public readonly uint[] triangles;
    }
}
