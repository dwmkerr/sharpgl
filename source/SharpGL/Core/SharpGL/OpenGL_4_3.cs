using System;
using System.Text;

namespace SharpGL
{
// ReSharper disable InconsistentNaming

    public partial class OpenGL
    {
        #region KHR_Debug

        //  See https://www.opengl.org/registry/specs/KHR/debug.txt

        private delegate void glDebugMessageControl(uint source, uint type, uint severity, int count, uint[] ids, bool enabled);
        private delegate void glDebugMessageInsert(uint source, uint type, uint id, uint severity, int length, string buf);
        private delegate void glDebugMessageCallback(GLDEBUGPROC callback, IntPtr userParam);
        private delegate uint glGetDebugMessageLog(uint count, int bufSize, uint[] sources, uint[] types, uint[] ids, uint[] severities, uint[] lengths, IntPtr messageLog);
        private delegate void glPushDebugGroup(uint source, uint id, int length, string message);
        private delegate void glPopDebugGroup();
        private delegate void glObjectLabel(uint identifier, uint name, int length, string label);
        private delegate void glGetObjectLabel(uint identifier, uint name, int bufSize, out int length, StringBuilder label);
        private delegate void glObjectPtrLabel(IntPtr ptr, int length, string label);
        private delegate void glGetObjectPtrLabel(IntPtr ptr, int bufSize, out int length, StringBuilder label);
        
        public delegate void GLDEBUGPROC (uint source, uint type, uint id, uint severity, int length, string message, IntPtr userParam);

        public const uint GL_DEBUG_OUTPUT                                      = 0x92E0;
        public const uint GL_DEBUG_OUTPUT_SYNCHRONOUS                          = 0x8242;
        public const uint GL_CONTEXT_FLAG_DEBUG_BIT                            = 0x00000002;
        public const uint GL_MAX_DEBUG_MESSAGE_LENGTH                          = 0x9143;
        public const uint GL_MAX_DEBUG_LOGGED_MESSAGES                         = 0x9144;
        public const uint GL_DEBUG_LOGGED_MESSAGES                             = 0x9145;
        public const uint GL_DEBUG_NEXT_LOGGED_MESSAGE_LENGTH                  = 0x8243;
        public const uint GL_MAX_DEBUG_GROUP_STACK_DEPTH                       = 0x826C;  
        public const uint GL_DEBUG_GROUP_STACK_DEPTH                           = 0x826D; 
        public const uint GL_MAX_LABEL_LENGTH                                  = 0x82E8;
        public const uint GL_DEBUG_CALLBACK_FUNCTION                           = 0x8244;
        public const uint GL_DEBUG_CALLBACK_USER_PARAM                         = 0x8245;
        public const uint GL_DEBUG_SOURCE_API                                  = 0x8246;
        public const uint GL_DEBUG_SOURCE_WINDOW_SYSTEM                        = 0x8247;
        public const uint GL_DEBUG_SOURCE_SHADER_COMPILER                      = 0x8248;
        public const uint GL_DEBUG_SOURCE_THIRD_PARTY                          = 0x8249;
        public const uint GL_DEBUG_SOURCE_APPLICATION                          = 0x824A;
        public const uint GL_DEBUG_SOURCE_OTHER                                = 0x824B;
        public const uint GL_DEBUG_TYPE_ERROR                                  = 0x824C;
        public const uint GL_DEBUG_TYPE_DEPRECATED_BEHAVIOR                    = 0x824D;
        public const uint GL_DEBUG_TYPE_UNDEFINED_BEHAVIOR                     = 0x824E;
        public const uint GL_DEBUG_TYPE_PORTABILITY                            = 0x824F;
        public const uint GL_DEBUG_TYPE_PERFORMANCE                            = 0x8250;
        public const uint GL_DEBUG_TYPE_OTHER                                  = 0x8251;
        public const uint GL_DEBUG_TYPE_MARKER                                 = 0x8268;
        public const uint GL_DEBUG_TYPE_PUSH_GROUP                             = 0x8269;  
        public const uint GL_DEBUG_TYPE_POP_GROUP                              = 0x826A; 
        public const uint GL_DEBUG_SEVERITY_HIGH                               = 0x9146;
        public const uint GL_DEBUG_SEVERITY_MEDIUM                             = 0x9147;
        public const uint GL_DEBUG_SEVERITY_LOW                                = 0x9148;
        public const uint GL_DEBUG_SEVERITY_NOTIFICATION                       = 0x826B;
        public const uint GL_BUFFER                                            = 0x82E0;
        public const uint GL_SHADER                                            = 0x82E1;
        public const uint GL_PROGRAM                                           = 0x82E2;
        public const uint GL_QUERY                                             = 0x82E3;
        public const uint GL_PROGRAM_PIPELINE                                  = 0x82E4;
        public const uint GL_SAMPLER                                           = 0x82E6;
        public const uint GL_DISPLAY_LIST                                      = 0x82E7;

        /// <summary>
        /// Control the reporting of debug messages in a debug context
        /// </summary>
        /// <param name="source">The source of debug messages to enable or disable.</param>
        /// <param name="type">The type of debug messages to enable or disable.</param>
        /// <param name="severity">The severity of debug messages to enable or disable.</param>
        /// <param name="count">The length of the array <paramref name="ids"/>.</param>
        /// <param name="ids">The address of an array of unsigned integers contianing the ids of the messages to enable or disable.</param>
        /// <param name="enabled">A Boolean flag determining whether the selected messages should be enabled or disabled.</param>
        public void DebugMessageControl(uint source, uint type, uint severity, int count, uint[] ids, bool enabled)
        {
            GetDelegateFor<glDebugMessageControl>()(source, type, severity, count, ids, enabled);
        }

        /// <summary>
        /// Inject an application-supplied message into the debug message queue.
        /// </summary>
        /// <param name="source">The source of the debug message to insert.</param>
        /// <param name="type">The type of the debug message insert.</param>
        /// <param name="id">The user-supplied identifier of the message to insert.</param>
        /// <param name="severity">The severity of the debug messages to insert.</param>
        /// <param name="length">The length string contained in the character array whose address is given by <paramref name="message"/>.</param>
        /// <param name="message">The address of a character array containing the message to insert.</param>
        public void DebugMessageInsert(uint source, uint type, uint id, uint severity, int length, string message)
        {
            GetDelegateFor<glDebugMessageInsert>()(source, type, id, severity, length, message);
        }

        /// <summary>
        /// Specify a callback to receive debugging messages from the GL.
        /// </summary>
        /// <param name="callback">The address of a callback function that will be called when a debug message is generated.</param>
        /// <param name="userParam">A user supplied pointer that will be passed on each invocation of callback.</param>
        public void DebugMessageCallback(GLDEBUGPROC callback, IntPtr userParam)
        {
            GetDelegateFor<glDebugMessageCallback>()(callback, userParam);
        }

        /// <summary>
        /// Retrieve messages from the debug message log.
        /// </summary>
        /// <param name="count">The number of debug messages to retrieve from the log.</param>
        /// <param name="bufSize">The size of the buffer whose address is given by <paramref name="messageLog"/>.</param>
        /// <param name="sources">The address of an array of variables to receive the sources of the retrieved messages.</param>
        /// <param name="types">The address of an array of variables to receive the types of the retrieved messages.</param>
        /// <param name="ids">The address of an array of unsigned integers to receive the ids of the retrieved messages.</param>
        /// <param name="severities">The address of an array of variables to receive the severites of the retrieved messages.</param>
        /// <param name="lengths">The address of an array of variables to receive the lengths of the received messages.</param>
        /// <param name="messageLog">The address of an array of characters that will receive the messages.</param>
        /// <returns>The number of messages received.</returns>
        public uint GetDebugMessageLog(uint count, int bufSize, uint[] sources, uint[] types, uint[] ids,
            uint[] severities, uint[] lengths, IntPtr messageLog)
        {
            return GetDelegateFor<glGetDebugMessageLog>()(count, bufSize, sources, types, ids, severities, lengths, messageLog);
        }

        /// <summary>
        /// Push a named debug group into the command stream.
        /// </summary>
        /// <param name="source">The source of the debug message.</param>
        /// <param name="id">The identifier of the message.</param>
        /// <param name="length">The length of the message to be sent to the debug output stream.</param>
        /// <param name="message">The a string containing the message to be sent to the debug output stream.</param>
        public void PushDebugGroup(uint source, uint id, int length, string message)
        {
            GetDelegateFor<glPushDebugGroup>()(source, id, length, message);
        }

        /// <summary>
        /// Pop the active debug group.
        /// </summary>
        public void PopDebugGroup()
        {
            GetDelegateFor<glPopDebugGroup>()();
        }

        /// <summary>
        /// Label a named object identified within a namespace.
        /// </summary>
        /// <param name="identifier">The namespace from which the name of the object is allocated.</param>
        /// <param name="name">The namespace from which the name of the object is allocated.</param>
        /// <param name="label">The address of a string containing the label to assign to the object.</param>
        public void ObjectLabel(uint identifier, uint name, string label)
        {
            GetDelegateFor<glObjectLabel>()(identifier, name, label.Length, label);
        }

        /// <summary>
        /// Retrieve the label of a named object identified within a namespace
        /// </summary>
        /// <param name="identifier">The namespace from which the name of the object is allocated.</param>
        /// <param name="name">The name of the object whose label to retrieve.</param>
        /// <param name="bufSize">The length of the buffer whose address is in <paramref name="label"/>.</param>
        /// <param name="length">The address of a variable to receive the length of the object label.</param>
        /// <param name="label">The address of a string that will receive the object label.</param>
        public void GetObjectLabel(uint identifier, uint name, int bufSize, out int length, out string label)
        {
            var builder = new StringBuilder(bufSize);
            GetDelegateFor<glGetObjectLabel>()(identifier, name, bufSize, out length, builder);
            label = builder.ToString();
        }

        /// <summary>
        /// Label a a sync object identified by a pointer
        /// </summary>
        /// <param name="ptr">A pointer identifying a sync object.</param>
        /// <param name="label">The address of a string containing the label to assign to the object.</param>
        public void ObjectPtrLabel(IntPtr ptr, string label)
        {
            GetDelegateFor<glObjectPtrLabel>()(ptr, label.Length, label);
        }

        /// <summary>
        /// Retrieve the label of a sync object identified by a pointer.
        /// </summary>
        /// <param name="ptr">The name of the sync object whose label to retrieve.</param>
        /// <param name="bufSize">SThe length of the buffer whose address is in <paramref name="label"/>.</param>
        /// <param name="length">The address of a variable to receive the length of the object label.</param>
        /// <param name="label">The address of a string that will receive the object label.</param>
        public void GetObjectPtrLabel(IntPtr ptr, int bufSize, out int length, out string label)
        {
            var builder = new StringBuilder(bufSize);
            GetDelegateFor<glGetObjectPtrLabel>()(ptr, bufSize, out length, builder);
            label = builder.ToString();
        }

        #endregion

        #region ARB_clear_buffer_object

        //  See: https://www.opengl.org/registry/specs/ARB/clear_buffer_object.txt
        
        private delegate void glClearBufferData(uint target, uint internalformat, uint format, uint type, IntPtr data);
        private delegate void glClearBufferSubData(uint target, uint internalformat, IntPtr offset, uint size, uint format, uint type, IntPtr data);
        private delegate void glClearNamedBufferDataEXT(uint buffer, uint internalformat, uint format, uint type, IntPtr data);
        private delegate void glClearNamedBufferSubDataEXT(uint buffer, uint internalformat, IntPtr offset, uint size, uint format, uint type, IntPtr data);

        /// <summary>
        /// Fill a buffer object's data store with a fixed value
        /// </summary>
        /// <param name="target">Specifies the target buffer object. The symbolic constant must be GL_ARRAY_BUFFER​, GL_ATOMIC_COUNTER_BUFFER​, GL_COPY_READ_BUFFER​, GL_COPY_WRITE_BUFFER​, GL_DRAW_INDIRECT_BUFFER​, GL_DISPATCH_INDIRECT_BUFFER​, GL_ELEMENT_ARRAY_BUFFER​, GL_PIXEL_PACK_BUFFER​, GL_PIXEL_UNPACK_BUFFER​, GL_QUERY_BUFFER​, GL_SHADER_STORAGE_BUFFER​, GL_TEXTURE_BUFFER​, GL_TRANSFORM_FEEDBACK_BUFFER​, or GL_UNIFORM_BUFFER​.</param>
        /// <param name="internalformat">The sized internal format with which the data will be stored in the buffer object.</param>
        /// <param name="format">Specifies the format of the pixel data. For transfers of depth, stencil, or depth/stencil data, you must use GL_DEPTH_COMPONENT​, GL_STENCIL_INDEX​, or GL_DEPTH_STENCIL​, where appropriate. For transfers of normalized integer or floating-point color image data, you must use one of the following: GL_RED​, GL_GREEN​, GL_BLUE​, GL_RG​, GL_RGB​, GL_BGR​, GL_RGBA​, and GL_BGRA​. For transfers of non-normalized integer data, you must use one of the following: GL_RED_INTEGER​, GL_GREEN_INTEGER​, GL_BLUE_INTEGER​, GL_RG_INTEGER​, GL_RGB_INTEGER​, GL_BGR_INTEGER​, GL_RGBA_INTEGER​, and GL_BGRA_INTEGER​.</param>
        /// <param name="type">Specifies the data type of the pixel data. The following symbolic values are accepted: GL_UNSIGNED_BYTE​, GL_BYTE​, GL_UNSIGNED_SHORT​, GL_SHORT​, GL_UNSIGNED_INT​, GL_INT​, GL_FLOAT​, GL_UNSIGNED_BYTE_3_3_2​, GL_UNSIGNED_BYTE_2_3_3_REV​, GL_UNSIGNED_SHORT_5_6_5​, GL_UNSIGNED_SHORT_5_6_5_REV​, GL_UNSIGNED_SHORT_4_4_4_4​, GL_UNSIGNED_SHORT_4_4_4_4_REV​, GL_UNSIGNED_SHORT_5_5_5_1​, GL_UNSIGNED_SHORT_1_5_5_5_REV​, GL_UNSIGNED_INT_8_8_8_8​, GL_UNSIGNED_INT_8_8_8_8_REV​, GL_UNSIGNED_INT_10_10_10_2​, and GL_UNSIGNED_INT_2_10_10_10_REV​.</param>
        /// <param name="data">Specifies a pointer to a single pixel of data to upload. This parameter may not be NULL.</param>
        public void ClearBufferData(uint target, uint internalformat, uint format, uint type, IntPtr data)
        {
            GetDelegateFor<glClearBufferData>()(target, internalformat, format, type, data);
        }

        /// <summary>
        /// Fill all or part of buffer object's data store with a fixed value
        /// </summary>
        /// <param name="target">Specifies the target buffer object. The symbolic constant must be GL_ARRAY_BUFFER​, GL_ATOMIC_COUNTER_BUFFER​, GL_COPY_READ_BUFFER​, GL_COPY_WRITE_BUFFER​, GL_DRAW_INDIRECT_BUFFER​, GL_DISPATCH_INDIRECT_BUFFER​, GL_ELEMENT_ARRAY_BUFFER​, GL_PIXEL_PACK_BUFFER​, GL_PIXEL_UNPACK_BUFFER​, GL_QUERY_BUFFER​, GL_SHADER_STORAGE_BUFFER​, GL_TEXTURE_BUFFER​, GL_TRANSFORM_FEEDBACK_BUFFER​, or GL_UNIFORM_BUFFER​.</param>
        /// <param name="internalformat">The sized internal format with which the data will be stored in the buffer object.</param>
        /// <param name="offset">The offset, in basic machine units into the buffer object's data store at which to start filling.</param>
        /// <param name="size">The size, in basic machine units of the range of the data store to fill.</param>
        /// <param name="format">Specifies the format of the pixel data. For transfers of depth, stencil, or depth/stencil data, you must use GL_DEPTH_COMPONENT​, GL_STENCIL_INDEX​, or GL_DEPTH_STENCIL​, where appropriate. For transfers of normalized integer or floating-point color image data, you must use one of the following: GL_RED​, GL_GREEN​, GL_BLUE​, GL_RG​, GL_RGB​, GL_BGR​, GL_RGBA​, and GL_BGRA​. For transfers of non-normalized integer data, you must use one of the following: GL_RED_INTEGER​, GL_GREEN_INTEGER​, GL_BLUE_INTEGER​, GL_RG_INTEGER​, GL_RGB_INTEGER​, GL_BGR_INTEGER​, GL_RGBA_INTEGER​, and GL_BGRA_INTEGER​.</param>
        /// <param name="type">Specifies the data type of the pixel data. The following symbolic values are accepted: GL_UNSIGNED_BYTE​, GL_BYTE​, GL_UNSIGNED_SHORT​, GL_SHORT​, GL_UNSIGNED_INT​, GL_INT​, GL_FLOAT​, GL_UNSIGNED_BYTE_3_3_2​, GL_UNSIGNED_BYTE_2_3_3_REV​, GL_UNSIGNED_SHORT_5_6_5​, GL_UNSIGNED_SHORT_5_6_5_REV​, GL_UNSIGNED_SHORT_4_4_4_4​, GL_UNSIGNED_SHORT_4_4_4_4_REV​, GL_UNSIGNED_SHORT_5_5_5_1​, GL_UNSIGNED_SHORT_1_5_5_5_REV​, GL_UNSIGNED_INT_8_8_8_8​, GL_UNSIGNED_INT_8_8_8_8_REV​, GL_UNSIGNED_INT_10_10_10_2​, and GL_UNSIGNED_INT_2_10_10_10_REV​.</param>
        /// <param name="data">Specifies a pointer to a single pixel of data to upload. This parameter may not be NULL.</param>
        public void ClearBufferSubData(uint target, uint internalformat, IntPtr offset, uint size, uint format, uint type, IntPtr data)
        {
            GetDelegateFor<glClearBufferSubData>()(target, internalformat, offset, size, format, type, data);
        }

        public void ClearNamedBufferDataEXT(uint buffer, uint internalformat, uint format, uint type, IntPtr data)
        {
            GetDelegateFor<glClearNamedBufferDataEXT>()(buffer, internalformat, format, type, data);
        }
        public void ClearNamedBufferSubDataEXT(uint buffer, uint internalformat, IntPtr offset, uint size, uint format, uint type, IntPtr data)
        {
            GetDelegateFor<glClearNamedBufferSubDataEXT>()(buffer, internalformat, offset, size, format, type, data);
        }

        #endregion

        #region GL_ARB_compute_shader

        /// <summary>
        /// Launch one or more compute work groups
        /// </summary>
        /// <param name="num_groups_x">The number of work groups to be launched in the X dimension.</param>
        /// <param name="num_groups_y">The number of work groups to be launched in the Y dimension.</param>
        /// <param name="num_groups_z">The number of work groups to be launched in the Z dimension.</param>
        public void DispatchCompute(uint num_groups_x, uint num_groups_y, uint num_groups_z)
        {
            GetDelegateFor<glDispatchCompute>()(num_groups_x, num_groups_y, num_groups_z);
        }

        /// <summary>
        /// Launch one or more compute work groups using parameters stored in a buffer
        /// </summary>
        /// <param name="indirect">The offset into the buffer object currently bound to the GL_DISPATCH_INDIRECT_BUFFER​ buffer target at which the dispatch parameters are stored.</param>
        public void DispatchComputeIndirect(IntPtr indirect)
        {
            GetDelegateFor<glDispatchComputeIndirect>()(indirect);
        }

        //  Delegates
        private delegate void glDispatchCompute(uint num_groups_x, uint num_groups_y, uint num_groups_z);
        private delegate void glDispatchComputeIndirect(IntPtr indirect);

        // Constants
        public const uint GL_COMPUTE_SHADER = 0x91B9;
        public const uint GL_MAX_COMPUTE_UNIFORM_BLOCKS = 0x91BB;
        public const uint GL_MAX_COMPUTE_TEXTURE_IMAGE_UNITS = 0x91BC;
        public const uint GL_MAX_COMPUTE_IMAGE_UNIFORMS = 0x91BD;
        public const uint GL_MAX_COMPUTE_SHARED_MEMORY_SIZE = 0x8262;
        public const uint GL_MAX_COMPUTE_UNIFORM_COMPONENTS = 0x8263;
        public const uint GL_MAX_COMPUTE_ATOMIC_COUNTER_BUFFERS = 0x8264;
        public const uint GL_MAX_COMPUTE_ATOMIC_COUNTERS = 0x8265;
        public const uint GL_MAX_COMBINED_COMPUTE_UNIFORM_COMPONENTS = 0x8266;
        public const uint GL_MAX_COMPUTE_WORK_GROUP_INVOCATIONS = 0x90EB;
        public const uint GL_MAX_COMPUTE_WORK_GROUP_COUNT = 0x91BE;
        public const uint GL_MAX_COMPUTE_WORK_GROUP_SIZE = 0x91BF;
        public const uint GL_COMPUTE_WORK_GROUP_SIZE = 0x8267;
        public const uint GL_UNIFORM_BLOCK_REFERENCED_BY_COMPUTE_SHADER = 0x90EC;
        public const uint GL_ATOMIC_COUNTER_BUFFER_REFERENCED_BY_COMPUTE_SHADER = 0x90ED;
        public const uint GL_DISPATCH_INDIRECT_BUFFER = 0x90EE;
        public const uint GL_DISPATCH_INDIRECT_BUFFER_BINDING = 0x90EF;
        public const uint GL_COMPUTE_SHADER_BIT = 0x00000020;

        #endregion

        #region GL_ARB_copy_image

        /// <summary>
        /// Perform a raw data copy between two images
        /// </summary>
        /// <param name="srcName">The name of a texture or renderbuffer object from which to copy.</param>
        /// <param name="srcTarget">The target representing the namespace of the source name srcName​.</param>
        /// <param name="srcLevel">The mipmap level to read from the source.</param>
        /// <param name="srcX">The X coordinate of the left edge of the souce region to copy.</param>
        /// <param name="srcY">The Y coordinate of the top edge of the souce region to copy.</param>
        /// <param name="srcZ">The Z coordinate of the near edge of the souce region to copy.</param>
        /// <param name="dstName">The name of a texture or renderbuffer object to which to copy.</param>
        /// <param name="dstTarget">The target representing the namespace of the destination name dstName​.</param>
        /// <param name="dstLevel">The desination mipmap level.</param>
        /// <param name="dstX">The X coordinate of the left edge of the destination region.</param>
        /// <param name="dstY">The Y coordinate of the top edge of the destination region.</param>
        /// <param name="dstZ">The Z coordinate of the near edge of the destination region.</param>
        /// <param name="srcWidth">The width of the region to be copied.</param>
        /// <param name="srcHeight">The height of the region to be copied.</param>
        /// <param name="srcDepth">The depth of the region to be copied.</param>
        public void CopyImageSubData(uint srcName, uint srcTarget, int srcLevel, int srcX, int srcY, int srcZ, uint dstName,
            uint dstTarget, int dstLevel, int dstX, int dstY, int dstZ, uint srcWidth, uint srcHeight, uint srcDepth)
        {
            GetDelegateFor<glCopyImageSubData>()(srcName, srcTarget, srcLevel, srcX, srcY, srcZ, dstName,
            dstTarget, dstLevel, dstX, dstY, dstZ, srcWidth, srcHeight, srcDepth);
        }

        //  Delegates
        private delegate void glCopyImageSubData(uint srcName, uint srcTarget, int srcLevel, int srcX, int srcY, int srcZ, uint dstName,
            uint dstTarget, int dstLevel, int dstX, int dstY, int dstZ, uint srcWidth, uint srcHeight, uint srcDepth);

        #endregion

        #region GL_ARB_ES3_compatibility

        public const uint GL_COMPRESSED_RGB8_ETC2 = 0x9274;
        public const uint GL_COMPRESSED_SRGB8_ETC2 = 0x9275;
        public const uint GL_COMPRESSED_RGB8_PUNCHTHROUGH_ALPHA1_ETC2 = 0x9276;
        public const uint GL_COMPRESSED_SRGB8_PUNCHTHROUGH_ALPHA1_ETC2 = 0x9277;
        public const uint GL_COMPRESSED_RGBA8_ETC2_EAC = 0x9278;
        public const uint GL_COMPRESSED_SRGB8_ALPHA8_ETC2_EAC = 0x9279;
        public const uint GL_COMPRESSED_R11_EAC = 0x9270;
        public const uint GL_COMPRESSED_SIGNED_R11_EAC = 0x9271;
        public const uint GL_COMPRESSED_RG11_EAC = 0x9272;
        public const uint GL_COMPRESSED_SIGNED_RG11_EAC = 0x9273;
        public const uint GL_PRIMITIVE_RESTART_FIXED_INDEX = 0x8D69;
        public const uint GL_ANY_SAMPLES_PASSED_CONSERVATIVE = 0x8D6A;
        public const uint GL_MAX_ELEMENT_INDEX = 0x8D6B;
        public const uint GL_TEXTURE_IMMUTABLE_LEVELS = 0x82DF;

        #endregion

        #region GL_ARB_explicit_uniform_location

        //  Constants

        /// <summary>
        /// The number of available pre-assigned uniform locations to that can default be 
        /// allocated in the default uniform block.
        /// </summary>
        public const int GL_MAX_UNIFORM_LOCATIONS = 0x826E;

        #endregion

        #region GL_ARB_framebuffer_no_attachments

        //  Methods

        /// <summary>
        /// Set a named parameter of a framebuffer.
        /// </summary>
        /// <param name="target">The target of the operation, which must be GL_READ_FRAMEBUFFER​, GL_DRAW_FRAMEBUFFER​ or GL_FRAMEBUFFER​.</param>
        /// <param name="pname">A token indicating the parameter to be modified.</param>
        /// <param name="param">The new value for the parameter named pname​.</param>
        public void FramebufferParameter(uint target, uint pname, int param)
        {
            GetDelegateFor<glFramebufferParameteri>()(target, pname, param);
        }

        /// <summary>
        /// Retrieve a named parameter from a framebuffer
        /// </summary>
        /// <param name="target">The target of the operation, which must be GL_READ_FRAMEBUFFER​, GL_DRAW_FRAMEBUFFER​ or GL_FRAMEBUFFER​.</param>
        /// <param name="pname">A token indicating the parameter to be retrieved.</param>
        /// <param name="parameters">The address of a variable to receive the value of the parameter named pname​.</param>
        public void GetFramebufferParameter(uint target, uint pname, int[] parameters)
        {
            GetDelegateFor<glGetFramebufferParameteriv>()(target, pname, parameters);
        }

        public void NamedFramebufferParameterEXT(uint framebuffer, uint pname, int param)
        {
            GetDelegateFor<glNamedFramebufferParameteriEXT>()(framebuffer, pname, param);
        }

        public void GetNamedFramebufferParameterEXT(uint framebuffer, uint pname, int[] parameters)
        {
            GetDelegateFor<glGetNamedFramebufferParameterivEXT>()(framebuffer, pname, parameters);
        }

        //  Delegates
        private delegate void glFramebufferParameteri(uint target, uint pname, int param);
        private delegate void glGetFramebufferParameteriv(uint target, uint pname, int[] parameters);
        private delegate void glNamedFramebufferParameteriEXT(uint framebuffer, uint pname, int param);
        private delegate void glGetNamedFramebufferParameterivEXT(uint framebuffer, uint pname, int[] parameters);

        #endregion

        #region GL_ARB_internalformat_query2

        /// <summary>
        /// Retrieve information about implementation-dependent support for internal formats
        /// </summary>
        /// <param name="target">Indicates the usage of the internal format. target​ must be GL_TEXTURE_1D​, GL_TEXTURE_1D_ARRAY​, GL_TEXTURE_2D​, GL_TEXTURE_2D_ARRAY​, GL_TEXTURE_3D​, GL_TEXTURE_CUBE_MAP​, GL_TEXTURE_CUBE_MAP_ARRAY​, GL_TEXTURE_RECTANGLE​, GL_TEXTURE_BUFFER​, GL_RENDERBUFFER​, GL_TEXTURE_2D_MULTISAMPLE​ or GL_TEXTURE_2D_MULTISAMPLE_ARRAY​.</param>
        /// <param name="internalformat">Specifies the internal format about which to retrieve information.</param>
        /// <param name="pname">Specifies the type of information to query.</param>
        /// <param name="bufSize">Specifies the maximum number of basic machine units that may be written to params​ by the function.</param>
        /// <param name="parameters">Specifies the address of a variable into which to write the retrieved information.</param>
        public void GetInternalformat(uint target, uint internalformat, uint pname, uint bufSize, int[] parameters)
        {
            GetDelegateFor<glGetInternalformativ>()(target, internalformat, pname, bufSize, parameters);
        }

        /// <summary>
        /// Retrieve information about implementation-dependent support for internal formats
        /// </summary>
        /// <param name="target">Indicates the usage of the internal format. target​ must be GL_TEXTURE_1D​, GL_TEXTURE_1D_ARRAY​, GL_TEXTURE_2D​, GL_TEXTURE_2D_ARRAY​, GL_TEXTURE_3D​, GL_TEXTURE_CUBE_MAP​, GL_TEXTURE_CUBE_MAP_ARRAY​, GL_TEXTURE_RECTANGLE​, GL_TEXTURE_BUFFER​, GL_RENDERBUFFER​, GL_TEXTURE_2D_MULTISAMPLE​ or GL_TEXTURE_2D_MULTISAMPLE_ARRAY​.</param>
        /// <param name="internalformat">Specifies the internal format about which to retrieve information.</param>
        /// <param name="pname">Specifies the type of information to query.</param>
        /// <param name="bufSize">Specifies the maximum number of basic machine units that may be written to params​ by the function.</param>
        /// <param name="parameters">Specifies the address of a variable into which to write the retrieved information.</param>
        public void GetInternalformat(uint target, uint internalformat, uint pname, uint bufSize, Int64[] parameters)
        {
            GetDelegateFor<glGetInternalformati64v>()(target, internalformat, pname, bufSize, parameters);
        }

        //  Delegates
        private delegate void glGetInternalformativ(uint target, uint internalformat, uint pname, uint bufSize, int[] parameters);
        private delegate void glGetInternalformati64v(uint target, uint internalformat, uint pname, uint bufSize, Int64[] parameters);

        //  Constants
        public const uint GL_RENDERBUFFER = 0x8D41;
        public const uint GL_TEXTURE_2D_MULTISAMPLE = 0x9100;
        public const uint GL_TEXTURE_2D_MULTISAMPLE_ARRAY = 0x9102;
        public const uint GL_NUM_SAMPLE_COUNTS = 0x9380;
        public const uint GL_INTERNALFORMAT_SUPPORTED = 0x826F;
        public const uint GL_INTERNALFORMAT_PREFERRED = 0x8270;
        public const uint GL_INTERNALFORMAT_RED_SIZE = 0x8271;
        public const uint GL_INTERNALFORMAT_GREEN_SIZE = 0x8272;
        public const uint GL_INTERNALFORMAT_BLUE_SIZE = 0x8273;
        public const uint GL_INTERNALFORMAT_ALPHA_SIZE = 0x8274;
        public const uint GL_INTERNALFORMAT_DEPTH_SIZE = 0x8275;
        public const uint GL_INTERNALFORMAT_STENCIL_SIZE = 0x8276;
        public const uint GL_INTERNALFORMAT_SHARED_SIZE = 0x8277;
        public const uint GL_INTERNALFORMAT_RED_TYPE = 0x8278;
        public const uint GL_INTERNALFORMAT_GREEN_TYPE = 0x8279;
        public const uint GL_INTERNALFORMAT_BLUE_TYPE = 0x827A;
        public const uint GL_INTERNALFORMAT_ALPHA_TYPE = 0x827B;
        public const uint GL_INTERNALFORMAT_DEPTH_TYPE = 0x827C;
        public const uint GL_INTERNALFORMAT_STENCIL_TYPE = 0x827D;
        public const uint GL_MAX_WIDTH = 0x827E;
        public const uint GL_MAX_HEIGHT = 0x827F;
        public const uint GL_MAX_DEPTH = 0x8280;
        public const uint GL_MAX_LAYERS = 0x8281;
        public const uint GL_MAX_COMBINED_DIMENSIONS = 0x8282;
        public const uint GL_COLOR_COMPONENTS = 0x8283;
        public const uint GL_DEPTH_COMPONENTS = 0x8284;
        public const uint GL_STENCIL_COMPONENTS = 0x8285;
        public const uint GL_COLOR_RENDERABLE = 0x8286;
        public const uint GL_DEPTH_RENDERABLE = 0x8287;
        public const uint GL_STENCIL_RENDERABLE = 0x8288;
        public const uint GL_FRAMEBUFFER_RENDERABLE = 0x8289;
        public const uint GL_FRAMEBUFFER_RENDERABLE_LAYERED = 0x828A;
        public const uint GL_FRAMEBUFFER_BLEND = 0x828B;
        public const uint GL_READ_PIXELS = 0x828C;
        public const uint GL_READ_PIXELS_FORMAT = 0x828D;
        public const uint GL_READ_PIXELS_TYPE = 0x828E;
        public const uint GL_TEXTURE_IMAGE_FORMAT = 0x828F;
        public const uint GL_TEXTURE_IMAGE_TYPE = 0x8290;
        public const uint GL_GET_TEXTURE_IMAGE_FORMAT = 0x8291;
        public const uint GL_GET_TEXTURE_IMAGE_TYPE = 0x8292;
        public const uint GL_MIPMAP = 0x8293;
        public const uint GL_MANUAL_GENERATE_MIPMAP = 0x8294;
        public const uint GL_AUTO_GENERATE_MIPMAP = 0x8295;
        public const uint GL_COLOR_ENCODING = 0x8296;
        public const uint GL_SRGB_READ = 0x8297;
        public const uint GL_SRGB_WRITE = 0x8298;
        public const uint GL_SRGB_DECODE_ARB = 0x8299;
        public const uint GL_FILTER = 0x829A;
        public const uint GL_VERTEX_TEXTURE = 0x829B;
        public const uint GL_TESS_CONTROL_TEXTURE = 0x829C;
        public const uint GL_TESS_EVALUATION_TEXTURE = 0x829D;
        public const uint GL_GEOMETRY_TEXTURE = 0x829E;
        public const uint GL_FRAGMENT_TEXTURE = 0x829F;
        public const uint GL_COMPUTE_TEXTURE = 0x82A0;
        public const uint GL_TEXTURE_SHADOW = 0x82A1;
        public const uint GL_TEXTURE_GATHER = 0x82A2;
        public const uint GL_TEXTURE_GATHER_SHADOW = 0x82A3;
        public const uint GL_SHADER_IMAGE_LOAD = 0x82A4;
        public const uint GL_SHADER_IMAGE_STORE = 0x82A5;
        public const uint GL_SHADER_IMAGE_ATOMIC = 0x82A6;
        public const uint GL_IMAGE_TEXEL_SIZE = 0x82A7;
        public const uint GL_IMAGE_COMPATIBILITY_CLASS = 0x82A8;
        public const uint GL_IMAGE_PIXEL_FORMAT = 0x82A9;
        public const uint GL_IMAGE_PIXEL_TYPE = 0x82AA;
        public const uint GL_IMAGE_FORMAT_COMPATIBILITY_TYPE = 0x90C7;
        public const uint GL_SIMULTANEOUS_TEXTURE_AND_DEPTH_TEST = 0x82AC;
        public const uint GL_SIMULTANEOUS_TEXTURE_AND_STENCIL_TEST = 0x82AD;
        public const uint GL_SIMULTANEOUS_TEXTURE_AND_DEPTH_WRITE = 0x82AE;
        public const uint GL_SIMULTANEOUS_TEXTURE_AND_STENCIL_WRITE = 0x82AF;
        public const uint GL_TEXTURE_COMPRESSED_BLOCK_WIDTH = 0x82B1;
        public const uint GL_TEXTURE_COMPRESSED_BLOCK_HEIGHT = 0x82B2;
        public const uint GL_TEXTURE_COMPRESSED_BLOCK_SIZE = 0x82B3;
        public const uint GL_CLEAR_BUFFER = 0x82B4;
        public const uint GL_TEXTURE_VIEW = 0x82B5;
        public const uint GL_VIEW_COMPATIBILITY_CLASS = 0x82B6;
        public const uint GL_FULL_SUPPORT = 0x82B7;
        public const uint GL_CAVEAT_SUPPORT = 0x82B8;
        public const uint GL_IMAGE_CLASS_4_X_32 = 0x82B9;
        public const uint GL_IMAGE_CLASS_2_X_32 = 0x82BA;
        public const uint GL_IMAGE_CLASS_1_X_32 = 0x82BB;
        public const uint GL_IMAGE_CLASS_4_X_16 = 0x82BC;
        public const uint GL_IMAGE_CLASS_2_X_16 = 0x82BD;
        public const uint GL_IMAGE_CLASS_1_X_16 = 0x82BE;
        public const uint GL_IMAGE_CLASS_4_X_8 = 0x82BF;
        public const uint GL_IMAGE_CLASS_2_X_8 = 0x82C0;
        public const uint GL_IMAGE_CLASS_1_X_8 = 0x82C1;
        public const uint GL_IMAGE_CLASS_11_11_10 = 0x82C2;
        public const uint GL_IMAGE_CLASS_10_10_10_2 = 0x82C3;
        public const uint GL_VIEW_CLASS_128_BITS = 0x82C4;
        public const uint GL_VIEW_CLASS_96_BITS = 0x82C5;
        public const uint GL_VIEW_CLASS_64_BITS = 0x82C6;
        public const uint GL_VIEW_CLASS_48_BITS = 0x82C7;
        public const uint GL_VIEW_CLASS_32_BITS = 0x82C8;
        public const uint GL_VIEW_CLASS_24_BITS = 0x82C9;
        public const uint GL_VIEW_CLASS_16_BITS = 0x82CA;
        public const uint GL_VIEW_CLASS_8_BITS = 0x82CB;
        public const uint GL_VIEW_CLASS_S3TC_DXT1_RGB = 0x82CC;
        public const uint GL_VIEW_CLASS_S3TC_DXT1_RGBA = 0x82CD;
        public const uint GL_VIEW_CLASS_S3TC_DXT3_RGBA = 0x82CE;
        public const uint GL_VIEW_CLASS_S3TC_DXT5_RGBA = 0x82CF;
        public const uint GL_VIEW_CLASS_RGTC1_RED = 0x82D0;
        public const uint GL_VIEW_CLASS_RGTC2_RG = 0x82D1;
        public const uint GL_VIEW_CLASS_BPTC_UNORM = 0x82D2;
        public const uint GL_VIEW_CLASS_BPTC_FLOAT = 0x82D3;

        #endregion

        #region GL_ARB_invalidate_subdata

        /// <summary>
        /// Invalidate a region of a texture image
        /// </summary>
        /// <param name="texture">The name of a texture object a subregion of which to invalidate.</param>
        /// <param name="level">The level of detail of the texture object within which the region resides.</param>
        /// <param name="xoffset">The X offset of the region to be invalidated.</param>
        /// <param name="yoffset">The Y offset of the region to be invalidated.</param>
        /// <param name="zoffset">The Z offset of the region to be invalidated.</param>
        /// <param name="width">The width of the region to be invalidated.</param>
        /// <param name="height">The height of the region to be invalidated.</param>
        /// <param name="depth">The depth of the region to be invalidated.</param>
        public void InvalidateTexSubImage(uint texture, int level, int xoffset, int yoffset, int zoffset,
            uint width, uint height, uint depth)
        {
            GetDelegateFor<glInvalidateTexSubImage>()(texture, level, xoffset, yoffset, zoffset, width, height, depth);
        }

        /// <summary>
        /// Invalidate the entirety a texture image
        /// </summary>
        /// <param name="texture">The name of a texture object to invalidate.</param>
        /// <param name="level">The level of detail of the texture object to invalidate.</param>
        public void InvalidateTexImage(uint texture, int level)
        {
            GetDelegateFor<glInvalidateTexImage>()(texture, level);
        }

        /// <summary>
        /// Invalidate a region of a buffer object's data store
        /// </summary>
        /// <param name="buffer">The name of a buffer object, a subrange of whose data store to invalidate.</param>
        /// <param name="offset">The offset within the buffer's data store of the start of the range to be invalidated.</param>
        /// <param name="length">The length of the range within the buffer's data store to be invalidated.</param>
        public void InvalidateBufferSubData(uint buffer, IntPtr offset, IntPtr length)
        {
            GetDelegateFor<glInvalidateBufferSubData>()(buffer, offset, length);
        }

        /// <summary>
        /// Invalidate the content of a buffer object's data store
        /// </summary>
        /// <param name="buffer">The name of a buffer object whose data store to invalidate.</param>
        public void InvalidateBufferData(uint buffer)
        {
            GetDelegateFor<glInvalidateBufferData>()(buffer);
        }

        /// <summary>
        /// Invalidate the content some or all of a framebuffer object's attachments
        /// </summary>
        /// <param name="target">The target to which the framebuffer is attached. target​ must be GL_FRAMEBUFFER​, GL_DRAW_FRAMEBUFFER​, or GL_READ_FRAMEBUFFER​.</param>
        /// <param name="numAttachments">The number of entries in the attachments​ array.</param>
        /// <param name="attachments">The address of an array identifying the attachments to be invalidated.</param>
        public void InvalidateFramebuffer(uint target, uint numAttachments, uint[] attachments)
        {
            GetDelegateFor<glInvalidateFramebuffer>()(target, numAttachments, attachments);
        }

        /// <summary>
        /// Invalidate the content of a region of some or all of a framebuffer object's attachments
        /// </summary>
        /// <param name="target">The target to which the framebuffer is attached. target​ must be GL_FRAMEBUFFER​, GL_DRAW_FRAMEBUFFER​, or GL_READ_FRAMEBUFFER​.</param>
        /// <param name="numAttachments">The number of entries in the attachments​ array.</param>
        /// <param name="attachments">The address of an array identifying the attachments to be invalidated.</param>
        /// <param name="x">The X offset of the region to be invalidated.</param>
        /// <param name="y">The Y offset of the region to be invalidated.</param>
        /// <param name="width">The width of the region to be invalidated.</param>
        /// <param name="height">The height of the region to be invalidated.</param>
        public void InvalidateSubFramebuffer(uint target, uint numAttachments, uint[] attachments,
            int x, int y, uint width, uint height)
        {
            GetDelegateFor<glInvalidateSubFramebuffer>()(target, numAttachments, attachments, x, y, width, height);
        }

        //  Delegates
        private delegate void glInvalidateTexSubImage(uint texture, int level, int xoffset,
            int yoffset, int zoffset, uint width, uint height, uint depth);
        private delegate void glInvalidateTexImage(uint texture, int level);
        private delegate void glInvalidateBufferSubData(uint buffer, IntPtr offset, IntPtr length);
        private delegate void glInvalidateBufferData(uint buffer);
        private delegate void glInvalidateFramebuffer(uint target, uint numAttachments, uint[] attachments);
        private delegate void glInvalidateSubFramebuffer(uint target, uint numAttachments, uint[] attachments,
            int x, int y, uint width, uint height);

        #endregion

        #region ARB_multi_draw_indirect

        /// <summary>
        /// Render multiple sets of primitives from array data, taking parameters from memory
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render. Symbolic constants GL_POINTS​, GL_LINE_STRIP​, GL_LINE_LOOP​, GL_LINES​, GL_LINE_STRIP_ADJACENCY​, GL_LINES_ADJACENCY​, GL_TRIANGLE_STRIP​, GL_TRIANGLE_FAN​, GL_TRIANGLES​, GL_TRIANGLE_STRIP_ADJACENCY​, GL_TRIANGLES_ADJACENCY​, and GL_PATCHES​ are accepted.</param>
        /// <param name="indirect">Specifies the address of an array of structures containing the draw parameters.</param>
        /// <param name="primcount">Specifies the the number of elements in the array of draw parameter structures.</param>
        /// <param name="stride">Specifies the distance in basic machine units between elements of the draw parameter array.</param>
        public void MultiDrawArraysIndirect(uint mode, IntPtr indirect, uint primcount, uint stride)
        {
            GetDelegateFor<glMultiDrawArraysIndirect>()(mode, indirect, primcount, stride);
        }

        /// <summary>
        /// Render indexed primitives from array data, taking parameters from memory
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render. Symbolic constants GL_POINTS​, GL_LINE_STRIP​, GL_LINE_LOOP​, GL_LINES​, GL_LINE_STRIP_ADJACENCY​, GL_LINES_ADJACENCY​, GL_TRIANGLE_STRIP​, GL_TRIANGLE_FAN​, GL_TRIANGLES​, GL_TRIANGLE_STRIP_ADJACENCY​, GL_TRIANGLES_ADJACENCY​, and GL_PATCHES​ are accepted.</param>
        /// <param name="type">Specifies the type of data in the buffer bound to the GL_ELEMENT_ARRAY_BUFFER​ binding.</param>
        /// <param name="indirect">Specifies a byte offset (cast to a pointer type) into the buffer bound to GL_DRAW_INDIRECT_BUFFER​, which designates the starting point of the structure containing the draw parameters.</param>
        /// <param name="primcount">Specifies the number of elements in the array addressed by indirect​.</param>
        /// <param name="stride">Specifies the distance in basic machine units between elements of the draw parameter array.</param>
        public void MultiDrawElementsIndirect(uint mode, uint type, IntPtr indirect, uint primcount, uint stride)
        {
            GetDelegateFor<glMultiDrawElementsIndirect>()(mode, type, indirect, primcount, stride);
        }

        private delegate void glMultiDrawArraysIndirect(uint mode, IntPtr indirect, uint primcount, uint stride);
        private delegate void glMultiDrawElementsIndirect(uint mode, uint type, IntPtr indirect, uint primcount, uint stride);

        #endregion

        #region GL_ARB_program_interface_query

        /// <summary>
        /// Query a property of an interface in a program
        /// </summary>
        /// <param name="program">The name of a program object whose interface to query.</param>
        /// <param name="programInterface">A token identifying the interface within program​ to query.</param>
        /// <param name="pname">The name of the parameter within programInterface​ to query.</param>
        /// <param name="parameters">The address of a variable to retrieve the value of pname​ for the program interface..</param>
        public void GetProgramInterface(uint program, uint programInterface, uint pname, int[] parameters)
        {
            GetDelegateFor<glGetProgramInterfaceiv>()(program, programInterface, pname, parameters);
        }

        /// <summary>
        /// Query the index of a named resource within a program
        /// </summary>
        /// <param name="program">The name of a program object whose resources to query.</param>
        /// <param name="programInterface">A token identifying the interface within program​ containing the resource named name​.</param>
        /// <param name="name">The name of the resource to query the index of.</param>
        public void GetProgramResourceIndex(uint program, uint programInterface, string name)
        {
            GetDelegateFor<glGetProgramResourceIndex>()(program, programInterface, name);
        }

        /// <summary>
        /// Query the name of an indexed resource within a program
        /// </summary>
        /// <param name="program">The name of a program object whose resources to query.</param>
        /// <param name="programInterface">A token identifying the interface within program​ containing the indexed resource.</param>
        /// <param name="index">The index of the resource within programInterface​ of program​.</param>
        /// <param name="bufSize">The size of the character array whose address is given by name​.</param>
        /// <param name="length">The address of a variable which will receive the length of the resource name.</param>
        /// <param name="name">The address of a character array into which will be written the name of the resource.</param>
        public void GetProgramResourceName(uint program, uint programInterface, uint index, uint bufSize, out uint length, out string name)
        {
            var lengthParameter = new uint[1];
            var nameParameter = new string[1];
            GetDelegateFor<glGetProgramResourceName>()(program, programInterface, index, bufSize, lengthParameter, nameParameter);
            length = lengthParameter[0];
            name = nameParameter[0];
        }

        /// <summary>
        /// Retrieve values for multiple properties of a single active resource within a program object
        /// </summary>
        /// <param name="program">The name of a program object whose resources to query.</param>
        /// <param name="programInterface">A token identifying the interface within program​ containing the resource named name​.</param>
        /// <param name="index">The index within the programInterface​ to query information about.</param>
        /// <param name="propCount">The number of properties being queried.</param>
        /// <param name="props">An array of properties of length propCount​ to query.</param>
        /// <param name="bufSize">The number of GLint values in the params​ array.</param>
        /// <param name="length">If not NULL, then this value will be filled in with the number of actual parameters written to params​.</param>
        /// <param name="parameters">The output array of parameters to write.</param>
        public void GetProgramResource(uint program, uint programInterface, uint index, uint propCount, uint[] props, uint bufSize, out uint length, out int[] parameters)
        {
            var lengthParameter = new uint[1];
            var parametersParameter = new int[bufSize];

            GetDelegateFor<glGetProgramResourceiv>()(program, programInterface, index, propCount, props, bufSize, lengthParameter, parametersParameter);
            length = lengthParameter[0];
            parameters = parametersParameter;
        }

        /// <summary>
        /// Query the location of a named resource within a program.
        /// </summary>
        /// <param name="program">The name of a program object whose resources to query.</param>
        /// <param name="programInterface">A token identifying the interface within program​ containing the resource named name​.</param>
        /// <param name="name">The name of the resource to query the location of.</param>
        public void GetProgramResourceLocation(uint program, uint programInterface, string name)
        {
            GetDelegateFor<glGetProgramResourceLocation>()(program, programInterface, name);
        }

        /// <summary>
        /// Query the fragment color index of a named variable within a program.
        /// </summary>
        /// <param name="program">The name of a program object whose resources to query.</param>
        /// <param name="programInterface">A token identifying the interface within program​ containing the resource named name​.</param>
        /// <param name="name">The name of the resource to query the location of.</param>
        public void GetProgramResourceLocationIndex(uint program, uint programInterface, string name)
        {
            GetDelegateFor<glGetProgramResourceLocationIndex>()(program, programInterface, name);
        }

        private delegate void glGetProgramInterfaceiv(uint program, uint programInterface, uint pname, int[] parameters);
        private delegate uint glGetProgramResourceIndex(uint program, uint programInterface, string name);
        private delegate void glGetProgramResourceName(uint program, uint programInterface, uint index, uint bufSize, uint[] length, string[] name);
        private delegate void glGetProgramResourceiv(uint program, uint programInterface, uint index, uint propCount, uint[] props, uint bufSize, uint[] length, int[] parameters);
        private delegate int glGetProgramResourceLocation(uint program, uint programInterface, string name);
        private delegate int glGetProgramResourceLocationIndex(uint program, uint programInterface, string name);

        #endregion

        #region GL_ARB_shader_storage_buffer_object

        /// <summary>
        /// Change an active shader storage block binding.
        /// </summary>
        /// <param name="program">The name of the program containing the block whose binding to change.</param>
        /// <param name="storageBlockIndex">The index storage block within the program.</param>
        /// <param name="storageBlockBinding">The index storage block binding to associate with the specified storage block.</param>
        public void ShaderStorageBlockBinding(uint program, uint storageBlockIndex, uint storageBlockBinding)
        {
            GetDelegateFor<glShaderStorageBlockBinding>()(program, storageBlockIndex, storageBlockBinding);
        }

        private delegate void glShaderStorageBlockBinding(uint program, uint storageBlockIndex, uint storageBlockBinding);

        //  Constants
        public const uint GL_SHADER_STORAGE_BUFFER = 0x90D2;
        public const uint GL_SHADER_STORAGE_BUFFER_BINDING = 0x90D3;
        public const uint GL_SHADER_STORAGE_BUFFER_START = 0x90D4;
        public const uint GL_SHADER_STORAGE_BUFFER_SIZE = 0x90D5;
        public const uint GL_MAX_VERTEX_SHADER_STORAGE_BLOCKS = 0x90D6;
        public const uint GL_MAX_GEOMETRY_SHADER_STORAGE_BLOCKS = 0x90D7;
        public const uint GL_MAX_TESS_CONTROL_SHADER_STORAGE_BLOCKS = 0x90D8;
        public const uint GL_MAX_TESS_EVALUATION_SHADER_STORAGE_BLOCKS = 0x90D9;
        public const uint GL_MAX_FRAGMENT_SHADER_STORAGE_BLOCKS = 0x90DA;
        public const uint GL_MAX_COMPUTE_SHADER_STORAGE_BLOCKS = 0x90DB;
        public const uint GL_MAX_COMBINED_SHADER_STORAGE_BLOCKS = 0x90DC;
        public const uint GL_MAX_SHADER_STORAGE_BUFFER_BINDINGS = 0x90DD;
        public const uint GL_MAX_SHADER_STORAGE_BLOCK_SIZE = 0x90DE;
        public const uint GL_SHADER_STORAGE_BUFFER_OFFSET_ALIGNMENT = 0x90DF;
        public const uint GL_SHADER_STORAGE_BARRIER_BIT = 0x2000;
        public const uint GL_MAX_COMBINED_SHADER_OUTPUT_RESOURCES = 0x8F39;

        #endregion

        #region GL_ARB_stencil_texturing

        //  Constants
        public const uint GL_DEPTH_STENCIL_TEXTURE_MODE = 0x90EA;

        #endregion

        #region GL_ARB_texture_buffer_range

        /// <summary>
        /// Bind a range of a buffer's data store to a buffer texture
        /// </summary>
        /// <param name="target">Specifies the target of the operation and must be GL_TEXTURE_BUFFER​.</param>
        /// <param name="internalformat">Specifies the internal format of the data in the store belonging to buffer​.</param>
        /// <param name="buffer">Specifies the name of the buffer object whose storage to attach to the active buffer texture.</param>
        /// <param name="offset">Specifies the offset of the start of the range of the buffer's data store to attach.</param>
        /// <param name="size">Specifies the size of the range of the buffer's data store to attach.</param>
        public void TexBufferRange(uint target, uint internalformat, uint buffer, IntPtr offset, IntPtr size)
        {
            GetDelegateFor<glTexBufferRange>()(target, internalformat, buffer, offset, size);
        }

        /// <summary>
        /// Bind a range of a buffer's data store to a buffer texture
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="target">Specifies the target of the operation and must be GL_TEXTURE_BUFFER​.</param>
        /// <param name="internalformat">Specifies the internal format of the data in the store belonging to buffer​.</param>
        /// <param name="buffer">Specifies the name of the buffer object whose storage to attach to the active buffer texture.</param>
        /// <param name="offset">Specifies the offset of the start of the range of the buffer's data store to attach.</param>
        /// <param name="size">Specifies the size of the range of the buffer's data store to attach.</param>
        public void TextureBufferRangeEXT(uint texture, uint target, uint internalformat, uint buffer, IntPtr offset, IntPtr size)
        {
            GetDelegateFor<glTextureBufferRangeEXT>()(texture, target, internalformat, buffer, offset, size);
        }

        private delegate void glTexBufferRange(uint target, uint internalformat, uint buffer, IntPtr offset, IntPtr size);
        private delegate void glTextureBufferRangeEXT(uint texture, uint target, uint internalformat, uint buffer, IntPtr offset, IntPtr size);

        #endregion

        #region GL_ARB_texture_storage_multisample

        /// <summary>
        /// Specify storage for a two-dimensional multisample texture.
        /// </summary>
        /// <param name="target">Specify the target of the operation. target​ must be GL_TEXTURE_2D_MULTISAMPLE​ or GL_PROXY_TEXTURE_2D_MULTISAMPLE​.</param>
        /// <param name="samples">Specify the number of samples in the texture.</param>
        /// <param name="internalformat">Specifies the sized internal format to be used to store texture image data.</param>
        /// <param name="width">Specifies the width of the texture, in texels.</param>
        /// <param name="height">Specifies the height of the texture, in texels.</param>
        /// <param name="fixedsamplelocations">Specifies whether the image will use identical sample locations and the same number of samples for all texels in the image, and the sample locations will not depend on the internal format or size of the image.</param>
        public void TexStorage2DMultisample(uint target, uint samples, uint internalformat, uint width, uint height, bool fixedsamplelocations)
        {
            GetDelegateFor<glTexStorage2DMultisample>()(target, samples, internalformat, width, height, fixedsamplelocations);
        }

        /// <summary>
        /// Specify storage for a three-dimensional multisample array texture
        /// </summary>
        /// <param name="target">Specify the target of the operation. target​ must be GL_TEXTURE_3D_MULTISAMPLE_ARRAY​ or GL_PROXY_TEXTURE_3D_MULTISAMPLE_ARRAY​.</param>
        /// <param name="samples">Specify the number of samples in the texture.</param>
        /// <param name="internalformat">Specifies the sized internal format to be used to store texture image data.</param>
        /// <param name="width">Specifies the width of the texture, in texels.</param>
        /// <param name="height">Specifies the height of the texture, in texels.</param>
        /// <param name="depth">Specifies the depth of the texture, in layers.</param>
        /// <param name="fixedsamplelocations">Specifies the depth of the texture, in layers.</param>
        public void TexStorage3DMultisample(uint target, uint samples, uint internalformat, uint width, uint height, uint depth, bool fixedsamplelocations)
        {
            GetDelegateFor<glTexStorage3DMultisample>()(target, samples, internalformat, width, height, depth, fixedsamplelocations);
        }

        /// <summary>
        /// Specify storage for a two-dimensional multisample texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="target">Specify the target of the operation. target​ must be GL_TEXTURE_2D_MULTISAMPLE​ or GL_PROXY_TEXTURE_2D_MULTISAMPLE​.</param>
        /// <param name="samples">Specify the number of samples in the texture.</param>
        /// <param name="internalformat">Specifies the sized internal format to be used to store texture image data.</param>
        /// <param name="width">Specifies the width of the texture, in texels.</param>
        /// <param name="height">Specifies the height of the texture, in texels.</param>
        /// <param name="fixedsamplelocations">Specifies whether the image will use identical sample locations and the same number of samples for all texels in the image, and the sample locations will not depend on the internal format or size of the image.</param>
        public void TexStorage2DMultisampleEXT(uint texture, uint target, uint samples, uint internalformat, uint width, uint height, bool fixedsamplelocations)
        {
            GetDelegateFor<glTexStorage2DMultisampleEXT>()(texture, target, samples, internalformat, width, height, fixedsamplelocations);
        }

        /// <summary>
        /// Specify storage for a three-dimensional multisample array texture
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="target">Specify the target of the operation. target​ must be GL_TEXTURE_3D_MULTISAMPLE_ARRAY​ or GL_PROXY_TEXTURE_3D_MULTISAMPLE_ARRAY​.</param>
        /// <param name="samples">Specify the number of samples in the texture.</param>
        /// <param name="internalformat">Specifies the sized internal format to be used to store texture image data.</param>
        /// <param name="width">Specifies the width of the texture, in texels.</param>
        /// <param name="height">Specifies the height of the texture, in texels.</param>
        /// <param name="depth">Specifies the depth of the texture, in layers.</param>
        /// <param name="fixedsamplelocations">Specifies the depth of the texture, in layers.</param>
        public void TexStorage3DMultisampleEXT(uint texture, uint target, uint samples, uint internalformat, uint width, uint height, uint depth, bool fixedsamplelocations)
        {
            GetDelegateFor<glTexStorage3DMultisampleEXT>()(texture, target, samples, internalformat, width, height, depth, fixedsamplelocations);
        }

        //  Delegates
        private delegate void glTexStorage2DMultisample(uint target, uint samples, uint internalformat, uint width, uint height, bool fixedsamplelocations);
        private delegate void glTexStorage3DMultisample(uint target, uint samples, uint internalformat, uint width, uint height, uint depth, bool fixedsamplelocations);
        private delegate void glTexStorage2DMultisampleEXT(uint texture, uint target, uint samples, uint internalformat, uint width, uint height, bool fixedsamplelocations);
        private delegate void glTexStorage3DMultisampleEXT(uint texture, uint target, uint samples, uint internalformat, uint width, uint height, uint depth, bool fixedsamplelocations);

        #endregion

        #region GL_ARB_texture_view

        /// <summary>
        /// Initialize a texture as a data alias of another texture's data store.
        /// </summary>
        /// <param name="texture">Specifies the texture object to be initialized as a view.</param>
        /// <param name="target">Specifies the target to be used for the newly initialized texture.</param>
        /// <param name="origtexture">Specifies the name of a texture object of which to make a view.</param>
        /// <param name="internalformat">Specifies the internal format for the newly created view.</param>
        /// <param name="minlevel">Specifies lowest level of detail of the view.</param>
        /// <param name="numlevels">Specifies the number of levels of detail to include in the view.</param>
        /// <param name="minlayer">Specifies the index of the first layer to include in the view.</param>
        /// <param name="numlayers">Specifies the number of layers to include in the view.</param>
        public void TextureView(uint texture, uint target, uint origtexture, uint internalformat, uint minlevel, uint numlevels, uint minlayer, uint numlayers)
        {
            GetDelegateFor<glTextureView>()(texture, target, origtexture, internalformat, minlevel, numlevels, minlayer, numlayers);
        }

        //  Delegates
        private delegate void glTextureView(uint texture, uint target, uint origtexture, uint internalformat, uint minlevel, uint numlevels, uint minlayer, uint numlayers);

        //  Constants
        public const uint GL_TEXTURE_VIEW_MIN_LEVEL = 0x82DB;
        public const uint GL_TEXTURE_VIEW_NUM_LEVELS = 0x82DC;
        public const uint GL_TEXTURE_VIEW_MIN_LAYER = 0x82DD;
        public const uint GL_TEXTURE_VIEW_NUM_LAYERS = 0x82DE;

        #endregion

        #region GL_ARB_vertex_attrib_binding

        /// <summary>
        /// Bind a buffer to a vertex buffer bind point.
        /// </summary>
        /// <param name="bindingindex">The index of the vertex buffer binding point to which to bind the buffer.</param>
        /// <param name="buffer">The name of an existing buffer to bind to the vertex buffer binding point.</param>
        /// <param name="offset">The offset of the first element of the buffer.</param>
        /// <param name="stride">The distance between elements within the buffer.</param>
        public void BindVertexBuffer(uint bindingindex, uint buffer, IntPtr offset, uint stride)
        {
            GetDelegateFor<glBindVertexBuffer>()(bindingindex, buffer, offset, stride);
        }

        /// <summary>
        /// Specify the organization of vertex arrays.
        /// </summary>
        /// <param name="attribindex">The generic vertex attribute array being described.</param>
        /// <param name="size">The number of values per vertex that are stored in the array.</param>
        /// <param name="type">The type of the data stored in the array.</param>
        /// <param name="normalized">GL_TRUE​ if the parameter represents a normalized integer (type​ must be an integer type). GL_FALSE​ otherwise.</param>
        /// <param name="relativeoffset">The offset, measured in basic machine units of the first element relative to the start of the vertex buffer binding this attribute fetches from.</param>
        public void VertexAttribFormat(uint attribindex, int size, uint type, bool normalized, uint relativeoffset)
        {
            GetDelegateFor<glVertexAttribFormat>()(attribindex, size, type, normalized, relativeoffset);
        }

        /// <summary>
        /// Specify the organization of vertex arrays.
        /// </summary>
        /// <param name="attribindex">The generic vertex attribute array being described.</param>
        /// <param name="size">The number of values per vertex that are stored in the array.</param>
        /// <param name="type">The type of the data stored in the array.</param>
        /// <param name="relativeoffset">The offset, measured in basic machine units of the first element relative to the start of the vertex buffer binding this attribute fetches from.</param>
        public void VertexAttribIFormat(uint attribindex, int size, uint type, uint relativeoffset)
        {
            GetDelegateFor<glVertexAttribIFormat>()(attribindex, size, type, relativeoffset);
        }

        /// <summary>
        /// Specify the organization of vertex arrays.
        /// </summary>
        /// <param name="attribindex">The generic vertex attribute array being described.</param>
        /// <param name="size">The number of values per vertex that are stored in the array.</param>
        /// <param name="type">The type of the data stored in the array.</param>
        /// <param name="relativeoffset">The offset, measured in basic machine units of the first element relative to the start of the vertex buffer binding this attribute fetches from.</param>
        public void VertexAttribLFormat(uint attribindex, int size, uint type, uint relativeoffset)
        {
            GetDelegateFor<glVertexAttribLFormat>()(attribindex, size, type, relativeoffset);
        }

        /// <summary>
        /// Associate a vertex attribute and a vertex buffer binding.
        /// </summary>
        /// <param name="attribindex">The index of the attribute to associate with a vertex buffer binding.</param>
        /// <param name="bindingindex">The index of the vertex buffer binding with which to associate the generic vertex attribute.</param>
        public void VertexAttribBinding(uint attribindex, uint bindingindex)
        {
            GetDelegateFor<glVertexAttribBinding>()(attribindex, bindingindex);
        }

        /// <summary>
        /// Modify the rate at which generic vertex attributes advance.
        /// </summary>
        /// <param name="bindingindex">The index of the binding whose divisor to modify.</param>
        /// <param name="divisor">The new value for the instance step rate to apply.</param>
        public void VertexBindingDivisor(uint bindingindex, uint divisor)
        {
            GetDelegateFor<glVertexBindingDivisor>()(bindingindex, divisor);
        }

        /// <summary>
        /// Bind a buffer to a vertex buffer bind point.
        /// Available only when When EXT_direct_state_access is present.
        /// </summary>
        /// <param name="vaobj">The vertex array object.</param>
        /// <param name="bindingindex">The index of the vertex buffer binding point to which to bind the buffer.</param>
        /// <param name="buffer">The name of an existing buffer to bind to the vertex buffer binding point.</param>
        /// <param name="offset">The offset of the first element of the buffer.</param>
        /// <param name="stride">The distance between elements within the buffer.</param>
        public void VertexArrayBindVertexBufferEXT(uint vaobj, uint bindingindex, uint buffer, IntPtr offset, uint stride)
        {
            GetDelegateFor<glVertexArrayBindVertexBufferEXT>()(vaobj, bindingindex, buffer, offset, stride);
        }

        /// <summary>
        /// Specify the organization of vertex arrays.
        /// Available only when When EXT_direct_state_access is present.
        /// </summary>
        /// <param name="vaobj">The vertex array object.</param>
        /// <param name="attribindex">The generic vertex attribute array being described.</param>
        /// <param name="size">The number of values per vertex that are stored in the array.</param>
        /// <param name="type">The type of the data stored in the array.</param>
        /// <param name="normalized">GL_TRUE​ if the parameter represents a normalized integer (type​ must be an integer type). GL_FALSE​ otherwise.</param>
        /// <param name="relativeoffset">The offset, measured in basic machine units of the first element relative to the start of the vertex buffer binding this attribute fetches from.</param>
        public void VertexArrayVertexAttribFormatEXT(uint vaobj, uint attribindex, int size, uint type, bool normalized, uint relativeoffset)
        {
            GetDelegateFor<glVertexArrayVertexAttribFormatEXT>()(vaobj, attribindex, size, type, normalized, relativeoffset);
        }

        /// <summary>
        /// Specify the organization of vertex arrays.
        /// Available only when When EXT_direct_state_access is present.
        /// </summary>
        /// <param name="vaobj">The vertex array object.</param>
        /// <param name="attribindex">The generic vertex attribute array being described.</param>
        /// <param name="size">The number of values per vertex that are stored in the array.</param>
        /// <param name="type">The type of the data stored in the array.</param>
        /// <param name="relativeoffset">The offset, measured in basic machine units of the first element relative to the start of the vertex buffer binding this attribute fetches from.</param>
        public void VertexArrayVertexAttribIFormatEXT(uint vaobj, uint attribindex, int size, uint type, uint relativeoffset)
        {
            GetDelegateFor<glVertexArrayVertexAttribIFormatEXT>()(vaobj, attribindex, size, type, relativeoffset);
        }

        /// <summary>
        /// Specify the organization of vertex arrays.
        /// Available only when When EXT_direct_state_access is present.
        /// </summary>
        /// <param name="vaobj">The vertex array object.</param>
        /// <param name="attribindex">The generic vertex attribute array being described.</param>
        /// <param name="size">The number of values per vertex that are stored in the array.</param>
        /// <param name="type">The type of the data stored in the array.</param>
        /// <param name="relativeoffset">The offset, measured in basic machine units of the first element relative to the start of the vertex buffer binding this attribute fetches from.</param>
        public void VertexArrayVertexAttribLFormatEXT(uint vaobj, uint attribindex, int size, uint type, uint relativeoffset)
        {
            GetDelegateFor<glVertexArrayVertexAttribLFormatEXT>()(vaobj, attribindex, size, type, relativeoffset);
        }

        /// <summary>
        /// Associate a vertex attribute and a vertex buffer binding.
        /// Available only when When EXT_direct_state_access is present.
        /// </summary>
        /// <param name="vaobj">The vertex array object.</param>
        /// <param name="attribindex">The index of the attribute to associate with a vertex buffer binding.</param>
        /// <param name="bindingindex">The index of the vertex buffer binding with which to associate the generic vertex attribute.</param>
        public void VertexArrayVertexAttribBindingEXT(uint vaobj, uint attribindex, uint bindingindex)
        {
            GetDelegateFor<glVertexArrayVertexAttribBindingEXT>()(vaobj, attribindex, bindingindex);
        }

        /// <summary>
        /// Modify the rate at which generic vertex attributes advance.
        /// Available only when When EXT_direct_state_access is present.
        /// </summary>
        /// <param name="vaobj">The vertex array object.</param>
        /// <param name="bindingindex">The index of the binding whose divisor to modify.</param>
        /// <param name="divisor">The new value for the instance step rate to apply.</param>
        public void VertexArrayVertexBindingDivisorEXT(uint vaobj, uint bindingindex, uint divisor)
        {
            GetDelegateFor<glVertexArrayVertexBindingDivisorEXT>()(vaobj, bindingindex, divisor);
        }

        //  Delegates
        private delegate void glBindVertexBuffer(uint bindingindex, uint buffer, IntPtr offset, uint stride);
        private delegate void glVertexAttribFormat(uint attribindex, int size, uint type, bool normalized, uint relativeoffset);
        private delegate void glVertexAttribIFormat(uint attribindex, int size, uint type, uint relativeoffset);
        private delegate void glVertexAttribLFormat(uint attribindex, int size, uint type, uint relativeoffset);
        private delegate void glVertexAttribBinding(uint attribindex, uint bindingindex);
        private delegate void glVertexBindingDivisor(uint bindingindex, uint divisor);
        private delegate void glVertexArrayBindVertexBufferEXT(uint vaobj, uint bindingindex, uint buffer, IntPtr offset, uint stride);
        private delegate void glVertexArrayVertexAttribFormatEXT(uint vaobj, uint attribindex, int size, uint type, bool normalized, uint relativeoffset);
        private delegate void glVertexArrayVertexAttribIFormatEXT(uint vaobj, uint attribindex, int size, uint type, uint relativeoffset);
        private delegate void glVertexArrayVertexAttribLFormatEXT(uint vaobj, uint attribindex, int size, uint type, uint relativeoffset);
        private delegate void glVertexArrayVertexAttribBindingEXT(uint vaobj, uint attribindex, uint bindingindex);
        private delegate void glVertexArrayVertexBindingDivisorEXT(uint vaobj, uint bindingindex, uint divisor);

        //  Constants
        public const uint GL_VERTEX_ATTRIB_BINDING = 0x82D4;
        public const uint GL_VERTEX_ATTRIB_RELATIVE_OFFSET = 0x82D5;
        public const uint GL_VERTEX_BINDING_DIVISOR = 0x82D6;
        public const uint GL_VERTEX_BINDING_OFFSET = 0x82D7;
        public const uint GL_VERTEX_BINDING_STRIDE = 0x82D8;
        public const uint GL_VERTEX_BINDING_BUFFER = 0x8F4F;
        public const uint GL_MAX_VERTEX_ATTRIB_RELATIVE_OFFSET = 0x82D9;
        public const uint GL_MAX_VERTEX_ATTRIB_BINDINGS = 0x82DA;

        #endregion

        #region WGL_ARB_robustness_isolation

        public const uint WGL_CONTEXT_RESET_ISOLATION_BIT_ARB = 0x00000008;

        #endregion
        
        #region GLX_ARB_robustness_isolation

        public const uint GLX_CONTEXT_RESET_ISOLATION_BIT_ARB = 0x00000008;

        #endregion
    }

// ReSharper restore InconsistentNaming
}
