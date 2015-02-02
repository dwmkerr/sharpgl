using System;
using System.Text;

namespace SharpGL
{
// ReSharper disable InconsistentNaming

    public partial class OpenGL
    {
        #region ARB_vertex_array_bgra

        //  No new tokens or functions.

        #endregion

        #region ARB_draw_elements_base_vertex

        private delegate void glDrawElementsBaseVertex(uint mode, int count, uint type,
            IntPtr indices, int basevertex);

        private delegate void glDrawRangeElementsBaseVertex(uint mode, uint start, uint end,
            int count, uint type, IntPtr indices, int basevertex);

        private delegate void glDrawElementsInstancedBaseVertex(
            uint mode, int count, uint type, IntPtr indices, int primcount, int basevertex);

        private delegate void glMultiDrawElementsBaseVertex(uint mode, int[] count, uint type,
            IntPtr[] indices, int primcount, int[] basevertex);

        /// <summary>
        /// Render primitives from array data with a per-element offset.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render. Symbolic constants GL_POINTS, GL_LINE_STRIP, GL_LINE_LOOP, GL_LINES, GL_TRIANGLE_STRIP, GL_TRIANGLE_FAN, GL_TRIANGLES, GL_LINES_ADJACENCY, GL_LINE_STRIP_ADJACENCY, GL_TRIANGLES_ADJACENCY, GL_TRIANGLE_STRIP_ADJACENCY and GL_PATCHES are accepted.</param>
        /// <param name="count">Specifies the number of elements to be rendered.</param>
        /// <param name="type">Specifies the type of the values in indices. Must be one of GL_UNSIGNED_BYTE, GL_UNSIGNED_SHORT, or GL_UNSIGNED_INT.</param>
        /// <param name="indices">Specifies a pointer to the location where the indices are stored.</param>
        /// <param name="basevertex">Specifies a constant that should be added to each element of indices when chosing elements from the enabled vertex arrays.</param>
        public void DrawElementsBaseVertex(uint mode, int count, uint type,
            IntPtr indices, int basevertex)
        {
            GetDelegateFor<glDrawElementsBaseVertex>()(mode, count, type, indices, basevertex);
        }

        /// <summary>
        /// Render primitives from array data with a per-element offset.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render. Symbolic constants GL_POINTS, GL_LINE_STRIP, GL_LINE_LOOP, GL_LINES, GL_TRIANGLE_STRIP, GL_TRIANGLE_FAN, GL_TRIANGLES, GL_LINES_ADJACENCY, GL_LINE_STRIP_ADJACENCY, GL_TRIANGLES_ADJACENCY, GL_TRIANGLE_STRIP_ADJACENCY and GL_PATCHES are accepted.</param>
        /// <param name="start">Specifies the minimum array index contained in indices.</param>
        /// <param name="end">Specifies the maximum array index contained in indices.</param>
        /// <param name="count">Specifies the number of elements to be rendered.</param>
        /// <param name="type">Specifies the type of the values in indices. Must be one of GL_UNSIGNED_BYTE, GL_UNSIGNED_SHORT, or GL_UNSIGNED_INT.</param>
        /// <param name="indices">Specifies a pointer to the location where the indices are stored.</param>
        /// <param name="basevertex">Specifies a constant that should be added to each element of indices when chosing elements from the enabled vertex arrays.</param>
        public void DrawRangeElementsBaseVertex(uint mode, uint start, uint end,
            int count, uint type, IntPtr indices, int basevertex)
        {
            GetDelegateFor<glDrawRangeElementsBaseVertex>()(mode, start, end, count, type, indices, basevertex);
        }

        /// <summary>
        /// Render multiple instances of a set of primitives from array data with a per-element offset.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render. Symbolic constants GL_POINTS, GL_LINE_STRIP, GL_LINE_LOOP, GL_LINES, GL_TRIANGLE_STRIP, GL_TRIANGLE_FAN, GL_TRIANGLES, GL_LINES_ADJACENCY, GL_LINE_STRIP_ADJACENCY, GL_TRIANGLES_ADJACENCY, GL_TRIANGLE_STRIP_ADJACENCY and GL_PATCHES are accepted.</param>
        /// <param name="count">Specifies the number of elements to be rendered.</param>
        /// <param name="type">Specifies the type of the values in indices. Must be one of GL_UNSIGNED_BYTE, GL_UNSIGNED_SHORT, or GL_UNSIGNED_INT.</param>
        /// <param name="indices">Specifies a pointer to the location where the indices are stored.</param>
        /// <param name="primcount">Specifies the number of instances of the indexed geometry that should be drawn.</param>
        /// <param name="basevertex">Specifies a constant that should be added to each element of indices when chosing elements from the enabled vertex arrays.</param>
        public void DrawElementsInstancedBaseVertex(uint mode, int count,
            uint type, IntPtr indices, int primcount, int basevertex)
        {
            GetDelegateFor<glDrawElementsInstancedBaseVertex>()(mode, count, type, indices, primcount, basevertex);
        }

        /// <summary>
        /// Render multiple sets of primitives by specifying indices of array data elements and an index to apply to each index.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render. Symbolic constants GL_POINTS, GL_LINE_STRIP, GL_LINE_LOOP, GL_LINES, GL_LINE_STRIP_ADJACENCY, GL_LINES_ADJACENCY, GL_TRIANGLE_STRIP, GL_TRIANGLE_FAN, GL_TRIANGLES, GL_TRIANGLE_STRIP_ADJACENCY and GL_TRIANGLES_ADJACENCY are accepted.</param>
        /// <param name="count">Points to an array of the elements counts.</param>
        /// <param name="type">Specifies the type of the values in indices. Must be one of GL_UNSIGNED_BYTE, GL_UNSIGNED_SHORT, or GL_UNSIGNED_INT.</param>
        /// <param name="indices">Specifies a pointer to the location where the indices are stored.</param>
        /// <param name="primcount">Specifies the size of the count array.</param>
        /// <param name="basevertex">Specifies a pointer to the location where the base vertices are stored.</param>
        public void MultiDrawElementsBaseVertex(uint mode, int[] count, uint type,
            IntPtr[] indices, int primcount, int[] basevertex)
        {
            GetDelegateFor<glMultiDrawElementsBaseVertex>()(mode, count, type, indices, primcount, basevertex);
        }

        #endregion

        #region ARB_fragment_coord_conventions

        //  No new tokens or functions.

        #endregion

        #region ARB_provoking_vertex

        private delegate void glProvokingVertex(uint mode);

        public const uint GL_FIRST_VERTEX_CONVENTION = 0x8E4D;
        public const uint GL_LAST_VERTEX_CONVENTION = 0x8E4E;
        public const uint GL_PROVOKING_VERTEX = 0x8E4F;
        public const uint GL_QUADS_FOLLOW_PROVOKING_VERTEX_CONVENTION = 0x8E4;

        /// <summary>
        /// Specifiy the vertex to be used as the source of data for flat shaded varyings.
        /// </summary>
        /// <param name="mode">Specifies the vertex to be used as the source of data for flat shaded varyings.</param>
        public void ProvokingVertex(uint mode)
        {
            GetDelegateFor<glProvokingVertex>()(mode);
        }

        #endregion

        #region ARB_seamless_cube_map

        public const uint GL_TEXTURE_CUBE_MAP_SEAMLESS = 0x884F;

        #endregion

        #region ARB_texture_multisample
        
        private delegate void glTexImage2DMultisample(uint target, int samples, uint internalformat, int width, int height, bool fixedsamplelocations);
        private delegate void glTexImage3DMultisample(uint target, int samples, uint internalformat, int width, int height, int depth, bool fixedsamplelocations);
        private delegate void glGetMultisamplefv(uint pname, uint index, float[] val);
        private delegate void glSampleMaski(uint index, uint mask);


        /// <summary>
        /// Establish the data storage, format, dimensions, and number of samples of a multisample texture's image.
        /// </summary>
        /// <param name="target">Specifies the target of the operation. target must be GL_TEXTURE_2D_MULTISAMPLE or GL_PROXY_TEXTURE_2D_MULTISAMPLE.</param>
        /// <param name="samples">The number of samples in the multisample texture's image.</param>
        /// <param name="internalformat">The internal format to be used to store the multisample texture's image. internalformat must specify a color-renderable, depth-renderable, or stencil-renderable format.</param>
        /// <param name="width">The width of the multisample texture's image, in texels.</param>
        /// <param name="height">The height of the multisample texture's image, in texels.</param>
        /// <param name="fixedsamplelocations">Specifies whether the image will use identical sample locations and the same number of samples for all texels in the image, and the sample locations will not depend on the internal format or size of the image.</param>
        public void TexImage2DMultisample(uint target, int samples, uint internalformat, int width, int height,
                                          bool fixedsamplelocations)
        {
            GetDelegateFor<glTexImage2DMultisample>()(target, samples, internalformat, width, height,
                                                      fixedsamplelocations);
        }


        /// <summary>
        /// Establish the data storage, format, dimensions, and number of samples of a multisample texture's image.
        /// </summary>
        /// <param name="target">Specifies the target of the operation. target must be GL_TEXTURE_2D_MULTISAMPLE_ARRAY or GL_PROXY_TEXTURE_2D_MULTISAMPLE_ARRAY.</param>
        /// <param name="samples">The number of samples in the multisample texture's image.</param>
        /// <param name="internalformat">The internal format to be used to store the multisample texture's image. internalformat must specify a color-renderable, depth-renderable, or stencil-renderable format.</param>
        /// <param name="width">The width of the multisample texture's image, in texels.</param>
        /// <param name="height">The height of the multisample texture's image, in texels.</param>
        /// <param name="depth">The depth.</param>
        /// <param name="fixedsamplelocations">Specifies whether the image will use identical sample locations and the same number of samples for all texels in the image, and the sample locations will not depend on the internal format or size of the image.</param>
        public void TexImage3DMultisample(uint target, int samples, uint internalformat, int width, int height, int depth,
                                          bool fixedsamplelocations)
        {
            GetDelegateFor<glTexImage3DMultisample>()(target, samples, internalformat, width, height, depth, 
                                                      fixedsamplelocations);
        }

        /// <summary>
        /// Retrieve the location of a sample.
        /// </summary>
        /// <param name="pname">Specifies the sample parameter name. pname must be GL_SAMPLE_POSITION.</param>
        /// <param name="index">Specifies the index of the sample whose position to query.</param>
        /// <param name="val">Specifies the address of an array to receive the position of the sample.</param>
        public void GetMultisample(uint pname, uint index, float[] val)
        {
            GetDelegateFor<glGetMultisamplefv>()(pname, index, val);
        }

        /// <summary>
        /// Set the value of a sub-word of the sample mask.
        /// </summary>
        /// <param name="index">Specifies which 32-bit sub-word of the sample mask to update.</param>
        /// <param name="mask">Specifies the new value of the mask sub-word.</param>
        public void SampleMask(uint index, uint mask)
        {
            GetDelegateFor<glSampleMaski>()(index, mask);
        }

        #endregion

        #region ARB_depth_clamp

        public const uint GL_DEPTH_CLAMP = 0x864F;

        #endregion

        #region ARB_sync

        private delegate IntPtr glFenceSync(uint condition, uint flags);

        private delegate bool glIsSync(IntPtr sync);

        private delegate void glDeleteSync(IntPtr sync);

        private delegate uint glClientWaitSync(IntPtr sync, uint flags, UInt64 timeout);

        private delegate void glWaitSync(IntPtr sync, uint flags, UInt64 timeout);

        private delegate void glGetInteger64v(uint pname, Int64[] @params);

        private delegate void glGetSynciv(IntPtr sync, uint pname, int bufSize, out int length,
                                        int[] values);

        /// <summary>
        /// Create a new sync object and insert it into the GL command stream.
        /// </summary>
        /// <param name="condition">Specifies the condition that must be met to set the sync object's state to signaled. condition must be GL_SYNC_GPU_COMMANDS_COMPLETE.</param>
        /// <param name="flags">Specifies a bitwise combination of flags controlling the behavior of the sync object. No flags are presently defined for this operation and flags must be zero.</param>
        /// <returns>The sync object.</returns>
        public IntPtr FenceSync(uint condition, uint flags)
        {
            return GetDelegateFor<glFenceSync>()(condition, flags); 
        }

        /// <summary>
        /// Determine if a name corresponds to a sync object.
        /// </summary>
        /// <param name="sync">Specifies a value that may be the name of a sync object.</param>
        /// <returns>glIsSync returns GL_TRUE if sync is currently the name of a sync object. If sync is not the name of a sync object, or if an error occurs, glIsSync returns GL_FALSE. Note that zero is not the name of a sync object.</returns>
        public bool IsSync(IntPtr sync)
        {
            return GetDelegateFor<glIsSync>()(sync);
        }

        /// <summary>
        /// Delete a sync object.
        /// </summary>
        /// <param name="sync">The sync object to be deleted.</param>
        public void DeleteSync(IntPtr sync)
        {
            GetDelegateFor<glDeleteSync>()(sync);
        }

        /// <summary>
        /// Block and wait for a sync object to become signaled.
        /// </summary>
        /// <param name="sync">The sync object whose status to wait on.</param>
        /// <param name="flags">A bitfield controlling the command flushing behavior. flags may be GL_SYNC_FLUSH_COMMANDS_BIT.</param>
        /// <param name="timeout">The timeout, specified in nanoseconds, for which the implementation should wait for sync to become signaled.</param>
        /// <returns>The return value is one of four status values:
        /// GL_ALREADY_SIGNALED indicates that sync was signaled at the time that glClientWaitSync was called.
        /// GL_TIMEOUT_EXPIRED indicates that at least timeout nanoseconds passed and sync did not become signaled.
        /// GL_CONDITION_SATISFIED indicates that sync was signaled before the timeout expired.
        /// GL_WAIT_FAILED indicates that an error occurred. Additionally, an OpenGL error will be generated.</returns>
        public uint ClientWaitSync(IntPtr sync, uint flags, UInt64 timeout)
        {
            return GetDelegateFor<glClientWaitSync>()(sync, flags, timeout);
        }

        /// <summary>
        /// Instruct the GL server to block until the specified sync object becomes signaled.
        /// </summary>
        /// <param name="sync">Specifies the sync object whose status to wait on.</param>
        /// <param name="flags">A bitfield controlling the command flushing behavior. flags may be zero.</param>
        /// <param name="timeout">Specifies the timeout that the server should wait before continuing. timeout must be GL_TIMEOUT_IGNORED.</param>
        public void WaitSync(IntPtr sync, uint flags, UInt64 timeout)
        {
            GetDelegateFor<glWaitSync>()(sync, flags, timeout);
        }

        /// <summary>
        /// Return the value or values of a selected parameter.
        /// </summary>
        /// <param name="pname">Specifies the parameter value to be returned for non-indexed versions of glGet. The symbolic constants in the list below are accepted.</param>
        /// <param name="params">Returns the value or values of the specified parameter.</param>
        public void GetInteger(uint pname, Int64[] @params)
        {
            GetDelegateFor<glGetInteger64v>()(pname, @params);
        }

        /// <summary>
        /// Query the properties of a sync object.
        /// </summary>
        /// <param name="sync">Specifies the sync object whose properties to query.</param>
        /// <param name="pname">Specifies the parameter whose value to retrieve from the sync object specified in sync.</param>
        /// <param name="bufSize">Specifies the size of the buffer whose address is given in values.</param>
        /// <param name="length">Specifies the address of an variable to receive the number of integers placed in values.</param>
        /// <param name="values">Specifies the address of an array to receive the values of the queried parameter.</param>
        public void GetSync(IntPtr sync, uint pname, int bufSize, out int length, int[] values)
        {
            GetDelegateFor<glGetSynciv>()(sync, pname, bufSize, out length, values);
        }

        #endregion
    }

// ReSharper restore InconsistentNaming
}
