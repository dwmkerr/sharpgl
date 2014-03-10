using System;
using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;
using SharpGL.SceneGraph.Core;

namespace SharpGL.SceneGraph
{
	/// <summary>
	/// The collections are a set of strongly typed collection classes, for use in
	/// SharpGL. They are strongly typed to keep the code efficient.
	/// </summary>
	namespace Collections
	{
        public static class VertexSearch
        {
            public static int Search(List<Vertex> vertices, int start, Vertex vertex, float accuracy)
            {
                //  Go through the verticies.
                for (int i = start; i < vertices.Count; i++)
                {
                    if ((vertices[i].X > (vertex.X - accuracy) && vertices[i].X < (vertex.X + accuracy))
                        && (vertices[i].Y > (vertex.Y - accuracy) && vertices[i].Y < (vertex.Y + accuracy))
                        && (vertices[i].Z > (vertex.Z - accuracy) && vertices[i].Z < (vertex.Z + accuracy)))
                        return i;
                }
                return -1;
            }
        }
	}
}
