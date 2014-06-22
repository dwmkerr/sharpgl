using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GlmNet;

namespace SharpGLHitTestSample
{
    /// <summary>
    /// A simple cube object 
    /// </summary>
    public class FlatShadedCube
    {
        #region cube data

        static readonly vec3[] _vertices = new vec3[]{ 
            new vec3(0.0f,0.0f,0.0f), new vec3(1.0f,0.0f,0.0f), new vec3(1.0f,1.0f,0.0f), new vec3(0.0f,1.0f,0.0f),
            new vec3(0.0f,0.0f,1.0f), new vec3(1.0f,0.0f,1.0f), new vec3(1.0f,1.0f,1.0f), new vec3(0.0f,1.0f,1.0f), 
            new vec3(0.0f,0.0f,0.0f), new vec3(0.0f,0.0f,1.0f), new vec3(0.0f,1.0f,1.0f), new vec3(0.0f,1.0f,0.0f), 
            new vec3(1.0f,0.0f,0.0f), new vec3(1.0f,0.0f,1.0f), new vec3(1.0f,1.0f,1.0f), new vec3(1.0f,1.0f,0.0f), 
            new vec3(0.0f,0.0f,0.0f), new vec3(1.0f,0.0f,0.0f), new vec3(1.0f,0.0f,1.0f), new vec3(0.0f,0.0f,1.0f), 
            new vec3(0.0f,1.0f,0.0f), new vec3(1.0f,1.0f,0.0f), new vec3(1.0f,1.0f,1.0f), new vec3(0.0f,1.0f,1.0f)
        };

        static readonly vec3[] _normals = new vec3[]{
            new vec3(0,0,-1),new vec3(0,0,-1),new vec3(0,0,-1),new vec3(0,0,-1),
            new vec3(0,0,1),new vec3(0,0,1),new vec3(0,0,1),new vec3(0,0,1),
            new vec3(-1,0,0),new vec3(-1,0,0),new vec3(-1,0,0),new vec3(-1,0,0),
            new vec3(1,0,0),new vec3(1,0,0),new vec3(1,0,0),new vec3(1,0,0),
            new vec3(0,-1,0),new vec3(0,-1,0),new vec3(0,-1,0),new vec3(0,-1,0),
            new vec3(0,1,0),new vec3(0,1,0),new vec3(0,1,0),new vec3(0,1,0),
        };

        static readonly uint[] _indices = new uint[]{
            1,2,0, 2,3,0,
            4,6,5, 4,7,6,
            8,10,9, 8,11,10,
            13,14,12, 14,15,12,
            16,18,17, 16,19,18,
            21,22,20, 22,23,20,
        };



        #endregion cube data

        public static vec3[] Normals
        {
            get { return FlatShadedCube._normals; }
        }

        public static vec3[] Vertices
        {
            get { return FlatShadedCube._vertices; }
        }

        public static uint[] Indices
        {
            get { return FlatShadedCube._indices; }
        } 

    }
}
