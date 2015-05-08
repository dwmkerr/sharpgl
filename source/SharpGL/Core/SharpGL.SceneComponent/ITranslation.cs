using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// Move from (0, 0, 0) to <see cref="ITranslation.Translate"/>, and you are at model's center in world coordiante.
    /// </summary>
    public interface ITranslation
    {
        /// <summary>
        /// Move from (0, 0, 0) to Translate, and you are at model's center in world coordiante.
        /// </summary>
        Vertex Translate { get; set; }
    }
}
