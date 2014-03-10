using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL.SceneGraph;
namespace ExtensionsSample
{
    /// <summary>
    /// Represents the torus object.
    /// </summary>
    public class Torus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Torus"/> class.
        /// </summary>
        public Torus()
        {
            InitialiseTorus();
        }
        
        /// <summary>
        /// Initialises the torus.
        /// </summary>
        /// <returns></returns>
        private bool InitialiseTorus()
        {
            //  Calculate the number of vertices and indices.
            numVertices=(torusPrecision+1)*(torusPrecision+1);
            numIndices=2*torusPrecision*torusPrecision*3;
        
            //  Create the vertices and indices.
            vertices = new TorusVertex[numVertices];
            indices = new uint[numIndices];

            //  Calculate the first ring - inner radius 4, outer radius 1.5
            for (int i = 0; i < torusPrecision + 1; i++)
            {
                vertices[i].position = (new Vertex(1.5f, 0.0f, 0.0f)).GetRotatedZ(i * 360.0f / torusPrecision) + new Vertex(4.0f, 0.0f, 0.0f);
                vertices[i].s = 0.0f;
                vertices[i].t = (float)i / torusPrecision;

                vertices[i].sTangent.Set(0.0f, 0.0f, -1.0f);
                vertices[i].tTangent = (new Vertex(0.0f, -1.0f, 0.0f)).GetRotatedZ(i * 360.0f / torusPrecision);
                vertices[i].normal = vertices[i].tTangent.VectorProduct(vertices[i].sTangent);
            }

            //  Rotate the first ring to get the other rings
            for(uint ring=1; ring<torusPrecision+1; ring++)
            {
                for (uint i = 0; i < torusPrecision + 1; i++)
                {
                    vertices[ring * (torusPrecision + 1) + i].position =
                        vertices[i].position.GetRotatedY(ring * 360.0f / torusPrecision);

                    vertices[ring * (torusPrecision + 1) + i].s = 2.0f * ring / torusPrecision;
                    vertices[ring * (torusPrecision + 1) + i].t = vertices[i].t;

                    vertices[ring * (torusPrecision + 1) + i].sTangent =
                        vertices[i].sTangent.GetRotatedY(ring * 360.0f / torusPrecision);
                    vertices[ring * (torusPrecision + 1) + i].tTangent =
                        vertices[i].tTangent.GetRotatedY(ring * 360.0f / torusPrecision);
                    vertices[ring * (torusPrecision + 1) + i].normal =
                        vertices[i].normal.GetRotatedY(ring * 360.0f / torusPrecision);
                }
            }

            //  Calculate the indices
            for (uint ring = 0; ring < torusPrecision; ring++)
            {
                for (uint i = 0; i < torusPrecision; i++)
                {
                    indices[((ring * torusPrecision + i) * 2) * 3 + 0] = ring * (torusPrecision + 1) + i;
                    indices[((ring * torusPrecision + i) * 2) * 3 + 1] = (ring + 1) * (torusPrecision + 1) + i;
                    indices[((ring * torusPrecision + i) * 2) * 3 + 2] = ring * (torusPrecision + 1) + i + 1;
                    indices[((ring * torusPrecision + i) * 2 + 1) * 3 + 0] = ring * (torusPrecision + 1) + i + 1;
                    indices[((ring * torusPrecision + i) * 2 + 1) * 3 + 1] = (ring + 1) * (torusPrecision + 1) + i;
                    indices[((ring * torusPrecision + i) * 2 + 1) * 3 + 2] = (ring + 1) * (torusPrecision + 1) + i + 1;
                }
            }
            
            //  OK, that's the torus done!
            return true;
        }

        /// <summary>
        /// The number of vertices.
        /// </summary>
        private uint numVertices = 0;
        
        /// <summary>
        /// The number of indices.
        /// </summary>
        private uint numIndices = 0;

        /// <summary>
        /// The torus indices.
        /// </summary>
        private uint[] indices;

        /// <summary>
        /// The torus vertices.
        /// </summary>
        private TorusVertex[] vertices;

        /// <summary>
        /// We define our torus to have a precision of 48. 
        /// This means that there are 48 vertices per ring when we construct it.
        /// </summary>
        private const uint torusPrecision = 48;

        /// <summary>
        /// Gets the num vertices.
        /// </summary>
        public uint NumVertices
        {
            get { return numVertices; }
        }

        /// <summary>
        /// Gets the num indices.
        /// </summary>
        public uint NumIndices
        {
            get { return numIndices; }
        }

        /// <summary>
        /// Gets the vertices.
        /// </summary>
        public TorusVertex[] Vertices
        {
            get { return vertices; }
        }

        /// <summary>
        /// Gets the indices.
        /// </summary>
        public uint[] Indices
        {
            get { return indices; }
        }
    }
}
