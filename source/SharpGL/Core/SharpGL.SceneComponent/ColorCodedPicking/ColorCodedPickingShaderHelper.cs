using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// Helps to get shader's source code for color coded picking.
    /// </summary>
    public sealed class ColorCodedPickingShaderHelper
    {

        /// <summary>
        /// vertex shader's cache.
        /// </summary>
        static string vertexShader = null;

        /// <summary>
        /// fragmente shader's cache.
        /// </summary>
        static string fragmentShader = null;

        /// <summary>
        /// Gets shader's source code for color coded picking.
        /// </summary>
        /// <param name="shaderType"></param>
        /// <returns></returns>
        public static string GetShaderSource(ShaderTypes shaderType)
        {
            string result = string.Empty;

            switch (shaderType)
            {
                case ShaderTypes.VertexShader:
                    if (vertexShader == null)
                    {
                        vertexShader = ManifestResourceLoader.LoadTextFile(@"ColorCodedPicking\PickingShader.vert");
                    }
                    result = vertexShader;
                    break;
                case ShaderTypes.FragmentShader:
                    if (fragmentShader == null)
                    {
                        fragmentShader = ManifestResourceLoader.LoadTextFile(@"ColorCodedPicking\PickingShader.frag");
                    }
                    result = fragmentShader;
                    break;
                default:
                    throw new NotImplementedException();
            }

            return result;
        }

        public enum ShaderTypes
        {
            VertexShader,
            FragmentShader,
        }

    }

}
