using SharpGLBase.Scene;
using SharpGL;
using SharpGL.SceneGraph.Assets;
using SharpGL.Shaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGLBase.Shaders.Parameters;
using GlmNet;

namespace SharpGLBase.Shaders
{
    /// <summary>
    /// An extended ShaderProgram. This includes a shadername 
    /// </summary>
    public class ExtShaderProgram : IDisposable
    {
        #region fields
        OpenGL _gl = null;
        #endregion fields

        #region properties
        /// <summary>
        /// The shader program.
        /// </summary>
        public ShaderProgram Program { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public IShaderParameterIds Parameters { get; private set; }

        #endregion properties

        public ExtShaderProgram(ShaderProgram program, IShaderParameterIds parameters)
        {
            Program = program;
            Parameters = parameters;
        }

        /// <summary>
        /// Applies the material to the shader if the "Parameters" are an implementation of "IMaterialShaderParameters".
        /// </summary>
        /// <param name="gl">The GL.</param>
        /// <param name="m">The material.</param>
        public void ApplyMaterial(OpenGL gl, Material m, bool throwException = true)
        {
            if (!typeof(IMaterialShaderParameters).IsAssignableFrom(Parameters.GetType()))
            {
                if (!throwException)
                    return;

                throw new InvalidCastException("'Parameters' does not implement 'IMaterialShaderParameters'." +
                    "This has to be implemented for this method to know how the shader expects it's parameter names.");
            }

            var s = Program;
            var prms = Parameters as IMaterialShaderParameters; 

            var amb = m.Ambient;
            var diff = m.Diffuse;
            var spec = m.Specular;
            var shini = m.Shininess;
            var emit = m.Emission;

            if (prms.AmbientId != null) 
                s.SetUniform3(gl, prms.AmbientId, amb.R / 255.0f, amb.G / 255.0f, amb.B / 255.0f);
            if (prms.DiffuseId != null) 
                s.SetUniform3(gl, prms.DiffuseId, diff.R / 255.0f, diff.G / 255.0f, diff.B / 255.0f);
            if (prms.SpecularId != null) 
                s.SetUniform3(gl, prms.SpecularId, spec.R / 255.0f, spec.G / 255.0f, spec.B / 255.0f);
            if (prms.ShininessId != null) 
                s.SetUniform1(gl, prms.ShininessId, shini);
            if (prms.EmissionId != null) 
                s.SetUniform3(gl, prms.EmissionId, emit.R / 255.0f, emit.G / 255.0f, emit.B / 255.0f);
        }

        /// <summary>
        /// Applies the Modelview-, projection- and normal matrix to the shaders if the "Parameters" are an implementation of "IMVPNParameters".
        /// </summary>
        /// <param name="gl">The GL.</param>
        /// <param name="modelview">The modelview class that contains the matrix.</param>
        /// <param name="projection">The projection class that contains the matrix.</param>
        /// <param name="normal">The normal class that contains the matrix.</param>
        public void ApplyMVPNMatrices(OpenGL gl, ModelView modelview, Projection projection, Normal normal, bool throwException = true)
        {
            if (!typeof(IMVPNParameters).IsAssignableFrom(Parameters.GetType()))
            {
                if (!throwException)
                    return;

                throw new InvalidCastException("'Parameters' does not implement 'IMVPNParameters'." + 
                    "This has to be implemented for this method to know how the shader expects it's parameter names.");
            }

            var prms = Parameters as IMVPNParameters;

            // Set the matrices.
            Program.SetUniformMatrix4(gl, prms.ProjectionMatrixId, projection.ToArray());
            Program.SetUniformMatrix4(gl, prms.ModelviewMatrixId, modelview.ToArray());
            Program.SetUniformMatrix3(gl, prms.NormalMatrixId, normal.ToArray());
        }

        /// <summary>
        /// Sets the light position to the shader if the "Parameters" are an implementation of "ISingleLightParameters"
        /// </summary>
        /// <param name="gl"></param>
        /// <param name="lightPosition"></param>
        public void SetLight(OpenGL gl, vec3 lightPosition, bool throwException = true)
        {
            if (!typeof(ISingleLightParameters).IsAssignableFrom(Parameters.GetType()))
            {
                if (!throwException)
                    return;


                throw new InvalidCastException("'Parameters' does not implement 'ILightingParameters'." +
                    "This has to be implemented for this method to know how the shader expects it's parameter names.");
            }

            var prms = Parameters as ISingleLightParameters;

            // Set the light position.
            if (prms.LightPositionId != null) 
                Program.SetUniform3(gl, prms.LightPositionId, lightPosition.x, lightPosition.y, lightPosition.z);
        }

        /// <summary>
        /// This method binds the shaderprogram to the GL, invokes the function and unbinds the shader.
        /// The function should contain everything that has to be loaded with this shader's settings.
        /// </summary>
        /// <param name="gl">The GL.</param>
        /// <param name="func">The code that has to be executing in this ShaderProgram. </param>
        public void UseProgram(OpenGL gl, Action func)
        {
            _gl = gl;

            // Bind the shader.
            Program.Bind(gl);
            

            // Invoke logic.
            func.Invoke();

            // Unbind the shader.
            Program.Unbind(gl);
        }

        /// <summary>
        /// Deletes the shaderprogram van the GL and clears the parameters of this object.
        /// </summary>
        public void Dispose()
        {
            Program.Delete(_gl);
            Program = null;
            Parameters = null;
        }
    }
}
