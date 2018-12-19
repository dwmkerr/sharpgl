using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// Rotation and Scale.
    /// </summary>
    public interface IMouseLinearTransform : IMouseRotation, IMouseScale
    {
        void TransformMatrix(OpenGL gl);
    }
}
