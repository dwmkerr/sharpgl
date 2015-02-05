using System;
using System.Text;

namespace SharpGL
{
// ReSharper disable InconsistentNaming

    public partial class OpenGL
    {
        #region ARB_get_program_binary

        private delegate void glGetProgramBinary(uint program, int bufSize, out int length, out uint binaryFormat, IntPtr binary);
        private delegate void glProgramBinary(uint program, uint binaryFormat, IntPtr rbinary, int length);
        private delegate void glProgramParameteri(uint program, uint pname, int value);

        /// <summary>
        /// Return a binary representation of a program object's compiled and linked executable source.
        /// </summary>
        /// <param name="program">Specifies the name of a program object whose binary representation to retrieve.</param>
        /// <param name="bufSize">Specifies the size of the buffer whose address is given by binary.</param>
        /// <param name="length">Specifies the address of a variable to receive the number of bytes written into binary.</param>
        /// <param name="binaryFormat">Specifies the address of a variable to receive a token indicating the format of the binary data returned by the GL.</param>
        /// <param name="binary">Specifies the address an array into which the GL will return program's binary representation.</param>
        public void GetProgramBinary(uint program, int bufSize, out int length, out uint binaryFormat, IntPtr binary)
        {
            GetDelegateFor<glGetProgramBinary>()(program, bufSize, out length, out binaryFormat, binary);
        }

        /// <summary>
        /// Load a program object with a program binary.
        /// </summary>
        /// <param name="program">Specifies the name of a program object into which to load a program binary.</param>
        /// <param name="binaryFormat">Specifies the format of the binary data in binary.</param>
        /// <param name="rbinary">Specifies the address of an array containing the binary to be loaded into program.</param>
        /// <param name="length">Specifies the number of bytes contained in binary.</param>
        public void ProgramBinary(uint program, uint binaryFormat, IntPtr rbinary, int length)
        {
            GetDelegateFor<glProgramBinary>()(program, binaryFormat, rbinary, length);
        }

        /// <summary>
        /// Specify a parameter for a program object.
        /// </summary>
        /// <param name="program">Specifies the name of a program object whose parameter to modify.</param>
        /// <param name="pname">Specifies the name of the parameter to modify.</param>
        /// <param name="value">Specifies the new value of the parameter specified by pname for program.</param>
        public void ProgramParameter(uint program, uint pname, int value)
        {
            GetDelegateFor<glProgramParameteri>()(program, pname, value);
        }

        public const uint GL_PROGRAM_BINARY_RETRIEVABLE_HINT = 0x8257;
        public const uint PROGRAM_BINARY_LENGTH = 0x8741;
        public const uint NUM_PROGRAM_BINARY_FORMATS = 0x87FE;
        public const uint PROGRAM_BINARY_FORMATS = 0x87FF;

        #endregion

        #region ARB_separate_shader_objects

        private delegate void glUseProgramStages(uint pipeline, uint stages, uint program);
        private delegate void glActiveShaderProgram(uint pipeline, uint program);
        private delegate uint glCreateShaderProgramv(uint type, int count, string[] strings);
        private delegate void glBindProgramPipeline(uint pipeline);
        private delegate void glDeleteProgramPipelines(int n, uint[] pipelines);
        private delegate void glGenProgramPipelines(int n, uint[] pipelines);
        private delegate bool glIsProgramPipeline(uint pipeline);
        private delegate void glGetProgramPipelineiv(uint pipeline, uint pname, int[] @params);
        private delegate void glProgramUniform1i(uint program, int location, int x);
        private delegate void glProgramUniform2i(uint program, int location, int x, int y);
        private delegate void glProgramUniform3i(uint program, int location, int x, int y, int z);
        private delegate void glProgramUniform4i(uint program, int location, int x, int y, int z, int w);
        private delegate void glProgramUniform1ui(uint program, int location, uint x);
        private delegate void glProgramUniform2ui(uint program, int location, uint x, uint y);
        private delegate void glProgramUniform3ui(uint program, int location, uint x, uint y, uint z);
        private delegate void glProgramUniform4ui(uint program, int location, uint x, uint y, uint z, uint w);
        private delegate void glProgramUniform1f(uint program, int location, float x);
        private delegate void glProgramUniform2f(uint program, int location, float x, float y);
        private delegate void glProgramUniform3f(uint program, int location, float x, float y, float z);
        private delegate void glProgramUniform4f(uint program, int location, float x, float y, float z, float w);
        private delegate void glProgramUniform1d(uint program, int location, double x);
        private delegate void glProgramUniform2d(uint program, int location, double x, double y);
        private delegate void glProgramUniform3d(uint program, int location, double x, double y, double z);
        private delegate void glProgramUniform4d(uint program, int location, double x, double y, double z, double w);
        private delegate void glProgramUniform1iv(uint program, int location, int count, int[] value);
        private delegate void glProgramUniform2iv(uint program, int location, int count, int[] value);
        private delegate void glProgramUniform3iv(uint program, int location, int count, int[] value);
        private delegate void glProgramUniform4iv(uint program, int location, int count, int[] value);
        private delegate void glProgramUniform1uiv(uint program, int location, int count, uint[] value);
        private delegate void glProgramUniform2uiv(uint program, int location, int count, uint[] value);
        private delegate void glProgramUniform3uiv(uint program, int location, int count, uint[] value);
        private delegate void glProgramUniform4uiv(uint program, int location, int count, uint[] value);
        private delegate void glProgramUniform1fv(uint program, int location, int count, float[] value);
        private delegate void glProgramUniform2fv(uint program, int location, int count, float[] value);
        private delegate void glProgramUniform3fv(uint program, int location, int count, float[] value);
        private delegate void glProgramUniform4fv(uint program, int location, int count, float[] value);
        private delegate void glProgramUniform1dv(uint program, int location, int count, double[] value);
        private delegate void glProgramUniform2dv(uint program, int location, int count, double[] value);
        private delegate void glProgramUniform3dv(uint program, int location, int count, double[] value);
        private delegate void glProgramUniform4dv(uint program, int location, int count, double[] value);
        private delegate void glProgramUniformMatrix2fv(uint program, int location, int count, bool transpose, float[] value);
        private delegate void glProgramUniformMatrix3fv(uint program, int location, int count, bool transpose, float[] value);
        private delegate void glProgramUniformMatrix4fv(uint program, int location, int count, bool transpose, float[] value);
        private delegate void glProgramUniformMatrix2dv(uint program, int location, int count, bool transpose, double[] value);
        private delegate void glProgramUniformMatrix3dv(uint program, int location, int count, bool transpose, double[] value);
        private delegate void glProgramUniformMatrix4dv(uint program, int location, int count, bool transpose, double[] value);
        private delegate void glProgramUniformMatrix2x3fv(uint program, int location, int count, bool transpose, float[] value);
        private delegate void glProgramUniformMatrix3x2fv(uint program, int location, int count, bool transpose, float[] value);
        private delegate void glProgramUniformMatrix2x4fv(uint program, int location, int count, bool transpose, float[] value);
        private delegate void glProgramUniformMatrix4x2fv(uint program, int location, int count, bool transpose, float[] value);
        private delegate void glProgramUniformMatrix3x4fv(uint program, int location, int count, bool transpose, float[] value);
        private delegate void glProgramUniformMatrix4x3fv(uint program, int location, int count, bool transpose, float[] value);
        private delegate void glProgramUniformMatrix2x3dv(uint program, int location, int count, bool transpose, double[] value);
        private delegate void glProgramUniformMatrix3x2dv(uint program, int location, int count, bool transpose, double[] value);
        private delegate void glProgramUniformMatrix2x4dv(uint program, int location, int count, bool transpose, double[] value);
        private delegate void glProgramUniformMatrix4x2dv(uint program, int location, int count, bool transpose, double[] value);
        private delegate void glProgramUniformMatrix3x4dv(uint program, int location, int count, bool transpose, double[] value);
        private delegate void glProgramUniformMatrix4x3dv(uint program, int location, int count, bool transpose, double[] value);
        private delegate void glValidateProgramPipeline(uint pipeline );
        private delegate void glGetProgramPipelineInfoLog(uint pipeline, int bufSize, out int length, StringBuilder infoLog);

        public const uint GL_VERTEX_SHADER_BIT = 0x00000001;
        public const uint GL_FRAGMENT_SHADER_BIT = 0x00000002;
        public const uint GL_GEOMETRY_SHADER_BIT = 0x00000004;
        public const uint GL_TESS_CONTROL_SHADER_BIT = 0x00000008;
        public const uint GL_TESS_EVALUATION_SHADER_BIT = 0x00000010;
        public const uint GL_ALL_SHADER_BITS = 0xFFFFFFFF;
        public const uint GL_PROGRAM_SEPARABLE = 0x8258;
        public const uint GL_ACTIVE_PROGRAM = 0x8259;
        public const uint GL_PROGRAM_PIPELINE_BINDING = 0x825A;

        /// <summary>
        /// Bind bind stages of a program object to a program pipeline.
        /// </summary>
        /// <param name="pipeline">Specifies the program pipeline object to which to bind stages from program.</param>
        /// <param name="stages">Specifies a set of program stages to bind to the program pipeline object.</param>
        /// <param name="program">Specifies the program object containing the shader executables to use in pipeline.</param>
       public void UseProgramStages(uint pipeline, uint stages, uint program)
       {
           GetDelegateFor<glUseProgramStages>()(pipeline, stages, program);
       }

       /// <summary>
       /// Set the active program object for a program pipeline object.
       /// </summary>
       /// <param name="pipeline">Specifies the program pipeline object to set the active program object for.</param>
       /// <param name="program">Specifies the program object to set as the active program pipeline object pipeline.</param>
       public void ActiveShaderProgram(uint pipeline, uint program)
       {
           GetDelegateFor<glActiveShaderProgram>()(pipeline, program); 
       }

       /// <summary>
       /// Create a stand-alone program from an array of null-terminated source code strings.
       /// </summary>
       /// <param name="type">Specifies the type of shader to create.</param>
       /// <param name="count">Specifies the number of source code strings in the array strings.</param>
       /// <param name="strings">Specifies the address of an array of pointers to source code strings from which to create the program object.</param>
       /// <returns></returns>
       public uint CreateShaderProgram(uint type, int count, string[] strings)
       {
           return GetDelegateFor<glCreateShaderProgramv>()(type, count, strings);   
       }

       /// <summary>
       /// Bind a program pipeline to the current context.
       /// </summary>
       /// <param name="pipeline">Specifies the name of the pipeline object to bind to the context.</param>
       public void BindProgramPipeline(uint pipeline)
       {
           GetDelegateFor<glBindProgramPipeline>()(pipeline);
       }

       /// <summary>
       /// Reserve program pipeline object names.
       /// </summary>
       /// <param name="n">Specifies the number of program pipeline object names to reserve.</param>
       /// <param name="pipelines">Specifies an array of into which the reserved names will be written.</param>
       public void DeleteProgramPipelines(int n, uint[] pipelines)
       {
           GetDelegateFor<glDeleteProgramPipelines>()(n, pipelines);
       }

       /// <summary>
       /// Gens the program pipelines.
       /// </summary>
       /// <param name="n">The n.</param>
       /// <param name="pipelines">The pipelines.</param>
       public void GenProgramPipelines(int n, uint[] pipelines)
       {
           GetDelegateFor<glGenProgramPipelines>()(n, pipelines);
       }

       /// <summary>
       /// Determine if a name corresponds to a program pipeline object.
       /// </summary>
       /// <param name="pipeline">Specifies a value that may be the name of a program pipeline object.</param>
       /// <returns>glIsProgramPipeline returns GL_TRUE if pipeline is currently the name of a program pipeline object. If pipeline is zero, or if pipeline is not the name of a program pipeline object, or if an error occurs, glIsProgramPipeline returns GL_FALSE. If pipeline is a name returned by glGenProgramPipelines, but that has not yet been bound through a call to glBindProgramPipeline, then the name is not a program pipeline object and glIsProgramPipeline returns GL_FALSE.</returns>
       public bool IsProgramPipeline(uint pipeline)
       {
           return GetDelegateFor<glIsProgramPipeline>()(pipeline);
       }

       /// <summary>
       /// Retrieve properties of a program pipeline object.
       /// </summary>
       /// <param name="pipeline">Specifies the name of a program pipeline object whose parameter retrieve.</param>
       /// <param name="pname">Specifies the name of the parameter to retrieve.</param>
       /// <param name="params">Specifies the address of a variable into which will be written the value or values of pname for pipeline.</param>
       public void GetProgramPipelineiv(uint pipeline, uint pname, int[] @params)
       {
           GetDelegateFor<glGetProgramPipelineiv>()(pipeline, pname, @params);
       }


       /// <summary>
       /// Specify the value of a uniform variable for a specified program object.
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
       /// <param name="x">Specifies the new values to be used for the specified uniform variable.</param>
       public void ProgramUniform(uint program, int location, int x)
       {
           GetDelegateFor<glProgramUniform1i>()(program, location, x);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object.
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
       /// <param name="x">Specifies the new values to be used for the specified uniform variable.</param>
       /// <param name="y">Specifies the new values to be used for the specified uniform variable.</param>
       public void ProgramUniform(uint program, int location, int x, int y)
       {
           GetDelegateFor<glProgramUniform2i>()(program, location, x, y);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object.
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
       /// <param name="x">Specifies the new values to be used for the specified uniform variable.</param>
       /// <param name="y">Specifies the new values to be used for the specified uniform variable.</param>
       /// <param name="z">Specifies the new values to be used for the specified uniform variable.</param>
       public void ProgramUniform(uint program, int location, int x, int y, int z)
       {
           GetDelegateFor<glProgramUniform3i>()(program, location, x, y, z);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object.
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
       /// <param name="x">Specifies the new values to be used for the specified uniform variable.</param>
       /// <param name="y">Specifies the new values to be used for the specified uniform variable.</param>
       /// <param name="z">Specifies the new values to be used for the specified uniform variable.</param>
       /// <param name="w">Specifies the new values to be used for the specified uniform variable.</param>
       public void ProgramUniform(uint program, int location, int x, int y, int z, int w)
       {
           GetDelegateFor<glProgramUniform4i>()(program, location, x, y, z, w);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object.
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
       /// <param name="x">Specifies the new values to be used for the specified uniform variable.</param>
       public void ProgramUniform(uint program, int location, uint x)
       {
           GetDelegateFor<glProgramUniform1ui>()(program, location, x);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object.
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
       /// <param name="x">Specifies the new values to be used for the specified uniform variable.</param>
       /// <param name="y">Specifies the new values to be used for the specified uniform variable.</param>
       public void ProgramUniform(uint program, int location, uint x, uint y)
       {
           GetDelegateFor<glProgramUniform2ui>()(program, location, x, y);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object.
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
       /// <param name="x">Specifies the new values to be used for the specified uniform variable.</param>
       /// <param name="y">Specifies the new values to be used for the specified uniform variable.</param>
       /// <param name="z">Specifies the new values to be used for the specified uniform variable.</param>
       public void ProgramUniform(uint program, int location, uint x, uint y, uint z)
       {
           GetDelegateFor<glProgramUniform3ui>()(program, location, x, y, z);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object.
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
       /// <param name="x">Specifies the new values to be used for the specified uniform variable.</param>
       /// <param name="y">Specifies the new values to be used for the specified uniform variable.</param>
       /// <param name="z">Specifies the new values to be used for the specified uniform variable.</param>
       /// <param name="w">Specifies the new values to be used for the specified uniform variable.</param>
       public void ProgramUniform(uint program, int location, uint x, uint y, uint z, uint w)
       {
           GetDelegateFor<glProgramUniform4ui>()(program, location, x, y, z, w);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object.
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
       /// <param name="x">Specifies the new values to be used for the specified uniform variable.</param>
       public void ProgramUniform(uint program, int location, float x)
       {
           GetDelegateFor<glProgramUniform1f>()(program, location, x);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object.
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
       /// <param name="x">Specifies the new values to be used for the specified uniform variable.</param>
       /// <param name="y">Specifies the new values to be used for the specified uniform variable.</param>
       public void ProgramUniform(uint program, int location, float x, float y)
       {
           GetDelegateFor<glProgramUniform2f>()(program, location, x, y);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object.
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
       /// <param name="x">Specifies the new values to be used for the specified uniform variable.</param>
       /// <param name="y">Specifies the new values to be used for the specified uniform variable.</param>
       /// <param name="z">Specifies the new values to be used for the specified uniform variable.</param>
       public void ProgramUniform(uint program, int location, float x, float y, float z)
       {
           GetDelegateFor<glProgramUniform3f>()(program, location, x, y, z);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object.
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
       /// <param name="x">Specifies the new values to be used for the specified uniform variable.</param>
       /// <param name="y">Specifies the new values to be used for the specified uniform variable.</param>
       /// <param name="z">Specifies the new values to be used for the specified uniform variable.</param>
       /// <param name="w">Specifies the new values to be used for the specified uniform variable.</param>
       public void ProgramUniform(uint program, int location, float x, float y, float z, float w)
       {
           GetDelegateFor<glProgramUniform4f>()(program, location, x, y, z, w);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object.
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
       /// <param name="x">Specifies the new values to be used for the specified uniform variable.</param>
       public void ProgramUniform(uint program, int location, double x)
       {
           GetDelegateFor<glProgramUniform1d>()(program, location, x);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object.
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
       /// <param name="x">Specifies the new values to be used for the specified uniform variable.</param>
       /// <param name="y">Specifies the new values to be used for the specified uniform variable.</param>
       public void ProgramUniform(uint program, int location, double x, double y)
       {
           GetDelegateFor<glProgramUniform2d>()(program, location, x, y);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object.
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
       /// <param name="x">Specifies the new values to be used for the specified uniform variable.</param>
       /// <param name="y">Specifies the new values to be used for the specified uniform variable.</param>
       /// <param name="z">Specifies the new values to be used for the specified uniform variable.</param>
       public void ProgramUniform(uint program, int location, double x, double y, double z)
       {
           GetDelegateFor<glProgramUniform3d>()(program, location, x, y, z);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object.
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
       /// <param name="x">Specifies the new values to be used for the specified uniform variable.</param>
       /// <param name="y">Specifies the new values to be used for the specified uniform variable.</param>
       /// <param name="z">Specifies the new values to be used for the specified uniform variable.</param>
       /// <param name="w">Specifies the new values to be used for the specified uniform variable.</param>
       public void ProgramUniform(uint program, int location, double x, double y, double z, double w)
       {
           GetDelegateFor<glProgramUniform4d>()(program, location, x, y, z, w);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object.
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform value to be modified.</param>
       /// <param name="count">Specifies the number of elements that are to be modified. This should be 1 if the targeted uniform variable is not an array, and 1 or more if it is an array.</param>
       /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
       public void ProgramUniform1(uint program, int location, int count, int[] value)
       {
           GetDelegateFor<glProgramUniform1iv>()(program, location, count, value);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object.
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform value to be modified.</param>
       /// <param name="count">Specifies the number of elements that are to be modified. This should be 1 if the targeted uniform variable is not an array, and 1 or more if it is an array.</param>
       /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
       public void ProgramUniform2(uint program, int location, int count, int[] value)
       {
           GetDelegateFor<glProgramUniform2iv>()(program, location, count, value);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object.
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform value to be modified.</param>
       /// <param name="count">Specifies the number of elements that are to be modified. This should be 1 if the targeted uniform variable is not an array, and 1 or more if it is an array.</param>
       /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
       public void ProgramUniform3(uint program, int location, int count, int[] value)
       {
           GetDelegateFor<glProgramUniform3iv>()(program, location, count, value);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object.
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform value to be modified.</param>
       /// <param name="count">Specifies the number of elements that are to be modified. This should be 1 if the targeted uniform variable is not an array, and 1 or more if it is an array.</param>
       /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
       public void ProgramUniform4(uint program, int location, int count, int[] value)
       {
           GetDelegateFor<glProgramUniform4iv>()(program, location, count, value);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object.
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform value to be modified.</param>
       /// <param name="count">Specifies the number of elements that are to be modified. This should be 1 if the targeted uniform variable is not an array, and 1 or more if it is an array.</param>
       /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
       public void ProgramUniform1(uint program, int location, int count, uint[] value)
       {
           GetDelegateFor<glProgramUniform1uiv>()(program, location, count, value);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object.
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform value to be modified.</param>
       /// <param name="count">Specifies the number of elements that are to be modified. This should be 1 if the targeted uniform variable is not an array, and 1 or more if it is an array.</param>
       /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
       public void ProgramUniform2(uint program, int location, int count, uint[] value)
       {
           GetDelegateFor<glProgramUniform2uiv>()(program, location, count, value);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object.
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform value to be modified.</param>
       /// <param name="count">Specifies the number of elements that are to be modified. This should be 1 if the targeted uniform variable is not an array, and 1 or more if it is an array.</param>
       /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
       public void ProgramUniform3(uint program, int location, int count, uint[] value)
       {
           GetDelegateFor<glProgramUniform3uiv>()(program, location, count, value);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object.
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform value to be modified.</param>
       /// <param name="count">Specifies the number of elements that are to be modified. This should be 1 if the targeted uniform variable is not an array, and 1 or more if it is an array.</param>
       /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
       public void ProgramUniform4(uint program, int location, int count, uint[] value)
       {
           GetDelegateFor<glProgramUniform4uiv>()(program, location, count, value);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object.
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform value to be modified.</param>
       /// <param name="count">Specifies the number of elements that are to be modified. This should be 1 if the targeted uniform variable is not an array, and 1 or more if it is an array.</param>
       /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
       public void ProgramUniform1(uint program, int location, int count, float[] value)
       {
           GetDelegateFor<glProgramUniform1fv>()(program, location, count, value);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object.
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform value to be modified.</param>
       /// <param name="count">Specifies the number of elements that are to be modified. This should be 1 if the targeted uniform variable is not an array, and 1 or more if it is an array.</param>
       /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
       public void ProgramUniform2(uint program, int location, int count, float[] value)
       {
           GetDelegateFor<glProgramUniform2fv>()(program, location, count, value);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object.
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform value to be modified.</param>
       /// <param name="count">Specifies the number of elements that are to be modified. This should be 1 if the targeted uniform variable is not an array, and 1 or more if it is an array.</param>
       /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
       public void ProgramUniform3(uint program, int location, int count, float[] value)
       {
           GetDelegateFor<glProgramUniform3fv>()(program, location, count, value);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object.
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform value to be modified.</param>
       /// <param name="count">Specifies the number of elements that are to be modified. This should be 1 if the targeted uniform variable is not an array, and 1 or more if it is an array.</param>
       /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
       public void ProgramUniform4(uint program, int location, int count, float[] value)
       {
           GetDelegateFor<glProgramUniform4fv>()(program, location, count, value);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object.
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform value to be modified.</param>
       /// <param name="count">Specifies the number of elements that are to be modified. This should be 1 if the targeted uniform variable is not an array, and 1 or more if it is an array.</param>
       /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
       public void ProgramUniform1(uint program, int location, int count, double[] value)
       {
           GetDelegateFor<glProgramUniform1dv>()(program, location, count, value);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object.
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform value to be modified.</param>
       /// <param name="count">Specifies the number of elements that are to be modified. This should be 1 if the targeted uniform variable is not an array, and 1 or more if it is an array.</param>
       /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
       public void ProgramUniform2(uint program, int location, int count, double[] value)
       {
           GetDelegateFor<glProgramUniform2dv>()(program, location, count, value);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object.
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform value to be modified.</param>
       /// <param name="count">Specifies the number of elements that are to be modified. This should be 1 if the targeted uniform variable is not an array, and 1 or more if it is an array.</param>
       /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
       public void ProgramUniform3(uint program, int location, int count, double[] value)
       {
           GetDelegateFor<glProgramUniform3dv>()(program, location, count, value);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object.
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform value to be modified.</param>
       /// <param name="count">Specifies the number of elements that are to be modified. This should be 1 if the targeted uniform variable is not an array, and 1 or more if it is an array.</param>
       /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
       public void ProgramUniform4(uint program, int location, int count, double[] value)
       {
           GetDelegateFor<glProgramUniform4dv>()(program, location, count, value);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform value to be modified.</param>
       /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
       /// <param name="transpose">Specifies whether to transpose the matrix as the values are loaded into the uniform variable.</param>
       /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
       public void ProgramUniformMatrix2(uint program, int location, int count, bool transpose, float[] value)
       {
           GetDelegateFor<glProgramUniformMatrix2fv>()(program, location, count, transpose, value);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform value to be modified.</param>
       /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
       /// <param name="transpose">Specifies whether to transpose the matrix as the values are loaded into the uniform variable.</param>
       /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
       public void ProgramUniformMatrix3(uint program, int location, int count, bool transpose, float[] value)
       {
           GetDelegateFor<glProgramUniformMatrix3fv>()(program, location, count, transpose, value);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform value to be modified.</param>
       /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
       /// <param name="transpose">Specifies whether to transpose the matrix as the values are loaded into the uniform variable.</param>
       /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
       public void ProgramUniformMatrix4(uint program, int location, int count, bool transpose, float[] value)
       {
           GetDelegateFor<glProgramUniformMatrix4fv>()(program, location, count, transpose, value);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform value to be modified.</param>
       /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
       /// <param name="transpose">Specifies whether to transpose the matrix as the values are loaded into the uniform variable.</param>
       /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
       public void ProgramUniformMatrix2(uint program, int location, int count, bool transpose, double[] value)
       {
           GetDelegateFor<glProgramUniformMatrix2dv>()(program, location, count, transpose, value);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform value to be modified.</param>
       /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
       /// <param name="transpose">Specifies whether to transpose the matrix as the values are loaded into the uniform variable.</param>
       /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
       public void ProgramUniformMatrix3(uint program, int location, int count, bool transpose, double[] value)
       {
           GetDelegateFor<glProgramUniformMatrix3dv>()(program, location, count, transpose, value);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform value to be modified.</param>
       /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
       /// <param name="transpose">Specifies whether to transpose the matrix as the values are loaded into the uniform variable.</param>
       /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
       public void ProgramUniformMatrix4(uint program, int location, int count, bool transpose, double[] value)
       {
           GetDelegateFor<glProgramUniformMatrix4dv>()(program, location, count, transpose, value);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform value to be modified.</param>
       /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
       /// <param name="transpose">Specifies whether to transpose the matrix as the values are loaded into the uniform variable.</param>
       /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
       public void ProgramUniformMatrix2x3(uint program, int location, int count, bool transpose, float[] value)
       {
           GetDelegateFor<glProgramUniformMatrix2x3fv>()(program, location, count, transpose, value);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform value to be modified.</param>
       /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
       /// <param name="transpose">Specifies whether to transpose the matrix as the values are loaded into the uniform variable.</param>
       /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
       public void ProgramUniformMatrix3x2(uint program, int location, int count, bool transpose, float[] value)
       {
           GetDelegateFor<glProgramUniformMatrix3x2fv>()(program, location, count, transpose, value);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform value to be modified.</param>
       /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
       /// <param name="transpose">Specifies whether to transpose the matrix as the values are loaded into the uniform variable.</param>
       /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
       public void ProgramUniformMatrix2x4(uint program, int location, int count, bool transpose, float[] value)
       {
           GetDelegateFor<glProgramUniformMatrix2x4fv>()(program, location, count, transpose, value);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform value to be modified.</param>
       /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
       /// <param name="transpose">Specifies whether to transpose the matrix as the values are loaded into the uniform variable.</param>
       /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
       public void ProgramUniformMatrix4x2(uint program, int location, int count, bool transpose, float[] value)
       {
           GetDelegateFor<glProgramUniformMatrix4x2fv>()(program, location, count, transpose, value);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform value to be modified.</param>
       /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
       /// <param name="transpose">Specifies whether to transpose the matrix as the values are loaded into the uniform variable.</param>
       /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
       public void ProgramUniformMatrix3x4(uint program, int location, int count, bool transpose, float[] value)
       {
           GetDelegateFor<glProgramUniformMatrix3x4fv>()(program, location, count, transpose, value);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform value to be modified.</param>
       /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
       /// <param name="transpose">Specifies whether to transpose the matrix as the values are loaded into the uniform variable.</param>
       /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
       public void ProgramUniformMatrix4x3(uint program, int location, int count, bool transpose, float[] value)
       {
           GetDelegateFor<glProgramUniformMatrix4x3fv>()(program, location, count, transpose, value);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform value to be modified.</param>
       /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
       /// <param name="transpose">Specifies whether to transpose the matrix as the values are loaded into the uniform variable.</param>
       /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
       public void ProgramUniformMatrix2x3(uint program, int location, int count, bool transpose, double[] value)
       {
           GetDelegateFor<glProgramUniformMatrix2x3dv>()(program, location, count, transpose, value);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform value to be modified.</param>
       /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
       /// <param name="transpose">Specifies whether to transpose the matrix as the values are loaded into the uniform variable.</param>
       /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
       public void ProgramUniformMatrix3x2(uint program, int location, int count, bool transpose, double[] value)
       {
           GetDelegateFor<glProgramUniformMatrix3x2dv>()(program, location, count, transpose, value);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform value to be modified.</param>
       /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
       /// <param name="transpose">Specifies whether to transpose the matrix as the values are loaded into the uniform variable.</param>
       /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
       public void ProgramUniformMatrix2x4(uint program, int location, int count, bool transpose, double[] value)
       {
           GetDelegateFor<glProgramUniformMatrix2x4dv>()(program, location, count, transpose, value);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform value to be modified.</param>
       /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
       /// <param name="transpose">Specifies whether to transpose the matrix as the values are loaded into the uniform variable.</param>
       /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
       public void ProgramUniformMatrix4x2(uint program, int location, int count, bool transpose, double[] value)
       {
           GetDelegateFor<glProgramUniformMatrix4x2dv>()(program, location, count, transpose, value);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform value to be modified.</param>
       /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
       /// <param name="transpose">Specifies whether to transpose the matrix as the values are loaded into the uniform variable.</param>
       /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
       public void ProgramUniformMatrix3x4(uint program, int location, int count, bool transpose, double[] value)
       {
           GetDelegateFor<glProgramUniformMatrix3x4dv>()(program, location, count, transpose, value);
       }

       /// <summary>
       /// Specify the value of a uniform variable for a specified program object
       /// </summary>
       /// <param name="program">Specifies the handle of the program containing the uniform variable to be modified.</param>
       /// <param name="location">Specifies the location of the uniform value to be modified.</param>
       /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
       /// <param name="transpose">Specifies whether to transpose the matrix as the values are loaded into the uniform variable.</param>
       /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
       public void ProgramUniformMatrix4x3(uint program, int location, int count, bool transpose, double[] value)
       {
           GetDelegateFor<glProgramUniformMatrix4x3dv>()(program, location, count, transpose, value);
       }

       /// <summary>
       /// Validate a program pipeline object against current GL state.
       /// </summary>
       /// <param name="pipeline">Specifies the name of a program pipeline object to validate.</param>
       public void ValidateProgramPipeline(uint pipeline)
       {
           GetDelegateFor<glValidateProgramPipeline>()(pipeline);
       }
       /// <summary>
       /// Retrieve the info log string from a program pipeline object.
       /// </summary>
       /// <param name="pipeline">Specifies the name of a program pipeline object from which to retrieve the info log.</param>
       /// <param name="bufSize">Specifies the maximum number of characters, including the null terminator, that may be written into infoLog.</param>
       /// <param name="length">Specifies the address of a variable into which will be written the number of characters written into infoLog.</param>
       /// <param name="infoLog">Specifies the address of an array of characters into which will be written the info log for pipeline.</param>
       public void lGetProgramPipelineInfoLog(uint pipeline, int bufSize, out int length, out string infoLog)
       {
           var builder = new StringBuilder(bufSize);
           GetDelegateFor<glGetProgramPipelineInfoLog>()(pipeline, bufSize, out length, builder);
           infoLog = builder.ToString();
       }

        #endregion

        #region ARB_ES2_compatibility

        private delegate void glReleaseShaderCompiler();
        private delegate void glShaderBinary(uint count, uint[] shaders, uint binaryformat, IntPtr binary, int length);
        private delegate void glGetShaderPrecisionFormat(uint shadertype, uint precisiontype, out int range, out int precision);
        private delegate void glDepthRangef(float n, float f);
        private delegate void glClearDepthf(float d);

        /// <summary>
        /// Release resources consumed by the implementation's shader compiler.
        /// </summary>
        public void ReleaseShaderCompiler()
        {
            GetDelegateFor<glReleaseShaderCompiler>()();
        }

        /// <summary>
        /// Load pre-compiled shader binaries
        /// </summary>
        /// <param name="count">Specifies the number of shader object handles contained in shaders.</param>
        /// <param name="shaders">Specifies the address of an array of shader handles into which to load pre-compiled shader binaries.</param>
        /// <param name="binaryformat">Specifies the format of the shader binaries contained in binary.</param>
        /// <param name="binary">Specifies the address of an array of bytes containing pre-compiled binary shader code.</param>
        /// <param name="length">Specifies the length of the array whose address is given in binary.</param>
        public void ShaderBinary(uint count, uint[] shaders, uint binaryformat, IntPtr binary, int length)
        {
            GetDelegateFor<glShaderBinary>()(count, shaders, binaryformat, binary, length);
        }

        /// <summary>
        /// Retrieve the range and precision for numeric formats supported by the shader compiler.
        /// </summary>
        /// <param name="shadertype">Specifies the type of shader whose precision to query. shaderType must be GL_VERTEX_SHADER or GL_FRAGMENT_SHADER.</param>
        /// <param name="precisiontype">Specifies the numeric format whose precision and range to query.</param>
        /// <param name="range">Specifies the address of array of two integers into which encodings of the implementation's numeric range are returned.</param>
        /// <param name="precision">Specifies the address of an integer into which the numeric precision of the implementation is written.</param>
        public void GetShaderPrecisionFormat(uint shadertype, uint precisiontype, out int range, out int precision)
        {
            GetDelegateFor<glGetShaderPrecisionFormat>()(shadertype, precisiontype, out range, out precision);
        }

        /// <summary>
        /// Specify mapping of depth values from normalized device coordinates to window coordinates.
        /// </summary>
        /// <param name="n">Specifies the mapping of the near clipping plane to window coordinates. The initial value is 0.</param>
        /// <param name="f">Specifies the mapping of the far clipping plane to window coordinates. The initial value is 1.</param>
        public void DepthRange(float n, float f)
        {
            GetDelegateFor<glDepthRangef>()(n, f);
        }

        /// <summary>
        /// Specify the clear value for the depth buffer
        /// </summary>
        /// <param name="d">Specifies the depth value used when the depth buffer is cleared. The initial value is 1.</param>
        public void ClearDepth(float d)
        {
            GetDelegateFor<glClearDepthf>()(d);
        }

        public const uint GL_SHADER_COMPILER = 0x8DFA;
        public const uint GL_SHADER_BINARY_FORMATS = 0x8DF8;
        public const uint GL_NUM_SHADER_BINARY_FORMATS = 0x8DF9;
        public const uint GL_MAX_VERTEX_UNIFORM_VECTORS = 0x8DFB;
        public const uint GL_MAX_VARYING_VECTORS = 0x8DFC;
        public const uint GL_MAX_FRAGMENT_UNIFORM_VECTORS = 0x8DFD;
        public const uint GL_IMPLEMENTATION_COLOR_READ_TYPE = 0x8B9A;
        public const uint GL_IMPLEMENTATION_COLOR_READ_FORMAT = 0x8B9B;
        public const uint GL_FIXED = 0x140C;
        public const uint GL_LOW_FLOAT = 0x8DF0;
        public const uint GL_MEDIUM_FLOAT = 0x8DF1;
        public const uint GL_HIGH_FLOAT = 0x8DF2;
        public const uint GL_LOW_INT = 0x8DF3;
        public const uint GL_MEDIUM_INT = 0x8DF4;
        public const uint GL_HIGH_INT = 0x8DF5;
        public const uint GL_RGB565 = 0x8D62;

        #endregion

        #region ARB_shader_precision

        //  No new methods or constants.

        #endregion

        #region ARB_vertex_attrib_64bit
        
        private delegate void glVertexAttribL1d(uint index, double x);
        private delegate void glVertexAttribL2d(uint index, double x, double y);
        private delegate void glVertexAttribL3d(uint index, double x, double y, double z);
        private delegate void glVertexAttribL4d(uint index, double x, double y, double z, double w);
        private delegate void glVertexAttribL1dv(uint index, double[] v);
        private delegate void glVertexAttribL2dv(uint index, double[] v);
        private delegate void glVertexAttribL3dv(uint index, double[] v);
        private delegate void glVertexAttribL4dv(uint index, double[] v);
        private delegate void glVertexAttribLPointer(uint index, int size, uint type, int stride, IntPtr pointer);
        private delegate void glGetVertexAttribLdv(uint index, uint pname, double[] @params);

        //  (note:  VertexArrayVertexAttribLOffsetEXT is provided only if EXT_direct_state_access is supported.)
        // void glVertexArrayVertexAttribLOffsetEXT(uint vaobj, uint buffer, uint index, int size, uint type, int stride, int offset);

        /// <summary>
        /// Specifies the value of a generic vertex attribute.
        /// </summary>
        /// <param name="index">Specifies the index of the generic vertex attribute to be modified.</param>
        /// <param name="x">Specifies the new values to be used for the specified vertex attribute.</param>
        public void VertexAttribL(uint index, double x)
        {
            GetDelegateFor<glVertexAttribL1d>()(index, x);
        }

        /// <summary>
        /// Specifies the value of a generic vertex attribute.
        /// </summary>
        /// <param name="index">Specifies the index of the generic vertex attribute to be modified.</param>
        /// <param name="x">Specifies the new values to be used for the specified vertex attribute.</param>
        /// <param name="y">Specifies the new values to be used for the specified vertex attribute.</param>
        public void VertexAttribL(uint index, double x, double y)
        {
            GetDelegateFor<glVertexAttribL2d>()(index, x, y);
        }

        /// <summary>
        /// Specifies the value of a generic vertex attribute.
        /// </summary>
        /// <param name="index">Specifies the index of the generic vertex attribute to be modified.</param>
        /// <param name="x">Specifies the new values to be used for the specified vertex attribute.</param>
        /// <param name="y">Specifies the new values to be used for the specified vertex attribute.</param>
        /// <param name="z">Specifies the new values to be used for the specified vertex attribute.</param>
        public void VertexAttribL(uint index, double x, double y, double z)
        {
            GetDelegateFor<glVertexAttribL3d>()(index, x, y, z);
        }

        /// <summary>
        /// Specifies the value of a generic vertex attribute.
        /// </summary>
        /// <param name="index">Specifies the index of the generic vertex attribute to be modified.</param>
        /// <param name="x">Specifies the new values to be used for the specified vertex attribute.</param>
        /// <param name="y">Specifies the new values to be used for the specified vertex attribute.</param>
        /// <param name="z">Specifies the new values to be used for the specified vertex attribute.</param>
        /// <param name="w">Specifies the new values to be used for the specified vertex attribute.</param>
        public void VertexAttribL(uint index, double x, double y, double z, double w)
        {
            GetDelegateFor<glVertexAttribL4d>()(index, x, y, z, w);
        }

        /// <summary>
        /// Specifies the value of a generic vertex attribute.
        /// </summary>
        /// <param name="index">Specifies the index of the generic vertex attribute to be modified.</param>
        /// <param name="v">Specifies the new values to be used for the specified vertex attribute.</param>
        public void VertexAttribL1(uint index, double[] v)
        {
            GetDelegateFor<glVertexAttribL1dv>()(index, v);
        }

        /// <summary>
        /// Specifies the value of a generic vertex attribute.
        /// </summary>
        /// <param name="index">Specifies the index of the generic vertex attribute to be modified.</param>
        /// <param name="v">Secifies a pointer to an array of values to be used for the generic vertex attribute.</param>
        public void VertexAttribL2(uint index, double[] v)
        {
            GetDelegateFor<glVertexAttribL2dv>()(index, v);
        }

        /// <summary>
        /// Specifies the value of a generic vertex attribute.
        /// </summary>
        /// <param name="index">Specifies the index of the generic vertex attribute to be modified.</param>
        /// <param name="v">Secifies a pointer to an array of values to be used for the generic vertex attribute.</param>
        public void VertexAttribL3(uint index, double[] v)
        {
            GetDelegateFor<glVertexAttribL3dv>()(index, v);
        }

        /// <summary>
        /// Specifies the value of a generic vertex attribute.
        /// </summary>
        /// <param name="index">Specifies the index of the generic vertex attribute to be modified.</param>
        /// <param name="v">Secifies a pointer to an array of values to be used for the generic vertex attribute.</param>
        public void VertexAttribL4(uint index, double[] v)
        {
            GetDelegateFor<glVertexAttribL4dv>()(index, v);
        }

        /// <summary>
        /// Define an array of generic vertex attribute data.
        /// </summary>
        /// <param name="index">Specifies the index of the generic vertex attribute to be modified.</param>
        /// <param name="size">Specifies the number of components per generic vertex attribute. Must be 1, 2, 3, 4. Additionally, the symbolic constant GL_BGRA​ is accepted by glVertexAttribPointer. The initial value is 4.</param>
        /// <param name="type">Specifies the data type of each component in the array. The different functions take different values. glVertexAttribLPointer takes only GL_DOUBLE​.</param>
        /// <param name="stride">Specifies the byte offset between consecutive generic vertex attributes. If stride​ is 0, the generic vertex attributes are understood to be tightly packed in the array. The initial value is 0.</param>
        /// <param name="pointer">Specifies a offset of the first component of the first generic vertex attribute in the array in the data store of the buffer currently bound to the GL_ARRAY_BUFFER​ target. The initial value is 0.</param>
        public void VertexAttribLPointer(uint index, int size, uint type, int stride, IntPtr pointer)
        {
            GetDelegateFor<glVertexAttribLPointer>()(index, size, type, stride, pointer);
        }

        /// <summary>
        /// Return a generic vertex attribute parameter.
        /// </summary>
        /// <param name="index">Specifies the generic vertex attribute parameter to be queried.</param>
        /// <param name="pname">Specifies the symbolic name of the vertex attribute parameter to be queried. Accepted values are GL_VERTEX_ATTRIB_ARRAY_BUFFER_BINDING, GL_VERTEX_ATTRIB_ARRAY_ENABLED, GL_VERTEX_ATTRIB_ARRAY_SIZE, GL_VERTEX_ATTRIB_ARRAY_STRIDE, GL_VERTEX_ATTRIB_ARRAY_TYPE, GL_VERTEX_ATTRIB_ARRAY_NORMALIZED, GL_VERTEX_ATTRIB_ARRAY_INTEGER, GL_VERTEX_ATTRIB_ARRAY_DIVISOR, or GL_CURRENT_VERTEX_ATTRIB.</param>
        /// <param name="params">Returns the requested data.</param>
        public void GetVertexAttribL(uint index, uint pname, double[] @params)
        {
            GetDelegateFor<glGetVertexAttribLdv>()(index, pname, @params);
        }

        // Already defined in ARB_gpu_shader_fp64!
        //public const uint GL_DOUBLE_VEC2 = 0x8FFC;
        //public const uint GL_DOUBLE_VEC3 = 0x8FFD;
        //public const uint GL_DOUBLE_VEC4 = 0x8FFE;
        //public const uint GL_DOUBLE_MAT2 = 0x8F46;
        //public const uint GL_DOUBLE_MAT3 = 0x8F47;
        //public const uint GL_DOUBLE_MAT4 = 0x8F48;
        //public const uint GL_DOUBLE_MAT2x3 = 0x8F49;
        //public const uint GL_DOUBLE_MAT2x4 = 0x8F4A;
        //public const uint GL_DOUBLE_MAT3x2 = 0x8F4B;
        //public const uint GL_DOUBLE_MAT3x4 = 0x8F4C;
        //public const uint GL_DOUBLE_MAT4x2 = 0x8F4D;
        //public const uint GL_DOUBLE_MAT4x3 = 0x8F4E;

        #endregion

        #region ARB_viewport_array
        
        private delegate void glViewportArrayv(uint first, int count, float[] v);
        private delegate void glViewportIndexedf(uint index, float x, float y, float w, float h);
        private delegate void glViewportIndexedfv(uint index, float[] v);
        private delegate void glScissorArrayv(uint first, int count, int[] v);
        private delegate void glScissorIndexed(uint index, int left, int bottom, int width, int height);
        private delegate void glScissorIndexedv(uint index, int[] v);
        private delegate void glDepthRangeArrayv(uint first, int count, double[] v);
        private delegate void glDepthRangeIndexed(uint index, double n, double f);
        private delegate void glGetFloati_v(uint target, uint index, float[] data);
        private delegate void glGetDoublei_v(uint target, uint index, double[] data);

        /// <summary>
        /// Set multiple viewports.
        /// </summary>
        /// <param name="first">Specify the first viewport to set.</param>
        /// <param name="count">Specify the number of viewports to set.</param>
        /// <param name="v">Specify the address of an array containing the viewport parameters.</param>
        public void ViewportArray(uint first, int count, float[] v)
        {
            GetDelegateFor<glViewportArrayv>()(first, count, v);
        }

        /// <summary>
        /// Set a specified viewport.
        /// </summary>
        /// <param name="index">Specify the first viewport to set.</param>
        /// <param name="x">Specifies the lower left corner of the viewport rectangle, in pixels. The initial value is (0,0).</param>
        /// <param name="y">Specifies the lower left corner of the viewport rectangle, in pixels. The initial value is (0,0).</param>
        /// <param name="w">specifies the width and height of the viewport. When a GL context is first attached to a window, width and height are set to the dimensions of that window.</param>
        /// <param name="h">specifies the width and height of the viewport. When a GL context is first attached to a window, width and height are set to the dimensions of that window.</param>
        public void ViewportIndexed(uint index, float x, float y, float w, float h)
        {
            GetDelegateFor<glViewportIndexedf>()(index, x, y, w, h);
        }

        /// <summary>
        /// Set a specified viewport.
        /// </summary>
        /// <param name="index">Specify the first viewport to set.</param>
        /// <param name="v">specifies the address of an array containing the viewport parameters.</param>
        public void ViewportIndexed(uint index, float[] v)
        {
            GetDelegateFor<glViewportIndexedfv>()(index, v);
        }

        /// <summary>
        /// Define the scissor box for multiple viewports.
        /// </summary>
        /// <param name="first">Specifies the index of the first viewport whose scissor box to modify.</param>
        /// <param name="count">Specifies the number of scissor boxes to modify.</param>
        /// <param name="v">Specifies the address of an array containing the left, bottom, width and height of each scissor box, in that order.</param>
        public void ScissorArray(uint first, int count, int[] v)
        {
            GetDelegateFor<glScissorArrayv>()(first, count, v);
        }

        /// <summary>
        /// Define the scissor box for a specific viewport.
        /// </summary>
        /// <param name="index">Specifies the index of the viewport whose scissor box to modify.</param>
        /// <param name="left">Specify the coordinate of the bottom left corner of the scissor box, in pixels.</param>
        /// <param name="bottom">Specify the coordinate of the bottom left corner of the scissor box, in pixels.</param>
        /// <param name="width">Specify ths dimensions of the scissor box, in pixels.</param>
        /// <param name="height">Specify ths dimensions of the scissor box, in pixels.</param>
        public void ScissorIndexed(uint index, int left, int bottom, int width, int height)
        {
            GetDelegateFor<glScissorIndexed>()(index, left, bottom, width, height);
        }

        /// <summary>
        /// Define the scissor box for a specific viewport.
        /// </summary>
        /// <param name="index">Specifies the index of the viewport whose scissor box to modify.</param>
        /// <param name="v">Specifies the address of an array containing the left, bottom, width and height of each scissor box, in that order.</param>
        public void ScissorIndexed(uint index, int[] v)
        {
            GetDelegateFor<glScissorIndexedv>()(index, v);
        }
        /// <summary>
        /// Specify mapping of depth values from normalized device coordinates to window coordinates for a specified set of viewports.
        /// </summary>
        /// <param name="first">Specifies the index of the first viewport whose depth range to update.</param>
        /// <param name="count">Specifies the number of viewports whose depth range to update.</param>
        /// <param name="v">Specifies the address of an array containing the near and far values for the depth range of each modified viewport.</param>
        public void DepthRangeArray(uint first, int count, double[] v)
        {
            GetDelegateFor<glDepthRangeArrayv>()(first, count, v);
        }

        /// <summary>
        /// Specify mapping of depth values from normalized device coordinates to window coordinates for a specified viewport
        /// </summary>
        /// <param name="index">Specifies the index of the viewport whose depth range to update.</param>
        /// <param name="n">Specifies the mapping of the near clipping plane to window coordinates. The initial value is 0.</param>
        /// <param name="f">Specifies the mapping of the far clipping plane to window coordinates. The initial value is 1.</param>
        public void DepthRangeIndexed(uint index, double n, double f)
        {
            GetDelegateFor<glDepthRangeIndexed>()(index, n, f);
        }

        /// <summary>
        /// Return the value or values of a selected parameter.
        /// </summary>
        /// <param name="target">Specifies the parameter value to be returned. The symbolic constants in the list below are accepted.</param>
        /// <param name="index">Specifies the zero-based index of the particular element being queried.</param>
        /// <param name="data">Returns the value or values of the specified parameter.</param>
        public void Get(uint target, uint index, float[] data)
        {
            GetDelegateFor<glGetFloati_v>()(target, index, data);
        }

        /// <summary>
        /// Return the value or values of a selected parameter.
        /// </summary>
        /// <param name="target">Specifies the parameter value to be returned. The symbolic constants in the list below are accepted.</param>
        /// <param name="index">Specifies the zero-based index of the particular element being queried.</param>
        /// <param name="data">Returns the value or values of the specified parameter.</param>
        public void Get(uint target, uint index, double[] data)
        {
            GetDelegateFor<glGetDoublei_v>()(target, index, data);
        }
        
        //  TODO: #86
        //  Note that GetIntegerIndexedvEXT, EnableIndexedEXT, DisableIndexedEXT and
        //  IsEnabledIndexedEXT are introduced by other OpenGL extensions such as
        //  EXT_draw_buffers2. If this extension is implemented against an earlier
        //  version of OpenGL that does not support GetIntegeri_v and so on, the
        //  'Indexed' versions of these functions may be used in their place.
        
        //void GetIntegerIndexedvEXT(enum target, uint index, int * v);
        //void EnableIndexedEXT(enum target, uint index);
        //void DisableIndexedEXT(enum target, uint index);
        //boolean IsEnabledIndexedEXT(enum target, uint index);

        #endregion
    }

// ReSharper restore InconsistentNaming
}
