using SharpGL;
using SharpGL.Shaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SharpGL.Shaders
{

    /// <summary>
    /// Provides a more restrictive ShaderProgram builder, 
    /// utilizes a modern .NET appoach and supports more than 2 kinds of shaders.
    /// </summary>
    public class ShaderProgramJOG
    {
        #region common program and shader enums
        /// <summary>
        /// Values that are available for the glGetProgram(...) call.
        /// </summary>
        public enum GetProgramActions : uint
        {
            GL_DELETE_STATUS = OpenGL.GL_DELETE_STATUS,
            GL_LINK_STATUS = OpenGL.GL_LINK_STATUS,
            GL_VALIDATE_STATUS = OpenGL.GL_VALIDATE_STATUS,
            GL_INFO_LOG_LENGTH = OpenGL.GL_INFO_LOG_LENGTH,
            GL_ATTACHED_SHADERS = OpenGL.GL_ATTACHED_SHADERS,
            GL_ACTIVE_ATOMIC_COUNTER_BUFFERS = OpenGL.GL_ACTIVE_ATOMIC_COUNTER_BUFFERS,
            GL_ACTIVE_ATTRIBUTES = OpenGL.GL_ACTIVE_ATTRIBUTES,
            GL_ACTIVE_ATTRIBUTE_MAX_LENGTH = OpenGL.GL_ACTIVE_ATTRIBUTE_MAX_LENGTH,
            GL_ACTIVE_UNIFORMS = OpenGL.GL_ACTIVE_UNIFORMS,
            GL_ACTIVE_UNIFORM_BLOCKS = OpenGL.GL_ACTIVE_UNIFORM_BLOCKS,
            GL_ACTIVE_UNIFORM_BLOCK_MAX_NAME_LENGTH = OpenGL.GL_ACTIVE_UNIFORM_BLOCK_MAX_NAME_LENGTH,
            GL_ACTIVE_UNIFORM_MAX_LENGTH = OpenGL.GL_ACTIVE_UNIFORM_MAX_LENGTH,
            GL_COMPUTE_WORK_GROUP_SIZE = OpenGL.GL_COMPUTE_WORK_GROUP_SIZE,
            GL_PROGRAM_BINARY_LENGTH = OpenGL.GL_PROGRAM_BINARY_LENGTH,
            GL_TRANSFORM_FEEDBACK_BUFFER_MODE = OpenGL.GL_TRANSFORM_FEEDBACK_BUFFER_MODE,
            GL_TRANSFORM_FEEDBACK_VARYINGS = OpenGL.GL_TRANSFORM_FEEDBACK_VARYINGS,
            GL_TRANSFORM_FEEDBACK_VARYING_MAX_LENGTH = OpenGL.GL_TRANSFORM_FEEDBACK_VARYING_MAX_LENGTH,
            GL_GEOMETRY_VERTICES_OUT = OpenGL.GL_GEOMETRY_VERTICES_OUT,
            GL_GEOMETRY_INPUT_TYPE = OpenGL.GL_GEOMETRY_INPUT_TYPE,
            GL_GEOMETRY_OUTPUT_TYPE = OpenGL.GL_GEOMETRY_OUTPUT_TYPE,
        }

        /// <summary>
        /// Values that are available for the glGetShader(...) call.
        /// </summary>
        public enum GetShaderActions : uint
        {
            GL_SHADER_TYPE = OpenGL.GL_SHADER_TYPE,
            GL_DELETE_STATUS = OpenGL.GL_DELETE_STATUS,
            GL_COMPILE_STATUS = OpenGL.GL_COMPILE_STATUS,
            GL_INFO_LOG_LENGTH = OpenGL.GL_INFO_LOG_LENGTH,
            GL_SHADER_SOURCE_LENGTH = OpenGL.GL_SHADER_SOURCE_LENGTH,
        }

        /// <summary>
        /// The available shader types.
        /// </summary>
        public enum ShaderTypes : uint
        {
            GL_COMPUTE_SHADER = OpenGL.GL_COMPUTE_SHADER,
            GL_VERTEX_SHADER = OpenGL.GL_VERTEX_SHADER,
            GL_TESS_CONTROL_SHADER = OpenGL.GL_TESS_CONTROL_SHADER,
            GL_TESS_EVALUATION_SHADER = OpenGL.GL_TESS_EVALUATION_SHADER,
            GL_GEOMETRY_SHADER = OpenGL.GL_GEOMETRY_SHADER,
            GL_FRAGMENT_SHADER = OpenGL.GL_FRAGMENT_SHADER,
        }
        #endregion common program and shader enums


        #region fields

        #endregion fields

        #region properties
        /// <summary>
        /// The id for this Program.
        /// </summary>
        public uint ShaderProgramId{get; protected set;}
        /// <summary>
        /// The ids of the shaders that are attached to this program.
        /// </summary>
        public Dictionary<ShaderTypes, uint> ShaderIds { get; protected set; }
        /// <summary>
        /// Attribute names and their positions in the shaders.
        /// </summary>
        public Dictionary<string, uint> Attribs { get; protected set; }
        /// <summary>
        /// The uniform names and their positions in the shaders.
        /// </summary>
        public Dictionary<string, int> Uniforms { get; protected set; }
        /// <summary>
        /// A value that will be true before invoking the Action in UseShader(...) and will be set to false after the Action finished.
        /// </summary>
        public bool ProgramIsBound { get; private set; }
        #endregion properties

        #region events
        #endregion events

        #region constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ShaderProgramJOG()
        { }

        /// <summary>
        /// Calls Initialize.
        /// </summary>
        /// <param name="gl"></param>
        /// <param name="shaderTypeAndCode"></param>
        /// <param name="attributeNames"></param>
        public ShaderProgramJOG(OpenGL gl, Dictionary<ShaderTypes, string> shaderTypeAndCode,
            IEnumerable<string> attributeNames, IEnumerable<string> uniformNames)
        {
            Initialize(gl, shaderTypeAndCode, attributeNames, uniformNames);
        }
        #endregion constructors

        /// <summary>
        /// Initialization of the ShaderProgram.
        /// Creates the Shaders and the program. 
        /// Attaches the shaders to the program.
        /// Retrieves the attribute and uniform positions that are utilized in this program.
        /// </summary>
        public void Initialize(OpenGL gl, Dictionary<ShaderTypes, string> shaderTypeAndCode,
            IEnumerable<string> attributeNames, IEnumerable<string> uniformNames)
        {
            ShaderIds = new Dictionary<ShaderTypes, uint>();
            Attribs = new Dictionary<string, uint>();
            Uniforms = new Dictionary<string, int>();

            CreateShaders(gl, shaderTypeAndCode);
            CreateProgram(gl);
            LinkProgram(gl);
            AddAttributeIds(gl, attributeNames);
            AddUniformIds(gl, uniformNames);

            AssertValid(gl);
        }

        /// <summary>
        /// Calls glUseProgram(ShaderProgramId) -> invokes executeInProgram-Action -> unbinds program using glUseProgram(0).
        /// </summary>
        /// <param name="gl"></param>
        /// <param name="executeInProgram">The logic to be executed for this shader program.</param>
        public void UseProgram(OpenGL gl, Action executeInProgram)
        {
            // Bind.
            gl.UseProgram(ShaderProgramId);

            // Make sure the program unbinds, even if a problem should occur.
            try
            {
                // Invoke logic.
                executeInProgram.Invoke();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Unbind.
                gl.UseProgram(0);
                ProgramIsBound = false;
            }
        }

        /// <summary>
        /// Deletes the shaders and the program and sets the ShaderProgramId to 0.
        /// </summary>
        /// <param name="gl"></param>
        /// <param name="deleteShaders"></param>
        public void DeleteProgram(OpenGL gl, bool deleteShaders)
        {
            while (ShaderIds.Count > 0)
            {
                DeleteShader(gl, ShaderIds.First().Key);
            }

            gl.DeleteProgram(ShaderProgramId);
            ShaderProgramId = 0;
        }

        /// <summary>
        /// Deletes the shader and removes it from the ShaderIds-Dictionary.
        /// </summary>
        /// <param name="gl"></param>
        /// <param name="type"></param>
        public void DeleteShader(OpenGL gl, ShaderTypes type)
        {
            var shader = ShaderIds[type];

            gl.DeleteShader(shader);

            ShaderIds.Remove(type);
        }

        #region init
        /// <summary>
        /// Creates the shaders.
        /// </summary>
        /// <param name="gl"></param>
        /// <param name="shaderTypeAndCode"></param>
        private void CreateShaders(OpenGL gl, Dictionary<ShaderTypes, string> shaderTypeAndCode)
        {
            foreach (var stc in shaderTypeAndCode)
            {
                CreateShader(gl, stc.Key, stc.Value);
            }
        }
        /// <summary>
        /// Creates a shader.
        /// </summary>
        /// <param name="gl"></param>
        /// <param name="type"></param>
        /// <param name="shaderCode"></param>
        private void CreateShader(OpenGL gl, ShaderTypes type, string shaderCode)
        {
            var id = gl.CreateShader((uint)type);
            gl.ShaderSource(id, shaderCode);
            gl.CompileShader(id);
            ShaderIds.Add(type, id);
        }
        /// <summary>
        /// Creates the program and attaches all the shaders to it.
        /// </summary>
        /// <param name="gl"></param>
        private void CreateProgram(OpenGL gl)
        {
            ShaderProgramId = gl.CreateProgram();

            foreach (var shader in ShaderIds)
            {
                gl.AttachShader(ShaderProgramId, shader.Value);
            }
        }

        /// <summary>
        /// Retrieve the attribute locations using the attribute names and add them to Attribs.
        /// </summary>
        /// <param name="gl"></param>
        /// <param name="attributeNames"></param>
        private void AddAttributeIds(OpenGL gl, IEnumerable<string> attributeNames)
        {
            if (Attribs == null)
                Attribs = new Dictionary<string, uint>();

            foreach (var name in attributeNames)
            {
                var location = gl.GetAttribLocation(ShaderProgramId, name);
                Attribs.Add(name, (uint)location);
            }
        }

        /// <summary>
        /// Retrieve the uniform locations using the uniform names and add them to Uniforms.
        /// </summary>
        /// <param name="gl"></param>
        /// <param name="uniformNames"></param>
        private void AddUniformIds(OpenGL gl, IEnumerable<string> uniformNames)
        {
            if (Uniforms == null)
                Uniforms = new Dictionary<string, int>();

            foreach (var name in uniformNames)
            {
                var location = gl.GetUniformLocation(ShaderProgramId, name);
                Uniforms.Add(name, location);
            }
        }
        /// <summary>
        /// Link the program.
        /// </summary>
        /// <param name="gl"></param>
        private void LinkProgram(OpenGL gl)
        {
            gl.LinkProgram(ShaderProgramId);
        }
        #endregion init


        #region Diagnostics
        public int[] GetProgramResult(OpenGL gl, GetProgramActions action, uint expectedLength)
        {
            int[] parameters = new int[expectedLength];
            gl.GetProgram(ShaderProgramId, (uint)action, parameters);
            return parameters;
        }
        public int[] GetShaderResult(OpenGL gl, ShaderTypes shaderType, GetShaderActions action, uint expectedLength)
        {
            int[] parameters = new int[expectedLength];
            gl.GetShader(ShaderIds[shaderType], (uint)action, parameters);
            return parameters;
        }


        public bool GetProgramLinkStatus(OpenGL gl)
        {
            var parameters = GetProgramResult(gl, GetProgramActions.GL_LINK_STATUS, 1);
            return parameters[0] == OpenGL.GL_TRUE;
        }

        public string GetProgramInfoLog(OpenGL gl)
        {
            //  Get the info log length.
            int[] infoLength = GetProgramResult(gl, GetProgramActions.GL_INFO_LOG_LENGTH, 1);
            int bufSize = infoLength[0];

            //  Get the compile info.
            StringBuilder il = new StringBuilder(bufSize);
            gl.GetProgramInfoLog(ShaderProgramId, bufSize, IntPtr.Zero, il);

            return il.ToString();
        }

        public bool GetShaderCompileStatus(OpenGL gl, ShaderTypes type)
        {
            var parameters = GetShaderResult(gl, type, GetShaderActions.GL_COMPILE_STATUS, 1);

            return parameters[0] == OpenGL.GL_TRUE;
        }

        public string GetShaderInfoLog(OpenGL gl, ShaderTypes type)
        {
            //  Get the info log length.
            int[] infoLength = GetShaderResult(gl, type, GetShaderActions.GL_INFO_LOG_LENGTH, 1);
            int bufSize = infoLength[0];

            //  Get the compile info.
            StringBuilder il = new StringBuilder(bufSize);
            gl.GetShaderInfoLog(ShaderIds[type], bufSize, IntPtr.Zero, il);

            return il.ToString();
        }


        public void AssertValid(OpenGL gl)
        {
            foreach (var shaderId in ShaderIds)
            {
                if (GetShaderCompileStatus(gl, shaderId.Key) == false)
                    throw new Exception(GetShaderInfoLog(gl, shaderId.Key));
            }

            if (GetProgramLinkStatus(gl) == false)
                throw new Exception(GetProgramInfoLog(gl));
        }

        #endregion Diagnostics
    }
}
