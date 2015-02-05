using System;
using System.Text;

namespace SharpGL
{
// ReSharper disable InconsistentNaming

    public partial class OpenGL
    {
        #region ARB_uniform_buffer_object

        private delegate void glGetUniformIndices(uint program, int uniformCount, string[] uniformNames, uint[] uniformIndices);

        private delegate void glGetActiveUniformsiv(uint program, int uniformCount, uint[] uniformIndices, uint pname, int[] @params);

        private delegate void glGetActiveUniformName(uint program, uint uniformIndex, int bufSize, out int length, StringBuilder uniformName);

        private delegate uint glGetUniformBlockIndex(uint program, string uniformBlockName);

        private delegate void glGetActiveUniformBlockiv(uint program, uint uniformBlockIndex, uint pname, int[] @params);

        private delegate void glGetActiveUniformBlockName(uint program, uint uniformBlockIndex, int bufSize, out int length, StringBuilder uniformBlockName);

        private delegate void glBindBufferRange(uint target, uint index, uint buffer, int offset, int size);

        private delegate void glBindBufferBase(uint target, uint index, uint buffer);

        private delegate void glGetIntegeri_v(uint target, uint index, int[] data);

        private delegate void glUniformBlockBinding(uint program, uint uniformBlockIndex, uint uniformBlockBinding);

        /// <summary>
        /// Retrieve the index of a named uniform block
        /// </summary>
        /// <param name="program">Specifies the name of a program containing uniforms whose indices to query.</param>
        /// <param name="uniformCount">Specifies the number of uniforms whose indices to query.</param>
        /// <param name="uniformNames">Specifies the address of an array of pointers to buffers containing the names of the queried uniforms.</param>
        /// <param name="uniformIndices">Specifies the address of an array that will receive the indices of the uniforms.</param>
        public void GetUniformIndices(uint program, int uniformCount, string[] uniformNames, uint[] uniformIndices)
        {
            GetDelegateFor<glGetUniformIndices>()(program, uniformCount, uniformNames, uniformIndices);
        }

        /// <summary>
        /// Returns information about several active uniform variables for the specified program object.
        /// </summary>
        /// <param name="program">Specifies the program object to be queried.</param>
        /// <param name="uniformCount">Specifies both the number of elements in the array of indices uniformIndices and the number of parameters written to params upon successful return.</param>
        /// <param name="uniformIndices">Specifies the address of an array of uniformCount integers containing the indices of uniforms within program whose parameter pname should be queried.</param>
        /// <param name="pname">Specifies the property of each uniform in uniformIndices that should be written into the corresponding element of params.</param>
        /// <param name="params">Specifies the address of an array of uniformCount integers which are to receive the value of pname for each uniform in uniformIndices.</param>
        public void GetActiveUniforms(uint program, int uniformCount, uint[] uniformIndices, uint pname, int[] @params)
        {
            GetDelegateFor<glGetActiveUniformsiv>()(program, uniformCount, uniformIndices, pname, @params);
        }

        /// <summary>
        /// Query the name of an active uniform.
        /// </summary>
        /// <param name="program">Specifies the program containing the active uniform index uniformIndex.</param>
        /// <param name="uniformIndex">Specifies the index of the active uniform whose name to query.</param>
        /// <param name="bufSize">Specifies the size of the buffer, in units of GLchar, of the buffer whose address is specified in uniformName.</param>
        /// <param name="length">Specifies the address of a variable that will receive the number of characters that were or would have been written to the buffer addressed by uniformName.</param>
        /// <param name="uniformName">Specifies the address of a buffer into which the GL will place the name of the active uniform at uniformIndex within program.</param>
        public void GetActiveUniformName(uint program, uint uniformIndex, int bufSize, out int length,
            out string uniformName)
        {
            var builder = new StringBuilder(bufSize);
            GetDelegateFor<glGetActiveUniformName>()(program, uniformIndex, bufSize, out length, builder);
            uniformName = builder.ToString();
        }

        /// <summary>
        /// Retrieve the index of a named uniform block.
        /// </summary>
        /// <param name="program">Specifies the name of a program containing the uniform block.</param>
        /// <param name="uniformBlockName">Specifies the address an array of characters to containing the name of the uniform block whose index to retrieve.</param>
        /// <returns>glGetUniformBlockIndex returns the uniform block index for the uniform block named uniformBlockName of program. If uniformBlockName does not identify an active uniform block of program, glGetUniformBlockIndex returns the special identifier, GL_INVALID_INDEX. Indices of the active uniform blocks of a program are assigned in consecutive order, beginning with zero.</returns>
        public uint GetUniformBlockIndex(uint program, string uniformBlockName)
        {
            return GetDelegateFor<glGetUniformBlockIndex>()(program, uniformBlockName);
        }

        /// <summary>
        /// Query information about an active uniform block.
        /// </summary>
        /// <param name="program">Specifies the name of a program containing the uniform block.</param>
        /// <param name="uniformBlockIndex">Specifies the index of the uniform block within program.</param>
        /// <param name="pname">Specifies the name of the parameter to query.</param>
        /// <param name="params">Specifies the address of a variable to receive the result of the query.</param>
        public void GetActiveUniformBlock(uint program, uint uniformBlockIndex, uint pname, int[] @params)
        {
            GetDelegateFor<glGetActiveUniformBlockiv>()(program, uniformBlockIndex, pname, @params);
        }

        /// <summary>
        /// Retrieve the name of an active uniform block.
        /// </summary>
        /// <param name="program">Specifies the name of a program containing the uniform block.</param>
        /// <param name="uniformBlockIndex">Specifies the index of the uniform block within program.</param>
        /// <param name="bufSize">Specifies the size of the buffer addressed by uniformBlockName.</param>
        /// <param name="length">Specifies the address of a variable to receive the number of characters that were written to uniformBlockName.</param>
        /// <param name="uniformBlockName">Specifies the address an array of characters to receive the name of the uniform block at uniformBlockIndex.</param>
        public void GetActiveUniformBlockName(uint program, uint uniformBlockIndex, int bufSize, out int length,
            out string uniformBlockName)
        {
            var builder = new StringBuilder(bufSize);
            GetDelegateFor<glGetActiveUniformBlockName>()(program, uniformBlockIndex, bufSize, out length, builder);
            uniformBlockName = builder.ToString();
        }
        
        /// <summary>
        /// Bind a range within a buffer object to an indexed buffer target.
        /// </summary>
        /// <param name="target">Specify the target of the bind operation. target must be either GL_TRANSFORM_FEEDBACK_BUFFER or GL_UNIFORM_BUFFER.</param>
        /// <param name="index">Specify the index of the binding point within the array specified by target.</param>
        /// <param name="buffer">The name of a buffer object to bind to the specified binding point.</param>
        /// <param name="offset">The starting offset in basic machine units into the buffer object buffer.</param>
        /// <param name="size">The amount of data in machine units that can be read from the buffet object while used as an indexed target.</param>
        public void BindBufferRange(uint target, uint index, uint buffer, int offset, int size)
        {
            GetDelegateFor<glBindBufferRange>()(target, index, buffer, offset, size);
        }

        /// <summary>
        /// Bind a buffer object to an indexed buffer target.
        /// </summary>
        /// <param name="target">Specify the target of the bind operation. target must be one of GL_ATOMIC_COUNTER_BUFFER, GL_TRANSFORM_FEEDBACK_BUFFER, GL_UNIFORM_BUFFER or GL_SHADER_STORAGE_BUFFER.</param>
        /// <param name="index">Specify the index of the binding point within the array specified by target.</param>
        /// <param name="buffer">The name of a buffer object to bind to the specified binding point.</param>
        public void BindBufferBase(uint target, uint index, uint buffer)
        {
            GetDelegateFor<glBindBufferBase>()(target, index, buffer);
        }

        /// <summary>
        /// Return the value or values of a selected parameter.
        /// </summary>
        /// <param name="target">Specifies the parameter value to be returned for indexed versions of glGet. The symbolic constants in the list below are accepted.</param>
        /// <param name="index">Specifies the index of the particular element being queried.</param>
        /// <param name="data">Returns the value or values of the specified parameter.</param>
        public void GetInteger(uint target, uint index, int[] data)
        {
            GetDelegateFor<glGetIntegeri_v>()(target, index, data);
        }

        /// <summary>
        /// Assign a binding point to an active uniform block.
        /// </summary>
        /// <param name="program">The name of a program object containing the active uniform block whose binding to assign.</param>
        /// <param name="uniformBlockIndex">The index of the active uniform block within program whose binding to assign.</param>
        /// <param name="uniformBlockBinding">Specifies the binding point to which to bind the uniform block with index uniformBlockIndex within program.</param>
        public void UniformBlockBinding(uint program, uint uniformBlockIndex, uint uniformBlockBinding)
        {
            GetDelegateFor<glUniformBlockBinding>()(program, uniformBlockIndex, uniformBlockBinding);
        }

        public const uint GL_UNIFORM_BUFFER = 0x8A11;
        public const uint GL_UNIFORM_BUFFER_BINDING = 0x8A28;
        public const uint GL_UNIFORM_BUFFER_START = 0x8A29;
        public const uint GL_UNIFORM_BUFFER_SIZE = 0x8A2A;
        public const uint GL_MAX_VERTEX_UNIFORM_BLOCKS = 0x8A2B;
        public const uint GL_MAX_GEOMETRY_UNIFORM_BLOCKS = 0x8A2C;
        public const uint GL_MAX_FRAGMENT_UNIFORM_BLOCKS = 0x8A2D;
        public const uint GL_MAX_COMBINED_UNIFORM_BLOCKS = 0x8A2E;
        public const uint GL_MAX_UNIFORM_BUFFER_BINDINGS = 0x8A2F;
        public const uint GL_MAX_UNIFORM_BLOCK_SIZE = 0x8A30;
        public const uint GL_MAX_COMBINED_VERTEX_UNIFORM_COMPONENTS = 0x8A31;
        public const uint GL_MAX_COMBINED_GEOMETRY_UNIFORM_COMPONENTS = 0x8A32;
        public const uint GL_MAX_COMBINED_FRAGMENT_UNIFORM_COMPONENTS = 0x8A33;
        public const uint GL_UNIFORM_BUFFER_OFFSET_ALIGNMENT = 0x8A34;
        public const uint GL_ACTIVE_UNIFORM_BLOCK_MAX_NAME_LENGTH = 0x8A35;
        public const uint GL_ACTIVE_UNIFORM_BLOCKS = 0x8A36;
        public const uint GL_UNIFORM_TYPE = 0x8A37;
        public const uint GL_UNIFORM_SIZE = 0x8A38;
        public const uint GL_UNIFORM_NAME_LENGTH = 0x8A39;
        public const uint GL_UNIFORM_BLOCK_INDEX = 0x8A3A;
        public const uint GL_UNIFORM_OFFSET = 0x8A3B;
        public const uint GL_UNIFORM_ARRAY_STRIDE = 0x8A3C;
        public const uint GL_UNIFORM_MATRIX_STRIDE = 0x8A3D;
        public const uint GL_UNIFORM_IS_ROW_MAJOR = 0x8A3E;
        public const uint GL_UNIFORM_BLOCK_BINDING = 0x8A3F;
        public const uint GL_UNIFORM_BLOCK_DATA_SIZE = 0x8A40;
        public const uint GL_UNIFORM_BLOCK_NAME_LENGTH = 0x8A41;
        public const uint GL_UNIFORM_BLOCK_ACTIVE_UNIFORMS = 0x8A42;
        public const uint GL_UNIFORM_BLOCK_ACTIVE_UNIFORM_INDICES = 0x8A43;
        public const uint GL_UNIFORM_BLOCK_REFERENCED_BY_VERTEX_SHADER = 0x8A44;
        public const uint GL_UNIFORM_BLOCK_REFERENCED_BY_GEOMETRY_SHADER = 0x8A45;
        public const uint GL_UNIFORM_BLOCK_REFERENCED_BY_FRAGMENT_SHADER = 0x8A46;
        public const uint GL_INVALID_INDEX = 0xFFFFFFFFu;

        #endregion

        #region ARB_draw_instanced
        
        private delegate void glDrawArraysInstanced(uint mode, int first, int count,
            int primcount);
        private delegate void glDrawElementsInstanced(uint mode, int count, uint type,
            IntPtr indices, int primcount);

        /// <summary>
        /// Draw multiple instances of a range of elements.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render. Symbolic constants GL_POINTS, GL_LINE_STRIP, GL_LINE_LOOP, GL_LINES, GL_TRIANGLE_STRIP, GL_TRIANGLE_FAN, GL_TRIANGLES GL_LINES_ADJACENCY, GL_LINE_STRIP_ADJACENCY, GL_TRIANGLES_ADJACENCY, GL_TRIANGLE_STRIP_ADJACENCY and GL_PATCHES are accepted.</param>
        /// <param name="first">Specifies the starting index in the enabled arrays.</param>
        /// <param name="count">Specifies the number of indices to be rendered.</param>
        /// <param name="primcount">Specifies the number of instances of the specified range of indices to be rendered.</param>
        public void DrawArraysInstanced(uint mode, int first, int count, int primcount)
        {
            GetDelegateFor<glDrawArraysInstanced>()(mode, first, count, primcount);
        }

        /// <summary>
        /// Draw multiple instances of a set of elements.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render. Symbolic constants GL_POINTS, GL_LINE_STRIP, GL_LINE_LOOP, GL_LINES, GL_LINE_STRIP_ADJACENCY, GL_LINES_ADJACENCY, GL_TRIANGLE_STRIP, GL_TRIANGLE_FAN, GL_TRIANGLES, GL_TRIANGLE_STRIP_ADJACENCY, GL_TRIANGLES_ADJACENCY and GL_PATCHES are accepted.</param>
        /// <param name="count">Specifies the number of elements to be rendered.</param>
        /// <param name="type">Specifies the type of the values in indices. Must be one of GL_UNSIGNED_BYTE, GL_UNSIGNED_SHORT, or GL_UNSIGNED_INT.</param>
        /// <param name="indices">Specifies a pointer to the location where the indices are stored.</param>
        /// <param name="primcount">Specifies the number of instances of the specified range of indices to be rendered.</param>
        public void DrawElementsInstanced(uint mode, int count, uint type,
            IntPtr indices, int primcount)
        {
            GetDelegateFor<glDrawElementsInstanced>()(mode, count, type, indices, primcount);            
        }
        

        #endregion

        #region EXT_copy_buffer (ARB_copy_buffer)

        //  EXT_copy_buffer is missing, so ARB_copy_buffer is used.
        private delegate void glCopyBufferSubData(uint readTarget, uint writeTarget, int readOffset, int writeOffset, int size);

        /// <summary>
        /// Copy all or part of the data store of a buffer object to the data store of another buffer object.
        /// </summary>
        /// <param name="readTarget">Specifies the target to which the source buffer object is bound for glCopyBufferSubData.</param>
        /// <param name="writeTarget">Specifies the target to which the destination buffer object is bound for glCopyBufferSubData.</param>
        /// <param name="readOffset">Specifies the name of the source buffer object for glCopyNamedBufferSubData.</param>
        /// <param name="writeOffset">Specifies the name of the destination buffer object for glCopyNamedBufferSubData.</param>
        /// <param name="size">Specifies the size, in basic machine units, of the data to be copied from the source buffer object to the destination buffer object.</param>
        public void CopyBufferSubData(uint readTarget, uint writeTarget, int readOffset, int writeOffset, int size)
        {
            GetDelegateFor<glCopyBufferSubData>()(readTarget, writeTarget, readOffset, writeOffset, size);
        }

        public const uint GL_COPY_READ_BUFFER = 0x8F36;
        public const uint GL_COPY_WRITE_BUFFER = 0x8F37;
 
        #endregion

        #region NV_primitive_restart

        //  Immediate mode function below never made it into the OpenGL core, so it's not exposed.
        //  private delegate void glPrimitiveRestartNV();
        private delegate void glPrimitiveRestartIndex(uint index);

        public const uint GL_PRIMITIVE_RESTART = 0x8558;
        public const uint GL_PRIMITIVE_RESTART_INDEX = 0x8559;

        /// <summary>
        /// Specify the primitive restart index.
        /// </summary>
        /// <param name="index">Specifies the value to be interpreted as the primitive restart index.</param>
        public void PrimitiveRestartIndex(uint index)
        {
            GetDelegateFor<glPrimitiveRestartIndex>()(index);
        }

        #endregion

        #region ARB_texture_buffer_object

        private delegate void glTexBuffer(uint target, uint internalformat, uint buffer);

        public const uint GL_TEXTURE_BUFFER = 0x8C2A;
        public const uint GL_MAX_TEXTURE_BUFFER_SIZE = 0x8C2B;
        public const uint GL_TEXTURE_BINDING_BUFFER = 0x8C2C;
        public const uint GL_TEXTURE_BUFFER_DATA_STORE_BINDING = 0x8C2D;
        public const uint GL_TEXTURE_BUFFER_FORMAT = 0x8C2E;

        /// <summary>
        /// Attach the storage for a buffer object to the active buffer texture.
        /// </summary>
        /// <param name="target">Specifies the target of the operation and must be GL_TEXTURE_BUFFER.</param>
        /// <param name="internalformat">Specifies the internal format of the data in the store belonging to buffer.</param>
        /// <param name="buffer">Specifies the name of the buffer object whose storage to attach to the active buffer texture.</param>
        public void TexBuffer(uint target, uint internalformat, uint buffer)
        {
            GetDelegateFor<glTexBuffer>()(target, internalformat, buffer);
        }

        #endregion

        #region ARB_texture_rectangle

        public const uint GL_TEXTURE_RECTANGLE = 0x84F5;
        public const uint GL_TEXTURE_BINDING_RECTANGLE = 0x84F6;
        public const uint GL_PROXY_TEXTURE_RECTANGLE = 0x84F7;
        public const uint GL_MAX_RECTANGLE_TEXTURE_SIZE = 0x84F8;
        public const uint GL_SAMPLER_2D_RECT = 0x8B63;
        public const uint GL_SAMPLER_2D_RECT_SHADOW = 0x8B64;

        #endregion
    }

// ReSharper restore InconsistentNaming
}
