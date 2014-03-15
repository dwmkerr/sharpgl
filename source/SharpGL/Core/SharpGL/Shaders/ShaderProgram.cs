using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.Shaders
{
    public class ShaderProgram
    {
        private readonly Shader vertexShader = new Shader();
        private readonly Shader fragmentShader = new Shader();

        public void Create(OpenGL gl, string vertexShaderSource, string fragmentShaderSource)
        {
            //  Create the shaders.
            vertexShader.Create(gl, OpenGL.GL_VERTEX_SHADER, vertexShaderSource);
            fragmentShader.Create(gl, OpenGL.GL_FRAGMENT_SHADER, fragmentShaderSource);

            //  Create the program, attach the shaders then link the program.
            shaderProgramObject = gl.CreateProgram();
            gl.AttachShader(shaderProgramObject, vertexShader.ShaderObject);
            gl.AttachShader(shaderProgramObject, fragmentShader.ShaderObject);
            gl.LinkProgram(shaderProgramObject);

            //  Now that we've compiled and linked the shader, check it's link status. If it's not linked properly, we're
            //  going to throw an exception.
            if (GetLinkStatus(gl) == false)
            {
                throw new ShaderCompilationException(string.Format("Failed to link shader program with ID {0}.", shaderProgramObject), GetInfoLog(gl));
            }
        }

        public void Delete(OpenGL gl)
        {
            gl.DetachShader(shaderProgramObject, vertexShader.ShaderObject);
            gl.DetachShader(shaderProgramObject, fragmentShader.ShaderObject);
            vertexShader.Delete(gl);
            fragmentShader.Delete(gl);
            gl.DeleteProgram(shaderProgramObject);
            shaderProgramObject = 0;
        }

        public void BindAttributeLocation(OpenGL gl, uint location, string attribute)
        {
            gl.BindAttribLocation(shaderProgramObject, location, attribute);
        }

        public void Bind(OpenGL gl)
        {
            gl.UseProgram(shaderProgramObject);
        }

        public void Unbind(OpenGL gl)
        {
            gl.UseProgram(0);
        }

        public bool GetLinkStatus(OpenGL gl)
        {
            int[] parameters = new int[] { 0 };
            gl.GetProgram(shaderProgramObject, OpenGL.GL_LINK_STATUS, parameters);
            return parameters[0] == OpenGL.GL_TRUE;
        }

        public string GetInfoLog(OpenGL gl)
        {
            //  Get the info log length.
            int[] infoLength = new int[] { 0 };
            gl.GetProgram(shaderProgramObject, OpenGL.GL_INFO_LOG_LENGTH, infoLength);
            int bufSize = infoLength[0];

            //  Get the compile info.
            StringBuilder il = new StringBuilder(bufSize);
            gl.GetProgramInfoLog(shaderProgramObject, bufSize, IntPtr.Zero, il);

            return il.ToString();
        }

        public void AssertValid(OpenGL gl)
        {
            if (vertexShader.GetCompileStatus(gl) == false)
                throw new Exception(vertexShader.GetInfoLog(gl));
            if (fragmentShader.GetCompileStatus(gl) == false)
                throw new Exception(fragmentShader.GetInfoLog(gl));
            if (GetLinkStatus(gl) == false)
                throw new Exception(GetInfoLog(gl));
        }

        public int GetUniformLocation(OpenGL gl, string uniformName)
        {
            return gl.GetUniformLocation(shaderProgramObject, uniformName);
        }

        public void UniformMatrix(OpenGL gl, int uniformLocation, float[] matrix)
        {
            gl.UniformMatrix4(uniformLocation, 1, false, matrix);
        }

        private uint shaderProgramObject;
    }
}
