using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// Rotate something(model, camera, etc) with mouse operation.
    /// </summary>
    public interface IMouseRotation
    {
        IScientificCamera Camera { get; set; }

        void MouseUp(int x, int y);

        void MouseMove(int x, int y);

        void SetBounds(int width, int height);

        void MouseDown(int x, int y);

        void ResetRotation();
    }
}
