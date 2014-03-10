using System.ComponentModel;
namespace ShaderBuilder
{
    public enum ShaderType
    {
        [Description("Fragment Shader")]
        FragmentShader,

        [Description("Vertex Shader")]
        VertexShader,

        [Description("Shader")]
        Shader
    }
}