using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SharpGL
{
    public partial class OpenGL
    {

        #region Missing constants
        public const uint GL_COPY_READ_BUFFER = 0x8F36;
        public const uint GL_COPY_WRITE_BUFFER = 0x8F37;
        public const uint GL_UNIFORM_BUFFER = 0x8A11;
        public const uint GL_HALF_FLOAT = 0x140B;
        public const uint GL_INT_2_10_10_10_REV = 0x8D9F;
        public const uint GL_COLOR_ATTACHMENT0 = 0x8CE0;
        public const uint GL_ACTIVE_ATOMIC_COUNTER_BUFFERS = 0x92D9;
        public const uint GL_ACTIVE_UNIFORM_BLOCKS = 0x8A36;
        public const uint GL_ACTIVE_UNIFORM_BLOCK_MAX_NAME_LENGTH = 0x8A35;
        public const uint GL_COMPUTE_WORK_GROUP_SIZE = 0x8267;
        public const uint GL_PROGRAM_BINARY_LENGTH = 0x8741;

        public const uint GL_COMPUTE_SHADER = 0x91B9;
        public const uint GL_TESS_CONTROL_SHADER = 0x8E88;
        public const uint GL_TESS_EVALUATION_SHADER = 0x8E87;

        public const uint GL_ATOMIC_COUNTER_BUFFER = 0x92C0;
        public const uint GL_DRAW_INDIRECT_BUFFER = 0x8F3F;
        public const uint GL_DISPATCH_INDIRECT_BUFFER = 0x90EE;
        public const uint GL_QUERY_BUFFER = 0x9192;
        public const uint GL_SHADER_STORAGE_BUFFER = 0x90D2;

        public const uint GL_MAP_READ_BIT = 0x0001;

        #endregion Missing constants

        #region Missing DLL Functions
        private delegate IntPtr glMapBufferRange(uint target, int offset, int length, uint access);
        #endregion Missing DLL Functions

        #region Missing Wrapped OpenGL Functionspublic void Accum(uint op, float value)
        public IntPtr MapBufferRange(uint target, int offset, int length, uint access)
        {
            return (IntPtr)InvokeExtensionFunction<glMapBufferRange>(target, offset, length, access);
        }

        #endregion Missing Wrapped OpenGL Functions




        public void BufferData(uint target, uint[] data, uint usage)
        {
            var dataSize = data.Length * sizeof(uint);
            IntPtr p = Marshal.AllocHGlobal(dataSize);
            var intData = new int[data.Length];
            Buffer.BlockCopy(data, 0, intData, 0, dataSize);
            Marshal.Copy(intData, 0, p, data.Length);
            InvokeExtensionFunction<glBufferData>(target, dataSize, p, usage);
            Marshal.FreeHGlobal(p);
        }

        public void BufferData(uint target, byte[] data, uint usage)
        {
            var dataSize = data.Length * sizeof(byte);
            IntPtr p = Marshal.AllocHGlobal(dataSize);
            var byteData = new byte[data.Length];
            Buffer.BlockCopy(data, 0, byteData, 0, dataSize);
            Marshal.Copy(byteData, 0, p, data.Length);
            InvokeExtensionFunction<glBufferData>(target, dataSize, p, usage);
            Marshal.FreeHGlobal(p);
        }
    }

}
