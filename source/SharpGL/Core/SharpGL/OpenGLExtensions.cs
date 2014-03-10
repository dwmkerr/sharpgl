using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace SharpGL
{
    public partial class OpenGL
    {
        /// <summary>
        /// Determines whether a named extension function is supported.
        /// </summary>
        /// <param name="extensionFunctionName">Name of the extension function.</param>
        /// <returns>
        /// 	<c>true</c> if the extension function is supported; otherwise, <c>false</c>.
        /// </returns>
        public bool IsExtensionFunctionSupported(string extensionFunctionName)
        {
            //  Try and get the proc address for the function.
            IntPtr procAddress = Win32.wglGetProcAddress(extensionFunctionName);

            //  As long as the pointer is non-zero, we can invoke the extension function.
            return procAddress != IntPtr.Zero;
        }

        /// <summary>
        /// Invokes an extension function.
        /// </summary>
        /// <typeparam name="T">The extension delegate type.</typeparam>
        /// <param name="args">The arguments to the pass to the function.</param>
        /// <returns>The return value of the extension function.</returns>
        private object InvokeExtensionFunction<T>(params object[] args)
        {
            //  Get the type of the extension function.
            Type delegateType = typeof(T);

            //  Get the name of the extension function.
            string name = delegateType.Name;

            //  Does the dictionary contain our extension function?
            Delegate del = null;
            if (extensionFunctions.ContainsKey(name) == false)
            {
                //  We haven't loaded it yet. Load it now.
                IntPtr proc = Win32.wglGetProcAddress(name);
                if (proc == IntPtr.Zero)
                    throw new Exception("Extension function " + name + " not supported");

                //  Get the delegate for the function pointer.
                del = Marshal.GetDelegateForFunctionPointer(proc, delegateType);
                if (del == null)
                    throw new Exception("Extension function " + name + " marshalled incorrectly");

                //  Add to the dictionary.
                extensionFunctions.Add(name, del);
            }

            //  Get the delegate.
            del = extensionFunctions[name];

            //  Try and invoke it.
            object result = null;
            try
            {
                result = del.DynamicInvoke(args);
            }
            catch(Exception e)
            {
                throw new Exception("Cannot invoke extension function " + name, e);
            }

            return result;
        }

        /// <summary>
        /// The set of extension functions.
        /// </summary>
        private Dictionary<string, Delegate> extensionFunctions = new Dictionary<string, Delegate>();

        #region OpenGL 1.2

        //  Methods
        public void BlendColor(float red, float green, float blue, float alpha)
        {
            InvokeExtensionFunction<glBlendColor>(red, green, blue, alpha);
        }
        public void BlendEquation(uint mode)
        {
            InvokeExtensionFunction<glBlendEquation>(mode);
        }
        public void DrawRangeElements(uint mode, uint start, uint end, int count, uint type, IntPtr indices)
        {
            InvokeExtensionFunction<glDrawRangeElements>(mode, start, end, count, type, indices);
        }
        public void TexImage3D(uint target, int level, int internalformat, int width, int height, int depth, int border, uint format, uint type, IntPtr pixels)
        {
            InvokeExtensionFunction<glTexImage3D>(target, level, internalformat, width, height, depth, border, format, type, pixels);
        }
        public void TexSubImage3D(uint target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, uint type, IntPtr pixels)
        {
            InvokeExtensionFunction<glTexSubImage3D>(target, level, xoffset, yoffset, zoffset, width, height, depth, format, type, pixels);
        }
        public void CopyTexSubImage3D(uint target, int level, int xoffset, int yoffset, int zoffset, int x, int y, int width, int height)
        {
            InvokeExtensionFunction<glCopyTexSubImage3D>(target, level, xoffset, yoffset, zoffset, x, y, width, height);
        }

        //  Deprecated Methods
        [Obsolete]
        public void ColorTable(uint target, uint internalformat, int width, uint format, uint type, IntPtr table)
        {
            InvokeExtensionFunction<glColorTable>(target, internalformat, width, format, type, table);
        }
        [Obsolete]
        public void ColorTableParameterfv(uint target, uint pname, float[] parameters)
        {
            InvokeExtensionFunction<glColorTableParameterfv>(target, pname, parameters);
        }
        [Obsolete]
        public void ColorTableParameteriv(uint target, uint pname, int[] parameters)
        {
            InvokeExtensionFunction<glColorTableParameteriv>(target, pname, parameters);
        }
        [Obsolete]
        public void CopyColorTable(uint target, uint internalformat, int x, int y, int width)
        {
            InvokeExtensionFunction<glCopyColorTable>(target, internalformat, x, y, width);
        }
        [Obsolete]
        public void GetColorTable(uint target, uint format, uint type, IntPtr table)
        {
            InvokeExtensionFunction<glGetColorTable>(target, format, type, table);
        }
        [Obsolete]
        public void GetColorTableParameter(uint target, uint pname, float[] parameters)
        {
            InvokeExtensionFunction<glGetColorTableParameterfv>(target, pname, parameters);
        }
        [Obsolete]
        public void GetColorTableParameter(uint target, uint pname, int[] parameters)
        {
            InvokeExtensionFunction<glGetColorTableParameteriv>(target, pname, parameters);
        }
        [Obsolete]
        public void ColorSubTable(uint target, int start, int count, uint format, uint type, IntPtr data)
        {
            InvokeExtensionFunction<glColorSubTable>(target, start, count, format, type, data);
        }
        [Obsolete]
        public void CopyColorSubTable(uint target, int start, int x, int y, int width)
        {
            InvokeExtensionFunction<glCopyColorSubTable>(target, start, x, y, width);
        }
        [Obsolete]
        public void ConvolutionFilter1D(uint target, uint internalformat, int width, uint format, uint type, IntPtr image)
        {
            InvokeExtensionFunction<glConvolutionFilter1D>(target, internalformat, width, format, type, image);
        }
        [Obsolete]
        public void ConvolutionFilter2D(uint target, uint internalformat, int width, int height, uint format, uint type, IntPtr image)
        {
            InvokeExtensionFunction<glConvolutionFilter2D>(target, internalformat, width, height, format, type, image);
        }
        [Obsolete]
        public void ConvolutionParameter(uint target, uint pname, float parameters)
        {
            InvokeExtensionFunction<glConvolutionParameterf>(target, pname, parameters);
        }
        [Obsolete]
        public void ConvolutionParameter(uint target, uint pname, float[] parameters)
        {
            InvokeExtensionFunction<glConvolutionParameterfv>(target, pname, parameters);
        }
        [Obsolete]
        public void ConvolutionParameter(uint target, uint pname, int parameters)
        {
            InvokeExtensionFunction<glConvolutionParameteri>(target, pname, parameters);
        }
        [Obsolete]
        public void ConvolutionParameter(uint target, uint pname, int[] parameters)
        {
            InvokeExtensionFunction<glConvolutionParameteriv>(target, pname, parameters);
        }
        [Obsolete]
        public void CopyConvolutionFilter1D(uint target, uint internalformat, int x, int y, int width)
        {
            InvokeExtensionFunction<glCopyConvolutionFilter1D>(target, internalformat, x, y, width);
        }
        [Obsolete]
        public void CopyConvolutionFilter2D(uint target, uint internalformat, int x, int y, int width, int height)
        {
            InvokeExtensionFunction<glCopyConvolutionFilter2D>(target, internalformat, x, y, width, height);
        }
        [Obsolete]
        public void GetConvolutionFilter(uint target, uint format, uint type, IntPtr image)
        {
            InvokeExtensionFunction<glGetConvolutionFilter>(target, format, type, image);
        }
        [Obsolete]
        public void GetConvolutionParameter(uint target, uint pname, float[] parameters)
        {
            InvokeExtensionFunction<glGetConvolutionParameterfv>(target, pname, parameters);
        }
        [Obsolete]
        public void GetConvolutionParameter(uint target, uint pname, int[] parameters)
        {
            InvokeExtensionFunction<glGetConvolutionParameteriv>(target, pname, parameters);
        }
        [Obsolete]
        public void GetSeparableFilter(uint target, uint format, uint type, IntPtr row, IntPtr column, IntPtr span)
        {
            InvokeExtensionFunction<glGetSeparableFilter>(target, format, type, row, column, span);
        }
        [Obsolete]
        public void SeparableFilter2D(uint target, uint internalformat, int width, int height, uint format, uint type, IntPtr row, IntPtr column)
        {
            InvokeExtensionFunction<glSeparableFilter2D>(target, internalformat, width, height, format, type, row, column);
        }
        [Obsolete]
        public void GetHistogram(uint target, bool reset, uint format, uint type, IntPtr values)
        {
            InvokeExtensionFunction<glGetHistogram>(target, reset, format, type, values);
        }
        [Obsolete]
        public void GetHistogramParameter(uint target, uint pname, float[] parameters)
        {
            InvokeExtensionFunction<glGetHistogramParameterfv>(target, pname, parameters);
        }
        [Obsolete]
        public void GetHistogramParameter(uint target, uint pname, int[] parameters)
        {
            InvokeExtensionFunction<glGetHistogramParameteriv>(target, pname, parameters);
        }
        [Obsolete]
        public void GetMinmax(uint target, bool reset, uint format, uint type, IntPtr values)
        {
            InvokeExtensionFunction<glGetMinmax>(target, reset, format, type, values);
        }
        [Obsolete]
        public void GetMinmaxParameter(uint target, uint pname, float[] parameters)
        {
            InvokeExtensionFunction<glGetMinmaxParameterfv>(target, pname, parameters);
        }
        [Obsolete]
        public void GetMinmaxParameter(uint target, uint pname, int[] parameters)
        {
            InvokeExtensionFunction<glGetMinmaxParameteriv>(target, pname, parameters);
        }
        [Obsolete]
        public void Histogram(uint target, int width, uint internalformat, bool sink)
        {
            InvokeExtensionFunction<glHistogram>(target, width, internalformat, sink);
        }
        [Obsolete]
        public void Minmax(uint target, uint internalformat, bool sink)
        {
            InvokeExtensionFunction<glMinmax>(target, internalformat, sink);
        }
        [Obsolete]
        public void ResetHistogram(uint target)
        {
            InvokeExtensionFunction<glResetHistogram>(target);
        }
        [Obsolete]
        public void ResetMinmax(uint target)
        {
            InvokeExtensionFunction<glResetMinmax>(target);
        }

        //  Delegates
        private delegate void glBlendColor (float red, float green, float blue, float alpha);
        private delegate void glBlendEquation (uint mode);
        private delegate void glDrawRangeElements (uint mode, uint start, uint end, int count, uint type, IntPtr indices);
        private delegate void glTexImage3D (uint target, int level, int internalformat, int width, int height, int depth, int border, uint format, uint type, IntPtr pixels);
        private delegate void glTexSubImage3D (uint target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, uint type, IntPtr pixels);
        private delegate void glCopyTexSubImage3D (uint target, int level, int xoffset, int yoffset, int zoffset, int x, int y, int width, int height);
        private delegate void glColorTable (uint target, uint internalformat, int width, uint format, uint type, IntPtr table);
        private delegate void glColorTableParameterfv (uint target, uint pname, float[] parameters);
        private delegate void glColorTableParameteriv (uint target, uint pname, int[] parameters);
        private delegate void glCopyColorTable (uint target, uint internalformat, int x, int y, int width);
        private delegate void glGetColorTable (uint target, uint format, uint type, IntPtr table);
        private delegate void glGetColorTableParameterfv (uint target, uint pname, float[] parameters);
        private delegate void glGetColorTableParameteriv (uint target, uint pname, int[] parameters);
        private delegate void glColorSubTable (uint target, int start, int count, uint format, uint type, IntPtr data);
        private delegate void glCopyColorSubTable (uint target, int start, int x, int y, int width);
        private delegate void glConvolutionFilter1D (uint target, uint internalformat, int width, uint format, uint type, IntPtr image);
        private delegate void glConvolutionFilter2D (uint target, uint internalformat, int width, int height, uint format, uint type, IntPtr image);
        private delegate void glConvolutionParameterf (uint target, uint pname, float parameters);
        private delegate void glConvolutionParameterfv (uint target, uint pname, float[] parameters);
        private delegate void glConvolutionParameteri (uint target, uint pname, int parameters);
        private delegate void glConvolutionParameteriv (uint target, uint pname, int[] parameters);
        private delegate void glCopyConvolutionFilter1D (uint target, uint internalformat, int x, int y, int width);
        private delegate void glCopyConvolutionFilter2D (uint target, uint internalformat, int x, int y, int width, int height);
        private delegate void glGetConvolutionFilter (uint target, uint format, uint type, IntPtr image);
        private delegate void glGetConvolutionParameterfv (uint target, uint pname, float[] parameters);
        private delegate void glGetConvolutionParameteriv (uint target, uint pname, int[] parameters);
        private delegate void glGetSeparableFilter (uint target, uint format, uint type, IntPtr row, IntPtr column, IntPtr span);
        private delegate void glSeparableFilter2D (uint target, uint internalformat, int width, int height, uint format, uint type, IntPtr row, IntPtr column);
        private delegate void glGetHistogram (uint target, bool reset, uint format, uint type, IntPtr values);
        private delegate void glGetHistogramParameterfv (uint target, uint pname, float[] parameters);
        private delegate void glGetHistogramParameteriv (uint target, uint pname, int[] parameters);
        private delegate void glGetMinmax (uint target, bool reset, uint format, uint type, IntPtr values);
        private delegate void glGetMinmaxParameterfv (uint target, uint pname, float[] parameters);
        private delegate void glGetMinmaxParameteriv (uint target, uint pname, int[] parameters);
        private delegate void glHistogram (uint target, int width, uint internalformat, bool sink);
        private delegate void glMinmax (uint target, uint internalformat, bool sink);
        private delegate void glResetHistogram (uint target);
        private delegate void glResetMinmax (uint target);

        //  Constants
        public const uint GL_UNSIGNED_BYTE_3_3_2             = 0x8032;
        public const uint GL_UNSIGNED_SHORT_4_4_4_4          = 0x8033;
        public const uint GL_UNSIGNED_SHORT_5_5_5_1          = 0x8034;
        public const uint GL_UNSIGNED_INT_8_8_8_8            = 0x8035;
        public const uint GL_UNSIGNED_INT_10_10_10_2         = 0x8036;
        public const uint GL_TEXTURE_BINDING_3D              = 0x806A;
        public const uint GL_PACK_SKIP_IMAGES                = 0x806B;
        public const uint GL_PACK_IMAGE_HEIGHT               = 0x806C;
        public const uint GL_UNPACK_SKIP_IMAGES              = 0x806D;
        public const uint GL_UNPACK_IMAGE_HEIGHT             = 0x806E;
        public const uint GL_TEXTURE_3D                      = 0x806F;
        public const uint GL_PROXY_TEXTURE_3D                = 0x8070;
        public const uint GL_TEXTURE_DEPTH                   = 0x8071;
        public const uint GL_TEXTURE_WRAP_R                  = 0x8072;
        public const uint GL_MAX_3D_TEXTURE_SIZE             = 0x8073;
        public const uint GL_UNSIGNED_BYTE_2_3_3_REV         = 0x8362;
        public const uint GL_UNSIGNED_SHORT_5_6_5            = 0x8363;
        public const uint GL_UNSIGNED_SHORT_5_6_5_REV        = 0x8364;
        public const uint GL_UNSIGNED_SHORT_4_4_4_4_REV      = 0x8365;
        public const uint GL_UNSIGNED_SHORT_1_5_5_5_REV      = 0x8366;
        public const uint GL_UNSIGNED_INT_8_8_8_8_REV        = 0x8367;
        public const uint GL_UNSIGNED_INT_2_10_10_10_REV     = 0x8368;
        public const uint GL_BGR                             = 0x80E0;
        public const uint GL_BGRA                            = 0x80E1;
        public const uint GL_MAX_ELEMENTS_VERTICES           = 0x80E8;
        public const uint GL_MAX_ELEMENTS_INDICES            = 0x80E9;
        public const uint GL_CLAMP_TO_EDGE                   = 0x812F;
        public const uint GL_TEXTURE_MIN_LOD                 = 0x813A;
        public const uint GL_TEXTURE_MAX_LOD                 = 0x813B;
        public const uint GL_TEXTURE_BASE_LEVEL              = 0x813C;
        public const uint GL_TEXTURE_MAX_LEVEL               = 0x813D;
        public const uint GL_SMOOTH_POINT_SIZE_RANGE         = 0x0B12;
        public const uint GL_SMOOTH_POINT_SIZE_GRANULARITY   = 0x0B13;
        public const uint GL_SMOOTH_LINE_WIDTH_RANGE         = 0x0B22;
        public const uint GL_SMOOTH_LINE_WIDTH_GRANULARITY   = 0x0B23;
        public const uint GL_ALIASED_LINE_WIDTH_RANGE        = 0x846E;

        #endregion

        #region OpenGL 1.3

        //  Methods
        
        
        public void ActiveTexture(uint texture)
        {
            InvokeExtensionFunction<glActiveTexture>(texture);
        }
        public void SampleCoverage(float value, bool invert)
        {
            InvokeExtensionFunction<glSampleCoverage>(value, invert);
        }
        public void CompressedTexImage3D(uint target, int level, uint internalformat, int width, int height, int depth, int border, int imageSize, IntPtr data)
        {
            InvokeExtensionFunction<glCompressedTexImage3D>(target, level, internalformat, width, height, depth, border, imageSize, data);
        }
        public void CompressedTexImage2D(uint target, int level, uint internalformat, int width, int height, int border, int imageSize, IntPtr data)
        {
            InvokeExtensionFunction<glCompressedTexImage2D>(target, level, internalformat, width, height, border, imageSize, data);
        }
        public void CompressedTexImage1D(uint target, int level, uint internalformat, int width, int border, int imageSize, IntPtr data)
        {
            InvokeExtensionFunction<glCompressedTexImage1D>(target, level, internalformat, width, border, imageSize, data);
        }
        public void CompressedTexSubImage3D(uint target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, int imageSize, IntPtr data)
        {
            InvokeExtensionFunction<glCompressedTexSubImage3D>(target, level, xoffset, yoffset, zoffset, width, height, depth, format, imageSize, data);
        }
        public void CompressedTexSubImage2D(uint target, int level, int xoffset, int yoffset, int width, int height, uint format, int imageSize, IntPtr data)
        {
            InvokeExtensionFunction<glCompressedTexSubImage2D>(target, level, xoffset, yoffset, width, height, format, imageSize, data);
        }
        public void CompressedTexSubImage1D(uint target, int level, int xoffset, int width, uint format, int imageSize, IntPtr data)
        {
            InvokeExtensionFunction<glCompressedTexSubImage1D>(target, level, xoffset, width, format, imageSize, data);
        }
        public void GetCompressedTexImage(uint target, int level, IntPtr img)
        {
            InvokeExtensionFunction<glGetCompressedTexImage>(target, level, img);
        }

        //  Deprecated Methods
        [Obsolete]
        public void ClientActiveTexture(uint texture)
        {
            InvokeExtensionFunction<glClientActiveTexture>(texture);
        }
        [Obsolete]
        public void MultiTexCoord1(uint target, double s)
        {
            InvokeExtensionFunction<glMultiTexCoord1d>(target, s);
        }
        [Obsolete]
        public void MultiTexCoord1(uint target, double[] v)
        {
            InvokeExtensionFunction<glMultiTexCoord1dv>(target, v);
        }
        [Obsolete]
        public void MultiTexCoord1(uint target, float s)
        {
            InvokeExtensionFunction<glMultiTexCoord1f>(target, s);
        }
        [Obsolete]
        public void MultiTexCoord1(uint target, float[] v)
        {
            InvokeExtensionFunction<glMultiTexCoord1fv>(target, v);
        }
        [Obsolete]
        public void MultiTexCoord1(uint target, int s)
        {
            InvokeExtensionFunction<glMultiTexCoord1i>(target, s);
        }
        [Obsolete]
        public void MultiTexCoord1(uint target, int[] v)
        {
            InvokeExtensionFunction<glMultiTexCoord1iv>(target, v);
        }
        [Obsolete]
        public void MultiTexCoord1(uint target, short s)
        {
            InvokeExtensionFunction<glMultiTexCoord1s>(target, s);
        }
        [Obsolete]
        public void MultiTexCoord1(uint target, short[] v)
        {
            InvokeExtensionFunction<glMultiTexCoord1sv>(target, v);
        }
        [Obsolete]
        public void MultiTexCoord2(uint target, double s, double t)
        {
            InvokeExtensionFunction<glMultiTexCoord2d>(target, s, t);
        }
        [Obsolete]
        public void MultiTexCoord2(uint target, double[] v)
        {
            InvokeExtensionFunction<glMultiTexCoord2dv>(target, v);
        }
        [Obsolete]
        public void MultiTexCoord2(uint target, float s, float t)
        {
            InvokeExtensionFunction<glMultiTexCoord2f>(target, s, t);
        }
        [Obsolete]
        public void MultiTexCoord2(uint target, float[] v)
        {
            InvokeExtensionFunction<glMultiTexCoord2fv>(target, v);
        }
        [Obsolete]
        public void MultiTexCoord2(uint target, int s, int t)
        {
            InvokeExtensionFunction<glMultiTexCoord2i>(target, s, t);
        }
        [Obsolete]
        public void MultiTexCoord2(uint target, int[] v)
        {
            InvokeExtensionFunction<glMultiTexCoord2iv>(target, v);
        }
        [Obsolete]
        public void MultiTexCoord2(uint target, short s, short t)
        {
            InvokeExtensionFunction<glMultiTexCoord2s>(target, s, t);
        }
        [Obsolete]
        public void MultiTexCoord2(uint target, short[] v)
        {
            InvokeExtensionFunction<glMultiTexCoord2sv>(target, v);
        }
        [Obsolete]
        public void MultiTexCoord3(uint target, double s, double t, double r)
        {
            InvokeExtensionFunction<glMultiTexCoord3d>(target, s, t, r);
        }
        [Obsolete]
        public void MultiTexCoord3(uint target, double[] v)
        {
            InvokeExtensionFunction<glMultiTexCoord3dv>(target, v);
        }
        [Obsolete]
        public void MultiTexCoord3(uint target, float s, float t, float r)
        {
            InvokeExtensionFunction<glMultiTexCoord3f>(target, s, t, r);
        }
        [Obsolete]
        public void MultiTexCoord3(uint target, float[] v)
        {
            InvokeExtensionFunction<glMultiTexCoord3fv>(target, v);
        }
        [Obsolete]
        public void MultiTexCoord3(uint target, int s, int t, int r)
        {
            InvokeExtensionFunction<glMultiTexCoord3i>(target, s, t, r);
        }
        [Obsolete]
        public void MultiTexCoord3(uint target, int[] v)
        {
            InvokeExtensionFunction<glMultiTexCoord3iv>(target, v);
        }
        [Obsolete]
        public void MultiTexCoord3(uint target, short s, short t, short r)
        {
            InvokeExtensionFunction<glMultiTexCoord3s>(target, s, t, r);
        }
        [Obsolete]
        public void MultiTexCoord3(uint target, short[] v)
        {
            InvokeExtensionFunction<glMultiTexCoord3sv>(target, v);
        }
        [Obsolete]
        public void MultiTexCoord4(uint target, double s, double t, double r, double q)
        {
            InvokeExtensionFunction<glMultiTexCoord4d>(target, s, t, r, q);
        }
        [Obsolete]
        public void MultiTexCoord4(uint target, double[] v)
        {
            InvokeExtensionFunction<glMultiTexCoord4dv>(target, v);
        }
        [Obsolete]
        public void MultiTexCoord4(uint target, float s, float t, float r, float q)
        {
            InvokeExtensionFunction<glMultiTexCoord4f>(target, s, t, r, q);
        }
        [Obsolete]
        public void MultiTexCoord4(uint target, float[] v)
        {
            InvokeExtensionFunction<glMultiTexCoord4fv>(target, v);
        }
        [Obsolete]
        public void MultiTexCoord4(uint target, int s, int t, int r, int q)
        {
            InvokeExtensionFunction<glMultiTexCoord4i>(target, s, t, r, q);
        }
        [Obsolete]
        public void MultiTexCoord4(uint target, int[] v)
        {
            InvokeExtensionFunction<glMultiTexCoord4iv>(target, v);
        }
        [Obsolete]
        public void MultiTexCoord4(uint target, short s, short t, short r, short q)
        {
            InvokeExtensionFunction<glMultiTexCoord4s>(target, s, t, r, q);
        }
        [Obsolete]
        public void MultiTexCoord4(uint target, short[] v)
        {
            InvokeExtensionFunction<glMultiTexCoord4sv>(target, v);
        }
        [Obsolete]
        public void LoadTransposeMatrix(float[] m)
        {
            InvokeExtensionFunction<glLoadTransposeMatrixf>(m);
        }
        [Obsolete]
        public void LoadTransposeMatrix(double[] m)
        {
            InvokeExtensionFunction<glLoadTransposeMatrixd>(m);
        }
        [Obsolete]
        public void MultTransposeMatrix(float[] m)
        {
            InvokeExtensionFunction<glMultTransposeMatrixf>(m);
        }
        [Obsolete]
        public void MultTransposeMatrix(double[] m)
        {
            InvokeExtensionFunction<glMultTransposeMatrixd>(m);
        }

        //  Delegates
        private delegate void glActiveTexture (uint texture);
        private delegate void glSampleCoverage (float value, bool invert);
        private delegate void glCompressedTexImage3D (uint target, int level, uint internalformat, int width, int height, int depth, int border, int imageSize, IntPtr data);
        private delegate void glCompressedTexImage2D (uint target, int level, uint internalformat, int width, int height, int border, int imageSize, IntPtr data);
        private delegate void glCompressedTexImage1D (uint target, int level, uint internalformat, int width, int border, int imageSize, IntPtr data);
        private delegate void glCompressedTexSubImage3D (uint target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, int imageSize, IntPtr data);
        private delegate void glCompressedTexSubImage2D (uint target, int level, int xoffset, int yoffset, int width, int height, uint format, int imageSize, IntPtr data);
        private delegate void glCompressedTexSubImage1D (uint target, int level, int xoffset, int width, uint format, int imageSize, IntPtr data);
        private delegate void glGetCompressedTexImage (uint target, int level, IntPtr img);

        private delegate void glClientActiveTexture (uint texture);
        private delegate void glMultiTexCoord1d (uint target, double s);
        private delegate void glMultiTexCoord1dv (uint target, double[] v);
        private delegate void glMultiTexCoord1f (uint target, float s);
        private delegate void glMultiTexCoord1fv (uint target, float[] v);
        private delegate void glMultiTexCoord1i (uint target, int s);
        private delegate void glMultiTexCoord1iv (uint target, int[] v);
        private delegate void glMultiTexCoord1s (uint target, short s);
        private delegate void glMultiTexCoord1sv (uint target, short[] v);
        private delegate void glMultiTexCoord2d (uint target, double s, double t);
        private delegate void glMultiTexCoord2dv (uint target, double[] v);
        private delegate void glMultiTexCoord2f (uint target, float s, float t);
        private delegate void glMultiTexCoord2fv (uint target, float[] v);
        private delegate void glMultiTexCoord2i (uint target, int s, int t);
        private delegate void glMultiTexCoord2iv (uint target, int[] v);
        private delegate void glMultiTexCoord2s (uint target, short s, short t);
        private delegate void glMultiTexCoord2sv (uint target, short[] v);
        private delegate void glMultiTexCoord3d (uint target, double s, double t, double r);
        private delegate void glMultiTexCoord3dv (uint target, double[] v);
        private delegate void glMultiTexCoord3f (uint target, float s, float t, float r);
        private delegate void glMultiTexCoord3fv (uint target, float[] v);
        private delegate void glMultiTexCoord3i (uint target, int s, int t, int r);
        private delegate void glMultiTexCoord3iv (uint target, int[] v);
        private delegate void glMultiTexCoord3s (uint target, short s, short t, short r);
        private delegate void glMultiTexCoord3sv (uint target, short[] v);
        private delegate void glMultiTexCoord4d (uint target, double s, double t, double r, double q);
        private delegate void glMultiTexCoord4dv (uint target, double[] v);
        private delegate void glMultiTexCoord4f (uint target, float s, float t, float r, float q);
        private delegate void glMultiTexCoord4fv (uint target, float[] v);
        private delegate void glMultiTexCoord4i (uint target, int s, int t, int r, int q);
        private delegate void glMultiTexCoord4iv (uint target, int[] v);
        private delegate void glMultiTexCoord4s (uint target, short s, short t, short r, short q);
        private delegate void glMultiTexCoord4sv (uint target, short[] v);
        private delegate void glLoadTransposeMatrixf (float[] m);
        private delegate void glLoadTransposeMatrixd (double[] m);
        private delegate void glMultTransposeMatrixf (float[] m);
        private delegate void glMultTransposeMatrixd (double[] m);

        //  Constants
        public const uint GL_TEXTURE0                        = 0x84C0;
        public const uint GL_TEXTURE1                        = 0x84C1;
        public const uint GL_TEXTURE2                        = 0x84C2;
        public const uint GL_TEXTURE3                        = 0x84C3;
        public const uint GL_TEXTURE4                        = 0x84C4;
        public const uint GL_TEXTURE5                        = 0x84C5;
        public const uint GL_TEXTURE6                        = 0x84C6;
        public const uint GL_TEXTURE7                        = 0x84C7;
        public const uint GL_TEXTURE8                        = 0x84C8;
        public const uint GL_TEXTURE9                        = 0x84C9;
        public const uint GL_TEXTURE10                       = 0x84CA;
        public const uint GL_TEXTURE11                       = 0x84CB;
        public const uint GL_TEXTURE12                       = 0x84CC;
        public const uint GL_TEXTURE13                       = 0x84CD;
        public const uint GL_TEXTURE14                       = 0x84CE;
        public const uint GL_TEXTURE15                       = 0x84CF;
        public const uint GL_TEXTURE16                       = 0x84D0;
        public const uint GL_TEXTURE17                       = 0x84D1;
        public const uint GL_TEXTURE18                       = 0x84D2;
        public const uint GL_TEXTURE19                       = 0x84D3;
        public const uint GL_TEXTURE20                       = 0x84D4;
        public const uint GL_TEXTURE21                       = 0x84D5;
        public const uint GL_TEXTURE22                       = 0x84D6;
        public const uint GL_TEXTURE23                       = 0x84D7;
        public const uint GL_TEXTURE24                       = 0x84D8;
        public const uint GL_TEXTURE25                       = 0x84D9;
        public const uint GL_TEXTURE26                       = 0x84DA;
        public const uint GL_TEXTURE27                       = 0x84DB;
        public const uint GL_TEXTURE28                       = 0x84DC;
        public const uint GL_TEXTURE29                       = 0x84DD;
        public const uint GL_TEXTURE30                       = 0x84DE;
        public const uint GL_TEXTURE31                       = 0x84DF;
        public const uint GL_ACTIVE_TEXTURE                  = 0x84E0;
        public const uint GL_MULTISAMPLE                     = 0x809D;
        public const uint GL_SAMPLE_ALPHA_TO_COVERAGE        = 0x809E;
        public const uint GL_SAMPLE_ALPHA_TO_ONE             = 0x809F;
        public const uint GL_SAMPLE_COVERAGE                 = 0x80A0;
        public const uint GL_SAMPLE_BUFFERS                  = 0x80A8;
        public const uint GL_SAMPLES                         = 0x80A9;
        public const uint GL_SAMPLE_COVERAGE_VALUE           = 0x80AA;
        public const uint GL_SAMPLE_COVERAGE_INVERT          = 0x80AB;
        public const uint GL_TEXTURE_CUBE_MAP                = 0x8513;
        public const uint GL_TEXTURE_BINDING_CUBE_MAP        = 0x8514;
        public const uint GL_TEXTURE_CUBE_MAP_POSITIVE_X     = 0x8515;
        public const uint GL_TEXTURE_CUBE_MAP_NEGATIVE_X     = 0x8516;
        public const uint GL_TEXTURE_CUBE_MAP_POSITIVE_Y     = 0x8517;
        public const uint GL_TEXTURE_CUBE_MAP_NEGATIVE_Y     = 0x8518;
        public const uint GL_TEXTURE_CUBE_MAP_POSITIVE_Z     = 0x8519;
        public const uint GL_TEXTURE_CUBE_MAP_NEGATIVE_Z     = 0x851A;
        public const uint GL_PROXY_TEXTURE_CUBE_MAP          = 0x851B;
        public const uint GL_MAX_CUBE_MAP_TEXTURE_SIZE       = 0x851C;
        public const uint GL_COMPRESSED_RGB                  = 0x84ED;
        public const uint GL_COMPRESSED_RGBA                 = 0x84EE;
        public const uint GL_TEXTURE_COMPRESSION_HINT        = 0x84EF;
        public const uint GL_TEXTURE_COMPRESSED_IMAGE_SIZE   = 0x86A0;
        public const uint GL_TEXTURE_COMPRESSED              = 0x86A1;
        public const uint GL_NUM_COMPRESSED_TEXTURE_FORMATS  = 0x86A2;
        public const uint GL_COMPRESSED_TEXTURE_FORMATS      = 0x86A3;
        public const uint GL_CLAMP_TO_BORDER                 = 0x812D;

        #endregion
        
        #region OpenGL 1.4

        //  Methods
        public void BlendFuncSeparate(uint sfactorRGB, uint dfactorRGB, uint sfactorAlpha, uint dfactorAlpha)
        {
            InvokeExtensionFunction<glBlendFuncSeparate>(sfactorRGB, dfactorRGB, sfactorAlpha, dfactorAlpha);
        }
        public void MultiDrawArrays(uint mode, int[] first, int[] count, int primcount)
        {
            InvokeExtensionFunction<glMultiDrawArrays>(mode, first, count, primcount);
        }
        public void MultiDrawElements(uint mode, int[] count, uint type, IntPtr indices, int primcount)
        {
            InvokeExtensionFunction<glMultiDrawElements>(mode, count, type, indices, primcount);
        }
        public void PointParameter(uint pname, float parameter)
        {
            InvokeExtensionFunction<glPointParameterf>(pname, parameter);
        }
        public void PointParameter(uint pname, float[] parameters)
        {
            InvokeExtensionFunction<glPointParameterfv>(pname, parameters);
        }
        public void PointParameter(uint pname, int parameter)
        {
            InvokeExtensionFunction<glPointParameteri>(pname, parameter);
        }
        public void PointParameter(uint pname, int[] parameters)
        {
            InvokeExtensionFunction<glPointParameteriv>(pname, parameters);
        }
        
        //  Deprecated Methods
        [Obsolete]
        public void FogCoord(float coord)
        {
            InvokeExtensionFunction<glFogCoordf>(coord);
        }
        [Obsolete]
        public void FogCoord(float[] coord)
        {
            InvokeExtensionFunction<glFogCoordfv>(coord);
        }
        [Obsolete]
        public void FogCoord(double coord)
        {
            InvokeExtensionFunction<glFogCoordd>(coord);
        }
        [Obsolete]
        public void FogCoord(double[] coord)
        {
            InvokeExtensionFunction<glFogCoorddv>(coord);
        }
        [Obsolete]
        public void FogCoordPointer(uint type, int stride, IntPtr pointer)
        {
            InvokeExtensionFunction<glFogCoordPointer>(type, stride, pointer);
        }
        [Obsolete]
        public void SecondaryColor3(sbyte red, sbyte green, sbyte blue)
        {
            InvokeExtensionFunction<glSecondaryColor3b>(red, green, blue);
        }
        [Obsolete]
        public void SecondaryColor3(sbyte[] v)
        {
            InvokeExtensionFunction<glSecondaryColor3bv>(v);
        }
        [Obsolete]
        public void SecondaryColor3(double red, double green, double blue)
        {
            InvokeExtensionFunction<glSecondaryColor3d>(red, green, blue);
        }
        [Obsolete]
        public void SecondaryColor3(double[] v)
        {
            InvokeExtensionFunction<glSecondaryColor3dv>(v);
        }
        [Obsolete]
        public void SecondaryColor3(float red, float green, float blue)
        {
            InvokeExtensionFunction<glSecondaryColor3f>(red, green, blue);
        }
        [Obsolete]
        public void SecondaryColor3(float[] v)
        {
            InvokeExtensionFunction<glSecondaryColor3fv>(v);
        }
        [Obsolete]
        public void SecondaryColor3(int red, int green, int blue)
        {
            InvokeExtensionFunction<glSecondaryColor3i>(red, green, blue);
        }
        [Obsolete]
        public void SecondaryColor3(int[] v)
        {
            InvokeExtensionFunction<glSecondaryColor3iv>(v);
        }
        [Obsolete]
        public void SecondaryColor3(short red, short green, short blue)
        {
            InvokeExtensionFunction<glSecondaryColor3s>(red, green, blue);
        }
        [Obsolete]
        public void SecondaryColor3(short[] v)
        {
            InvokeExtensionFunction<glSecondaryColor3sv>(v);
        }
        [Obsolete]
        public void SecondaryColor3(byte red, byte green, byte blue)
        {
            InvokeExtensionFunction<glSecondaryColor3ub>(red, green, blue);
        }
        [Obsolete]
        public void SecondaryColor3(byte[] v)
        {
            InvokeExtensionFunction<glSecondaryColor3ubv>(v);
        }
        [Obsolete]
        public void SecondaryColor3(uint red, uint green, uint blue)
        {
            InvokeExtensionFunction<glSecondaryColor3ui>(red, green, blue);
        }
        [Obsolete]
        public void SecondaryColor3(uint[] v)
        {
            InvokeExtensionFunction<glSecondaryColor3uiv>(v);
        }
        [Obsolete]
        public void SecondaryColor3(ushort red, ushort green, ushort blue)
        {
            InvokeExtensionFunction<glSecondaryColor3us>(red, green, blue);
        }
        [Obsolete]
        public void SecondaryColor3(ushort[] v)
        {
            InvokeExtensionFunction<glSecondaryColor3usv>(v);
        }
        [Obsolete]
        public void SecondaryColorPointer(int size, uint type, int stride, IntPtr pointer)
        {
            InvokeExtensionFunction<glSecondaryColorPointer>(size, type, stride, pointer);
        }
        [Obsolete]
        public void WindowPos2(double x, double y)
        {
            InvokeExtensionFunction<glWindowPos2d>(x, y);
        }
        [Obsolete]
        public void WindowPos2(double[] v)
        {
            InvokeExtensionFunction<glWindowPos2dv>(v);
        }
        [Obsolete]
        public void WindowPos2(float x, float y)
        {
            InvokeExtensionFunction<glWindowPos2f>(x, y);
        }
        [Obsolete]
        public void WindowPos2(float[] v)
        {
            InvokeExtensionFunction<glWindowPos2fv>(v);
        }
        [Obsolete]
        public void WindowPos2(int x, int y)
        {
            InvokeExtensionFunction<glWindowPos2i>(x, y);
        }
        [Obsolete]
        public void WindowPos2(int[] v)
        {
            InvokeExtensionFunction<glWindowPos2iv>(v);
        }
        [Obsolete]
        public void WindowPos2(short x, short y)
        {
            InvokeExtensionFunction<glWindowPos2s>(x, y);
        }
        [Obsolete]
        public void WindowPos2(short[] v)
        {
            InvokeExtensionFunction<glWindowPos2sv>(v);
        }
        [Obsolete]
        public void WindowPos3(double x, double y, double z)
        {
            InvokeExtensionFunction<glWindowPos3d>(x, y, z);
        }
        [Obsolete]
        public void WindowPos3(double[] v)
        {
            InvokeExtensionFunction<glWindowPos3dv>(v);
        }
        [Obsolete]
        public void WindowPos3(float x, float y, float z)
        {
            InvokeExtensionFunction<glWindowPos3f>(x, y, z);
        }
        [Obsolete]
        public void WindowPos3(float[] v)
        {
            InvokeExtensionFunction<glWindowPos3fv>(v);
        }
        [Obsolete]
        public void WindowPos3(int x, int y, int z)
        {
            InvokeExtensionFunction<glWindowPos3i>(x, y, z);
        }
        [Obsolete]
        public void WindowPos3(int[] v)
        {
            InvokeExtensionFunction<glWindowPos3iv>(v);
        }
        [Obsolete]
        public void WindowPos3(short x, short y, short z)
        {
            InvokeExtensionFunction<glWindowPos3s>(x, y, z);
        }
        [Obsolete]
        public void WindowPos3(short[] v)
        {
            InvokeExtensionFunction<glWindowPos3sv>(v);
        }

        //  Delegates
        private delegate void glBlendFuncSeparate (uint sfactorRGB, uint dfactorRGB, uint sfactorAlpha, uint dfactorAlpha);
        private delegate void glMultiDrawArrays (uint mode, int[] first, int[] count, int primcount);
        private delegate void glMultiDrawElements (uint mode, int[] count, uint type, IntPtr indices, int primcount);
        private delegate void glPointParameterf (uint pname, float parameter);
        private delegate void glPointParameterfv (uint pname, float[] parameters);
        private delegate void glPointParameteri (uint pname, int parameter);
        private delegate void glPointParameteriv (uint pname, int[] parameters);
        private delegate void glFogCoordf (float coord);
        private delegate void glFogCoordfv (float[] coord);
        private delegate void glFogCoordd (double coord);
        private delegate void glFogCoorddv (double[] coord);
        private delegate void glFogCoordPointer (uint type, int stride, IntPtr pointer);
        private delegate void glSecondaryColor3b (sbyte red, sbyte green, sbyte blue);
        private delegate void glSecondaryColor3bv (sbyte[] v);
        private delegate void glSecondaryColor3d (double red, double green, double blue);
        private delegate void glSecondaryColor3dv (double[] v);
        private delegate void glSecondaryColor3f (float red, float green, float blue);
        private delegate void glSecondaryColor3fv (float[] v);
        private delegate void glSecondaryColor3i (int red, int green, int blue);
        private delegate void glSecondaryColor3iv (int[] v);
        private delegate void glSecondaryColor3s (short red, short green, short blue);
        private delegate void glSecondaryColor3sv (short[] v);
        private delegate void glSecondaryColor3ub (byte red, byte green, byte blue);
        private delegate void glSecondaryColor3ubv (byte[] v);
        private delegate void glSecondaryColor3ui (uint red, uint green, uint blue);
        private delegate void glSecondaryColor3uiv (uint[] v);
        private delegate void glSecondaryColor3us (ushort red, ushort green, ushort blue);
        private delegate void glSecondaryColor3usv (ushort[] v);
        private delegate void glSecondaryColorPointer (int size, uint type, int stride, IntPtr pointer);
        private delegate void glWindowPos2d (double x, double y);
        private delegate void glWindowPos2dv (double[] v);
        private delegate void glWindowPos2f (float x, float y);
        private delegate void glWindowPos2fv (float[] v);
        private delegate void glWindowPos2i (int x, int y);
        private delegate void glWindowPos2iv (int[] v);
        private delegate void glWindowPos2s (short x, short y);
        private delegate void glWindowPos2sv (short[] v);
        private delegate void glWindowPos3d (double x, double y, double z);
        private delegate void glWindowPos3dv (double[] v);
        private delegate void glWindowPos3f (float x, float y, float z);
        private delegate void glWindowPos3fv (float[] v);
        private delegate void glWindowPos3i (int x, int y, int z);
        private delegate void glWindowPos3iv (int[] v);
        private delegate void glWindowPos3s (short x, short y, short z);
        private delegate void glWindowPos3sv (short[] v);

        //  Constants
        public const uint GL_BLEND_DST_RGB                   = 0x80C8;
        public const uint GL_BLEND_SRC_RGB                   = 0x80C9;
        public const uint GL_BLEND_DST_ALPHA                 = 0x80CA;
        public const uint GL_BLEND_SRC_ALPHA                 = 0x80CB;
        public const uint GL_POINT_FADE_THRESHOLD_SIZE       = 0x8128;
        public const uint GL_DEPTH_COMPONENT16               = 0x81A5;
        public const uint GL_DEPTH_COMPONENT24               = 0x81A6;
        public const uint GL_DEPTH_COMPONENT32               = 0x81A7;
        public const uint GL_MIRRORED_REPEAT                 = 0x8370;
        public const uint GL_MAX_TEXTURE_LOD_BIAS            = 0x84FD;
        public const uint GL_TEXTURE_LOD_BIAS                = 0x8501;
        public const uint GL_INCR_WRAP                       = 0x8507;
        public const uint GL_DECR_WRAP                       = 0x8508;
        public const uint GL_TEXTURE_DEPTH_SIZE              = 0x884A;
        public const uint GL_TEXTURE_COMPARE_MODE            = 0x884C;
        public const uint GL_TEXTURE_COMPARE_FUNC            = 0x884D;

        #endregion
        
        #region OpenGL 1.5

        //  Methods
        public void GenQueries(int n, uint[] ids)
        {
            InvokeExtensionFunction<glGenQueries>(n, ids);
        }
        public void DeleteQueries(int n, uint[] ids)
        {
            InvokeExtensionFunction<glDeleteQueries>(n, ids);
        }
        public bool IsQuery(uint id)
        {
            return (bool)InvokeExtensionFunction<glIsQuery>(id);
        }
        public void BeginQuery(uint target, uint id)
        {
            InvokeExtensionFunction<glBeginQuery>(target, id);
        }
        public void EndQuery(uint target)
        {
            InvokeExtensionFunction<glEndQuery>(target);
        }
        public void GetQuery(uint target, uint pname, int[] parameters)
        {
            InvokeExtensionFunction<glGetQueryiv>(target, pname, parameters);
        }
        public void GetQueryObject(uint id, uint pname, int[] parameters)
        {
            InvokeExtensionFunction<glGetQueryObjectiv>(id, pname, parameters);
        }
        public void GetQueryObject(uint id, uint pname, uint[] parameters)
        {
            InvokeExtensionFunction<glGetQueryObjectuiv>(id, pname, parameters);
        }
        public void BindBuffer(uint target, uint buffer)
        {
            InvokeExtensionFunction<glBindBuffer>(target, buffer);
        }
        public void DeleteBuffers(int n, uint[] buffers)
        {
            InvokeExtensionFunction<glDeleteBuffers>(n, buffers);
        }
        public void GenBuffers(int n, uint[] buffers)
        {
            InvokeExtensionFunction<glGenBuffers>(n, buffers);
        }
        public bool IsBuffer(uint buffer)
        {
            return (bool)InvokeExtensionFunction<glIsBuffer>(buffer);
        }
        public void BufferData(uint target, int size, IntPtr data, uint usage)
        {
            InvokeExtensionFunction<glBufferData>(target, size, data, usage);
        }
        public void BufferSubData(uint target, int offset, int size, IntPtr data)
        {
            InvokeExtensionFunction<glBufferSubData>(target, offset, size, data);
        }
        public void GetBufferSubData(uint target, int offset, int size, IntPtr data)
        {
            InvokeExtensionFunction<glGetBufferSubData>(target, offset, size, data);
        }
        public IntPtr MapBuffer(uint target, uint access)
        {
            return (IntPtr)InvokeExtensionFunction<glMapBuffer>(target, access);
        }
        public bool UnmapBuffer(uint target)
        {
            return (bool)InvokeExtensionFunction<glUnmapBuffer>(target);
        }
        public void GetBufferParameter(uint target, uint pname, int[] parameters)
        {
            InvokeExtensionFunction<glGetBufferParameteriv>(target, pname, parameters);
        }
        public void GetBufferPointer(uint target, uint pname, IntPtr[] parameters)
        {
            InvokeExtensionFunction<glGetBufferPointerv>();
        }
        
        //  Delegates
        private delegate void glGenQueries (int n, uint[] ids);
        private delegate void glDeleteQueries (int n, uint[] ids);
        private delegate bool glIsQuery (uint id);
        private delegate void glBeginQuery (uint target, uint id);
        private delegate void glEndQuery (uint target);
        private delegate void glGetQueryiv (uint target, uint pname, int[] parameters);
        private delegate void glGetQueryObjectiv (uint id, uint pname, int[] parameters);
        private delegate void glGetQueryObjectuiv (uint id, uint pname, uint[] parameters);
        private delegate void glBindBuffer (uint target, uint buffer);
        private delegate void glDeleteBuffers (int n, uint[] buffers);
        private delegate void glGenBuffers (int n, uint[] buffers);
        private delegate bool glIsBuffer (uint buffer);
        private delegate void glBufferData (uint target, int size, IntPtr data, uint usage);
        private delegate void glBufferSubData (uint target, int offset, int size, IntPtr data);
        private delegate void glGetBufferSubData (uint target, int offset, int size, IntPtr data);
        private delegate IntPtr glMapBuffer (uint target, uint access);
        private delegate bool glUnmapBuffer (uint target);
        private delegate void glGetBufferParameteriv (uint target, uint pname, int[] parameters);
        private delegate void glGetBufferPointerv (uint target, uint pname, IntPtr[] parameters);

        //  Constants
        public const uint GL_BUFFER_SIZE                             = 0x8764;
        public const uint GL_BUFFER_USAGE                            = 0x8765;
        public const uint GL_QUERY_COUNTER_BITS                      = 0x8864;
        public const uint GL_CURRENT_QUERY                           = 0x8865;
        public const uint GL_QUERY_RESULT                            = 0x8866;
        public const uint GL_QUERY_RESULT_AVAILABLE                  = 0x8867;
        public const uint GL_ARRAY_BUFFER                            = 0x8892;
        public const uint GL_ELEMENT_ARRAY_BUFFER                    = 0x8893;
        public const uint GL_ARRAY_BUFFER_BINDING                    = 0x8894;
        public const uint GL_ELEMENT_ARRAY_BUFFER_BINDING            = 0x8895;
        public const uint GL_VERTEX_ATTRIB_ARRAY_BUFFER_BINDING      = 0x889F;
        public const uint GL_READ_ONLY                               = 0x88B8;
        public const uint GL_WRITE_ONLY                              = 0x88B9;
        public const uint GL_READ_WRITE                              = 0x88BA;
        public const uint GL_BUFFER_ACCESS                           = 0x88BB;
        public const uint GL_BUFFER_MAPPED                           = 0x88BC;
        public const uint GL_BUFFER_MAP_POINTER                      = 0x88BD;
        public const uint GL_STREAM_DRAW                             = 0x88E0;
        public const uint GL_STREAM_READ                             = 0x88E1;
        public const uint GL_STREAM_COPY                             = 0x88E2;
        public const uint GL_STATIC_DRAW                             = 0x88E4;
        public const uint GL_STATIC_READ                             = 0x88E5;
        public const uint GL_STATIC_COPY                             = 0x88E6;
        public const uint GL_DYNAMIC_DRAW                            = 0x88E8;
        public const uint GL_DYNAMIC_READ                            = 0x88E9;
        public const uint GL_DYNAMIC_COPY                            = 0x88EA;
        public const uint GL_SAMPLES_PASSED                          = 0x8914;

        #endregion
        
        #region OpenGL 2.0

        //  Methods
        public void BlendEquationSeparate (uint modeRGB, uint modeAlpha)
        {
            InvokeExtensionFunction<glBlendEquationSeparate>(modeRGB, modeAlpha);
        }
        public void DrawBuffers (int n, uint[] bufs)
        {
            InvokeExtensionFunction<glDrawBuffers>(n, bufs);
        }
        public void StencilOpSeparate (uint face, uint sfail, uint dpfail, uint dppass)
        {
            InvokeExtensionFunction<glStencilOpSeparate>(face, sfail, dpfail, dppass);
        }
        public void StencilFuncSeparate (uint face, uint func, int reference, uint mask)
        {
            InvokeExtensionFunction<glStencilFuncSeparate>(face, func, reference, mask);
        }
        public void StencilMaskSeparate (uint face, uint mask)
        {
            InvokeExtensionFunction<glStencilMaskSeparate>(face, mask);
        }
        public void AttachShader (uint program, uint shader)
        {
            InvokeExtensionFunction<glAttachShader>(program, shader);
        }
        public void BindAttribLocation (uint program, uint index, string name)
        {
            InvokeExtensionFunction<glBindAttribLocation>(program, index, name);
        }
        public void CompileShader (uint shader)
        {
            InvokeExtensionFunction<glCompileShader>(shader);
        }
        public uint CreateProgram ()
        {
            return (uint)InvokeExtensionFunction<glCreateProgram>();
        }
        public uint CreateShader (uint type)
        {
            return (uint)InvokeExtensionFunction<glCreateShader>(type);
        }
        public void DeleteProgram (uint program)
        {
            InvokeExtensionFunction<glDeleteProgram>(program);
        }
        public void DeleteShader (uint shader)
        {
            InvokeExtensionFunction<glDeleteShader>(shader);
        }
        public void DetachShader (uint program, uint shader)
        {
            InvokeExtensionFunction<glDetachShader>(program, shader);
        }
        public void DisableVertexAttribArray (uint index)
        {
            InvokeExtensionFunction<glDisableVertexAttribArray>(index);
        }
        public void EnableVertexAttribArray (uint index)
        {
            InvokeExtensionFunction<glEnableVertexAttribArray>(index);
        }
        public void GetActiveAttrib (uint program, uint index, int bufSize, int[] length, int[] size, uint[] type, string name)
        {
            InvokeExtensionFunction<glGetActiveAttrib>(program, index, bufSize, length, size, type, name);
        }
        public void GetActiveUniform (uint program, uint index, int bufSize, int[] length, int[] size, uint[] type, string name)
        {
            InvokeExtensionFunction<glGetActiveUniform>(program, index, bufSize, length, size, type, name);
        }
        public void GetAttachedShaders (uint program, int maxCount, int[] count, uint[] obj)
        {
            InvokeExtensionFunction<glGetAttachedShaders>(program, maxCount, count, obj);
        }
        public int GetAttribLocation (uint program, string name)
        {
            return (int)InvokeExtensionFunction<glGetAttribLocation>(program, name);
        }
        public void GetProgram (uint program, uint pname, int[] parameters)
        {
            InvokeExtensionFunction<glGetProgramiv>(program, pname, parameters);
        }
        public void GetProgramInfoLog(uint program, int bufSize, IntPtr length, StringBuilder infoLog)
        {
            InvokeExtensionFunction<glGetProgramInfoLog>(program, bufSize, length, infoLog);
        }
        public void GetShader (uint shader, uint pname, int[] parameters)
        {
            InvokeExtensionFunction<glGetShaderiv>(shader, pname, parameters);
        }
        public void GetShaderInfoLog (uint shader, int bufSize, IntPtr length, StringBuilder infoLog)
        {
            InvokeExtensionFunction<glGetShaderInfoLog>(shader, bufSize, length, infoLog);

        }
        public void GetShaderSource(uint shader, int bufSize, IntPtr length, StringBuilder source)
        {
            InvokeExtensionFunction<glGetShaderSource>(shader, bufSize, length, source);
        }
        public int GetUniformLocation (uint program, string name)
        {
            return (int)InvokeExtensionFunction<glGetUniformLocation>(program, name);
        }
        public void GetUniform (uint program, int location, float[] parameters)
        {
            InvokeExtensionFunction<glGetUniformfv>(program, location, parameters);
        }
        public void GetUniform (uint program, int location, int[] parameters)
        {
            InvokeExtensionFunction<glGetUniformiv>(program, location, parameters);
        }
        public void GetVertexAttrib (uint index, uint pname, double[] parameters)
        {
            InvokeExtensionFunction<glGetVertexAttribdv>(index, pname, parameters);
        }
        public void GetVertexAttrib (uint index, uint pname, float[] parameters)
        {
            InvokeExtensionFunction<glGetVertexAttribfv>(index, pname, parameters);
        }
        public void GetVertexAttrib (uint index, uint pname, int[] parameters)
        {
            InvokeExtensionFunction<glGetVertexAttribiv>(index, pname, parameters);
        }
        public void GetVertexAttribPointer(uint index, uint pname, IntPtr pointer)
        {
            InvokeExtensionFunction<glGetVertexAttribPointerv>(index, pname, pointer);
        }
        public bool IsProgram (uint program)
        {
            return (bool)InvokeExtensionFunction<glIsProgram>(program);
        }
        public bool IsShader (uint shader)
        {
            return (bool)InvokeExtensionFunction<glIsShader>(shader);
        }
        public void LinkProgram (uint program)
        {
            InvokeExtensionFunction<glLinkProgram>(program);
        }
       
        public void ShaderSource (uint shader, string source)
        {
            source = source ?? string.Empty;
            InvokeExtensionFunction<glShaderSource>(shader, 1, new[] { source }, new[] { source.Length });

        }

        public static IntPtr StringToPtrAnsi(string str)
        {
            if (string.IsNullOrEmpty(str))
                return IntPtr.Zero;

            byte[] bytes = Encoding.ASCII.GetBytes(str + '\0');
            IntPtr strPtr = Marshal.AllocHGlobal(bytes.Length);
            Marshal.Copy(bytes, 0, strPtr, bytes.Length);

            return strPtr;
        }
        public void UseProgram (uint program)
        {
            InvokeExtensionFunction<glUseProgram>(program);
        }
        public void Uniform1 (int location, float v0)
        {
            InvokeExtensionFunction<glUniform1f>(location, v0);
        }
        public void Uniform2 (int location, float v0, float v1)
        {
            InvokeExtensionFunction<glUniform2f>(location, v0, v1);
        }
        public void Uniform3 (int location, float v0, float v1, float v2)
        {
            InvokeExtensionFunction<glUniform3f>(location, v0, v1, v2);
        }
        public void Uniform4 (int location, float v0, float v1, float v2, float v3)
        {
            InvokeExtensionFunction<glUniform4f>(location, v0, v1, v2, v3);
        }
        public void Uniform1 (int location, int v0)
        {
            InvokeExtensionFunction<glUniform1i>(location, v0);
        }
        public void Uniform2 (int location, int v0, int v1)
        {
            InvokeExtensionFunction<glUniform2i>(location, v0, v1);
        }
        public void Uniform3(int location, int v0, int v1, int v2)
        {
            InvokeExtensionFunction<glUniform3i>(location, v0, v1, v2);
        }
        public void Uniform (int location, int v0, int v1, int v2, int v3)
        {
            InvokeExtensionFunction<glUniform4i>(location, v0, v1, v2, v3);
        }
        public void Uniform1 (int location, int count, float[] value)
        {
            InvokeExtensionFunction<glUniform1fv>(location, count, value);
        }
        public void Uniform2 (int location, int count, float[] value)
        {
            InvokeExtensionFunction<glUniform2fv>(location, count, value);
        }
        public void Uniform3 (int location, int count, float[] value)
        {
            InvokeExtensionFunction<glUniform3fv>(location, count, value);
        }
        public void Uniform4 (int location, int count, float[] value)
        {
            InvokeExtensionFunction<glUniform4fv>(location, count, value);
        }
        public void Uniform1 (int location, int count, int[] value)
        {
            InvokeExtensionFunction<glUniform1iv>(location, count, value);
        }
        public void Uniform2 (int location, int count, int[] value)
        {
            InvokeExtensionFunction<glUniform2iv>(location, count, value);
        }
        public void Uniform3 (int location, int count, int[] value)
        {
            InvokeExtensionFunction<glUniform3iv>(location, count, value);
        }
        public void Uniform4 (int location, int count, int[] value)
        {
            InvokeExtensionFunction<glUniform4iv>(location, count, value);
        }
        public void UniformMatrix2 (int location, int count, bool transpose, float[] value)
        {
            InvokeExtensionFunction<glUniformMatrix2fv>(location, count, transpose, value);
        }
        public void UniformMatrix3 (int location, int count, bool transpose, float[] value)
        {
            InvokeExtensionFunction<glUniformMatrix3fv>(location, count, transpose, value);
        }
        public void UniformMatrix4 (int location, int count, bool transpose, float[] value)
        {
            InvokeExtensionFunction<glUniformMatrix4fv>(location, count, transpose, value);
        }
        public void ValidateProgram (uint program)
        {
            InvokeExtensionFunction<glValidateProgram>(program);
        }
        public void VertexAttrib1 (uint index, double x)
        {
            InvokeExtensionFunction<glVertexAttrib1d>(index, x);
        }
        public void VertexAttrib1 (uint index, double[] v)
        {
            InvokeExtensionFunction<glVertexAttrib1dv>(index, v);
        }
        public void VertexAttrib (uint index, float x)
        {
            InvokeExtensionFunction<glVertexAttrib1f>(index, x);
        }
        public void VertexAttrib1 (uint index, float[] v)
        {
            InvokeExtensionFunction<glVertexAttrib1fv>(index, v);
        }
        public void VertexAttrib (uint index, short x)
        {
            InvokeExtensionFunction<glVertexAttrib1s>(index, x);
        }
        public void VertexAttrib1 (uint index, short[] v)
        {
            InvokeExtensionFunction<glVertexAttrib1sv>(index, v);
        }
        public void VertexAttrib2 (uint index, double x, double y)
        {
            InvokeExtensionFunction<glVertexAttrib2d>(index, x, y);
        }
        public void VertexAttrib2 (uint index, double[] v)
        {
            InvokeExtensionFunction<glVertexAttrib2dv>(index, v);
        }
        public void VertexAttrib2 (uint index, float x, float y)
        {
            InvokeExtensionFunction<glVertexAttrib2f>(index, x, y);
        }
        public void VertexAttrib2 (uint index, float[] v)
        {
            InvokeExtensionFunction<glVertexAttrib2fv>(index, v);
        }
        public void VertexAttrib2 (uint index, short x, short y)
        {
            InvokeExtensionFunction<glVertexAttrib2s>(index, x, y);
        }
        public void VertexAttrib2 (uint index, short[] v)
        {
            InvokeExtensionFunction<glVertexAttrib2sv>(index, v);
        }
        public void VertexAttrib3 (uint index, double x, double y, double z)
        {
            InvokeExtensionFunction<glVertexAttrib3d>(index, x, y, z);
        }
        public void VertexAttrib3 (uint index, double[] v)
        {
            InvokeExtensionFunction<glVertexAttrib3dv>(index, v);
        }
        public void VertexAttrib3 (uint index, float x, float y, float z)
        {
            InvokeExtensionFunction<glVertexAttrib3f>(index, x, y, z);
        }
        public void VertexAttrib3 (uint index, float[] v)
        {
            InvokeExtensionFunction<glVertexAttrib3fv>(index, v);
        }
        public void VertexAttrib3 (uint index, short x, short y, short z)
        {
            InvokeExtensionFunction<glVertexAttrib3s>(index, x, y, z);
        }
        public void VertexAttrib3 (uint index, short[] v)
        {
            InvokeExtensionFunction<glVertexAttrib3sv>(index, v);
        }
        public void VertexAttrib4N (uint index, sbyte[] v)
        {
            InvokeExtensionFunction<glVertexAttrib4Nbv>(index, v);
        }
        public void VertexAttrib4N (uint index, int[] v)
        {
            InvokeExtensionFunction<glVertexAttrib4Niv>(index, v);
        }
        public void VertexAttrib4N (uint index, short[] v)
        {
            InvokeExtensionFunction<glVertexAttrib4Nsv>(index, v);
        }
        public void VertexAttrib4N (uint index, byte x, byte y, byte z, byte w)
        {
            InvokeExtensionFunction<glVertexAttrib4Nub>(index, x, y, z, w);
        }
        public void VertexAttrib4N (uint index, byte[] v)
        {
            InvokeExtensionFunction<glVertexAttrib4Nubv>(index, v);
        }
        public void VertexAttrib4N (uint index, uint[] v)
        {
            InvokeExtensionFunction<glVertexAttrib4Nuiv>(index, v);
        }
        public void VertexAttrib4N (uint index, ushort[] v)
        {
            InvokeExtensionFunction<glVertexAttrib4Nusv>(index, v);
        }
        public void VertexAttrib4 (uint index, sbyte[] v)
        {
            InvokeExtensionFunction<glVertexAttrib4bv>();
        }
        public void VertexAttrib4 (uint index, double x, double y, double z, double w)
        {
            InvokeExtensionFunction<glVertexAttrib4d>(index, x, y, z, w);
        }
        public void VertexAttrib4 (uint index, double[] v)
        {
            InvokeExtensionFunction<glVertexAttrib4dv>(index, v);
        }
        public void VertexAttrib4 (uint index, float x, float y, float z, float w)
        {
            InvokeExtensionFunction<glVertexAttrib4f>(index, x, y, z, w);
        }
        public void VertexAttrib4 (uint index, float[] v)
        {
            InvokeExtensionFunction<glVertexAttrib4fv>(index, v);
        }
        public void VertexAttrib4 (uint index, int[] v)
        {
            InvokeExtensionFunction<glVertexAttrib4iv>(index, v);
        }
        public void VertexAttrib4 (uint index, short x, short y, short z, short w)
        {
            InvokeExtensionFunction<glVertexAttrib4s>(index, x, y, z, w);
        }
        public void VertexAttrib4 (uint index, short[] v)
        {
            InvokeExtensionFunction<glVertexAttrib4sv>(index, v);
        }
        public void VertexAttrib4 (uint index, byte[] v)
        {
            InvokeExtensionFunction<glVertexAttrib4ubv>(index, v);
        }
        public void VertexAttrib4 (uint index, uint[] v)
        {
            InvokeExtensionFunction<glVertexAttrib4uiv>(index, v);
        }
        public void VertexAttrib4 (uint index, ushort[] v)
        {
            InvokeExtensionFunction<glVertexAttrib4usv>(index, v);
        }
        public void VertexAttribPointer (uint index, int size, uint type, bool normalized, int stride, IntPtr pointer)
        {
            InvokeExtensionFunction<glVertexAttribPointer>(index, size, type, normalized, stride, pointer);
        }

        //  Delegates
        private delegate void glBlendEquationSeparate (uint modeRGB, uint modeAlpha);
        private delegate void glDrawBuffers (int n, uint[] bufs);
        private delegate void glStencilOpSeparate (uint face, uint sfail, uint dpfail, uint dppass);
        private delegate void glStencilFuncSeparate (uint face, uint func, int reference, uint mask);
        private delegate void glStencilMaskSeparate (uint face, uint mask);
        private delegate void glAttachShader (uint program, uint shader);
        private delegate void glBindAttribLocation (uint program, uint index, string name);
        private delegate void glCompileShader (uint shader);
        private delegate uint glCreateProgram ();
        private delegate uint glCreateShader (uint type);
        private delegate void glDeleteProgram (uint program);
        private delegate void glDeleteShader (uint shader);
        private delegate void glDetachShader (uint program, uint shader);
        private delegate void glDisableVertexAttribArray (uint index);
        private delegate void glEnableVertexAttribArray (uint index);
        private delegate void glGetActiveAttrib (uint program, uint index, int bufSize, int[] length, int[] size, uint[] type, string name);
        private delegate void glGetActiveUniform (uint program, uint index, int bufSize, int[] length, int[] size, uint[] type, string name);
        private delegate void glGetAttachedShaders (uint program, int maxCount, int[] count, uint[] obj);
        private delegate int glGetAttribLocation (uint program, string name);
        private delegate void glGetProgramiv (uint program, uint pname, int[] parameters);
        private delegate void glGetProgramInfoLog(uint program, int bufSize, IntPtr length, StringBuilder infoLog);
        private delegate void glGetShaderiv (uint shader, uint pname, int[] parameters);
        private delegate void glGetShaderInfoLog (uint shader, int bufSize, IntPtr length, StringBuilder infoLog);
        private delegate void glGetShaderSource (uint shader, int bufSize, IntPtr length, StringBuilder source);
        private delegate int glGetUniformLocation (uint program, string name);
        private delegate void glGetUniformfv (uint program, int location, float[] parameters);
        private delegate void glGetUniformiv (uint program, int location, int[] parameters);
        private delegate void glGetVertexAttribdv (uint index, uint pname, double[] parameters);
        private delegate void glGetVertexAttribfv (uint index, uint pname, float[] parameters);
        private delegate void glGetVertexAttribiv (uint index, uint pname, int[] parameters);
        private delegate void glGetVertexAttribPointerv (uint index, uint pname, IntPtr pointer);
        private delegate bool glIsProgram (uint program);
        private delegate bool glIsShader (uint shader);
        private delegate void glLinkProgram (uint program);
        private delegate void glShaderSource (uint shader, int count, string[] source, int[] length);
        private delegate void glUseProgram (uint program);
        private delegate void glUniform1f (int location, float v0);
        private delegate void glUniform2f (int location, float v0, float v1);
        private delegate void glUniform3f (int location, float v0, float v1, float v2);
        private delegate void glUniform4f (int location, float v0, float v1, float v2, float v3);
        private delegate void glUniform1i (int location, int v0);
        private delegate void glUniform2i (int location, int v0, int v1);
        private delegate void glUniform3i (int location, int v0, int v1, int v2);
        private delegate void glUniform4i (int location, int v0, int v1, int v2, int v3);
        private delegate void glUniform1fv (int location, int count, float[] value);
        private delegate void glUniform2fv (int location, int count, float[] value);
        private delegate void glUniform3fv (int location, int count, float[] value);
        private delegate void glUniform4fv (int location, int count, float[] value);
        private delegate void glUniform1iv (int location, int count, int[] value);
        private delegate void glUniform2iv (int location, int count, int[] value);
        private delegate void glUniform3iv (int location, int count, int[] value);
        private delegate void glUniform4iv (int location, int count, int[] value);
        private delegate void glUniformMatrix2fv (int location, int count, bool transpose, float[] value);
        private delegate void glUniformMatrix3fv (int location, int count, bool transpose, float[] value);
        private delegate void glUniformMatrix4fv (int location, int count, bool transpose, float[] value);
        private delegate void glValidateProgram (uint program);
        private delegate void glVertexAttrib1d (uint index, double x);
        private delegate void glVertexAttrib1dv (uint index, double[] v);
        private delegate void glVertexAttrib1f (uint index, float x);
        private delegate void glVertexAttrib1fv (uint index, float[] v);
        private delegate void glVertexAttrib1s (uint index, short x);
        private delegate void glVertexAttrib1sv (uint index, short[] v);
        private delegate void glVertexAttrib2d (uint index, double x, double y);
        private delegate void glVertexAttrib2dv (uint index, double[] v);
        private delegate void glVertexAttrib2f (uint index, float x, float y);
        private delegate void glVertexAttrib2fv (uint index, float[] v);
        private delegate void glVertexAttrib2s (uint index, short x, short y);
        private delegate void glVertexAttrib2sv (uint index, short[] v);
        private delegate void glVertexAttrib3d (uint index, double x, double y, double z);
        private delegate void glVertexAttrib3dv (uint index, double[] v);
        private delegate void glVertexAttrib3f (uint index, float x, float y, float z);
        private delegate void glVertexAttrib3fv (uint index, float[] v);
        private delegate void glVertexAttrib3s (uint index, short x, short y, short z);
        private delegate void glVertexAttrib3sv (uint index, short[] v);
        private delegate void glVertexAttrib4Nbv (uint index, byte[] v);
        private delegate void glVertexAttrib4Niv (uint index, int[] v);
        private delegate void glVertexAttrib4Nsv (uint index, short[] v);
        private delegate void glVertexAttrib4Nub (uint index, byte x, byte y, byte z, byte w);
        private delegate void glVertexAttrib4Nubv (uint index, byte[] v);
        private delegate void glVertexAttrib4Nuiv (uint index, uint[] v);
        private delegate void glVertexAttrib4Nusv (uint index, short[] v);
        private delegate void glVertexAttrib4bv (uint index, sbyte[] v);
        private delegate void glVertexAttrib4d (uint index, double x, double y, double z, double w);
        private delegate void glVertexAttrib4dv (uint index, double[] v);
        private delegate void glVertexAttrib4f (uint index, float x, float y, float z, float w);
        private delegate void glVertexAttrib4fv (uint index, float[] v);
        private delegate void glVertexAttrib4iv (uint index, int[] v);
        private delegate void glVertexAttrib4s (uint index, short x, short y, short z, short w);
        private delegate void glVertexAttrib4sv (uint index, short[] v);
        private delegate void glVertexAttrib4ubv (uint index, byte[] v);
        private delegate void glVertexAttrib4uiv (uint index, uint[] v);
        private delegate void glVertexAttrib4usv (uint index, ushort[] v);
        private delegate void glVertexAttribPointer (uint index, int size, uint type, bool normalized, int stride, IntPtr pointer);

        //  Constants
        public const uint GL_BLEND_EQUATION_RGB                  = 0x8009;
        public const uint GL_VERTEX_ATTRIB_ARRAY_ENABLED         = 0x8622;
        public const uint GL_VERTEX_ATTRIB_ARRAY_SIZE            = 0x8623;
        public const uint GL_VERTEX_ATTRIB_ARRAY_STRIDE          = 0x8624;
        public const uint GL_VERTEX_ATTRIB_ARRAY_TYPE            = 0x8625;
        public const uint GL_CURRENT_VERTEX_ATTRIB               = 0x8626;
        public const uint GL_VERTEX_PROGRAM_POINT_SIZE           = 0x8642;
        public const uint GL_VERTEX_ATTRIB_ARRAY_POINTER         = 0x8645;
        public const uint GL_STENCIL_BACK_FUNC                   = 0x8800;
        public const uint GL_STENCIL_BACK_FAIL                   = 0x8801;
        public const uint GL_STENCIL_BACK_PASS_DEPTH_FAIL        = 0x8802;
        public const uint GL_STENCIL_BACK_PASS_DEPTH_PASS        = 0x8803;
        public const uint GL_MAX_DRAW_BUFFERS                    = 0x8824;
        public const uint GL_DRAW_BUFFER0                        = 0x8825;
        public const uint GL_DRAW_BUFFER1                        = 0x8826;
        public const uint GL_DRAW_BUFFER2                        = 0x8827;
        public const uint GL_DRAW_BUFFER3                        = 0x8828;
        public const uint GL_DRAW_BUFFER4                        = 0x8829;
        public const uint GL_DRAW_BUFFER5                        = 0x882A;
        public const uint GL_DRAW_BUFFER6                        = 0x882B;
        public const uint GL_DRAW_BUFFER7                        = 0x882C;
        public const uint GL_DRAW_BUFFER8                        = 0x882D;
        public const uint GL_DRAW_BUFFER9                        = 0x882E;
        public const uint GL_DRAW_BUFFER10                       = 0x882F;
        public const uint GL_DRAW_BUFFER11                       = 0x8830;
        public const uint GL_DRAW_BUFFER12                       = 0x8831;
        public const uint GL_DRAW_BUFFER13                       = 0x8832;
        public const uint GL_DRAW_BUFFER14                       = 0x8833;
        public const uint GL_DRAW_BUFFER15                       = 0x8834;
        public const uint GL_BLEND_EQUATION_ALPHA                = 0x883D;
        public const uint GL_MAX_VERTEX_ATTRIBS                  = 0x8869;
        public const uint GL_VERTEX_ATTRIB_ARRAY_NORMALIZED      = 0x886A;
        public const uint GL_MAX_TEXTURE_IMAGE_UNITS             = 0x8872;
        public const uint GL_FRAGMENT_SHADER                     = 0x8B30;
        public const uint GL_VERTEX_SHADER                       = 0x8B31;
        public const uint GL_MAX_FRAGMENT_UNIFORM_COMPONENTS     = 0x8B49;
        public const uint GL_MAX_VERTEX_UNIFORM_COMPONENTS       = 0x8B4A;
        public const uint GL_MAX_VARYING_FLOATS                  = 0x8B4B;
        public const uint GL_MAX_VERTEX_TEXTURE_IMAGE_UNITS      = 0x8B4C;
        public const uint GL_MAX_COMBINED_TEXTURE_IMAGE_UNITS    = 0x8B4D;
        public const uint GL_SHADER_TYPE                         = 0x8B4F;
        public const uint GL_FLOAT_VEC2                          = 0x8B50;
        public const uint GL_FLOAT_VEC3                          = 0x8B51;
        public const uint GL_FLOAT_VEC4                          = 0x8B52;
        public const uint GL_INT_VEC2                            = 0x8B53;
        public const uint GL_INT_VEC3                            = 0x8B54;
        public const uint GL_INT_VEC4                            = 0x8B55;
        public const uint GL_BOOL                                = 0x8B56;
        public const uint GL_BOOL_VEC2                           = 0x8B57;
        public const uint GL_BOOL_VEC3                           = 0x8B58;
        public const uint GL_BOOL_VEC4                           = 0x8B59;
        public const uint GL_FLOAT_MAT2                          = 0x8B5A;
        public const uint GL_FLOAT_MAT3                          = 0x8B5B;
        public const uint GL_FLOAT_MAT4                          = 0x8B5C;
        public const uint GL_SAMPLER_1D                          = 0x8B5D;
        public const uint GL_SAMPLER_2D                          = 0x8B5E;
        public const uint GL_SAMPLER_3D                          = 0x8B5F;
        public const uint GL_SAMPLER_CUBE                        = 0x8B60;
        public const uint GL_SAMPLER_1D_SHADOW                   = 0x8B61;
        public const uint GL_SAMPLER_2D_SHADOW                   = 0x8B62;
        public const uint GL_DELETE_STATUS                       = 0x8B80;
        public const uint GL_COMPILE_STATUS                      = 0x8B81;
        public const uint GL_LINK_STATUS                         = 0x8B82;
        public const uint GL_VALIDATE_STATUS                     = 0x8B83;
        public const uint GL_INFO_LOG_LENGTH                     = 0x8B84;
        public const uint GL_ATTACHED_SHADERS                    = 0x8B85;
        public const uint GL_ACTIVE_UNIFORMS                     = 0x8B86;
        public const uint GL_ACTIVE_UNIFORM_MAX_LENGTH           = 0x8B87;
        public const uint GL_SHADER_SOURCE_LENGTH                = 0x8B88;
        public const uint GL_ACTIVE_ATTRIBUTES                   = 0x8B89;
        public const uint GL_ACTIVE_ATTRIBUTE_MAX_LENGTH         = 0x8B8A;
        public const uint GL_FRAGMENT_SHADER_DERIVATIVE_HINT     = 0x8B8B;
        public const uint GL_SHADING_LANGUAGE_VERSION            = 0x8B8C;
        public const uint GL_CURRENT_PROGRAM                     = 0x8B8D;
        public const uint GL_POINT_SPRITE_COORD_ORIGIN           = 0x8CA0;
        public const uint GL_LOWER_LEFT                          = 0x8CA1;
        public const uint GL_UPPER_LEFT                          = 0x8CA2;
        public const uint GL_STENCIL_BACK_REF                    = 0x8CA3;
        public const uint GL_STENCIL_BACK_VALUE_MASK             = 0x8CA4;
        public const uint GL_STENCIL_BACK_WRITEMASK              = 0x8CA5;
        
        #endregion

        #region OpenGL 2.1

        //  Methods
        public void UniformMatrix2x3(int location, int count, bool transpose, float[] value)
        {
            InvokeExtensionFunction<glUniformMatrix2x3fv>(location, count, transpose, value);
        }
        public void UniformMatrix3x2(int location, int count, bool transpose, float[] value)
        {
            InvokeExtensionFunction<glUniformMatrix3x2fv>(location, count, transpose, value);
        }
        public void UniformMatrix2x4(int location, int count, bool transpose, float[] value)
        {
            InvokeExtensionFunction<glUniformMatrix2x4fv>(location, count, transpose, value);
        }
        public void UniformMatrix4x2(int location, int count, bool transpose, float[] value)
        {
            InvokeExtensionFunction<glUniformMatrix4x2fv>(location, count, transpose, value);
        }
        public void UniformMatrix3x4(int location, int count, bool transpose, float[] value)
        {
            InvokeExtensionFunction<glUniformMatrix3x4fv>(location, count, transpose, value);
        }
        public void UniformMatrix4x3(int location, int count, bool transpose, float[] value)
        {
            InvokeExtensionFunction<glUniformMatrix4x3fv>(location, count, transpose, value);
        }

        //  Delegates
        private delegate void glUniformMatrix2x3fv (int location, int count, bool transpose, float[] value);
        private delegate void glUniformMatrix3x2fv (int location, int count, bool transpose, float[] value);
        private delegate void glUniformMatrix2x4fv (int location, int count, bool transpose, float[] value);
        private delegate void glUniformMatrix4x2fv (int location, int count, bool transpose, float[] value);
        private delegate void glUniformMatrix3x4fv (int location, int count, bool transpose, float[] value);
        private delegate void glUniformMatrix4x3fv (int location, int count, bool transpose, float[] value);

        //  Constants
        public const uint GL_PIXEL_PACK_BUFFER                   = 0x88EB;
        public const uint GL_PIXEL_UNPACK_BUFFER                 = 0x88EC;
        public const uint GL_PIXEL_PACK_BUFFER_BINDING           = 0x88ED;
        public const uint GL_PIXEL_UNPACK_BUFFER_BINDING         = 0x88EF;
        public const uint GL_FLOAT_MAT2x3                        = 0x8B65;
        public const uint GL_FLOAT_MAT2x4                        = 0x8B66;
        public const uint GL_FLOAT_MAT3x2                        = 0x8B67;
        public const uint GL_FLOAT_MAT3x4                        = 0x8B68;
        public const uint GL_FLOAT_MAT4x2                        = 0x8B69;
        public const uint GL_FLOAT_MAT4x3                        = 0x8B6A;
        public const uint GL_SRGB                                = 0x8C40;
        public const uint GL_SRGB8                               = 0x8C41;
        public const uint GL_SRGB_ALPHA                          = 0x8C42;
        public const uint GL_SRGB8_ALPHA8                        = 0x8C43;
        public const uint GL_COMPRESSED_SRGB                     = 0x8C48;
        public const uint GL_COMPRESSED_SRGB_ALPHA               = 0x8C49;
       
        #endregion

        #region OpenGL 3.0
        
        //  Methods
        public void ColorMask(uint index, bool r, bool g, bool b, bool a)
        {
            InvokeExtensionFunction<glColorMaski>(index, r, g, b, a);
        }
        public void GetBoolean(uint target, uint index, bool[] data)
        {
            InvokeExtensionFunction<glGetBooleani_v>(target, index, data);
        }
        public void GetInteger(uint target, uint index, int[] data)
        {
            InvokeExtensionFunction<glGetIntegeri_v>(target, index, data);
        }
        public void Enable(uint target, uint index)
        {
            InvokeExtensionFunction<glEnablei>(target, index);
        }
        public void Disable(uint target, uint index)
        {
            InvokeExtensionFunction<glDisablei>(target, index);
        }
        public bool IsEnabled(uint target, uint index)
        {
            return (bool)InvokeExtensionFunction<glIsEnabledi>(target, index);
        }
        public void BeginTransformFeedback(uint primitiveMode)
        {
            InvokeExtensionFunction<glBeginTransformFeedback>(primitiveMode);
        }
        public void EndTransformFeedback()
        {
            InvokeExtensionFunction<glEndTransformFeedback>();
        }
        public void BindBufferRange(uint target, uint index, uint buffer, int offset, int size)
        {
            InvokeExtensionFunction<glBindBufferRange>(target, index, buffer, offset, size);
        }
        public void BindBufferBase(uint target, uint index, uint buffer)
        {
            InvokeExtensionFunction<glBindBufferBase>(target, index, buffer);
        }
        public void TransformFeedbackVaryings(uint program, int count, string[] varyings, uint bufferMode)
        {
            InvokeExtensionFunction<glTransformFeedbackVaryings>(program, count, varyings, bufferMode);
        }
        public void GetTransformFeedbackVarying(uint program, uint index, int bufSize, int[] length, int[] size, uint[] type, string name)
        {
            InvokeExtensionFunction<glGetTransformFeedbackVarying>(program, index, bufSize, length, size, type, name);
        }
        public void ClampColor(uint target, uint clamp)
        {
            InvokeExtensionFunction<glClampColor>(target, clamp);
        }
        public void BeginConditionalRender(uint id, uint mode)
        {
            InvokeExtensionFunction<glBeginConditionalRender>(id, mode);
        }
        public void EndConditionalRender()
        {
            InvokeExtensionFunction<glEndConditionalRender>();
        }
        public void VertexAttribIPointer(uint index, int size, uint type, int stride, IntPtr pointer)
        {
            InvokeExtensionFunction<glVertexAttribIPointer>(index, size, type, stride, pointer);
        }
        public void GetVertexAttribI(uint index, uint pname, int[] parameters)
        {
            InvokeExtensionFunction<glGetVertexAttribIiv>(index, pname, parameters);
        }
        public void GetVertexAttribI(uint index, uint pname, uint[] parameters)
        {
            InvokeExtensionFunction<glGetVertexAttribIuiv>(index, pname, parameters);
        }
        public void VertexAttribI1(uint index, int x)
        {
            InvokeExtensionFunction<glVertexAttribI1i>(index, x);
        }
        public void VertexAttribI2(uint index, int x, int y)
        {
            InvokeExtensionFunction<glVertexAttribI2i>(index, x, y);
        }
        public void VertexAttribI3(uint index, int x, int y, int z)
        {
            InvokeExtensionFunction<glVertexAttribI3i>(index, x, y, z);
        }
        public void VertexAttribI4(uint index, int x, int y, int z, int w)
        {
            InvokeExtensionFunction<glVertexAttribI4i>(index, x, y, z, w);
        }
        public void VertexAttribI1(uint index, uint x)
        {
            InvokeExtensionFunction<glVertexAttribI1ui>(index, x);
        }
        public void VertexAttribI2(uint index, uint x, uint y)
        {
            InvokeExtensionFunction<glVertexAttribI2ui>(index, x, y);
        }
        public void VertexAttribI3(uint index, uint x, uint y, uint z)
        {
            InvokeExtensionFunction<glVertexAttribI3ui>(index, x, y, z);
        }
        public void VertexAttribI4(uint index, uint x, uint y, uint z, uint w)
        {
            InvokeExtensionFunction<glVertexAttribI4ui>(index, x, y, z);
        }
        public void VertexAttribI1(uint index, int[] v)
        {
            InvokeExtensionFunction<glVertexAttribI1iv>(index, v);
        }
        public void VertexAttribI2(uint index, int[] v)
        {
            InvokeExtensionFunction<glVertexAttribI2iv>(index, v);
        }
        public void VertexAttribI3(uint index, int[] v)
        {
            InvokeExtensionFunction<glVertexAttribI3iv>(index, v);
        }
        public void VertexAttribI4(uint index, int[] v)
        {
            InvokeExtensionFunction<glVertexAttribI4iv>(index, v);
        }
        public void VertexAttribI1(uint index, uint[] v)
        {
            InvokeExtensionFunction<glVertexAttribI1uiv>(index, v);
        }
        public void VertexAttribI2(uint index, uint[] v)
        {
            InvokeExtensionFunction<glVertexAttribI2uiv>(index, v);
        }
        public void VertexAttribI3(uint index, uint[] v)
        {
            InvokeExtensionFunction<glVertexAttribI3uiv>(index, v);
        }
        public void VertexAttribI4(uint index, uint[] v)
        {
            InvokeExtensionFunction<glVertexAttribI4uiv>(index, v);
        }
        public void VertexAttribI4(uint index, sbyte[] v)
        {
            InvokeExtensionFunction<glVertexAttribI4bv>(index, v);
        }
        public void VertexAttribI4(uint index, short[] v)
        {
            InvokeExtensionFunction<glVertexAttribI4sv>(index, v);
        }
        public void VertexAttribI4(uint index, byte[] v)
        {
            InvokeExtensionFunction<glVertexAttribI4ubv>(index, v);
        }
        public void VertexAttribI4(uint index, ushort[] v)
        {
            InvokeExtensionFunction<glVertexAttribI4usv>(index, v);
        }
        public void GetUniform(uint program, int location, uint[] parameters)
        {
            InvokeExtensionFunction<glGetUniformuiv>(program, location, parameters);
        }
        public void BindFragDataLocation(uint program, uint color, string name)
        {
            InvokeExtensionFunction<glBindFragDataLocation>(program, color, name);
        }
        public int GetFragDataLocation(uint program, string name)
        {
            return (int)InvokeExtensionFunction<glGetFragDataLocation>(program, name);
        }
        public void Uniform1(int location, uint v0)
        {
            InvokeExtensionFunction<glUniform1ui>(location, v0);
        }
        public void Uniform2(int location, uint v0, uint v1)
        {
            InvokeExtensionFunction<glUniform2ui>(location, v0, v1);
        }
        public void Uniform3(int location, uint v0, uint v1, uint v2)
        {
            InvokeExtensionFunction<glUniform3ui>(location, v0, v1, v2);
        }
        public void Uniform4(int location, uint v0, uint v1, uint v2, uint v3)
        {
            InvokeExtensionFunction<glUniform4ui>(location, v0, v1, v2, v3);
        }
        public void Uniform1(int location, int count, uint[] value)
        {
            InvokeExtensionFunction<glUniform1uiv>(location, count, value);
        }
        public void Uniform2(int location, int count, uint[] value)
        {
            InvokeExtensionFunction<glUniform2uiv>(location, count, value);
        }
        public void Uniform3(int location, int count, uint[] value)
        {
            InvokeExtensionFunction<glUniform3uiv>(location, count, value);
        }
        public void Uniform4(int location, int count, uint[] value)
        {
            InvokeExtensionFunction<glUniform4uiv>(location, count, value);
        }
        public void TexParameterI(uint target, uint pname, int[] parameters)
        {
            InvokeExtensionFunction<glTexParameterIiv>(target, pname, parameters);
        }
        public void TexParameterI(uint target, uint pname, uint[] parameters)
        {
            InvokeExtensionFunction<glTexParameterIuiv>(target, pname, parameters);
        }
        public void GetTexParameterI(uint target, uint pname, int[] parameters)
        {
            InvokeExtensionFunction<glGetTexParameterIiv>(target, pname, parameters);
        }
        public void GetTexParameterI(uint target, uint pname, uint[] parameters)
        {
            InvokeExtensionFunction<glGetTexParameterIuiv>(target, pname, parameters);
        }
        public void ClearBuffer(uint buffer, int drawbuffer, int[] value)
        {
            InvokeExtensionFunction<glClearBufferiv>(buffer, drawbuffer, value);
        }
        public void ClearBuffer(uint buffer, int drawbuffer, uint[] value)
        {
            InvokeExtensionFunction<glClearBufferuiv>(buffer, drawbuffer, value);
        }
        public void ClearBuffer(uint buffer, int drawbuffer, float[] value)
        {
            InvokeExtensionFunction<glClearBufferfv>(buffer, drawbuffer, value);
        }
        public void ClearBuffer(uint buffer, int drawbuffer, float depth, int stencil)
        {
            InvokeExtensionFunction<glClearBufferfi>(buffer, drawbuffer, depth, stencil);
        }
        public string GetString(uint name, uint index)
        {
            return (string)InvokeExtensionFunction<glGetStringi>(name, index);
        }

        //  Delegates
        private delegate void glColorMaski (uint index, bool r, bool g, bool b, bool a);
        private delegate void glGetBooleani_v (uint target, uint index, bool[] data);
        private delegate void glGetIntegeri_v (uint target, uint index, int[] data);
        private delegate void glEnablei (uint target, uint index);
        private delegate void glDisablei (uint target, uint index);
        private delegate bool glIsEnabledi (uint target, uint index);
        private delegate void glBeginTransformFeedback (uint primitiveMode);
        private delegate void glEndTransformFeedback ();
        private delegate void glBindBufferRange (uint target, uint index, uint buffer, int offset, int size);
        private delegate void glBindBufferBase (uint target, uint index, uint buffer);
        private delegate void glTransformFeedbackVaryings (uint program, int count, string[] varyings, uint bufferMode);
        private delegate void glGetTransformFeedbackVarying (uint program, uint index, int bufSize, int[] length, int[] size, uint[] type, string name);
        private delegate void glClampColor (uint target, uint clamp);
        private delegate void glBeginConditionalRender (uint id, uint mode);
        private delegate void glEndConditionalRender ();
        private delegate void glVertexAttribIPointer (uint index, int size, uint type, int stride, IntPtr pointer);
        private delegate void glGetVertexAttribIiv (uint index, uint pname, int[] parameters);
        private delegate void glGetVertexAttribIuiv (uint index, uint pname, uint[] parameters);
        private delegate void glVertexAttribI1i (uint index, int x);
        private delegate void glVertexAttribI2i (uint index, int x, int y);
        private delegate void glVertexAttribI3i (uint index, int x, int y, int z);
        private delegate void glVertexAttribI4i (uint index, int x, int y, int z, int w);
        private delegate void glVertexAttribI1ui (uint index, uint x);
        private delegate void glVertexAttribI2ui (uint index, uint x, uint y);
        private delegate void glVertexAttribI3ui (uint index, uint x, uint y, uint z);
        private delegate void glVertexAttribI4ui (uint index, uint x, uint y, uint z, uint w);
        private delegate void glVertexAttribI1iv (uint index, int[] v);
        private delegate void glVertexAttribI2iv (uint index, int[] v);
        private delegate void glVertexAttribI3iv (uint index, int[] v);
        private delegate void glVertexAttribI4iv (uint index, int[] v);
        private delegate void glVertexAttribI1uiv (uint index, uint[] v);
        private delegate void glVertexAttribI2uiv (uint index, uint[] v);
        private delegate void glVertexAttribI3uiv (uint index, uint[] v);
        private delegate void glVertexAttribI4uiv (uint index, uint[] v);
        private delegate void glVertexAttribI4bv (uint index, sbyte[] v);
        private delegate void glVertexAttribI4sv (uint index, short[] v);
        private delegate void glVertexAttribI4ubv (uint index, byte[] v);
        private delegate void glVertexAttribI4usv (uint index, ushort[] v);
        private delegate void glGetUniformuiv (uint program, int location, uint[] parameters);
        private delegate void glBindFragDataLocation (uint program, uint color, string name);
        private delegate int glGetFragDataLocation (uint program, string name);
        private delegate void glUniform1ui (int location, uint v0);
        private delegate void glUniform2ui (int location, uint v0, uint v1);
        private delegate void glUniform3ui (int location, uint v0, uint v1, uint v2);
        private delegate void glUniform4ui (int location, uint v0, uint v1, uint v2, uint v3);
        private delegate void glUniform1uiv (int location, int count, uint[] value);
        private delegate void glUniform2uiv (int location, int count, uint[] value);
        private delegate void glUniform3uiv (int location, int count, uint[] value);
        private delegate void glUniform4uiv (int location, int count, uint[] value);
        private delegate void glTexParameterIiv (uint target, uint pname, int[] parameters);
        private delegate void glTexParameterIuiv (uint target, uint pname, uint[] parameters);
        private delegate void glGetTexParameterIiv (uint target, uint pname, int[] parameters);
        private delegate void glGetTexParameterIuiv (uint target, uint pname, uint[] parameters);
        private delegate void glClearBufferiv (uint buffer, int drawbuffer, int[] value);
        private delegate void glClearBufferuiv (uint buffer, int drawbuffer, uint[] value);
        private delegate void glClearBufferfv (uint buffer, int drawbuffer, float[] value);
        private delegate void glClearBufferfi (uint buffer, int drawbuffer, float depth, int stencil);
        private delegate string glGetStringi (uint name, uint index);

        //  Constants
        public const uint GL_COMPARE_REF_TO_TEXTURE                        = 0x884E;
        public const uint GL_CLIP_DISTANCE0                                = 0x3000;
        public const uint GL_CLIP_DISTANCE1                                = 0x3001;
        public const uint GL_CLIP_DISTANCE2                                = 0x3002;
        public const uint GL_CLIP_DISTANCE3                                = 0x3003;
        public const uint GL_CLIP_DISTANCE4                                = 0x3004;
        public const uint GL_CLIP_DISTANCE5                                = 0x3005;
        public const uint GL_CLIP_DISTANCE6                                = 0x3006;
        public const uint GL_CLIP_DISTANCE7                                = 0x3007;
        public const uint GL_MAX_CLIP_DISTANCES                            = 0x0D32;
        public const uint GL_MAJOR_VERSION                                 = 0x821B;
        public const uint GL_MINOR_VERSION                                 = 0x821C;
        public const uint GL_NUM_EXTENSIONS                                = 0x821D;
        public const uint GL_CONTEXT_FLAGS                                 = 0x821E;
        public const uint GL_DEPTH_BUFFER                                  = 0x8223;
        public const uint GL_STENCIL_BUFFER                                = 0x8224;
        public const uint GL_COMPRESSED_RED                                = 0x8225;
        public const uint GL_COMPRESSED_RG                                 = 0x8226;
        public const uint GL_CONTEXT_FLAG_FORWARD_COMPATIBLE_BIT           = 0x0001;
        public const uint GL_RGBA32F                                       = 0x8814;
        public const uint GL_RGB32F                                        = 0x8815;
        public const uint GL_RGBA16F                                       = 0x881A;
        public const uint GL_RGB16F                                        = 0x881B;
        public const uint GL_VERTEX_ATTRIB_ARRAY_INTEGER                   = 0x88FD;
        public const uint GL_MAX_ARRAY_TEXTURE_LAYERS                      = 0x88FF;
        public const uint GL_MIN_PROGRAM_TEXEL_OFFSET                      = 0x8904;
        public const uint GL_MAX_PROGRAM_TEXEL_OFFSET                      = 0x8905;
        public const uint GL_CLAMP_READ_COLOR                              = 0x891C;
        public const uint GL_FIXED_ONLY                                    = 0x891D;
        public const uint GL_MAX_VARYING_COMPONENTS                        = 0x8B4B;
        public const uint GL_TEXTURE_1D_ARRAY                              = 0x8C18;
        public const uint GL_PROXY_TEXTURE_1D_ARRAY                        = 0x8C19;
        public const uint GL_TEXTURE_2D_ARRAY                              = 0x8C1A;
        public const uint GL_PROXY_TEXTURE_2D_ARRAY                        = 0x8C1B;
        public const uint GL_TEXTURE_BINDING_1D_ARRAY                      = 0x8C1C;
        public const uint GL_TEXTURE_BINDING_2D_ARRAY                      = 0x8C1D;
        public const uint GL_R11F_G11F_B10F                                = 0x8C3A;
        public const uint GL_UNSIGNED_INT_10F_11F_11F_REV                  = 0x8C3B;
        public const uint GL_RGB9_E5                                       = 0x8C3D;
        public const uint GL_UNSIGNED_INT_5_9_9_9_REV                      = 0x8C3E;
        public const uint GL_TEXTURE_SHARED_SIZE                           = 0x8C3F;
        public const uint GL_TRANSFORM_FEEDBACK_VARYING_MAX_LENGTH         = 0x8C76;
        public const uint GL_TRANSFORM_FEEDBACK_BUFFER_MODE                = 0x8C7F;
        public const uint GL_MAX_TRANSFORM_FEEDBACK_SEPARATE_COMPONENTS    = 0x8C80;
        public const uint GL_TRANSFORM_FEEDBACK_VARYINGS                   = 0x8C83;
        public const uint GL_TRANSFORM_FEEDBACK_BUFFER_START               = 0x8C84;
        public const uint GL_TRANSFORM_FEEDBACK_BUFFER_SIZE                = 0x8C85;
        public const uint GL_PRIMITIVES_GENERATED                          = 0x8C87;
        public const uint GL_TRANSFORM_FEEDBACK_PRIMITIVES_WRITTEN         = 0x8C88;
        public const uint GL_RASTERIZER_DISCARD                            = 0x8C89;
        public const uint GL_MAX_TRANSFORM_FEEDBACK_INTERLEAVED_COMPONENTS = 0x8C8A;
        public const uint GL_MAX_TRANSFORM_FEEDBACK_SEPARATE_ATTRIBS       = 0x8C8B;
        public const uint GL_INTERLEAVED_ATTRIBS                           = 0x8C8C;
        public const uint GL_SEPARATE_ATTRIBS                              = 0x8C8D;
        public const uint GL_TRANSFORM_FEEDBACK_BUFFER                     = 0x8C8E;
        public const uint GL_TRANSFORM_FEEDBACK_BUFFER_BINDING             = 0x8C8F;
        public const uint GL_RGBA32UI                                      = 0x8D70;
        public const uint GL_RGB32UI                                       = 0x8D71;
        public const uint GL_RGBA16UI                                      = 0x8D76;
        public const uint GL_RGB16UI                                       = 0x8D77;
        public const uint GL_RGBA8UI                                       = 0x8D7C;
        public const uint GL_RGB8UI                                        = 0x8D7D;
        public const uint GL_RGBA32I                                       = 0x8D82;
        public const uint GL_RGB32I                                        = 0x8D83;
        public const uint GL_RGBA16I                                       = 0x8D88;
        public const uint GL_RGB16I                                        = 0x8D89;
        public const uint GL_RGBA8I                                        = 0x8D8E;
        public const uint GL_RGB8I                                         = 0x8D8F;
        public const uint GL_RED_INTEGER                                   = 0x8D94;
        public const uint GL_GREEN_INTEGER                                 = 0x8D95;
        public const uint GL_BLUE_INTEGER                                  = 0x8D96;
        public const uint GL_RGB_INTEGER                                   = 0x8D98;
        public const uint GL_RGBA_INTEGER                                  = 0x8D99;
        public const uint GL_BGR_INTEGER                                   = 0x8D9A;
        public const uint GL_BGRA_INTEGER                                  = 0x8D9B;
        public const uint GL_SAMPLER_1D_ARRAY                              = 0x8DC0;
        public const uint GL_SAMPLER_2D_ARRAY                              = 0x8DC1;
        public const uint GL_SAMPLER_1D_ARRAY_SHADOW                       = 0x8DC3;
        public const uint GL_SAMPLER_2D_ARRAY_SHADOW                       = 0x8DC4;
        public const uint GL_SAMPLER_CUBE_SHADOW                           = 0x8DC5;
        public const uint GL_UNSIGNED_INT_VEC2                             = 0x8DC6;
        public const uint GL_UNSIGNED_INT_VEC3                             = 0x8DC7;
        public const uint GL_UNSIGNED_INT_VEC4                             = 0x8DC8;
        public const uint GL_INT_SAMPLER_1D                                = 0x8DC9;
        public const uint GL_INT_SAMPLER_2D                                = 0x8DCA;
        public const uint GL_INT_SAMPLER_3D                                = 0x8DCB;
        public const uint GL_INT_SAMPLER_CUBE                              = 0x8DCC;
        public const uint GL_INT_SAMPLER_1D_ARRAY                          = 0x8DCE;
        public const uint GL_INT_SAMPLER_2D_ARRAY                          = 0x8DCF;
        public const uint GL_UNSIGNED_INT_SAMPLER_1D                       = 0x8DD1;
        public const uint GL_UNSIGNED_INT_SAMPLER_2D                       = 0x8DD2;
        public const uint GL_UNSIGNED_INT_SAMPLER_3D                       = 0x8DD3;
        public const uint GL_UNSIGNED_INT_SAMPLER_CUBE                     = 0x8DD4;
        public const uint GL_UNSIGNED_INT_SAMPLER_1D_ARRAY                 = 0x8DD6;
        public const uint GL_UNSIGNED_INT_SAMPLER_2D_ARRAY                 = 0x8DD7;
        public const uint GL_QUERY_WAIT                                    = 0x8E13;
        public const uint GL_QUERY_NO_WAIT                                 = 0x8E14;
        public const uint GL_QUERY_BY_REGION_WAIT                          = 0x8E15;
        public const uint GL_QUERY_BY_REGION_NO_WAIT                       = 0x8E16;
        public const uint GL_BUFFER_ACCESS_FLAGS                           = 0x911F;
        public const uint GL_BUFFER_MAP_LENGTH                             = 0x9120;
        public const uint GL_BUFFER_MAP_OFFSET                             = 0x9121;
     
        #endregion

        #region OpenGL 3.1

        //  Methods
        public void DrawArraysInstanced(uint mode, int first, int count, int primcount)
        {
            InvokeExtensionFunction<glDrawArraysInstanced>(mode, first, count, primcount);
        }
        public void DrawElementsInstanced(uint mode, int count, uint type, IntPtr indices, int primcount)
        {
            InvokeExtensionFunction<glDrawElementsInstanced>(mode, count, type, indices, primcount);
        }
        public void TexBuffer(uint target, uint internalformat, uint buffer)
        {
            InvokeExtensionFunction<glTexBuffer>(target, internalformat, buffer);
        }
        public void PrimitiveRestartIndex(uint index)
        {
            InvokeExtensionFunction<glPrimitiveRestartIndex>(index);
        }

        //  Delegates
        private delegate void glDrawArraysInstanced (uint mode, int first, int count, int primcount);
        private delegate void glDrawElementsInstanced (uint mode, int count, uint type, IntPtr indices, int primcount);
        private delegate void glTexBuffer (uint target, uint internalformat, uint buffer);
        private delegate void glPrimitiveRestartIndex (uint index);

        //  Constants
        public const uint GL_SAMPLER_2D_RECT                       = 0x8B63;
        public const uint GL_SAMPLER_2D_RECT_SHADOW                = 0x8B64;
        public const uint GL_SAMPLER_BUFFER                        = 0x8DC2;
        public const uint GL_INT_SAMPLER_2D_RECT                   = 0x8DCD;
        public const uint GL_INT_SAMPLER_BUFFER                    = 0x8DD0;
        public const uint GL_UNSIGNED_INT_SAMPLER_2D_RECT          = 0x8DD5;
        public const uint GL_UNSIGNED_INT_SAMPLER_BUFFER           = 0x8DD8;
        public const uint GL_TEXTURE_BUFFER                        = 0x8C2A;
        public const uint GL_MAX_TEXTURE_BUFFER_SIZE               = 0x8C2B;
        public const uint GL_TEXTURE_BINDING_BUFFER                = 0x8C2C;
        public const uint GL_TEXTURE_BUFFER_DATA_STORE_BINDING     = 0x8C2D;
        public const uint GL_TEXTURE_BUFFER_FORMAT                 = 0x8C2E;
        public const uint GL_TEXTURE_RECTANGLE                     = 0x84F5;
        public const uint GL_TEXTURE_BINDING_RECTANGLE             = 0x84F6;
        public const uint GL_PROXY_TEXTURE_RECTANGLE               = 0x84F7;
        public const uint GL_MAX_RECTANGLE_TEXTURE_SIZE            = 0x84F8;
        public const uint GL_RED_SNORM                             = 0x8F90;
        public const uint GL_RG_SNORM                              = 0x8F91;
        public const uint GL_RGB_SNORM                             = 0x8F92;
        public const uint GL_RGBA_SNORM                            = 0x8F93;
        public const uint GL_R8_SNORM                              = 0x8F94;
        public const uint GL_RG8_SNORM                             = 0x8F95;
        public const uint GL_RGB8_SNORM                            = 0x8F96;
        public const uint GL_RGBA8_SNORM                           = 0x8F97;
        public const uint GL_R16_SNORM                             = 0x8F98;
        public const uint GL_RG16_SNORM                            = 0x8F99;
        public const uint GL_RGB16_SNORM                           = 0x8F9A;
        public const uint GL_RGBA16_SNORM                          = 0x8F9B;
        public const uint GL_SIGNED_NORMALIZED                     = 0x8F9C;
        public const uint GL_PRIMITIVE_RESTART                     = 0x8F9D;
        public const uint GL_PRIMITIVE_RESTART_INDEX               = 0x8F9E;
        
        #endregion

        #region OpenGL 3.2

        //  Methods
        public void GetInteger64(uint target, uint index, Int64[] data)
        {
            InvokeExtensionFunction<glGetInteger64i_v>(target, index, data);
        }
        public void GetBufferParameteri64(uint target, uint pname, Int64[] parameters)
        {
            InvokeExtensionFunction<glGetBufferParameteri64v>(target, pname, parameters);
        }
        public void FramebufferTexture(uint target, uint attachment, uint texture, int level)
        {
            InvokeExtensionFunction<glFramebufferTexture>(target, attachment, texture, level);
        }

        //  Delegates
        private delegate void glGetInteger64i_v (uint target, uint index, Int64[] data);
        private delegate void glGetBufferParameteri64v (uint target, uint pname, Int64[] parameters);
        private delegate void glFramebufferTexture (uint target, uint attachment, uint texture, int level);

        //  Constants
        public const uint GL_CONTEXT_CORE_PROFILE_BIT                  = 0x00000001;
        public const uint GL_CONTEXT_COMPATIBILITY_PROFILE_BIT         = 0x00000002;
        public const uint GL_LINES_ADJACENCY                           = 0x000A;
        public const uint GL_LINE_STRIP_ADJACENCY                      = 0x000B;
        public const uint GL_TRIANGLES_ADJACENCY                       = 0x000C;
        public const uint GL_TRIANGLE_STRIP_ADJACENCY                  = 0x000D;
        public const uint GL_PROGRAM_POINT_SIZE                        = 0x8642;
        public const uint GL_MAX_GEOMETRY_TEXTURE_IMAGE_UNITS          = 0x8C29;
        public const uint GL_FRAMEBUFFER_ATTACHMENT_LAYERED            = 0x8DA7;
        public const uint GL_FRAMEBUFFER_INCOMPLETE_LAYER_TARGETS      = 0x8DA8;
        public const uint GL_GEOMETRY_SHADER                           = 0x8DD9;
        public const uint GL_GEOMETRY_VERTICES_OUT                     = 0x8916;
        public const uint GL_GEOMETRY_INPUT_TYPE                       = 0x8917;
        public const uint GL_GEOMETRY_OUTPUT_TYPE                      = 0x8918;
        public const uint GL_MAX_GEOMETRY_UNIFORM_COMPONENTS           = 0x8DDF;
        public const uint GL_MAX_GEOMETRY_OUTPUT_VERTICES              = 0x8DE0;
        public const uint GL_MAX_GEOMETRY_TOTAL_OUTPUT_COMPONENTS      = 0x8DE1;
        public const uint GL_MAX_VERTEX_OUTPUT_COMPONENTS              = 0x9122;
        public const uint GL_MAX_GEOMETRY_INPUT_COMPONENTS             = 0x9123;
        public const uint GL_MAX_GEOMETRY_OUTPUT_COMPONENTS            = 0x9124;
        public const uint GL_MAX_FRAGMENT_INPUT_COMPONENTS             = 0x9125;
        public const uint GL_CONTEXT_PROFILE_MASK                      = 0x9126;
        
        #endregion

        #region OpenGL 3.3

        //  Methods
        public void VertexAttribDivisor(uint index, uint divisor)
        {
            InvokeExtensionFunction<glVertexAttribDivisor>(index, divisor);
        }
        
        //  Delegates
        private delegate void glVertexAttribDivisor (uint index, uint divisor);

        //  Constants
        public const uint GL_VERTEX_ATTRIB_ARRAY_DIVISOR             = 0x88FE;
        
        #endregion

        #region OpenGL 4.0

        //  Methods        
        public void MinSampleShading(float value)
        {
            InvokeExtensionFunction<glMinSampleShading>(value);
        }
        public void BlendEquation(uint buf, uint mode)
        {
            InvokeExtensionFunction<glBlendEquationi>(buf, mode);
        }
        public void BlendEquationSeparate(uint buf, uint modeRGB, uint modeAlpha)
        {
            InvokeExtensionFunction<glBlendEquationSeparatei>(buf, modeRGB, modeAlpha);
        }
        public void BlendFunc(uint buf, uint src, uint dst)
        {
            InvokeExtensionFunction<glBlendFunci>(buf, src, dst);
        }
        public void BlendFuncSeparate(uint buf, uint srcRGB, uint dstRGB, uint srcAlpha, uint dstAlpha)
        {
            InvokeExtensionFunction<glBlendFuncSeparatei>(buf, srcRGB, dstRGB, srcAlpha, dstAlpha);
        }

        //  Delegates        
        private delegate void glMinSampleShading (float value);
        private delegate void glBlendEquationi (uint buf, uint mode);
        private delegate void glBlendEquationSeparatei (uint buf, uint modeRGB, uint modeAlpha);
        private delegate void glBlendFunci (uint buf, uint src, uint dst);
        private delegate void glBlendFuncSeparatei (uint buf, uint srcRGB, uint dstRGB, uint srcAlpha, uint dstAlpha);

        //  Constants
        public const uint GL_SAMPLE_SHADING                        = 0x8C36;
        public const uint GL_MIN_SAMPLE_SHADING_VALUE              = 0x8C37;
        public const uint GL_MIN_PROGRAM_TEXTURE_GATHER_OFFSET     = 0x8E5E;
        public const uint GL_MAX_PROGRAM_TEXTURE_GATHER_OFFSET     = 0x8E5F;
        public const uint GL_TEXTURE_CUBE_MAP_ARRAY                = 0x9009;
        public const uint GL_TEXTURE_BINDING_CUBE_MAP_ARRAY        = 0x900A;
        public const uint GL_PROXY_TEXTURE_CUBE_MAP_ARRAY          = 0x900B;
        public const uint GL_SAMPLER_CUBE_MAP_ARRAY                = 0x900C;
        public const uint GL_SAMPLER_CUBE_MAP_ARRAY_SHADOW         = 0x900D;
        public const uint GL_INT_SAMPLER_CUBE_MAP_ARRAY            = 0x900E;
        public const uint GL_UNSIGNED_INT_SAMPLER_CUBE_MAP_ARRAY   = 0x900F;

        #endregion

        #region GL_EXT_texture3D

        /// <summary>
        /// Specify a three-dimensional texture subimage.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="level">The level.</param>
        /// <param name="internalformat">The internalformat.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="depth">The depth.</param>
        /// <param name="border">The border.</param>
        /// <param name="format">The format.</param>
        /// <param name="type">The type.</param>
        /// <param name="pixels">The pixels.</param>
        public void TexImage3DEXT (uint target, int level, uint internalformat, uint width, 
            uint height, uint depth, int border, uint format, uint type, IntPtr pixels)
        {
            InvokeExtensionFunction<glTexImage3DEXT>(target, level, internalformat, width, height, depth, border, format, type, pixels);
        }

        /// <summary>
        /// Texes the sub image3 DEXT.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="level">The level.</param>
        /// <param name="xoffset">The xoffset.</param>
        /// <param name="yoffset">The yoffset.</param>
        /// <param name="zoffset">The zoffset.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="depth">The depth.</param>
        /// <param name="format">The format.</param>
        /// <param name="type">The type.</param>
        /// <param name="pixels">The pixels.</param>
        public void TexSubImage3DEXT(uint target, int level, int xoffset, int yoffset, int zoffset,
            uint width, uint height, uint depth, uint format, uint type, IntPtr pixels)
        {
            InvokeExtensionFunction<glTexSubImage3DEXT>(target, level, xoffset, yoffset, zoffset, width, height, depth, format, type, pixels);
        }

        private delegate void glTexImage3DEXT(uint target, int level, uint internalformat, uint width,
            uint height, uint depth, int border, uint format, uint type, IntPtr pixels);
        private delegate void glTexSubImage3DEXT(uint target, int level, int xoffset, int yoffset, int zoffset,
            uint width, uint height, uint depth, uint format, uint type, IntPtr pixels);

        #endregion

        #region GL_EXT_bgra

        public const uint GL_BGR_EXT = 0x80E0;
        public const uint GL_BGRA_EXT = 0x80E1;

        #endregion

        #region GL_EXT_packed_pixels

        public const uint GL_UNSIGNED_BYTE_3_3_2_EXT = 0x8032;
        public const uint GL_UNSIGNED_SHORT_4_4_4_4_EXT = 0x8033;
        public const uint GL_UNSIGNED_SHORT_5_5_5_1_EXT = 0x8034;
        public const uint GL_UNSIGNED_INT_8_8_8_8_EXT = 0x8035;
        public const uint GL_UNSIGNED_INT_10_10_10_2_EXT = 0x8036;

        #endregion

        #region GL_EXT_rescale_normal

        public const uint GL_RESCALE_NORMAL_EXT = 0x803A;

        #endregion

        #region GL_EXT_separate_specular_color

        public const uint GL_LIGHT_MODEL_COLOR_CONTROL_EXT = 0x81F8;
        public const uint GL_SINGLE_COLOR_EXT = 0x81F9;
        public const uint GL_SEPARATE_SPECULAR_COLOR_EXT = 0x81FA;

        #endregion

        #region GL_SGIS_texture_edge_clamp

        public const uint GL_CLAMP_TO_EDGE_SGIS = 0x812F;

        #endregion

        #region GL_SGIS_texture_lod

        public const uint GL_TEXTURE_MIN_LOD_SGIS = 0x813A;
        public const uint GL_TEXTURE_MAX_LOD_SGIS = 0x813B;
        public const uint GL_TEXTURE_BASE_LEVEL_SGIS = 0x813C;
        public const uint GL_TEXTURE_MAX_LEVEL_SGIS = 0x813D;

        #endregion

        #region GL_EXT_draw_range_elements

        /// <summary>
        /// Render primitives from array data.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="count">The count.</param>
        /// <param name="type">The type.</param>
        /// <param name="indices">The indices.</param>
        public void DrawRangeElementsEXT(uint mode, uint start, uint end, uint count, uint type, IntPtr indices)
        {
            InvokeExtensionFunction<glDrawRangeElementsEXT>(mode, start, end, count, type, indices);
        }

        private delegate void glDrawRangeElementsEXT(uint mode, uint start, uint end, uint count, uint type, IntPtr indices);

        public const uint GL_MAX_ELEMENTS_VERTICES_EXT = 0x80E8;
        public const uint GL_MAX_ELEMENTS_INDICES_EXT = 0x80E9;

        #endregion

        #region GL_SGI_color_table

        //  Delegates
        public void ColorTableSGI(uint target, uint internalformat, uint width, uint format, uint type, IntPtr table)
        {
            InvokeExtensionFunction<glColorTableSGI>(target, internalformat, width, format, type, table);
        }

        public void ColorTableParameterSGI(uint target, uint pname, float[] parameters)
        {
            InvokeExtensionFunction<glColorTableParameterfvSGI>(target, pname, parameters);
        }

        public void ColorTableParameterSGI(uint target, uint pname, int[] parameters)
        {
            InvokeExtensionFunction<glColorTableParameterivSGI>(target, pname, parameters);
        }

        public void CopyColorTableSGI(uint target, uint internalformat, int x, int y, uint width)
        {
            InvokeExtensionFunction<glCopyColorTableSGI>(target, internalformat, x, y, width);
        }

        public void GetColorTableSGI(uint target, uint format, uint type, IntPtr table)
        {
            InvokeExtensionFunction<glGetColorTableSGI>(target, format, type, table);
        }

        public void GetColorTableParameterSGI(uint target, uint pname, float[] parameters)
        {
            InvokeExtensionFunction<glGetColorTableParameterfvSGI>(target, pname, parameters);
        }

        public void GetColorTableParameterSGI(uint target, uint pname, int[] parameters)
        {
            InvokeExtensionFunction<glGetColorTableParameterivSGI>(target, pname, parameters);
        }

        //  Delegates
        private delegate void glColorTableSGI(uint target, uint internalformat, uint width, uint format, uint type, IntPtr table);
        private delegate void glColorTableParameterfvSGI(uint target, uint pname, float[] parameters);
        private delegate void glColorTableParameterivSGI(uint target, uint pname, int[] parameters);
        private delegate void glCopyColorTableSGI(uint target, uint internalformat, int x, int y, uint width);
        private delegate void glGetColorTableSGI(uint target, uint format, uint type, IntPtr table);
        private delegate void glGetColorTableParameterfvSGI(uint target, uint pname, float[] parameters);
        private delegate void glGetColorTableParameterivSGI(uint target, uint pname, int[] parameters);

        //  Constants
        public const uint GL_COLOR_TABLE_SGI = 0x80D0;
        public const uint GL_POST_CONVOLUTION_COLOR_TABLE_SGI = 0x80D1;
        public const uint GL_POST_COLOR_MATRIX_COLOR_TABLE_SGI = 0x80D2;
        public const uint GL_PROXY_COLOR_TABLE_SGI = 0x80D3;
        public const uint GL_PROXY_POST_CONVOLUTION_COLOR_TABLE_SGI = 0x80D4;
        public const uint GL_PROXY_POST_COLOR_MATRIX_COLOR_TABLE_SGI = 0x80D5;
        public const uint GL_COLOR_TABLE_SCALE_SGI = 0x80D6;
        public const uint GL_COLOR_TABLE_BIAS_SGI = 0x80D7;
        public const uint GL_COLOR_TABLE_FORMAT_SGI = 0x80D8;
        public const uint GL_COLOR_TABLE_WIDTH_SGI = 0x80D9;
        public const uint GL_COLOR_TABLE_RED_SIZE_SGI = 0x80DA;
        public const uint GL_COLOR_TABLE_GREEN_SIZE_SGI = 0x80DB;
        public const uint GL_COLOR_TABLE_BLUE_SIZE_SGI = 0x80DC;
        public const uint GL_COLOR_TABLE_ALPHA_SIZE_SGI = 0x80DD;
        public const uint GL_COLOR_TABLE_LUMINANCE_SIZE_SGI = 0x80DE;
        public const uint GL_COLOR_TABLE_INTENSITY_SIZE_SGI = 0x80DF;

        #endregion

        #region GL_EXT_convolution

        //  Methods.
        public void ConvolutionFilter1DEXT(uint target, uint internalformat, int width, uint format, uint type, IntPtr image)
        {
            InvokeExtensionFunction<glConvolutionFilter1DEXT>(target, internalformat, width, format, type, image);
        }

        public void ConvolutionFilter2DEXT(uint target, uint internalformat, int width, int height, uint format, uint type, IntPtr image)
        {
            InvokeExtensionFunction<glConvolutionFilter2DEXT>(target, internalformat, width, height, format, type, image);
        }

        public void ConvolutionParameterEXT(uint target, uint pname, float parameters)
        {
            InvokeExtensionFunction<glConvolutionParameterfEXT>(target, pname, parameters);
        }

        public void ConvolutionParameterEXT(uint target, uint pname, float[] parameters)
        {
            InvokeExtensionFunction<glConvolutionParameterfvEXT>(target, pname, parameters);
        }

        public void ConvolutionParameterEXT(uint target, uint pname, int parameter)
        {
            InvokeExtensionFunction<glConvolutionParameteriEXT>(target, pname, parameter);
        }

        public void ConvolutionParameterEXT(uint target, uint pname, int[] parameters)
        {
            InvokeExtensionFunction<glConvolutionParameterivEXT>(target, pname, parameters);
        }

        public void CopyConvolutionFilter1DEXT(uint target, uint internalformat, int x, int y, int width)
        {
            InvokeExtensionFunction<glCopyConvolutionFilter1DEXT>(target, internalformat, x, y, width);
        }

        public void CopyConvolutionFilter2DEXT(uint target, uint internalformat, int x, int y, int width, int height)
        {
            InvokeExtensionFunction<glCopyConvolutionFilter2DEXT>(target, internalformat, x, y, width, height);
        }

        public void GetConvolutionFilterEXT(uint target, uint format, uint type, IntPtr image)
        {
            InvokeExtensionFunction<glGetConvolutionFilterEXT>(target, format, type, image);
        }

        public void GetConvolutionParameterfvEXT(uint target, uint pname, float[] parameters)
        {
            InvokeExtensionFunction<glGetConvolutionParameterfvEXT>(target, pname, parameters);
        }

        public void GetConvolutionParameterivEXT(uint target, uint pname, int[] parameters)
        {
            InvokeExtensionFunction<glGetConvolutionParameterivEXT>(target, pname, parameters);
        }

        public void GetSeparableFilterEXT(uint target, uint format, uint type, IntPtr row, IntPtr column, IntPtr span)
        {
            InvokeExtensionFunction<glGetSeparableFilterEXT>(target, format, type, row, column, span);
        }

        public void SeparableFilter2DEXT(uint target, uint internalformat, int width, int height, uint format, uint type, IntPtr row, IntPtr column)
        {
            InvokeExtensionFunction<glSeparableFilter2DEXT>(target, internalformat, width, height, format, type, row, column);
        }

        //  Delegates
        private delegate void glConvolutionFilter1DEXT(uint target, uint internalformat, int width, uint format, uint type, IntPtr image);
        private delegate void glConvolutionFilter2DEXT(uint target, uint internalformat, int width, int height, uint format, uint type, IntPtr image);
        private delegate void glConvolutionParameterfEXT(uint target, uint pname, float parameters);
        private delegate void glConvolutionParameterfvEXT(uint target, uint pname, float[] parameters);
        private delegate void glConvolutionParameteriEXT(uint target, uint pname, int parameter);
        private delegate void glConvolutionParameterivEXT(uint target, uint pname, int[] parameters);
        private delegate void glCopyConvolutionFilter1DEXT(uint target, uint internalformat, int x, int y, int width);
        private delegate void glCopyConvolutionFilter2DEXT(uint target, uint internalformat, int x, int y, int width, int height);
        private delegate void glGetConvolutionFilterEXT(uint target, uint format, uint type, IntPtr image);
        private delegate void glGetConvolutionParameterfvEXT(uint target, uint pname, float[] parameters);
        private delegate void glGetConvolutionParameterivEXT(uint target, uint pname, int[] parameters);
        private delegate void glGetSeparableFilterEXT(uint target, uint format, uint type, IntPtr row, IntPtr column, IntPtr span);
        private delegate void glSeparableFilter2DEXT(uint target, uint internalformat, int width, int height, uint format, uint type, IntPtr row, IntPtr column);

        //  Constants        
        public static uint GL_CONVOLUTION_1D_EXT = 0x8010;
        public static uint GL_CONVOLUTION_2D_EXT = 0x8011;
        public static uint GL_SEPARABLE_2D_EXT = 0x8012;
        public static uint GL_CONVOLUTION_BORDER_MODE_EXT = 0x8013;
        public static uint GL_CONVOLUTION_FILTER_SCALE_EXT = 0x8014;
        public static uint GL_CONVOLUTION_FILTER_BIAS_EXT = 0x8015;
        public static uint GL_REDUCE_EXT = 0x8016;
        public static uint GL_CONVOLUTION_FORMAT_EXT = 0x8017;
        public static uint GL_CONVOLUTION_WIDTH_EXT = 0x8018;
        public static uint GL_CONVOLUTION_HEIGHT_EXT = 0x8019;
        public static uint GL_MAX_CONVOLUTION_WIDTH_EXT = 0x801A;
        public static uint GL_MAX_CONVOLUTION_HEIGHT_EXT = 0x801B;
        public static uint GL_POST_CONVOLUTION_RED_SCALE_EXT = 0x801C;
        public static uint GL_POST_CONVOLUTION_GREEN_SCALE_EXT = 0x801D;
        public static uint GL_POST_CONVOLUTION_BLUE_SCALE_EXT = 0x801E;
        public static uint GL_POST_CONVOLUTION_ALPHA_SCALE_EXT = 0x801F;
        public static uint GL_POST_CONVOLUTION_RED_BIAS_EXT = 0x8020;
        public static uint GL_POST_CONVOLUTION_GREEN_BIAS_EXT = 0x8021;
        public static uint GL_POST_CONVOLUTION_BLUE_BIAS_EXT = 0x8022;
        public static uint GL_POST_CONVOLUTION_ALPHA_BIAS_EXT = 0x8023;

        #endregion

        #region GL_SGI_color_matrix

        public const uint GL_COLOR_MATRIX_SGI = 0x80B1;
        public const uint GL_COLOR_MATRIX_STACK_DEPTH_SGI = 0x80B2;
        public const uint GL_MAX_COLOR_MATRIX_STACK_DEPTH_SGI = 0x80B3;
        public const uint GL_POST_COLOR_MATRIX_RED_SCALE_SGI = 0x80B4;
        public const uint GL_POST_COLOR_MATRIX_GREEN_SCALE_SGI = 0x80B5;
        public const uint GL_POST_COLOR_MATRIX_BLUE_SCALE_SGI = 0x80B6;
        public const uint GL_POST_COLOR_MATRIX_ALPHA_SCALE_SGI = 0x80B7;
        public const uint GL_POST_COLOR_MATRIX_RED_BIAS_SGI = 0x80B8;
        public const uint GL_POST_COLOR_MATRIX_GREEN_BIAS_SGI = 0x80B9;
        public const uint GL_POST_COLOR_MATRIX_BLUE_BIAS_SGI = 0x80BA;
        public const uint GL_POST_COLOR_MATRIX_ALPHA_BIAS_SGI = 0x80BB;

        #endregion

        #region GL_EXT_histogram

        //  Methods
        public void GetHistogramEXT(uint target, bool reset, uint format, uint type, IntPtr values)
        {
            InvokeExtensionFunction<glGetHistogramEXT>(target, reset, format, type, values);
        }

        public void GetHistogramParameterEXT(uint target, uint pname, float[] parameters)
        {
            InvokeExtensionFunction<glGetHistogramParameterfvEXT>(target, pname, parameters);
        }

        public void GetHistogramParameterEXT(uint target, uint pname, int[] parameters)
        {
            InvokeExtensionFunction<glGetHistogramParameterivEXT>(target, pname, parameters);
        }

        public void GetMinmaxEXT(uint target, bool reset, uint format, uint type, IntPtr values)
        {
            InvokeExtensionFunction<glGetMinmaxEXT>(target, reset, format, type, values);
        }

        public void GetMinmaxParameterfvEXT(uint target, uint pname, float[] parameters)
        {
            InvokeExtensionFunction<glGetMinmaxParameterfvEXT>(target, pname, parameters);
        }

        public void GetMinmaxParameterivEXT(uint target, uint pname, int[] parameters)
        {
            InvokeExtensionFunction<glGetMinmaxParameterivEXT>(target, pname, parameters);
        }

        public void HistogramEXT(uint target, int width, uint internalformat, bool sink)
        {
            InvokeExtensionFunction<glHistogramEXT>(target, width, internalformat, sink);
        }

        public void MinmaxEXT(uint target, uint internalformat, bool sink)
        {
            InvokeExtensionFunction<glMinmaxEXT>(target, internalformat, sink);
        }

        public void ResetHistogramEXT(uint target)
        {
            InvokeExtensionFunction<glResetHistogramEXT>(target);
        }

        public void ResetMinmaxEXT(uint target)
        {
            InvokeExtensionFunction<glResetMinmaxEXT>(target);
        }

        //  Delegates
        private delegate void glGetHistogramEXT(uint target, bool reset, uint format, uint type, IntPtr values);
        private delegate void glGetHistogramParameterfvEXT(uint target, uint pname, float[] parameters);
        private delegate void glGetHistogramParameterivEXT(uint target, uint pname, int[] parameters);
        private delegate void glGetMinmaxEXT(uint target, bool reset, uint format, uint type, IntPtr values);
        private delegate void glGetMinmaxParameterfvEXT(uint target, uint pname, float[] parameters);
        private delegate void glGetMinmaxParameterivEXT(uint target, uint pname, int[] parameters);
        private delegate void glHistogramEXT(uint target, int width, uint internalformat, bool sink);
        private delegate void glMinmaxEXT(uint target, uint internalformat, bool sink);
        private delegate void glResetHistogramEXT(uint target);
        private delegate void glResetMinmaxEXT(uint target);

        //  Constants
        public const uint GL_HISTOGRAM_EXT = 0x8024;
        public const uint GL_PROXY_HISTOGRAM_EXT = 0x8025;
        public const uint GL_HISTOGRAM_WIDTH_EXT = 0x8026;
        public const uint GL_HISTOGRAM_FORMAT_EXT = 0x8027;
        public const uint GL_HISTOGRAM_RED_SIZE_EXT = 0x8028;
        public const uint GL_HISTOGRAM_GREEN_SIZE_EXT = 0x8029;
        public const uint GL_HISTOGRAM_BLUE_SIZE_EXT = 0x802A;
        public const uint GL_HISTOGRAM_ALPHA_SIZE_EXT = 0x802B;
        public const uint GL_HISTOGRAM_LUMINANCE_SIZE_EXT = 0x802C;
        public const uint GL_HISTOGRAM_SINK_EXT = 0x802D;
        public const uint GL_MINMAX_EXT = 0x802E;
        public const uint GL_MINMAX_FORMAT_EXT = 0x802F;
        public const uint GL_MINMAX_SINK_EXT = 0x8030;
        public const uint GL_TABLE_TOO_LARGE_EXT = 0x8031;

        #endregion

        #region GL_EXT_blend_color

        //  Methods
        public void BlendColorEXT(float red, float green, float blue, float alpha)
        {
            InvokeExtensionFunction<glBlendColorEXT>(red, green, blue, alpha);
        }

        //  Delegates
        private delegate void glBlendColorEXT(float red, float green, float blue, float alpha);

        //  Constants        
        public const uint GL_CONSTANT_COLOR_EXT = 0x8001;
        public const uint GL_ONE_MINUS_CONSTANT_COLOR_EXT = 0x8002;
        public const uint GL_CONSTANT_ALPHA_EXT = 0x8003;
        public const uint GL_ONE_MINUS_CONSTANT_ALPHA_EXT = 0x8004;
        public const uint GL_BLEND_COLOR_EXT = 0x8005;

        #endregion

        #region GL_EXT_blend_minmax

        //  Methods
        public void BlendEquationEXT(uint mode)
        {
            InvokeExtensionFunction<glBlendEquationEXT>(mode);
        }

        //  Delegates
        private delegate void glBlendEquationEXT(uint mode);

        //  Constants        
        public const uint GL_FUNC_ADD_EXT = 0x8006;
        public const uint GL_MIN_EXT = 0x8007;
        public const uint GL_MAX_EXT = 0x8008;
        public const uint GL_BLEND_EQUATION_EXT = 0x8009;

        #endregion

        #region GL_ARB_multitexture

        //  Methods
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void ActiveTextureARB(uint texture)
        {
            InvokeExtensionFunction<glActiveTextureARB>(texture);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void ClientActiveTextureARB(uint texture)
        {
            InvokeExtensionFunction<glClientActiveTextureARB>(texture);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord1ARB(uint target, double s)
        {
            InvokeExtensionFunction<glMultiTexCoord1dARB>(target, s);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord1ARB(uint target, double[] v)
        {
            InvokeExtensionFunction<glMultiTexCoord1dvARB>(target, v);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord1ARB(uint target, float s)
        {
            InvokeExtensionFunction<glMultiTexCoord1fARB>(target, s);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord1ARB(uint target, float[] v)
        {
            InvokeExtensionFunction<glMultiTexCoord1fvARB>(target, v);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord1ARB(uint target, int s)
        {
            InvokeExtensionFunction<glMultiTexCoord1iARB>(target, s);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord1ARB(uint target, int[] v)
        {
            InvokeExtensionFunction<glMultiTexCoord1ivARB>(target, v);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord1ARB(uint target, short s)
        {
            InvokeExtensionFunction<glMultiTexCoord1sARB>(target, s);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord1ARB(uint target, short[] v)
        {
            InvokeExtensionFunction<glMultiTexCoord1svARB>(target, v);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord2ARB(uint target, double s, double t)
        {
            InvokeExtensionFunction<glMultiTexCoord2dARB>(target, s, t);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord2ARB(uint target, double[] v)
        {
            InvokeExtensionFunction<glMultiTexCoord2dvARB>(target, v);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord2ARB(uint target, float s, float t)
        {
            InvokeExtensionFunction<glMultiTexCoord2fARB>(target, s, t);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord2ARB(uint target, float[] v)
        {
            InvokeExtensionFunction<glMultiTexCoord2fvARB>(target, v);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord2ARB(uint target, int s, int t)
        {
            InvokeExtensionFunction<glMultiTexCoord2iARB>(target, s, t);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord2ARB(uint target, int[] v)
        {
            InvokeExtensionFunction<glMultiTexCoord2ivARB>(target, v);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord2ARB(uint target, short s, short t)
        {
            InvokeExtensionFunction<glMultiTexCoord2sARB>(target, s, t);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord2ARB(uint target, short[] v)
        {
            InvokeExtensionFunction<glMultiTexCoord2svARB>(target, v);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord3ARB(uint target, double s, double t, double r)
        {
            InvokeExtensionFunction<glMultiTexCoord3dARB>(s, t, r);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord3ARB(uint target, double[] v)
        {
            InvokeExtensionFunction<glMultiTexCoord3dvARB>(target, v);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord3ARB(uint target, float s, float t, float r)
        {
            InvokeExtensionFunction<glMultiTexCoord3fARB>(target, s, t, r);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord3ARB(uint target, float[] v)
        {
            InvokeExtensionFunction<glMultiTexCoord3fvARB>(target, v);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord3ARB(uint target, int s, int t, int r)
        {
            InvokeExtensionFunction<glMultiTexCoord3iARB>(target, s, t, r);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord3ARB(uint target, int[] v)
        {
            InvokeExtensionFunction<glMultiTexCoord3ivARB>(target, v);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord3ARB(uint target, short s, short t, short r)
        {
            InvokeExtensionFunction<glMultiTexCoord3sARB>(target, s, t, r);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord3ARB(uint target, short[] v)
        {
            InvokeExtensionFunction<glMultiTexCoord3svARB>(target, v);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord4ARB(uint target, double s, double t, double r, double q)
        {
            InvokeExtensionFunction<glMultiTexCoord4dARB>(target, s, t, r, q);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord4ARB(uint target, double[] v)
        {
            InvokeExtensionFunction<glMultiTexCoord4dvARB>(target, v);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord4ARB(uint target, float s, float t, float r, float q)
        {
            InvokeExtensionFunction<glMultiTexCoord4fARB>(target, s, t, r, q);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord4ARB(uint target, float[] v)
        {
            InvokeExtensionFunction<glMultiTexCoord4fvARB>(target, v);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord4ARB(uint target, int s, int t, int r, int q)
        {
            InvokeExtensionFunction<glMultiTexCoord4iARB>(target, s, t, r, q);
        }
        public void MultiTexCoord4ARB(uint target, int[] v)
        {
            InvokeExtensionFunction<glMultiTexCoord4ivARB>(target, v);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord4ARB(uint target, short s, short t, short r, short q)
        {
            InvokeExtensionFunction<glMultiTexCoord4sARB>(target, s, t, r, q);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord4ARB(uint target, short[] v)
        {
            InvokeExtensionFunction<glMultiTexCoord4svARB>(target, v);
        }

        //  Delegates 
        private delegate void glActiveTextureARB(uint texture);
        private delegate void glClientActiveTextureARB(uint texture);
        private delegate void glMultiTexCoord1dARB(uint target, double s);
        private delegate void glMultiTexCoord1dvARB(uint target, double[] v);
        private delegate void glMultiTexCoord1fARB(uint target, float s);
        private delegate void glMultiTexCoord1fvARB(uint target, float[] v);
        private delegate void glMultiTexCoord1iARB(uint target, int s);
        private delegate void glMultiTexCoord1ivARB(uint target, int[] v);
        private delegate void glMultiTexCoord1sARB(uint target, short s);
        private delegate void glMultiTexCoord1svARB(uint target, short[] v);
        private delegate void glMultiTexCoord2dARB(uint target, double s, double t);
        private delegate void glMultiTexCoord2dvARB(uint target, double[] v);
        private delegate void glMultiTexCoord2fARB(uint target, float s, float t);
        private delegate void glMultiTexCoord2fvARB(uint target, float[] v);
        private delegate void glMultiTexCoord2iARB(uint target, int s, int t);
        private delegate void glMultiTexCoord2ivARB(uint target, int[] v);
        private delegate void glMultiTexCoord2sARB(uint target, short s, short t);
        private delegate void glMultiTexCoord2svARB(uint target, short[] v);
        private delegate void glMultiTexCoord3dARB(uint target, double s, double t, double r);
        private delegate void glMultiTexCoord3dvARB(uint target, double[] v);
        private delegate void glMultiTexCoord3fARB(uint target, float s, float t, float r);
        private delegate void glMultiTexCoord3fvARB(uint target, float[] v);
        private delegate void glMultiTexCoord3iARB(uint target, int s, int t, int r);
        private delegate void glMultiTexCoord3ivARB(uint target, int[] v);
        private delegate void glMultiTexCoord3sARB(uint target, short s, short t, short r);
        private delegate void glMultiTexCoord3svARB(uint target, short[] v);
        private delegate void glMultiTexCoord4dARB(uint target, double s, double t, double r, double q);
        private delegate void glMultiTexCoord4dvARB(uint target, double[] v);
        private delegate void glMultiTexCoord4fARB(uint target, float s, float t, float r, float q);
        private delegate void glMultiTexCoord4fvARB(uint target, float[] v);
        private delegate void glMultiTexCoord4iARB(uint target, int s, int t, int r, int q);
        private delegate void glMultiTexCoord4ivARB(uint target, int[] v);
        private delegate void glMultiTexCoord4sARB(uint target, short s, short t, short r, short q);
        private delegate void glMultiTexCoord4svARB(uint target, short[] v);

        //  Constants
        public const uint GL_TEXTURE0_ARB = 0x84C0;
        public const uint GL_TEXTURE1_ARB = 0x84C1;
        public const uint GL_TEXTURE2_ARB = 0x84C2;
        public const uint GL_TEXTURE3_ARB = 0x84C3;
        public const uint GL_TEXTURE4_ARB = 0x84C4;
        public const uint GL_TEXTURE5_ARB = 0x84C5;
        public const uint GL_TEXTURE6_ARB = 0x84C6;
        public const uint GL_TEXTURE7_ARB = 0x84C7;
        public const uint GL_TEXTURE8_ARB = 0x84C8;
        public const uint GL_TEXTURE9_ARB = 0x84C9;
        public const uint GL_TEXTURE10_ARB = 0x84CA;
        public const uint GL_TEXTURE11_ARB = 0x84CB;
        public const uint GL_TEXTURE12_ARB = 0x84CC;
        public const uint GL_TEXTURE13_ARB = 0x84CD;
        public const uint GL_TEXTURE14_ARB = 0x84CE;
        public const uint GL_TEXTURE15_ARB = 0x84CF;
        public const uint GL_TEXTURE16_ARB = 0x84D0;
        public const uint GL_TEXTURE17_ARB = 0x84D1;
        public const uint GL_TEXTURE18_ARB = 0x84D2;
        public const uint GL_TEXTURE19_ARB = 0x84D3;
        public const uint GL_TEXTURE20_ARB = 0x84D4;
        public const uint GL_TEXTURE21_ARB = 0x84D5;
        public const uint GL_TEXTURE22_ARB = 0x84D6;
        public const uint GL_TEXTURE23_ARB = 0x84D7;
        public const uint GL_TEXTURE24_ARB = 0x84D8;
        public const uint GL_TEXTURE25_ARB = 0x84D9;
        public const uint GL_TEXTURE26_ARB = 0x84DA;
        public const uint GL_TEXTURE27_ARB = 0x84DB;
        public const uint GL_TEXTURE28_ARB = 0x84DC;
        public const uint GL_TEXTURE29_ARB = 0x84DD;
        public const uint GL_TEXTURE30_ARB = 0x84DE;
        public const uint GL_TEXTURE31_ARB = 0x84DF;
        public const uint GL_ACTIVE_TEXTURE_ARB = 0x84E0;
        public const uint GL_CLIENT_ACTIVE_TEXTURE_ARB = 0x84E1;
        public const uint GL_MAX_TEXTURE_UNITS_ARB = 0x84E2;

        #endregion

        #region GL_ARB_texture_compression

        //  Methods
        public void CompressedTexImage3DARB(uint target, int level, uint internalformat, int width, int height, int depth, int border, int imageSize, IntPtr data)
        {
            InvokeExtensionFunction<glCompressedTexImage3DARB>(target, level, internalformat, width, height, depth, border, imageSize, data);
        }
        public void CompressedTexImage2DARB(uint target, int level, uint internalformat, int width, int height, int border, int imageSize, IntPtr data)
        {
            InvokeExtensionFunction<glCompressedTexImage2DARB>(target, level, internalformat, width, height, border, imageSize, data);
        }
        public void CompressedTexImage1DARB(uint target, int level, uint internalformat, int width, int border, int imageSize, IntPtr data)
        {
            InvokeExtensionFunction<glCompressedTexImage1DARB>(target, level, internalformat, width, border, imageSize, data);
        }
        public void CompressedTexSubImage3DARB(uint target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, int imageSize, IntPtr data)
        {
            InvokeExtensionFunction<glCompressedTexSubImage3DARB>(target, level, xoffset, yoffset, zoffset, width, height, depth, format, imageSize, data);
        }
        public void CompressedTexSubImage2DARB(uint target, int level, int xoffset, int yoffset, int width, int height, uint format, int imageSize, IntPtr data)
        {
            InvokeExtensionFunction<glCompressedTexSubImage2DARB>(target, level, xoffset, yoffset, width, height, format, imageSize, data);
        }
        public void CompressedTexSubImage1DARB(uint target, int level, int xoffset, int width, uint format, int imageSize, IntPtr data)
        {
            InvokeExtensionFunction<glCompressedTexSubImage1DARB>(target, level, xoffset, width, format, imageSize, data);
        }

        //  Delegates
        private delegate void glCompressedTexImage3DARB(uint target, int level, uint internalformat, int width, int height, int depth, int border, int imageSize, IntPtr data);
        private delegate void glCompressedTexImage2DARB(uint target, int level, uint internalformat, int width, int height, int border, int imageSize, IntPtr data);
        private delegate void glCompressedTexImage1DARB(uint target, int level, uint internalformat, int width, int border, int imageSize, IntPtr data);
        private delegate void glCompressedTexSubImage3DARB(uint target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, int imageSize, IntPtr data);
        private delegate void glCompressedTexSubImage2DARB(uint target, int level, int xoffset, int yoffset, int width, int height, uint format, int imageSize, IntPtr data);
        private delegate void glCompressedTexSubImage1DARB(uint target, int level, int xoffset, int width, uint format, int imageSize, IntPtr data);
        private delegate void glGetCompressedTexImageARB(uint target, int level, IntPtr img);

        //  Constants
        public const uint GL_COMPRESSED_ALPHA_ARB = 0x84E9;
        public const uint GL_COMPRESSED_LUMINANCE_ARB = 0x84EA;
        public const uint GL_COMPRESSED_LUMINANCE_ALPHA_ARB = 0x84EB;
        public const uint GL_COMPRESSED_INTENSITY_ARB = 0x84EC;
        public const uint GL_COMPRESSED_RGB_ARB = 0x84ED;
        public const uint GL_COMPRESSED_RGBA_ARB = 0x84EE;
        public const uint GL_TEXTURE_COMPRESSION_HINT_ARB = 0x84EF;
        public const uint GL_TEXTURE_COMPRESSED_IMAGE_SIZE_ARB = 0x86A0;
        public const uint GL_TEXTURE_COMPRESSED_ARB = 0x86A1;
        public const uint GL_NUM_COMPRESSED_TEXTURE_FORMATS_ARB = 0x86A2;
        public const uint GL_COMPRESSED_TEXTURE_FORMATS_ARB = 0x86A3;

        #endregion

        #region GL_EXT_texture_cube_map

        //  Constants
        public const uint GL_NORMAL_MAP_EXT = 0x8511;
        public const uint GL_REFLECTION_MAP_EXT = 0x8512;
        public const uint GL_TEXTURE_CUBE_MAP_EXT = 0x8513;
        public const uint GL_TEXTURE_BINDING_CUBE_MAP_EXT = 0x8514;
        public const uint GL_TEXTURE_CUBE_MAP_POSITIVE_X_EXT = 0x8515;
        public const uint GL_TEXTURE_CUBE_MAP_NEGATIVE_X_EXT = 0x8516;
        public const uint GL_TEXTURE_CUBE_MAP_POSITIVE_Y_EXT = 0x8517;
        public const uint GL_TEXTURE_CUBE_MAP_NEGATIVE_Y_EXT = 0x8518;
        public const uint GL_TEXTURE_CUBE_MAP_POSITIVE_Z_EXT = 0x8519;
        public const uint GL_TEXTURE_CUBE_MAP_NEGATIVE_Z_EXT = 0x851A;
        public const uint GL_PROXY_TEXTURE_CUBE_MAP_EXT = 0x851B;
        public const uint GL_MAX_CUBE_MAP_TEXTURE_SIZE_EXT = 0x851C;

        #endregion

        #region GL_ARB_multisample

        //  Methods
        public void SampleCoverageARB(float value, bool invert)
        {
            InvokeExtensionFunction<glSampleCoverageARB>(value, invert);
        }

        //  Delegates
        private delegate void glSampleCoverageARB(float value, bool invert);

        //  Constants
        public const uint GL_MULTISAMPLE_ARB = 0x809D;
        public const uint GL_SAMPLE_ALPHA_TO_COVERAGE_ARB = 0x809E;
        public const uint GL_SAMPLE_ALPHA_TO_ONE_ARB = 0x809F;
        public const uint GL_SAMPLE_COVERAGE_ARB = 0x80A0;
        public const uint GL_SAMPLE_BUFFERS_ARB = 0x80A8;
        public const uint GL_SAMPLES_ARB = 0x80A9;
        public const uint GL_SAMPLE_COVERAGE_VALUE_ARB = 0x80AA;
        public const uint GL_SAMPLE_COVERAGE_INVERT_ARB = 0x80AB;
        public const uint GL_MULTISAMPLE_BIT_ARB = 0x20000000;

        #endregion

        #region GL_ARB_texture_env_add

        //  Appears to not have any functionality

        #endregion

        #region GL_ARB_texture_env_combine

        //  Constants
        public const uint GL_COMBINE_ARB = 0x8570;
        public const uint GL_COMBINE_RGB_ARB = 0x8571;
        public const uint GL_COMBINE_ALPHA_ARB = 0x8572;
        public const uint GL_SOURCE0_RGB_ARB = 0x8580;
        public const uint GL_SOURCE1_RGB_ARB = 0x8581;
        public const uint GL_SOURCE2_RGB_ARB = 0x8582;
        public const uint GL_SOURCE0_ALPHA_ARB = 0x8588;
        public const uint GL_SOURCE1_ALPHA_ARB = 0x8589;
        public const uint GL_SOURCE2_ALPHA_ARB = 0x858A;
        public const uint GL_OPERAND0_RGB_ARB = 0x8590;
        public const uint GL_OPERAND1_RGB_ARB = 0x8591;
        public const uint GL_OPERAND2_RGB_ARB = 0x8592;
        public const uint GL_OPERAND0_ALPHA_ARB = 0x8598;
        public const uint GL_OPERAND1_ALPHA_ARB = 0x8599;
        public const uint GL_OPERAND2_ALPHA_ARB = 0x859A;
        public const uint GL_RGB_SCALE_ARB = 0x8573;
        public const uint GL_ADD_SIGNED_ARB = 0x8574;
        public const uint GL_INTERPOLATE_ARB = 0x8575;
        public const uint GL_SUBTRACT_ARB = 0x84E7;
        public const uint GL_CONSTANT_ARB = 0x8576;
        public const uint GL_PRIMARY_COLOR_ARB = 0x8577;
        public const uint GL_PREVIOUS_ARB = 0x8578;

        #endregion

        #region GL_ARB_texture_env_dot3

        //  Constants
        public const uint GL_DOT3_RGB_ARB = 0x86AE;
        public const uint GL_DOT3_RGBA_ARB = 0x86AF;

        #endregion

        #region GL_ARB_texture_border_clamp

        //  Constants
        public const uint GL_CLAMP_TO_BORDER_ARB = 0x812D;

        #endregion

        #region GL_ARB_transpose_matrix

        //  Methods
        public void glLoadTransposeMatrixARB(float[] m)
        {
            InvokeExtensionFunction<glLoadTransposeMatrixfARB>(m);
        }
        public void glLoadTransposeMatrixARB(double[] m)
        {
            InvokeExtensionFunction<glLoadTransposeMatrixdARB>(m);
        }
        public void glMultTransposeMatrixARB(float[] m)
        {
            InvokeExtensionFunction<glMultTransposeMatrixfARB>(m);
        }
        public void glMultTransposeMatrixARB(double[] m)
        {
            InvokeExtensionFunction<glMultTransposeMatrixdARB>(m);
        }

        //  Delegates
        private delegate void glLoadTransposeMatrixfARB(float[] m);
        private delegate void glLoadTransposeMatrixdARB(double[] m);
        private delegate void glMultTransposeMatrixfARB(float[] m);
        private delegate void glMultTransposeMatrixdARB(double[] m);

        //  Constants
        public const uint GL_TRANSPOSE_MODELVIEW_MATRIX_ARB = 0x84E3;
        public const uint GL_TRANSPOSE_PROJECTION_MATRIX_ARB = 0x84E4;
        public const uint GL_TRANSPOSE_TEXTURE_MATRIX_ARB = 0x84E5;
        public const uint GL_TRANSPOSE_COLOR_MATRIX_ARB = 0x84E6;

        #endregion

        #region GL_SGIS_generate_mipmap

        //  Constants
        public const uint GL_GENERATE_MIPMAP_SGIS = 0x8191;
        public const uint GL_GENERATE_MIPMAP_HINT_SGIS = 0x8192;

        #endregion

        #region GL_NV_blend_square

        //  Appears to be empty.

        #endregion

        #region GL_ARB_depth_texture

        //  Constants
        public const uint GL_DEPTH_COMPONENT16_ARB = 0x81A5;
        public const uint GL_DEPTH_COMPONENT24_ARB = 0x81A6;
        public const uint GL_DEPTH_COMPONENT32_ARB = 0x81A7;
        public const uint GL_TEXTURE_DEPTH_SIZE_ARB = 0x884A;
        public const uint GL_DEPTH_TEXTURE_MODE_ARB = 0x884B;

        #endregion

        #region GL_ARB_shadow

        //  Constants
        public const uint GL_TEXTURE_COMPARE_MODE_ARB = 0x884C;
        public const uint GL_TEXTURE_COMPARE_FUNC_ARB = 0x884D;
        public const uint GL_COMPARE_R_TO_TEXTURE_ARB = 0x884E;

        #endregion

        #region GL_EXT_fog_coord

        //  Methods
        public void FogCoordEXT(float coord)
        {
            InvokeExtensionFunction<glFogCoordfEXT>(coord);
        }
        public void FogCoordEXT(float[] coord)
        {
            InvokeExtensionFunction<glFogCoordfvEXT>(coord);
        }
        public void FogCoordEXT(double coord)
        {
            InvokeExtensionFunction<glFogCoorddEXT>(coord);
        }
        public void FogCoordEXT(double[] coord)
        {
            InvokeExtensionFunction<glFogCoorddvEXT>(coord);
        }
        public void FogCoordPointerEXT(uint type, int stride, IntPtr pointer)
        {
            InvokeExtensionFunction<glFogCoordPointerEXT>(type, stride, pointer);
        }

        //  Delegates
        private delegate void glFogCoordfEXT(float coord);
        private delegate void glFogCoordfvEXT(float[] coord);
        private delegate void glFogCoorddEXT(double coord);
        private delegate void glFogCoorddvEXT(double[] coord);
        private delegate void glFogCoordPointerEXT(uint type, int stride, IntPtr pointer);

        //  Constants
        public const uint GL_FOG_COORDINATE_SOURCE_EXT = 0x8450;
        public const uint GL_FOG_COORDINATE_EXT = 0x8451;
        public const uint GL_FRAGMENT_DEPTH_EXT = 0x8452;
        public const uint GL_CURRENT_FOG_COORDINATE_EXT = 0x8453;
        public const uint GL_FOG_COORDINATE_ARRAY_TYPE_EXT = 0x8454;
        public const uint GL_FOG_COORDINATE_ARRAY_STRIDE_EXT = 0x8455;
        public const uint GL_FOG_COORDINATE_ARRAY_POINTER_EXT = 0x8456;
        public const uint GL_FOG_COORDINATE_ARRAY_EXT = 0x8457;

        #endregion

        #region GL_EXT_multi_draw_arrays

        //  Methods
        public void MultiDrawArraysEXT(uint mode, int[] first, int[] count, int primcount)
        {
            InvokeExtensionFunction<glMultiDrawArraysEXT>(mode, first, count, primcount);
        }
        public void MultiDrawElementsEXT(uint mode, int[] count, uint type, IntPtr indices, int primcount)
        {
            InvokeExtensionFunction<glMultiDrawElementsEXT>(mode, count, type, indices, primcount);
        }

        //  Delegates
        private delegate void glMultiDrawArraysEXT(uint mode, int[] first, int[] count, int primcount);
        private delegate void glMultiDrawElementsEXT(uint mode, int[] count, uint type, IntPtr indices, int primcount);

        #endregion

        #region GL_ARB_point_parameters

        //  Methods
        public void glPointParameterARB(uint pname, float parameter)
        {
            InvokeExtensionFunction<glPointParameterfARB>(pname, parameter);
        }
        public void glPointParameterARB(uint pname, float[] parameters)
        {
            InvokeExtensionFunction<glPointParameterfvARB>(pname, parameters);
        }

        //  Delegates
        private delegate void glPointParameterfARB(uint pname, float param);
        private delegate void glPointParameterfvARB(uint pname, float[] parameters);

        //  Constants
        public const uint GL_POINT_SIZE_MIN_ARB = 0x8126;
        public const uint GL_POINT_SIZE_MAX_ARB = 0x8127;
        public const uint GL_POINT_FADE_THRESHOLD_SIZE_ARB = 0x8128;
        public const uint GL_POINT_DISTANCE_ATTENUATION_ARB = 0x8129;

        #endregion

        #region GL_EXT_secondary_color

        //  Methods
        public void SecondaryColor3EXT(sbyte red, sbyte green, sbyte blue)
        {
            InvokeExtensionFunction<glSecondaryColor3bEXT>(red, green, blue);
        }
        public void SecondaryColor3EXT(sbyte[] v)
        {
            InvokeExtensionFunction<glSecondaryColor3bvEXT>(v);
        }
        public void SecondaryColor3EXT(double red, double green, double blue)
        {
            InvokeExtensionFunction<glSecondaryColor3dEXT>(red, green, blue);
        }
        public void SecondaryColor3EXT(double[] v)
        {
            InvokeExtensionFunction<glSecondaryColor3dvEXT>(v);
        }
        public void SecondaryColor3EXT(float red, float green, float blue)
        {
            InvokeExtensionFunction<glSecondaryColor3fEXT>(red, green, blue);
        }
        public void SecondaryColor3EXT(float[] v)
        {
            InvokeExtensionFunction<glSecondaryColor3fvEXT>(v);
        }
        public void SecondaryColor3EXT(int red, int green, int blue)
        {
            InvokeExtensionFunction<glSecondaryColor3iEXT>(red, green, blue);
        }
        public void SecondaryColor3EXT(int[] v)
        {
            InvokeExtensionFunction<glSecondaryColor3ivEXT>(v);
        }
        public void SecondaryColor3EXT(short red, short green, short blue)
        {
            InvokeExtensionFunction<glSecondaryColor3sEXT>(red, green, blue);
        }
        public void SecondaryColor3EXT(short[] v)
        {
            InvokeExtensionFunction<glSecondaryColor3svEXT>(v);
        }
        public void SecondaryColor3EXT(byte red, byte green, byte blue)
        {
            InvokeExtensionFunction<glSecondaryColor3ubEXT>(red, green, blue);
        }
        public void SecondaryColor3EXT(byte[] v)
        {
            InvokeExtensionFunction<glSecondaryColor3ubvEXT>(v);
        }
        public void SecondaryColor3EXT(uint red, uint green, uint blue)
        {
            InvokeExtensionFunction<glSecondaryColor3uiEXT>(red, green, blue);
        }
        public void SecondaryColor3EXT(uint[] v)
        {
            InvokeExtensionFunction<glSecondaryColor3uivEXT>(v);
        }
        public void SecondaryColor3EXT(ushort red, ushort green, ushort blue)
        {
            InvokeExtensionFunction<glSecondaryColor3usEXT>(red, green, blue);
        }
        public void SecondaryColor3EXT(ushort[] v)
        {
            InvokeExtensionFunction<glSecondaryColor3usvEXT>(v);
        }
        public void SecondaryColorPointerEXT(int size, uint type, int stride, IntPtr pointer)
        {
            InvokeExtensionFunction<glSecondaryColorPointerEXT>(size, type, stride, pointer);
        }

        //  Delegates
        private delegate void glSecondaryColor3bEXT(sbyte red, sbyte green, sbyte blue);
        private delegate void glSecondaryColor3bvEXT(sbyte[] v);
        private delegate void glSecondaryColor3dEXT(double red, double green, double blue);
        private delegate void glSecondaryColor3dvEXT(double[] v);
        private delegate void glSecondaryColor3fEXT(float red, float green, float blue);
        private delegate void glSecondaryColor3fvEXT(float[] v);
        private delegate void glSecondaryColor3iEXT(int red, int green, int blue);
        private delegate void glSecondaryColor3ivEXT(int[] v);
        private delegate void glSecondaryColor3sEXT(short red, short green, short blue);
        private delegate void glSecondaryColor3svEXT(short[] v);
        private delegate void glSecondaryColor3ubEXT(byte red, byte green, byte blue);
        private delegate void glSecondaryColor3ubvEXT(byte[] v);
        private delegate void glSecondaryColor3uiEXT(uint red, uint green, uint blue);
        private delegate void glSecondaryColor3uivEXT(uint[] v);
        private delegate void glSecondaryColor3usEXT(ushort red, ushort green, ushort blue);
        private delegate void glSecondaryColor3usvEXT(ushort[] v);
        private delegate void glSecondaryColorPointerEXT(int size, uint type, int stride, IntPtr pointer);

        //  Constants        
        public const uint GL_COLOR_SUM_EXT = 0x8458;
        public const uint GL_CURRENT_SECONDARY_COLOR_EXT = 0x8459;
        public const uint GL_SECONDARY_COLOR_ARRAY_SIZE_EXT = 0x845A;
        public const uint GL_SECONDARY_COLOR_ARRAY_TYPE_EXT = 0x845B;
        public const uint GL_SECONDARY_COLOR_ARRAY_STRIDE_EXT = 0x845C;
        public const uint GL_SECONDARY_COLOR_ARRAY_POINTER_EXT = 0x845D;
        public const uint GL_SECONDARY_COLOR_ARRAY_EXT = 0x845E;

        #endregion

        #region  GL_EXT_blend_func_separate

        //  Methods
        public void BlendFuncSeparateEXT(uint sfactorRGB, uint dfactorRGB, uint sfactorAlpha, uint dfactorAlpha)
        {
            InvokeExtensionFunction<glBlendFuncSeparateEXT>(sfactorRGB, dfactorRGB, sfactorAlpha, dfactorAlpha);
        }

        //  Delegates
        private delegate void glBlendFuncSeparateEXT(uint sfactorRGB, uint dfactorRGB, uint sfactorAlpha, uint dfactorAlpha);

        //  Constants
        public const uint GL_BLEND_DST_RGB_EXT = 0x80C8;
        public const uint GL_BLEND_SRC_RGB_EXT = 0x80C9;
        public const uint GL_BLEND_DST_ALPHA_EXT = 0x80CA;
        public const uint GL_BLEND_SRC_ALPHA_EXT = 0x80CB;

        #endregion

        #region GL_EXT_stencil_wrap

        //  Constants
        public const uint GL_INCR_WRAP_EXT = 0x8507;
        public const uint GL_DECR_WRAP_EXT = 0x8508;

        #endregion

        #region GL_ARB_texture_env_crossbar

        //  No methods or constants.

        #endregion

        #region GL_EXT_texture_lod_bias

        //  Constants
        public const uint GL_MAX_TEXTURE_LOD_BIAS_EXT = 0x84FD;
        public const uint GL_TEXTURE_FILTER_CONTROL_EXT = 0x8500;
        public const uint GL_TEXTURE_LOD_BIAS_EXT = 0x8501;

        #endregion

        #region GL_ARB_texture_mirrored_repeat

        //  Constants
        public const uint GL_MIRRORED_REPEAT_ARB = 0x8370;

        #endregion

        #region GL_ARB_window_pos

        //  Methods
        public void WindowPos2ARB(double x, double y)
        {
            InvokeExtensionFunction<glWindowPos2dARB>(x, y);
        }
        public void WindowPos2ARB(double[] v)
        {
            InvokeExtensionFunction<glWindowPos2dvARB>(v);
        }
        public void WindowPos2ARB(float x, float y)
        {
            InvokeExtensionFunction<glWindowPos2fARB>(x, y);
        }
        public void WindowPos2ARB(float[] v)
        {
            InvokeExtensionFunction<glWindowPos2fvARB>(v);
        }
        public void WindowPos2ARB(int x, int y)
        {
            InvokeExtensionFunction<glWindowPos2iARB>(x, y);
        }
        public void WindowPos2ARB(int[] v)
        {
            InvokeExtensionFunction<glWindowPos2ivARB>(v);
        }
        public void WindowPos2ARB(short x, short y)
        {
            InvokeExtensionFunction<glWindowPos2sARB>(x, y);
        }
        public void WindowPos2ARB(short[] v)
        {
            InvokeExtensionFunction<glWindowPos2svARB>(v);
        }
        public void WindowPos3ARB(double x, double y, double z)
        {
            InvokeExtensionFunction<glWindowPos3dARB>(x, y, z);
        }
        public void WindowPos3ARB(double[] v)
        {
            InvokeExtensionFunction<glWindowPos3dvARB>(v);
        }
        public void WindowPos3ARB(float x, float y, float z)
        {
            InvokeExtensionFunction<glWindowPos3fARB>(x, y, z);
        }
        public void WindowPos3ARB(float[] v)
        {
            InvokeExtensionFunction<glWindowPos3fvARB>(v);
        }
        public void WindowPos3ARB(int x, int y, int z)
        {
            InvokeExtensionFunction<glWindowPos3iARB>(x, y, z);
        }
        public void WindowPos3ARB(int[] v)
        {
            InvokeExtensionFunction<glWindowPos3ivARB>(v);
        }
        public void WindowPos3ARB(short x, short y, short z)
        {
            InvokeExtensionFunction<glWindowPos3sARB>(x, y, z);
        }
        public void WindowPos3ARB(short[] v)
        {
            InvokeExtensionFunction<glWindowPos3svARB>(v);
        }

        //  Delegates
        private delegate void glWindowPos2dARB(double x, double y);
        private delegate void glWindowPos2dvARB(double[] v);
        private delegate void glWindowPos2fARB(float x, float y);
        private delegate void glWindowPos2fvARB(float[] v);
        private delegate void glWindowPos2iARB(int x, int y);
        private delegate void glWindowPos2ivARB(int[] v);
        private delegate void glWindowPos2sARB(short x, short y);
        private delegate void glWindowPos2svARB(short[] v);
        private delegate void glWindowPos3dARB(double x, double y, double z);
        private delegate void glWindowPos3dvARB(double[] v);
        private delegate void glWindowPos3fARB(float x, float y, float z);
        private delegate void glWindowPos3fvARB(float[] v);
        private delegate void glWindowPos3iARB(int x, int y, int z);
        private delegate void glWindowPos3ivARB(int[] v);
        private delegate void glWindowPos3sARB(short x, short y, short z);
        private delegate void glWindowPos3svARB(short[] v);

        #endregion

        #region GL_ARB_vertex_buffer_object

        //  Methods
        public void BindBufferARB(uint target, uint buffer)
        {
            InvokeExtensionFunction<glBindBufferARB>(target, buffer);
        }
        public void DeleteBuffersARB(int n, uint[] buffers)
        {
            InvokeExtensionFunction<glDeleteBuffersARB>(n, buffers);
        }
        public void GenBuffersARB(int n, uint[] buffers)
        {
            InvokeExtensionFunction<glGenBuffersARB>(n, buffers);
        }
        public bool IsBufferARB(uint buffer)
        {
            return (bool)InvokeExtensionFunction<glIsBufferARB>(buffer);
        }
        public void BufferDataARB(uint target, uint size, IntPtr data, uint usage)
        {
            InvokeExtensionFunction<glBufferDataARB>(target, size, data, usage);
        }
        public void BufferSubDataARB(uint target, uint offset, uint size, IntPtr data)
        {
            InvokeExtensionFunction<glBufferSubDataARB>(target, offset, size, data);
        }
        public void GetBufferSubDataARB(uint target, uint offset, uint size, IntPtr data)
        {
            InvokeExtensionFunction<glGetBufferSubDataARB>(target, offset, size, data);
        }
        public IntPtr MapBufferARB(uint target, uint access)
        {
            return (IntPtr)InvokeExtensionFunction<glMapBufferARB>(target, access);
        }
        public bool UnmapBufferARB(uint target)
        {
            return (bool)InvokeExtensionFunction<glUnmapBufferARB>(target);
        }
        public void GetBufferParameterARB(uint target, uint pname, int[] parameters)
        {
            InvokeExtensionFunction<glGetBufferParameterivARB>(target, pname, parameters);
        }
        public void GetBufferPointerARB(uint target, uint pname, IntPtr parameters)
        {
            InvokeExtensionFunction<glGetBufferPointervARB>(target, pname, parameters);
        }

        //  Delegates
        private delegate void glBindBufferARB(uint target, uint buffer);
        private delegate void glDeleteBuffersARB(int n, uint[] buffers);
        private delegate void glGenBuffersARB(int n, uint[] buffers);
        private delegate bool glIsBufferARB(uint buffer);
        private delegate void glBufferDataARB(uint target, uint size, IntPtr data, uint usage);
        private delegate void glBufferSubDataARB(uint target, uint offset, uint size, IntPtr data);
        private delegate void glGetBufferSubDataARB(uint target, uint offset, uint size, IntPtr data);
        private delegate IntPtr glMapBufferARB(uint target, uint access);
        private delegate bool glUnmapBufferARB(uint target);
        private delegate void glGetBufferParameterivARB(uint target, uint pname, int[] parameters);
        private delegate void glGetBufferPointervARB(uint target, uint pname, IntPtr parameters);

        //  Constants
        public const uint GL_BUFFER_SIZE_ARB = 0x8764;
        public const uint GL_BUFFER_USAGE_ARB = 0x8765;
        public const uint GL_ARRAY_BUFFER_ARB = 0x8892;
        public const uint GL_ELEMENT_ARRAY_BUFFER_ARB = 0x8893;
        public const uint GL_ARRAY_BUFFER_BINDING_ARB = 0x8894;
        public const uint GL_ELEMENT_ARRAY_BUFFER_BINDING_ARB = 0x8895;
        public const uint GL_VERTEX_ARRAY_BUFFER_BINDING_ARB = 0x8896;
        public const uint GL_NORMAL_ARRAY_BUFFER_BINDING_ARB = 0x8897;
        public const uint GL_COLOR_ARRAY_BUFFER_BINDING_ARB = 0x8898;
        public const uint GL_INDEX_ARRAY_BUFFER_BINDING_ARB = 0x8899;
        public const uint GL_TEXTURE_COORD_ARRAY_BUFFER_BINDING_ARB = 0x889A;
        public const uint GL_EDGE_FLAG_ARRAY_BUFFER_BINDING_ARB = 0x889B;
        public const uint GL_SECONDARY_COLOR_ARRAY_BUFFER_BINDING_ARB = 0x889C;
        public const uint GL_FOG_COORDINATE_ARRAY_BUFFER_BINDING_ARB = 0x889D;
        public const uint GL_WEIGHT_ARRAY_BUFFER_BINDING_ARB = 0x889E;
        public const uint GL_VERTEX_ATTRIB_ARRAY_BUFFER_BINDING_ARB = 0x889F;
        public const uint GL_READ_ONLY_ARB = 0x88B8;
        public const uint GL_WRITE_ONLY_ARB = 0x88B9;
        public const uint GL_READ_WRITE_ARB = 0x88BA;
        public const uint GL_BUFFER_ACCESS_ARB = 0x88BB;
        public const uint GL_BUFFER_MAPPED_ARB = 0x88BC;
        public const uint GL_BUFFER_MAP_POINTER_ARB = 0x88BD;
        public const uint GL_STREAM_DRAW_ARB = 0x88E0;
        public const uint GL_STREAM_READ_ARB = 0x88E1;
        public const uint GL_STREAM_COPY_ARB = 0x88E2;
        public const uint GL_STATIC_DRAW_ARB = 0x88E4;
        public const uint GL_STATIC_READ_ARB = 0x88E5;
        public const uint GL_STATIC_COPY_ARB = 0x88E6;
        public const uint GL_DYNAMIC_DRAW_ARB = 0x88E8;
        public const uint GL_DYNAMIC_READ_ARB = 0x88E9;
        public const uint GL_DYNAMIC_COPY_ARB = 0x88EA;
        #endregion

        #region GL_ARB_occlusion_query

        //  Methods
        public void GenQueriesARB(int n, uint[] ids)
        {
            InvokeExtensionFunction<glGenQueriesARB>(n, ids);
        }
        public void DeleteQueriesARB(int n, uint[] ids)
        {
            InvokeExtensionFunction<glDeleteQueriesARB>(n, ids);
        }
        public bool IsQueryARB(uint id)
        {
            return (bool)InvokeExtensionFunction<glIsQueryARB>(id);
        }
        public void BeginQueryARB(uint target, uint id)
        {
            InvokeExtensionFunction<glBeginQueryARB>(target, id);
        }
        public void EndQueryARB(uint target)
        {
            InvokeExtensionFunction<glEndQueryARB>(target);
        }
        public void GetQueryARB(uint target, uint pname, int[] parameters)
        {
            InvokeExtensionFunction<glGetQueryivARB>(target, pname, parameters);
        }
        public void GetQueryObjectARB(uint id, uint pname, int[] parameters)
        {
            InvokeExtensionFunction<glGetQueryObjectivARB>(id, pname, parameters);
        }
        public void GetQueryObjectARB(uint id, uint pname, uint[] parameters)
        {
            InvokeExtensionFunction<glGetQueryObjectuivARB>(id, pname, parameters);
        }

        //  Delegates
        private delegate void glGenQueriesARB(int n, uint[] ids);
        private delegate void glDeleteQueriesARB(int n, uint[] ids);
        private delegate bool glIsQueryARB(uint id);
        private delegate void glBeginQueryARB(uint target, uint id);
        private delegate void glEndQueryARB(uint target);
        private delegate void glGetQueryivARB(uint target, uint pname, int[] parameters);
        private delegate void glGetQueryObjectivARB(uint id, uint pname, int[] parameters);
        private delegate void glGetQueryObjectuivARB(uint id, uint pname, uint[] parameters);

        //  Constants
        public const uint GL_QUERY_COUNTER_BITS_ARB = 0x8864;
        public const uint GL_CURRENT_QUERY_ARB = 0x8865;
        public const uint GL_QUERY_RESULT_ARB = 0x8866;
        public const uint GL_QUERY_RESULT_AVAILABLE_ARB = 0x8867;
        public const uint GL_SAMPLES_PASSED_ARB = 0x8914;
        public const uint GL_ANY_SAMPLES_PASSED = 0x8C2F;

        #endregion

        #region GL_ARB_shader_objects

        //  Methods
        public void DeleteObjectARB(uint obj)
        {
            InvokeExtensionFunction<glDeleteObjectARB>(obj);
        }
        public uint GetHandleARB(uint pname)
        {
            return (uint)InvokeExtensionFunction<glGetHandleARB>(pname);
        }
        public void DetachObjectARB(uint containerObj, uint attachedObj)
        {
            InvokeExtensionFunction<glDetachObjectARB>(containerObj, attachedObj);
        }
        public uint CreateShaderObjectARB(uint shaderType)
        {
            return (uint)InvokeExtensionFunction<glCreateShaderObjectARB>(shaderType);
        }
        public void ShaderSourceARB(uint shaderObj, int count, string[] source, ref int length)
        {
            InvokeExtensionFunction<glShaderSourceARB>(shaderObj, count, source, length);
        }
        public void CompileShaderARB(uint shaderObj)
        {
            InvokeExtensionFunction<glCompileShaderARB>(shaderObj);
        }
        public uint CreateProgramObjectARB()
        {
            return (uint)InvokeExtensionFunction<glCreateProgramObjectARB>();
        }
        public void AttachObjectARB(uint containerObj, uint obj)
        {
            InvokeExtensionFunction<glAttachObjectARB>(containerObj, obj);
        }
        public void LinkProgramARB(uint programObj)
        {
            InvokeExtensionFunction<glLinkProgramARB>(programObj);
        }
        public void UseProgramObjectARB(uint programObj)
        {
            InvokeExtensionFunction<glUseProgramObjectARB>(programObj);
        }
        public void ValidateProgramARB(uint programObj)
        {
            InvokeExtensionFunction<glValidateProgramARB>(programObj);
        }
        public void Uniform1ARB(int location, float v0)
        {
            InvokeExtensionFunction<glUniform1fARB>(location, v0);
        }
        public void Uniform2ARB(int location, float v0, float v1)
        {
            InvokeExtensionFunction<glUniform2fARB>(location, v0, v1);
        }
        public void Uniform3ARB(int location, float v0, float v1, float v2)
        {
            InvokeExtensionFunction<glUniform3fARB>(location, v0, v1, v2);
        }
        public void Uniform4ARB(int location, float v0, float v1, float v2, float v3)
        {
            InvokeExtensionFunction<glUniform4fARB>(location, v0, v1, v2, v3);
        }
        public void Uniform1ARB(int location, int v0)
        {
            InvokeExtensionFunction<glUniform1iARB>(location, v0);
        }
        public void Uniform2ARB(int location, int v0, int v1)
        {
            InvokeExtensionFunction<glUniform2iARB>(location, v0, v1);
        }
        public void Uniform3ARB(int location, int v0, int v1, int v2)
        {
            InvokeExtensionFunction<glUniform3iARB>(location, v0, v1, v2);
        }
        public void Uniform4ARB(int location, int v0, int v1, int v2, int v3)
        {
            InvokeExtensionFunction<glUniform4iARB>(location, v0, v1, v2, v3);
        }
        public void Uniform1ARB(int location, int count, float[] value)
        {
            InvokeExtensionFunction<glUniform1fvARB>(location, count, value);
        }
        public void Uniform2ARB(int location, int count, float[] value)
        {
            InvokeExtensionFunction<glUniform2fvARB>(location, count, value);
        }
        public void Uniform3ARB(int location, int count, float[] value)
        {
            InvokeExtensionFunction<glUniform3fvARB>(location, count, value);
        }
        public void Uniform4ARB(int location, int count, float[] value)
        {
            InvokeExtensionFunction<glUniform4fvARB>(location, count, value);
        }
        public void Uniform1ARB(int location, int count, int[] value)
        {
            InvokeExtensionFunction<glUniform1ivARB>(location, count, value);
        }
        public void Uniform2ARB(int location, int count, int[] value)
        {
            InvokeExtensionFunction<glUniform2ivARB>(location, count, value);
        }
        public void Uniform3ARB(int location, int count, int[] value)
        {
            InvokeExtensionFunction<glUniform3ivARB>(location, count, value);
        }
        public void Uniform4ARB(int location, int count, int[] value)
        {
            InvokeExtensionFunction<glUniform4ivARB>(location, count, value);
        }
        public void UniformMatrix2ARB(int location, int count, bool transpose, float[] value)
        {
            InvokeExtensionFunction<glUniformMatrix2fvARB>(location, count, transpose, value);
        }
        public void UniformMatrix3ARB(int location, int count, bool transpose, float[] value)
        {
            InvokeExtensionFunction<glUniformMatrix3fvARB>(location, count, transpose, value);
        }
        public void UniformMatrix4ARB(int location, int count, bool transpose, float[] value)
        {
            InvokeExtensionFunction<glUniformMatrix4fvARB>(location, count, transpose, value);
        }
        public void GetObjectParameterARB(uint obj, uint pname, float[] parameters)
        {
            InvokeExtensionFunction<glGetObjectParameterfvARB>(obj, pname, parameters);
        }
        public void GetObjectParameterARB(uint obj, uint pname, int[] parameters)
        {
            InvokeExtensionFunction<glGetObjectParameterivARB>(obj, pname, parameters);
        }
        public void GetInfoLogARB(uint obj, int maxLength, ref int length, string infoLog)
        {
            InvokeExtensionFunction<glGetInfoLogARB>(obj, maxLength, length, infoLog);
        }
        public void GetAttachedObjectsARB(uint containerObj, int maxCount, ref int count, ref uint obj)
        {
            InvokeExtensionFunction<glGetAttachedObjectsARB>(containerObj, maxCount, count, obj);
        }
        public int GetUniformLocationARB(uint programObj, string name)
        {
            return (int)InvokeExtensionFunction<glGetUniformLocationARB>(programObj, name);
        }
        public void GetActiveUniformARB(uint programObj, uint index, int maxLength, ref int length, ref int size, ref uint type, string name)
        {
            InvokeExtensionFunction<glGetActiveUniformARB>(programObj, index, maxLength, length, size, type, name);
        }
        public void GetUniformARB(uint programObj, int location, float[] parameters)
        {
            InvokeExtensionFunction<glGetUniformfvARB>(programObj, location, parameters);
        }
        public void GetUniformARB(uint programObj, int location, int[] parameters)
        {
            InvokeExtensionFunction<glGetUniformivARB>(programObj, location, parameters);
        }
        public void GetShaderSourceARB(uint obj, int maxLength, ref int length, string source)
        {
            InvokeExtensionFunction<glGetShaderSourceARB>(obj, maxLength, length, source);
        }

        //  Delegates
        private delegate void glDeleteObjectARB(uint obj);
        private delegate uint glGetHandleARB(uint pname);
        private delegate void glDetachObjectARB(uint containerObj, uint attachedObj);
        private delegate uint glCreateShaderObjectARB(uint shaderType);
        private delegate void glShaderSourceARB(uint shaderObj, int count, string[] source, ref int length);
        private delegate void glCompileShaderARB(uint shaderObj);
        private delegate uint glCreateProgramObjectARB();
        private delegate void glAttachObjectARB(uint containerObj, uint obj);
        private delegate void glLinkProgramARB(uint programObj);
        private delegate void glUseProgramObjectARB(uint programObj);
        private delegate void glValidateProgramARB(uint programObj);
        private delegate void glUniform1fARB(int location, float v0);
        private delegate void glUniform2fARB(int location, float v0, float v1);
        private delegate void glUniform3fARB(int location, float v0, float v1, float v2);
        private delegate void glUniform4fARB(int location, float v0, float v1, float v2, float v3);
        private delegate void glUniform1iARB(int location, int v0);
        private delegate void glUniform2iARB(int location, int v0, int v1);
        private delegate void glUniform3iARB(int location, int v0, int v1, int v2);
        private delegate void glUniform4iARB(int location, int v0, int v1, int v2, int v3);
        private delegate void glUniform1fvARB(int location, int count, float[] value);
        private delegate void glUniform2fvARB(int location, int count, float[] value);
        private delegate void glUniform3fvARB(int location, int count, float[] value);
        private delegate void glUniform4fvARB(int location, int count, float[] value);
        private delegate void glUniform1ivARB(int location, int count, int[] value);
        private delegate void glUniform2ivARB(int location, int count, int[] value);
        private delegate void glUniform3ivARB(int location, int count, int[] value);
        private delegate void glUniform4ivARB(int location, int count, int[] value);
        private delegate void glUniformMatrix2fvARB(int location, int count, bool transpose, float[] value);
        private delegate void glUniformMatrix3fvARB(int location, int count, bool transpose, float[] value);
        private delegate void glUniformMatrix4fvARB(int location, int count, bool transpose, float[] value);
        private delegate void glGetObjectParameterfvARB(uint obj, uint pname, float[] parameters);
        private delegate void glGetObjectParameterivARB(uint obj, uint pname, int[] parameters);
        private delegate void glGetInfoLogARB(uint obj, int maxLength, ref int length, string infoLog);
        private delegate void glGetAttachedObjectsARB(uint containerObj, int maxCount, ref int count, ref uint obj);
        private delegate int glGetUniformLocationARB(uint programObj, string name);
        private delegate void glGetActiveUniformARB(uint programObj, uint index, int maxLength, ref int length, ref int size, ref uint type, string name);
        private delegate void glGetUniformfvARB(uint programObj, int location, float[] parameters);
        private delegate void glGetUniformivARB(uint programObj, int location, int[] parameters);
        private delegate void glGetShaderSourceARB(uint obj, int maxLength, ref int length, string source);

        //  Constants
        public const uint GL_PROGRAM_OBJECT_ARB = 0x8B40;
        public const uint GL_SHADER_OBJECT_ARB = 0x8B48;
        public const uint GL_OBJECT_TYPE_ARB = 0x8B4E;
        public const uint GL_OBJECT_SUBTYPE_ARB = 0x8B4F;
        public const uint GL_FLOAT_VEC2_ARB = 0x8B50;
        public const uint GL_FLOAT_VEC3_ARB = 0x8B51;
        public const uint GL_FLOAT_VEC4_ARB = 0x8B52;
        public const uint GL_INT_VEC2_ARB = 0x8B53;
        public const uint GL_INT_VEC3_ARB = 0x8B54;
        public const uint GL_INT_VEC4_ARB = 0x8B55;
        public const uint GL_BOOL_ARB = 0x8B56;
        public const uint GL_BOOL_VEC2_ARB = 0x8B57;
        public const uint GL_BOOL_VEC3_ARB = 0x8B58;
        public const uint GL_BOOL_VEC4_ARB = 0x8B59;
        public const uint GL_FLOAT_MAT2_ARB = 0x8B5A;
        public const uint GL_FLOAT_MAT3_ARB = 0x8B5B;
        public const uint GL_FLOAT_MAT4_ARB = 0x8B5C;
        public const uint GL_SAMPLER_1D_ARB = 0x8B5D;
        public const uint GL_SAMPLER_2D_ARB = 0x8B5E;
        public const uint GL_SAMPLER_3D_ARB = 0x8B5F;
        public const uint GL_SAMPLER_CUBE_ARB = 0x8B60;
        public const uint GL_SAMPLER_1D_SHADOW_ARB = 0x8B61;
        public const uint GL_SAMPLER_2D_SHADOW_ARB = 0x8B62;
        public const uint GL_SAMPLER_2D_RECT_ARB = 0x8B63;
        public const uint GL_SAMPLER_2D_RECT_SHADOW_ARB = 0x8B64;
        public const uint GL_OBJECT_DELETE_STATUS_ARB = 0x8B80;
        public const uint GL_OBJECT_COMPILE_STATUS_ARB = 0x8B81;
        public const uint GL_OBJECT_LINK_STATUS_ARB = 0x8B82;
        public const uint GL_OBJECT_VALIDATE_STATUS_ARB = 0x8B83;
        public const uint GL_OBJECT_INFO_LOG_LENGTH_ARB = 0x8B84;
        public const uint GL_OBJECT_ATTACHED_OBJECTS_ARB = 0x8B85;
        public const uint GL_OBJECT_ACTIVE_UNIFORMS_ARB = 0x8B86;
        public const uint GL_OBJECT_ACTIVE_UNIFORM_MAX_LENGTH_ARB = 0x8B87;
        public const uint GL_OBJECT_SHADER_SOURCE_LENGTH_ARB = 0x8B88;

        #endregion

        #region GL_ARB_vertex_program

        //  Methods
        public void VertexAttrib1ARB(uint index, double x)
        {
            InvokeExtensionFunction<glVertexAttrib1dARB>(index, x);
        }
        public void VertexAttrib1ARB(uint index, double[] v)
        {
            InvokeExtensionFunction<glVertexAttrib1dvARB>(index, v);
        }
        public void VertexAttrib1ARB(uint index, float x)
        {
            InvokeExtensionFunction<glVertexAttrib1fARB>(index, x);
        }
        public void VertexAttrib1ARB(uint index, float[] v)
        {
            InvokeExtensionFunction<glVertexAttrib1fvARB>(index, v);
        }
        public void VertexAttrib1ARB(uint index, short x)
        {
            InvokeExtensionFunction<glVertexAttrib1sARB>(index, x);
        }
        public void VertexAttrib1ARB(uint index, short[] v)
        {
            InvokeExtensionFunction<glVertexAttrib1svARB>(index, v);
        }
        public void VertexAttrib2ARB(uint index, double x, double y)
        {
            InvokeExtensionFunction<glVertexAttrib2dARB>(index, x, y);
        }
        public void VertexAttrib2ARB(uint index, double[] v)
        {
            InvokeExtensionFunction<glVertexAttrib2dvARB>(index, v);
        }
        public void VertexAttrib2ARB(uint index, float x, float y)
        {
            InvokeExtensionFunction<glVertexAttrib2fARB>(index, x, y);
        }
        public void VertexAttrib2ARB(uint index, float[] v)
        {
            InvokeExtensionFunction<glVertexAttrib2fvARB>(index, v);
        }
        public void VertexAttrib2ARB(uint index, short x, short y)
        {
            InvokeExtensionFunction<glVertexAttrib2sARB>(index, x, y);
        }
        public void VertexAttrib2ARB(uint index, short[] v)
        {
            InvokeExtensionFunction<glVertexAttrib2svARB>(index, v);
        }
        public void VertexAttrib3ARB(uint index, double x, double y, double z)
        {
            InvokeExtensionFunction<glVertexAttrib3dARB>(index, x, y, z);
        }
        public void VertexAttrib3ARB(uint index, double[] v)
        {
            InvokeExtensionFunction<glVertexAttrib3dvARB>(index, v);
        }
        public void VertexAttrib3ARB(uint index, float x, float y, float z)
        {
            InvokeExtensionFunction<glVertexAttrib3fARB>(index, x, y, z);
        }
        public void VertexAttrib3ARB(uint index, float[] v)
        {
            InvokeExtensionFunction<glVertexAttrib3fvARB>(index, v);
        }
        public void VertexAttrib3ARB(uint index, short x, short y, short z)
        {
            InvokeExtensionFunction<glVertexAttrib3sARB>(index, x, y, z);
        }
        public void VertexAttrib3ARB(uint index, short[] v)
        {
            InvokeExtensionFunction<glVertexAttrib3svARB>(index, v);
        }
        public void VertexAttrib4NARB(uint index, sbyte[] v)
        {
            InvokeExtensionFunction<glVertexAttrib4NbvARB>(index, v);
        }
        public void VertexAttrib4NARB(uint index, int[] v)
        {
            InvokeExtensionFunction<glVertexAttrib4NivARB>(index, v);
        }
        public void VertexAttrib4NARB(uint index, short[] v)
        {
            InvokeExtensionFunction<glVertexAttrib4NsvARB>(index, v);
        }
        public void VertexAttrib4NARB(uint index, byte x, byte y, byte z, byte w)
        {
            InvokeExtensionFunction<glVertexAttrib4NubARB>(index, x, y, z, w);
        }
        public void VertexAttrib4NARB(uint index, byte[] v)
        {
            InvokeExtensionFunction<glVertexAttrib4NubvARB>(index, v);
        }
        public void VertexAttrib4NARB(uint index, uint[] v)
        {
            InvokeExtensionFunction<glVertexAttrib4NuivARB>(index, v);
        }
        public void VertexAttrib4NARB(uint index, ushort[] v)
        {
            InvokeExtensionFunction<glVertexAttrib4NusvARB>(index, v);
        }
        public void VertexAttrib4ARB(uint index, sbyte[] v)
        {
            InvokeExtensionFunction<glVertexAttrib4bvARB>(index, v);
        }
        public void VertexAttrib4ARB(uint index, double x, double y, double z, double w)
        {
            InvokeExtensionFunction<glVertexAttrib4dARB>(index, x, y, z, w);
        }
        public void VertexAttrib4ARB(uint index, double[] v)
        {
            InvokeExtensionFunction<glVertexAttrib4dvARB>(index, v);
        }
        public void VertexAttrib4ARB(uint index, float x, float y, float z, float w)
        {
            InvokeExtensionFunction<glVertexAttrib4fARB>(index, x, y, z, w);
        }
        public void VertexAttrib4ARB(uint index, float[] v)
        {
            InvokeExtensionFunction<glVertexAttrib4fvARB>(index, v);
        }
        public void VertexAttrib4ARB(uint index, int[] v)
        {
            InvokeExtensionFunction<glVertexAttrib4ivARB>(index, v);
        }
        public void VertexAttrib4ARB(uint index, short x, short y, short z, short w)
        {
            InvokeExtensionFunction<glVertexAttrib4sARB>(index, x, y, z, w);
        }
        public void VertexAttrib4ARB(uint index, short[] v)
        {
            InvokeExtensionFunction<glVertexAttrib4svARB>(index, v);
        }
        public void VertexAttrib4ARB(uint index, byte[] v)
        {
            InvokeExtensionFunction<glVertexAttrib4ubvARB>(index, v);
        }
        public void VertexAttrib4ARB(uint index, uint[] v)
        {
            InvokeExtensionFunction<glVertexAttrib4uivARB>(index, v);
        }
        public void VertexAttrib4ARB(uint index, ushort[] v)
        {
            InvokeExtensionFunction<glVertexAttrib4usvARB>(index, v);
        }
        public void VertexAttribPointerARB(uint index, int size, uint type, bool normalized, int stride, IntPtr pointer)
        {
            InvokeExtensionFunction<glVertexAttribPointerARB>(index, size, type, normalized, stride, pointer);
        }
        public void EnableVertexAttribArrayARB(uint index)
        {
            InvokeExtensionFunction<glEnableVertexAttribArrayARB>(index);
        }
        public void DisableVertexAttribArrayARB(uint index)
        {
            InvokeExtensionFunction<glDisableVertexAttribArrayARB>(index);
        }
        public void ProgramStringARB(uint target, uint format, int len, IntPtr str)
        {
            InvokeExtensionFunction<glProgramStringARB>(target, format, len, str);
        }
        public void BindProgramARB(uint target, uint program)
        {
            InvokeExtensionFunction<glBindProgramARB>(target, program);
        }
        public void DeleteProgramsARB(int n, uint[] programs)
        {
            InvokeExtensionFunction<glDeleteProgramsARB>(n, programs);
        }
        public void GenProgramsARB(int n, uint[] programs)
        {
            InvokeExtensionFunction<glGenProgramsARB>(n, programs);
        }
        public void ProgramEnvParameter4ARB(uint target, uint index, double x, double y, double z, double w)
        {
            InvokeExtensionFunction<glProgramEnvParameter4dARB>(target, index, x, y, z, w);
        }
        public void ProgramEnvParameter4ARB(uint target, uint index, double[] parameters)
        {
            InvokeExtensionFunction<glProgramEnvParameter4dvARB>(target, index, parameters);
        }
        public void ProgramEnvParameter4ARB(uint target, uint index, float x, float y, float z, float w)
        {
            InvokeExtensionFunction<glProgramEnvParameter4fARB>(target, index, x, y, z, w);
        }
        public void ProgramEnvParameter4ARB(uint target, uint index, float[] parameters)
        {
            InvokeExtensionFunction<glProgramEnvParameter4fvARB>(target, index, parameters);
        }
        public void ProgramLocalParameter4ARB(uint target, uint index, double x, double y, double z, double w)
        {
            InvokeExtensionFunction<glProgramLocalParameter4dARB>(target, index, x, y, z, w);
        }
        public void ProgramLocalParameter4ARB(uint target, uint index, double[] parameters)
        {
            InvokeExtensionFunction<glProgramLocalParameter4dvARB>(target, index, parameters);
        }
        public void ProgramLocalParameter4ARB(uint target, uint index, float x, float y, float z, float w)
        {
            InvokeExtensionFunction<glProgramLocalParameter4fARB>(target, index, x, y, z, w);
        }
        public void ProgramLocalParameter4ARB(uint target, uint index, float[] parameters)
        {
            InvokeExtensionFunction<glProgramLocalParameter4fvARB>(target, index, parameters);
        }
        public void GetProgramEnvParameterdARB(uint target, uint index, double[] parameters)
        {
            InvokeExtensionFunction<glGetProgramEnvParameterdvARB>(target, index, parameters);
        }
        public void GetProgramEnvParameterfARB(uint target, uint index, float[] parameters)
        {
            InvokeExtensionFunction<glGetProgramEnvParameterfvARB>(target, index, parameters);
        }
        public void GetProgramLocalParameterARB(uint target, uint index, double[] parameters)
        {
            InvokeExtensionFunction<glGetProgramLocalParameterdvARB>(target, index, parameters);
        }
        public void GetProgramLocalParameterARB(uint target, uint index, float[] parameters)
        {
            InvokeExtensionFunction<glGetProgramLocalParameterfvARB>(target, index, parameters);
        }
        public void GetProgramARB(uint target, uint pname, int[] parameters)
        {
            InvokeExtensionFunction<glGetProgramivARB>(target, pname, parameters);
        }
        public void GetProgramStringARB(uint target, uint pname, IntPtr str)
        {
            InvokeExtensionFunction<glGetProgramStringARB>(target, pname, str);
        }
        public void GetVertexAttribARB(uint index, uint pname, double[] parameters)
        {
            InvokeExtensionFunction<glGetVertexAttribdvARB>(index, pname, parameters);
        }
        public void GetVertexAttribARB(uint index, uint pname, float[] parameters)
        {
            InvokeExtensionFunction<glGetVertexAttribfvARB>(index, pname, parameters);
        }
        public void GetVertexAttribARB(uint index, uint pname, int[] parameters)
        {
            InvokeExtensionFunction<glGetVertexAttribivARB>(index, pname, parameters);
        }
        public void GetVertexAttribPointerARB(uint index, uint pname, IntPtr pointer)
        {
            InvokeExtensionFunction<glGetVertexAttribPointervARB>(index, pname, pointer);
        }

        //  Delegates
        private delegate void glVertexAttrib1dARB(uint index, double x);
        private delegate void glVertexAttrib1dvARB(uint index, double[] v);
        private delegate void glVertexAttrib1fARB(uint index, float x);
        private delegate void glVertexAttrib1fvARB(uint index, float[] v);
        private delegate void glVertexAttrib1sARB(uint index, short x);
        private delegate void glVertexAttrib1svARB(uint index, short[] v);
        private delegate void glVertexAttrib2dARB(uint index, double x, double y);
        private delegate void glVertexAttrib2dvARB(uint index, double[] v);
        private delegate void glVertexAttrib2fARB(uint index, float x, float y);
        private delegate void glVertexAttrib2fvARB(uint index, float[] v);
        private delegate void glVertexAttrib2sARB(uint index, short x, short y);
        private delegate void glVertexAttrib2svARB(uint index, short[] v);
        private delegate void glVertexAttrib3dARB(uint index, double x, double y, double z);
        private delegate void glVertexAttrib3dvARB(uint index, double[] v);
        private delegate void glVertexAttrib3fARB(uint index, float x, float y, float z);
        private delegate void glVertexAttrib3fvARB(uint index, float[] v);
        private delegate void glVertexAttrib3sARB(uint index, short x, short y, short z);
        private delegate void glVertexAttrib3svARB(uint index, short[] v);
        private delegate void glVertexAttrib4NbvARB(uint index, sbyte[] v);
        private delegate void glVertexAttrib4NivARB(uint index, int[] v);
        private delegate void glVertexAttrib4NsvARB(uint index, short[] v);
        private delegate void glVertexAttrib4NubARB(uint index, byte x, byte y, byte z, byte w);
        private delegate void glVertexAttrib4NubvARB(uint index, byte[] v);
        private delegate void glVertexAttrib4NuivARB(uint index, uint[] v);
        private delegate void glVertexAttrib4NusvARB(uint index, ushort[] v);
        private delegate void glVertexAttrib4bvARB(uint index, sbyte[] v);
        private delegate void glVertexAttrib4dARB(uint index, double x, double y, double z, double w);
        private delegate void glVertexAttrib4dvARB(uint index, double[] v);
        private delegate void glVertexAttrib4fARB(uint index, float x, float y, float z, float w);
        private delegate void glVertexAttrib4fvARB(uint index, float[] v);
        private delegate void glVertexAttrib4ivARB(uint index, int[] v);
        private delegate void glVertexAttrib4sARB(uint index, short x, short y, short z, short w);
        private delegate void glVertexAttrib4svARB(uint index, short[] v);
        private delegate void glVertexAttrib4ubvARB(uint index, byte[] v);
        private delegate void glVertexAttrib4uivARB(uint index, uint[] v);
        private delegate void glVertexAttrib4usvARB(uint index, ushort[] v);
        private delegate void glVertexAttribPointerARB(uint index, int size, uint type, bool normalized, int stride, IntPtr pointer);
        private delegate void glEnableVertexAttribArrayARB(uint index);
        private delegate void glDisableVertexAttribArrayARB(uint index);
        private delegate void glProgramStringARB(uint target, uint format, int len, IntPtr str);
        private delegate void glBindProgramARB(uint target, uint program);
        private delegate void glDeleteProgramsARB(int n, uint[] programs);
        private delegate void glGenProgramsARB(int n, uint[] programs);
        private delegate void glProgramEnvParameter4dARB(uint target, uint index, double x, double y, double z, double w);
        private delegate void glProgramEnvParameter4dvARB(uint target, uint index, double[] parameters);
        private delegate void glProgramEnvParameter4fARB(uint target, uint index, float x, float y, float z, float w);
        private delegate void glProgramEnvParameter4fvARB(uint target, uint index, float[] parameters);
        private delegate void glProgramLocalParameter4dARB(uint target, uint index, double x, double y, double z, double w);
        private delegate void glProgramLocalParameter4dvARB(uint target, uint index, double[] parameters);
        private delegate void glProgramLocalParameter4fARB(uint target, uint index, float x, float y, float z, float w);
        private delegate void glProgramLocalParameter4fvARB(uint target, uint index, float[] parameters);
        private delegate void glGetProgramEnvParameterdvARB(uint target, uint index, double[] parameters);
        private delegate void glGetProgramEnvParameterfvARB(uint target, uint index, float[] parameters);
        private delegate void glGetProgramLocalParameterdvARB(uint target, uint index, double[] parameters);
        private delegate void glGetProgramLocalParameterfvARB(uint target, uint index, float[] parameters);
        private delegate void glGetProgramivARB(uint target, uint pname, int[] parameters);
        private delegate void glGetProgramStringARB(uint target, uint pname, IntPtr str);
        private delegate void glGetVertexAttribdvARB(uint index, uint pname, double[] parameters);
        private delegate void glGetVertexAttribfvARB(uint index, uint pname, float[] parameters);
        private delegate void glGetVertexAttribivARB(uint index, uint pname, int[] parameters);
        private delegate void glGetVertexAttribPointervARB(uint index, uint pname, IntPtr pointer);

        //  Constants
        public const uint GL_COLOR_SUM_ARB = 0x8458;
        public const uint GL_VERTEX_PROGRAM_ARB = 0x8620;
        public const uint GL_VERTEX_ATTRIB_ARRAY_ENABLED_ARB = 0x8622;
        public const uint GL_VERTEX_ATTRIB_ARRAY_SIZE_ARB = 0x8623;
        public const uint GL_VERTEX_ATTRIB_ARRAY_STRIDE_ARB = 0x8624;
        public const uint GL_VERTEX_ATTRIB_ARRAY_TYPE_ARB = 0x8625;
        public const uint GL_CURRENT_VERTEX_ATTRIB_ARB = 0x8626;
        public const uint GL_PROGRAM_LENGTH_ARB = 0x8627;
        public const uint GL_PROGRAM_STRING_ARB = 0x8628;
        public const uint GL_MAX_PROGRAM_MATRIX_STACK_DEPTH_ARB = 0x862E;
        public const uint GL_MAX_PROGRAM_MATRICES_ARB = 0x862F;
        public const uint GL_CURRENT_MATRIX_STACK_DEPTH_ARB = 0x8640;
        public const uint GL_CURRENT_MATRIX_ARB = 0x8641;
        public const uint GL_VERTEX_PROGRAM_POINT_SIZE_ARB = 0x8642;
        public const uint GL_VERTEX_PROGRAM_TWO_SIDE_ARB = 0x8643;
        public const uint GL_VERTEX_ATTRIB_ARRAY_POINTER_ARB = 0x8645;
        public const uint GL_PROGRAM_ERROR_POSITION_ARB = 0x864B;
        public const uint GL_PROGRAM_BINDING_ARB = 0x8677;
        public const uint GL_MAX_VERTEX_ATTRIBS_ARB = 0x8869;
        public const uint GL_VERTEX_ATTRIB_ARRAY_NORMALIZED_ARB = 0x886A;
        public const uint GL_PROGRAM_ERROR_STRING_ARB = 0x8874;
        public const uint GL_PROGRAM_FORMAT_ASCII_ARB = 0x8875;
        public const uint GL_PROGRAM_FORMAT_ARB = 0x8876;
        public const uint GL_PROGRAM_INSTRUCTIONS_ARB = 0x88A0;
        public const uint GL_MAX_PROGRAM_INSTRUCTIONS_ARB = 0x88A1;
        public const uint GL_PROGRAM_NATIVE_INSTRUCTIONS_ARB = 0x88A2;
        public const uint GL_MAX_PROGRAM_NATIVE_INSTRUCTIONS_ARB = 0x88A3;
        public const uint GL_PROGRAM_TEMPORARIES_ARB = 0x88A4;
        public const uint GL_MAX_PROGRAM_TEMPORARIES_ARB = 0x88A5;
        public const uint GL_PROGRAM_NATIVE_TEMPORARIES_ARB = 0x88A6;
        public const uint GL_MAX_PROGRAM_NATIVE_TEMPORARIES_ARB = 0x88A7;
        public const uint GL_PROGRAM_PARAMETERS_ARB = 0x88A8;
        public const uint GL_MAX_PROGRAM_PARAMETERS_ARB = 0x88A9;
        public const uint GL_PROGRAM_NATIVE_PARAMETERS_ARB = 0x88AA;
        public const uint GL_MAX_PROGRAM_NATIVE_PARAMETERS_ARB = 0x88AB;
        public const uint GL_PROGRAM_ATTRIBS_ARB = 0x88AC;
        public const uint GL_MAX_PROGRAM_ATTRIBS_ARB = 0x88AD;
        public const uint GL_PROGRAM_NATIVE_ATTRIBS_ARB = 0x88AE;
        public const uint GL_MAX_PROGRAM_NATIVE_ATTRIBS_ARB = 0x88AF;
        public const uint GL_PROGRAM_ADDRESS_REGISTERS_ARB = 0x88B0;
        public const uint GL_MAX_PROGRAM_ADDRESS_REGISTERS_ARB = 0x88B1;
        public const uint GL_PROGRAM_NATIVE_ADDRESS_REGISTERS_ARB = 0x88B2;
        public const uint GL_MAX_PROGRAM_NATIVE_ADDRESS_REGISTERS_ARB = 0x88B3;
        public const uint GL_MAX_PROGRAM_LOCAL_PARAMETERS_ARB = 0x88B4;
        public const uint GL_MAX_PROGRAM_ENV_PARAMETERS_ARB = 0x88B5;
        public const uint GL_PROGRAM_UNDER_NATIVE_LIMITS_ARB = 0x88B6;
        public const uint GL_TRANSPOSE_CURRENT_MATRIX_ARB = 0x88B7;
        public const uint GL_MATRIX0_ARB = 0x88C0;
        public const uint GL_MATRIX1_ARB = 0x88C1;
        public const uint GL_MATRIX2_ARB = 0x88C2;
        public const uint GL_MATRIX3_ARB = 0x88C3;
        public const uint GL_MATRIX4_ARB = 0x88C4;
        public const uint GL_MATRIX5_ARB = 0x88C5;
        public const uint GL_MATRIX6_ARB = 0x88C6;
        public const uint GL_MATRIX7_ARB = 0x88C7;
        public const uint GL_MATRIX8_ARB = 0x88C8;
        public const uint GL_MATRIX9_ARB = 0x88C9;
        public const uint GL_MATRIX10_ARB = 0x88CA;
        public const uint GL_MATRIX11_ARB = 0x88CB;
        public const uint GL_MATRIX12_ARB = 0x88CC;
        public const uint GL_MATRIX13_ARB = 0x88CD;
        public const uint GL_MATRIX14_ARB = 0x88CE;
        public const uint GL_MATRIX15_ARB = 0x88CF;
        public const uint GL_MATRIX16_ARB = 0x88D0;
        public const uint GL_MATRIX17_ARB = 0x88D1;
        public const uint GL_MATRIX18_ARB = 0x88D2;
        public const uint GL_MATRIX19_ARB = 0x88D3;
        public const uint GL_MATRIX20_ARB = 0x88D4;
        public const uint GL_MATRIX21_ARB = 0x88D5;
        public const uint GL_MATRIX22_ARB = 0x88D6;
        public const uint GL_MATRIX23_ARB = 0x88D7;
        public const uint GL_MATRIX24_ARB = 0x88D8;
        public const uint GL_MATRIX25_ARB = 0x88D9;
        public const uint GL_MATRIX26_ARB = 0x88DA;
        public const uint GL_MATRIX27_ARB = 0x88DB;
        public const uint GL_MATRIX28_ARB = 0x88DC;
        public const uint GL_MATRIX29_ARB = 0x88DD;
        public const uint GL_MATRIX30_ARB = 0x88DE;
        public const uint GL_MATRIX31_ARB = 0x88DF;

        #endregion

        #region GL_ARB_vertex_shader

        //  Methods
        public void BindAttribLocationARB(uint programObj, uint index, string name)
        {
            InvokeExtensionFunction<glBindAttribLocationARB>(programObj, index, name);
        }
        public void GetActiveAttribARB(uint programObj, uint index, int maxLength, int[] length, int[] size, uint[] type, string name)
        {
            InvokeExtensionFunction<glGetActiveAttribARB>(programObj, index, maxLength, length, size, type, name);
        }
        public uint GetAttribLocationARB(uint programObj, string name)
        {
            return (uint)InvokeExtensionFunction<glGetAttribLocationARB>(programObj, name);
        }

        //  Delegates
        private delegate void glBindAttribLocationARB(uint programObj, uint index, string name);
        private delegate void glGetActiveAttribARB(uint programObj, uint index, int maxLength, int[] length, int[] size, uint[] type, string name);
        private delegate uint glGetAttribLocationARB(uint programObj, string name);

        //  Constants
        public const uint GL_VERTEX_SHADER_ARB = 0x8B31;
        public const uint GL_MAX_VERTEX_UNIFORM_COMPONENTS_ARB = 0x8B4A;
        public const uint GL_MAX_VARYING_FLOATS_ARB = 0x8B4B;
        public const uint GL_MAX_VERTEX_TEXTURE_IMAGE_UNITS_ARB = 0x8B4C;
        public const uint GL_MAX_COMBINED_TEXTURE_IMAGE_UNITS_ARB = 0x8B4D;
        public const uint GL_OBJECT_ACTIVE_ATTRIBUTES_ARB = 0x8B89;
        public const uint GL_OBJECT_ACTIVE_ATTRIBUTE_MAX_LENGTH_ARB = 0x8B8A;

        #endregion

        #region GL_ARB_fragment_shader

        public const uint GL_FRAGMENT_SHADER_ARB = 0x8B30;
        public const uint GL_MAX_FRAGMENT_UNIFORM_COMPONENTS_ARB = 0x8B49;
        public const uint GL_FRAGMENT_SHADER_DERIVATIVE_HINT_ARB = 0x8B8B;

        #endregion

        #region GL_ARB_draw_buffers

        //  Methods
        public void DrawBuffersARB(int n, uint[] bufs)
        {
            InvokeExtensionFunction<glDrawBuffersARB>(n, bufs);
        }

        //  Delegates
        private delegate void glDrawBuffersARB(int n, uint[] bufs);

        //  Constants        
        public const uint GL_MAX_DRAW_BUFFERS_ARB = 0x8824;
        public const uint GL_DRAW_BUFFER0_ARB = 0x8825;
        public const uint GL_DRAW_BUFFER1_ARB = 0x8826;
        public const uint GL_DRAW_BUFFER2_ARB = 0x8827;
        public const uint GL_DRAW_BUFFER3_ARB = 0x8828;
        public const uint GL_DRAW_BUFFER4_ARB = 0x8829;
        public const uint GL_DRAW_BUFFER5_ARB = 0x882A;
        public const uint GL_DRAW_BUFFER6_ARB = 0x882B;
        public const uint GL_DRAW_BUFFER7_ARB = 0x882C;
        public const uint GL_DRAW_BUFFER8_ARB = 0x882D;
        public const uint GL_DRAW_BUFFER9_ARB = 0x882E;
        public const uint GL_DRAW_BUFFER10_ARB = 0x882F;
        public const uint GL_DRAW_BUFFER11_ARB = 0x8830;
        public const uint GL_DRAW_BUFFER12_ARB = 0x8831;
        public const uint GL_DRAW_BUFFER13_ARB = 0x8832;
        public const uint GL_DRAW_BUFFER14_ARB = 0x8833;
        public const uint GL_DRAW_BUFFER15_ARB = 0x8834;

        #endregion

        #region GL_ARB_texture_non_power_of_two

        //  No methods or constants

        #endregion

        #region GL_ARB_texture_rectangle

        //  Constants
        public const uint GL_TEXTURE_RECTANGLE_ARB = 0x84F5;
        public const uint GL_TEXTURE_BINDING_RECTANGLE_ARB = 0x84F6;
        public const uint GL_PROXY_TEXTURE_RECTANGLE_ARB = 0x84F7;
        public const uint GL_MAX_RECTANGLE_TEXTURE_SIZE_ARB = 0x84F8;

        #endregion

        #region GL_ARB_point_sprite

        //  Constants
        public const uint GL_POINT_SPRITE_ARB = 0x8861;
        public const uint GL_COORD_REPLACE_ARB = 0x8862;

        #endregion

        #region GL_ARB_texture_float

        //  Constants
        public const uint GL_TEXTURE_RED_TYPE_ARB = 0x8C10;
        public const uint GL_TEXTURE_GREEN_TYPE_ARB = 0x8C11;
        public const uint GL_TEXTURE_BLUE_TYPE_ARB = 0x8C12;
        public const uint GL_TEXTURE_ALPHA_TYPE_ARB = 0x8C13;
        public const uint GL_TEXTURE_LUMINANCE_TYPE_ARB = 0x8C14;
        public const uint GL_TEXTURE_INTENSITY_TYPE_ARB = 0x8C15;
        public const uint GL_TEXTURE_DEPTH_TYPE_ARB = 0x8C16;
        public const uint GL_UNSIGNED_NORMALIZED_ARB = 0x8C17;
        public const uint GL_RGBA32F_ARB = 0x8814;
        public const uint GL_RGB32F_ARB = 0x8815;
        public const uint GL_ALPHA32F_ARB = 0x8816;
        public const uint GL_INTENSITY32F_ARB = 0x8817;
        public const uint GL_LUMINANCE32F_ARB = 0x8818;
        public const uint GL_LUMINANCE_ALPHA32F_ARB = 0x8819;
        public const uint GL_RGBA16F_ARB = 0x881A;
        public const uint GL_RGB16F_ARB = 0x881B;
        public const uint GL_ALPHA16F_ARB = 0x881C;
        public const uint GL_INTENSITY16F_ARB = 0x881D;
        public const uint GL_LUMINANCE16F_ARB = 0x881E;
        public const uint GL_LUMINANCE_ALPHA16F_ARB = 0x881F;

        #endregion

        #region GL_EXT_blend_equation_separate

        //  Methods
        public void BlendEquationSeparateEXT(uint modeRGB, uint modeAlpha)
        {
            InvokeExtensionFunction<glBlendEquationEXT>(modeRGB, modeAlpha);
        }

        //  Delegates
        private delegate void glBlendEquationSeparateEXT(uint modeRGB, uint modeAlpha);

        //  Constants
        public const uint GL_BLEND_EQUATION_RGB_EXT = 0x8009;
        public const uint GL_BLEND_EQUATION_ALPHA_EXT = 0x883D;

        #endregion

        #region GL_EXT_stencil_two_side

        //  Methods
        public void ActiveStencilFaceEXT(uint face)
        {
            InvokeExtensionFunction<glActiveStencilFaceEXT>(face);
        }

        //  Delegates
        private delegate void glActiveStencilFaceEXT(uint face);

        //  Constants
        public const uint GL_STENCIL_TEST_TWO_SIDE_EXT = 0x8009;
        public const uint GL_ACTIVE_STENCIL_FACE_EXT = 0x883D;

        #endregion

        #region GL_ARB_pixel_buffer_object

        public const uint GL_PIXEL_PACK_BUFFER_ARB = 0x88EB;
        public const uint GL_PIXEL_UNPACK_BUFFER_ARB = 0x88EC;
        public const uint GL_PIXEL_PACK_BUFFER_BINDING_ARB = 0x88ED;
        public const uint GL_PIXEL_UNPACK_BUFFER_BINDING_ARB = 0x88EF;

        #endregion

        #region GL_EXT_texture_sRGB

        public const uint GL_SRGB_EXT = 0x8C40;
        public const uint GL_SRGB8_EXT = 0x8C41;
        public const uint GL_SRGB_ALPHA_EXT = 0x8C42;
        public const uint GL_SRGB8_ALPHA8_EXT = 0x8C43;
        public const uint GL_SLUMINANCE_ALPHA_EXT = 0x8C44;
        public const uint GL_SLUMINANCE8_ALPHA8_EXT = 0x8C45;
        public const uint GL_SLUMINANCE_EXT = 0x8C46;
        public const uint GL_SLUMINANCE8_EXT = 0x8C47;
        public const uint GL_COMPRESSED_SRGB_EXT = 0x8C48;
        public const uint GL_COMPRESSED_SRGB_ALPHA_EXT = 0x8C49;
        public const uint GL_COMPRESSED_SLUMINANCE_EXT = 0x8C4A;
        public const uint GL_COMPRESSED_SLUMINANCE_ALPHA_EXT = 0x8C4B;
        public const uint GL_COMPRESSED_SRGB_S3TC_DXT1_EXT = 0x8C4C;
        public const uint GL_COMPRESSED_SRGB_ALPHA_S3TC_DXT1_EXT = 0x8C4D;
        public const uint GL_COMPRESSED_SRGB_ALPHA_S3TC_DXT3_EXT = 0x8C4E;
        public const uint GL_COMPRESSED_SRGB_ALPHA_S3TC_DXT5_EXT = 0x8C4F;

        #endregion

        #region GL_EXT_framebuffer_object

        //  Methods
        public bool IsRenderbufferEXT(uint renderbuffer)
        {
            return (bool)InvokeExtensionFunction<glIsRenderbufferEXT>(renderbuffer);
        }

        public void BindRenderbufferEXT(uint target, uint renderbuffer)
        {
            InvokeExtensionFunction<glBindRenderbufferEXT>(target, renderbuffer);
        }

        public void DeleteRenderbuffersEXT(uint n, uint[] renderbuffers)
        {
            InvokeExtensionFunction<glDeleteRenderbuffersEXT>(n, renderbuffers);
        }

        public void GenRenderbuffersEXT(uint n, uint[] renderbuffers)
        {
            InvokeExtensionFunction<glGenRenderbuffersEXT>(n, renderbuffers);
        }

        public void RenderbufferStorageEXT(uint target, uint internalformat, int width, int height)
        {
            InvokeExtensionFunction<glRenderbufferStorageEXT>(target, internalformat, width, height);
        }

        public void GetRenderbufferParameterivEXT(uint target, uint pname, int[] parameters)
        {
            InvokeExtensionFunction<glGetRenderbufferParameterivEXT>(target, pname, parameters);
        }

        public bool IsFramebufferEXT(uint framebuffer)
        {
            return (bool)InvokeExtensionFunction<glIsFramebufferEXT>(framebuffer);
        }

        public void BindFramebufferEXT(uint target, uint framebuffer)
        {
            InvokeExtensionFunction<glBindFramebufferEXT>(target, framebuffer);
        }

        public void DeleteFramebuffersEXT(uint n, uint[] framebuffers)
        {
            InvokeExtensionFunction<glDeleteFramebuffersEXT>(n, framebuffers);
        }

        public void GenFramebuffersEXT(uint n, uint[] framebuffers)
        {
            InvokeExtensionFunction<glGenFramebuffersEXT>(n, framebuffers);
        }

        public uint CheckFramebufferStatusEXT(uint target)
        {
            return (uint)InvokeExtensionFunction<glCheckFramebufferStatusEXT>(target);
        }

        public void FramebufferTexture1DEXT(uint target, uint attachment, uint textarget, uint texture, int level)
        {
            InvokeExtensionFunction<glFramebufferTexture1DEXT>(target, attachment, textarget, texture, level);
        }

        public void FramebufferTexture2DEXT(uint target, uint attachment, uint textarget, uint texture, int level)
        {
            InvokeExtensionFunction<glFramebufferTexture2DEXT>(target, attachment, textarget, texture, level);
        }

        public void FramebufferTexture3DEXT(uint target, uint attachment, uint textarget, uint texture, int level, int zoffset)
        {
            InvokeExtensionFunction<glFramebufferTexture3DEXT>(target, attachment, textarget, texture, level);
        }

        public void FramebufferRenderbufferEXT(uint target, uint attachment, uint renderbuffertarget, uint renderbuffer)
        {
            InvokeExtensionFunction<glFramebufferRenderbufferEXT>(target, attachment, renderbuffertarget, renderbuffer);
        }

        public void GetFramebufferAttachmentParameterivEXT(uint target, uint attachment, uint pname, int[] parameters)
        {
            InvokeExtensionFunction<glGetFramebufferAttachmentParameterivEXT>(target, attachment, pname, parameters);
        }

        public void GenerateMipmapEXT(uint target)
        {
            InvokeExtensionFunction<glGenerateMipmapEXT>(target);
        }

        //  Delegates
        private delegate bool glIsRenderbufferEXT(uint renderbuffer);
        private delegate void glBindRenderbufferEXT(uint target, uint renderbuffer);
        private delegate void glDeleteRenderbuffersEXT(uint n, uint[] renderbuffers);
        private delegate void glGenRenderbuffersEXT(uint n, uint[] renderbuffers);
        private delegate void glRenderbufferStorageEXT(uint target, uint internalformat, int width, int height);
        private delegate void glGetRenderbufferParameterivEXT(uint target, uint pname, int[] parameters);
        private delegate bool glIsFramebufferEXT(uint framebuffer);
        private delegate void glBindFramebufferEXT(uint target, uint framebuffer);
        private delegate void glDeleteFramebuffersEXT(uint n, uint[] framebuffers);
        private delegate void glGenFramebuffersEXT(uint n, uint[] framebuffers);
        private delegate uint glCheckFramebufferStatusEXT(uint target);
        private delegate void glFramebufferTexture1DEXT(uint target, uint attachment, uint textarget, uint texture, int level);
        private delegate void glFramebufferTexture2DEXT(uint target, uint attachment, uint textarget, uint texture, int level);
        private delegate void glFramebufferTexture3DEXT(uint target, uint attachment, uint textarget, uint texture, int level, int zoffset);
        private delegate void glFramebufferRenderbufferEXT(uint target, uint attachment, uint renderbuffertarget, uint renderbuffer);
        private delegate void glGetFramebufferAttachmentParameterivEXT(uint target, uint attachment, uint pname, int[] parameters);
        private delegate void glGenerateMipmapEXT(uint target);

        //  Constants
        public const uint GL_INVALID_FRAMEBUFFER_OPERATION_EXT = 0x0506;
        public const uint GL_MAX_RENDERBUFFER_SIZE_EXT = 0x84E8;
        public const uint GL_FRAMEBUFFER_BINDING_EXT = 0x8CA6;
        public const uint GL_RENDERBUFFER_BINDING_EXT = 0x8CA7;
        public const uint GL_FRAMEBUFFER_ATTACHMENT_OBJECT_TYPE_EXT = 0x8CD0;
        public const uint GL_FRAMEBUFFER_ATTACHMENT_OBJECT_NAME_EXT = 0x8CD1;
        public const uint GL_FRAMEBUFFER_ATTACHMENT_TEXTURE_LEVEL_EXT = 0x8CD2;
        public const uint GL_FRAMEBUFFER_ATTACHMENT_TEXTURE_CUBE_MAP_FACE_EXT = 0x8CD3;
        public const uint GL_FRAMEBUFFER_ATTACHMENT_TEXTURE_3D_ZOFFSET_EXT = 0x8CD4;
        public const uint GL_FRAMEBUFFER_COMPLETE_EXT = 0x8CD5;
        public const uint GL_FRAMEBUFFER_INCOMPLETE_ATTACHMENT_EXT = 0x8CD6;
        public const uint GL_FRAMEBUFFER_INCOMPLETE_MISSING_ATTACHMENT_EXT = 0x8CD7;
        public const uint GL_FRAMEBUFFER_INCOMPLETE_DIMENSIONS_EXT = 0x8CD9;
        public const uint GL_FRAMEBUFFER_INCOMPLETE_FORMATS_EXT = 0x8CDA;
        public const uint GL_FRAMEBUFFER_INCOMPLETE_DRAW_BUFFER_EXT = 0x8CDB;
        public const uint GL_FRAMEBUFFER_INCOMPLETE_READ_BUFFER_EXT = 0x8CDC;
        public const uint GL_FRAMEBUFFER_UNSUPPORTED_EXT = 0x8CDD;
        public const uint GL_MAX_COLOR_ATTACHMENTS_EXT = 0x8CDF;
        public const uint GL_COLOR_ATTACHMENT0_EXT = 0x8CE0;
        public const uint GL_COLOR_ATTACHMENT1_EXT = 0x8CE1;
        public const uint GL_COLOR_ATTACHMENT2_EXT = 0x8CE2;
        public const uint GL_COLOR_ATTACHMENT3_EXT = 0x8CE3;
        public const uint GL_COLOR_ATTACHMENT4_EXT = 0x8CE4;
        public const uint GL_COLOR_ATTACHMENT5_EXT = 0x8CE5;
        public const uint GL_COLOR_ATTACHMENT6_EXT = 0x8CE6;
        public const uint GL_COLOR_ATTACHMENT7_EXT = 0x8CE7;
        public const uint GL_COLOR_ATTACHMENT8_EXT = 0x8CE8;
        public const uint GL_COLOR_ATTACHMENT9_EXT = 0x8CE9;
        public const uint GL_COLOR_ATTACHMENT10_EXT = 0x8CEA;
        public const uint GL_COLOR_ATTACHMENT11_EXT = 0x8CEB;
        public const uint GL_COLOR_ATTACHMENT12_EXT = 0x8CEC;
        public const uint GL_COLOR_ATTACHMENT13_EXT = 0x8CED;
        public const uint GL_COLOR_ATTACHMENT14_EXT = 0x8CEE;
        public const uint GL_COLOR_ATTACHMENT15_EXT = 0x8CEF;
        public const uint GL_DEPTH_ATTACHMENT_EXT = 0x8D00;
        public const uint GL_STENCIL_ATTACHMENT_EXT = 0x8D20;
        public const uint GL_FRAMEBUFFER_EXT = 0x8D40;
        public const uint GL_RENDERBUFFER_EXT = 0x8D41;
        public const uint GL_RENDERBUFFER_WIDTH_EXT = 0x8D42;
        public const uint GL_RENDERBUFFER_HEIGHT_EXT = 0x8D43;
        public const uint GL_RENDERBUFFER_INTERNAL_FORMAT_EXT = 0x8D44;
        public const uint GL_STENCIL_INDEX1_EXT = 0x8D46;
        public const uint GL_STENCIL_INDEX4_EXT = 0x8D47;
        public const uint GL_STENCIL_INDEX8_EXT = 0x8D48;
        public const uint GL_STENCIL_INDEX16_EXT = 0x8D49;
        public const uint GL_RENDERBUFFER_RED_SIZE_EXT = 0x8D50;
        public const uint GL_RENDERBUFFER_GREEN_SIZE_EXT = 0x8D51;
        public const uint GL_RENDERBUFFER_BLUE_SIZE_EXT = 0x8D52;
        public const uint GL_RENDERBUFFER_ALPHA_SIZE_EXT = 0x8D53;
        public const uint GL_RENDERBUFFER_DEPTH_SIZE_EXT = 0x8D54;
        public const uint GL_RENDERBUFFER_STENCIL_SIZE_EXT = 0x8D55;

        #endregion

        #region GL_EXT_framebuffer_multisample

        //  Methods
        public void RenderbufferStorageMultisampleEXT(uint target, int samples, uint internalformat, int width, int height)
        {
            InvokeExtensionFunction<glRenderbufferStorageMultisampleEXT>(target, samples, internalformat, width, height);
        }

        //  Delegates
        private delegate void glRenderbufferStorageMultisampleEXT(uint target, int samples, uint internalformat, int width, int height);

        //  Constants
        public const uint GL_RENDERBUFFER_SAMPLES_EXT = 0x8CAB;
        public const uint GL_FRAMEBUFFER_INCOMPLETE_MULTISAMPLE_EXT = 0x8D56;
        public const uint GL_MAX_SAMPLES_EXT = 0x8D57;

        #endregion

        #region GL_EXT_draw_instanced

        //  Methods
        public void DrawArraysInstancedEXT(uint mode, int start, int count, int primcount)
        {
            InvokeExtensionFunction<glDrawArraysInstancedEXT>(mode, start, count, primcount);
        }
        public void DrawElementsInstancedEXT(uint mode, int count, uint type, IntPtr indices, int primcount)
        {
            InvokeExtensionFunction<glDrawElementsInstancedEXT>(mode, count, type, indices, primcount);
        }

        //  Delegates
        private delegate void glDrawArraysInstancedEXT(uint mode, int start, int count, int primcount);
        private delegate void glDrawElementsInstancedEXT(uint mode, int count, uint type, IntPtr indices, int primcount);

        #endregion

        #region GL_ARB_vertex_array_object

        //  Methods
        public void BindVertexArray(uint array)
        {
            InvokeExtensionFunction<glBindVertexArray>(array);
        }
        public void DeleteVertexArrays(int n, uint[] arrays)
        {
            InvokeExtensionFunction<glDeleteVertexArrays>(n, arrays);
        }
        public void GenVertexArrays(int n, uint[] arrays)
        {
            InvokeExtensionFunction<glGenVertexArrays>(n, arrays);
        }
        public bool IsVertexArray(uint array)
        {
            return (bool)InvokeExtensionFunction<glIsVertexArray>(array);
        }

        //  Delegates
        private delegate void glBindVertexArray(uint array);
        private delegate void glDeleteVertexArrays(int n, uint[] arrays);
        private delegate void glGenVertexArrays(int n, uint[] arrays);
        private delegate bool glIsVertexArray(uint array);

        //  Constants
        public const uint GL_VERTEX_ARRAY_BINDING = 0x85B5;

        #endregion

        #region GL_EXT_framebuffer_sRGB

        //  Constants
        public const uint GL_FRAMEBUFFER_SRGB_EXT = 0x8DB9;
        public const uint GL_FRAMEBUFFER_SRGB_CAPABLE_EXT = 0x8DBA;

        #endregion

        #region GGL_EXT_transform_feedback

        //  Methods
        public void BeginTransformFeedbackEXT(uint primitiveMode)
        {
            InvokeExtensionFunction<glBeginTransformFeedbackEXT>(primitiveMode);
        }
        public void EndTransformFeedbackEXT()
        {
            InvokeExtensionFunction<glEndTransformFeedbackEXT>();
        }
        public void BindBufferRangeEXT(uint target, uint index, uint buffer, int offset, int size)
        {
            InvokeExtensionFunction<glBindBufferRangeEXT>(target, index, buffer, offset, size);
        }
        public void BindBufferOffsetEXT(uint target, uint index, uint buffer, int offset)
        {
            InvokeExtensionFunction<glBindBufferOffsetEXT>(target, index, buffer, offset);
        }
        public void BindBufferBaseEXT(uint target, uint index, uint buffer)
        {
            InvokeExtensionFunction<glBindBufferBaseEXT>(target, index, buffer);
        }
        public void TransformFeedbackVaryingsEXT(uint program, int count, string[] varyings, uint bufferMode)
        {
            InvokeExtensionFunction<glTransformFeedbackVaryingsEXT>(program, count, varyings, bufferMode);
        }
        public void GetTransformFeedbackVaryingEXT(uint program, uint index, int bufSize, int[] length, int[] size, uint[] type, string name)
        {
            InvokeExtensionFunction<glGetTransformFeedbackVaryingEXT>(program, index, bufSize, length, size, type, name);
        }

        //  Delegates
        private delegate void glBeginTransformFeedbackEXT(uint primitiveMode);
        private delegate void glEndTransformFeedbackEXT ();
        private delegate void glBindBufferRangeEXT (uint target, uint index, uint buffer, int offset, int size);
        private delegate void glBindBufferOffsetEXT (uint target, uint index, uint buffer, int offset);
        private delegate void glBindBufferBaseEXT (uint target, uint index, uint buffer);
        private delegate void glTransformFeedbackVaryingsEXT (uint program, int count, string[] varyings, uint bufferMode);
        private delegate void glGetTransformFeedbackVaryingEXT (uint program, uint index, int bufSize, int[] length, int[] size, uint[] type, string name);

        //  Constants
        public const uint GL_TRANSFORM_FEEDBACK_BUFFER_EXT                       = 0x8C8E;
        public const uint GL_TRANSFORM_FEEDBACK_BUFFER_START_EXT                 = 0x8C84;
        public const uint GL_TRANSFORM_FEEDBACK_BUFFER_SIZE_EXT                  = 0x8C85;
        public const uint GL_TRANSFORM_FEEDBACK_BUFFER_BINDING_EXT               = 0x8C8F;
        public const uint GL_INTERLEAVED_ATTRIBS_EXT                             = 0x8C8C;
        public const uint GL_SEPARATE_ATTRIBS_EXT                                = 0x8C8D;
        public const uint GL_PRIMITIVES_GENERATED_EXT                            = 0x8C87;
        public const uint GL_TRANSFORM_FEEDBACK_PRIMITIVES_WRITTEN_EXT           = 0x8C88;
        public const uint GL_RASTERIZER_DISCARD_EXT                              = 0x8C89;
        public const uint GL_MAX_TRANSFORM_FEEDBACK_INTERLEAVED_COMPONENTS_EXT   = 0x8C8A;
        public const uint GL_MAX_TRANSFORM_FEEDBACK_SEPARATE_ATTRIBS_EXT         = 0x8C8B;
        public const uint GL_MAX_TRANSFORM_FEEDBACK_SEPARATE_COMPONENTS_EXT      = 0x8C80;
        public const uint GL_TRANSFORM_FEEDBACK_VARYINGS_EXT                     = 0x8C83;
        public const uint GL_TRANSFORM_FEEDBACK_BUFFER_MODE_EXT                  = 0x8C7F;
        public const uint GL_TRANSFORM_FEEDBACK_VARYING_MAX_LENGTH_EXT           = 0x8C76;

        #endregion
    }
}
