using System;

namespace SharpGL
{
// ReSharper disable InconsistentNaming

    public partial class OpenGL
    {
        #region ARB_clip_control

        private delegate void glClipControl(uint origin, uint depth);

        public const uint GL_NEGATIVE_ONE_TO_ONE = 0x935E;
        public const uint GL_ZERO_TO_ONE = 0x935F;
        public const uint GL_CLIP_ORIGIN = 0x935C;
        public const uint GL_CLIP_DEPTH_MODE = 0x935D;

        /// <summary>
        /// Control clip coordinate to window coordinate behavior
        /// </summary>
        /// <param name="origin">Specifies the clip control origin. Must be one of GL_LOWER_LEFT or GL_UPPER_LEFT.</param>
        /// <param name="depth">Specifies the clip control depth mode. Must be one of GL_NEGATIVE_ONE_TO_ONE or GL_ZERO_TO_ONE.</param>
        public void ClipControl(uint origin, uint depth)
        {
            GetDelegateFor<glClipControl>()(origin, depth);
        }

        #endregion

        #region ARB_cull_distance

        public const uint GL_MAX_CULL_DISTANCES = 0x82F9;
        public const uint GL_MAX_COMBINED_CLIP_AND_CULL_DISTANCES = 0x82FA;

        #endregion

        #region ARB_ES3_1_compatibility

        private delegate void glMemoryBarrierByRegion(uint barriers);

        /// <summary>
        /// Defines a barrier ordering memory transactions
        /// </summary>
        /// <param name="barriers">Specifies the barriers to insert. Must be a bitwise combination of GL_VERTEX_ATTRIB_ARRAY_BARRIER_BIT, GL_ELEMENT_ARRAY_BARRIER_BIT, GL_UNIFORM_BARRIER_BIT, GL_TEXTURE_FETCH_BARRIER_BIT, GL_SHADER_IMAGE_ACCESS_BARRIER_BIT, GL_COMMAND_BARRIER_BIT, GL_PIXEL_BUFFER_BARRIER_BIT, GL_TEXTURE_UPDATE_BARRIER_BIT, GL_BUFFER_UPDATE_BARRIER_BIT, GL_FRAMEBUFFER_BARRIER_BIT, GL_TRANSFORM_FEEDBACK_BARRIER_BIT, GL_ATOMIC_COUNTER_BARRIER_BIT, or GL_SHADER_STORAGE_BARRIER_BIT. If the special value GL_ALL_BARRIER_BITS is specified, all supported barriers will be inserted.</param>
        public void MemoryBarrierByRegion(uint barriers)
        {
            GetDelegateFor<glMemoryBarrierByRegion>()(barriers);
        }

        #endregion

        #region ARB_conditional_render_inverted

        public const uint GL_QUERY_WAIT_INVERTED = 0x8E17;
        public const uint GL_QUERY_NO_WAIT_INVERTED = 0x8E18;
        public const uint GL_QUERY_BY_REGION_WAIT_INVERTED = 0x8E19;
        public const uint GL_QUERY_BY_REGION_NO_WAIT_INVERTED = 0x8E1A;

        #endregion

        #region ARB_direct_state_access

        private delegate void glCreateTransformFeedbacks(int n, uint[] ids);
        private delegate void glTransformFeedbackBufferBase(uint xfb, uint index, uint buffer);
        private delegate void glTransformFeedbackBufferRange(uint xfb, uint index, uint buffer, int offset, int size);
        private delegate void glGetTransformFeedbackiv(uint xfb, uint pname, out int param);
        private delegate void glGetTransformFeedbacki_v(uint xfb, uint pname, uint index, out int param);
        private delegate void glGetTransformFeedbacki64_v(uint xfb, uint pname, uint index, out Int64 param);
        private delegate void glCreateBuffers(int n, uint[] buffers);
        private delegate void glNamedBufferStorage(uint buffer, int size, IntPtr data, uint flags );
        private delegate void glNamedBufferData(uint buffer, int size, IntPtr data, uint usage);
        private delegate void glNamedBufferSubData(uint buffer, int offset, int size, IntPtr data);
        private delegate void glCopyNamedBufferSubData(uint readBuffer, uint writeBuffer, int readOffset, int writeOffset, int size);
        private delegate void glClearNamedBufferData(uint buffer, uint internalformat, uint format, uint type,  IntPtr data);
        private delegate void glClearNamedBufferSubData(uint buffer, uint internalformat, int offset, int size, uint format, uint type, IntPtr data);
        private delegate IntPtr glMapNamedBuffer(uint buffer, uint access);
        private delegate IntPtr glMapNamedBufferRange(uint buffer, int offset, int length, uint access);
        private delegate bool glUnmapNamedBuffer(uint buffer);
        private delegate void glFlushMappedNamedBufferRange(uint buffer, int offset, int length);
        private delegate void glGetNamedBufferParameteriv(uint buffer, uint pname, out int param);
        private delegate void glGetNamedBufferParameteri64v(uint buffer, uint pname, out IntPtr param);
        private delegate void glGetNamedBufferPointerv(uint buffer, uint pname, out IntPtr @params);
        private delegate void glGetNamedBufferSubData(uint buffer, int offset, int size, IntPtr data);
        private delegate void glCreateFramebuffers(int n, uint[] framebuffers);
        private delegate void glNamedFramebufferRenderbuffer(uint framebuffer, uint attachment, uint renderbuffertarget, uint renderbuffer);
        private delegate void glNamedFramebufferParameteri(uint framebuffer, uint pname, int param);
        private delegate void glNamedFramebufferTexture(uint framebuffer, uint attachment, uint texture, int level);
        private delegate void glNamedFramebufferTextureLayer(uint framebuffer, uint attachment, uint texture, int level, int layer);
        private delegate void glNamedFramebufferDrawBuffer(uint framebuffer, uint mode);
        private delegate void glNamedFramebufferDrawBuffers(uint framebuffer, int n, uint[] bufs);
        private delegate void glNamedFramebufferReadBuffer(uint framebuffer, uint mode);
        private delegate void glInvalidateNamedFramebufferData(uint framebuffer, int numAttachments, uint[] attachments);
        private delegate void glInvalidateNamedFramebufferSubData(uint framebuffer, int numAttachments, uint[] attachments, int x, int y, int width, int height);
        private delegate void glClearNamedFramebufferiv(uint framebuffer, uint buffer, int drawbuffer, int[] value);
        private delegate void glClearNamedFramebufferuiv(uint framebuffer, uint buffer, int drawbuffer, uint[] value);
        private delegate void glClearNamedFramebufferfv(uint framebuffer, uint buffer, int drawbuffer, float[] value);
        private delegate void glClearNamedFramebufferfi(uint framebuffer, uint buffer, float depth, int stencil);
        private delegate void glBlitNamedFramebuffer(uint readFramebuffer, uint drawFramebuffer, int srcX0, int srcY0, int srcX1, int srcY1, int dstX0, int dstY0, int dstX1, int dstY1, uint mask, uint filter);
        private delegate uint glCheckNamedFramebufferStatus(uint framebuffer, uint target);
        private delegate void glGetNamedFramebufferParameteriv(uint framebuffer, uint pname, out int param);
        private delegate void glGetNamedFramebufferAttachmentParameteriv(uint framebuffer, uint attachment, uint pname, out int @params);
        private delegate void glCreateRenderbuffers(int n, uint[] renderbuffers);
        private delegate void glNamedRenderbufferStorage(uint renderbuffer, uint internalformat, int width, int height);
        private delegate void glNamedRenderbufferStorageMultisample(uint renderbuffer, int samples, uint internalformat, int width, int height);
        private delegate void glGetNamedRenderbufferParameteriv(uint renderbuffer, uint pname, out int @params);
        private delegate void glCreateTextures(uint target, int n, uint[] textures);
        private delegate void glTextureBuffer(uint texture, uint internalformat, uint buffer);
        private delegate void glTextureBufferRange(uint texture, uint internalformat, uint buffer, int offset, int size);
        private delegate void glTextureStorage1D(uint texture, int levels, uint internalformat, int width);
        private delegate void glTextureStorage2D(uint texture, int levels, uint internalformat, int width, int height);
        private delegate void glTextureStorage3D(uint texture, int levels, uint internalformat, int width, int height, int depth);
        private delegate void glTextureStorage2DMultisample(uint texture, int samples, uint internalformat, int width, int height, bool fixedsamplelocations);
        private delegate void glTextureStorage3DMultisample(uint texture, int samples, uint internalformat, int width, int height, int depth, bool fixedsamplelocations);
        private delegate void glTextureSubImage1D(uint texture, int level, int xoffset, int width, uint format, uint type, IntPtr pixels);
        private delegate void glTextureSubImage2D(uint texture, int level, int xoffset, int yoffset, int width, int height, uint format, uint type, IntPtr pixels);
        private delegate void glTextureSubImage3D(uint texture, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, uint type, IntPtr pixels);
        private delegate void glCompressedTextureSubImage1D(uint texture, int level, int xoffset, int width, uint format, int imageSize,  IntPtr data);
        private delegate void glCompressedTextureSubImage2D(uint texture, int level, int xoffset, int yoffset, int width, int height, uint format, int imageSize, IntPtr data);
        private delegate void glCompressedTextureSubImage3D(uint texture, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, int imageSize,  IntPtr data);
        private delegate void glCopyTextureSubImage1D(uint texture, int level, int xoffset, int x, int y, int width);
        private delegate void glCopyTextureSubImage2D(uint texture, int level, int xoffset, int yoffset, int x, int y, int width, int height);
        private delegate void glCopyTextureSubImage3D(uint texture, int level, int xoffset, int yoffset, int zoffset, int x, int y, int width, int height);
        private delegate void glTextureParameterf(uint texture, uint pname, float param);
        private delegate void glTextureParameterfv(uint texture, uint pname, float[] param);
        private delegate void glTextureParameteri(uint texture, uint pname, int param);
        private delegate void glTextureParameterIiv(uint texture, uint pname, int[] @params);
        private delegate void glTextureParameterIuiv(uint texture, uint pname, uint[] @params);
        private delegate void glTextureParameteriv(uint texture, uint pname, int[] param);
        private delegate void glGenerateTextureMipmap(uint texture);
        private delegate void glBindTextureUnit(uint unit, uint texture);
        private delegate void glGetTextureImage(uint texture, int level, uint format, uint type, int bufSize, IntPtr pixels);
        private delegate void glGetCompressedTextureImage(uint texture, int level, int bufSize, IntPtr pixels);
        private delegate void glGetTextureLevelParameterfv(uint texture, int level, uint pname, float[] @params);
        private delegate void glGetTextureLevelParameteriv(uint texture, int level, uint pname, int[] @params);
        private delegate void glGetTextureParameterfv(uint texture, uint pname, float[] @params);
        private delegate void glGetTextureParameterIiv(uint texture, uint pname, int[] @params);
        private delegate void glGetTextureParameterIuiv(uint texture, uint pname, uint[] @params);
        private delegate void glGetTextureParameteriv(uint texture, uint pname, int[] @params);
        private delegate void glCreateVertexArrays(int n, uint[] arrays);
        private delegate void glDisableVertexArrayAttrib(uint vaobj, uint index);
        private delegate void glEnableVertexArrayAttrib(uint vaobj, uint index);
        private delegate void glVertexArrayElementBuffer(uint vaobj, uint buffer);
        private delegate void glVertexArrayVertexBuffer(uint vaobj, uint bindingindex, uint buffer, int offset, int stride);
        private delegate void glVertexArrayVertexBuffers(uint vaobj, uint first, int count, uint[] buffers, int[] offsets, int[] strides);
        private delegate void glVertexArrayAttribFormat(uint vaobj, uint attribindex, int size, uint type, bool normalized, uint relativeoffset);
        private delegate void glVertexArrayAttribIFormat(uint vaobj, uint attribindex, int size, uint type, uint relativeoffset);
        private delegate void glVertexArrayAttribLFormat(uint vaobj, uint attribindex, int size, uint type, uint relativeoffset);
        private delegate void glVertexArrayAttribBinding(uint vaobj, uint attribindex, uint bindingindex);
        private delegate void glVertexArrayBindingDivisor(uint vaobj, uint bindingindex, uint divisor);
        private delegate void glGetVertexArrayiv(uint vaobj, uint pname, out int param);
        private delegate void glGetVertexArrayIndexediv(uint vaobj, uint index, uint pname, int[] param);
        private delegate void glGetVertexArrayIndexed64iv(uint vaobj, uint index, uint pname, Int64[] param);
        private delegate void glCreateSamplers(int n, uint[] samplers);
        private delegate void glCreateProgramPipelines(int n, uint[] pipelines);
        private delegate void glCreateQueries(uint target, int n, uint[] ids);
        private delegate void glGetQueryBufferObjectiv(uint id, uint buffer, uint pname, int offset);
        private delegate void glGetQueryBufferObjectuiv(uint id, uint buffer, uint pname, int offset);
        private delegate void glGetQueryBufferObjecti64v(uint id, uint buffer, uint pname, int offset);
        private delegate void glGetQueryBufferObjectui64v(uint id, uint buffer, uint pname, int offset);

        public const uint GL_QUERY_TARGET = 0x82EA;
        public const uint GL_TEXTURE_BINDING = 0x82EB;

        /// <summary>
        /// Create transform feedback objects.
        /// </summary>
        /// <param name="n">Number of transform feedback objects to create.</param>
        /// <param name="ids">Specifies an array in which names of the new transform feedback objects are stored.</param>
        public void CreateTransformFeedbacks(int n, uint[] ids)
        {
            GetDelegateFor<glCreateTransformFeedbacks>()(n, ids);
        }

        /// <summary>
        /// Bind a buffer object to a transform feedback buffer object.
        /// </summary>
        /// <param name="xfb">Name of the transform feedback buffer object.</param>
        /// <param name="index">Index of the binding point within <paramref name="xfb"/>.</param>
        /// <param name="buffer">Name of the buffer object to bind to the specified binding point.</param>
        public void TransformFeedbackBufferBase(uint xfb, uint index, uint buffer)
        {
            GetDelegateFor<glTransformFeedbackBufferBase>()(xfb, index, buffer);
        }

        /// <summary>
        /// Bind a range within a buffer object to a transform feedback buffer object.
        /// </summary>
        /// <param name="xfb">Name of the transform feedback buffer object.</param>
        /// <param name="index">Index of the binding point within <paramref name="xfb"/>.</param>
        /// <param name="buffer">Name of the buffer object to bind to the specified binding point.</param>
        /// <param name="offset">The starting offset in basic machine units into the buffer object.</param>
        /// <param name="size">The amount of data in basic machine units that can be read from or written to the buffer object while used as an indexed target.</param>
        public void TransformFeedbackBufferRange(uint xfb, uint index, uint buffer, int offset, int size)
        {
            GetDelegateFor<glTransformFeedbackBufferRange>()(xfb, index, buffer, offset, size);
        }

        /// <summary>
        /// Query the state of a transform feedback object.
        /// </summary>
        /// <param name="xfb">The name of an existing transform feedback object, or zero for the default transform feedback object.</param>
        /// <param name="pname">Property to use for the query. Must be one of the values: GL_TRANSFORM_FEEDBACK_BUFFER_BINDING, GL_TRANSFORM_FEEDBACK_BUFFER_START, GL_TRANSFORM_FEEDBACK_BUFFER_SIZE, GL_TRANSFORM_FEEDBACK_PAUSED, GL_TRANSFORM_FEEDBACK_ACTIVE.</param>
        /// <param name="param">The address of a buffer into which will be written the requested state information.</param>
        public void GetTransformFeedback(uint xfb, uint pname, out int param)
        {
            GetDelegateFor<glGetTransformFeedbackiv>()(xfb, pname, out param);
        }
        
        /// <summary>
        /// Query the state of a transform feedback object.
        /// </summary>
        /// <param name="xfb">The name of an existing transform feedback object, or zero for the default transform feedback object.</param>
        /// <param name="pname">Property to use for the query. Must be one of the values: GL_TRANSFORM_FEEDBACK_BUFFER_BINDING, GL_TRANSFORM_FEEDBACK_BUFFER_START, GL_TRANSFORM_FEEDBACK_BUFFER_SIZE, GL_TRANSFORM_FEEDBACK_PAUSED, GL_TRANSFORM_FEEDBACK_ACTIVE.</param>
        /// <param name="index">Index of the transform feedback stream (for indexed state).</param>
        /// <param name="param">The address of a buffer into which will be written the requested state information.</param>
        public void GetTransformFeedback(uint xfb, uint pname, uint index, out int param)
        {
            GetDelegateFor<glGetTransformFeedbacki_v>()(xfb, pname, index, out param);
        }
        
        /// <summary>
        /// Query the state of a transform feedback object.
        /// </summary>
        /// <param name="xfb">The name of an existing transform feedback object, or zero for the default transform feedback object.</param>
        /// <param name="pname">Property to use for the query. Must be one of the values: GL_TRANSFORM_FEEDBACK_BUFFER_BINDING, GL_TRANSFORM_FEEDBACK_BUFFER_START, GL_TRANSFORM_FEEDBACK_BUFFER_SIZE, GL_TRANSFORM_FEEDBACK_PAUSED, GL_TRANSFORM_FEEDBACK_ACTIVE.</param>
        /// <param name="index">Index of the transform feedback stream (for indexed state).</param>
        /// <param name="param">The address of a buffer into which will be written the requested state information.</param>
        public void GetTransformFeedback(uint xfb, uint pname, uint index, out Int64 param)
        {
            GetDelegateFor<glGetTransformFeedbacki64_v>()(xfb, pname, index, out param);
        }
        
        /// <summary>
        /// Create buffer objects.
        /// </summary>
        /// <param name="n">Specifies the number of buffer objects to create.</param>
        /// <param name="buffers">Specifies an array in which names of the new buffer objects are stored.</param>
        public void CreateBuffers(int n, uint[] buffers)
        {
            GetDelegateFor<glCreateBuffers>()(n, buffers);
        }

        /// <summary>
        /// Nameds the buffer storage.
        /// </summary>
        /// <param name="buffer">Specifies the name of the buffer object for glNamedBufferStorage function.</param>
        /// <param name="size">Specifies the size in bytes of the buffer object's new data store.</param>
        /// <param name="data">Specifies a pointer to data that will be copied into the data store for initialization, or NULL if no data is to be copied.</param>
        /// <param name="flags">Specifies the size in bytes of the buffer object's new data store.</param>
        public void NamedBufferStorage(uint buffer, int size, IntPtr data, uint flags)
        {
            GetDelegateFor<glNamedBufferStorage>()(buffer, size, data, flags);
        }

        /// <summary>
        /// Creates and initializes a buffer object's data store.
        /// </summary>
        /// <param name="buffer">Specifies the name of the buffer object for glNamedBufferData function.</param>
        /// <param name="size">Specifies the size in bytes of the buffer object's new data store.</param>
        /// <param name="data">Specifies a pointer to data that will be copied into the data store for initialization, or NULL if no data is to be copied..</param>
        /// <param name="usage">Specifies the expected usage pattern of the data store. The symbolic constant must be GL_STREAM_DRAW, GL_STREAM_READ, GL_STREAM_COPY, GL_STATIC_DRAW, GL_STATIC_READ, GL_STATIC_COPY, GL_DYNAMIC_DRAW, GL_DYNAMIC_READ, or GL_DYNAMIC_COPY.</param>
        public void NamedBufferData(uint buffer, int size, IntPtr data, uint usage)
        {
            GetDelegateFor<glNamedBufferData>()(buffer, size, data, usage);
        }

        /// <summary>
        /// Updates a subset of a buffer object's data store.
        /// </summary>
        /// <param name="buffer">Specifies the name of the buffer object for glNamedBufferSubData.</param>
        /// <param name="offset">Specifies the offset into the buffer object's data store where data replacement will begin, measured in bytes.</param>
        /// <param name="size">Specifies the size in bytes of the data store region being replaced.</param>
        /// <param name="data">Specifies a pointer to the new data that will be copied into the data store.</param>
        public void NamedBufferSubData(uint buffer, int offset, int size, IntPtr data)
        {
            GetDelegateFor<glNamedBufferSubData>()(buffer, offset, size, data);
        }

        
        /// <summary>
        /// Copy all or part of the data store of a buffer object to the data store of another buffer object.
        /// </summary>
        /// <param name="readBuffer">Specifies the name of the source buffer object for glCopyNamedBufferSubData.</param>
        /// <param name="writeBuffer">Specifies the name of the destination buffer object for glCopyNamedBufferSubData.</param>
        /// <param name="readOffset">Specifies the offset, in basic machine units, within the data store of the source buffer object at which data will be read.</param>
        /// <param name="writeOffset">Specifies the offset, in basic machine units, within the data store of the destination buffer object at which data will be written.</param>
        /// <param name="size">Specifies the size, in basic machine units, of the data to be copied from the source buffer object to the destination buffer object.</param>
        public void CopyNamedBufferSubData(uint readBuffer, uint writeBuffer, int readOffset, int writeOffset, int size)
        {
            GetDelegateFor<glCopyNamedBufferSubData>()(readBuffer, writeBuffer, readOffset, writeOffset, size);
        }

        /// <summary>
        /// Fill a buffer object's data store with a fixed value
        /// </summary>
        /// <param name="buffer">Specifies the name of the buffer object for glClearNamedBufferData.</param>
        /// <param name="internalformat">The internal format with which the data will be stored in the buffer object.</param>
        /// <param name="format">The format of the data in memory addressed by data.</param>
        /// <param name="type">The type of the data in memory addressed by data.</param>
        /// <param name="data">The address of a memory location storing the data to be replicated into the buffer's data store.</param>
        public void ClearNamedBufferData(uint buffer, uint internalformat, uint format, uint type, IntPtr data)
        {
            GetDelegateFor<glClearNamedBufferData>()(buffer, internalformat, format, type, data);
        }

        /// <summary>
        /// Fill all or part of buffer object's data store with a fixed value.
        /// </summary>
        /// <param name="buffer">Specifies the name of the buffer object for glClearNamedBufferSubData.</param>
        /// <param name="internalformat">The internal format with which the data will be stored in the buffer object.</param>
        /// <param name="offset">The offset in basic machine units into the buffer object's data store at which to start filling.</param>
        /// <param name="size">The size in basic machine units of the range of the data store to fill.  </param>
        /// <param name="format">The format of the data in memory addressed by data.</param>
        /// <param name="type">The type of the data in memory addressed by data.</param>
        /// <param name="data">The address of a memory location storing the data to be replicated into the buffer's data store.</param>
        public void ClearNamedBufferSubData(uint buffer, uint internalformat, int offset, int size, uint format, uint type, IntPtr data)
        {
            GetDelegateFor<glClearNamedBufferSubData>()(buffer, internalformat, offset, size, format, type, data);
        }
        
        /// <summary>
        /// Map all of a buffer object's data store into the client's address space.
        /// </summary>
        /// <param name="buffer">Specifies the name of the buffer object for glMapNamedBuffer.</param>
        /// <param name="access">Specifies the access policy for glMapBuffer and glMapNamedBuffer, indicating whether it will be possible to read from, write to, or both read from and write to the buffer object's mapped data store. The symbolic constant must be GL_READ_ONLY, GL_WRITE_ONLY, or GL_READ_WRITE.</param>
        /// <returns>
        /// If an error is generated, a NULL pointer is returned.
        /// If no error occurs, the returned pointer will reflect an allocation aligned to the value of GL_MIN_MAP_BUFFER_ALIGNMENT basic machine unit.
        /// </returns>
        public IntPtr MapNamedBuffer(uint buffer, uint access)
        {
            return GetDelegateFor<glMapNamedBuffer>()(buffer, access);
        }

        /// <summary>
        /// Map all or part of a buffer object's data store into the client's address space.
        /// </summary>
        /// <param name="buffer">Specifies the name of the buffer object for glMapNamedBufferRange.</param>
        /// <param name="offset">Specifies the starting offset within the buffer of the range to be mapped.</param>
        /// <param name="length">Specifies the length of the range to be mapped.</param>
        /// <param name="access">Specifies a combination of access flags indicating the desired access to the mapped range.</param>
        /// <returns>
        /// If an error occurs, a NULL pointer is returned.
        /// If no error occurs, the returned pointer will reflect an allocation aligned to the value of GL_MIN_MAP_BUFFER_ALIGNMENT basic machine units. Subtracting offset from this returned pointer will always produce a multiple of the value of GL_MIN_MAP_BUFFER_ALIGNMENT.
        /// </returns>
        public IntPtr MapNamedBufferRange(uint buffer, int offset, int length, uint access)
        {
            return GetDelegateFor<glMapNamedBufferRange>()(buffer, offset, length, access);
            
        }

        /// <summary>
        /// Release the mapping of a buffer object's data store into the client's address space.
        /// </summary>
        /// <param name="buffer">Specifies the name of the buffer object for glUnmapNamedBuffer.</param>
        /// <returns>glUnmapBuffer returns GL_TRUE unless the data store contents have become corrupt during the time the data store was mapped. This can occur for system-specific reasons that affect the availability of graphics memory, such as screen mode changes. In such situations, GL_FALSE is returned and the data store contents are undefined. An application must detect this rare condition and reinitialize the data store.</returns>
        public bool UnmapNamedBuffer(uint buffer)
        {
            return GetDelegateFor<glUnmapNamedBuffer>()(buffer);
        }

        /// <summary>
        /// Indicate modifications to a range of a mapped buffer.
        /// </summary>
        /// <param name="buffer">Specifies the name of the buffer object for glFlushMappedNamedBufferRange.</param>
        /// <param name="offset">Specifies the start of the buffer subrange, in basic machine units.</param>
        /// <param name="length">Specifies the length of the buffer subrange, in basic machine units.</param>
        public void FlushMappedNamedBufferRange(uint buffer, int offset, int length)
        {
            GetDelegateFor<glFlushMappedNamedBufferRange>()(buffer, offset, length);
        }

        /// <summary>
        /// Return parameters of a named buffer object
        /// </summary>
        /// <param name="buffer">Specifies the name of the buffer object.</param>
        /// <param name="pname">Specifies the name of the buffer object parameter to query.</param>
        /// <param name="param">Returns the requested parameter.</param>
        public void GetNamedBufferParameter(uint buffer, uint pname, out int param)
        {
            GetDelegateFor<glGetNamedBufferParameteriv>()(buffer, pname, out param);
        }
        
        /// <summary>
        /// Return parameters of a named buffer object
        /// </summary>
        /// <param name="buffer">Specifies the name of the buffer object.</param>
        /// <param name="pname">Specifies the name of the buffer object parameter to query.</param>
        /// <param name="param">Returns the requested parameter.</param>
        public void GetNamedBufferParameter(uint buffer, uint pname, out IntPtr param)
        {
            GetDelegateFor<glGetNamedBufferParameteri64v>()(buffer, pname, out param);
        }

        /// <summary>
        /// Return the pointer to a mapped buffer object's data store.
        /// </summary>
        /// <param name="buffer">Specifies the name of the buffer object for glGetNamedBufferPointerv.</param>
        /// <param name="pname">Specifies the name of the pointer to be returned. Must be GL_BUFFER_MAP_POINTER.</param>
        /// <param name="params">Returns the pointer value specified by pname.</param>
        public void GetNamedBufferPointer(uint buffer, uint pname, out IntPtr @params)
        {
            GetDelegateFor<glGetNamedBufferPointerv>()(buffer, pname, out @params);
        }

        
        /// <summary>
        /// Returns a subset of a buffer object's data store.
        /// </summary>
        /// <param name="buffer">Specifies the name of the buffer object for glGetNamedBufferSubData.</param>
        /// <param name="offset">Specifies the offset into the buffer object's data store from which data will be returned, measured in bytes.</param>
        /// <param name="size">Specifies the size in bytes of the data store region being returned.</param>
        /// <param name="data">Specifies a pointer to the location where buffer object data is returned.</param>
        public void GetNamedBufferSubData(uint buffer, int offset, int size, IntPtr data)
        {
            GetDelegateFor<glGetNamedBufferSubData>()(buffer, offset, size, data);
        }
        
        /// <summary>
        /// Creates the framebuffers.
        /// </summary>
        /// <param name="n">Number of framebuffer objects to create.</param>
        /// <param name="framebuffers">Specifies an array in which names of the new framebuffer objects are stored.</param>
        public void CreateFramebuffers(int n, uint[] framebuffers)
        {
            GetDelegateFor<glCreateFramebuffers>()(n, framebuffers);
        }
        
        /// <summary>
        /// Attach a renderbuffer as a logical buffer of a framebuffer object.
        /// </summary>
        /// <param name="framebuffer">Specifies the name of the framebuffer object for glNamedFramebufferRenderbuffer.</param>
        /// <param name="attachment">Specifies the attachment point of the framebuffer.</param>
        /// <param name="renderbuffertarget">Specifies the renderbuffer target. Must be GL_RENDERBUFFER.</param>
        /// <param name="renderbuffer">Specifies the name of an existing renderbuffer object of type renderbuffertarget to attach.</param>
        public void NamedFramebufferRenderbuffer(uint framebuffer, uint attachment, uint renderbuffertarget, uint renderbuffer)
        {
            GetDelegateFor<glNamedFramebufferRenderbuffer>()(framebuffer, attachment, renderbuffertarget, renderbuffer);
        }
        
        /// <summary>
        /// Set a named parameter of a framebuffer object.
        /// </summary>
        /// <param name="framebuffer">Specifies the name of the framebuffer object for glNamedFramebufferParameteri.</param>
        /// <param name="pname">Specifies the framebuffer parameter to be modified.</param>
        /// <param name="param">The new value for the parameter named pname.</param>
        public void NamedFramebufferParameter(uint framebuffer, uint pname, int param)
        {
            GetDelegateFor<glNamedFramebufferParameteri>()(framebuffer, pname, param);
        }
        
        /// <summary>
        /// Attach a level of a texture object as a logical buffer of a framebuffer object
        /// </summary>
        /// <param name="framebuffer">Specifies the name of the framebuffer object for glNamedFramebufferTexture.</param>
        /// <param name="attachment">Specifies the attachment point of the framebuffer.</param>
        /// <param name="texture">Specifies the name of an existing texture object to attach.</param>
        /// <param name="level">Specifies the mipmap level of the texture object to attach.</param>
        public void NamedFramebufferTexture(uint framebuffer, uint attachment, uint texture, int level)
        {
            GetDelegateFor<glNamedFramebufferTexture>()(framebuffer, attachment, texture, level);
        }

        /// <summary>
        /// Attach a single layer of a texture object as a logical buffer of a framebuffer object.
        /// </summary>
        /// <param name="framebuffer">Specifies the name of the framebuffer object for glNamedFramebufferTextureLayer.</param>
        /// <param name="attachment">Specifies the attachment point of the framebuffer.</param>
        /// <param name="texture">Specifies the name of an existing texture object to attach.</param>
        /// <param name="level">Specifies the mipmap level of the texture object to attach.</param>
        /// <param name="layer">Specifies the layer of the texture object to attach.</param>
        public void NamedFramebufferTextureLayer(uint framebuffer, uint attachment, uint texture, int level, int layer)
        {
            GetDelegateFor<glNamedFramebufferTextureLayer>()(framebuffer, attachment, texture, level, layer);
        }
        
        /// <summary>
        /// Specify which color buffers are to be drawn into.
        /// </summary>
        /// <param name="framebuffer">Specifies the name of the framebuffer object for glNamedFramebufferDrawBuffer function. Must be zero or the name of a framebuffer object.</param>
        /// <param name="mode">For default framebuffer, the argument specifies up to four color buffers to be drawn into. Symbolic constants GL_NONE, GL_FRONT_LEFT, GL_FRONT_RIGHT, GL_BACK_LEFT, GL_BACK_RIGHT, GL_FRONT, GL_BACK, GL_LEFT, GL_RIGHT, and GL_FRONT_AND_BACK are accepted. The initial value is GL_FRONT for single-buffered contexts, and GL_BACK for double-buffered contexts. For framebuffer objects, GL_COLOR_ATTACHMENT$m$ and GL_NONE enums are accepted, where $m$ is a value between 0 and GL_MAX_COLOR_ATTACHMENTS.</param>
        public void NamedFramebufferDrawBuffer(uint framebuffer, uint mode)
        {
            GetDelegateFor<glNamedFramebufferDrawBuffer>()(framebuffer, mode);
        }
        
        /// <summary>
        /// Specifies a list of color buffers to be drawn into.
        /// </summary>
        /// <param name="framebuffer">Specifies the name of the framebuffer object for glNamedFramebufferDrawBuffers.</param>
        /// <param name="n">Specifies the number of buffers in <paramref name="bufs"/>.</param>
        /// <param name="bufs">Points to an array of symbolic constants specifying the buffers into which fragment colors or data values will be written.</param>
        public void NamedFramebufferDrawBuffers(uint framebuffer, int n, uint[] bufs)
        {
            GetDelegateFor<glNamedFramebufferDrawBuffers>()(framebuffer, n, bufs);
        }
        
        /// <summary>
        /// Select a color buffer source for pixels.
        /// </summary>
        /// <param name="framebuffer">Specifies the name of the framebuffer object for glNamedFramebufferReadBuffer function.</param>
        /// <param name="mode">Specifies a color buffer. Accepted values are GL_FRONT_LEFT, GL_FRONT_RIGHT, GL_BACK_LEFT, GL_BACK_RIGHT, GL_FRONT, GL_BACK, GL_LEFT, GL_RIGHT, and the constants GL_COLOR_ATTACHMENTi.</param>
        public void NamedFramebufferReadBuffer(uint framebuffer, uint mode)
        {
            GetDelegateFor<glNamedFramebufferReadBuffer>()(framebuffer, mode);
        }
        
        /// <summary>
        /// Invalidate the content of some or all of a framebuffer's attachments.
        /// </summary>
        /// <param name="framebuffer">Specifies the name of the framebuffer object for glInvalidateNamedFramebufferData.</param>
        /// <param name="numAttachments">Specifies the number of entries in the attachments array.</param>
        /// <param name="attachments">Specifies a pointer to an array identifying the attachments to be invalidated.</param>
        public void InvalidateNamedFramebufferData(uint framebuffer, int numAttachments, uint[] attachments)
        {
            GetDelegateFor<glInvalidateNamedFramebufferData>()(framebuffer, numAttachments, attachments);
        }
        
        /// <summary>
        /// Invalidate the content of a region of some or all of a framebuffer's attachments.
        /// </summary>
        /// <param name="framebuffer">Specifies the name of the framebuffer object for glInvalidateNamedFramebufferSubData.</param>
        /// <param name="numAttachments">Specifies the number of entries in the attachments array.</param>
        /// <param name="attachments">Specifies a pointer to an array identifying the attachments to be invalidated.</param>
        /// <param name="x">Specifies the X offset of the region to be invalidated.</param>
        /// <param name="y">Specifies the Y offset of the region to be invalidated.</param>
        /// <param name="width">Specifies the width of the region to be invalidated.</param>
        /// <param name="height">Specifies the height of the region to be invalidated.</param>
        public void InvalidateNamedFramebufferSubData(uint framebuffer, int numAttachments, uint[] attachments, int x, int y, int width, int height)
        {
            GetDelegateFor<glInvalidateNamedFramebufferSubData>()(framebuffer, numAttachments, attachments, x, y, width, height);
        }
        
        /// <summary>
        /// Clear individual buffers of a framebuffer.
        /// </summary>
        /// <param name="framebuffer">Specifies the name of the framebuffer object for glClearNamedFramebuffer*.</param>
        /// <param name="buffer">Specify the buffer to clear.</param>
        /// <param name="drawbuffer">Specify a particular draw buffer to clear.</param>
        /// <param name="value">A pointer to the value or values to clear the buffer to.</param>
        public void ClearNamedFramebufferiv(uint framebuffer, uint buffer, int drawbuffer, int[] value)
        {
            GetDelegateFor<glClearNamedFramebufferiv>()(framebuffer, buffer, drawbuffer, value);
        }
        
        /// <summary>
        /// Clear individual buffers of a framebuffer.
        /// </summary>
        /// <param name="framebuffer">Specifies the name of the framebuffer object for glClearNamedFramebuffer*.</param>
        /// <param name="buffer">Specify the buffer to clear.</param>
        /// <param name="drawbuffer">Specify a particular draw buffer to clear.</param>
        /// <param name="value">A pointer to the value or values to clear the buffer to.</param>
        public void ClearNamedFramebufferuiv(uint framebuffer, uint buffer, int drawbuffer, uint[] value)
        {
            GetDelegateFor<glClearNamedFramebufferuiv>()(framebuffer, buffer, drawbuffer, value);
        }
        
        /// <summary>
        /// Clear individual buffers of a framebuffer.
        /// </summary>
        /// <param name="framebuffer">Specifies the name of the framebuffer object for glClearNamedFramebuffer*.</param>
        /// <param name="buffer">Specify the buffer to clear.</param>
        /// <param name="drawbuffer">Specify a particular draw buffer to clear.</param>
        /// <param name="value">A pointer to the value or values to clear the buffer to.</param>
        public void ClearNamedFramebufferfv(uint framebuffer, uint buffer, int drawbuffer, float[] value)
        {
            GetDelegateFor<glClearNamedFramebufferfv>()(framebuffer, buffer, drawbuffer, value);
        }
        
        /// <summary>
        /// Clear individual buffers of a framebuffer.
        /// </summary>
        /// <param name="framebuffer">Specifies the name of the framebuffer object for glClearNamedFramebuffer*.</param>
        /// <param name="buffer">Specify the buffer to clear.</param>
        /// <param name="depth">The value to clear the depth buffer to.</param>
        /// <param name="stencil">The value to clear the stencil buffer to.</param>
        public void ClearNamedFramebufferfi(uint framebuffer, uint buffer, float depth, int stencil)
        {
            GetDelegateFor<glClearNamedFramebufferfi>()(framebuffer, buffer, depth, stencil);
        }

        /// <summary>
        /// Blits the named framebuffer.
        /// </summary>
        /// <param name="readFramebuffer">Specifies the name of the source framebuffer object for glBlitNamedFramebuffer.</param>
        /// <param name="drawFramebuffer">Specifies the name of the destination framebuffer object for glBlitNamedFramebuffer.</param>
        /// <param name="srcX0">Specify the bounds of the source rectangle within the read buffer of the read framebuffer.</param>
        /// <param name="srcY0">Specify the bounds of the source rectangle within the read buffer of the read framebuffer.</param>
        /// <param name="srcX1">Specify the bounds of the source rectangle within the read buffer of the read framebuffer.</param>
        /// <param name="srcY1">Specify the bounds of the source rectangle within the read buffer of the read framebuffer.</param>
        /// <param name="dstX0">Specify the bounds of the destination rectangle within the write buffer of the write framebuffer.</param>
        /// <param name="dstY0">Specify the bounds of the destination rectangle within the write buffer of the write framebuffer.</param>
        /// <param name="dstX1">Specify the bounds of the destination rectangle within the write buffer of the write framebuffer.</param>
        /// <param name="dstY1">Specify the bounds of the destination rectangle within the write buffer of the write framebuffer.</param>
        /// <param name="mask">The bitwise OR of the flags indicating which buffers are to be copied. The allowed flags are GL_COLOR_BUFFER_BIT, GL_DEPTH_BUFFER_BIT and GL_STENCIL_BUFFER_BIT.</param>
        /// <param name="filter">Specifies the interpolation to be applied if the image is stretched. Must be GL_NEAREST or GL_LINEAR.</param>
        public void BlitNamedFramebuffer(
            uint readFramebuffer, uint drawFramebuffer, int srcX0, int srcY0, int srcX1, int srcY1, int dstX0, int dstY0,
            int dstX1, int dstY1, uint mask, uint filter)
        {
            GetDelegateFor<glBlitNamedFramebuffer>()(readFramebuffer, drawFramebuffer, srcX0, srcY0, srcX1, srcY1, dstX0, dstY0,
            dstX1, dstY1, mask, filter);
        }
        
        /// <summary>
        /// Check the completeness status of a framebuffer.
        /// </summary>
        /// <param name="framebuffer">Specifies the name of the framebuffer object for glCheckNamedFramebufferStatus</param>
        /// <param name="target">Specify the target to which the framebuffer is bound for glCheckFramebufferStatus, and the target against which framebuffer completeness of framebuffer is checked for glCheckNamedFramebufferStatus.</param>
        /// <returns>The return value is GL_FRAMEBUFFER_COMPLETE if the specified framebuffer is complete. Otherwise, the return value is determined as follows:
        /// GL_FRAMEBUFFER_UNDEFINED is returned if the specified framebuffer is the default read or draw framebuffer, but the default framebuffer does not exist.
        /// GL_FRAMEBUFFER_INCOMPLETE_ATTACHMENT is returned if any of the framebuffer attachment points are framebuffer incomplete.
        /// GL_FRAMEBUFFER_INCOMPLETE_MISSING_ATTACHMENT is returned if the framebuffer does not have at least one image attached to it.
        /// GL_FRAMEBUFFER_INCOMPLETE_DRAW_BUFFER is returned if the value of GL_FRAMEBUFFER_ATTACHMENT_OBJECT_TYPE is GL_NONE for any color attachment point(s) named by GL_DRAW_BUFFERi.
        /// GL_FRAMEBUFFER_INCOMPLETE_READ_BUFFER is returned if GL_READ_BUFFER is not GL_NONE and the value of GL_FRAMEBUFFER_ATTACHMENT_OBJECT_TYPE is GL_NONE for the color attachment point named by GL_READ_BUFFER.
        /// GL_FRAMEBUFFER_UNSUPPORTED is returned if the combination of internal formats of the attached images violates an implementation-dependent set of restrictions.
        /// GL_FRAMEBUFFER_INCOMPLETE_MULTISAMPLE is returned if the value of GL_RENDERBUFFER_SAMPLES is not the same for all attached renderbuffers; if the value of GL_TEXTURE_SAMPLES is the not same for all attached textures; or, if the attached images are a mix of renderbuffers and textures, the value of GL_RENDERBUFFER_SAMPLES does not match the value of GL_TEXTURE_SAMPLES.
        /// GL_FRAMEBUFFER_INCOMPLETE_MULTISAMPLE is also returned if the value of GL_TEXTURE_FIXED_SAMPLE_LOCATIONS is not the same for all attached textures; or, if the attached images are a mix of renderbuffers and textures, the value of GL_TEXTURE_FIXED_SAMPLE_LOCATIONS is not GL_TRUE for all attached textures.
        /// GL_FRAMEBUFFER_INCOMPLETE_LAYER_TARGETS is returned if any framebuffer attachment is layered, and any populated attachment is not layered, or if all populated color attachments are not from textures of the same target.
        /// Additionally, if an error occurs, zero is returned.</returns>
        public uint CheckNamedFramebufferStatus(uint framebuffer, uint target)
        {
            return GetDelegateFor<glCheckNamedFramebufferStatus>()(framebuffer, target);
        }

        /// <summary>
        /// Query a named parameter of a framebuffer object.
        /// </summary>
        /// <param name="framebuffer">Specifies the name of the framebuffer object for glGetNamedFramebufferParameteriv.</param>
        /// <param name="pname">Specifies the parameter of the framebuffer object to query.</param>
        /// <param name="param">Returns the value of parameter pname for the framebuffer object.</param>
        public void GetNamedFramebufferParameter(uint framebuffer, uint pname, out int param)
        {
            GetDelegateFor<glGetNamedFramebufferParameteriv>()(framebuffer, pname, out param);
        }
        
        /// <summary>
        /// Retrieve information about attachments of a framebuffer object.
        /// </summary>
        /// <param name="framebuffer">Specifies the name of the framebuffer object for glGetNamedFramebufferAttachmentParameteriv.</param>
        /// <param name="attachment">Specifies the attachment of the framebuffer object to query.</param>
        /// <param name="pname">Specifies the parameter of attachment to query.</param>
        /// <param name="params">Returns the value of parameter pname for attachment.</param>
        public void GetNamedFramebufferAttachmentParameter(uint framebuffer, uint attachment, uint pname, out int @params)
        {
            GetDelegateFor<glGetNamedFramebufferAttachmentParameteriv>()(framebuffer, attachment, pname, out @params);
        }
        
        /// <summary>
        /// Create renderbuffer objects.
        /// </summary>
        /// <param name="n">Number of renderbuffer objects to create.</param>
        /// <param name="renderbuffers">Specifies an array in which names of the new renderbuffer objects are stored.</param>
        public void CreateRenderbuffers(int n, uint[] renderbuffers)
        {
            GetDelegateFor<glCreateRenderbuffers>()(n, renderbuffers);
        }
        
        /// <summary>
        /// Establish data storage, format and dimensions of a renderbuffer object's image.
        /// </summary>
        /// <param name="renderbuffer">Specifies the name of the renderbuffer object for glNamedRenderbufferStorage function.</param>
        /// <param name="internalformat">Specifies the internal format to use for the renderbuffer object's image.</param>
        /// <param name="width">Specifies the width of the renderbuffer, in pixels.</param>
        /// <param name="height">Specifies the height of the renderbuffer, in pixels.</param>
        public void NamedRenderbufferStorage(uint renderbuffer, uint internalformat, int width, int height)
        {
            GetDelegateFor<glNamedRenderbufferStorage>()(renderbuffer, internalformat, width, height);
        }

        /// <summary>
        /// Establish data storage, format, dimensions and sample count of a renderbuffer object's image.
        /// </summary>
        /// <param name="renderbuffer">Specifies the name of the renderbuffer object for glNamedRenderbufferStorageMultisample function.</param>
        /// <param name="samples">Specifies the number of samples to be used for the renderbuffer object's storage.</param>
        /// <param name="internalformat">Specifies the internal format to use for the renderbuffer object's image.</param>
        /// <param name="width">Specifies the width of the renderbuffer, in pixels.</param>
        /// <param name="height">Specifies the height of the renderbuffer, in pixels.</param>
        public void NamedRenderbufferStorageMultisample(uint renderbuffer, int samples, uint internalformat, int width, int height)
        {
            GetDelegateFor<glNamedRenderbufferStorageMultisample>()(renderbuffer, samples, internalformat, width, height);
        }
        
        /// <summary>
        /// Query a named parameter of a renderbuffer object.
        /// </summary>
        /// <param name="renderbuffer">Specifies the name of the renderbuffer object for glGetNamedRenderbufferParameteriv.</param>
        /// <param name="pname">Specifies the parameter of the renderbuffer object to query.</param>
        /// <param name="params">Returns the value of parameter pname for the renderbuffer object.</param>
        public void GetNamedRenderbufferParameteriv(uint renderbuffer, uint pname, out int @params)
        {
            GetDelegateFor<glGetNamedRenderbufferParameteriv>()(renderbuffer, pname, out @params);
        }
        
        /// <summary>
        /// Create texture objects.
        /// </summary>
        /// <param name="target">Specifies the effective texture target of each created texture.</param>
        /// <param name="n">Number of texture objects to create.</param>
        /// <param name="textures">Specifies an array in which names of the new texture objects are stored.</param>
        public void CreateTextures(uint target, int n, uint[] textures)
        {
            GetDelegateFor<glCreateTextures>()(target, n, textures);
        }
        
        /// <summary>
        /// Attach a buffer object's data store to a buffer texture object.
        /// </summary>
        /// <param name="texture">Specifies the texture object name for glTextureBuffer.</param>
        /// <param name="internalformat">Specifies the internal format of the data in the store belonging to buffer.</param>
        /// <param name="buffer">Specifies the name of the buffer object whose storage to attach to the active buffer texture.</param>
        public void TextureBuffer(uint texture, uint internalformat, uint buffer)
        {
            GetDelegateFor<glTextureBuffer>()(texture, internalformat, buffer);
        }

        /// <summary>
        /// Attach a range of a buffer object's data store to a buffer texture object.
        /// </summary>
        /// <param name="texture">Specifies the texture object name for glTextureBufferRange.</param>
        /// <param name="internalformat">Specifies the internal format of the data in the store belonging to buffer.</param>
        /// <param name="buffer">Specifies the name of the buffer object whose storage to attach to the active buffer texture.</param>
        /// <param name="offset">Specifies the offset of the start of the range of the buffer's data store to attach.</param>
        /// <param name="size">Specifies the size of the range of the buffer's data store to attach.</param>
        public void TextureBufferRange(uint texture, uint internalformat, uint buffer, int offset, int size)
        {
            GetDelegateFor<glTextureBufferRange>()(texture, internalformat, buffer, offset, size);
        }
        
        /// <summary>
        /// Simultaneously specify storage for all levels of a one-dimensional texture.
        /// </summary>
        /// <param name="texture">Specifies the texture object name for glTextureStorage1D. The effective target of texture must be one of the valid non-proxy target values above.</param>
        /// <param name="levels">Specify the number of texture levels.</param>
        /// <param name="internalformat">Specifies the sized internal format to be used to store texture image data.</param>
        /// <param name="width">Specifies the width of the texture, in texels.</param>
        public void TextureStorage1D(uint texture, int levels, uint internalformat, int width)
        {
            GetDelegateFor<glTextureStorage1D>()(texture, levels, internalformat, width);
        }
        
        /// <summary>
        /// Simultaneously specify storage for all levels of a two-dimensional or one-dimensional array texture.
        /// </summary>
        /// <param name="texture">Specifies the texture object name for glTextureStorage2D. The effective target of texture must be one of the valid non-proxy target values above.</param>
        /// <param name="levels">Specify the number of texture levels.</param>
        /// <param name="internalformat">Specifies the sized internal format to be used to store texture image data.</param>
        /// <param name="width">Specifies the width of the texture, in texels.</param>
        /// <param name="height">Specifies the height of the texture, in texels.</param>
        public void TextureStorage2D(uint texture, int levels, uint internalformat, int width, int height)
        {
            GetDelegateFor<glTextureStorage2D>()(texture, levels, internalformat, width, height);
        }
        
        /// <summary>
        /// Simultaneously specify storage for all levels of a three-dimensional, two-dimensional array or cube-map array texture.
        /// </summary>
        /// <param name="texture">Specifies the texture object name for glTextureStorage3D. The effective target of texture must be one of the valid non-proxy target values above.</param>
        /// <param name="levels">Specify the number of texture levels.</param>
        /// <param name="internalformat">Specifies the sized internal format to be used to store texture image data.</param>
        /// <param name="width">Specifies the width of the texture, in texels.</param>
        /// <param name="height">Specifies the height of the texture, in texels.</param>
        /// <param name="depth">Specifies the depth of the texture, in texels.</param>
        public void TextureStorage3D(uint texture, int levels, uint internalformat, int width, int height, int depth)
        {
            GetDelegateFor<glTextureStorage3D>()(texture, levels, internalformat, width, height, depth);
        }

        /// <summary>
        /// Specify storage for a two-dimensional multisample texture.
        /// </summary>
        /// <param name="texture">Specifies the texture object name for glTextureStorage2DMultisample. The effective target of texture must be one of the valid non-proxy target values above.</param>
        /// <param name="samples">Specify the number of samples in the texture.</param>
        /// <param name="internalformat">Specifies the sized internal format to be used to store texture image data.</param>
        /// <param name="width">Specifies the width of the texture, in texels.</param>
        /// <param name="height">Specifies the height of the texture, in texels.</param>
        /// <param name="fixedsamplelocations">Specifies whether the image will use identical sample locations and the same number of samples for all texels in the image, and the sample locations will not depend on the internal format or size of the image.</param>
        public void TextureStorage2DMultisample(uint texture, int samples, uint internalformat, int width, int height,
                                                  bool fixedsamplelocations)
        {
            GetDelegateFor<glTextureStorage2DMultisample>()(texture, samples, internalformat, width, height, fixedsamplelocations);
        }

        /// <summary>
        /// Specify storage for a two-dimensional multisample array texture.
        /// </summary>
        /// <param name="texture">Specifies the texture object name for glTextureStorage3DMultisample. The effective target of texture must be one of the valid non-proxy target values above.</param>
        /// <param name="samples">Specify the number of samples in the texture.</param>
        /// <param name="internalformat">Specifies the sized internal format to be used to store texture image data.</param>
        /// <param name="width">Specifies the width of the texture, in texels.</param>
        /// <param name="height">Specifies the height of the texture, in texels.</param>
        /// <param name="depth">Specifies the depth of the texture, in layers.</param>
        /// <param name="fixedsamplelocations">Specifies whether the image will use identical sample locations and the same number of samples for all texels in the image, and the sample locations will not depend on the internal format or size of the image.</param>
        public void TextureStorage3DMultisample(uint texture, int samples, uint internalformat, int width, int height,
                                                  int depth, bool fixedsamplelocations)
        {
            GetDelegateFor<glTextureStorage3DMultisample>()(texture, samples, internalformat, width, height,
                                                  depth, fixedsamplelocations);
        }

        /// <summary>
        /// Specify a one-dimensional texture subimage.
        /// </summary>
        /// <param name="texture">Specifies the texture object name for glTextureSubImage1D. The effective target of texture must be one of the valid target values above.</param>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xoffset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="width">Specifies the width of the texture subimage.</param>
        /// <param name="format">Specifies the format of the pixel data. The following symbolic values are accepted: GL_RED, GL_RG, GL_RGB, GL_BGR, GL_RGBA, GL_DEPTH_COMPONENT, and GL_STENCIL_INDEX.</param>
        /// <param name="type">Specifies the data type of the pixel data. The following symbolic values are accepted: GL_UNSIGNED_BYTE, GL_BYTE, GL_UNSIGNED_SHORT, GL_SHORT, GL_UNSIGNED_INT, GL_INT, GL_FLOAT, GL_UNSIGNED_BYTE_3_3_2, GL_UNSIGNED_BYTE_2_3_3_REV, GL_UNSIGNED_SHORT_5_6_5, GL_UNSIGNED_SHORT_5_6_5_REV, GL_UNSIGNED_SHORT_4_4_4_4, GL_UNSIGNED_SHORT_4_4_4_4_REV, GL_UNSIGNED_SHORT_5_5_5_1, GL_UNSIGNED_SHORT_1_5_5_5_REV, GL_UNSIGNED_INT_8_8_8_8, GL_UNSIGNED_INT_8_8_8_8_REV, GL_UNSIGNED_INT_10_10_10_2, and GL_UNSIGNED_INT_2_10_10_10_REV.</param>
        /// <param name="pixels">Specifies a pointer to the image data in memory.</param>
        public void TextureSubImage1D(uint texture, int level, int xoffset, int width, uint format, uint type,
                                      IntPtr pixels)
        {
            GetDelegateFor<glTextureSubImage1D>()(texture, level, xoffset, width, format, type, pixels);
        }

        /// <summary>
        /// Specify a two-dimensional texture subimage.
        /// </summary>
        /// <param name="texture">Specifies the texture object name for glTextureSubImage2D. The effective target of texture must be one of the valid target values above.</param>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xoffset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="yoffset">Specifies a texel offset in the y direction within the texture array.</param>
        /// <param name="width">Specifies the width of the texture subimage.</param>
        /// <param name="height">Specifies the height of the texture subimage.</param>
        /// <param name="format">Specifies the format of the pixel data. The following symbolic values are accepted: GL_RED, GL_RG, GL_RGB, GL_BGR, GL_RGBA, GL_DEPTH_COMPONENT, and GL_STENCIL_INDEX.</param>
        /// <param name="type">Specifies the data type of the pixel data. The following symbolic values are accepted: GL_UNSIGNED_BYTE, GL_BYTE, GL_UNSIGNED_SHORT, GL_SHORT, GL_UNSIGNED_INT, GL_INT, GL_FLOAT, GL_UNSIGNED_BYTE_3_3_2, GL_UNSIGNED_BYTE_2_3_3_REV, GL_UNSIGNED_SHORT_5_6_5, GL_UNSIGNED_SHORT_5_6_5_REV, GL_UNSIGNED_SHORT_4_4_4_4, GL_UNSIGNED_SHORT_4_4_4_4_REV, GL_UNSIGNED_SHORT_5_5_5_1, GL_UNSIGNED_SHORT_1_5_5_5_REV, GL_UNSIGNED_INT_8_8_8_8, GL_UNSIGNED_INT_8_8_8_8_REV, GL_UNSIGNED_INT_10_10_10_2, and GL_UNSIGNED_INT_2_10_10_10_REV.</param>
        /// <param name="pixels">Specifies a pointer to the image data in memory.</param>
        public void TextureSubImage2D(uint texture, int level, int xoffset, int yoffset, int width, int height,
                                      uint format, uint type, IntPtr pixels)
        {
            GetDelegateFor<glTextureSubImage2D>()(texture, level, xoffset, yoffset, width, height, format, type, pixels);
        }

        /// <summary>
        /// Specify a three-dimensional texture subimage.
        /// </summary>
        /// <param name="texture">Specifies the texture object name for glTextureSubImage3D. The effective target of texture must be one of the valid target values above.</param>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xoffset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="yoffset">Specifies a texel offset in the y direction within the texture array.</param>
        /// <param name="zoffset">Specifies a texel offset in the z direction within the texture array.</param>
        /// <param name="width">Specifies the width of the texture subimage.</param>
        /// <param name="height">Specifies the height of the texture subimage.</param>
        /// <param name="depth">Specifies the depth of the texture subimage.</param>
        /// <param name="format">Specifies the format of the pixel data. The following symbolic values are accepted: GL_RED, GL_RG, GL_RGB, GL_BGR, GL_RGBA, GL_DEPTH_COMPONENT, and GL_STENCIL_INDEX.</param>
        /// <param name="type">Specifies the data type of the pixel data. The following symbolic values are accepted: GL_UNSIGNED_BYTE, GL_BYTE, GL_UNSIGNED_SHORT, GL_SHORT, GL_UNSIGNED_INT, GL_INT, GL_FLOAT, GL_UNSIGNED_BYTE_3_3_2, GL_UNSIGNED_BYTE_2_3_3_REV, GL_UNSIGNED_SHORT_5_6_5, GL_UNSIGNED_SHORT_5_6_5_REV, GL_UNSIGNED_SHORT_4_4_4_4, GL_UNSIGNED_SHORT_4_4_4_4_REV, GL_UNSIGNED_SHORT_5_5_5_1, GL_UNSIGNED_SHORT_1_5_5_5_REV, GL_UNSIGNED_INT_8_8_8_8, GL_UNSIGNED_INT_8_8_8_8_REV, GL_UNSIGNED_INT_10_10_10_2, and GL_UNSIGNED_INT_2_10_10_10_REV.</param>
        /// <param name="pixels">Specifies a pointer to the image data in memory.</param>
        public void TextureSubImage3D(uint texture, int level, int xoffset, int yoffset, int zoffset, int width,
                                      int height, int depth, uint format, uint type, IntPtr pixels)
        {
            GetDelegateFor<glTextureSubImage3D>()(texture, level, xoffset, yoffset, zoffset, width, height, depth,
                                                  format, type, pixels);
        }

        /// <summary>
        /// Specify a one-dimensional texture subimage in a compressed format.
        /// </summary>
        /// <param name="texture">Specifies the texture object name for glCompressedTextureSubImage2D function.</param>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xoffset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="width">Specifies the width of the texture subimage.</param>
        /// <param name="format">Specifies the format of the compressed image data stored at address data.</param>
        /// <param name="imageSize">Specifies the number of unsigned bytes of image data starting at the address specified by data.</param>
        /// <param name="data">Specifies a pointer to the compressed image data in memory.</param>
        public void CompressedTextureSubImage1D(uint texture, int level, int xoffset, int width, uint format, int imageSize, IntPtr data)
        {
            GetDelegateFor<glCompressedTextureSubImage1D>()(texture, level, xoffset, width, format, imageSize, data);
        }

        /// <summary>
        /// Specify a two-dimensional texture subimage in a compressed format.
        /// </summary>
        /// <param name="texture">Specifies the texture object name for glCompressedTextureSubImage2D function.</param>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xoffset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="yoffset">Specifies a texel offset in the y direction within the texture array.</param>
        /// <param name="width">Specifies the width of the texture subimage.</param>
        /// <param name="height">Specifies the height of the texture subimage.</param>
        /// <param name="format">Specifies the format of the compressed image data stored at address data.</param>
        /// <param name="imageSize">Specifies the number of unsigned bytes of image data starting at the address specified by data.</param>
        /// <param name="data">Specifies a pointer to the compressed image data in memory.</param>
        public void CompressedTextureSubImage2D(uint texture, int level, int xoffset, int yoffset,
                                                  int width, int height, uint format, int imageSize,
                                                  IntPtr data)
        {
            GetDelegateFor<glCompressedTextureSubImage2D>()(texture, level, xoffset, yoffset, width, height, format, imageSize, data);
        }

        /// <summary>
        /// Specify a three-dimensional texture subimage in a compressed format.
        /// </summary>
        /// <param name="texture">Specifies the texture object name for glCompressedTextureSubImage3D function.</param>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xoffset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="yoffset">Specifies a texel offset in the y direction within the texture array.</param>
        /// <param name="zoffset">Specifies a texel offset in the z direction within the texture array.</param>
        /// <param name="width">Specifies the width of the texture subimage.</param>
        /// <param name="height">Specifies the height of the texture subimage.</param>
        /// <param name="depth">Specifies the depth of the texture subimage.</param>
        /// <param name="format">Specifies the format of the compressed image data stored at address data.</param>
        /// <param name="imageSize">Specifies the number of unsigned bytes of image data starting at the address specified by data.</param>
        /// <param name="data">Specifies a pointer to the compressed image data in memory.</param>
        public void CompressedTextureSubImage3D(uint texture, int level, int xoffset, int yoffset, int zoffset,
                                                  int width, int height, int depth, uint format, int imageSize,
                                                  IntPtr data)
        {
            GetDelegateFor<glCompressedTextureSubImage3D>()(texture, level, xoffset, yoffset, zoffset, width, height, depth,
                                                  format, imageSize, data);
        }
        
        /// <summary>
        /// Copy a one-dimensional texture subimage.
        /// </summary>
        /// <param name="texture">Specifies the texture object name for glCopyTextureSubImage1D function.</param>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xoffset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="x">Specify the window coordinates of the lower left corner of the rectangular region of pixels to be copied.</param>
        /// <param name="y">Specify the window coordinates of the lower left corner of the rectangular region of pixels to be copied.</param>
        /// <param name="width">Specifies the width of the texture subimage.</param>
        public void CopyTextureSubImage1D(uint texture, int level, int xoffset, int x, int y, int width)
        {
            GetDelegateFor<glCopyTextureSubImage1D>()(texture, level, xoffset, x, y, width);
        }
        
        /// <summary>
        /// Copy a two-dimensional texture subimage.
        /// </summary>
        /// <param name="texture">Specifies the texture object name for glCopyTextureSubImage2D function.</param>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xoffset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="yoffset">Specifies a texel offset in the y direction within the texture array.</param>
        /// <param name="x">Specify the window coordinates of the lower left corner of the rectangular region of pixels to be copied.</param>
        /// <param name="y">Specify the window coordinates of the lower left corner of the rectangular region of pixels to be copied.</param>
        /// <param name="width">Specifies the width of the texture subimage.</param>
        /// <param name="height">Specifies the width of the texture subimage.</param>
        public void CopyTextureSubImage2D(uint texture, int level, int xoffset, int yoffset, int x, int y,
                                            int width, int height)
        {
            GetDelegateFor<glCopyTextureSubImage2D>()(texture, level, xoffset, yoffset, x, y, width, height);
        }

        /// <summary>
        /// Copy a three-dimensional texture subimage.
        /// </summary>
        /// <param name="texture">Specifies the texture object name for glCopyTextureSubImage3D function.</param>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xoffset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="yoffset">Specifies a texel offset in the y direction within the texture array.</param>
        /// <param name="zoffset">Specifies a texel offset in the z direction within the texture array.</param>
        /// <param name="x">Specify the window coordinates of the lower left corner of the rectangular region of pixels to be copied.</param>
        /// <param name="y">Specify the window coordinates of the lower left corner of the rectangular region of pixels to be copied.</param>
        /// <param name="width">Specifies the width of the texture subimage.</param>
        /// <param name="height">Specifies the width of the texture subimage.</param>
        public void CopyTextureSubImage3D(uint texture, int level, int xoffset, int yoffset, int zoffset, int x, int y,
                                            int width, int height)
        {
            GetDelegateFor<glCopyTextureSubImage3D>()(texture, level, xoffset, yoffset, zoffset, x, y, width, height);
        }

        /// <summary>
        /// Set texture parameters.
        /// </summary>
        /// <param name="texture">Specifies the texture object name for glTextureParameter functions.</param>
        /// <param name="pname">Specifies the symbolic name of a single-valued texture parameter. pname can be one of the following: GL_DEPTH_STENCIL_TEXTURE_MODE, GL_TEXTURE_BASE_LEVEL, GL_TEXTURE_COMPARE_FUNC, GL_TEXTURE_COMPARE_MODE, GL_TEXTURE_LOD_BIAS, GL_TEXTURE_MIN_FILTER, GL_TEXTURE_MAG_FILTER, GL_TEXTURE_MIN_LOD, GL_TEXTURE_MAX_LOD, GL_TEXTURE_MAX_LEVEL, GL_TEXTURE_SWIZZLE_R, GL_TEXTURE_SWIZZLE_G, GL_TEXTURE_SWIZZLE_B, GL_TEXTURE_SWIZZLE_A, GL_TEXTURE_WRAP_S, GL_TEXTURE_WRAP_T, or GL_TEXTURE_WRAP_R. For the vector commands (glTexParameter*v), pname can also be one of GL_TEXTURE_BORDER_COLOR or GL_TEXTURE_SWIZZLE_RGBA.</param>
        /// <param name="param">The parameter value.</param>
        public void TextureParameter(uint texture, uint pname, float param)
        {
            GetDelegateFor<glTextureParameterf>()(texture, pname, param);
        }
        
        /// <summary>
        /// Set texture parameters.
        /// </summary>
        /// <param name="texture">Specifies the texture object name for glTextureParameter functions.</param>
        /// <param name="pname">Specifies the symbolic name of a single-valued texture parameter. pname can be one of the following: GL_DEPTH_STENCIL_TEXTURE_MODE, GL_TEXTURE_BASE_LEVEL, GL_TEXTURE_COMPARE_FUNC, GL_TEXTURE_COMPARE_MODE, GL_TEXTURE_LOD_BIAS, GL_TEXTURE_MIN_FILTER, GL_TEXTURE_MAG_FILTER, GL_TEXTURE_MIN_LOD, GL_TEXTURE_MAX_LOD, GL_TEXTURE_MAX_LEVEL, GL_TEXTURE_SWIZZLE_R, GL_TEXTURE_SWIZZLE_G, GL_TEXTURE_SWIZZLE_B, GL_TEXTURE_SWIZZLE_A, GL_TEXTURE_WRAP_S, GL_TEXTURE_WRAP_T, or GL_TEXTURE_WRAP_R. For the vector commands (glTexParameter*v), pname can also be one of GL_TEXTURE_BORDER_COLOR or GL_TEXTURE_SWIZZLE_RGBA.</param>
        /// <param name="param">The parameter value.</param>
        public void TextureParameter(uint texture, uint pname, float[] param)
        {
            GetDelegateFor<glTextureParameterfv>()(texture, pname, param);
        }
        
        /// <summary>
        /// Set texture parameters.
        /// </summary>
        /// <param name="texture">Specifies the texture object name for glTextureParameter functions.</param>
        /// <param name="pname">Specifies the symbolic name of a single-valued texture parameter. pname can be one of the following: GL_DEPTH_STENCIL_TEXTURE_MODE, GL_TEXTURE_BASE_LEVEL, GL_TEXTURE_COMPARE_FUNC, GL_TEXTURE_COMPARE_MODE, GL_TEXTURE_LOD_BIAS, GL_TEXTURE_MIN_FILTER, GL_TEXTURE_MAG_FILTER, GL_TEXTURE_MIN_LOD, GL_TEXTURE_MAX_LOD, GL_TEXTURE_MAX_LEVEL, GL_TEXTURE_SWIZZLE_R, GL_TEXTURE_SWIZZLE_G, GL_TEXTURE_SWIZZLE_B, GL_TEXTURE_SWIZZLE_A, GL_TEXTURE_WRAP_S, GL_TEXTURE_WRAP_T, or GL_TEXTURE_WRAP_R. For the vector commands (glTexParameter*v), pname can also be one of GL_TEXTURE_BORDER_COLOR or GL_TEXTURE_SWIZZLE_RGBA.</param>
        /// <param name="param">The parameter value.</param>
        public void TextureParameter(uint texture, uint pname, int param)
        {
            GetDelegateFor<glTextureParameteri>()(texture, pname, param);
        }
        
        /// <summary>
        /// Set texture parameters.
        /// </summary>
        /// <param name="texture">Specifies the texture object name for glTextureParameter functions.</param>
        /// <param name="pname">Specifies the symbolic name of a single-valued texture parameter. pname can be one of the following: GL_DEPTH_STENCIL_TEXTURE_MODE, GL_TEXTURE_BASE_LEVEL, GL_TEXTURE_COMPARE_FUNC, GL_TEXTURE_COMPARE_MODE, GL_TEXTURE_LOD_BIAS, GL_TEXTURE_MIN_FILTER, GL_TEXTURE_MAG_FILTER, GL_TEXTURE_MIN_LOD, GL_TEXTURE_MAX_LOD, GL_TEXTURE_MAX_LEVEL, GL_TEXTURE_SWIZZLE_R, GL_TEXTURE_SWIZZLE_G, GL_TEXTURE_SWIZZLE_B, GL_TEXTURE_SWIZZLE_A, GL_TEXTURE_WRAP_S, GL_TEXTURE_WRAP_T, or GL_TEXTURE_WRAP_R. For the vector commands (glTexParameter*v), pname can also be one of GL_TEXTURE_BORDER_COLOR or GL_TEXTURE_SWIZZLE_RGBA.</param>
        /// <param name="params">The parameter value.</param>
        public void TextureParameterI(uint texture, uint pname, int[] @params)
        {
            GetDelegateFor<glTextureParameterIiv>()(texture, pname, @params);
        }
        
        /// <summary>
        /// Set texture parameters.
        /// </summary>
        /// <param name="texture">Specifies the texture object name for glTextureParameter functions.</param>
        /// <param name="pname">Specifies the symbolic name of a single-valued texture parameter. pname can be one of the following: GL_DEPTH_STENCIL_TEXTURE_MODE, GL_TEXTURE_BASE_LEVEL, GL_TEXTURE_COMPARE_FUNC, GL_TEXTURE_COMPARE_MODE, GL_TEXTURE_LOD_BIAS, GL_TEXTURE_MIN_FILTER, GL_TEXTURE_MAG_FILTER, GL_TEXTURE_MIN_LOD, GL_TEXTURE_MAX_LOD, GL_TEXTURE_MAX_LEVEL, GL_TEXTURE_SWIZZLE_R, GL_TEXTURE_SWIZZLE_G, GL_TEXTURE_SWIZZLE_B, GL_TEXTURE_SWIZZLE_A, GL_TEXTURE_WRAP_S, GL_TEXTURE_WRAP_T, or GL_TEXTURE_WRAP_R. For the vector commands (glTexParameter*v), pname can also be one of GL_TEXTURE_BORDER_COLOR or GL_TEXTURE_SWIZZLE_RGBA.</param>
        /// <param name="params">The parameter value.</param>
        public void TextureParameterI(uint texture, uint pname, uint[] @params)
        {
            GetDelegateFor<glTextureParameterIuiv>()(texture, pname, @params);
        }
        
        /// <summary>
        /// Set texture parameters.
        /// </summary>
        /// <param name="texture">Specifies the texture object name for glTextureParameter functions.</param>
        /// <param name="pname">Specifies the symbolic name of a single-valued texture parameter. pname can be one of the following: GL_DEPTH_STENCIL_TEXTURE_MODE, GL_TEXTURE_BASE_LEVEL, GL_TEXTURE_COMPARE_FUNC, GL_TEXTURE_COMPARE_MODE, GL_TEXTURE_LOD_BIAS, GL_TEXTURE_MIN_FILTER, GL_TEXTURE_MAG_FILTER, GL_TEXTURE_MIN_LOD, GL_TEXTURE_MAX_LOD, GL_TEXTURE_MAX_LEVEL, GL_TEXTURE_SWIZZLE_R, GL_TEXTURE_SWIZZLE_G, GL_TEXTURE_SWIZZLE_B, GL_TEXTURE_SWIZZLE_A, GL_TEXTURE_WRAP_S, GL_TEXTURE_WRAP_T, or GL_TEXTURE_WRAP_R. For the vector commands (glTexParameter*v), pname can also be one of GL_TEXTURE_BORDER_COLOR or GL_TEXTURE_SWIZZLE_RGBA.</param>
        /// <param name="param">The parameter value.</param>
        public void TextureParameter(uint texture, uint pname, int[] param)
        {
            GetDelegateFor<glTextureParameteriv>()(texture, pname, param);
        }

        /// <summary>
        /// Generate mipmaps for a specified texture object.
        /// </summary>
        /// <param name="texture">Specifies the texture object name for glGenerateTextureMipmap.</param>
        public void GenerateTextureMipmap(uint texture)
        {
            GetDelegateFor<glGenerateTextureMipmap>()(texture);
        }
        
        /// <summary>
        /// Bind an existing texture object to the specified texture unit.
        /// </summary>
        /// <param name="unit">Specifies the texture unit, to which the texture object should be bound to.</param>
        /// <param name="texture">Specifies the name of a texture.</param>
        public void BindTextureUnit(uint unit, uint texture)
        {
            GetDelegateFor<glBindTextureUnit>()(unit, texture);
        }
        
        /// <summary>
        /// Return a texture image.
        /// </summary>
        /// <param name="texture">Specifies the texture object name.</param>
        /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="format">Specifies a pixel format for the returned data. The supported formats are GL_STENCIL_INDEX, GL_DEPTH_COMPONENT, GL_DEPTH_STENCIL, GL_RED, GL_GREEN, GL_BLUE, GL_RG, GL_RGB, GL_RGBA, GL_BGR, GL_BGRA, GL_RED_INTEGER, GL_GREEN_INTEGER, GL_BLUE_INTEGER, GL_RG_INTEGER, GL_RGB_INTEGER, GL_RGBA_INTEGER, GL_BGR_INTEGER, GL_BGRA_INTEGER.</param>
        /// <param name="type">Specifies a pixel type for the returned data. The supported types are GL_UNSIGNED_BYTE, GL_BYTE, GL_UNSIGNED_SHORT, GL_SHORT, GL_UNSIGNED_INT, GL_INT, GL_HALF_FLOAT, GL_FLOAT, GL_UNSIGNED_BYTE_3_3_2, GL_UNSIGNED_BYTE_2_3_3_REV, GL_UNSIGNED_SHORT_5_6_5, GL_UNSIGNED_SHORT_5_6_5_REV, GL_UNSIGNED_SHORT_4_4_4_4, GL_UNSIGNED_SHORT_4_4_4_4_REV, GL_UNSIGNED_SHORT_5_5_5_1, GL_UNSIGNED_SHORT_1_5_5_5_REV, GL_UNSIGNED_INT_8_8_8_8, GL_UNSIGNED_INT_8_8_8_8_REV, GL_UNSIGNED_INT_10_10_10_2, GL_UNSIGNED_INT_2_10_10_10_REV, GL_UNSIGNED_INT_24_8, GL_UNSIGNED_INT_10F_11F_11F_REV, GL_UNSIGNED_INT_5_9_9_9_REV, and GL_FLOAT_32_UNSIGNED_INT_24_8_REV.</param>
        /// <param name="bufSize">Specifies the size of the buffer pixels for glGetnTexImage and glGetTextureImage functions.</param>
        /// <param name="pixels">Returns the texture image. Should be a pointer to an array of the type specified by type.</param>
        public void GetTextureImage(uint texture, int level, uint format, uint type, int bufSize, IntPtr pixels)
        {
            GetDelegateFor<glGetTextureImage>()(texture, level, format, type, bufSize, pixels);
        }

        /// <summary>
        /// Return a compressed texture image.
        /// </summary>
        /// <param name="texture">Specifies the texture object name for glGetCompressedTextureImage function.</param>
        /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the n-th mipmap reduction image.</param>
        /// <param name="bufSize">Specifies the size of the buffer pixels for glGetCompressedTextureImage and glGetnCompressedTexImage functions.</param>
        /// <param name="pixels">Returns the compressed texture image.</param>
        public void GetCompressedTextureImage(uint texture, int level, int bufSize, IntPtr pixels)
        {
            GetDelegateFor<glGetCompressedTextureImage>()(texture, level, bufSize, pixels);
        }
        
        /// <summary>
        /// Return texture parameter values for a specific level of detail.
        /// </summary>
        /// <param name="texture">Specifies the texture object name for glGetTextureLevelParameterfv and glGetTextureLevelParameteriv functions.</param>
        /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="pname">Specifies the symbolic name of a texture parameter. GL_TEXTURE_WIDTH, GL_TEXTURE_HEIGHT, GL_TEXTURE_DEPTH, GL_TEXTURE_INTERNAL_FORMAT, GL_TEXTURE_RED_SIZE, GL_TEXTURE_GREEN_SIZE, GL_TEXTURE_BLUE_SIZE, GL_TEXTURE_ALPHA_SIZE, GL_TEXTURE_DEPTH_SIZE, GL_TEXTURE_COMPRESSED, GL_TEXTURE_COMPRESSED_IMAGE_SIZE, and GL_TEXTURE_BUFFER_OFFSET are accepted.</param>
        /// <param name="params">Returns the requested data.</param>
        public void GetTextureLevelParameter(uint texture, int level, uint pname, float[] @params)
        {
            GetDelegateFor<glGetTextureLevelParameterfv>()(texture, level, pname, @params);
        }
        
        /// <summary>
        /// Return texture parameter values for a specific level of detail.
        /// </summary>
        /// <param name="texture">Specifies the texture object name for glGetTextureLevelParameterfv and glGetTextureLevelParameteriv functions.</param>
        /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="pname">Specifies the symbolic name of a texture parameter. GL_TEXTURE_WIDTH, GL_TEXTURE_HEIGHT, GL_TEXTURE_DEPTH, GL_TEXTURE_INTERNAL_FORMAT, GL_TEXTURE_RED_SIZE, GL_TEXTURE_GREEN_SIZE, GL_TEXTURE_BLUE_SIZE, GL_TEXTURE_ALPHA_SIZE, GL_TEXTURE_DEPTH_SIZE, GL_TEXTURE_COMPRESSED, GL_TEXTURE_COMPRESSED_IMAGE_SIZE, and GL_TEXTURE_BUFFER_OFFSET are accepted.</param>
        /// <param name="params">Returns the requested data.</param>
        public void GetTextureLevelParameter(uint texture, int level, uint pname, int[] @params)
        {
            GetDelegateFor<glGetTextureLevelParameteriv>()(texture, level, pname, @params);
        }
        
        /// <summary>
        /// Return texture parameter values.
        /// </summary>
        /// <param name="texture">Specifies the texture object name for glGetTextureParameterfv, glGetTextureParameteriv, glGetTextureParameterIiv, and glGetTextureParameterIuiv functions.</param>
        /// <param name="pname">Specifies the symbolic name of a texture parameter. GL_DEPTH_STENCIL_TEXTURE_MODE, GL_IMAGE_FORMAT_COMPATIBILITY_TYPE, GL_TEXTURE_BASE_LEVEL, GL_TEXTURE_BORDER_COLOR, GL_TEXTURE_COMPARE_MODE, GL_TEXTURE_COMPARE_FUNC, GL_TEXTURE_IMMUTABLE_FORMAT, GL_TEXTURE_IMMUTABLE_LEVELS, GL_TEXTURE_LOD_BIAS, GL_TEXTURE_MAG_FILTER, GL_TEXTURE_MAX_LEVEL, GL_TEXTURE_MAX_LOD, GL_TEXTURE_MIN_FILTER, GL_TEXTURE_MIN_LOD, GL_TEXTURE_SWIZZLE_R, GL_TEXTURE_SWIZZLE_G, GL_TEXTURE_SWIZZLE_B, GL_TEXTURE_SWIZZLE_A, GL_TEXTURE_SWIZZLE_RGBA, GL_TEXTURE_TARGET, GL_TEXTURE_VIEW_MIN_LAYER, GL_TEXTURE_VIEW_MIN_LEVEL, GL_TEXTURE_VIEW_NUM_LAYERS, GL_TEXTURE_VIEW_NUM_LEVELS, GL_TEXTURE_WRAP_S, GL_TEXTURE_WRAP_T, and GL_TEXTURE_WRAP_R are accepted.</param>
        /// <param name="params">Returns the texture parameters.</param>
        public void glGetTextureParameter(uint texture, uint pname, float[] @params)
        {
            GetDelegateFor<glGetTextureParameterfv>()(texture, pname, @params);
        }

        /// <summary>
        /// Return texture parameter values.
        /// </summary>
        /// <param name="texture">Specifies the texture object name for glGetTextureParameterfv, glGetTextureParameteriv, glGetTextureParameterIiv, and glGetTextureParameterIuiv functions.</param>
        /// <param name="pname">Specifies the symbolic name of a texture parameter. GL_DEPTH_STENCIL_TEXTURE_MODE, GL_IMAGE_FORMAT_COMPATIBILITY_TYPE, GL_TEXTURE_BASE_LEVEL, GL_TEXTURE_BORDER_COLOR, GL_TEXTURE_COMPARE_MODE, GL_TEXTURE_COMPARE_FUNC, GL_TEXTURE_IMMUTABLE_FORMAT, GL_TEXTURE_IMMUTABLE_LEVELS, GL_TEXTURE_LOD_BIAS, GL_TEXTURE_MAG_FILTER, GL_TEXTURE_MAX_LEVEL, GL_TEXTURE_MAX_LOD, GL_TEXTURE_MIN_FILTER, GL_TEXTURE_MIN_LOD, GL_TEXTURE_SWIZZLE_R, GL_TEXTURE_SWIZZLE_G, GL_TEXTURE_SWIZZLE_B, GL_TEXTURE_SWIZZLE_A, GL_TEXTURE_SWIZZLE_RGBA, GL_TEXTURE_TARGET, GL_TEXTURE_VIEW_MIN_LAYER, GL_TEXTURE_VIEW_MIN_LEVEL, GL_TEXTURE_VIEW_NUM_LAYERS, GL_TEXTURE_VIEW_NUM_LEVELS, GL_TEXTURE_WRAP_S, GL_TEXTURE_WRAP_T, and GL_TEXTURE_WRAP_R are accepted.</param>
        /// <param name="params">Returns the texture parameters.</param>
        public void glGetTextureParameterI(uint texture, uint pname, int[] @params)
        {
            GetDelegateFor<glGetTextureParameterIiv>()(texture, pname, @params);
        }

        /// <summary>
        /// Return texture parameter values.
        /// </summary>
        /// <param name="texture">Specifies the texture object name for glGetTextureParameterfv, glGetTextureParameteriv, glGetTextureParameterIiv, and glGetTextureParameterIuiv functions.</param>
        /// <param name="pname">Specifies the symbolic name of a texture parameter. GL_DEPTH_STENCIL_TEXTURE_MODE, GL_IMAGE_FORMAT_COMPATIBILITY_TYPE, GL_TEXTURE_BASE_LEVEL, GL_TEXTURE_BORDER_COLOR, GL_TEXTURE_COMPARE_MODE, GL_TEXTURE_COMPARE_FUNC, GL_TEXTURE_IMMUTABLE_FORMAT, GL_TEXTURE_IMMUTABLE_LEVELS, GL_TEXTURE_LOD_BIAS, GL_TEXTURE_MAG_FILTER, GL_TEXTURE_MAX_LEVEL, GL_TEXTURE_MAX_LOD, GL_TEXTURE_MIN_FILTER, GL_TEXTURE_MIN_LOD, GL_TEXTURE_SWIZZLE_R, GL_TEXTURE_SWIZZLE_G, GL_TEXTURE_SWIZZLE_B, GL_TEXTURE_SWIZZLE_A, GL_TEXTURE_SWIZZLE_RGBA, GL_TEXTURE_TARGET, GL_TEXTURE_VIEW_MIN_LAYER, GL_TEXTURE_VIEW_MIN_LEVEL, GL_TEXTURE_VIEW_NUM_LAYERS, GL_TEXTURE_VIEW_NUM_LEVELS, GL_TEXTURE_WRAP_S, GL_TEXTURE_WRAP_T, and GL_TEXTURE_WRAP_R are accepted.</param>
        /// <param name="params">Returns the texture parameters.</param>
        public void glGetTextureParameterI(uint texture, uint pname, uint[] @params)
        {
            GetDelegateFor<glGetTextureParameterIuiv>()(texture, pname, @params);
        }

        /// <summary>
        /// Return texture parameter values.
        /// </summary>
        /// <param name="texture">Specifies the texture object name for glGetTextureParameterfv, glGetTextureParameteriv, glGetTextureParameterIiv, and glGetTextureParameterIuiv functions.</param>
        /// <param name="pname">Specifies the symbolic name of a texture parameter. GL_DEPTH_STENCIL_TEXTURE_MODE, GL_IMAGE_FORMAT_COMPATIBILITY_TYPE, GL_TEXTURE_BASE_LEVEL, GL_TEXTURE_BORDER_COLOR, GL_TEXTURE_COMPARE_MODE, GL_TEXTURE_COMPARE_FUNC, GL_TEXTURE_IMMUTABLE_FORMAT, GL_TEXTURE_IMMUTABLE_LEVELS, GL_TEXTURE_LOD_BIAS, GL_TEXTURE_MAG_FILTER, GL_TEXTURE_MAX_LEVEL, GL_TEXTURE_MAX_LOD, GL_TEXTURE_MIN_FILTER, GL_TEXTURE_MIN_LOD, GL_TEXTURE_SWIZZLE_R, GL_TEXTURE_SWIZZLE_G, GL_TEXTURE_SWIZZLE_B, GL_TEXTURE_SWIZZLE_A, GL_TEXTURE_SWIZZLE_RGBA, GL_TEXTURE_TARGET, GL_TEXTURE_VIEW_MIN_LAYER, GL_TEXTURE_VIEW_MIN_LEVEL, GL_TEXTURE_VIEW_NUM_LAYERS, GL_TEXTURE_VIEW_NUM_LEVELS, GL_TEXTURE_WRAP_S, GL_TEXTURE_WRAP_T, and GL_TEXTURE_WRAP_R are accepted.</param>
        /// <param name="params">Returns the texture parameters.</param>
        public void glGetTextureParameter(uint texture, uint pname, int[] @params)
        {
            GetDelegateFor<glGetTextureParameteriv>()(texture, pname, @params);
        }
        
        /// <summary>
        /// Create vertex array objects.
        /// </summary>
        /// <param name="n">Number of vertex array objects to create.</param>
        /// <param name="arrays">Specifies an array in which names of the new vertex array objects are stored.</param>
        public void CreateVertexArrays(int n, uint[] arrays)
        {
            GetDelegateFor<glCreateVertexArrays>()(n, arrays);
        }
        
        /// <summary>
        /// Enable or disable a generic vertex attribute array
        /// </summary>
        /// <param name="vaobj">Specifies the name of the vertex array object for glDisableVertexArrayAttrib and glEnableVertexArrayAttrib functions.</param>
        /// <param name="index">Specifies the index of the generic vertex attribute to be enabled or disabled.</param>
        public void DisableVertexArrayAttrib(uint vaobj, uint index)
        {
            GetDelegateFor<glDisableVertexArrayAttrib>()(vaobj, index);
        }

        /// <summary>
        /// Enable or disable a generic vertex attribute array
        /// </summary>
        /// <param name="vaobj">Specifies the name of the vertex array object for glDisableVertexArrayAttrib and glEnableVertexArrayAttrib functions.</param>
        /// <param name="index">Specifies the index of the generic vertex attribute to be enabled or disabled.</param>
        public void EnableVertexArrayAttrib(uint vaobj, uint index)
        {
            GetDelegateFor<glEnableVertexArrayAttrib>()(vaobj, index);
        }
        
        /// <summary>
        /// Configures element array buffer binding of a vertex array object.
        /// </summary>
        /// <param name="vaobj">Specifies the name of the vertex array object.</param>
        /// <param name="buffer">Specifies the name of the buffer object to use for the element array buffer binding.</param>
        public void VertexArrayElementBuffer(uint vaobj, uint buffer)
        {
            GetDelegateFor<glVertexArrayElementBuffer>()(vaobj, buffer);
        }


        /// <summary>
        /// Bind a buffer to a vertex buffer bind point.
        /// </summary>
        /// <param name="vaobj">Specifies the name of the vertex array object to be used by glVertexArrayVertexBuffer function.</param>
        /// <param name="bindingindex">The index of the vertex buffer binding point to which to bind the buffer.</param>
        /// <param name="buffer">The name of a buffer to bind to the vertex buffer binding point.</param>
        /// <param name="offset">The offset of the first element of the buffer.</param>
        /// <param name="stride">The distance between elements within the buffer.</param>
        public void VertexArrayVertexBuffer(uint vaobj, uint bindingindex, uint buffer, int offset, int stride)
        {
            GetDelegateFor<glVertexArrayVertexBuffer>()(vaobj, bindingindex, buffer, offset, stride);
        }

        /// <summary>
        /// Attach multiple buffer objects to a vertex array object.
        /// </summary>
        /// <param name="vaobj">Specifies the name of the vertex array object for glVertexArrayVertexBuffers.</param>
        /// <param name="first">Specifies the first vertex buffer binding point to which a buffer object is to be bound.</param>
        /// <param name="count">Specifies the number of buffers to bind.</param>
        /// <param name="buffers">Specifies the address of an array of names of existing buffer objects.</param>
        /// <param name="offsets">Specifies the address of an array of offsets to associate with the binding points.</param>
        /// <param name="strides">Specifies the address of an array of strides to associate with the binding points.</param>
        public void VertexArrayVertexBuffers(uint vaobj, uint first, int count, uint[] buffers, int[] offsets,
                                               int[] strides)
        {
            GetDelegateFor<glVertexArrayVertexBuffers>()(vaobj, first, count, buffers, offsets, strides);
        }

        /// <summary>
        /// Specify the organization of vertex arrays.
        /// </summary>
        /// <param name="vaobj">Specifies the name of the vertex array object for glVertexArrayAttrib{I, L}Format functions.</param>
        /// <param name="attribindex">The generic vertex attribute array being described.</param>
        /// <param name="size">The number of values per vertex that are stored in the array.</param>
        /// <param name="type">The type of the data stored in the array.</param>
        /// <param name="normalized">The distance between elements within the buffer.</param>
        /// <param name="relativeoffset">The distance between elements within the buffer.</param>
        public void VertexArrayAttribFormat(uint vaobj, uint attribindex, int size, uint type, bool normalized,
                                              uint relativeoffset)
        {
            GetDelegateFor<glVertexArrayAttribFormat>()(vaobj, attribindex, size, type, normalized, relativeoffset);
        }

        /// <summary>
        /// Specify the organization of vertex arrays.
        /// </summary>
        /// <param name="vaobj">Specifies the name of the vertex array object for glVertexArrayAttrib{I, L}Format functions.</param>
        /// <param name="attribindex">The generic vertex attribute array being described.</param>
        /// <param name="size">The number of values per vertex that are stored in the array.</param>
        /// <param name="type">The type of the data stored in the array.</param>
        /// <param name="relativeoffset">The distance between elements within the buffer.</param>
        public void VertexArrayAttribIFormat(uint vaobj, uint attribindex, int size, uint type, uint relativeoffset)
        {
            GetDelegateFor<glVertexArrayAttribIFormat>()(vaobj, attribindex, size, type, relativeoffset);
        }

        /// <summary>
        /// Specify the organization of vertex arrays.
        /// </summary>
        /// <param name="vaobj">Specifies the name of the vertex array object for glVertexArrayAttrib{I, L}Format functions.</param>
        /// <param name="attribindex">The generic vertex attribute array being described.</param>
        /// <param name="size">The number of values per vertex that are stored in the array.</param>
        /// <param name="type">The type of the data stored in the array.</param>
        /// <param name="relativeoffset">The distance between elements within the buffer.</param>
        public void VertexArrayAttribLFormat(uint vaobj, uint attribindex, int size, uint type, uint relativeoffset)
        {
            GetDelegateFor<glVertexArrayAttribLFormat>()(vaobj, attribindex, size, type, relativeoffset);
        }

        /// <summary>
        /// Associate a vertex attribute and a vertex buffer binding for a vertex array object.
        /// </summary>
        /// <param name="vaobj">Specifies the name of the vertex array object for glVertexArrayAttribBinding.</param>
        /// <param name="attribindex">The index of the attribute to associate with a vertex buffer binding.</param>
        /// <param name="bindingindex">The index of the vertex buffer binding with which to associate the generic vertex attribute.</param>
        public void VertexArrayAttribBinding(uint vaobj, uint attribindex, uint bindingindex)
        {
            GetDelegateFor<glVertexArrayAttribBinding>()(vaobj, attribindex, bindingindex);
        }

        /// <summary>
        /// Modify the rate at which generic vertex attributes advance.
        /// </summary>
        /// <param name="vaobj">Specifies the name of the vertex array object for glVertexArrayBindingDivisor function.</param>
        /// <param name="bindingindex">The index of the binding whose divisor to modify.</param>
        /// <param name="divisor">The new value for the instance step rate to apply.</param>
        public void VertexArrayBindingDivisor(uint vaobj, uint bindingindex, uint divisor)
        {
            GetDelegateFor<glVertexArrayBindingDivisor>()(vaobj, bindingindex, divisor);
        }

        /// <summary>
        /// Retrieve parameters of a vertex array object.
        /// </summary>
        /// <param name="vaobj">Specifies the name of the vertex array object to use for the query.</param>
        /// <param name="pname">Name of the property to use for the query. Must be GL_ELEMENT_ARRAY_BUFFER_BINDING.</param>
        /// <param name="param">Returns the requested value.</param>
        public void GetVertexArray(uint vaobj, uint pname, out int param)
        {
            GetDelegateFor<glGetVertexArrayiv>()(vaobj, pname, out param);
        }

        /// <summary>
        /// Retrieve parameters of an attribute of a vertex array object.
        /// </summary>
        /// <param name="vaobj">Specifies the name of a vertex array object.</param>
        /// <param name="index">Specifies the index of the vertex array object attribute. Must be a number between 0 and (GL_MAX_VERTEX_ATTRIBS - 1).</param>
        /// <param name="pname">Specifies the property to be used for the query. For glGetVertexArrayIndexediv, it must be one of the following values: GL_VERTEX_ATTRIB_ARRAY_ENABLED, GL_VERTEX_ATTRIB_ARRAY_SIZE, GL_VERTEX_ATTRIB_ARRAY_STRIDE, GL_VERTEX_ATTRIB_ARRAY_TYPE, GL_VERTEX_ATTRIB_ARRAY_NORMALIZED, GL_VERTEX_ATTRIB_ARRAY_INTEGER, GL_VERTEX_ATTRIB_ARRAY_LONG, GL_VERTEX_ATTRIB_ARRAY_DIVISOR, or GL_VERTEX_ATTRIB_RELATIVE_OFFSET. For glGetVertexArrayIndexed64v, it must be equal to GL_VERTEX_BINDING_OFFSET.</param>
        /// <param name="param">Returns the requested value.</param>
        public void glGetVertexArrayIndexed(uint vaobj, uint index, uint pname, int[] param)
        {
            GetDelegateFor<glGetVertexArrayIndexediv>()(vaobj, index, pname, param);
        }

        /// <summary>
        /// Retrieve parameters of an attribute of a vertex array object.
        /// </summary>
        /// <param name="vaobj">Specifies the name of a vertex array object.</param>
        /// <param name="index">Specifies the index of the vertex array object attribute. Must be a number between 0 and (GL_MAX_VERTEX_ATTRIBS - 1).</param>
        /// <param name="pname">Specifies the property to be used for the query. For glGetVertexArrayIndexediv, it must be one of the following values: GL_VERTEX_ATTRIB_ARRAY_ENABLED, GL_VERTEX_ATTRIB_ARRAY_SIZE, GL_VERTEX_ATTRIB_ARRAY_STRIDE, GL_VERTEX_ATTRIB_ARRAY_TYPE, GL_VERTEX_ATTRIB_ARRAY_NORMALIZED, GL_VERTEX_ATTRIB_ARRAY_INTEGER, GL_VERTEX_ATTRIB_ARRAY_LONG, GL_VERTEX_ATTRIB_ARRAY_DIVISOR, or GL_VERTEX_ATTRIB_RELATIVE_OFFSET. For glGetVertexArrayIndexed64v, it must be equal to GL_VERTEX_BINDING_OFFSET.</param>
        /// <param name="param">Returns the requested value.</param>
        public void glGetVertexArrayIndexed(uint vaobj, uint index, uint pname, Int64[] param)
        {
            GetDelegateFor<glGetVertexArrayIndexed64iv>()(vaobj, index, pname, param);
        }

        /// <summary>
        /// Create sampler objects.
        /// </summary>
        /// <param name="n">Number of sampler objects to create.</param>
        /// <param name="samplers">Specifies an array in which names of the new sampler objects are stored.</param>
        public void CreateSamplers(int n, uint[] samplers)
        {
            GetDelegateFor<glCreateSamplers>()(n, samplers);
        }

        /// <summary>
        /// Create program pipeline objects.
        /// </summary>
        /// <param name="n">Number of program pipeline objects to create.</param>
        /// <param name="pipelines">Specifies an array in which names of the new program pipeline objects are stored.</param>
        public void CreateProgramPipelines(int n, uint[] pipelines)
        {
            GetDelegateFor<glCreateProgramPipelines>()(n, pipelines);
        }

        /// <summary>
        /// Create query objects.
        /// </summary>
        /// <param name="target">Specifies the target of each created query object.</param>
        /// <param name="n">Number of query objects to create.</param>
        /// <param name="ids">Specifies an array in which names of the new query objects are stored.</param>
        public void CreateQueries(uint target, int n, uint[] ids)
        {
            GetDelegateFor<glCreateQueries>()(target, n, ids);
        }

        /// <summary>
        /// Documentation unavailable: https://www.khronos.org/bugzilla/show_bug.cgi?id=1252
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="buffer">The buffer.</param>
        /// <param name="pname">The pname.</param>
        /// <param name="offset">The offset.</param>
        public void GetQueryBufferObjectiv(uint id, uint buffer, uint pname, int offset)
        {
            GetDelegateFor<glGetQueryBufferObjectiv>()(id, buffer, pname, offset);
        }

        /// <summary>
        /// Documentation unavailable: https://www.khronos.org/bugzilla/show_bug.cgi?id=1252
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="buffer">The buffer.</param>
        /// <param name="pname">The pname.</param>
        /// <param name="offset">The offset.</param>
        public void GetQueryBufferObjectuiv(uint id, uint buffer, uint pname, int offset)
        {
            GetDelegateFor<glGetQueryBufferObjectuiv>()(id, buffer, pname, offset);
        }

        /// <summary>
        /// Documentation unavailable: https://www.khronos.org/bugzilla/show_bug.cgi?id=1252
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="buffer">The buffer.</param>
        /// <param name="pname">The pname.</param>
        /// <param name="offset">The offset.</param>
        public void GetQueryBufferObjecti64v(uint id, uint buffer, uint pname, int offset)
        {
            GetDelegateFor<glGetQueryBufferObjecti64v>()(id, buffer, pname, offset);
        }

        /// <summary>
        /// Documentation unavailable: https://www.khronos.org/bugzilla/show_bug.cgi?id=1252
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="buffer">The buffer.</param>
        /// <param name="pname">The pname.</param>
        /// <param name="offset">The offset.</param>
        public void GetQueryBufferObjectui64v(uint id, uint buffer, uint pname, int offset)
        {
            GetDelegateFor<glGetQueryBufferObjectui64v>()(id, buffer, pname, offset);
        }

        #endregion

        #region ARB_get_texture_sub_image

        private delegate void glGetTextureSubImage(uint texture, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, uint type, int bufSize, IntPtr pixels);
        private delegate void glGetCompressedTextureSubImage(uint texture, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, int bufSize, IntPtr pixels);

        /// <summary>
        /// Retrieve a sub-region of a texture image from a texture object.
        /// </summary>
        /// <param name="texture">Specifies the name of the source texture object. Must be GL_TEXTURE_1D, GL_TEXTURE_1D_ARRAY, GL_TEXTURE_2D, GL_TEXTURE_2D_ARRAY, GL_TEXTURE_3D, GL_TEXTURE_CUBE_MAP, GL_TEXTURE_CUBE_MAP_ARRAY or GL_TEXTURE_RECTANGLE. In specific, buffer and multisample textures are not permitted.</param>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xoffset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="yoffset">Specifies a texel offset in the y direction within the texture array.</param>
        /// <param name="zoffset">Specifies a texel offset in the z direction within the texture array.</param>
        /// <param name="width">Specifies the width of the texture subimage.</param>
        /// <param name="height">Specifies the height of the texture subimage.</param>
        /// <param name="depth">Specifies the depth of the texture subimage.</param>
        /// <param name="format">Specifies the format of the pixel data. The following symbolic values are accepted: GL_RED, GL_RG, GL_RGB, GL_BGR, GL_RGBA, GL_BGRA, GL_DEPTH_COMPONENT and GL_STENCIL_INDEX.</param>
        /// <param name="type">Specifies the data type of the pixel data. The following symbolic values are accepted: GL_UNSIGNED_BYTE, GL_BYTE, GL_UNSIGNED_SHORT, GL_SHORT, GL_UNSIGNED_INT, GL_INT, GL_FLOAT, GL_UNSIGNED_BYTE_3_3_2, GL_UNSIGNED_BYTE_2_3_3_REV, GL_UNSIGNED_SHORT_5_6_5, GL_UNSIGNED_SHORT_5_6_5_REV, GL_UNSIGNED_SHORT_4_4_4_4, GL_UNSIGNED_SHORT_4_4_4_4_REV, GL_UNSIGNED_SHORT_5_5_5_1, GL_UNSIGNED_SHORT_1_5_5_5_REV, GL_UNSIGNED_INT_8_8_8_8, GL_UNSIGNED_INT_8_8_8_8_REV, GL_UNSIGNED_INT_10_10_10_2, and GL_UNSIGNED_INT_2_10_10_10_REV.</param>
        /// <param name="bufSize">Specifies the size of the buffer to receive the retrieved pixel data.</param>
        /// <param name="pixels">Returns the texture subimage. Should be a pointer to an array of the type specified by type.</param>
        public void GetTextureSubImage(uint texture, int level, int xoffset, int yoffset, int zoffset, int width,
                                       int height, int depth, uint format, uint type, int bufSize, IntPtr pixels)
        {
            GetDelegateFor<glGetTextureSubImage>()(texture, level, xoffset, yoffset, zoffset, width, height,
                                                   depth, format, type, bufSize, pixels);
        }

        /// <summary>
        /// Retrieve a sub-region of a compressed texture image from a compressed texture object.
        /// </summary>
        /// <param name="texture">Specifies the name of the source texture object. Must be GL_TEXTURE_1D, GL_TEXTURE_1D_ARRAY, GL_TEXTURE_2D, GL_TEXTURE_2D_ARRAY, GL_TEXTURE_3D, GL_TEXTURE_CUBE_MAP, GL_TEXTURE_CUBE_MAP_ARRAY or GL_TEXTURE_RECTANGLE. In specific, buffer and multisample textures are not permitted.</param>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xoffset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="yoffset">Specifies a texel offset in the y direction within the texture array..</param>
        /// <param name="zoffset">Specifies a texel offset in the z direction within the texture array.</param>
        /// <param name="width">Specifies the width of the texture subimage. Must be a multiple of the compressed block's width, unless the offset is zero and the size equals the texture image size.</param>
        /// <param name="height">Specifies the height of the texture subimage. Must be a multiple of the compressed block's height, unless the offset is zero and the size equals the texture image size.</param>
        /// <param name="depth">Specifies the depth of the texture subimage. Must be a multiple of the compressed block's depth, unless the offset is zero and the size equals the texture image size.</param>
        /// <param name="bufSize">Specifies the size of the buffer to receive the retrieved pixel data.</param>
        /// <param name="pixels">Returns the texture subimage. Should be a pointer to an array of the type specified by type.</param>
        public void GetCompressedTextureSubImage(uint texture, int level, int xoffset, int yoffset, int zoffset,
                                                 int width, int height, int depth, int bufSize, IntPtr pixels)
        {
            GetDelegateFor<glGetCompressedTextureSubImage>()(texture, level, xoffset, yoffset, zoffset,
                                                             width, height, depth, bufSize, pixels);
        }

        #endregion

        #region KHR_robustness

        private delegate uint glGetGraphicsResetStatus();
        private delegate void glReadnPixels(int x, int y, int width, int height, uint format, uint type, int bufSize, IntPtr data);
        private delegate void glGetnUniformfv(uint program, int location, int bufSize, float[] @params);
        private delegate void glGetnUniformiv(uint program, int location, int bufSize, int[] @params);
        private delegate void glGetnUniformuiv(uint program, int location, int bufSize, uint[] @params);

        /// <summary>
        /// Check if the rendering context has not been lost due to software or hardware issues.
        /// </summary>
        /// <returns>
        /// glGetGraphicsResetStatus can return one of the following constants:
        /// GL_NO_ERROR
        /// Indicates that the GL context has not been in a reset state since the last call.
        /// GL_GUILTY_CONTEXT_RESET
        /// Indicates that a reset has been detected that is attributable to the current GL context.
        /// GL_INNOCENT_CONTEXT_RESET
        /// Indicates a reset has been detected that is not attributable to the current GL context.
        /// GL_UNKNOWN_CONTEXT_RESET
        /// Indicates a detected graphics reset whose cause is unknown.
        /// </returns>
        public uint GetGraphicsResetStatus()
        {
            return GetDelegateFor<glGetGraphicsResetStatus>()();
        }

        /// <summary>
        /// Read a block of pixels from the frame buffer.
        /// </summary>
        /// <param name="x">Specify the window coordinates of the first pixel that is read from the frame buffer. This location is the lower left corner of a rectangular block of pixels.</param>
        /// <param name="y">Specify the window coordinates of the first pixel that is read from the frame buffer. This location is the lower left corner of a rectangular block of pixels.</param>
        /// <param name="width">Specify the dimensions of the pixel rectangle. width and height of one correspond to a single pixel.</param>
        /// <param name="height">Specify the dimensions of the pixel rectangle. width and height of one correspond to a single pixel.</param>
        /// <param name="format">Specifies the format of the pixel data. The following symbolic values are accepted: GL_STENCIL_INDEX, GL_DEPTH_COMPONENT, GL_DEPTH_STENCIL, GL_RED, GL_GREEN, GL_BLUE, GL_RGB, GL_BGR, GL_RGBA, and GL_BGRA.</param>
        /// <param name="type">Specifies the data type of the pixel data. Must be one of GL_UNSIGNED_BYTE, GL_BYTE, GL_UNSIGNED_SHORT, GL_SHORT, GL_UNSIGNED_INT, GL_INT, GL_HALF_FLOAT, GL_FLOAT, GL_UNSIGNED_BYTE_3_3_2, GL_UNSIGNED_BYTE_2_3_3_REV, GL_UNSIGNED_SHORT_5_6_5, GL_UNSIGNED_SHORT_5_6_5_REV, GL_UNSIGNED_SHORT_4_4_4_4, GL_UNSIGNED_SHORT_4_4_4_4_REV, GL_UNSIGNED_SHORT_5_5_5_1, GL_UNSIGNED_SHORT_1_5_5_5_REV, GL_UNSIGNED_INT_8_8_8_8, GL_UNSIGNED_INT_8_8_8_8_REV, GL_UNSIGNED_INT_10_10_10_2, GL_UNSIGNED_INT_2_10_10_10_REV, GL_UNSIGNED_INT_24_8, GL_UNSIGNED_INT_10F_11F_11F_REV, GL_UNSIGNED_INT_5_9_9_9_REV, or GL_FLOAT_32_UNSIGNED_INT_24_8_REV.</param>
        /// <param name="bufSize">Specifies the size of the buffer data for glReadnPixels function.</param>
        /// <param name="data">Returns the pixel data.</param>
        public void ReadnPixels(int x, int y, int width, int height, uint format, uint type, int bufSize, IntPtr data)
        {
            GetDelegateFor<glReadnPixels>()(x, y, width, height, format, type, bufSize, data);
        }

        /// <summary>
        /// Return the values of uniform variables.
        /// </summary>
        /// <param name="program">Specifies the program object to be queried.</param>
        /// <param name="location">Specifies the location of the uniform variable to be queried.</param>
        /// <param name="bufSize">Size of the buffer <paramref name="params"/>.</param>
        /// <param name="params">Returns the value of the specified uniform variables.</param>
        public void GetnUniform(uint program, int location, int bufSize, float[] @params)
        {
            GetDelegateFor<glGetnUniformfv>()(program, location, bufSize, @params);
        }

        /// <summary>
        /// Return the values of uniform variables.
        /// </summary>
        /// <param name="program">Specifies the program object to be queried.</param>
        /// <param name="location">Specifies the location of the uniform variable to be queried.</param>
        /// <param name="bufSize">Size of the buffer <paramref name="params"/>.</param>
        /// <param name="params">Returns the value of the specified uniform variables.</param>
        public void GetnUniform(uint program, int location, int bufSize, int[] @params)
        {
            GetDelegateFor<glGetnUniformiv>()(program, location, bufSize, @params);
        }

        /// <summary>
        /// Return the values of uniform variables.
        /// </summary>
        /// <param name="program">Specifies the program object to be queried.</param>
        /// <param name="location">Specifies the location of the uniform variable to be queried.</param>
        /// <param name="bufSize">Size of the buffer <paramref name="params"/>.</param>
        /// <param name="params">Returns the value of the specified uniform variables.</param>
        public void GetnUniform(uint program, int location, int bufSize, uint[] @params)
        {
            GetDelegateFor<glGetnUniformuiv>()(program, location, bufSize, @params);
        }

        #endregion

        #region ARB_texture_barrier
        
        private delegate void glTextureBarrier();

        /// <summary>
        /// Controls the ordering of reads and writes to rendered fragments across drawing commands.
        /// </summary>
        public void TextureBarrier()
        {
            GetDelegateFor<glTextureBarrier>()();
        }

        #endregion
    }

// ReSharper restore InconsistentNaming
}
