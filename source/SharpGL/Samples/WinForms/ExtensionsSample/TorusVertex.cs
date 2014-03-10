using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL.SceneGraph;

namespace ExtensionsSample
{
    /// <summary>
    /// The torus vertex structure.
    /// </summary>
    public struct TorusVertex
    {
        //  Offset: 0
        public Vertex position;
        
        //  Offset: 12
        public float s;

        //  Offset: 16
        public float t;

        //  Offset 20
        public Vertex sTangent;
        
        //  Offset 32
        public Vertex tTangent;
        
        //  Offset 44
        public Vertex normal;
        
        //  Offset 56
        public Vertex tangentSpaceLight;
    }
}
