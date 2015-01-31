using System;
using System.Text;
using SharpGL.Enumerations;

namespace SharpGL
{
// ReSharper disable InconsistentNaming

    public partial class OpenGL
    {
        #region ARB_buffer_storage

        private delegate void glBufferStorage(uint target, int size, IntPtr data, uint flags);
        private delegate void glNamedBufferStorage(uint buffer, int size, IntPtr data, uint flags);

        public const uint GL_MAP_PERSISTENT_BIT = 0x0040;
        public const uint GL_MAP_COHERENT_BIT = 0x0080;
        public const uint GL_DYNAMIC_STORAGE_BIT = 0x0100;
        public const uint GL_CLIENT_STORAGE_BIT = 0x0200;
        public const uint GL_BUFFER_IMMUTABLE_STORAGE = 0x821F;
        public const uint GL_BUFFER_STORAGE_FLAGS = 0x8220;
        public const uint GL_CLIENT_MAPPED_BUFFER_BARRIER_BIT = 0x00004000;

        /// <summary>
        /// Creates and initializes a buffer object's immutable data store.
        /// </summary>
        /// <param name="target">Specifies the target buffer object. The symbolic constant must be GL_ARRAY_BUFFER​, GL_ATOMIC_COUNTER_BUFFER​, GL_COPY_READ_BUFFER​, GL_COPY_WRITE_BUFFER​, GL_DRAW_INDIRECT_BUFFER​, GL_DISPATCH_INDIRECT_BUFFER​, GL_ELEMENT_ARRAY_BUFFER​, GL_PIXEL_PACK_BUFFER​, GL_PIXEL_UNPACK_BUFFER​, GL_QUERY_BUFFER​, GL_SHADER_STORAGE_BUFFER​, GL_TEXTURE_BUFFER​, GL_TRANSFORM_FEEDBACK_BUFFER​, or GL_UNIFORM_BUFFER​.</param>
        /// <param name="size">Specifies the size in bytes of the buffer object's new data store.</param>
        /// <param name="data">Specifies a pointer to data that will be copied into the data store for initialization, or NULL​ if no data is to be copied.</param>
        /// <param name="flags">Specifies the intended usage of the buffer's data store. Must be a bitwise combination of the following flags. GL_DYNAMIC_STORAGE_BIT​, GL_MAP_READ_BIT​GL_MAP_WRITE_BIT​, GL_MAP_PERSISTENT_BIT​, GL_MAP_COHERENT_BIT​, and GL_CLIENT_STORAGE_BIT​.</param>
        public void BufferStorage(uint target, int size, IntPtr data, uint flags)
        {
            GetDelegateFor<glBufferStorage>()(target, size, data, flags);
        }

        /// <summary>
        /// Creates and initializes a buffer object's immutable data store.
        /// </summary>
        /// <param name="buffer">Specifies the named buffer object.</param>
        /// <param name="size">Specifies the size in bytes of the buffer object's new data store.</param>
        /// <param name="data">Specifies a pointer to data that will be copied into the data store for initialization, or NULL​ if no data is to be copied.</param>
        /// <param name="flags">Specifies the intended usage of the buffer's data store. Must be a bitwise combination of the following flags. GL_DYNAMIC_STORAGE_BIT​, GL_MAP_READ_BIT​GL_MAP_WRITE_BIT​, GL_MAP_PERSISTENT_BIT​, GL_MAP_COHERENT_BIT​, and GL_CLIENT_STORAGE_BIT​.</param>
        public void NamedBufferStorage(uint buffer, int size, IntPtr data, uint flags)
        {
            GetDelegateFor<glNamedBufferStorage>()(buffer, size, data, flags);
        }

        #endregion

        #region ARB_clear_texture

        private delegate void glClearTexImage(uint texture, int level, uint format, uint type, IntPtr data);
        private delegate void glClearTexSubImage(uint texture, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, uint type, IntPtr data);

        public const uint GL_CLEAR_TEXTURE = 0x9365;

        /// <summary>
        /// Fills all a texture image with a constant value
        /// </summary>
        /// <param name="texture">The name of an existing texture object containing the image to be cleared.</param>
        /// <param name="level">The level of <paramref name="texture"/> containing the region to be cleared.</param>
        /// <param name="format">The format of the data whose address in memory is given by <paramref name="data"/>.</param>
        /// <param name="type">The type of the data whose address in memory is given by <paramref name="data"/>.</param>
        /// <param name="data">The address in memory of the data to be used to clear the specified region.</param>
        public void ClearTexImage(uint texture, int level, uint format, uint type, IntPtr data)
        {
            GetDelegateFor<glClearTexImage>()(texture, level, format, type, data);
        }

        /// <summary>
        /// Fills all or part of a texture image with a constant value
        /// </summary>
        /// <param name="texture">The name of an existing texture object containing the image to be cleared.</param>
        /// <param name="level">The level of <paramref name="texture"/> containing the region to be cleared.</param>
        /// <param name="xoffset">The coordinate of the left edge of the region to be cleared.</param>
        /// <param name="yoffset">The coordinate of the lower edge of the region to be cleared.</param>
        /// <param name="zoffset">The coordinate of the front of the region to be cleared.</param>
        /// <param name="width">The width of the region to be cleared.</param>
        /// <param name="height">The height of the region to be cleared.</param>
        /// <param name="depth">The depth of the region to be cleared.</param>
        /// <param name="format">The format of the data whose address in memory is given by <paramref name="data"/>.</param>
        /// <param name="type">The type of the data whose address in memory is given by <paramref name="data"/>.</param>
        /// <param name="data">The address in memory of the data to be used to clear the specified region.</param>
        public void ClearTexSubImage(uint texture, int level, int xoffset, int yoffset, int zoffset, int width,
            int height, int depth, uint format, uint type, IntPtr data)
        {
            GetDelegateFor<glClearTexSubImage>()(texture, level, xoffset, yoffset, zoffset, width,
                height, depth, format, type, data);
        }

        #endregion

        #region ARB_enhanced_layouts

        public const uint GL_LOCATION_COMPONENT = 0x934A;
        public const uint GL_TRANSFORM_FEEDBACK_BUFFER_INDEX = 0x934B;
        public const uint GL_TRANSFORM_FEEDBACK_BUFFER_STRIDE = 0x934C;

        #endregion

        #region ARB_multi_bind

        private delegate void glBindBuffersBase(uint target, uint first, int count, uint[] buffers);
        private delegate void glBindBuffersRange(uint target, uint first, int count, uint[] buffers, int[] offsets, int[] sizes);
        private delegate void glBindTextures(uint first, int count, uint[] textures);
        private delegate void glBindSamplers(uint first, int count, uint[] samplers);
        private delegate void glBindImageTextures(uint first, int count, uint[] textures);
        private delegate void glBindVertexBuffers(uint first, int count, uint[] buffers, int[] offsets, int[] strides);

        /// <summary>
        /// Bind one or more buffer objects to a sequence of indexed buffer targets
        /// </summary>
        /// <param name="target">Specify the target of the bind operation. target must be one of GL_ATOMIC_COUNTER_BUFFER, GL_TRANSFORM_FEEDBACK_BUFFER, GL_UNIFORM_BUFFER or GL_SHADER_STORAGE_BUFFER.</param>
        /// <param name="first">Specify the index of the first binding point within the array specified by target.</param>
        /// <param name="count">Specify the number of contiguous binding points to which to bind buffers.</param>
        /// <param name="buffers">A pointer to an array of names of buffer objects to bind to the targets on the specified binding point, or NULL.</param>
        public void BindBuffersBase(uint target, uint first, int count, uint[] buffers)
        {
            GetDelegateFor<glBindBuffersBase>()(target, first, count, buffers);
        }

        /// <summary>
        /// Bind ranges of one or more buffer objects to a sequence of indexed buffer targets.
        /// </summary>
        /// <param name="target">Specify the target of the bind operation. target must be one of GL_ATOMIC_COUNTER_BUFFER, GL_TRANSFORM_FEEDBACK_BUFFER, GL_UNIFORM_BUFFER or GL_SHADER_STORAGE_BUFFER.</param>
        /// <param name="first">Specify the index of the first binding point within the array specified by target.</param>
        /// <param name="count">Specify the number of contiguous binding points to which to bind buffers.</param>
        /// <param name="buffers">A pointer to an array of names of buffer objects to bind to the targets on the specified binding point, or NULL.</param>
        /// <param name="offsets">The offsets.</param>
        /// <param name="sizes">The sizes.</param>
        public void BindBuffersRange(uint target, uint first, int count, uint[] buffers, int[] offsets, int[] sizes)
        {
            GetDelegateFor<glBindBuffersRange>()(target, first, count, buffers, offsets, sizes);
        }

        /// <summary>
        /// bind one or more named textures to a sequence of consecutive texture units.
        /// </summary>
        /// <param name="first">Specifies the first texture unit to which a texture is to be bound.</param>
        /// <param name="count">Specifies the number of textures to bind.</param>
        /// <param name="textures">Specifies the address of an array of names of existing texture objects.</param>
        public void BindTextures(uint first, int count, uint[] textures)
        {
            GetDelegateFor<glBindTextures>()(first, count, textures);
        }

        /// <summary>
        /// Bind one or more named sampler objects to a sequence of consecutive sampler units
        /// </summary>
        /// <param name="first">Specifies the first sampler unit to which a sampler object is to be bound.</param>
        /// <param name="count">Specifies the number of samplers to bind.</param>
        /// <param name="samplers">Specifies the address of an array of names of existing sampler objects.</param>
        public void BindSamplers(uint first, int count, uint[] samplers)
        {
            GetDelegateFor<glBindSamplers>()(first, count, samplers);
        }

        /// <summary>
        /// Bind one or more named texture images to a sequence of consecutive image units
        /// </summary>
        /// <param name="first">Specifies the first image unit to which a texture is to be bound.</param>
        /// <param name="count">Specifies the number of textures to bind.</param>
        /// <param name="textures">Specifies the address of an array of names of existing texture objects.</param>
        public void BindImageTextures(uint first, int count, uint[] textures)
        {
            GetDelegateFor<glBindImageTextures>()(first, count, textures);
        }

        /// <summary>
        /// Attach multiple buffer objects to a vertex array object
        /// </summary>
        /// <param name="first">Specifies the first vertex buffer binding point to which a buffer object is to be bound.</param>
        /// <param name="count">Specifies the number of buffers to bind.</param>
        /// <param name="buffers">Specifies the address of an array of names of existing buffer objects.</param>
        /// <param name="offsets">Specifies the address of an array of offsets to associate with the binding points.</param>
        /// <param name="strides">Specifies the address of an array of strides to associate with the binding points.</param>
        public void BindVertexBuffers(uint first, int count, uint[] buffers, int[] offsets, int[] strides)
        {
            GetDelegateFor<glBindVertexBuffers>()(first, count, buffers, offsets, strides);
        }

        #endregion

        #region ARB_query_buffer_object

        public const uint GL_QUERY_RESULT_NO_WAIT = 0x9194;
        public const uint GL_QUERY_BUFFER = 0x9192;
        public const uint GL_QUERY_BUFFER_BINDING = 0x9193;
        public const uint GL_QUERY_BUFFER_BARRIER_BIT = 0x00008000;

        #endregion

        #region ARB_mirror_texture_clamp_to_edge

        public const uint GL_MIRROR_CLAMP_TO_EDGE = 0x8743;

        #endregion

        #region ARB_texture_stencil8

        //  No new constants/functions.

        #endregion
    }

// ReSharper restore InconsistentNaming
}
