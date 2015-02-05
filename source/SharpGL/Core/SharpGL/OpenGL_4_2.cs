using System;

namespace SharpGL
{
// ReSharper disable InconsistentNaming

    public partial class OpenGL
    {
        #region ARB_shader_atomic_counters

        private delegate void glGetActiveAtomicCounterBufferiv(uint program, uint bufferIndex, uint pname, int[] @params
            );

        /// <summary>
        /// Retrieve information about the set of active atomic counter buffers for a program.
        /// </summary>
        /// <param name="program">The name of a program object from which to retrieve information.</param>
        /// <param name="bufferIndex">Specifies index of an active atomic counter buffer.</param>
        /// <param name="pname">Specifies which parameter of the atomic counter buffer to retrieve.</param>
        /// <param name="params">Specifies the address of a variable into which to write the retrieved information.</param>
        public void GetActiveAtomicCounterBuffer(uint program, uint bufferIndex, uint pname, int[] @params)
        {
            GetDelegateFor<glGetActiveAtomicCounterBufferiv>()(program, bufferIndex, pname, @params);
        }

        public const uint GL_ATOMIC_COUNTER_BUFFER = 0x92C0;
        public const uint GL_ATOMIC_COUNTER_BUFFER_BINDING = 0x92C1;
        public const uint GL_ATOMIC_COUNTER_BUFFER_START = 0x92C2;
        public const uint GL_ATOMIC_COUNTER_BUFFER_SIZE = 0x92C3;
        public const uint GL_ATOMIC_COUNTER_BUFFER_DATA_SIZE = 0x92C4;
        public const uint GL_ATOMIC_COUNTER_BUFFER_ACTIVE_ATOMIC_COUNTERS = 0x92C5;
        public const uint GL_ATOMIC_COUNTER_BUFFER_ACTIVE_ATOMIC_COUNTER_INDICES = 0x92C6;
        public const uint GL_ATOMIC_COUNTER_BUFFER_REFERENCED_BY_VERTEX_SHADER = 0x92C7;
        public const uint GL_ATOMIC_COUNTER_BUFFER_REFERENCED_BY_TESS_CONTROL_SHADER = 0x92C8;
        public const uint GL_ATOMIC_COUNTER_BUFFER_REFERENCED_BY_TESS_EVALUATION_SHADER = 0x92C9;
        public const uint GL_ATOMIC_COUNTER_BUFFER_REFERENCED_BY_GEOMETRY_SHADER = 0x92CA;
        public const uint GL_ATOMIC_COUNTER_BUFFER_REFERENCED_BY_FRAGMENT_SHADER = 0x92CB;
        public const uint GL_MAX_VERTEX_ATOMIC_COUNTER_BUFFERS = 0x92CC;
        public const uint GL_MAX_TESS_CONTROL_ATOMIC_COUNTER_BUFFERS = 0x92CD;
        public const uint GL_MAX_TESS_EVALUATION_ATOMIC_COUNTER_BUFFERS = 0x92CE;
        public const uint GL_MAX_GEOMETRY_ATOMIC_COUNTER_BUFFERS = 0x92CF;
        public const uint GL_MAX_FRAGMENT_ATOMIC_COUNTER_BUFFERS = 0x92D0;
        public const uint GL_MAX_COMBINED_ATOMIC_COUNTER_BUFFERS = 0x92D1;
        public const uint GL_MAX_VERTEX_ATOMIC_COUNTERS = 0x92D2;
        public const uint GL_MAX_TESS_CONTROL_ATOMIC_COUNTERS = 0x92D3;
        public const uint GL_MAX_TESS_EVALUATION_ATOMIC_COUNTERS = 0x92D4;
        public const uint GL_MAX_GEOMETRY_ATOMIC_COUNTERS = 0x92D5;
        public const uint GL_MAX_FRAGMENT_ATOMIC_COUNTERS = 0x92D6;
        public const uint GL_MAX_COMBINED_ATOMIC_COUNTERS = 0x92D7;
        public const uint GL_MAX_ATOMIC_COUNTER_BUFFER_SIZE = 0x92D8;
        public const uint GL_MAX_ATOMIC_COUNTER_BUFFER_BINDINGS = 0x92DC;
        public const uint GL_ACTIVE_ATOMIC_COUNTER_BUFFERS = 0x92D9;
        public const uint GL_UNIFORM_ATOMIC_COUNTER_BUFFER_INDEX = 0x92DA;
        public const uint GL_UNSIGNED_INT_ATOMIC_COUNTER = 0x92DB;

        #endregion

        #region ARB_shader_image_load_store

        private delegate void glBindImageTexture(
            uint unit, uint texture, int level, bool layered, int layer, uint access, uint format);

        private delegate void glMemoryBarrier(uint barriers);

        /// <summary>
        /// Bind a level of a texture to an image unit.
        /// </summary>
        /// <param name="unit">Specifies the index of the image unit to which to bind the texture.</param>
        /// <param name="texture">Specifies the name of the texture to bind to the image unit.</param>
        /// <param name="level">Specifies the level of the texture that is to be bound.</param>
        /// <param name="layered">Specifies whether a layered texture binding is to be established.</param>
        /// <param name="layer">If layered is GL_FALSE, specifies the layer of texture to be bound to the image unit. Ignored otherwise.</param>
        /// <param name="access">Specifies a token indicating the type of access that will be performed on the image.</param>
        /// <param name="format">Specifies the format that the elements of the image will be treated as for the purposes of formatted stores.</param>
        public void BindImageTexture(uint unit, uint texture, int level, bool layered, int layer, uint access,
                                     uint format)
        {
            GetDelegateFor<glBindImageTexture>()(unit, texture, level, layered, layer, access, format);
        }

        /// <summary>
        /// Defines a barrier ordering memory transactions.
        /// </summary>
        /// <param name="barriers">Specifies the barriers to insert. Must be a bitwise combination of GL_VERTEX_ATTRIB_ARRAY_BARRIER_BIT​, GL_ELEMENT_ARRAY_BARRIER_BIT​, GL_UNIFORM_BARRIER_BIT​, GL_TEXTURE_FETCH_BARRIER_BIT​, GL_SHADER_IMAGE_ACCESS_BARRIER_BIT​, GL_COMMAND_BARRIER_BIT​, GL_PIXEL_BUFFER_BARRIER_BIT​, GL_TEXTURE_UPDATE_BARRIER_BIT​, GL_BUFFER_UPDATE_BARRIER_BIT​, GL_FRAMEBUFFER_BARRIER_BIT​, GL_TRANSFORM_FEEDBACK_BARRIER_BIT​, GL_QUERY_BUFFER_BARRIER_BIT​, GL_ATOMIC_COUNTER_BARRIER_BIT​, GL_CLIENT_MAPPED_BUFFER_BARRIER_BIT​, or GL_SHADER_STORAGE_BARRIER_BIT​. If the special value GL_ALL_BARRIER_BITS​ is specified, all supported barriers will be inserted.</param>
        public void MemoryBarrier(uint barriers)
        {
            GetDelegateFor<glMemoryBarrier>()(barriers);
        }

        public uint GL_MAX_IMAGE_UNITS = 0x8F38;
        public uint GL_MAX_COMBINED_IMAGE_UNITS_AND_FRAGMENT_OUTPUTS = 0x8F39;
        public uint GL_MAX_IMAGE_SAMPLES = 0x906D;
        public uint GL_MAX_VERTEX_IMAGE_UNIFORMS = 0x90CA;
        public uint GL_MAX_TESS_CONTROL_IMAGE_UNIFORMS = 0x90CB;
        public uint GL_MAX_TESS_EVALUATION_IMAGE_UNIFORMS = 0x90CC;
        public uint GL_MAX_GEOMETRY_IMAGE_UNIFORMS = 0x90CD;
        public uint GL_MAX_FRAGMENT_IMAGE_UNIFORMS = 0x90CE;
        public uint GL_MAX_COMBINED_IMAGE_UNIFORMS = 0x90CF;
        public uint GL_IMAGE_BINDING_NAME = 0x8F3A;
        public uint GL_IMAGE_BINDING_LEVEL = 0x8F3B;
        public uint GL_IMAGE_BINDING_LAYERED = 0x8F3C;
        public uint GL_IMAGE_BINDING_LAYER = 0x8F3D;
        public uint GL_IMAGE_BINDING_ACCESS = 0x8F3E;
        public uint GL_IMAGE_BINDING_FORMAT = 0x906E;
        public uint GL_VERTEX_ATTRIB_ARRAY_BARRIER_BIT = 0x00000001;
        public uint GL_ELEMENT_ARRAY_BARRIER_BIT = 0x00000002;
        public uint GL_UNIFORM_BARRIER_BIT = 0x00000004;
        public uint GL_TEXTURE_FETCH_BARRIER_BIT = 0x00000008;
        public uint GL_SHADER_IMAGE_ACCESS_BARRIER_BIT = 0x00000020;
        public uint GL_COMMAND_BARRIER_BIT = 0x00000040;
        public uint GL_PIXEL_BUFFER_BARRIER_BIT = 0x00000080;
        public uint GL_TEXTURE_UPDATE_BARRIER_BIT = 0x00000100;
        public uint GL_BUFFER_UPDATE_BARRIER_BIT = 0x00000200;
        public uint GL_FRAMEBUFFER_BARRIER_BIT = 0x00000400;
        public uint GL_TRANSFORM_FEEDBACK_BARRIER_BIT = 0x00000800;
        public uint GL_ATOMIC_COUNTER_BARRIER_BIT = 0x00001000;
        public uint GL_ALL_BARRIER_BITS = 0xFFFFFFFF;
        public uint GL_IMAGE_1D = 0x904C;
        public uint GL_IMAGE_2D = 0x904D;
        public uint GL_IMAGE_3D = 0x904E;
        public uint GL_IMAGE_2D_RECT = 0x904F;
        public uint GL_IMAGE_CUBE = 0x9050;
        public uint GL_IMAGE_BUFFER = 0x9051;
        public uint GL_IMAGE_1D_ARRAY = 0x9052;
        public uint GL_IMAGE_2D_ARRAY = 0x9053;
        public uint GL_IMAGE_CUBE_MAP_ARRAY = 0x9054;
        public uint GL_IMAGE_2D_MULTISAMPLE = 0x9055;
        public uint GL_IMAGE_2D_MULTISAMPLE_ARRAY = 0x9056;
        public uint GL_INT_IMAGE_1D = 0x9057;
        public uint GL_INT_IMAGE_2D = 0x9058;
        public uint GL_INT_IMAGE_3D = 0x9059;
        public uint GL_INT_IMAGE_2D_RECT = 0x905A;
        public uint GL_INT_IMAGE_CUBE = 0x905B;
        public uint GL_INT_IMAGE_BUFFER = 0x905C;
        public uint GL_INT_IMAGE_1D_ARRAY = 0x905D;
        public uint GL_INT_IMAGE_2D_ARRAY = 0x905E;
        public uint GL_INT_IMAGE_CUBE_MAP_ARRAY = 0x905F;
        public uint GL_INT_IMAGE_2D_MULTISAMPLE = 0x9060;
        public uint GL_INT_IMAGE_2D_MULTISAMPLE_ARRAY = 0x9061;
        public uint GL_UNSIGNED_INT_IMAGE_1D = 0x9062;
        public uint GL_UNSIGNED_INT_IMAGE_2D = 0x9063;
        public uint GL_UNSIGNED_INT_IMAGE_3D = 0x9064;
        public uint GL_UNSIGNED_INT_IMAGE_2D_RECT = 0x9065;
        public uint GL_UNSIGNED_INT_IMAGE_CUBE = 0x9066;
        public uint GL_UNSIGNED_INT_IMAGE_BUFFER = 0x9067;
        public uint GL_UNSIGNED_INT_IMAGE_1D_ARRAY = 0x9068;
        public uint GL_UNSIGNED_INT_IMAGE_2D_ARRAY = 0x9069;
        public uint GL_UNSIGNED_INT_IMAGE_CUBE_MAP_ARRAY = 0x906A;
        public uint GL_UNSIGNED_INT_IMAGE_2D_MULTISAMPLE = 0x906B;
        public uint GL_UNSIGNED_INT_IMAGE_2D_MULTISAMPLE_ARRAY = 0x906C;
        public uint GL_IMAGE_FORMAT_COMPATIBILITY_TYPE = 0x90C7;
        public uint GL_IMAGE_FORMAT_COMPATIBILITY_BY_SIZE = 0x90C8;
        public uint GL_IMAGE_FORMAT_COMPATIBILITY_BY_CLASS = 0x90C9;

        #endregion

        #region ARB_texture_storage
        
        private delegate void glTexStorage1D(uint target, int levels, uint internalformat, int width);
        private delegate void glTexStorage2D(uint target, int levels, uint internalformat, int width, int height);
        private delegate void glTexStorage3D(uint target, int levels, uint internalformat, int width, int height, int depth);

        //  When EXT_direct_state_access is present:
        //
        // void TextureStorage1DEXT(uint texture, uint target, int levels, uint internalformat, int width);
        // void TextureStorage2DEXT(uint texture, uint target, int levels, uint internalformat, int width, int height);
        // void TextureStorage3DEXT(uint texture, uint target, int levels, uint internalformat, int width, int height, int depth);

        /// <summary>
        /// Simultaneously specify storage for all levels of a one-dimensional texture.
        /// </summary>
        /// <param name="target">Specifies the target to which the texture object is bound for glTexStorage1D. Must be one of GL_TEXTURE_1D or GL_PROXY_TEXTURE_1D.</param>
        /// <param name="levels">Specify the number of texture levels.</param>
        /// <param name="internalformat">Specifies the sized internal format to be used to store texture image data.</param>
        /// <param name="width">Specifies the width of the texture, in texels.</param>
        public void TexStorage1D(uint target, int levels, uint internalformat, int width)
        {
            GetDelegateFor<glTexStorage1D>()(target, levels, internalformat, width);
        }

        /// <summary>
        /// Simultaneously specify storage for all levels of a two-dimensional or one-dimensional array texture.
        /// </summary>
        /// <param name="target">Specifies the target to which the texture object is bound for glTexStorage2D. Must be one of GL_TEXTURE_2D, GL_TEXTURE_1D_ARRAY, GL_TEXTURE_RECTANGLE, GL_PROXY_TEXTURE_2D, GL_PROXY_TEXTURE_1D_ARRAY, GL_PROXY_TEXTURE_RECTANGLE, or GL_PROXY_TEXTURE_CUBE_MAP.</param>
        /// <param name="levels">Specify the number of texture levels.</param>
        /// <param name="internalformat">Specifies the sized internal format to be used to store texture image data.</param>
        /// <param name="width">Specifies the width of the texture, in texels.</param>
        /// <param name="height">Specifies the height of the texture, in texels.</param>
        public void TexStorage2D(uint target, int levels, uint internalformat, int width, int height)
        {
            GetDelegateFor<glTexStorage2D>()(target, levels, internalformat, width, height);
        }

        /// <summary>
        /// Simultaneously specify storage for all levels of a three-dimensional, two-dimensional array or cube-map array texture.
        /// </summary>
        /// <param name="target">Specifies the target to which the texture object is bound for glTexStorage3D. Must be one of GL_TEXTURE_3D, GL_TEXTURE_2D_ARRAY, GL_TEXTURE_CUBE_ARRAY, GL_PROXY_TEXTURE_3D, GL_PROXY_TEXTURE_2D_ARRAY or GL_PROXY_TEXTURE_CUBE_ARRAY.</param>
        /// <param name="levels">Specify the number of texture levels.</param>
        /// <param name="internalformat">Specifies the sized internal format to be used to store texture image data.</param>
        /// <param name="width">Specifies the width of the texture, in texels.</param>
        /// <param name="height">Specifies the height of the texture, in texels.</param>
        /// <param name="depth">Specifies the depth of the texture, in texels.</param>
        public void TexStorage3D(uint target, int levels, uint internalformat, int width, int height, int depth)
        {
            GetDelegateFor<glTexStorage3D>()(target, levels, internalformat, width, height, depth);
        }

        public const uint GL_TEXTURE_IMMUTABLE_FORMAT = 0x912F;
        public const uint GL_ALPHA8_EXT = 0x803C;
        public const uint GL_LUMINANCE8_EXT = 0x8040;
        public const uint GL_LUMINANCE8_ALPHA8_EXT = 0x8045;

        #endregion

        #region ARB_transform_feedback_instanced

        private delegate void glDrawTransformFeedbackInstanced(uint mode, uint id, int primcount);
        private delegate void glDrawTransformFeedbackStreamInstanced(uint mode, uint id, uint stream, int primcount);

        /// <summary>
        /// Render multiple instances of primitives using a count derived from a transform feedback object.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render. Symbolic constants GL_POINTS​, GL_LINE_STRIP​, GL_LINE_LOOP​, GL_LINES​, GL_LINE_STRIP_ADJACENCY​, GL_LINES_ADJACENCY​, GL_TRIANGLE_STRIP​, GL_TRIANGLE_FAN​, GL_TRIANGLES​, GL_TRIANGLE_STRIP_ADJACENCY​, GL_TRIANGLES_ADJACENCY​, and GL_PATCHES​ are accepted.</param>
        /// <param name="id">Specifies the name of a transform feedback object from which to retrieve a primitive count.</param>
        /// <param name="primcount">Specifies the number of instances of the geometry to render.</param>
        public void DrawTransformFeedbackInstanced(uint mode, uint id, int primcount)
        {
            GetDelegateFor<glDrawTransformFeedbackInstanced>()(mode, id, primcount);
        }

        /// <summary>
        /// Render multiple instances of primitives using a count derived from a specifed stream of a transform feedback object.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render. Symbolic constants GL_POINTS​, GL_LINE_STRIP​, GL_LINE_LOOP​, GL_LINES​, GL_LINE_STRIP_ADJACENCY​, GL_LINES_ADJACENCY​, GL_TRIANGLE_STRIP​, GL_TRIANGLE_FAN​, GL_TRIANGLES​, GL_TRIANGLE_STRIP_ADJACENCY​, GL_TRIANGLES_ADJACENCY​, and GL_PATCHES​ are accepted.</param>
        /// <param name="id">Specifies the name of a transform feedback object from which to retrieve a primitive count.</param>
        /// <param name="stream">Specifies the index of the transform feedback stream from which to retrieve a primitive count.</param>
        /// <param name="primcount">Specifies the number of instances of the geometry to render.</param>
        public void DrawTransformFeedbackStreamInstanced(uint mode, uint id, uint stream, int primcount)
        {
            GetDelegateFor<glDrawTransformFeedbackStreamInstanced>()(mode, id, stream, primcount);
        }

        #endregion

        #region ARB_shading_language_420pack

        //  No new tokens or functions.

        #endregion

        #region ARB_base_instance

        private delegate void glDrawArraysInstancedBaseInstance(uint mode, int first, int count, int primcount, uint baseinstance);
        private delegate void glDrawElementsInstancedBaseInstance(uint mode, int count, uint type, IntPtr indices, int primcount, uint baseinstance);
        private delegate void glDrawElementsInstancedBaseVertexBaseInstance(uint mode, int count, uint type, IntPtr indices, int primcount, int basevertex, uint baseinstance);

        /// <summary>
        /// Draw multiple instances of a range of elements with offset applied to instanced attributes.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render. Symbolic constants GL_POINTS​, GL_LINE_STRIP​, GL_LINE_LOOP​, GL_LINES​, GL_TRIANGLE_STRIP​, GL_TRIANGLE_FAN​, GL_TRIANGLES​GL_LINES_ADJACENCY​, GL_LINE_STRIP_ADJACENCY​, GL_TRIANGLES_ADJACENCY​, GL_TRIANGLE_STRIP_ADJACENCY​ and GL_PATCHES​ are accepted.</param>
        /// <param name="first">Specifies the starting index in the enabled arrays.</param>
        /// <param name="count">Specifies the number of indices to be rendered.</param>
        /// <param name="primcount">Specifies the number of instances of the specified range of indices to be rendered.</param>
        /// <param name="baseinstance">Specifies the base instance for use in fetching instanced vertex attributes.</param>
        public void DrawArraysInstancedBaseInstance(uint mode, int first, int count, int primcount, uint baseinstance)
        {
            GetDelegateFor<glDrawArraysInstancedBaseInstance>()(mode, first, count, primcount, baseinstance);
        }

        /// <summary>
        /// Draw multiple instances of a set of elements with offset applied to instanced attributes.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render. Symbolic constants GL_POINTS, GL_LINE_STRIP, GL_LINE_LOOP, GL_LINES, GL_LINE_STRIP_ADJACENCY, GL_LINES_ADJACENCY, GL_TRIANGLE_STRIP, GL_TRIANGLE_FAN, GL_TRIANGLES, GL_TRIANGLE_STRIP_ADJACENCY, GL_TRIANGLES_ADJACENCY and GL_PATCHES are accepted.</param>
        /// <param name="count">Specifies the number of elements to be rendered.</param>
        /// <param name="type">Specifies the type of the values in indices. Must be one of GL_UNSIGNED_BYTE, GL_UNSIGNED_SHORT, or GL_UNSIGNED_INT.</param>
        /// <param name="indices">Specifies a pointer to the location where the indices are stored.</param>
        /// <param name="primcount">Specifies the number of instances of the specified range of indices to be rendered.</param>
        /// <param name="baseinstance">Specifies the base instance for use in fetching instanced vertex attributes.</param>
        public void DrawElementsInstancedBaseInstance(uint mode, int count, uint type, IntPtr indices, int primcount, uint baseinstance)
        {
            GetDelegateFor<glDrawElementsInstancedBaseInstance>()(mode, count, type, indices, primcount, baseinstance);
        }

        /// <summary>
        /// Render multiple instances of a set of primitives from array data with a per-element offset.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render. Symbolic constants GL_POINTS​, GL_LINE_STRIP​, GL_LINE_LOOP​, GL_LINES​, GL_TRIANGLE_STRIP​, GL_TRIANGLE_FAN​, GL_TRIANGLES​, GL_LINES_ADJACENCY​, GL_LINE_STRIP_ADJACENCY​, GL_TRIANGLES_ADJACENCY​, GL_TRIANGLE_STRIP_ADJACENCY​ and GL_PATCHES​ are accepted.</param>
        /// <param name="count">Specifies the number of elements to be rendered.</param>
        /// <param name="type">Specifies the type of the values in indices. Must be one of GL_UNSIGNED_BYTE​, GL_UNSIGNED_SHORT​, or GL_UNSIGNED_INT​.</param>
        /// <param name="indices">Specifies a byte offset (cast to a pointer type) into the buffer bound to GL_ELEMENT_ARRAY_BUFFER​ to start reading indices from.</param>
        /// <param name="primcount">Specifies the number of instances of the indexed geometry that should be drawn.</param>
        /// <param name="basevertex">Specifies a constant that should be added to each element of indices​ when chosing elements from the enabled vertex arrays.</param>
        /// <param name="baseinstance">Specifies the base instance for use in fetching instanced vertex attributes.</param>
        public void DrawElementsInstancedBaseVertexBaseInstance(uint mode, int count, uint type, IntPtr indices,
                                                                int primcount, int basevertex, uint baseinstance)
        {
            GetDelegateFor<glDrawElementsInstancedBaseVertexBaseInstance>()(mode, count, type, indices, primcount,
                                                                            basevertex, baseinstance);
        }

        #endregion

        #region ARB_internalformat_query

        private delegate void glGetInternalformativ(uint target, uint internalformat, uint pname, int bufSize, int[] @params);

        /// <summary>
        /// Retrieve information about implementation-dependent support for internal formats
        /// </summary>
        /// <param name="target">Indicates the usage of the internal format. target​ must be GL_TEXTURE_1D​, GL_TEXTURE_1D_ARRAY​, GL_TEXTURE_2D​, GL_TEXTURE_2D_ARRAY​, GL_TEXTURE_3D​, GL_TEXTURE_CUBE_MAP​, GL_TEXTURE_CUBE_MAP_ARRAY​, GL_TEXTURE_RECTANGLE​, GL_TEXTURE_BUFFER​, GL_RENDERBUFFER​, GL_TEXTURE_2D_MULTISAMPLE​ or GL_TEXTURE_2D_MULTISAMPLE_ARRAY​.</param>
        /// <param name="internalformat">Specifies the internal format about which to retrieve information.</param>
        /// <param name="pname">Specifies the type of information to query.</param>
        /// <param name="bufSize">Specifies the maximum number of basic machine units that may be written to params​ by the function.</param>
        /// <param name="parameters">Specifies the address of a variable into which to write the retrieved information.</param>
        public void GetInternalformat(uint target, uint internalformat, uint pname, int bufSize, int[] parameters)
        {
            GetDelegateFor<glGetInternalformativ>()(target, internalformat, pname, bufSize, parameters);
        }

        public const uint GL_NUM_SAMPLE_COUNTS = 0x9380;

        #endregion

        #region ARB_compressed_texture_pixel_storage

        public const uint GL_UNPACK_COMPRESSED_BLOCK_WIDTH = 0x9127;
        public const uint GL_UNPACK_COMPRESSED_BLOCK_HEIGHT = 0x9128;
        public const uint GL_UNPACK_COMPRESSED_BLOCK_DEPTH = 0x9129;
        public const uint GL_UNPACK_COMPRESSED_BLOCK_SIZE = 0x912A;
        public const uint GL_PACK_COMPRESSED_BLOCK_WIDTH = 0x912B;
        public const uint GL_PACK_COMPRESSED_BLOCK_HEIGHT = 0x912C;
        public const uint GL_PACK_COMPRESSED_BLOCK_DEPTH = 0x912D;
        public const uint GL_PACK_COMPRESSED_BLOCK_SIZE = 0x912E;

        #endregion

        #region ARB_shading_language_packing

        //  No new functions or tokens.

        #endregion

        #region ARB_map_buffer_alignment

        public const uint GL_MIN_MAP_BUFFER_ALIGNMENT = 0x90BC;

        #endregion

        #region ARB_conservative_depth

        //  No new functions or tokens.

        #endregion

        #region ARB_texture_compression_bptc

        public const uint GL_COMPRESSED_RGBA_BPTC_UNORM_ARB = 0x8E8C;
        public const uint GL_COMPRESSED_SRGB_ALPHA_BPTC_UNORM_ARB = 0x8E8D;
        public const uint GL_COMPRESSED_RGB_BPTC_SIGNED_FLOAT_ARB = 0x8E8E;
        public const uint GL_COMPRESSED_RGB_BPTC_UNSIGNED_FLOAT_ARB = 0x8E8F;

        #endregion
    }

// ReSharper restore InconsistentNaming
}
