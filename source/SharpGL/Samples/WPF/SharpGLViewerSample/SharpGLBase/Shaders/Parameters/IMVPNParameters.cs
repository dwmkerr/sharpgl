using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGLBase.Shaders.Parameters
{
    /// <summary>
    /// Implement this interface for shaders that accept  Modelview-, Projection- and/or Normal matrices.
    /// </summary>
    interface IMVPNParameters
    {
        string ProjectionMatrixId { get; }
        string ModelviewMatrixId { get; }
        string NormalMatrixId { get; }
    }
}
