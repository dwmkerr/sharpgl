using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Runtime.InteropServices;

namespace SharpGL
{
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
    }
}
