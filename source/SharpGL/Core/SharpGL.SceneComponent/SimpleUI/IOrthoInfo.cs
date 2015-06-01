using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent.SimpleUI
{
    public interface IOrthoInfo
    {
        Vertex LeftBottom { get; set; }
        Vertex RightBottom { get; set; }
        Vertex LeftTop { get; set; }
        Vertex RightTop { get; set; }
        float Width { get; set; }
        float Height { get; set; }
    }
}
