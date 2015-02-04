using System;
using System.Text;

namespace SharpGL
{
// ReSharper disable InconsistentNaming

    public partial class OpenGL
    {
        #region ARB_texture_query_lod

        //  No new tokens or functions.

        #endregion

        #region ARB_gpu_shader5

        public const uint GL_GEOMETRY_SHADER_INVOCATIONS = 0x887F;
        public const uint GL_MAX_GEOMETRY_SHADER_INVOCATIONS = 0x8E5A;
        public const uint GL_MIN_FRAGMENT_INTERPOLATION_OFFSET = 0x8E5B;
        public const uint GL_MAX_FRAGMENT_INTERPOLATION_OFFSET = 0x8E5C;
        public const uint GL_FRAGMENT_INTERPOLATION_OFFSET_BITS = 0x8E5D;
        public const uint GL_MAX_VERTEX_STREAMS = 0x8E71;

        #endregion

        #region ARB_gpu_shader_fp64

        private delegate void glUniform1d(int location, double x);
        private delegate void glUniform2d(int location, double x, double y);
        private delegate void glUniform3d(int location, double x, double y, double z);
        private delegate void glUniform4d(int location, double x, double y, double z, double w);
        private delegate void glUniform1dv(int location, int count, double[] value);
        private delegate void glUniform2dv(int location, int count, double[] value);
        private delegate void glUniform3dv(int location, int count, double[] value);
        private delegate void glUniform4dv(int location, int count, double[] value);
        private delegate void glUniformMatrix2dv(int location, int count, bool transpose, double[] value);
        private delegate void glUniformMatrix3dv(int location, int count, bool transpose, double[] value);
        private delegate void glUniformMatrix4dv(int location, int count, bool transpose, double[] value);
        private delegate void glUniformMatrix2x3dv(int location, int count, bool transpose, double[] value);
        private delegate void glUniformMatrix2x4dv(int location, int count, bool transpose, double[] value);
        private delegate void glUniformMatrix3x2dv(int location, int count, bool transpose, double[] value);
        private delegate void glUniformMatrix3x4dv(int location, int count, bool transpose, double[] value);
        private delegate void glUniformMatrix4x2dv(int location, int count, bool transpose, double[] value);
        private delegate void glUniformMatrix4x3dv(int location, int count, bool transpose, double[] value);
        private delegate void glGetUniformdv(uint program, int location, double[] @params);

        // (All of the following ProgramUniform* functions are supported if and only if EXT_direct_state_access is supported.)
        private delegate void glProgramUniform1dEXT(uint program, int location, double x);
        private delegate void glProgramUniform2dEXT(uint program, int location, double x, double y);
        private delegate void glProgramUniform3dEXT(uint program, int location, double x, double y, double z);
        private delegate void glProgramUniform4dEXT(uint program, int location, double x, double y, double z, double w);
        private delegate void glProgramUniform1dvEXT(uint program, int location, int count, double[] value);
        private delegate void glProgramUniform2dvEXT(uint program, int location, int count, double[] value);
        private delegate void glProgramUniform3dvEXT(uint program, int location, int count, double[] value);
        private delegate void glProgramUniform4dvEXT(uint program, int location, int count,  double[] value);
        private delegate void glProgramUniformMatrix2dvEXT(uint program, int location, int count, bool transpose, double[] value);
        private delegate void glProgramUniformMatrix3dvEXT(uint program, int location, int count, bool transpose, double[] value);
        private delegate void glProgramUniformMatrix4dvEXT(uint program, int location, int count, bool transpose, double[] value);
        private delegate void glProgramUniformMatrix2x3dvEXT(uint program, int location, int count, bool transpose, double[] value);
        private delegate void glProgramUniformMatrix2x4dvEXT(uint program, int location, int count, bool transpose, double[] value);
        private delegate void glProgramUniformMatrix3x2dvEXT(uint program, int location, int count, bool transpose, double[] value);
        private delegate void glProgramUniformMatrix3x4dvEXT(uint program, int location, int count, bool transpose, double[] value);
        private delegate void glProgramUniformMatrix4x2dvEXT(uint program, int location, int count, bool transpose, double[] value);
        private delegate void glProgramUniformMatrix4x3dvEXT(uint program, int location, int count, bool transpose, double[] value);

        public const uint GL_DOUBLE_VEC2 = 0x8FFC;
        public const uint GL_DOUBLE_VEC3 = 0x8FFD;
        public const uint GL_DOUBLE_VEC4 = 0x8FFE;
        public const uint GL_DOUBLE_MAT2 = 0x8F46;
        public const uint GL_DOUBLE_MAT3 = 0x8F47;
        public const uint GL_DOUBLE_MAT4 = 0x8F48;
        public const uint GL_DOUBLE_MAT2x3 = 0x8F49;
        public const uint GL_DOUBLE_MAT2x4 = 0x8F4A;
        public const uint GL_DOUBLE_MAT3x2 = 0x8F4B;
        public const uint GL_DOUBLE_MAT3x4 = 0x8F4C;
        public const uint GL_DOUBLE_MAT4x2 = 0x8F4D;
        public const uint GL_DOUBLE_MAT4x3 = 0x8F4E;


        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="x">For the scalar commands, specifies the new values to be used for the specified uniform variable.</param>
        public void Uniform(int location, double x)
        {
            GetDelegateFor<glUniform1d>()(location, x);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="x">For the scalar commands, specifies the new values to be used for the specified uniform variable.</param>
        /// <param name="y">For the scalar commands, specifies the new values to be used for the specified uniform variable.</param>
        public void Uniform(int location, double x, double y)
        {
            GetDelegateFor<glUniform2d>()(location, x, y);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="x">For the scalar commands, specifies the new values to be used for the specified uniform variable.</param>
        /// <param name="y">For the scalar commands, specifies the new values to be used for the specified uniform variable.</param>
        /// <param name="z">For the scalar commands, specifies the new values to be used for the specified uniform variable.</param>
        public void Uniform(int location, double x, double y, double z)
        {
            GetDelegateFor<glUniform3d>()(location, x, y, z);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="x">For the scalar commands, specifies the new values to be used for the specified uniform variable.</param>
        /// <param name="y">For the scalar commands, specifies the new values to be used for the specified uniform variable.</param>
        /// <param name="z">For the scalar commands, specifies the new values to be used for the specified uniform variable.</param>
        /// <param name="w">For the scalar commands, specifies the new values to be used for the specified uniform variable.</param>
        public void Uniform(int location, double x, double y, double z, double w)
        {
            GetDelegateFor<glUniform4d>()(location, x, y, z, w);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="count">Specifies the number of elements that are to be modified. This should be 1 if the targeted uniform variable is not an array, and 1 or more if it is an array.</param>
        /// <param name="value">For the vector and matrix commands, specifies a pointer to an array of count values that will be used to update the specified uniform variable.</param>
        public void Uniform1(int location, int count, double[] value)
        {
            GetDelegateFor<glUniform1dv>()(location, count, value);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="count">Specifies the number of elements that are to be modified. This should be 1 if the targeted uniform variable is not an array, and 1 or more if it is an array.</param>
        /// <param name="value">For the vector and matrix commands, specifies a pointer to an array of count values that will be used to update the specified uniform variable.</param>
        public void Uniform2(int location, int count, double[] value)
        {
            GetDelegateFor<glUniform2dv>()(location, count, value);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="count">Specifies the number of elements that are to be modified. This should be 1 if the targeted uniform variable is not an array, and 1 or more if it is an array.</param>
        /// <param name="value">For the vector and matrix commands, specifies a pointer to an array of count values that will be used to update the specified uniform variable.</param>
        public void Uniform3(int location, int count, double[] value)
        {
            GetDelegateFor<glUniform3dv>()(location, count, value);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="count">Specifies the number of elements that are to be modified. This should be 1 if the targeted uniform variable is not an array, and 1 or more if it is an array.</param>
        /// <param name="value">For the vector and matrix commands, specifies a pointer to an array of count values that will be used to update the specified uniform variable.</param>
        public void Uniform4(int location, int count, double[] value)
        {
            GetDelegateFor<glUniform4dv>()(location, count, value);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
        /// <param name="transpose">Specifies whether to transpose the matrix as the values are loaded into the uniform variable.</param>
        /// <param name="value">Specifies a pointer to an array of count values that will be used to update the specified uniform variable.</param>
        public void UniformMatrix2(int location, int count, bool transpose, double[] value)
        {
            GetDelegateFor<glUniformMatrix2dv>()(location, count, transpose, value);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
        /// <param name="transpose">Specifies whether to transpose the matrix as the values are loaded into the uniform variable.</param>
        /// <param name="value">Specifies a pointer to an array of count values that will be used to update the specified uniform variable.</param>
        public void UniformMatrix3(int location, int count, bool transpose, double[] value)
        {
            GetDelegateFor<glUniformMatrix3dv>()(location, count, transpose, value);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
        /// <param name="transpose">Specifies whether to transpose the matrix as the values are loaded into the uniform variable.</param>
        /// <param name="value">Specifies a pointer to an array of count values that will be used to update the specified uniform variable.</param>
        public void UniformMatrix4(int location, int count, bool transpose, double[] value)
        {
            GetDelegateFor<glUniformMatrix4dv>()(location, count, transpose, value);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
        /// <param name="transpose">Specifies whether to transpose the matrix as the values are loaded into the uniform variable.</param>
        /// <param name="value">Specifies a pointer to an array of count values that will be used to update the specified uniform variable.</param>
        public void UniformMatrix2x3(int location, int count, bool transpose, double[] value)
        {
            GetDelegateFor<glUniformMatrix2x3dv>()(location, count, transpose, value);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
        /// <param name="transpose">Specifies whether to transpose the matrix as the values are loaded into the uniform variable.</param>
        /// <param name="value">Specifies a pointer to an array of count values that will be used to update the specified uniform variable.</param>
        public void UniformMatrix2x4(int location, int count, bool transpose, double[] value)
        {
            GetDelegateFor<glUniformMatrix2x4dv>()(location, count, transpose, value);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
        /// <param name="transpose">Specifies whether to transpose the matrix as the values are loaded into the uniform variable.</param>
        /// <param name="value">Specifies a pointer to an array of count values that will be used to update the specified uniform variable.</param>
        public void UniformMatrix3x2(int location, int count, bool transpose, double[] value)
        {
            GetDelegateFor<glUniformMatrix3x2dv>()(location, count, transpose, value);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
        /// <param name="transpose">Specifies whether to transpose the matrix as the values are loaded into the uniform variable.</param>
        /// <param name="value">Specifies a pointer to an array of count values that will be used to update the specified uniform variable.</param>
        public void UniformMatrix3x4(int location, int count, bool transpose, double[] value)
        {
            GetDelegateFor<glUniformMatrix3x4dv>()(location, count, transpose, value);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
        /// <param name="transpose">Specifies whether to transpose the matrix as the values are loaded into the uniform variable.</param>
        /// <param name="value">Specifies a pointer to an array of count values that will be used to update the specified uniform variable.</param>
        public void UniformMatrix4x2(int location, int count, bool transpose, double[] value)
        {
            GetDelegateFor<glUniformMatrix4x2dv>()(location, count, transpose, value);
        }
        
        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
        /// <param name="transpose">Specifies whether to transpose the matrix as the values are loaded into the uniform variable.</param>
        /// <param name="value">Specifies a pointer to an array of count values that will be used to update the specified uniform variable.</param>
        public void UniformMatrix4x3(int location, int count, bool transpose, double[] value)
        {
            GetDelegateFor<glUniformMatrix4x3dv>()(location, count, transpose, value);
        }

        /// <summary>
        /// Return the value of a uniform variable.
        /// </summary>
        /// <param name="program">Specifies the program object to be queried.</param>
        /// <param name="location">Specifies the location of the uniform variable to be queried.</param>
        /// <param name="params">Returns the value of the specified uniform variable.</param>
        public void GetUniform(uint program, int location, double[] @params)
        {
            GetDelegateFor<glGetUniformdv>()(program, location, @params);
        }

        /// <summary>
        /// Specify the value of a uniform variable for a program object.
        /// </summary>
        /// <param name="program">The program object.</param>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="x">For the scalar commands, specifies the new values to be used for the specified uniform variable.</param>
        public void ProgramUniform1EXT(uint program, int location, double x)
        {
            GetDelegateFor<glProgramUniform1dEXT>()(program, location, x);
        }

        /// <summary>
        /// Specify the value of a uniform variable for a program object.
        /// </summary>
        /// <param name="program">The program object.</param>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="x">For the scalar commands, specifies the new values to be used for the specified uniform variable.</param>
        /// <param name="y">For the scalar commands, specifies the new values to be used for the specified uniform variable.</param>
        public void ProgramUniform2EXT(uint program, int location, double x, double y)
        {
            GetDelegateFor<glProgramUniform2dEXT>()(program, location, x, y);
        }

        /// <summary>
        /// Specify the value of a uniform variable for a program object.
        /// </summary>
        /// <param name="program">The program object.</param>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="x">For the scalar commands, specifies the new values to be used for the specified uniform variable.</param>
        /// <param name="y">For the scalar commands, specifies the new values to be used for the specified uniform variable.</param>
        /// <param name="z">For the scalar commands, specifies the new values to be used for the specified uniform variable.</param>
        public void ProgramUniform3EXT(uint program, int location, double x, double y, double z)
        {
            GetDelegateFor<glProgramUniform3dEXT>()(program, location, x, y, z);
        }

        /// <summary>
        /// Specify the value of a uniform variable for a program object.
        /// </summary>
        /// <param name="program">The program object.</param>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="x">For the scalar commands, specifies the new values to be used for the specified uniform variable.</param>
        /// <param name="y">For the scalar commands, specifies the new values to be used for the specified uniform variable.</param>
        /// <param name="z">For the scalar commands, specifies the new values to be used for the specified uniform variable.</param>
        /// <param name="w">For the scalar commands, specifies the new values to be used for the specified uniform variable.</param>
        public void ProgramUniform4EXT(uint program, int location, double x, double y, double z, double w)
        {
            GetDelegateFor<glProgramUniform4dEXT>()(program, location, x, y, z, w);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the a program object.
        /// </summary>
        /// <param name="program">The program object.</param>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
        /// <param name="value">Specifies a pointer to an array of count values that will be used to update the specified uniform variable.</param>
        public void ProgramUniform1EXT(uint program, int location, int count, double[] value)
        {
            GetDelegateFor<glProgramUniform1dvEXT>()(program, location, count, value);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the a program object.
        /// </summary>
        /// <param name="program">The program object.</param>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
        /// <param name="value">Specifies a pointer to an array of count values that will be used to update the specified uniform variable.</param>
        public void ProgramUniform2EXT(uint program, int location, int count, double[] value)
        {
            GetDelegateFor<glProgramUniform2dvEXT>()(program, location, count, value);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the a program object.
        /// </summary>
        /// <param name="program">The program object.</param>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
        /// <param name="value">Specifies a pointer to an array of count values that will be used to update the specified uniform variable.</param>
        public void ProgramUniform3EXT(uint program, int location, int count, double[] value)
        {
            GetDelegateFor<glProgramUniform3dvEXT>()(program, location, count, value);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the a program object.
        /// </summary>
        /// <param name="program">The program object.</param>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
        /// <param name="value">Specifies a pointer to an array of count values that will be used to update the specified uniform variable.</param>
        public void ProgramUniform4EXT(uint program, int location, int count, double[] value)
        {
            GetDelegateFor<glProgramUniform4dvEXT>()(program, location, count, value);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the a program object.
        /// </summary>
        /// <param name="program">The program object.</param>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
        /// <param name="transpose">Specifies whether to transpose the matrix as the values are loaded into the uniform variable.</param>
        /// <param name="value">Specifies a pointer to an array of count values that will be used to update the specified uniform variable.</param>
        public void ProgramUniformMatrix2EXT(uint program, int location, int count, bool transpose, double[] value)
        {
            GetDelegateFor<glProgramUniformMatrix2dvEXT>()(program, location, count, transpose, value);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the a program object.
        /// </summary>
        /// <param name="program">The program object.</param>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
        /// <param name="transpose">Specifies whether to transpose the matrix as the values are loaded into the uniform variable.</param>
        /// <param name="value">Specifies a pointer to an array of count values that will be used to update the specified uniform variable.</param>
        public void ProgramUniformMatrix3EXT(uint program, int location, int count, bool transpose, double[] value)
        {
            GetDelegateFor<glProgramUniformMatrix3dvEXT>()(program, location, count, transpose, value);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the a program object.
        /// </summary>
        /// <param name="program">The program object.</param>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
        /// <param name="transpose">Specifies whether to transpose the matrix as the values are loaded into the uniform variable.</param>
        /// <param name="value">Specifies a pointer to an array of count values that will be used to update the specified uniform variable.</param>
        public void ProgramUniformMatrix4EXT(uint program, int location, int count, bool transpose, double[] value)
        {
            GetDelegateFor<glProgramUniformMatrix4dvEXT>()(program, location, count, transpose, value);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the a program object.
        /// </summary>
        /// <param name="program">The program object.</param>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
        /// <param name="transpose">Specifies whether to transpose the matrix as the values are loaded into the uniform variable.</param>
        /// <param name="value">Specifies a pointer to an array of count values that will be used to update the specified uniform variable.</param>
        public void ProgramUniformMatrix2x3EXT(uint program, int location, int count, bool transpose, double[] value)
        {
            GetDelegateFor<glProgramUniformMatrix2x3dvEXT>()(program, location, count, transpose, value);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the a program object.
        /// </summary>
        /// <param name="program">The program object.</param>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
        /// <param name="transpose">Specifies whether to transpose the matrix as the values are loaded into the uniform variable.</param>
        /// <param name="value">Specifies a pointer to an array of count values that will be used to update the specified uniform variable.</param>
        public void ProgramUniformMatrix2x4EXT(uint program, int location, int count, bool transpose, double[] value)
        {
            GetDelegateFor<glProgramUniformMatrix2x4dvEXT>()(program, location, count, transpose, value);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the a program object.
        /// </summary>
        /// <param name="program">The program object.</param>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
        /// <param name="transpose">Specifies whether to transpose the matrix as the values are loaded into the uniform variable.</param>
        /// <param name="value">Specifies a pointer to an array of count values that will be used to update the specified uniform variable.</param>
        public void ProgramUniformMatrix3x2EXT(uint program, int location, int count, bool transpose, double[] value)
        {
            GetDelegateFor<glProgramUniformMatrix3x2dvEXT>()(program, location, count, transpose, value);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the a program object.
        /// </summary>
        /// <param name="program">The program object.</param>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
        /// <param name="transpose">Specifies whether to transpose the matrix as the values are loaded into the uniform variable.</param>
        /// <param name="value">Specifies a pointer to an array of count values that will be used to update the specified uniform variable.</param>
        public void ProgramUniformMatrix3x4EXT(uint program, int location, int count, bool transpose, double[] value)
        {
            GetDelegateFor<glProgramUniformMatrix3x4dvEXT>()(program, location, count, transpose, value);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the a program object.
        /// </summary>
        /// <param name="program">The program object.</param>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
        /// <param name="transpose">Specifies whether to transpose the matrix as the values are loaded into the uniform variable.</param>
        /// <param name="value">Specifies a pointer to an array of count values that will be used to update the specified uniform variable.</param>
        public void ProgramUniformMatrix4x2EXT(uint program, int location, int count, bool transpose, double[] value)
        {
            GetDelegateFor<glProgramUniformMatrix4x2dvEXT>()(program, location, count, transpose, value);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the a program object.
        /// </summary>
        /// <param name="program">The program object.</param>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
        /// <param name="transpose">Specifies whether to transpose the matrix as the values are loaded into the uniform variable.</param>
        /// <param name="value">Specifies a pointer to an array of count values that will be used to update the specified uniform variable.</param>
        public void ProgramUniformMatrix4x3EXT(uint program, int location, int count, bool transpose, double[] value)
        {
            GetDelegateFor<glProgramUniformMatrix4x3dvEXT>()(program, location, count, transpose, value);
        }
        
        #endregion

        #region ARB_shader_subroutine

        private delegate int glGetSubroutineUniformLocation(uint program, uint shadertype, string name);
        private delegate uint glGetSubroutineIndex(uint program, uint shadertype, string name);
        private delegate void glGetActiveSubroutineUniformiv(uint program, uint shadertype, uint index, uint pname, int[] values);
        private delegate void glGetActiveSubroutineUniformName(uint program, uint shadertype, uint index, int bufsize, out int length, StringBuilder name);
        private delegate void glGetActiveSubroutineName(uint program, uint shadertype, uint index, int bufsize, out int length, StringBuilder name);
        private delegate void glUniformSubroutinesuiv(uint shadertype, int count, uint[] indices);
        private delegate void glGetUniformSubroutineuiv(uint shadertype, int location, uint[] @params);
        private delegate void glGetProgramStageiv(uint program, uint shadertype, uint pname, int[] values);

        /// <summary>
        /// Retrieve the location of a subroutine uniform of a given shader stage within a program.
        /// </summary>
        /// <param name="program">Specifies the name of the program containing shader stage.</param>
        /// <param name="shadertype">Specifies the shader stage from which to query for subroutine uniform index. shadertype​ must be one of GL_VERTEX_SHADER​, GL_TESS_CONTROL_SHADER​, GL_TESS_EVALUATION_SHADER​, GL_GEOMETRY_SHADER​, GL_FRAGMENT_SHADER​, or GL_COMPUTE_SHADER​.</param>
        /// <param name="name">Specifies the name of the subroutine uniform whose index to query.</param>
        /// <returns>Returns the location of the subroutine uniform variable name​ in the shader stage of type shadertype​ attached to program​, with behavior otherwise identical to glGetUniformLocation​.</returns>
        public int GetSubroutineUniformLocation(uint program, uint shadertype, string name)
        {
            return GetDelegateFor<glGetSubroutineUniformLocation>()(program, shadertype, name);
        }

        /// <summary>
        /// Retrieve the index of a subroutine uniform of a given shader stage within a program.
        /// </summary>
        /// <param name="program">Specifies the name of the program containing shader stage.</param>
        /// <param name="shadertype">Specifies the shader stage from which to query for subroutine uniform index. shadertype must be one of GL_VERTEX_SHADER, GL_TESS_CONTROL_SHADER, GL_TESS_EVALUATION_SHADER, GL_GEOMETRY_SHADER or GL_FRAGMENT_SHADER.</param>
        /// <param name="name">Specifies the name of the subroutine uniform whose index to query.</param>
        /// <returns>Returns the index of a subroutine uniform within a shader stage attached to a program object. program contains the name of the program to which the shader is attached. shadertype specifies the stage from which to query shader subroutine index. name contains the null-terminated name of the subroutine uniform whose name to query.</returns>
        public uint GetSubroutineIndex(uint program, uint shadertype, string name)
        {
            return GetDelegateFor<glGetSubroutineIndex>()(program, shadertype, name);
        }

        /// <summary>
        /// Gets the active subroutine uniform.
        /// </summary>
        /// <param name="program">The program.</param>
        /// <param name="shadertype">The shadertype.</param>
        /// <param name="index">The index.</param>
        /// <param name="pname">The pname.</param>
        /// <param name="values">The values.</param>
        public void GetActiveSubroutineUniform(uint program, uint shadertype, uint index, uint pname, int[] values)
        {
            GetDelegateFor<glGetActiveSubroutineUniformiv>()(program, shadertype, index, pname, values);
        }

        /// <summary>
        /// Query the name of an active shader subroutine uniform.
        /// </summary>
        /// <param name="program">Specifies the name of the program containing the subroutine.</param>
        /// <param name="shadertype">Specifies the shader stage from which to query for the subroutine parameter. shadertype must be one of GL_VERTEX_SHADER, GL_TESS_CONTROL_SHADER, GL_TESS_EVALUATION_SHADER, GL_GEOMETRY_SHADER or GL_FRAGMENT_SHADER.</param>
        /// <param name="index">Specifies the index of the shader subroutine uniform.</param>
        /// <param name="bufsize">Specifies the size of the buffer whose address is given in name.</param>
        /// <param name="length">Specifies the address of a variable into which is written the number of characters copied into name.</param>
        /// <param name="name">Specifies the address of a buffer that will receive the name of the specified shader subroutine uniform.</param>
        public void GetActiveSubroutineUniformName(uint program, uint shadertype, uint index, int bufsize,
                                                     out int length, out string name)
        {
            var builder = new StringBuilder(bufsize);
            GetDelegateFor<glGetActiveSubroutineUniformName>()(program, shadertype, index, bufsize, out length, builder);
            name = builder.ToString();
        }

        /// <summary>
        /// Gets the name of the active subroutine.
        /// </summary>
        /// <param name="program">Specifies the name of the program containing the subroutine.</param>
        /// <param name="shadertype">Specifies the shader stage from which to query the subroutine name.</param>
        /// <param name="index">Specifies the index of the shader subroutine uniform.</param>
        /// <param name="bufsize">Specifies the size of the buffer whose address is given in name.</param>
        /// <param name="length">Specifies the address of a variable which is to receive the length of the shader subroutine uniform name.</param>
        /// <param name="name">Specifies the address of an array into which the name of the shader subroutine uniform will be written.</param>
        public void GetActiveSubroutineName(uint program, uint shadertype, uint index, int bufsize, out int length,
                                              out string name)
        {
            var builder = new StringBuilder(bufsize);
            GetDelegateFor<glGetActiveSubroutineName>()(program, shadertype, index, bufsize, out length, builder);
            name = builder.ToString();
        }

        /// <summary>
        /// Load active subroutine uniforms.
        /// </summary>
        /// <param name="shadertype">Specifies the shader stage from which to query for subroutine uniform index. shadertype must be one of GL_VERTEX_SHADER, GL_TESS_CONTROL_SHADER, GL_TESS_EVALUATION_SHADER, GL_GEOMETRY_SHADER or GL_FRAGMENT_SHADER.</param>
        /// <param name="count">Specifies the number of uniform indices stored in indices.</param>
        /// <param name="indices">Specifies the address of an array holding the indices to load into the shader subroutine variables.</param>
        public void UniformSubroutines(uint shadertype, int count, uint[] indices)
        {
            GetDelegateFor<glUniformSubroutinesuiv>()(shadertype, count, indices);
        }

        /// <summary>
        /// Retrieve the value of a subroutine uniform of a given shader stage of the current program.
        /// </summary>
        /// <param name="shadertype">Specifies the shader stage from which to query for subroutine uniform index. shadertype must be one of GL_VERTEX_SHADER, GL_TESS_CONTROL_SHADER, GL_TESS_EVALUATION_SHADER, GL_GEOMETRY_SHADER or GL_FRAGMENT_SHADER.</param>
        /// <param name="location">Specifies the location of the subroutine uniform.</param>
        /// <param name="params">Specifies the address of a variable to receive the value or values of the subroutine uniform.</param>
        public void GetUniformSubroutine(uint shadertype, int location, uint[] @params)
        {
            GetDelegateFor<glGetUniformSubroutineuiv>()(shadertype, location, @params);
        }

        /// <summary>
        /// Retrieve properties of a program object corresponding to a specified shader stage.
        /// </summary>
        /// <param name="program">Specifies the name of the program containing shader stage.</param>
        /// <param name="shadertype">Specifies the shader stage from which to query for the subroutine parameter. shadertype must be one of GL_VERTEX_SHADER, GL_TESS_CONTROL_SHADER, GL_TESS_EVALUATION_SHADER, GL_GEOMETRY_SHADER or GL_FRAGMENT_SHADER.</param>
        /// <param name="pname">Specifies the parameter of the shader to query. pname must be GL_ACTIVE_SUBROUTINE_UNIFORMS, GL_ACTIVE_SUBROUTINE_UNIFORM_LOCATIONS, GL_ACTIVE_SUBROUTINES, GL_ACTIVE_SUBROUTINE_UNIFORM_MAX_LENGTH, or GL_ACTIVE_SUBROUTINE_MAX_LENGTH.</param>
        /// <param name="values">Specifies the address of a variable into which the queried value or values will be placed.</param>
        public void GetProgramStage(uint program, uint shadertype, uint pname, int[] values)
        {
            GetDelegateFor<glGetProgramStageiv>()(program, shadertype, pname, values);
        }

        public const uint GL_ACTIVE_SUBROUTINES = 0x8DE5;
        public const uint GL_ACTIVE_SUBROUTINE_UNIFORMS = 0x8DE6;
        public const uint GL_ACTIVE_SUBROUTINE_UNIFORM_LOCATIONS = 0x8E47;
        public const uint GL_ACTIVE_SUBROUTINE_MAX_LENGTH = 0x8E48;
        public const uint GL_ACTIVE_SUBROUTINE_UNIFORM_MAX_LENGTH = 0x8E49;
        public const uint GL_MAX_SUBROUTINES = 0x8DE7;
        public const uint GL_MAX_SUBROUTINE_UNIFORM_LOCATIONS = 0x8DE8;
        public const uint GL_NUM_COMPATIBLE_SUBROUTINES = 0x8E4A;
        public const uint GL_COMPATIBLE_SUBROUTINES = 0x8E4B;
        
        #endregion

        #region ARB_texture_gather

        public const uint GL_MIN_PROGRAM_TEXTURE_GATHER_OFFSET_ARB = 0x8E5E;
        public const uint GL_MAX_PROGRAM_TEXTURE_GATHER_OFFSET_ARB = 0x8E5F;
        public const uint GL_MAX_PROGRAM_TEXTURE_GATHER_COMPONENTS_ARB = 0x8F9F;

        #endregion

        #region ARB_draw_indirect
        
        private delegate void glDrawArraysIndirect(uint mode, IntPtr indirect);
        private delegate void glDrawElementsIndirect(uint mode, uint type, IntPtr indirect);

        /// <summary>
        /// Render primitives from array data, taking parameters from memory.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render. Symbolic constants GL_POINTS​, GL_LINE_STRIP​, GL_LINE_LOOP​, GL_LINES​, GL_LINE_STRIP_ADJACENCY​, GL_LINES_ADJACENCY​, GL_TRIANGLE_STRIP​, GL_TRIANGLE_FAN​, GL_TRIANGLES​, GL_TRIANGLE_STRIP_ADJACENCY​, GL_TRIANGLES_ADJACENCY​, and GL_PATCHES​ are accepted.</param>
        /// <param name="indirect">Specifies a byte offset (cast to a pointer type) into the buffer bound to GL_DRAW_INDIRECT_BUFFER​, which designates the starting point of the structure containing the draw parameters.</param>
        public void DrawArraysIndirect(uint mode, IntPtr indirect)
        {
            GetDelegateFor<glDrawArraysIndirect>()(mode, indirect);
        }

        /// <summary>
        /// Render indexed primitives from array data, taking parameters from memory.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render. Symbolic constants GL_POINTS​, GL_LINE_STRIP​, GL_LINE_LOOP​, GL_LINES​, GL_LINE_STRIP_ADJACENCY​, GL_LINES_ADJACENCY​, GL_TRIANGLE_STRIP​, GL_TRIANGLE_FAN​, GL_TRIANGLES​, GL_TRIANGLE_STRIP_ADJACENCY​, GL_TRIANGLES_ADJACENCY​, and GL_PATCHES​ are accepted.</param>
        /// <param name="type">Specifies the type of data in the buffer bound to the GL_ELEMENT_ARRAY_BUFFER​ binding.</param>
        /// <param name="indirect">Specifies a byte offset (cast to a pointer type) into the buffer bound to GL_DRAW_INDIRECT_BUFFER​, which designates the starting point of the structure containing the draw parameters.</param>
        public void DrawElementsIndirect(uint mode, uint type, IntPtr indirect)
        {
            GetDelegateFor<glDrawElementsIndirect>()(mode, type, indirect);
        }
        
        public const uint GL_DRAW_INDIRECT_BUFFER = 0x8F3F;
        public const uint GL_DRAW_INDIRECT_BUFFER_BINDING = 0x8F43;

        #endregion

        #region ARB_sample_shading

        private delegate void glMinSampleShading(float value);

        /// <summary>
        /// Specifies minimum rate at which sample shaing takes place.
        /// </summary>
        /// <param name="value">Specifies the rate at which samples are shaded within each covered pixel.</param>
        public void MinSampleShading(float value)
        {
            GetDelegateFor<glMinSampleShading>()(value);
        }

        public const uint GL_SAMPLE_SHADING = 0x8C36;
        public const uint GL_MIN_SAMPLE_SHADING_VALUE = 0x8C37;

        #endregion

        #region ARB_tessellation_shader
        
        private delegate void glPatchParameteri(uint pname, int value);
        private delegate void glPatchParameterfv(uint pname, IntPtr values);

        /// <summary>
        /// Specifies the parameters for patch primitives.
        /// </summary>
        /// <param name="pname">Specifies the name of the parameter to set. The symbolc constants GL_PATCH_VERTICES​, GL_PATCH_DEFAULT_OUTER_LEVEL​, and GL_PATCH_DEFAULT_INNER_LEVEL​ are accepted.</param>
        /// <param name="value">Specifies the new value for the parameter given by pname​.</param>
        public void PatchParameter(uint pname, int value)
        {
            GetDelegateFor<glPatchParameteri>()(pname, value);
        }

        /// <summary>
        /// Specifies the parameters for patch primitives.
        /// </summary>
        /// <param name="pname">Specifies the name of the parameter to set. The symbolc constants GL_PATCH_VERTICES​, GL_PATCH_DEFAULT_OUTER_LEVEL​, and GL_PATCH_DEFAULT_INNER_LEVEL​ are accepted.</param>
        /// <param name="values">Specifies the address of an array containing the new values for the parameter given by pname​.</param>
        public void PatchParameter(uint pname, IntPtr values)
        {
            GetDelegateFor<glPatchParameterfv>()(pname, values);
        }

        public const uint GL_PATCHES = 0xE;
        public const uint GL_PATCH_VERTICES = 0x8E72;
        public const uint GL_PATCH_DEFAULT_INNER_LEVEL = 0x8E73;
        public const uint GL_PATCH_DEFAULT_OUTER_LEVEL = 0x8E74;
        public const uint GL_TESS_CONTROL_OUTPUT_VERTICES = 0x8E75;
        public const uint GL_TESS_GEN_MODE = 0x8E76;
        public const uint GL_TESS_GEN_SPACING = 0x8E77;
        public const uint GL_TESS_GEN_VERTEX_ORDER = 0x8E78;
        public const uint GL_TESS_GEN_POINT_MODE = 0x8E79;
        public const uint GL_ISOLINES = 0x8E7A;
        public const uint GL_FRACTIONAL_ODD = 0x8E7B;
        public const uint GL_FRACTIONAL_EVEN = 0x8E7C;
        public const uint GL_MAX_PATCH_VERTICES = 0x8E7D;
        public const uint GL_MAX_TESS_GEN_LEVEL = 0x8E7E;
        public const uint GL_MAX_TESS_CONTROL_UNIFORM_COMPONENTS = 0x8E7F;
        public const uint GL_MAX_TESS_EVALUATION_UNIFORM_COMPONENTS = 0x8E80;
        public const uint GL_MAX_TESS_CONTROL_TEXTURE_IMAGE_UNITS = 0x8E81;
        public const uint GL_MAX_TESS_EVALUATION_TEXTURE_IMAGE_UNITS = 0x8E82;
        public const uint GL_MAX_TESS_CONTROL_OUTPUT_COMPONENTS = 0x8E83;
        public const uint GL_MAX_TESS_PATCH_COMPONENTS = 0x8E84;
        public const uint GL_MAX_TESS_CONTROL_TOTAL_OUTPUT_COMPONENTS = 0x8E85;
        public const uint GL_MAX_TESS_EVALUATION_OUTPUT_COMPONENTS = 0x8E86;
        public const uint GL_MAX_TESS_CONTROL_UNIFORM_BLOCKS = 0x8E89;
        public const uint GL_MAX_TESS_EVALUATION_UNIFORM_BLOCKS = 0x8E8A;
        public const uint GL_MAX_TESS_CONTROL_INPUT_COMPONENTS = 0x886C;
        public const uint GL_MAX_TESS_EVALUATION_INPUT_COMPONENTS = 0x886D;
        public const uint GL_MAX_COMBINED_TESS_CONTROL_UNIFORM_COMPONENTS = 0x8E1E;
        public const uint GL_MAX_COMBINED_TESS_EVALUATION_UNIFORM_COMPONENTS = 0x8E1F;
        public const uint GL_UNIFORM_BLOCK_REFERENCED_BY_TESS_CONTROL_SHADER = 0x84F0;
        public const uint GL_UNIFORM_BLOCK_REFERENCED_BY_TESS_EVALUATION_SHADER = 0x84F1;
        public const uint GL_TESS_EVALUATION_SHADER = 0x8E87;
        public const uint GL_TESS_CONTROL_SHADER = 0x8E88;

        #endregion

        #region ARB_texture_buffer_object_rgb32

        //  No new tokens or functions.

        #endregion

        #region ARB_texture_cube_map_array

        public const uint GL_TEXTURE_CUBE_MAP_ARRAY = 0x9009;
        public const uint GL_TEXTURE_BINDING_CUBE_MAP_ARRAY = 0x900A;
        public const uint GL_PROXY_TEXTURE_CUBE_MAP_ARRAY = 0x900B;
        public const uint GL_SAMPLER_CUBE_MAP_ARRAY = 0x900C;
        public const uint GL_SAMPLER_CUBE_MAP_ARRAY_SHADOW = 0x900D;
        public const uint GL_INT_SAMPLER_CUBE_MAP_ARRAY = 0x900E;
        public const uint GL_UNSIGNED_INT_SAMPLER_CUBE_MAP_ARRAY = 0x900F;

        #endregion

        #region ARB_transform_feedback2
        
        private delegate void glBindTransformFeedback(uint target, uint id);
        private delegate void glDeleteTransformFeedbacks(int n, uint[] ids);
        private delegate void glGenTransformFeedbacks(int n, uint[] ids);
        private delegate bool glIsTransformFeedback(uint id);
        private delegate void glPauseTransformFeedback();
        private delegate void glResumeTransformFeedback();
        private delegate void glDrawTransformFeedback(uint mode, uint id);

        /// <summary>
        /// Bind a transform feedback object.
        /// </summary>
        /// <param name="target">Specifies the target to which to bind the transform feedback object id. target must be GL_TRANSFORM_FEEDBACK.</param>
        /// <param name="id">Specifies the name of a transform feedback object reserved by glGenTransformFeedbacks.</param>
        public void BindTransformFeedback(uint target, uint id)
        {
            GetDelegateFor<glBindTransformFeedback>()(target, id);
        }

        /// <summary>
        /// Delete transform feedback objects.
        /// </summary>
        /// <param name="n">Specifies the number of transform feedback objects to delete.</param>
        /// <param name="ids">Specifies an array of names of transform feedback objects to delete.</param>
        public void DeleteTransformFeedbacks(int n, uint[] ids)
        {
            GetDelegateFor<glDeleteTransformFeedbacks>()(n, ids);
        }

        /// <summary>
        /// Reserve transform feedback object names.
        /// </summary>
        /// <param name="n">Specifies the number of transform feedback object names to reserve.</param>
        /// <param name="ids">Specifies an array of into which the reserved names will be written.</param>
        public void GenTransformFeedbacks(int n, uint[] ids)
        {
            GetDelegateFor<glGenTransformFeedbacks>()(n, ids);
        }

        /// <summary>
        /// Determine if a name corresponds to a transform feedback object.
        /// </summary>
        /// <param name="id">Specifies a value that may be the name of a transform feedback object.</param>
        /// <returns>glIsTransformFeedback returns GL_TRUE if id is currently the name of a transform feedback object. If id is zero, or if id is not the name of a transform feedback object, or if an error occurs, glIsTransformFeedback returns GL_FALSE. If id is a name returned by glGenTransformFeedbacks, but that has not yet been bound through a call to glBindTransformFeedback, then the name is not a transform feedback object and glIsTransformFeedback returns GL_FALSE.</returns>
        public bool IsTransformFeedback(uint id)
        {
            return GetDelegateFor<glIsTransformFeedback>()(id);
        }

        /// <summary>
        /// Pause transform feedback operations.
        /// </summary>
        public void PauseTransformFeedback()
        {
            GetDelegateFor<glPauseTransformFeedback>()();
        }

        /// <summary>
        /// Resume transform feedback operations.
        /// </summary>
        public void ResumeTransformFeedback()
        {
            GetDelegateFor<glResumeTransformFeedback>()();
        }

        /// <summary>
        /// Render primitives using a count derived from a transform feedback object.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render. Symbolic constants GL_POINTS​, GL_LINE_STRIP​, GL_LINE_LOOP​, GL_LINES​, GL_LINE_STRIP_ADJACENCY​, GL_LINES_ADJACENCY​, GL_TRIANGLE_STRIP​, GL_TRIANGLE_FAN​, GL_TRIANGLES​, GL_TRIANGLE_STRIP_ADJACENCY​, GL_TRIANGLES_ADJACENCY​, and GL_PATCHES​ are accepted.</param>
        /// <param name="id">Specifies the name of a transform feedback object from which to retrieve a primitive count.</param>
        public void DrawTransformFeedback(uint mode, uint id)
        {
            GetDelegateFor<glDrawTransformFeedback>()(mode, id);
        }

        public const uint GL_TRANSFORM_FEEDBACK = 0x8E22;
        public const uint GL_TRANSFORM_FEEDBACK_BUFFER_PAUSED = 0x8E23;
        public const uint GL_TRANSFORM_FEEDBACK_BUFFER_ACTIVE = 0x8E24;
        public const uint GL_TRANSFORM_FEEDBACK_BINDING = 0x8E25;

        #endregion

        #region
        
        private delegate void glDrawTransformFeedbackStream(uint mode, uint id, uint stream);
        private delegate void glBeginQueryIndexed(uint target, uint index, uint id);
        private delegate void glEndQueryIndexed(uint target, uint index);
        private delegate void glGetQueryIndexediv(uint target, uint index, uint pname, int[] @params);

        /// <summary>
        /// Render primitives using a count derived from a specifed stream of a transform feedback object.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render. Symbolic constants GL_POINTS, GL_LINE_STRIP, GL_LINE_LOOP, GL_LINES, GL_LINE_STRIP_ADJACENCY, GL_LINES_ADJACENCY, GL_TRIANGLE_STRIP, GL_TRIANGLE_FAN, GL_TRIANGLES, GL_TRIANGLE_STRIP_ADJACENCY, GL_TRIANGLES_ADJACENCY, and GL_PATCHES are accepted.</param>
        /// <param name="id">Specifies the name of a transform feedback object from which to retrieve a primitive count.</param>
        /// <param name="stream">Specifies the index of the transform feedback stream from which to retrieve a primitive count.</param>
        public void DrawTransformFeedbackStream(uint mode, uint id, uint stream)
        {
            GetDelegateFor<glDrawTransformFeedbackStream>()(mode, id, stream);
        }

        /// <summary>
        /// Delimit the boundaries of a query object on an indexed target.
        /// </summary>
        /// <param name="target">Specifies the target type of query object established between glBeginQueryIndexed and the subsequent glEndQueryIndexed. The symbolic constant must be one of GL_SAMPLES_PASSED, GL_ANY_SAMPLES_PASSED, GL_PRIMITIVES_GENERATED, GL_TRANSFORM_FEEDBACK_PRIMITIVES_WRITTEN, or GL_TIME_ELAPSED.</param>
        /// <param name="index">Specifies the index of the query target upon which to begin the query.</param>
        /// <param name="id">Specifies the name of a query object.</param>
        public void BeginQueryIndexed(uint target, uint index, uint id)
        {
            GetDelegateFor<glBeginQueryIndexed>()(target, index, id);
        }

        /// <summary>
        /// Delimit the boundaries of a query object on an indexed target.
        /// </summary>
        /// <param name="target">Specifies the target type of query object established between glBeginQueryIndexed and the subsequent glEndQueryIndexed. The symbolic constant must be one of GL_SAMPLES_PASSED, GL_ANY_SAMPLES_PASSED, GL_PRIMITIVES_GENERATED, GL_TRANSFORM_FEEDBACK_PRIMITIVES_WRITTEN, or GL_TIME_ELAPSED.</param>
        /// <param name="index">Specifies the index of the query target upon which to begin the query.</param>
        public void EndQueryIndexed(uint target, uint index)
        {
            GetDelegateFor<glEndQueryIndexed>()(target, index);
        }

        /// <summary>
        /// Return parameters of an indexed query object target.
        /// </summary>
        /// <param name="target">Specifies a query object target. Must be GL_SAMPLES_PASSED, GL_ANY_SAMPLES_PASSED, GL_ANY_SAMPLES_PASSED_CONSERVATIVE GL_PRIMITIVES_GENERATED, GL_TRANSFORM_FEEDBACK_PRIMITIVES_WRITTEN, GL_TIME_ELAPSED, or GL_TIMESTAMP.</param>
        /// <param name="index">Specifies the index of the query object target.</param>
        /// <param name="pname">Specifies the symbolic name of a query object target parameter. Accepted values are GL_CURRENT_QUERY or GL_QUERY_COUNTER_BITS.</param>
        /// <param name="params">Returns the requested data.</param>
        public void GetQueryIndexed(uint target, uint index, uint pname, int[] @params)
        {
            GetDelegateFor<glGetQueryIndexediv>()(target, index, pname, @params);
        }

        public const uint GL_MAX_TRANSFORM_FEEDBACK_BUFFERS = 0x8E70;

        //  ARB_gpu_shader5
        //public const uint GL_MAX_VERTEX_STREAMS = 0x8E71;

        #endregion

        #region 
        
        private delegate void glBlendEquationi(uint buf, uint mode);
        private delegate void glBlendEquationSeparatei(uint buf, uint modeRGB, uint modeAlpha);
        private delegate void glBlendFunci(uint buf, uint src, uint dst);
        private delegate void glBlendFuncSeparatei(uint buf, uint srcRGB, uint dstRGB, uint srcAlpha, uint dstAlpha);

        /// <summary>
        /// Specify the equation used for both the RGB blend equation and the Alpha blend equation.
        /// </summary>
        /// <param name="buf">For glBlendEquationi, specifies the index of the draw buffer for which to set the blend equation.</param>
        /// <param name="mode">Specifies how source and destination colors are combined. It must be GL_FUNC_ADD, GL_FUNC_SUBTRACT, GL_FUNC_REVERSE_SUBTRACT, GL_MIN, GL_MAX.</param>
        public void BlendEquation(uint buf, uint mode)
        {
            GetDelegateFor<glBlendEquationi>()(buf, mode);
        }

        /// <summary>
        /// Set the RGB blend equation and the alpha blend equation separately.
        /// </summary>
        /// <param name="buf">Specifies the index of the draw buffer for which to set the blend equations.</param>
        /// <param name="modeRGB">Specifies the RGB blend equation, how the red, green, and blue components of the source and destination colors are combined. It must be GL_FUNC_ADD​, GL_FUNC_SUBTRACT​, GL_FUNC_REVERSE_SUBTRACT​, GL_MIN​, GL_MAX​.</param>
        /// <param name="modeAlpha">Specifies the alpha blend equation, how the alpha component of the source and destination colors are combined. It must be GL_FUNC_ADD​, GL_FUNC_SUBTRACT​, GL_FUNC_REVERSE_SUBTRACT​, GL_MIN​, GL_MAX​.</param>
        public void BlendEquationSeparate(uint buf, uint modeRGB, uint modeAlpha)
        {
            GetDelegateFor<glBlendEquationSeparatei>()(buf, modeRGB, modeAlpha);
        }

        /// <summary>
        /// Specify pixel arithmetic for RGB and alpha components separately.
        /// </summary>
        /// <param name="buf">For glBlendFunci, specifies the index of the draw buffer for which to set the blend function.</param>
        /// <param name="src">Specifies how the red, green, blue, and alpha source blending factors are computed. The initial value is GL_ONE​.</param>
        /// <param name="dst">Specifies how the red, green, blue, and alpha destination blending factors are computed. The initial value is GL_ZERO​.</param>
        public void BlendFunc(uint buf, uint src, uint dst)
        {
            GetDelegateFor<glBlendFunci>()(buf, src, dst);
        }

        /// <summary>
        /// Specify pixel arithmetic for RGB and alpha components separately.
        /// </summary>
        /// <param name="buf">Specifies the index of the draw buffer for which to set the blend functions.</param>
        /// <param name="srcRGB">Specifies how the red, green, and blue blending factors are computed. The initial value is GL_ONE​.</param>
        /// <param name="dstRGB">Specifies how the red, green, and blue destination blending factors are computed. The initial value is GL_ZERO​.</param>
        /// <param name="srcAlpha">Specified how the alpha source blending factor is computed. The initial value is GL_ONE​.</param>
        /// <param name="dstAlpha">Specified how the alpha destination blending factor is computed. The initial value is GL_ZERO​.</param>
        public void BlendFuncSeparate(uint buf, uint srcRGB, uint dstRGB, uint srcAlpha, uint dstAlpha)
        {
            GetDelegateFor<glBlendFuncSeparatei>()(buf, srcRGB, dstRGB, srcAlpha, dstAlpha);
        }

        #endregion
    }

// ReSharper restore InconsistentNaming
}
