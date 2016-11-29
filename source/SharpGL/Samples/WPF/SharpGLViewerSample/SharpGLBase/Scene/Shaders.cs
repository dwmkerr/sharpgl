using SharpGLBase.Shaders;
using SharpGL;
using SharpGL.Shaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGLBase.Shaders.Parameters;

namespace SharpGLBase.Scene
{
    /// <summary>
    /// 
    /// </summary>
    public static class Shaders
    {
        #region fields
        //  We're going to specify the attribute locations for the position and normal, 
        //  so that we can force both shaders to explicitly have the same locations.
        const uint _positionAttribute = 0;
        const uint _normalAttribute = 1;
        static Dictionary<uint, string> _attributeLocations;

        //  The shaders we use.
        static Dictionary<string, ExtShaderProgram> _allShaders = new Dictionary<string, ExtShaderProgram>();

        #endregion fields

        #region properties
        /// <summary>
        /// Contains all the initialized named shaders.
        /// </summary>
        public static Dictionary<string, ExtShaderProgram> AllShaders
        {
            get { return _allShaders; }
            set { _allShaders = value; }
        }
        #endregion properties

        /// <summary>
        /// Static constructor.
        /// </summary>
        static Shaders()
        {
            _attributeLocations = new Dictionary<uint, string>
            {
                {_positionAttribute, "Position"},
                {_normalAttribute, "Normal"},
            };
        }

        public static ExtShaderProgram LoadPerPixelShader(OpenGL gl)
        {
            return CreateShader(gl, new PerPixelParameters(), @"Shaders\PerPixel.vert", @"Shaders\PerPixel.frag");
        }
        public static ExtShaderProgram LoadToonShader(OpenGL gl)
        {
            return CreateShader(gl, new ToonParameters(), @"Shaders\Toon.vert", @"Shaders\Toon.frag");
        }
        public static ExtShaderProgram LoadSimpleShader(OpenGL gl)
        {
            return CreateShader(gl, new SimpleShaderParameters(), @"Shaders\SimpleShader.vert", @"Shaders\SimpleShader.frag");
        }


        /// <summary>
        /// Create and add a new ShaderProgram from the given sources. 
        /// Call this function if you decide to add your own shaders.
        /// </summary>
        /// <param name="gl">The OpenGL</param>
        /// <param name="vertexShaderSource">The path to the vertex shader code.</param>
        /// <param name="fragmentShaderSource">The path to the fragment shader code.</param>
        /// <param name="shaderName">The name for the shader.</param>
        public static ExtShaderProgram CreateShader(OpenGL gl, IShaderParameterIds parameters, string vertexShaderSource, string fragmentShaderSource)
        {
            //  Create the per pixel shader.
            ShaderProgram shader = new ShaderProgram();
            shader.Create(gl,
                ManifestResourceLoader.LoadTextFile(vertexShaderSource),
                ManifestResourceLoader.LoadTextFile(fragmentShaderSource), _attributeLocations);

            return new ExtShaderProgram(shader, parameters);
        }
    }
}
