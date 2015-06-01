using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// interactive with mouse and transform model.
    /// </summary>
    public interface IMouseTransform : IMouseLinearTransform, ITranslation
    {
    }
}
