using SharpGL.SceneGraph.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO
{
    public interface IFileModel
    {
        float[] Vertices { get; set; }
        float[] Indices { get; set; }
        float[] Normals { get; set; }
        Material[] Materials { get; set; }


        IFileModel LoadModel(string path);
    }
}
