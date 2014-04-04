using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGLBase.Shaders.Parameters
{
    /// <summary>
    /// Implement this interface for shaders that have similar input parameters as SharpGL.SceneGraph.Assets.Material.
    /// </summary>
    public interface IMaterialShaderParameters : IShaderParameterIds
    {
        string LightPositionId { get; }
        string DiffuseId { get; }
        string AmbientId { get; }
        string SpecularId { get; }
        string ShininessId { get; }
        string EmissionId { get; }
    }
}
