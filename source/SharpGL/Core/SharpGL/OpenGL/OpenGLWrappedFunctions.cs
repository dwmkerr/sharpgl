using System;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.InteropServices;
using SharpGL.RenderContextProviders;
using SharpGL.Version;

namespace SharpGL
{
	/// <summary>
	/// The OpenGL class wraps Suns OpenGL 3D library.
    /// </summary>
	public partial class OpenGL
	{        

		#region Wrapped OpenGL Functions

		/// <summary>
		/// Set the Accumulation Buffer operation.
		/// </summary>
		/// <param name="op">Operation of the buffer.</param>
		/// <param name="value">Reference value.</param>
		public void Accum(uint op, float value)
		{
			PreGLCall();
			glAccum(op, value);
			PostGLCall();
		}

        /// <summary>
        /// Set the Accumulation Buffer operation.
        /// </summary>
        /// <param name="op">Operation of the buffer.</param>
        /// <param name="value">Reference value.</param>
        public void Accum(Enumerations.AccumOperation op, float value)
        {
            PreGLCall();
            glAccum((uint)op, value);
            PostGLCall();
        }

        /// <summary>
        /// Specify the Alpha Test function.
        /// </summary>
        /// <param name="func">Specifies the alpha comparison function. Symbolic constants OpenGL.NEVER, OpenGL.LESS, OpenGL.EQUAL, OpenGL.LEQUAL, OpenGL.GREATER, OpenGL.NOTEQUAL, OpenGL.GEQUAL and OpenGL.ALWAYS are accepted. The initial value is OpenGL.ALWAYS.</param>
        /// <param name="reference">Specifies the reference	value that incoming alpha values are compared to. This value is clamped to the range 0	through	1, where 0 represents the lowest possible alpha value and 1 the highest possible value. The initial reference value is 0.</param>
		public void AlphaFunc(uint func, float reference)
        {
            PreGLCall();
            glAlphaFunc(func, reference);
            PostGLCall();
        }

        /// <summary>
        /// Specify the Alpha Test function.
        /// </summary>
        /// <param name="function">Specifies the alpha comparison function.</param>
        /// <param name="reference">Specifies the reference	value that incoming alpha values are compared to. This value is clamped to the range 0	through	1, where 0 represents the lowest possible alpha value and 1 the highest possible value. The initial reference value is 0.</param>
        public void AlphaFunc(Enumerations.AlphaTestFunction function, float reference)
        {
            PreGLCall();
            glAlphaFunc((uint)function, reference);
            PostGLCall();
        }

        /// <summary>
        /// Determine if textures are loaded in texture memory.
        /// </summary>
        /// <param name="n">Specifies the number of textures to be queried.</param>
        /// <param name="textures">Specifies an array containing the names of the textures to be queried.</param>
        /// <param name="residences">Specifies an array in which the texture residence status is returned. The residence status of a texture named by an element of textures is returned in the corresponding element of residences.</param>
        /// <returns></returns>
		public byte AreTexturesResident(int n,  uint []textures, byte []residences)
        {
            PreGLCall();
            byte returnValue = glAreTexturesResident(n, textures, residences);
            PostGLCall();

            return returnValue;
        }

        /// <summary>
        /// Render a vertex using the specified vertex array element.
        /// </summary>
        /// <param name="i">Specifies an index	into the enabled vertex	data arrays.</param>
		public void ArrayElement(int i)
        {
            PreGLCall();
            glArrayElement(i);
            PostGLCall();
        }
        
		/// <summary>
		/// Begin drawing geometry in the specified mode.
		/// </summary>
		/// <param name="mode">The mode to draw in, e.g. OpenGL.POLYGONS.</param>
        public void Begin(uint mode)
        {
            // Do PreGLCall now, and PostGLCall AFTER End()
            PreGLCall();

            //  Let's remember something important here - you CANNOT call 'glGetError'
            //  between glBegin and glEnd. So we set the 'begun' flag - this'll
            //  turn off error reporting until glEnd.
            glBegin(mode);

            //  Set the begun flag.
            insideGLBegin = true;
        }

        /// <summary>
        /// Begin drawing geometry in the specified mode.
        /// </summary>
        /// <param name="mode">The mode to draw in, e.g. OpenGL.POLYGONS.</param>
        public void Begin(Enumerations.BeginMode mode)
        {
            // Do PreGLCall now, and PostGLCall AFTER End()
            PreGLCall();

            //  Let's remember something important here - you CANNOT call 'glGetError'
            //  between glBegin and glEnd. So we set the 'begun' flag - this'll
            //  turn off error reporting until glEnd.
            glBegin((uint)mode);

            //  Set the begun flag.
            insideGLBegin = true;
        }

		/// <summary>
		/// This function begins drawing a NURBS curve.
		/// </summary>
		/// <param name="nurbsObject">The NURBS object.</param>
        public void BeginCurve(IntPtr nurbsObject)
		{
			PreGLCall();
			gluBeginCurve(nurbsObject);
			PostGLCall();
		}

		/// <summary>
		/// This function begins drawing a NURBS surface.
		/// </summary>
		/// <param name="nurbsObject">The NURBS object.</param>
		public void BeginSurface(IntPtr nurbsObject)
		{
			PreGLCall();
			gluBeginSurface(nurbsObject);
			PostGLCall();
		}

		/// <summary>
		/// Call this function after creating a texture to finalise creation of it, 
		/// or to make an existing texture current.
		/// </summary>
		/// <param name="target">The target type, e.g TEXTURE_2D.</param>
		/// <param name="texture">The OpenGL texture object.</param>
		public void BindTexture(uint target, uint texture)
		{
			PreGLCall();
			glBindTexture(target, texture);
			PostGLCall();
		}

        /// <summary>
        /// Draw a bitmap.
        /// </summary>
        /// <param name="width">Specify the pixel width	of the bitmap image.</param>
        /// <param name="height">Specify the pixel height of the bitmap image.</param>
        /// <param name="xorig">Specify	the location of	the origin in the bitmap image. The origin is measured from the lower left corner of the bitmap, with right and up being the positive axes.</param>
        /// <param name="yorig">Specify	the location of	the origin in the bitmap image. The origin is measured from the lower left corner of the bitmap, with right and up being the positive axes.</param>
        /// <param name="xmove">Specify	the x and y offsets to be added	to the current	raster position	after the bitmap is drawn.</param>
        /// <param name="ymove">Specify	the x and y offsets to be added	to the current	raster position	after the bitmap is drawn.</param>
        /// <param name="bitmap">Specifies the address of the bitmap image.</param>
		public void Bitmap(int width, int height, float xorig, float yorig, float xmove, float ymove,  byte []bitmap)
        {
            PreGLCall();
            glBitmap(width, height, xorig, yorig, xmove, ymove, bitmap);
            PostGLCall();
        }

		/// <summary>
		/// This function sets the current blending function.
		/// </summary>
		/// <param name="sfactor">Source factor.</param>
		/// <param name="dfactor">Destination factor.</param>
		public void BlendFunc(uint sfactor, uint dfactor)
		{
			PreGLCall();
			glBlendFunc(sfactor,dfactor);
			PostGLCall();
		}

        /// <summary>
        /// This function sets the current blending function.
        /// </summary>
        /// <param name="sourceFactor">The source factor.</param>
        /// <param name="destinationFactor">The destination factor.</param>
        public void BlendFunc(Enumerations.BlendingSourceFactor sourceFactor, Enumerations.BlendingDestinationFactor destinationFactor)
        {
            PreGLCall();
            glBlendFunc((uint)sourceFactor, (uint)destinationFactor);
            PostGLCall();
        }

		/// <summary>
		/// This function calls a certain display list.
		/// </summary>
		/// <param name="list">The display list to call.</param>
		public void CallList(uint list)
		{
			PreGLCall();
			glCallList(list);
			PostGLCall();
		}

        /// <summary>
        /// Execute	a list of display lists.
        /// </summary>
        /// <param name="n">Specifies the number of display lists to be executed.</param>
        /// <param name="type">Specifies the type of values in lists. Symbolic constants OpenGL.BYTE, OpenGL.UNSIGNED_BYTE, OpenGL.SHORT, OpenGL.UNSIGNED_SHORT, OpenGL.INT, OpenGL.UNSIGNED_INT, OpenGL.FLOAT, OpenGL.2_BYTES, OpenGL.3_BYTES and OpenGL.4_BYTES are accepted.</param>
        /// <param name="lists">Specifies the address of an array of name offsets in the display list. The pointer type is void because the offsets can be bytes, shorts, ints, or floats, depending on the value of type.</param>
		public void CallLists (int n, uint type, IntPtr lists)
        {
            PreGLCall();
            glCallLists(n, type, lists);
            PostGLCall();
        }

        /// <summary>
        /// Execute	a list of display lists.
        /// </summary>
        /// <param name="n">Specifies the number of display lists to be executed.</param>
        /// <param name="type">Specifies the type of values in lists. Symbolic constants OpenGL.BYTE, OpenGL.UNSIGNED_BYTE, OpenGL.SHORT, OpenGL.UNSIGNED_SHORT, OpenGL.INT, OpenGL.UNSIGNED_INT, OpenGL.FLOAT, OpenGL.2_BYTES, OpenGL.3_BYTES and OpenGL.4_BYTES are accepted.</param>
        /// <param name="lists">Specifies the address of an array of name offsets in the display list. The pointer type is void because the offsets can be bytes, shorts, ints, or floats, depending on the value of type.</param>
        public void CallLists(int n, Enumerations.DataType type, IntPtr lists)
        {
            PreGLCall();
            glCallLists(n, (uint)type, lists);
            PostGLCall();
        }

        /// <summary>
        /// Execute	a list of display lists. Automatically uses the GL_UNSIGNED_BYTE version of the function.
        /// </summary>
        /// <param name="n">The number of lists.</param>
        /// <param name="lists">The lists.</param>
        public void CallLists(int n, byte[] lists)
        {
            PreGLCall();
            glCallLists(n, GL_UNSIGNED_BYTE, lists);
            PostGLCall();
        }

        /// <summary>
        /// Execute	a list of display lists. Automatically uses the GL_UNSIGNED_INT version of the function.
        /// </summary>
        /// <param name="n">The number of lists.</param>
        /// <param name="lists">The lists.</param>
        public void CallLists(int n, uint[] lists)
        {
            PreGLCall();
            glCallLists(n, GL_UNSIGNED_INT, lists);
            PostGLCall();
        }

		/// <summary>
		/// This function clears the buffers specified by mask.
		/// </summary>
		/// <param name="mask">Which buffers to clear.</param>
		public void Clear(uint mask)
		{
			PreGLCall();
			glClear(mask);
			PostGLCall();
		}

        /// <summary>
        /// Specify clear values for the accumulation buffer.
        /// </summary>
        /// <param name="red">Specify the red, green, blue and alpha values used when the accumulation buffer is cleared. The initial values are all 0.</param>
        /// <param name="green">Specify the red, green, blue and alpha values used when the accumulation buffer is cleared. The initial values are all 0.</param>
        /// <param name="blue">Specify the red, green, blue and alpha values used when the accumulation buffer is cleared. The initial values are all 0.</param>
        /// <param name="alpha">Specify the red, green, blue and alpha values used when the accumulation buffer is cleared. The initial values are all 0.</param>
		public void ClearAccum (float red, float green, float blue, float alpha)
        {
            PreGLCall();
            glClearAccum(red, green, blue, alpha);
            PostGLCall();
        }

		/// <summary>
		/// This function sets the color that the drawing buffer is 'cleared' to.
		/// </summary>
		/// <param name="red">Red component of the color (between 0 and 1).</param>
		/// <param name="green">Green component of the color (between 0 and 1).</param>
		/// <param name="blue">Blue component of the color (between 0 and 1)./</param>
		/// <param name="alpha">Alpha component of the color (between 0 and 1).</param>
		public void ClearColor (float red, float green, float blue, float alpha)
		{
			PreGLCall();
			glClearColor(red, green, blue, alpha);
			PostGLCall();
		}

        /// <summary>
        /// Specify the clear value for the depth buffer.
        /// </summary>
        /// <param name="depth">Specifies the depth value used	when the depth buffer is cleared. The initial value is 1.</param>
		public void ClearDepth(double depth)
        {
            PreGLCall();
            glClearDepth(depth);
            PostGLCall();
        }

        /// <summary>
        /// Specify the clear value for the color index buffers.
        /// </summary>
        /// <param name="c">Specifies the index used when the color index buffers are cleared. The initial value is 0.</param>
		public void ClearIndex (float c)
        {
            PreGLCall();
            glClearIndex(c);
            PostGLCall();
        }

        /// <summary>
        /// Specify the clear value for the stencil buffer.
        /// </summary>
        /// <param name="s">Specifies the index used when the stencil buffer is cleared. The initial value is 0.</param>
		public void ClearStencil (int s)
        {
            PreGLCall();
            glClearStencil(s);
            PostGLCall();
        }

        /// <summary>
        /// Specify a plane against which all geometry is clipped.
        /// </summary>
        /// <param name="plane">Specifies which clipping plane is being positioned. Symbolic names of the form OpenGL.CLIP_PLANEi, where i is an integer between 0 and OpenGL.MAX_CLIP_PLANES -1, are accepted.</param>
        /// <param name="equation">Specifies the address of an	array of four double-precision floating-point values. These values are interpreted as a plane equation.</param>
		public void ClipPlane (uint plane,  double []equation)
        {
            PreGLCall();
            glClipPlane(plane, equation);
            PostGLCall();
        }

        /// <summary>
        /// Specify a plane against which all geometry is clipped.
        /// </summary>
        /// <param name="plane">Specifies which clipping plane is being positioned. Symbolic names of the form OpenGL.CLIP_PLANEi, where i is an integer between 0 and OpenGL.MAX_CLIP_PLANES -1, are accepted.</param>
        /// <param name="equation">Specifies the address of an	array of four double-precision floating-point values. These values are interpreted as a plane equation.</param>
        public void ClipPlane(Enumerations.ClipPlaneName plane, double[] equation)
        {
            PreGLCall();
            glClipPlane((uint)plane, equation);
            PostGLCall();
        }

		/// <summary>
		/// Sets the current color.
		/// </summary>
		/// <param name="red">Red color component (between 0 and 255).</param>
		/// <param name="green">Green color component (between 0 and 255).</param>
		/// <param name="blue">Blue color component (between 0 and 255).</param>
		public void Color(byte red, byte green, byte blue)
		{
			PreGLCall();
			glColor3ub(red, green, blue);
			PostGLCall();
		}

        /// <summary>
        /// Sets the current color.
        /// </summary>
        /// <param name="red">Red color component (between 0 and 255).</param>
        /// <param name="green">Green color component (between 0 and 255).</param>
        /// <param name="blue">Blue color component (between 0 and 255).</param>
        /// <param name="alpha">Alpha color component (between 0 and 255).</param>
        public void Color(byte red, byte green, byte blue, byte alpha)
        {
            PreGLCall();
            glColor4ub(red, green, blue, alpha);
            PostGLCall();
        }

		/// <summary>
		/// Sets the current color.
		/// </summary>
		/// <param name="red">Red color component (between 0 and 1).</param>
		/// <param name="green">Green color component (between 0 and 1).</param>
		/// <param name="blue">Blue color component (between 0 and 1).</param>
		public void Color(double red, double green, double blue)
		{
			PreGLCall();
			glColor3d(red, green, blue);
			PostGLCall();
		}

        /// <summary>
        /// Sets the current color.
        /// </summary>
        /// <param name="red">Red color component (between 0 and 1).</param>
        /// <param name="green">Green color component (between 0 and 1).</param>
        /// <param name="blue">Blue color component (between 0 and 1).</param>
        /// <param name="alpha">Alpha color component.</param>
        public void Color(double red, double green, double blue, double alpha)
        {
            PreGLCall();
            glColor4d(red, green, blue, alpha);
            PostGLCall();
        }

		/// <summary>
		/// Sets the current color.
		/// </summary>
		/// <param name="red">Red color component (between 0 and 1).</param>
		/// <param name="green">Green color component (between 0 and 1).</param>
		/// <param name="blue">Blue color component (between 0 and 1).</param>
		public void Color(float red, float green, float blue)
		{
			PreGLCall();
			glColor3f(red, green, blue);
			PostGLCall();
		}

		/// <summary>
		/// Sets the current color to 'v'.
		/// </summary>
		/// <param name="v">An array of either 3 or 4 float values.</param>
		public void Color(float[] v)
		{
			PreGLCall();
			if(v.Length == 3)
				glColor3fv(v);
			else if(v.Length == 4)
				glColor4fv(v);
			PostGLCall();
		}

        /// <summary>
        /// Sets the current color to 'v'.
        /// </summary>
        /// <param name="v">An array of either 3 or 4 int values.</param>
        public void Color(int[] v)
        {
            PreGLCall();
            if (v.Length == 3)
                glColor3iv(v);
            else if (v.Length == 4)
                glColor4iv(v);
            PostGLCall();
        }

        /// <summary>
        /// Sets the current color to 'v'.
        /// </summary>
        /// <param name="v">An array of either 3 or 4 int values.</param>
        public void Color(short[] v)
        {
            PreGLCall();
            if (v.Length == 3)
                glColor3sv(v);
            else if (v.Length == 4)
                glColor4sv(v);
            PostGLCall();
        }

        /// <summary>
        /// Sets the current color to 'v'.
        /// </summary>
        /// <param name="v">An array of either 3 or 4 double values.</param>
        public void Color(double[] v)
        {
            PreGLCall();
            if (v.Length == 3)
                glColor3dv(v);
            else if (v.Length == 4)
                glColor4dv(v);
            PostGLCall();
        }

        /// <summary>
        /// Sets the current color to 'v'.
        /// </summary>
        /// <param name="v">An array of either 3 or 4 byte values.</param>
        public void Color(byte[] v)
        {
            PreGLCall();
            if (v.Length == 3)
                glColor3bv(v);
            else if (v.Length == 4)
                glColor4bv(v);
            PostGLCall();
        }

        /// <summary>
        /// Sets the current color to 'v'.
        /// </summary>
        /// <param name="v">An array of either 3 or 4 unsigned int values.</param>
        public void Color(uint[] v) 
        {
            PreGLCall();
            if (v.Length == 3)
                glColor3uiv(v);
            else if (v.Length == 4)
                glColor4uiv(v);
            PostGLCall();
        }

        /// <summary>
        /// Sets the current color to 'v'.
        /// </summary>
        /// <param name="v">An array of either 3 or 4 unsigned short values.</param>
        public void Color(ushort[] v)
        {
            PreGLCall();
            if (v.Length == 3)
                glColor3usv(v);
            else if (v.Length == 4)
                glColor4usv(v);
            PostGLCall();
        }

		/// <summary>
		/// Sets the current color.
		/// </summary>
		/// <param name="red">Red color component (between 0 and 1).</param>
		/// <param name="green">Green color component (between 0 and 1).</param>
		/// <param name="blue">Blue color component (between 0 and 1).</param>
		public void Color(int red, int green, int blue)
		{
			PreGLCall();
			glColor3i(red, green, blue);
			PostGLCall();
		}

        /// <summary>
        /// Sets the current color.
        /// </summary>
        /// <param name="red">Red color component (between 0 and 1).</param>
        /// <param name="green">Green color component (between 0 and 1).</param>
        /// <param name="blue">Blue color component (between 0 and 1).</param>
        /// <param name="alpha">Alpha color component.</param>
        public void Color(int red, int green, int blue, int alpha)
        {
            PreGLCall();
            glColor4i(red, green, blue, alpha);
            PostGLCall();
        }

		/// <summary>
		/// Sets the current color.
		/// </summary>
		/// <param name="red">Red color component (between 0 and 1).</param>
		/// <param name="green">Green color component (between 0 and 1).</param>
		/// <param name="blue">Blue color component (between 0 and 1).</param>
		public void Color(short red, short green, short blue)
		{
			PreGLCall();
			glColor3s(red, green, blue);
			PostGLCall();
		}

        /// <summary>
        /// Sets the current color.
        /// </summary>
        /// <param name="red">Red color component (between 0 and 1).</param>
        /// <param name="green">Green color component (between 0 and 1).</param>
        /// <param name="blue">Blue color component (between 0 and 1).</param>
        /// <param name="alpha">Alpha color component.</param>
        public void Color(short red, short green, short blue, short alpha)
        {
            PreGLCall();
            glColor4s(red, green, blue, alpha);
            PostGLCall();
        }

		/// <summary>
		/// Sets the current color.
		/// </summary>
		/// <param name="red">Red color component (between 0 and 1).</param>
		/// <param name="green">Green color component (between 0 and 1).</param>
		/// <param name="blue">Blue color component (between 0 and 1).</param>
		public void Color(uint red, uint green, uint blue)
		{
			PreGLCall();
			glColor3ui(red, green, blue);
			PostGLCall();
		}

        /// <summary>
        /// Sets the current color.
        /// </summary>
        /// <param name="red">Red color component (between 0 and 1).</param>
        /// <param name="green">Green color component (between 0 and 1).</param>
        /// <param name="blue">Blue color component (between 0 and 1).</param>
        /// <param name="alpha">Alpha color component.</param>
        public void Color(uint red, uint green, uint blue, uint alpha)
        {
            PreGLCall();
            glColor4ui(red, green, blue, alpha);
            PostGLCall();
        }

		/// <summary>
		/// Sets the current color.
		/// </summary>
		/// <param name="red">Red color component (between 0 and 1).</param>
		/// <param name="green">Green color component (between 0 and 1).</param>
		/// <param name="blue">Blue color component (between 0 and 1).</param>
		public void Color(ushort red, ushort green, ushort blue)
		{
			PreGLCall();
			glColor3us(red, green, blue);
			PostGLCall();
		}

        /// <summary>
        /// Sets the current color.
        /// </summary>
        /// <param name="red">Red color component (between 0 and 1).</param>
        /// <param name="green">Green color component (between 0 and 1).</param>
        /// <param name="blue">Blue color component (between 0 and 1).</param>
        /// <param name="alpha">Alpha color component.</param>
        public void Color(ushort red, ushort green, ushort blue, ushort alpha)
        {
            PreGLCall();
            glColor4us(red, green, blue, alpha);
            PostGLCall();
        }

		/// <summary>
		/// Sets the current color.
		/// </summary>
		/// <param name="red">Red color component (between 0 and 1).</param>
		/// <param name="green">Green color component (between 0 and 1).</param>
		/// <param name="blue">Blue color component (between 0 and 1).</param>
		/// <param name="alpha">Alpha color component (between 0 and 1).</param>
		public void Color(float red, float green, float blue, float alpha)
		{
			PreGLCall();
			glColor4f(red, green, blue, alpha);
			PostGLCall();
		}

		/// <summary>
		/// This function sets the current colour mask.
		/// </summary>
		/// <param name="red">Red component mask.</param>
		/// <param name="green">Green component mask.</param>
		/// <param name="blue">Blue component mask.</param>
		/// <param name="alpha">Alpha component mask.</param>
		public void ColorMask(byte red, byte green, byte blue, byte alpha)
		{
			PreGLCall();
			glColorMask(red, green, blue, alpha);
			PostGLCall();
		}

        /// <summary>
        /// Cause a material color to track the current color.
        /// </summary>
        /// <param name="face">Specifies whether front, back, or both front and back material parameters should track the current color. Accepted values are OpenGL.FRONT, OpenGL.BACK, and OpenGL.FRONT_AND_BACK. The initial value is OpenGL.FRONT_AND_BACK.</param>
        /// <param name="mode">Specifies which	of several material parameters track the current color. Accepted values are	OpenGL.EMISSION, OpenGL.AMBIENT, OpenGL.DIFFUSE, OpenGL.SPECULAR and OpenGL.AMBIENT_AND_DIFFUSE. The initial value is OpenGL.AMBIENT_AND_DIFFUSE.</param>
		public void ColorMaterial (uint face, uint mode)
        {
            PreGLCall();
            glColorMaterial(face, mode);
            PostGLCall();
        }

        /// <summary>
        /// Define an array of colors.
        /// </summary>
        /// <param name="size">Specifies the number	of components per color. Must be 3	or 4.</param>
        /// <param name="type">Specifies the data type of each color component in the array. Symbolic constants OpenGL.BYTE, OpenGL.UNSIGNED_BYTE, OpenGL.SHORT, OpenGL.UNSIGNED_SHORT, OpenGL.INT, OpenGL.UNSIGNED_INT, OpenGL.FLOAT and OpenGL.DOUBLE are accepted.</param>
        /// <param name="stride">Specifies the byte offset between consecutive colors. If stride is 0, (the initial value), the colors are understood to be tightly packed in the array.</param>
        /// <param name="pointer">Specifies a pointer to the first component of the first color element in the array.</param>
		public void ColorPointer (int size, uint type, int stride,  IntPtr pointer)
        {
            PreGLCall();
            glColorPointer(size, type, stride, pointer);
            PostGLCall();
        }

        /// <summary>
        /// Copy pixels in	the frame buffer.
        /// </summary>
        /// <param name="x">Specify the window coordinates of the lower left corner of the rectangular region of pixels to be copied.</param>
        /// <param name="y">Specify the window coordinates of the lower left corner of the rectangular region of pixels to be copied.</param>
        /// <param name="width">Specify the dimensions of the rectangular region of pixels to be copied. Both must be nonnegative.</param>
        /// <param name="height">Specify the dimensions of the rectangular region of pixels to be copied. Both must be nonnegative.</param>
        /// <param name="type">Specifies whether color values, depth values, or stencil values are to be copied. Symbolic constants OpenGL.COLOR, OpenGL.DEPTH, and OpenGL.STENCIL are accepted.</param>
		public void CopyPixels (int x, int y, int width, int height, uint type)
        {
            PreGLCall();
            glCopyPixels(x, y, width, height, type);
            PostGLCall();
        }

        /// <summary>
        /// Copy pixels into a 1D texture image.
        /// </summary>
        /// <param name="target">Specifies the target texture. Must be OpenGL.TEXTURE_1D.</param>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="internalFormat">Specifies the internal format of the texture.</param>
        /// <param name="x">Specify the window coordinates of the left corner of the row of pixels to be copied.</param>
        /// <param name="y">Specify the window coordinates of the left corner of the row of pixels to be copied.</param>
        /// <param name="width">Specifies the width of the texture image. Must be 0 or 2^n = (2 * border) for some integer n. The height of the texture image is 1.</param>
        /// <param name="border">Specifies the width of the border. Must be either 0 or 1.</param>
		public void CopyTexImage1D (uint target, int level, uint internalFormat, int x, int y, int width, int border)
        {
            PreGLCall();
            glCopyTexImage1D(target, level, internalFormat, x, y, width, border);
            PostGLCall();
        }

        /// <summary>
        /// Copy pixels into a	2D texture image.
        /// </summary>
        /// <param name="target">Specifies the target texture. Must be OpenGL.TEXTURE_2D.</param>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="internalFormat">Specifies the internal format of the texture.</param>
        /// <param name="x">Specify the window coordinates of the left corner of the row of pixels to be copied.</param>
        /// <param name="y">Specify the window coordinates of the left corner of the row of pixels to be copied.</param>
        /// <param name="width">Specifies the width of the texture image.</param>
        /// <param name="height">Specifies the height of the texture image.</param>
        /// <param name="border">Specifies the width of the border. Must be either 0 or 1.</param>
		public void CopyTexImage2D (uint target, int level, uint internalFormat, int x, int y, int width, int height, int border)
        {
            PreGLCall();
            glCopyTexImage2D(target, level, internalFormat, x, y, width, height, border);
            PostGLCall();
        }

        /// <summary>
        /// Copy a one-dimensional texture subimage.
        /// </summary>
        /// <param name="target">Specifies the target texture. Must be OpenGL.TEXTURE_1D.</param>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xoffset">Specifies the texel offset within the texture array.</param>
        /// <param name="x">Specify the window coordinates of the left corner of the row of pixels to be copied.</param>
        /// <param name="y">Specify the window coordinates of the left corner of the row of pixels to be copied.</param>
        /// <param name="width">Specifies the width of the texture image.</param>
		public void CopyTexSubImage1D (uint target, int level, int xoffset, int x, int y, int width)
        {
            PreGLCall();
            glCopyTexSubImage1D(target, level, xoffset, x, y, width);
            PostGLCall();
        }

        /// <summary>
        /// Copy a two-dimensional texture subimage.
        /// </summary>
        /// <param name="target">Specifies the target texture. Must be OpenGL.TEXTURE_2D.</param>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xoffset">Specifies the texel offset within the texture array.</param>
        /// <param name="yoffset">Specifies the texel offset within the texture array.</param>
        /// <param name="x">Specify the window coordinates of the left corner of the row of pixels to be copied.</param>
        /// <param name="y">Specify the window coordinates of the left corner of the row of pixels to be copied.</param>
        /// <param name="width">Specifies the width of the texture image.</param>
        /// <param name="height">Specifies the height of the texture image.</param>
		public void CopyTexSubImage2D (uint target, int level, int xoffset, int yoffset, int x, int y, int width, int height)
        {
            PreGLCall();
            glCopyTexSubImage2D(target, level, xoffset, yoffset, x, y, width, height);
            PostGLCall();
        }

        /// <summary>
        /// Specify whether front- or back-facing facets can be culled.
        /// </summary>
        /// <param name="mode">Specifies whether front- or back-facing facets are candidates for culling. Symbolic constants OpenGL.FRONT, OpenGL.BACK, and OpenGL.FRONT_AND_BACK are accepted. The initial	value is OpenGL.BACK.</param>
		public void CullFace (uint mode)
        {
            PreGLCall();
            glCullFace(mode);
            PostGLCall();
        }

		/// <summary>
		/// This function draws a sphere from the quadric object.
		/// </summary>
		/// <param name="qobj">The quadric object.</param>
		/// <param name="baseRadius">Radius at the base.</param>
		/// <param name="topRadius">Radius at the top.</param>
		/// <param name="height">Height of cylinder.</param>
		/// <param name="slices">Cylinder slices.</param>
		/// <param name="stacks">Cylinder stacks.</param>
		public void Cylinder(IntPtr qobj, double baseRadius, double topRadius, double height,int slices, int stacks)
		{
			PreGLCall();
			gluCylinder(qobj, baseRadius, topRadius, height, slices, stacks);
			PostGLCall();
		}

		/// <summary>
		/// This function deletes a list, or a range of lists.
		/// </summary>
		/// <param name="list">The list to delete.</param>
		/// <param name="range">The range of lists (often just 1).</param>
		public void DeleteLists(uint list, int range)
		{
			PreGLCall();
			glDeleteLists(list, range);
			PostGLCall();
		}

		/// <summary>
		/// This function deletes the underlying glu nurbs renderer.
		/// </summary>
		/// <param name="nurbsObject">The pointer to the nurbs object.</param>
		public void DeleteNurbsRenderer(IntPtr nurbsObject)
		{
			PreGLCall();
			gluDeleteNurbsRenderer(nurbsObject);
			PostGLCall();
		}

		/// <summary>
		/// This function deletes a set of Texture objects.
		/// </summary>
		/// <param name="n">Number of textures to delete.</param>
		/// <param name="textures">The array containing the names of the textures to delete.</param>
		public void DeleteTextures (int n,  uint []textures)
		{
			PreGLCall();
			glDeleteTextures(n, textures);
			PostGLCall();
		}

		/// <summary>
		/// Call this function to delete an OpenGL Quadric object.
		/// </summary>
		/// <param name="quadric"></param>
		public void DeleteQuadric(IntPtr quadric)
		{
            PreGLCall();
			gluDeleteQuadric(quadric);
            PostGLCall();
		}

		/// <summary>
		/// This function sets the current depth buffer comparison function, the default it LESS.
		/// </summary>
		/// <param name="func">The comparison function to set.</param>
		public void DepthFunc(uint func)
		{
			PreGLCall();
			glDepthFunc(func);
			PostGLCall();
		}
        
		/// <summary>
		/// This function sets the current depth buffer comparison function, the default it LESS.
		/// </summary>
        /// <param name="function">The comparison function to set.</param>
        public void DepthFunc(Enumerations.DepthFunction function)
		{
			PreGLCall();
            glDepthFunc((uint)function);
			PostGLCall();
		}
        

		/// <summary>
		/// This function sets the depth mask.
		/// </summary>
		/// <param name="flag">The depth mask flag, normally 1.</param>
		public void DepthMask(byte flag)
		{
			PreGLCall();
			glDepthMask(flag);
			PostGLCall();
		}

        /// <summary>
        /// Specify mapping of depth values from normalized device coordinates	to window coordinates.
        /// </summary>
        /// <param name="zNear">Specifies the mapping of the near clipping plane to window coordinates. The initial value is 0.</param>
        /// <param name="zFar">Specifies the mapping of the near clipping plane to window coordinates. The initial value is 1.</param>
		public void DepthRange (double zNear, double zFar)
        {
            PreGLCall();
            glDepthRange(zNear, zFar);
            PostGLCall();
        }

		/// <summary>
		/// Call this function to disable an OpenGL capability.
		/// </summary>
		/// <param name="cap">The capability to disable.</param>
		public void Disable(uint cap)
		{
			PreGLCall();
			glDisable(cap);
			PostGLCall();
		}

		/// <summary>
		/// This function disables a client state array, such as a vertex array.
		/// </summary>
		/// <param name="array">The array to disable.</param>
		public void DisableClientState (uint array)
		{
			PreGLCall();
			glDisableClientState(array);
			PostGLCall();
		}

        /// <summary>
        /// Render	primitives from	array data.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render. Symbolic constants OpenGL.POINTS, OpenGL.LINE_STRIP, OpenGL.LINE_LOOP, OpenGL.LINES, OpenGL.TRIANGLE_STRIP, OpenGL.TRIANGLE_FAN, OpenGL.TRIANGLES, OpenGL.QUAD_STRIP, OpenGL.QUADS, and OpenGL.POLYGON are accepted.</param>
        /// <param name="first">Specifies the starting	index in the enabled arrays.</param>
        /// <param name="count">Specifies the number of indices to be rendered.</param>
		public void DrawArrays (uint mode, int first, int count)
        {
            PreGLCall();
            glDrawArrays(mode, first, count);
            PostGLCall();
        }

        /// <summary>
        /// Specify which color buffers are to be drawn into.
        /// </summary>
        /// <param name="mode">Specifies up to	four color buffers to be drawn into. Symbolic constants OpenGL.NONE, OpenGL.FRONT_LEFT, OpenGL.FRONT_RIGHT,	OpenGL.BACK_LEFT, OpenGL.BACK_RIGHT, OpenGL.FRONT, OpenGL.BACK, OpenGL.LEFT, OpenGL.RIGHT, OpenGL.FRONT_AND_BACK, and OpenGL.AUXi, where i is between 0 and (OpenGL.AUX_BUFFERS - 1), are accepted (OpenGL.AUX_BUFFERS is not the upper limit; use glGet to query the number of	available aux buffers.)  The initial value is OpenGL.FRONT for single- buffered contexts, and OpenGL.BACK for double-buffered contexts.</param>
		public void DrawBuffer (uint mode)
        {
            PreGLCall();
            glDrawBuffer(mode);
            PostGLCall();
        }

        /// <summary>
        /// Specify which color buffers are to be drawn into.
        /// </summary>
        /// <param name="drawBufferMode">Specifies up to	four color buffers to be drawn into.</param>
        public void DrawBuffer(Enumerations.DrawBufferMode drawBufferMode)
        {
            PreGLCall();
            glDrawBuffer((uint)drawBufferMode);
            PostGLCall();
        }

        /// <summary>
        /// Render primitives from array data.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to	render. Symbolic constants OpenGL.POINTS, OpenGL.LINE_STRIP, OpenGL.LINE_LOOP, OpenGL.LINES, OpenGL.TRIANGLE_STRIP, OpenGL.TRIANGLE_FAN, OpenGL.TRIANGLES, OpenGL.QUAD_STRIP, OpenGL.QUADS, and OpenGL.POLYGON are accepted.</param>
        /// <param name="count">Specifies the number of elements to be rendered.</param>
        /// <param name="indices">Specifies a pointer to the location where the indices are stored.</param>
        public void DrawElements(uint mode, int count, uint[] indices)
        {
            PreGLCall();
            glDrawElements(mode, count, GL_UNSIGNED_INT, indices);
            PostGLCall();
        }

        /// <summary>
        /// Render primitives from array data.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to	render. Symbolic constants OpenGL.POINTS, OpenGL.LINE_STRIP, OpenGL.LINE_LOOP, OpenGL.LINES, OpenGL.TRIANGLE_STRIP, OpenGL.TRIANGLE_FAN, OpenGL.TRIANGLES, OpenGL.QUAD_STRIP, OpenGL.QUADS, and OpenGL.POLYGON are accepted.</param>
        /// <param name="count">Specifies the number of elements to be rendered.</param>
        /// <param name="type">Specifies the type of the values in indices.	Must be one of OpenGL.UNSIGNED_BYTE, OpenGL.UNSIGNED_SHORT, or OpenGL.UNSIGNED_INT.</param>
        /// <param name="indices">Specifies a pointer to the location where the indices are stored.</param>
        public void DrawElements(uint mode, int count, uint type, IntPtr indices)
        {
            PreGLCall();
            glDrawElements(mode, count, type, indices);
            PostGLCall();
        }

        /// <summary>
        /// Draws a rectangle of pixel data at the current raster position.
        /// </summary>
        /// <param name="width">Width of pixel data.</param>
        /// <param name="height">Height of pixel data.</param>
        /// <param name="format">Format of pixel data.</param>
        /// <param name="pixels">Pixel data buffer.</param>
        public void DrawPixels(int width, int height, uint format, float[] pixels)
        {
          PreGLCall();
          glDrawPixels(width, height, format, GL_FLOAT, pixels);
          PostGLCall();
        }

        /// <summary>
        /// Draws a rectangle of pixel data at the current raster position.
        /// </summary>
        /// <param name="width">Width of pixel data.</param>
        /// <param name="height">Height of pixel data.</param>
        /// <param name="format">Format of pixel data.</param>
        /// <param name="pixels">Pixel data buffer.</param>
        public void DrawPixels(int width, int height, uint format, uint[] pixels)
        {
          PreGLCall();
          glDrawPixels(width, height, format, GL_UNSIGNED_INT, pixels);
          PostGLCall();
        }

        /// <summary>
        /// Draws a rectangle of pixel data at the current raster position.
        /// </summary>
        /// <param name="width">Width of pixel data.</param>
        /// <param name="height">Height of pixel data.</param>
        /// <param name="format">Format of pixel data.</param>
        /// <param name="pixels">Pixel data buffer.</param>
        public void DrawPixels(int width, int height, uint format, ushort[] pixels)
        {
          PreGLCall();
          glDrawPixels(width, height, format, GL_UNSIGNED_SHORT, pixels);
          PostGLCall();
        }

        /// <summary>
        /// Draws a rectangle of pixel data at the current raster position.
        /// </summary>
        /// <param name="width">Width of pixel data.</param>
        /// <param name="height">Height of pixel data.</param>
        /// <param name="format">Format of pixel data.</param>
        /// <param name="pixels">Pixel data buffer.</param>
        public void DrawPixels(int width, int height, uint format, byte[] pixels)
        {
          PreGLCall();
          glDrawPixels(width, height, format, GL_UNSIGNED_BYTE, pixels);
          PostGLCall();
        }

        /// <summary>
        /// Draws a rectangle of pixel data at the current raster position.
        /// </summary>
        /// <param name="width">Width of pixel data.</param>
        /// <param name="height">Height of pixel data.</param>
        /// <param name="format">Format of pixel data.</param>
        /// <param name="type">The GL data type.</param>
        /// <param name="pixels">Pixel data buffer.</param>
        public void DrawPixels(int width, int height, uint format, uint type, IntPtr pixels)
        {
          PreGLCall();
          glDrawPixels(width, height, format, type, pixels);
          PostGLCall();
        }

        /// <summary>
        /// Flag edges as either boundary or nonboundary.
        /// </summary>
        /// <param name="flag">Specifies the current edge flag	value, either OpenGL.TRUE or OpenGL.FALSE. The initial value is OpenGL.TRUE.</param>
		public void EdgeFlag (byte flag)
        {
            PreGLCall();
            glEdgeFlag(flag);
            PostGLCall();
        }

        /// <summary>
        /// Define an array of edge flags.
        /// </summary>
        /// <param name="stride">Specifies the byte offset between consecutive edge flags. If stride is	0 (the initial value), the edge	flags are understood to	be tightly packed in the array.</param>
        /// <param name="pointer">Specifies a pointer to the first edge flag in the array.</param>
		public void EdgeFlagPointer (int stride,  int[] pointer)
        {
            PreGLCall();
            glEdgeFlagPointer(stride, pointer);
            PostGLCall();
        }

        /// <summary>
        /// Flag edges as either boundary or nonboundary.
        /// </summary>
        /// <param name="flag">Specifies a pointer to an array that contains a single boolean element,	which replaces the current edge	flag value.</param>
		public void EdgeFlag( byte []flag)
        {
            PreGLCall();
            glEdgeFlagv(flag);
            PostGLCall();
        }

		/// <summary>
		/// Call this function to enable an OpenGL capability.
		/// </summary>
		/// <param name="cap">The capability you wish to enable.</param>
		public void Enable(uint cap)
		{
			PreGLCall();
			glEnable(cap);
			PostGLCall();
		}

		/// <summary>
		/// This function enables one of the client state arrays, such as a vertex array.
		/// </summary>
		/// <param name="array">The array to enable.</param>
		public void EnableClientState(uint array)
		{
			PreGLCall();
			glEnableClientState(array);
			PostGLCall();
		}

		/// <summary>
		/// This is not an imported OpenGL function, but very useful. If 'test' is
		/// true, cap is enabled, otherwise, it's disable.
		/// </summary>
		/// <param name="cap">The capability you want to enable.</param>
		/// <param name="test">The logical comparison.</param>
		public void EnableIf(uint cap, bool test)
		{
			if(test)	Enable(cap);
			else		Disable(cap);
		}

		/// <summary>
		/// Signals the End of drawing.
		/// </summary>
		public void End()
		{
			glEnd();
            
            //  Clear the begun flag.
            insideGLBegin = false;

            // This matches Begin()'s PreGLCall()
            PostGLCall();
		}

    /// <summary>
    /// This function ends the drawing of a NURBS curve.
    /// </summary>
    /// <param name="nurbsObject">The nurbs object.</param>
		public void EndCurve(IntPtr nurbsObject)
		{
			PreGLCall();
			gluEndCurve(nurbsObject);
			PostGLCall();
		}

		/// <summary>
		/// Ends the current display list compilation.
		/// </summary>
		public void EndList()
		{
			PreGLCall();
			glEndList();
			PostGLCall();
		}

    /// <summary>
    /// This function ends the drawing of a NURBS surface.
    /// </summary>
    /// <param name="nurbsObject">The nurbs object.</param>
		public void EndSurface(IntPtr nurbsObject)
		{
			PreGLCall();
			gluEndSurface(nurbsObject);
			PostGLCall();
		}
		
		/// <summary>
		/// Evaluate from the current evaluator.
		/// </summary>
		/// <param name="u">Domain coordinate.</param>
		public void EvalCoord1(double u)
		{
			PreGLCall();
			glEvalCoord1d(u);
			PostGLCall();
		}

        /// <summary>
        /// Evaluate from the current evaluator.
        /// </summary>
        /// <param name="u">Domain coordinate.</param>
		public void EvalCoord1( double []u)
        {
            PreGLCall();
            glEvalCoord1dv(u);
            PostGLCall();
        }

		/// <summary>
		/// Evaluate from the current evaluator.
		/// </summary>
		/// <param name="u">Domain coordinate.</param>
		public void EvalCoord1(float u)
		{
			PreGLCall();
			glEvalCoord1f(u);
			PostGLCall();
		}

        /// <summary>
        /// Evaluate from the current evaluator.
        /// </summary>
        /// <param name="u">Domain coordinate.</param>
		public void EvalCoord1( float []u)
        {
            PreGLCall();
            glEvalCoord1fv(u);
            PostGLCall();
        }

        /// <summary>
        /// Evaluate from the current evaluator.
        /// </summary>
        /// <param name="u">Domain coordinate.</param>
        /// <param name="v">Domain coordinate.</param>
		public void EvalCoord2(double u, double v)
        {
            PreGLCall();
            glEvalCoord2d(u, v);
            PostGLCall();
        }

        /// <summary>
        /// Evaluate from the current evaluator.
        /// </summary>
        /// <param name="u">Domain coordinate.</param>
        public void EvalCoord2(double[] u)
        {
            PreGLCall();
            glEvalCoord2dv(u);
            PostGLCall();
        }

        /// <summary>
        /// Evaluate from the current evaluator.
        /// </summary>
        /// <param name="u">Domain coordinate.</param>
        /// <param name="v">Domain coordinate.</param>
        public void EvalCoord2(float u, float v)
        {
            PreGLCall();
            glEvalCoord2f(u, v);
            PostGLCall();
        }

        /// <summary>
        /// Evaluate from the current evaluator.
        /// </summary>
        /// <param name="u">Domain coordinate.</param>
        public void EvalCoord2(float[] u)
        {
            PreGLCall();
            glEvalCoord2fv(u);
            PostGLCall();
        }

		/// <summary>
		/// Evaluates a 'mesh' from the current evaluators.
		/// </summary>
		/// <param name="mode">Drawing mode, can be POINT or LINE.</param>
		/// <param name="i1">Beginning of range.</param>
		/// <param name="i2">End of range.</param>
		public void EvalMesh1(uint mode, int i1, int i2)
		{
			PreGLCall();
			glEvalMesh1(mode, i1, i2);
			PostGLCall();
		}
		/// <summary>
		/// Evaluates a 'mesh' from the current evaluators.
		/// </summary>
		/// <param name="mode">Drawing mode, fill, point or line.</param>
		/// <param name="i1">Beginning of range.</param>
		/// <param name="i2">End of range.</param>
		/// <param name="j1">Beginning of range.</param>
		/// <param name="j2">End of range.</param>
		public void EvalMesh2(uint mode, int i1, int i2, int j1, int j2)
		{
			PreGLCall();
			glEvalMesh2(mode, i1, i2, j1, j2);
			PostGLCall();
		}

        /// <summary>
        /// Generate and evaluate a single point in a mesh.
        /// </summary>
        /// <param name="i">The integer value for grid domain variable i.</param>
		public void EvalPoint1(int i)
        {
            PreGLCall();
            glEvalPoint1(i);
            PostGLCall();
        }

        /// <summary>
        /// Generate and evaluate a single point in a mesh.
        /// </summary>
        /// <param name="i">The integer value for grid domain variable i.</param>
        /// <param name="j">The integer value for grid domain variable j.</param>
		public void EvalPoint2(int i, int j)
        {
            PreGLCall();
            glEvalPoint2(i, j);
            PostGLCall();
        }

		/// <summary>
		/// This function sets the feedback buffer, that will receive feedback data.
		/// </summary>
		/// <param name="size">Size of the buffer.</param>
		/// <param name="type">Type of data in the buffer.</param>
		/// <param name="buffer">The buffer itself.</param>
		public void FeedbackBuffer(int size, uint type, float []buffer)
		{
			PreGLCall();
			glFeedbackBuffer(size, type, buffer);
			PostGLCall();
		}

		/// <summary>
		/// This function is similar to flush, but in a sense does it more, as it
		/// executes all commands aon both the client and the server.
		/// </summary>
		public void Finish()
		{
			PreGLCall();
			glFinish();
			PostGLCall();
		}

		/// <summary>
		/// This forces OpenGL to execute any commands you have given it.
		/// </summary>
		public void Flush()
		{
			PreGLCall();
			glFlush();
			PostGLCall();
		}

		/// <summary>
		/// Sets a fog parameter.
		/// </summary>
		/// <param name="pname">The parameter to set.</param>
		/// <param name="param">The value to set it to.</param>
		public void Fog(uint pname, float param)
		{
			PreGLCall();
			glFogf(pname, param);
			PostGLCall();
		}

		/// <summary>
		/// Sets a fog parameter.
		/// </summary>
		/// <param name="pname">The parameter to set.</param>
		/// <param name="parameters">The values to set it to.</param>
		public void Fog(uint pname,  float[] parameters)
		{
			PreGLCall();
			glFogfv(pname, parameters);
			PostGLCall();
		}

		/// <summary>
		/// Sets a fog parameter.
		/// </summary>
		/// <param name="pname">The parameter to set.</param>
		/// <param name="param">The value to set it to.</param>
		public void Fog(uint pname, int param)
		{
			PreGLCall();
			glFogi(pname, param);
			PostGLCall();
		}

		/// <summary>
		/// Sets a fog parameter.
		/// </summary>
		/// <param name="pname">The parameter to set.</param>
		/// <param name="parameters">The values to set it to.</param>
		public void Fog(uint pname,  int[] parameters)
		{
			PreGLCall();
			glFogiv(pname, parameters);
			PostGLCall();
		}

		/// <summary>
		/// This function sets what defines a front face.
		/// </summary>
		/// <param name="mode">Winding mode, counter clockwise by default.</param>
		public void FrontFace(uint mode)
		{
			PreGLCall();
			glFrontFace(mode);
			PostGLCall();
		}

		/// <summary>
		/// This function creates a frustrum transformation and mulitplies it to the current
		/// matrix (which in most cases should be the projection matrix).
		/// </summary>
		/// <param name="left">Left clip position.</param>
		/// <param name="right">Right clip position.</param>
		/// <param name="bottom">Bottom clip position.</param>
		/// <param name="top">Top clip position.</param>
		/// <param name="zNear">Near clip position.</param>
		/// <param name="zFar">Far clip position.</param>
		public void Frustum(double left, double right, double bottom, 
			double top, double zNear, double zFar)
		{
			PreGLCall();
			glFrustum(left, right, bottom, top, zNear, zFar);
			PostGLCall();
		}

		/// <summary>
		/// This function generates 'range' number of contiguos display list indices.
		/// </summary>
		/// <param name="range">The number of lists to generate.</param>
		/// <returns>The first list.</returns>
		public uint GenLists(int range)
		{
			PreGLCall();
			uint list = glGenLists(range);
			PostGLCall();

			return list;
		}

		/// <summary>
		/// Create a set of unique texture names.
		/// </summary>
		/// <param name="n">Number of names to create.</param>
		/// <param name="textures">Array to store the texture names.</param>
		public void GenTextures(int n, uint []textures)
		{
			PreGLCall();
			glGenTextures(n, textures);
			PostGLCall();
		}

		/// <summary>
		/// This function queries OpenGL for data, and puts it in the buffer supplied.
		/// </summary>
		/// <param name="pname">The parameter to query.</param>
		/// <param name="parameters"></param>
		public void GetBooleanv (uint pname, byte[] parameters)
		{
			PreGLCall();
			glGetBooleanv(pname, parameters);
			PostGLCall();
		}

        /// <summary>
        /// This function queries OpenGL for data, and puts it in the buffer supplied.
        /// </summary>
        /// <param name="pname">The parameter to query.</param>
        /// <param name="parameters"></param>
        public void GetBooleanv(Enumerations.GetTarget pname, byte[] parameters)
        {
            PreGLCall();
            glGetBooleanv((uint)pname, parameters);
            PostGLCall();
        }

        /// <summary>
        /// Return the coefficients of the specified clipping plane.
        /// </summary>
        /// <param name="plane">Specifies a	clipping plane.	 The number of clipping planes depends on the implementation, but at least six clipping planes are supported. They are identified by symbolic names of the form OpenGL.CLIP_PLANEi where 0 Less Than i Less Than OpenGL.MAX_CLIP_PLANES.</param>
        /// <param name="equation">Returns four double-precision values that are the coefficients of the plane equation of plane in eye coordinates. The initial value is (0, 0, 0, 0).</param>
		public void GetClipPlane (uint plane, double []equation)
        {
            PreGLCall();
            glGetClipPlane(plane, equation);
            PostGLCall();
        }

		/// <summary>
		/// This function queries OpenGL for data, and puts it in the buffer supplied.
		/// </summary>
		/// <param name="pname">The parameter to query.</param>
		/// <param name="parameters">The buffer to put that data into.</param>
		public void GetDouble(uint pname, double []parameters)
		{
			PreGLCall();
			glGetDoublev(pname, parameters);
			PostGLCall();
		}

        /// <summary>
        /// This function queries OpenGL for data, and puts it in the buffer supplied.
        /// </summary>
        /// <param name="pname">The parameter to query.</param>
        /// <param name="parameters">The buffer to put that data into.</param>
        public void GetDouble(Enumerations.GetTarget pname, double[] parameters)
        {
            PreGLCall();
            glGetDoublev((uint)pname, parameters);
            PostGLCall();
        }

		/// <summary>
		/// Get the current OpenGL error code.
		/// </summary>
		/// <returns>The current OpenGL error code.</returns>
		public uint GetError()
		{
			return glGetError();
		}

        /// <summary>
        /// Get the current OpenGL error code.
        /// </summary>
        /// <returns>The current OpenGL error code.</returns>
        public Enumerations.ErrorCode GetErrorCode()
        {
            return (Enumerations.ErrorCode)glGetError();
        }

		/// <summary>
		/// This this function to query OpenGL values.
		/// </summary>
		/// <param name="pname">The parameter to query.</param>
		/// <param name="parameters">The parameters</param>
		public void GetFloat(uint pname, float[] parameters)
		{
			PreGLCall();
			glGetFloatv(pname, parameters);
			PostGLCall();
		}

        /// <summary>
        /// This this function to query OpenGL values.
        /// </summary>
        /// <param name="pname">The parameter to query.</param>
        /// <param name="parameters">The parameters</param>
        public void GetFloat(Enumerations.GetTarget pname, float[] parameters)
        {
            PreGLCall();
            glGetFloatv((uint)pname, parameters);
            PostGLCall();
        }

		/// <summary>
		/// Use this function to query OpenGL parameter values.
		/// </summary>
		/// <param name="pname">The Parameter to query</param>
		/// <param name="parameters">An array to put the values into.</param>
		public void GetInteger(uint pname, int[] parameters)
		{
			PreGLCall();
			glGetIntegerv(pname, parameters);
			PostGLCall();
		}

        /// <summary>
        /// Use this function to query OpenGL parameter values.
        /// </summary>
        /// <param name="pname">The Parameter to query</param>
        /// <param name="parameters">An array to put the values into.</param>
        public void GetInteger(Enumerations.GetTarget pname, int[] parameters)
        {
            PreGLCall();
            glGetIntegerv((uint)pname, parameters);
            PostGLCall();
        }

        /// <summary>
        /// Return light source parameter values.
        /// </summary>
        /// <param name="light">Specifies a light source. The number of possible lights depends on the implementation, but at least eight lights are supported. They are identified by symbolic names of the form OpenGL.LIGHTi where i ranges from 0 to the value of OpenGL.GL_MAX_LIGHTS - 1.</param>
        /// <param name="pname">Specifies a light source parameter for light.</param>
        /// <param name="parameters">Returns the requested data.</param>
		public void GetLight(uint light, uint pname, float []parameters)
        {
            PreGLCall();
            glGetLightfv(light, pname, parameters);
            PostGLCall();
        }

        /// <summary>
        /// Return light source parameter values.
        /// </summary>
        /// <param name="light">Specifies a light source. The number of possible lights depends on the implementation, but at least eight lights are supported. They are identified by symbolic names of the form OpenGL.LIGHTi where i ranges from 0 to the value of OpenGL.GL_MAX_LIGHTS - 1.</param>
        /// <param name="pname">Specifies a light source parameter for light.</param>
        /// <param name="parameters">Returns the requested data.</param>
        public void GetLight(uint light, uint pname, int[] parameters)
        {
            PreGLCall();
            glGetLightiv(light, pname, parameters);
            PostGLCall();
        }

        /// <summary>
        /// Return evaluator parameters.
        /// </summary>
        /// <param name="target">Specifies the symbolic name of a map.</param>
        /// <param name="query">Specifies which parameter to return.</param>
        /// <param name="v">Returns the requested data.</param>
		public void GetMap(uint target, uint query, double []v)
        {
            PreGLCall();
            glGetMapdv(target, query, v);
            PostGLCall();
        }

        /// <summary>
        /// Return evaluator parameters.
        /// </summary>
        /// <param name="target">Specifies the symbolic name of a map.</param>
        /// <param name="query">Specifies which parameter to return.</param>
        /// <param name="v">Returns the requested data.</param>
        public void GetMap(Enumerations.GetMapTarget target, uint query, double[] v)
        {
            PreGLCall();
            glGetMapdv((uint)target, query, v);
            PostGLCall();
        }

        /// <summary>
        /// Return evaluator parameters.
        /// </summary>
        /// <param name="target">Specifies the symbolic name of a map.</param>
        /// <param name="query">Specifies which parameter to return.</param>
        /// <param name="v">Returns the requested data.</param>
        public void GetMap(Enumerations.GetMapTarget target, uint query, float[] v)
        {
            PreGLCall();
            glGetMapfv((uint)target, query, v);
            PostGLCall();
        }

        /// <summary>
        /// Return evaluator parameters.
        /// </summary>
        /// <param name="target">Specifies the symbolic name of a map.</param>
        /// <param name="query">Specifies which parameter to return.</param>
        /// <param name="v">Returns the requested data.</param>
		public void GetMap(uint target, uint query, float []v)
        {
            PreGLCall();
            glGetMapfv(target, query, v);
            PostGLCall();
        }

        /// <summary>
        /// Return evaluator parameters.
        /// </summary>
        /// <param name="target">Specifies the symbolic name of a map.</param>
        /// <param name="query">Specifies which parameter to return.</param>
        /// <param name="v">Returns the requested data.</param>
        public void GetMap(Enumerations.GetMapTarget target, uint query, int[] v)
        {
            PreGLCall();
            glGetMapiv((uint)target, query, v);
            PostGLCall();
        }

        /// <summary>
        /// Return evaluator parameters.
        /// </summary>
        /// <param name="target">Specifies the symbolic name of a map.</param>
        /// <param name="query">Specifies which parameter to return.</param>
        /// <param name="v">Returns the requested data.</param>
		public void GetMap(uint target, uint query, int []v)
        {
            PreGLCall();
            glGetMapiv(target, query, v);
            PostGLCall();
        }

        /// <summary>
        /// Return material parameters.
        /// </summary>
        /// <param name="face">Specifies which of the two materials is being queried. OpenGL.FRONT or OpenGL.BACK are accepted, representing the front and back materials, respectively.</param>
        /// <param name="pname">Specifies the material parameter to return.</param>
        /// <param name="parameters">Returns the requested data.</param>
        public void GetMaterial(uint face, uint pname, float[] parameters)
        {
            PreGLCall();
            glGetMaterialfv(face, pname, parameters);
            PostGLCall();
        }

        /// <summary>
        /// Return material parameters.
        /// </summary>
        /// <param name="face">Specifies which of the two materials is being queried. OpenGL.FRONT or OpenGL.BACK are accepted, representing the front and back materials, respectively.</param>
        /// <param name="pname">Specifies the material parameter to return.</param>
        /// <param name="parameters">Returns the requested data.</param>
        public void GetMaterial(uint face, uint pname, int[] parameters)
        {
            PreGLCall();
            glGetMaterialiv(face, pname, parameters);
            PostGLCall();
        }

        /// <summary>
        /// Return the specified pixel map.
        /// </summary>
        /// <param name="map">Specifies the	name of	the pixel map to return.</param>
        /// <param name="values">Returns the pixel map	contents.</param>
		public void GetPixelMap(uint map, float []values)
        {
            PreGLCall();
            glGetPixelMapfv(map, values);
            PostGLCall();
        }

        /// <summary>
        /// Return the specified pixel map.
        /// </summary>
        /// <param name="map">Specifies the	name of	the pixel map to return.</param>
        /// <param name="values">Returns the pixel map	contents.</param>
		public void GetPixelMap(uint map, uint []values)
        {
            PreGLCall();
            glGetPixelMapuiv(map, values);
            PostGLCall();
        }

        /// <summary>
        /// Return the specified pixel map.
        /// </summary>
        /// <param name="map">Specifies the	name of	the pixel map to return.</param>
        /// <param name="values">Returns the pixel map	contents.</param>
		public void GetPixelMap(uint map, ushort []values)
        {
            PreGLCall();
            glGetPixelMapusv(map, values);
            PostGLCall();
        }

        /// <summary>
        /// Return the address of the specified pointer.
        /// </summary>
        /// <param name="pname">Specifies the array or buffer pointer to be returned.</param>
        /// <param name="parameters">Returns the pointer value specified by parameters.</param>
		public void GetPointerv (uint pname, int[] parameters)
        {
            PreGLCall();
            glGetPointerv(pname, parameters);
            PostGLCall();
        }

        /// <summary>
        /// Return the polygon stipple pattern.
        /// </summary>
        /// <param name="mask">Returns the stipple pattern. The initial value is all 1's.</param>
		public void GetPolygonStipple (byte []mask)
        {
            PreGLCall();
            glGetPolygonStipple(mask);
            PostGLCall();
        }

        /// <summary>
        /// Return a string	describing the current GL connection.
        /// </summary>
        /// <param name="name">Specifies a symbolic constant, one of OpenGL.VENDOR, OpenGL.RENDERER, OpenGL.VERSION, or OpenGL.EXTENSIONS.</param>
        /// <returns>Pointer to the specified string.</returns>
		public unsafe string GetString (uint name)
		{
            PreGLCall();
			sbyte* pStr = glGetString(name);
			var str = new string(pStr);
            PostGLCall();

            return str;
		}

        /// <summary>
        /// Return texture environment parameters.
        /// </summary>
        /// <param name="target">Specifies a texture environment.  Must be OpenGL.TEXTURE_ENV.</param>
        /// <param name="pname">Specifies the	symbolic name of a texture environment parameter.  Accepted values are OpenGL.TEXTURE_ENV_MODE, and OpenGL.TEXTURE_ENV_COLOR.</param>
        /// <param name="parameters">Returns the requested	data.</param>
		public void GetTexEnv(uint target, uint pname, float []parameters)
        {
            PreGLCall();
            glGetTexEnvfv(target, pname, parameters);
            PostGLCall();
        }

        /// <summary>
        /// Return texture environment parameters.
        /// </summary>
        /// <param name="target">Specifies a texture environment.  Must be OpenGL.TEXTURE_ENV.</param>
        /// <param name="pname">Specifies the	symbolic name of a texture environment parameter.  Accepted values are OpenGL.TEXTURE_ENV_MODE, and OpenGL.TEXTURE_ENV_COLOR.</param>
        /// <param name="parameters">Returns the requested	data.</param>
        public void GetTexEnv(uint target, uint pname, int[] parameters)
        {
            PreGLCall();
            glGetTexEnviv(target, pname, parameters);
            PostGLCall();
        }

        /// <summary>
        /// Control the generation of texture coordinates.
        /// </summary>
        /// <param name="coord">Specifies a texture coordinate. Must be one of OpenGL.S, OpenGL.T, OpenGL.R, or OpenGL.Q.</param>
        /// <param name="pname">Specifies the symbolic name of the texture-coordinate generation function. Must be OpenGL.TEXTURE_GEN_MODE.</param>
        /// <param name="parameters">Specifies a single-valued texture generation parameter, one of OpenGL.OBJECT_LINEAR, OpenGL.EYE_LINEAR, or OpenGL.SPHERE_MAP.</param>
        public void GetTexGen(uint coord, uint pname, double[] parameters) 
        {
            PreGLCall();
            glGetTexGendv(coord, pname, parameters);
            PostGLCall();
        }

        /// <summary>
        /// Control the generation of texture coordinates.
        /// </summary>
        /// <param name="coord">Specifies a texture coordinate. Must be one of OpenGL.S, OpenGL.T, OpenGL.R, or OpenGL.Q.</param>
        /// <param name="pname">Specifies the symbolic name of the texture-coordinate generation function. Must be OpenGL.TEXTURE_GEN_MODE.</param>
        /// <param name="parameters">Specifies a single-valued texture generation parameter, one of OpenGL.OBJECT_LINEAR, OpenGL.EYE_LINEAR, or OpenGL.SPHERE_MAP.</param>
        public void GetTexGen(uint coord, uint pname, float[] parameters)
        {
            PreGLCall();
            glGetTexGenfv(coord, pname, parameters);
            PostGLCall();
        }
        
        /// <summary>
        /// Control the generation of texture coordinates.
        /// </summary>
        /// <param name="coord">Specifies a texture coordinate. Must be one of OpenGL.S, OpenGL.T, OpenGL.R, or OpenGL.Q.</param>
        /// <param name="pname">Specifies the symbolic name of the texture-coordinate generation function. Must be OpenGL.TEXTURE_GEN_MODE.</param>
        /// <param name="parameters">Specifies a single-valued texture generation parameter, one of OpenGL.OBJECT_LINEAR, OpenGL.EYE_LINEAR, or OpenGL.SPHERE_MAP.</param>
        public void GetTexGen(uint coord, uint pname, int[] parameters)
        {
            PreGLCall();
            glGetTexGeniv(coord, pname, parameters);
            PostGLCall();
        }

        /// <summary>
        /// Return a texture image.
        /// </summary>
        /// <param name="target">Specifies which texture is to	be obtained. OpenGL.TEXTURE_1D and OpenGL.TEXTURE_2D are accepted.</param>
        /// <param name="level">Specifies the level-of-detail number of the desired image.  Level	0 is the base image level.  Level n is the nth mipmap reduction image.</param>
        /// <param name="format">Specifies a pixel format for the returned data.</param>
        /// <param name="type">Specifies a pixel type for the returned data.</param>
        /// <param name="pixels">Returns the texture image.  Should be	a pointer to an array of the type specified by type.</param>
		public void GetTexImage (uint target, int level, uint format, uint type, int []pixels)
        {
            PreGLCall();
            glGetTexImage(target, level, format, type, pixels);
            PostGLCall();
        }

        /// <summary>
        /// Return texture parameter values for a specific level of detail.
        /// </summary>
        /// <param name="target">Specifies the	symbolic name of the target texture.</param>
        /// <param name="level">Specifies the level-of-detail	number of the desired image.  Level	0 is the base image level.  Level n is the nth mipmap reduction image.</param>
        /// <param name="pname">Specifies the symbolic name of a texture parameter.</param>
        /// <param name="parameters">Returns the requested	data.</param>
		public void GetTexLevelParameter(uint target, int level, uint pname, float []parameters)
        {
            PreGLCall();
            glGetTexLevelParameterfv(target, level, pname, parameters);
            PostGLCall();
        }

        /// <summary>
        /// Return texture parameter values for a specific level of detail.
        /// </summary>
        /// <param name="target">Specifies the	symbolic name of the target texture.</param>
        /// <param name="level">Specifies the level-of-detail	number of the desired image.  Level	0 is the base image level.  Level n is the nth mipmap reduction image.</param>
        /// <param name="pname">Specifies the symbolic name of a texture parameter.</param>
        /// <param name="parameters">Returns the requested	data.</param>
        public void GetTexLevelParameter(uint target, int level, uint pname, int[] parameters)
        {
            PreGLCall();
            glGetTexLevelParameteriv(target, level, pname, parameters);
            PostGLCall();
        }

        /// <summary>
        /// Return texture parameter values.
        /// </summary>
        /// <param name="target">Specifies the symbolic name of the target texture.</param>
        /// <param name="pname">Specifies the symbolic name of a texture parameter.</param>
        /// <param name="parameters">Returns the texture parameters.</param>
        public void GetTexParameter(uint target, uint pname, float[] parameters) 
        {
            PreGLCall();
            glGetTexParameterfv(target, pname, parameters);
            PostGLCall();
        }
        /// <summary>
        /// Return texture parameter values.
        /// </summary>
        /// <param name="target">Specifies the symbolic name of the target texture.</param>
        /// <param name="pname">Specifies the symbolic name of a texture parameter.</param>
        /// <param name="parameters">Returns the texture parameters.</param>
        public void GetTexParameter(uint target, uint pname, int[] parameters)
        {
            PreGLCall();
            glGetTexParameteriv(target, pname, parameters);
            PostGLCall();
        }

        /// <summary>
        /// Specify implementation-specific hints.
        /// </summary>
        /// <param name="target">Specifies a symbolic constant indicating the behavior to be controlled.</param>
        /// <param name="mode">Specifies a symbolic constant indicating the desired behavior.</param>
		public void Hint (uint target, uint mode)
        {
            PreGLCall();
            glHint(target, mode);
            PostGLCall();
        }

        /// <summary>
        /// Specify implementation-specific hints.
        /// </summary>
        /// <param name="target">Specifies a symbolic constant indicating the behavior to be controlled.</param>
        /// <param name="mode">Specifies a symbolic constant indicating the desired behavior.</param>
        public void Hint(Enumerations.HintTarget target, Enumerations.HintMode mode)
        {
            PreGLCall();
            glHint((uint)target, (uint)mode);
            PostGLCall();
        }

        /// <summary>
        /// Control	the writing of individual bits in the color	index buffers.
        /// </summary>
        /// <param name="mask">Specifies a bit	mask to	enable and disable the writing of individual bits in the color index buffers. Initially, the mask is all 1's.</param>
		public void IndexMask (uint mask)
        {
            PreGLCall();
            glIndexMask(mask);
            PostGLCall();
        }

        /// <summary>
        /// Define an array of color indexes.
        /// </summary>
        /// <param name="type">Specifies the data type of each color index in the array.  Symbolic constants OpenGL.UNSIGNED_BYTE, OpenGL.SHORT, OpenGL.INT, OpenGL.FLOAT, and OpenGL.DOUBLE are accepted.</param>
        /// <param name="stride">Specifies the byte offset between consecutive color indexes.  If stride is 0 (the initial value), the color indexes are understood	to be tightly packed in the array.</param>
        /// <param name="pointer">Specifies a pointer to the first index in the array.</param>
		public void IndexPointer (uint type, int stride,  int[] pointer)
        {
            PreGLCall();
            glIndexPointer(type, stride, pointer);
            PostGLCall();
        }

        /// <summary>
        /// Set the current color index.
        /// </summary>
        /// <param name="c">Specifies the new value for the current color index.</param>
		public void Index(double c)
        {
            PreGLCall();
            glIndexd(c);
            PostGLCall();
        }

        /// <summary>
        /// Set the current color index.
        /// </summary>
        /// <param name="c">Specifies the new value for the current color index.</param>
        public void Index(double[] c)
        {
            PreGLCall();
            glIndexdv(c);
            PostGLCall();
        }

        /// <summary>
        /// Set the current color index.
        /// </summary>
        /// <param name="c">Specifies the new value for the current color index.</param>
        public void Index(float c)
        {
            PreGLCall();
            glIndexf(c);
            PostGLCall();
        }

        /// <summary>
        /// Set the current color index.
        /// </summary>
        /// <param name="c">Specifies the new value for the current color index.</param>
        public void Index(float[] c)
        {
            PreGLCall();
            glIndexfv(c);
            PostGLCall();
        }

        /// <summary>
        /// Set the current color index.
        /// </summary>
        /// <param name="c">Specifies the new value for the current color index.</param>
        public void Index(int c)
        {
            PreGLCall();
            glIndexi(c);
            PostGLCall();
        }

        /// <summary>
        /// Set the current color index.
        /// </summary>
        /// <param name="c">Specifies the new value for the current color index.</param>
        public void Index(int[] c)
        {
            PreGLCall();
            glIndexiv(c);
            PostGLCall();
        }

        /// <summary>
        /// Set the current color index.
        /// </summary>
        /// <param name="c">Specifies the new value for the current color index.</param>
        public void Index(short c)
        {
            PreGLCall();
            glIndexs(c);
            PostGLCall();
        }

        /// <summary>
        /// Set the current color index.
        /// </summary>
        /// <param name="c">Specifies the new value for the current color index.</param>
        public void Index(short[] c)
        {
            PreGLCall();
            glIndexsv(c);
            PostGLCall();
        }

        /// <summary>
        /// Set the current color index.
        /// </summary>
        /// <param name="c">Specifies the new value for the current color index.</param>
        public void Index(byte c)
        {
            PreGLCall();
            glIndexub(c);
            PostGLCall();
        }

        /// <summary>
        /// Set the current color index.
        /// </summary>
        /// <param name="c">Specifies the new value for the current color index.</param>
        public void Index(byte[] c)
        {
            PreGLCall();
            glIndexubv(c);
            PostGLCall();
        }

		/// <summary>
		/// This function initialises the select buffer names.
		/// </summary>
		public void InitNames()
		{
			PreGLCall();
			glInitNames();
			PostGLCall();
		}

        /// <summary>
        /// Simultaneously specify and enable several interleaved arrays.
        /// </summary>
        /// <param name="format">Specifies the type of array to enable.</param>
        /// <param name="stride">Specifies the offset in bytes between each aggregate array element.</param>
        /// <param name="pointer">The array.</param>
		public void InterleavedArrays (uint format, int stride,  int[] pointer)
        {
            PreGLCall();
            glInterleavedArrays(format, stride, pointer);
            PostGLCall();
        }

		/// <summary>
		/// Use this function to query if a certain OpenGL function is enabled or not.
		/// </summary>
		/// <param name="cap">The capability to test.</param>
		/// <returns>True if the capability is enabled, otherwise, false.</returns>
		public bool IsEnabled (uint cap)
		{
			PreGLCall();
			byte e = glIsEnabled(cap);
			PostGLCall();

			return e != 0;
		}

		/// <summary>
		/// This function determines whether a specified value is a display list.
		/// </summary>
		/// <param name="list">The value to test.</param>
		/// <returns>TRUE if it is a list, FALSE otherwise.</returns>
		public byte IsList(uint list)
		{
			PreGLCall();
			byte islist = glIsList(list);
			PostGLCall();

			return islist;
		}

        /// <summary>
        /// Determine if a name corresponds	to a texture.
        /// </summary>
        /// <param name="texture">Specifies a value that may be the name of a texture.</param>
        /// <returns>True if texture is a texture object.</returns>
		public byte IsTexture (uint texture)
        {
            PreGLCall();
            byte returnValue = glIsTexture(texture);
            PostGLCall();

            return returnValue;
        }

		/// <summary>
		/// This function sets a parameter of the lighting model.
		/// </summary>
		/// <param name="pname">The name of the parameter.</param>
		/// <param name="param">The parameter to set it to.</param>
		public void LightModel(uint pname, float param)
		{
			PreGLCall();
			glLightModelf(pname, param);
			PostGLCall();
		}

		/// <summary>
		/// This function sets a parameter of the lighting model.
		/// </summary>
		/// <param name="pname">The name of the parameter.</param>
		/// <param name="param">The parameter to set it to.</param>
        public void LightModel(Enumerations.LightModelParameter pname, float param)
		{
			PreGLCall();
			glLightModelf((uint)pname, param);
			PostGLCall();
		}

		/// <summary>
		/// This function sets a parameter of the lighting model.
		/// </summary>
		/// <param name="pname">The name of the parameter.</param>
		/// <param name="parameters">The parameter to set it to.</param>
		public void LightModel(uint pname,  float[] parameters)
		{
			PreGLCall();
			glLightModelfv(pname, parameters);
			PostGLCall();
		}

		/// <summary>
		/// This function sets a parameter of the lighting model.
		/// </summary>
		/// <param name="pname">The name of the parameter.</param>
		/// <param name="parameters">The parameter to set it to.</param>
        public void LightModel(Enumerations.LightModelParameter pname, float[] parameters)
		{
			PreGLCall();
			glLightModelfv((uint)pname, parameters);
			PostGLCall();
		}

		/// <summary>
		/// This function sets a parameter of the lighting model.
		/// </summary>
		/// <param name="pname">The name of the parameter.</param>
		/// <param name="param">The parameter to set it to.</param>
		public void LightModel(uint pname, int param)
		{
			PreGLCall();
			glLightModeli(pname, param);
			PostGLCall();
		}

		/// <summary>
		/// This function sets a parameter of the lighting model.
		/// </summary>
		/// <param name="pname">The name of the parameter.</param>
		/// <param name="param">The parameter to set it to.</param>
        public void LightModel(Enumerations.LightModelParameter pname, int param)
		{
			PreGLCall();
			glLightModeli((uint)pname, param);
			PostGLCall();
		}

		/// <summary>
		/// This function sets a parameter of the lighting model.
		/// </summary>
		/// <param name="pname">The name of the parameter.</param>
		/// <param name="parameters">The parameter to set it to.</param>
		public void LightModel (uint pname, int[] parameters)
		{
			PreGLCall();
			glLightModeliv(pname, parameters);
			PostGLCall();
		}

		/// <summary>
		/// This function sets a parameter of the lighting model.
		/// </summary>
		/// <param name="pname">The name of the parameter.</param>
		/// <param name="parameters">The parameter to set it to.</param>
        public void LightModel(Enumerations.LightModelParameter pname, int[] parameters)
		{
			PreGLCall();
			glLightModeliv((uint)pname, parameters);
			PostGLCall();
		}

		/// <summary>
		/// Set the parameter (pname) of the light 'light'.
		/// </summary>
		/// <param name="light">The light you wish to set parameters for.</param>
		/// <param name="pname">The parameter you want to set.</param>
		/// <param name="param">The value that you want to set the parameter to.</param>
		public void Light(uint light, uint pname, float param)
		{
            PreGLCall();
			glLightf(light, pname, param);
			PostGLCall();
		}

        /// <summary>
        /// Set the parameter (pname) of the light 'light'.
        /// </summary>
        /// <param name="light">The light you wish to set parameters for.</param>
        /// <param name="pname">The parameter you want to set.</param>
        /// <param name="param">The value that you want to set the parameter to.</param>
        public void Light(Enumerations.LightName light, Enumerations.LightParameter pname, float param)
        {
            PreGLCall();
            glLightf((uint)light, (uint)pname, param);
            PostGLCall();
        }

		/// <summary>
		/// Set the parameter (pname) of the light 'light'.
		/// </summary>
		/// <param name="light">The light you wish to set parameters for.</param>
		/// <param name="pname">The parameter you want to set.</param>
		/// <param name="parameters">The value that you want to set the parameter to.</param>
		public void Light(uint light, uint pname,  float[] parameters)
		{
			PreGLCall();
			glLightfv(light, pname, parameters);
			PostGLCall();
		}
        
        /// <summary>
        /// Set the parameter (pname) of the light 'light'.
        /// </summary>
        /// <param name="light">The light you wish to set parameters for.</param>
        /// <param name="pname">The parameter you want to set.</param>
        /// <param name="parameters">The value that you want to set the parameter to.</param>
        public void Light(Enumerations.LightName light, Enumerations.LightParameter pname, float[] parameters)
        {
            PreGLCall();
            glLightfv((uint)light, (uint)pname, parameters);
            PostGLCall();
        }

		/// <summary>
		/// Set the parameter (pname) of the light 'light'.
		/// </summary>
		/// <param name="light">The light you wish to set parameters for.</param>
		/// <param name="pname">The parameter you want to set.</param>
		/// <param name="param">The value that you want to set the parameter to.</param>
		public void Light(uint light, uint pname, int param)
		{
			PreGLCall();
			glLighti(light, pname, param);
			PostGLCall();
		}

        /// <summary>
        /// Set the parameter (pname) of the light 'light'.
        /// </summary>
        /// <param name="light">The light you wish to set parameters for.</param>
        /// <param name="pname">The parameter you want to set.</param>
        /// <param name="param">The value that you want to set the parameter to.</param>
        public void Light(Enumerations.LightName light, Enumerations.LightParameter pname, int param)
        {
            PreGLCall();
            glLighti((uint)light, (uint)pname, param);
            PostGLCall();
        }

        /// <summary>
        /// Set the parameter (pname) of the light 'light'.
        /// </summary>
        /// <param name="light">The light you wish to set parameters for.</param>
        /// <param name="pname">The parameter you want to set.</param>
        /// <param name="parameters">The parameters.</param>
		public void Light(uint light, uint pname,  int []parameters)
		{
			PreGLCall();
			glLightiv(light, pname, parameters);
			PostGLCall();
		}

        /// <summary>
        /// Set the parameter (pname) of the light 'light'.
        /// </summary>
        /// <param name="light">The light you wish to set parameters for.</param>
        /// <param name="pname">The parameter you want to set.</param>
        /// <param name="parameters">The parameters.</param>
        public void Light(Enumerations.LightName light, Enumerations.LightParameter pname, int[] parameters)
        {
            PreGLCall();
            glLightiv((uint)light, (uint)pname, parameters);
            PostGLCall();
        }

        /// <summary>
        /// Specify the line stipple pattern.
        /// </summary>
        /// <param name="factor">Specifies a multiplier for each bit in the line stipple pattern.  If factor is 3, for example, each bit in the pattern is used three times before the next	bit in the pattern is used. factor is clamped to the range	[1, 256] and defaults to 1.</param>
        /// <param name="pattern">Specifies a 16-bit integer whose bit	pattern determines which fragments of a line will be drawn when	the line is rasterized.	 Bit zero is used first; the default pattern is all 1's.</param>
        public void LineStipple(int factor, ushort pattern)
        {
            PreGLCall();
            glLineStipple(factor, pattern);
            PostGLCall();
        }

		/// <summary>
		/// Set's the current width of lines.
		/// </summary>
		/// <param name="width">New line width to set.</param>
		public void LineWidth(float width)
		{
			PreGLCall();
			glLineWidth(width);
			PostGLCall();
		}

        /// <summary>
        /// Set the display-list base for glCallLists.
        /// </summary>
        /// <param name="listbase">Specifies an integer offset that will be added to glCallLists offsets to generate display-list names. The initial value is 0.</param>
		public void ListBase (uint listbase)
        {
            PreGLCall();
            glListBase(listbase);
            PostGLCall();
        }

		/// <summary>
		/// Call this function to load the identity matrix into the current matrix stack.
		/// </summary>
		public void LoadIdentity()
		{
			PreGLCall();
			glLoadIdentity();
			PostGLCall();
		}

        /// <summary>
        /// Replace the current matrix with the specified matrix.
        /// </summary>
        /// <param name="m">Specifies a pointer to 16 consecutive values, which are used as the elements of a 4x4 column-major matrix.</param>
		public void LoadMatrix( double []m)
        {
            PreGLCall();
            glLoadMatrixd(m);
            PreGLCall();
        }

        /// <summary>
        /// Replace the current matrix with the specified matrix.
        /// </summary>
        /// <param name="m">Specifies a pointer to 16 consecutive values, which are used as the elements of a 4x4 column-major matrix.</param>
        public void LoadMatrixf(float[] m)
        {
            PreGLCall();
            glLoadMatrixf(m);
            PreGLCall();
        }

		/// <summary>
		/// This function replaces the name at the top of the selection names stack
		/// with 'name'.
		/// </summary>
		/// <param name="name">The name to replace it with.</param>
		public void LoadName (uint name)
		{
			PreGLCall();
			glLoadName(name);
			PostGLCall();
		}

        /// <summary>
        /// Specify a logical pixel operation for color index rendering.
        /// </summary>
        /// <param name="opcode">Specifies a symbolic constant	that selects a logical operation.</param>
		public void LogicOp (uint opcode)
        {
            PreGLCall();
            glLogicOp(opcode);
            PostGLCall();
        }

        /// <summary>
        /// Specify a logical pixel operation for color index rendering.
        /// </summary>
        /// <param name="logicOp">Specifies a symbolic constant	that selects a logical operation.</param>
        public void LogicOp(Enumerations.LogicOp logicOp)
        {
            PreGLCall();
            glLogicOp((uint)logicOp);
            PostGLCall();
        }

		/// <summary>
		/// This function transforms the projection matrix so that it looks at a certain
		/// point, from a certain point.
		/// </summary>
		/// <param name="eyex">Position of the eye.</param>
		/// <param name="eyey">Position of the eye.</param>
		/// <param name="eyez">Position of the eye.</param>
		/// <param name="centerx">Point to look at.</param>
		/// <param name="centery">Point to look at.</param>
		/// <param name="centerz">Point to look at.</param>
		/// <param name="upx">'Up' Vector X Component.</param>
		/// <param name="upy">'Up' Vector Y Component.</param>
		/// <param name="upz">'Up' Vector Z Component.</param>
		public void LookAt(double eyex, double eyey, double eyez, 
			double centerx, double centery, double centerz, 
			double upx, double upy, double upz)
		{
			PreGLCall();
			gluLookAt(eyex, eyey, eyez, centerx, centery, centerz, upx, upy, upz);
			PostGLCall();
		}

		/// <summary>
		/// Defines a 1D evaluator.
		/// </summary>
		/// <param name="target">What the control points represent (e.g. MAP1_VERTEX_3).</param>
		/// <param name="u1">Range of the variable 'u'.</param>
		/// <param name="u2">Range of the variable 'u'.</param>
		/// <param name="stride">Offset between beginning of one control point, and beginning of next.</param>
		/// <param name="order">The degree plus one, should agree with the number of control points.</param>
		/// <param name="points">The data for the points.</param>
		public void Map1(uint target, double u1, double u2, int stride, int order,  double []points)
		{
			PreGLCall();
			glMap1d(target, u1, u2, stride, order, points);
			PostGLCall();
		}

		/// <summary>
		/// Defines a 1D evaluator.
		/// </summary>
		/// <param name="target">What the control points represent (e.g. MAP1_VERTEX_3).</param>
		/// <param name="u1">Range of the variable 'u'.</param>
		/// <param name="u2">Range of the variable 'u'.</param>
		/// <param name="stride">Offset between beginning of one control point, and beginning of next.</param>
		/// <param name="order">The degree plus one, should agree with the number of control points.</param>
		/// <param name="points">The data for the points.</param>
		public void Map1(uint target, float u1, float u2, int stride, int order,  float []points)
		{
			PreGLCall();
			glMap1f(target, u1, u2, stride, order, points);
			PostGLCall();
		}

		/// <summary>
		/// Defines a 2D evaluator.
		/// </summary>
		/// <param name="target">What the control points represent (e.g. MAP2_VERTEX_3).</param>
		/// <param name="u1">Range of the variable 'u'.</param>
		/// <param name="u2">Range of the variable 'u.</param>
		/// <param name="ustride">Offset between beginning of one control point and the next.</param>
		/// <param name="uorder">The degree plus one.</param>
		/// <param name="v1">Range of the variable 'v'.</param>
		/// <param name="v2">Range of the variable 'v'.</param>
		/// <param name="vstride">Offset between beginning of one control point and the next.</param>
		/// <param name="vorder">The degree plus one.</param>
		/// <param name="points">The data for the points.</param>
		public void Map2(uint target, double u1, double u2, int ustride, int uorder, double v1, double v2, int vstride, int vorder,  double []points)
		{
			PreGLCall();
			glMap2d(target, u1, u2, ustride, uorder, v1, v2, vstride, vorder, points);
			PostGLCall();
		}

		/// <summary>
		/// Defines a 2D evaluator.
		/// </summary>
		/// <param name="target">What the control points represent (e.g. MAP2_VERTEX_3).</param>
		/// <param name="u1">Range of the variable 'u'.</param>
		/// <param name="u2">Range of the variable 'u.</param>
		/// <param name="ustride">Offset between beginning of one control point and the next.</param>
		/// <param name="uorder">The degree plus one.</param>
		/// <param name="v1">Range of the variable 'v'.</param>
		/// <param name="v2">Range of the variable 'v'.</param>
		/// <param name="vstride">Offset between beginning of one control point and the next.</param>
		/// <param name="vorder">The degree plus one.</param>
		/// <param name="points">The data for the points.</param>
		public void Map2(uint target, float u1, float u2, int ustride, int uorder, float v1, float v2, int vstride, int vorder,  float []points)
		{
			PreGLCall();
			glMap2f(target, u1, u2, ustride, uorder, v1, v2, vstride, vorder, points);
			PostGLCall();
		}

		/// <summary>
		/// This function defines a grid that goes from u1 to u1 in n steps, evenly spaced.
		/// </summary>
		/// <param name="un">Number of steps.</param>
		/// <param name="u1">Range of variable 'u'.</param>
		/// <param name="u2">Range of variable 'u'.</param>
		public void MapGrid1(int un, double u1, double u2)
		{
			PreGLCall();
			glMapGrid1d(un, u1, u2);
			PostGLCall();
		}

		/// <summary>
		/// This function defines a grid that goes from u1 to u1 in n steps, evenly spaced.
		/// </summary>
		/// <param name="un">Number of steps.</param>
		/// <param name="u1">Range of variable 'u'.</param>
		/// <param name="u2">Range of variable 'u'.</param>
		public void MapGrid1(int un, float u1, float u2)
		{
			PreGLCall();
			glMapGrid1d(un, u1, u2);
			PostGLCall();
		}

		/// <summary>
		/// This function defines a grid that goes from u1 to u1 in n steps, evenly spaced,
		/// and the same for v.
		/// </summary>
		/// <param name="un">Number of steps.</param>
		/// <param name="u1">Range of variable 'u'.</param>
		/// <param name="u2">Range of variable 'u'.</param>
		/// <param name="vn">Number of steps.</param>
		/// <param name="v1">Range of variable 'v'.</param>
		/// <param name="v2">Range of variable 'v'.</param>
		public void MapGrid2(int un, double u1, double u2, int vn, double v1, double v2)
		{
			PreGLCall();
			glMapGrid2d(un, u1, u2, vn, v1, v2);
			PostGLCall();
		}

		/// <summary>
		/// This function defines a grid that goes from u1 to u1 in n steps, evenly spaced,
		/// and the same for v.
		/// </summary>
		/// <param name="un">Number of steps.</param>
		/// <param name="u1">Range of variable 'u'.</param>
		/// <param name="u2">Range of variable 'u'.</param>
		/// <param name="vn">Number of steps.</param>
		/// <param name="v1">Range of variable 'v'.</param>
		/// <param name="v2">Range of variable 'v'.</param>
		public void MapGrid2(int un, float u1, float u2, int vn, float v1, float v2)
		{
			PreGLCall();
			glMapGrid2f(un, u1, u2, vn, v1, v2);
			PostGLCall();
		}

		/// <summary>
		/// This function sets a material parameter.
		/// </summary>
		/// <param name="face">What faces is this parameter for (i.e front/back etc).</param>
		/// <param name="pname">What parameter you want to set.</param>
		/// <param name="param">The value to set 'pname' to.</param>
		public void Material(uint face, uint pname, float param)
		{
			PreGLCall();
			glMaterialf(face, pname, param);
			PostGLCall();
		}

		/// <summary>
		/// This function sets a material parameter.
		/// </summary>
		/// <param name="face">What faces is this parameter for (i.e front/back etc).</param>
		/// <param name="pname">What parameter you want to set.</param>
		/// <param name="parameters">The value to set 'pname' to.</param>
		public void Material(uint face, uint pname,  float[] parameters)
		{
			PreGLCall();
			glMaterialfv(face, pname, parameters);
			PostGLCall();
		}

		/// <summary>
		/// This function sets a material parameter.
		/// </summary>
		/// <param name="face">What faces is this parameter for (i.e front/back etc).</param>
		/// <param name="pname">What parameter you want to set.</param>
		/// <param name="param">The value to set 'pname' to.</param>
		public void Material(uint face, uint pname, int param)
		{
			PreGLCall();
			glMateriali(face, pname, param);
			PostGLCall();
		}

		/// <summary>
		/// This function sets a material parameter.
		/// </summary>
		/// <param name="face">What faces is this parameter for (i.e front/back etc).</param>
		/// <param name="pname">What parameter you want to set.</param>
		/// <param name="parameters">The value to set 'pname' to.</param>
		public void Material(uint face, uint pname,  int[] parameters)
		{
			PreGLCall();
			glMaterialiv(face, pname, parameters);
			PostGLCall();
		}

		/// <summary>
		/// Set the current matrix mode (the matrix that matrix operations will be 
		/// performed on).
		/// </summary>
		/// <param name="mode">The mode, normally PROJECTION or MODELVIEW.</param>
		public void MatrixMode (uint mode)
		{
			PreGLCall();
			glMatrixMode(mode);
			PostGLCall();
		}

		/// <summary>
		/// Set the current matrix mode (the matrix that matrix operations will be 
		/// performed on).
		/// </summary>
		/// <param name="mode">The mode, normally PROJECTION or MODELVIEW.</param>
        public void MatrixMode(Enumerations.MatrixMode mode)
		{
			PreGLCall();
			glMatrixMode((uint)mode);
			PostGLCall();
		}

        /// <summary>
        /// Multiply the current matrix with the specified matrix.
        /// </summary>
        /// <param name="m">Points to 16 consecutive values that are used as the elements of a 4x4 column-major matrix.</param>
		public void MultMatrix( double []m)
        {
            PreGLCall();
            glMultMatrixd(m);
            PostGLCall();
        }

        /// <summary>
        /// Multiply the current matrix with the specified matrix.
        /// </summary>
        /// <param name="m">Points to 16 consecutive values that are used as the elements of a 4x4 column-major matrix.</param>
		public void MultMatrix( float []m)
        {
            PreGLCall();
            glMultMatrixf(m);
            PostGLCall();
        }

		/// <summary>
		/// This function starts compiling a new display list.
		/// </summary>
		/// <param name="list">The list to compile.</param>
		/// <param name="mode">Either COMPILE or COMPILE_AND_EXECUTE.</param>
		public void NewList(uint list, uint mode)
		{
			PreGLCall();
			glNewList(list, mode);
			PostGLCall();
		}

		/// <summary>
		/// This function creates a new glu NURBS renderer object.
		/// </summary>
		/// <returns>A Pointer to the NURBS renderer.</returns>
		public IntPtr NewNurbsRenderer()
		{
			PreGLCall();
			IntPtr nurbs = gluNewNurbsRenderer();
			PostGLCall();

			return nurbs;
		}

		/// <summary>
		/// This function creates a new OpenGL Quadric Object.
		/// </summary>
		/// <returns>The pointer to the Quadric Object.</returns>
		public IntPtr NewQuadric()
		{
			PreGLCall();
			IntPtr quad = gluNewQuadric();
			PostGLCall();

			return quad;
		}

        /// <summary>
        /// Set the current normal.
        /// </summary>
        /// <param name="nx">Normal Coordinate.</param>
        /// <param name="ny">Normal Coordinate.</param>
        /// <param name="nz">Normal Coordinate.</param>
		public void Normal(byte nx, byte ny, byte nz)
        {
            PreGLCall();
            glNormal3b(nx, ny, nz);
            PostGLCall();
        }

        /// <summary>
        /// This function sets the current normal.
        /// </summary>
        /// <param name="v">The normal.</param>
        public void Normal(byte[] v)
        {
            PreGLCall();
            glNormal3bv(v);
            PostGLCall();
        }

        /// <summary>
        /// Set the current normal.
        /// </summary>
        /// <param name="nx">Normal Coordinate.</param>
        /// <param name="ny">Normal Coordinate.</param>
        /// <param name="nz">Normal Coordinate.</param>
        public void Normal(double nx, double ny, double nz)
        {
            PreGLCall();
            glNormal3d(nx, ny, nz);
            PostGLCall();
        }

        /// <summary>
        /// This function sets the current normal.
        /// </summary>
        /// <param name="v">The normal.</param>
        public void Normal(double[] v)
        {
            PreGLCall();
            glNormal3dv(v);
            PostGLCall();
        }

        /// <summary>
        /// Set the current normal.
        /// </summary>
        /// <param name="nx">Normal Coordinate.</param>
        /// <param name="ny">Normal Coordinate.</param>
        /// <param name="nz">Normal Coordinate.</param>
        public void Normal(float nx, float ny, float nz)
        {
            PreGLCall();
            glNormal3f(nx, ny, nz);
            PostGLCall();
        }

		/// <summary>
		/// This function sets the current normal.
		/// </summary>
		/// <param name="v">The normal.</param>
		public void Normal(float[] v)
		{
			PreGLCall();
			glNormal3fv(v);
			PostGLCall();
        }

        /// <summary>
        /// Set the current normal.
        /// </summary>
        /// <param name="nx">Normal Coordinate.</param>
        /// <param name="ny">Normal Coordinate.</param>
        /// <param name="nz">Normal Coordinate.</param>
        public void Normal3i(int nx, int ny, int nz)
        {
            PreGLCall();
            glNormal3i(nx, ny, nz);
            PostGLCall();
        }

        /// <summary>
        /// This function sets the current normal.
        /// </summary>
        /// <param name="v">The normal.</param>
        public void Normal(int[] v)
        {
            PreGLCall();
            glNormal3iv(v);
            PostGLCall();
        }

        /// <summary>
        /// Set the current normal.
        /// </summary>
        /// <param name="nx">Normal Coordinate.</param>
        /// <param name="ny">Normal Coordinate.</param>
        /// <param name="nz">Normal Coordinate.</param>
        public void Normal(short nx, short ny, short nz)
        {
            PreGLCall();
            glNormal3s(nx, ny, nz);
            PostGLCall();
        }

        /// <summary>
        /// This function sets the current normal.
        /// </summary>
        /// <param name="v">The normal.</param>
        public void Normal(short[] v)
        {
            PreGLCall();
            glNormal3sv(v);
            PostGLCall();
        }

        /// <summary>
        /// Set's the pointer to the normal array.
        /// </summary>
        /// <param name="type">The type of data.</param>
        /// <param name="stride">The space in bytes between each normal.</param>
        /// <param name="pointer">The normals.</param>
        public void NormalPointer(uint type, int stride, IntPtr pointer)
        {
            PreGLCall();
            glNormalPointer(type, stride, pointer);
            PostGLCall();
        }

		/// <summary>
		/// Set's the pointer to the normal array.
		/// </summary>
		/// <param name="type">The type of data.</param>
		/// <param name="stride">The space in bytes between each normal.</param>
		/// <param name="pointer">The normals.</param>
		public void NormalPointer(uint type, int stride, float[] pointer)
		{
			PreGLCall();
			glNormalPointer(type, stride, pointer);
			PostGLCall();
		}

		/// <summary>
		/// This function defines a NURBS Curve.
		/// </summary>
		/// <param name="nurbsObject">The NURBS object.</param>
		/// <param name="knotsCount">The number of knots.</param>
		/// <param name="knots">The knots themselves.</param>
		/// <param name="stride">The stride, i.e. distance between vertices in the 
		/// control points array.</param>
		/// <param name="controlPointsArray">The array of control points.</param>
		/// <param name="order">The order of the polynomial.</param>
		/// <param name="type">The type of data to generate.</param>
		public void NurbsCurve(IntPtr nurbsObject, int knotsCount, float[] knots, 
			int stride, float[] controlPointsArray, int order, uint type)
		{
			PreGLCall();
			gluNurbsCurve(nurbsObject, knotsCount, knots, stride, controlPointsArray,
				order, type);
			PostGLCall();
		}

		/// <summary>
		/// This function sets a NURBS property.
		/// </summary>
		/// <param name="nurbsObject">The object to set the property for.</param>
		/// <param name="property">The property to set.</param>
		/// <param name="value">The new value of the property.</param>
		public void NurbsProperty(IntPtr nurbsObject, int property, float value)
		{
			PreGLCall();
			gluNurbsProperty(nurbsObject, property, value);
			PostGLCall();
		}

    /// <summary>
    /// This function defines a NURBS surface.
    /// </summary>
    /// <param name="nurbsObject">The NURBS object.</param>
    /// <param name="sknotsCount">The sknots count.</param>
    /// <param name="sknots">The s-knots.</param>
    /// <param name="tknotsCount">The number of t-knots.</param>
    /// <param name="tknots">The t-knots.</param>
    /// <param name="sStride">The distance between s vertices.</param>
    /// <param name="tStride">The distance between t vertices.</param>
    /// <param name="controlPointsArray">The control points.</param>
    /// <param name="sOrder">The order of the s polynomial.</param>
    /// <param name="tOrder">The order of the t polynomial.</param>
    /// <param name="type">The type of data to generate.</param>
		public void NurbsSurface(IntPtr nurbsObject, int sknotsCount, float[] sknots, 
			int tknotsCount, float[] tknots, int sStride, int tStride, 
			float[] controlPointsArray, int sOrder, int tOrder, uint type)
		{
			PreGLCall();
			gluNurbsSurface(nurbsObject, sknotsCount, sknots, tknotsCount, tknots,
				sStride, tStride, controlPointsArray, sOrder, tOrder, type);
			PostGLCall();
		}
		
		/// <summary>
		/// This function creates an orthographic projection matrix (i.e one with no 
		/// perspective) and multiplies it to the current matrix stack, which would
		/// normally be 'PROJECTION'.
		/// </summary>
		/// <param name="left">Left clipping plane.</param>
		/// <param name="right">Right clipping plane.</param>
		/// <param name="bottom">Bottom clipping plane.</param>
		/// <param name="top">Top clipping plane.</param>
		/// <param name="zNear">Near clipping plane.</param>
		/// <param name="zFar">Far clipping plane.</param>
		public void Ortho(double left, double right, double bottom, 
			double top, double zNear, double zFar)
		{
			PreGLCall();
			glOrtho(left, right, bottom, top, zNear, zFar);
			PostGLCall();
		}
		/// <summary>
		/// This function creates an orthographic project based on a screen size.
		/// </summary>
		/// <param name="left">Left of the screen. (Normally 0).</param>
		/// <param name="right">Right of the screen.(Normally width).</param>
		/// <param name="bottom">Bottom of the screen (normally 0).</param>
		/// <param name="top">Top of the screen (normally height).</param>
		public void Ortho2D(double left, double right, double bottom, double top)
		{
			PreGLCall();
			gluOrtho2D(left, right, bottom, top);
			PostGLCall();
		}

		/// <summary>
		/// This function draws a partial disk from the quadric object.
		/// </summary>
		/// <param name="qobj">The Quadric objec.t</param>
		/// <param name="innerRadius">Radius of the inside of the disk.</param>
		/// <param name="outerRadius">Radius of the outside of the disk.</param>
		/// <param name="slices">The slices.</param>
		/// <param name="loops">The loops.</param>
		/// <param name="startAngle">Starting angle.</param>
		/// <param name="sweepAngle">Sweep angle.</param>
		public void PartialDisk(IntPtr qobj,double innerRadius,double outerRadius, int slices, int loops, double startAngle, double sweepAngle)
		{
			PreGLCall();
			gluPartialDisk(qobj, innerRadius, outerRadius, slices, loops, startAngle, sweepAngle);
			PostGLCall();
		}

        /// <summary>
        /// Place a marker in the feedback buffer.
        /// </summary>
        /// <param name="token">Specifies a marker value to be placed in the feedback buffer following a OpenGL.PASS_THROUGH_TOKEN.</param>
		public void PassThrough (float token)
        {
            PreGLCall();
            glPassThrough(token);
            PostGLCall();
        }

		/// <summary>
		/// This function creates a perspective matrix and multiplies it to the current
		/// matrix stack (which in most cases should be 'PROJECTION').
		/// </summary>
		/// <param name="fovy">Field of view angle (human eye = 60 Degrees).</param>
		/// <param name="aspect">Apsect Ratio (width of screen divided by height of screen).</param>
		/// <param name="zNear">Near clipping plane (normally 1).</param>
		/// <param name="zFar">Far clipping plane.</param>
		public void Perspective(double fovy, double aspect, double zNear, double zFar)
		{
			PreGLCall();
			gluPerspective(fovy, aspect, zNear, zFar);
			PostGLCall();
		}

		/// <summary>
		/// This function creates a 'pick matrix' normally used for selecting objects that
		/// are at a certain point on the screen.
		/// </summary>
		/// <param name="x">X Point.</param>
		/// <param name="y">Y Point.</param>
		/// <param name="width">Width of point to test (4 is normal).</param>
		/// <param name="height">Height of point to test (4 is normal).</param>
		/// <param name="viewport">The current viewport.</param>
		public void PickMatrix(double x, double y, double width, double height, int[] viewport)
		{
			PreGLCall();
			gluPickMatrix(x, y, width, height, viewport);
			PostGLCall();
		}

        /// <summary>
        /// Set up pixel transfer maps.
        /// </summary>
        /// <param name="map">Specifies a symbolic	map name.</param>
        /// <param name="mapsize">Specifies the size of the map being defined.</param>
        /// <param name="values">Specifies an	array of mapsize values.</param>
		public void PixelMap(uint map, int mapsize,  float[] values)
        {
            PreGLCall();
            glPixelMapfv(map, mapsize, values);
            PostGLCall();
        }

        /// <summary>
        /// Set up pixel transfer maps.
        /// </summary>
        /// <param name="map">Specifies a symbolic	map name.</param>
        /// <param name="mapsize">Specifies the size of the map being defined.</param>
        /// <param name="values">Specifies an	array of mapsize values.</param>
        public void PixelMap(uint map, int mapsize, uint[] values)
        {
            PreGLCall();
            glPixelMapuiv(map, mapsize, values);
            PostGLCall();
        }

        /// <summary>
        /// Set up pixel transfer maps.
        /// </summary>
        /// <param name="map">Specifies a symbolic	map name.</param>
        /// <param name="mapsize">Specifies the size of the map being defined.</param>
        /// <param name="values">Specifies an	array of mapsize values.</param>
        public void PixelMap(uint map, int mapsize, ushort[] values)
        {
            PreGLCall();
            glPixelMapusv(map, mapsize, values);
            PostGLCall();
        }

        /// <summary>
        /// Set pixel storage modes.
        /// </summary>
        /// <param name="pname">Specifies the symbolic	name of	the parameter to be set.</param>
        /// <param name="param">Specifies the value that pname	is set to.</param>
		public void PixelStore(uint pname, float param)
        {
            PreGLCall();
            glPixelStoref(pname, param);
            PostGLCall();
        }

        /// <summary>
        /// Set pixel storage modes.
        /// </summary>
        /// <param name="pname">Specifies the symbolic	name of	the parameter to be set.</param>
        /// <param name="param">Specifies the value that pname	is set to.</param>
        public void PixelStore(uint pname, int param)
        {
            PreGLCall();
            glPixelStorei(pname, param);
            PostGLCall();
        }

        /// <summary>
        /// Set pixel transfer modes.
        /// </summary>
        /// <param name="pname">Specifies the symbolic name of the pixel transfer parameter to be set.</param>
        /// <param name="param">Specifies the value that pname is set to.</param>
        public void PixelTransfer(uint pname, bool param)
        {
            PreGLCall();
            int p = param ? 1 : 0;
            glPixelTransferi(pname, p);
            PostGLCall();
        }

        /// <summary>
        /// Set pixel transfer modes.
        /// </summary>
        /// <param name="pname">Specifies the symbolic name of the pixel transfer parameter to be set.</param>
        /// <param name="param">Specifies the value that pname is set to.</param>
        public void PixelTransfer(Enumerations.PixelTransferParameterName pname, bool param)
        {
            PreGLCall();
            int p = param ? 1 : 0;
            glPixelTransferi((uint)pname, p);
            PostGLCall();
        }

        /// <summary>
        /// Set pixel transfer modes.
        /// </summary>
        /// <param name="pname">Specifies the symbolic name of the pixel transfer parameter to be set.</param>
        /// <param name="param">Specifies the value that pname is set to.</param>
		public void PixelTransfer(uint pname, float param)
        {
            PreGLCall();
            glPixelTransferf(pname, param);
            PostGLCall();
        }

        /// <summary>
        /// Set pixel transfer modes.
        /// </summary>
        /// <param name="pname">Specifies the symbolic name of the pixel transfer parameter to be set.</param>
        /// <param name="param">Specifies the value that pname is set to.</param>
        public void PixelTransfer(Enumerations.PixelTransferParameterName pname, float param)
        {
            PreGLCall();
            glPixelTransferf((uint)pname, param);
            PostGLCall();
        }

        /// <summary>
        /// Set pixel transfer modes.
        /// </summary>
        /// <param name="pname">Specifies the symbolic name of the pixel transfer parameter to be set.</param>
        /// <param name="param">Specifies the value that pname is set to.</param>
        public void PixelTransfer(uint pname, int param)
        {
            PreGLCall();
            glPixelTransferi(pname, param);
            PostGLCall();
        }

        /// <summary>
        /// Set pixel transfer modes.
        /// </summary>
        /// <param name="pname">Specifies the symbolic name of the pixel transfer parameter to be set.</param>
        /// <param name="param">Specifies the value that pname is set to.</param>
        public void PixelTransfer(Enumerations.PixelTransferParameterName pname, int param)
        {
            PreGLCall();
            glPixelTransferi((uint)pname, param);
            PostGLCall();
        }

        /// <summary>
        /// Specify	the pixel zoom factors.
        /// </summary>
        /// <param name="xfactor">Specify the x and y zoom factors for pixel write operations.</param>
        /// <param name="yfactor">Specify the x and y zoom factors for pixel write operations.</param>
		public void PixelZoom (float xfactor, float yfactor)
        {
            PreGLCall();
            glPixelZoom(xfactor, yfactor);
            PostGLCall();
        }

		/// <summary>
		/// The size of points to be rasterised.
		/// </summary>
		/// <param name="size">Size in pixels.</param>
		public void PointSize(float size)
		{
			PreGLCall();
			glPointSize(size);
			PostGLCall();
		}

		/// <summary>
		/// This sets the current drawing mode of polygons (points, lines, filled).
		/// </summary>
		/// <param name="face">The faces this applies to (front, back or both).</param>
		/// <param name="mode">The mode to set to (points, lines, or filled).</param>
		public void PolygonMode(uint face, uint mode)
		{
			PreGLCall();
			glPolygonMode(face, mode);
			PostGLCall();
		}

        /// <summary>
        /// This sets the current drawing mode of polygons (points, lines, filled).
        /// </summary>
        /// <param name="face">The faces this applies to (front, back or both).</param>
        /// <param name="mode">The mode to set to (points, lines, or filled).</param>
        public void PolygonMode(Enumerations.FaceMode face, Enumerations.PolygonMode mode)
        {
            PreGLCall();
            glPolygonMode((uint)face, (uint)mode);
            PostGLCall();
        }

        /// <summary>
        /// Set	the scale and units used to calculate depth	values.
        /// </summary>
        /// <param name="factor">Specifies a scale factor that	is used	to create a variable depth offset for each polygon. The initial value is 0.</param>
        /// <param name="units">Is multiplied by an implementation-specific value to create a constant depth offset. The initial value is 0.</param>
		public void PolygonOffset (float factor, float units)
        {
            PreGLCall();
            glPolygonOffset(factor, units);
            PostGLCall();
        }

        /// <summary>
        /// Set the polygon stippling pattern.
        /// </summary>
        /// <param name="mask">Specifies a pointer to a 32x32 stipple pattern that will be unpacked from memory in the same way that glDrawPixels unpacks pixels.</param>
		public void PolygonStipple ( byte []mask)
        {
            PreGLCall();
            glPolygonStipple(mask);
            PostGLCall();
        }

		/// <summary>
		/// This function restores the attribute stack to the state it was when
		/// PushAttrib was called.
		/// </summary>
		public void PopAttrib()
		{
			PreGLCall();
			glPopAttrib();
			PostGLCall();
		}

        /// <summary>
        /// Pop the client attribute stack.
        /// </summary>
		public void PopClientAttrib ()
        {
            PreGLCall();
            glPopClientAttrib();
            PostGLCall();
        }

		/// <summary>
		/// Restore the previously saved state of the current matrix stack.
		/// </summary>
		public void PopMatrix()
		{            
            PreGLCall();
			glPopMatrix();
			PostGLCall();
		}

		/// <summary>
		/// This takes the top name off the selection names stack.
		/// </summary>
		public void PopName()
		{
			PreGLCall();
			glPopName();
			PostGLCall();
		}

        /// <summary>
        /// Set texture residence priority.
        /// </summary>
        /// <param name="n">Specifies the number of textures to be prioritized.</param>
        /// <param name="textures">Specifies an array containing the names of the textures to be prioritized.</param>
        /// <param name="priorities">Specifies	an array containing the	texture priorities. A priority given in an element of priorities applies to the	texture	named by the corresponding element of textures.</param>
		public void PrioritizeTextures (int n,  uint []textures,  float []priorities)
        {
            PreGLCall();
            glPrioritizeTextures(n, textures, priorities);
            PostGLCall();
        }

		/// <summary>
		/// This function Maps the specified object coordinates into window coordinates.
		/// </summary>
		/// <param name="objx">The object's x coord.</param>
		/// <param name="objy">The object's y coord.</param>
		/// <param name="objz">The object's z coord.</param>
		/// <param name="modelMatrix">The modelview matrix.</param>
		/// <param name="projMatrix">The projection matrix.</param>
		/// <param name="viewport">The viewport.</param>
		/// <param name="winx">The window x coord.</param>
		/// <param name="winy">The Window y coord.</param>
		/// <param name="winz">The Window z coord.</param>
		public void Project(double objx, double objy, double objz, double[] modelMatrix, double[] projMatrix, int[] viewport, double[] winx, double[] winy, double[] winz)
		{
			PreGLCall();
			gluProject(objx, objy, objz, modelMatrix, projMatrix, viewport, winx, winy, winz);
			PostGLCall();
		}		

		/// <summary>
		/// Save the current state of the attribute groups specified by 'mask'.
		/// </summary>
		/// <param name="mask">The attibute groups to save.</param>
		public void PushAttrib(uint mask)
		{
			PreGLCall();
			glPushAttrib(mask);
			PostGLCall();
		}

        /// <summary>
        /// Save the current state of the attribute groups specified by 'mask'.
        /// </summary>
        /// <param name="mask">The attibute groups to save.</param>
        public void PushAttrib(Enumerations.AttributeMask mask)
        {
            PreGLCall();
            glPushAttrib((uint)mask);
            PostGLCall();
        }

        /// <summary>
        /// Push the client attribute stack.
        /// </summary>
        /// <param name="mask">Specifies a mask that indicates	which attributes to save.</param>
		public void PushClientAttrib (uint mask)
        {
            PreGLCall();
            glPushClientAttrib(mask);
            PostGLCall();
        }

		/// <summary>
		/// Save the current state of the current matrix stack.
		/// </summary>
		public void PushMatrix()
		{
			PreGLCall();
			glPushMatrix();
			PostGLCall();
		}

		/// <summary>
		/// This function adds a new name to the selection buffer.
		/// </summary>
		/// <param name="name">The name to add.</param>
		public void PushName(uint name)
		{
			PreGLCall();
			glPushName(name);
			PostGLCall();
		}

		/// <summary>
		/// This set's the Generate Normals propery of the specified Quadric object.
		/// </summary>
		/// <param name="quadricObject">The quadric object.</param>
		/// <param name="normals">The type of normals to generate.</param>
		public void QuadricNormals(IntPtr quadricObject, uint normals)
		{
			PreGLCall();
			gluQuadricNormals(quadricObject, normals);
			PostGLCall();
		}

		/// <summary>
		/// This function sets the type of texture coordinates being generated by
		/// the specified quadric object.
		/// </summary>
		/// <param name="quadricObject">The quadric object.</param>
		/// <param name="textureCoords">The type of coordinates to generate.</param>
		public void QuadricTexture(IntPtr quadricObject, int textureCoords)
		{
			PreGLCall();
			gluQuadricTexture(quadricObject, textureCoords);
			PostGLCall();
		}

		/// <summary>
		/// This sets the orientation for the quadric object.
		/// </summary>
		/// <param name="quadricObject">The quadric object.</param>
		/// <param name="orientation">The orientation.</param>
		public void QuadricOrientation(IntPtr quadricObject, int orientation)
		{
			PreGLCall();
			gluQuadricOrientation(quadricObject, orientation);
			PostGLCall();
		}

		/// <summary>
		/// This sets the current drawstyle for the Quadric Object.
		/// </summary>
		/// <param name="quadObject">The quadric object.</param>
		/// <param name="drawStyle">The draw style.</param>
		public void QuadricDrawStyle (IntPtr quadObject, uint drawStyle)
		{
			PreGLCall();
			gluQuadricDrawStyle(quadObject, drawStyle);
			PostGLCall();
		}

        /// <summary>
        /// This function sets the current raster position.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        public void RasterPos(double x, double y)
        {
            PreGLCall();
            glRasterPos2d(x, y);
            PostGLCall();
        }

        /// <summary>
        /// This function sets the current raster position.
        /// </summary>
        /// <param name="v">The coordinate.</param>
        public void RasterPos(double[] v) 
        {
            PreGLCall();
            if (v.Length == 2)
                glRasterPos2dv(v);
            else if (v.Length == 3)
                glRasterPos3dv(v);
            else
                glRasterPos4dv(v);
            PostGLCall();
        }

        /// <summary>
        /// This function sets the current raster position.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        public void RasterPos(float x, float y)
        {
            PreGLCall();
            glRasterPos2f(x, y);
            PostGLCall();
        }

        /// <summary>
        /// This function sets the current raster position.
        /// </summary>
        /// <param name="v">The coordinate.</param>
        public void RasterPos(float[] v)
        {
            PreGLCall();
            if (v.Length == 2)
                glRasterPos2fv(v);
            else if (v.Length == 3)
                glRasterPos3fv(v);
            else
                glRasterPos4fv(v);
            PostGLCall();
        }

		/// <summary>
		/// This function sets the current raster position.
		/// </summary>
		/// <param name="x">X coordinate.</param>
		/// <param name="y">Y coordinate.</param>
		public void RasterPos(int x, int y)
		{
			PreGLCall();
			glRasterPos2i(x, y);
			PostGLCall();
        }

        /// <summary>
        /// This function sets the current raster position.
        /// </summary>
        /// <param name="v">The coordinate.</param>
        public void RasterPos(int[] v)
        {
            PreGLCall();
            if (v.Length == 2)
                glRasterPos2iv(v);
            else if (v.Length == 3)
                glRasterPos3iv(v);
            else
                glRasterPos4iv(v);
            PostGLCall();
        }

        /// <summary>
        /// This function sets the current raster position.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        public void RasterPos(short x, short y)
        {
            PreGLCall();
            glRasterPos2s(x, y);
            PostGLCall();
        }

        /// <summary>
        /// This function sets the current raster position.
        /// </summary>
        /// <param name="v">The coordinate.</param>
        public void RasterPos(short[] v)
        {
            PreGLCall();
            if (v.Length == 2)
                glRasterPos2sv(v);
            else if (v.Length == 3)
                glRasterPos3sv(v);
            else
                glRasterPos4sv(v);
            PostGLCall();
        }

        /// <summary>
        /// This function sets the current raster position.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        /// <param name="z">Z coordinate.</param>
        public void RasterPos(double x, double y, double z)
        {
            PreGLCall();
            glRasterPos3d(x, y, z);
            PostGLCall();
        }

        /// <summary>
        /// This function sets the current raster position.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        /// <param name="z">Z coordinate.</param>
        public void RasterPos(float x, float y, float z)
        {
            PreGLCall();
            glRasterPos3f(x, y, z);
            PostGLCall();
        }

        /// <summary>
        /// This function sets the current raster position.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        /// <param name="z">Z coordinate.</param>
        public void RasterPos(int x, int y, int z)
        {
            PreGLCall();
            glRasterPos3i(x, y, z);
            PostGLCall();
        }

        /// <summary>
        /// This function sets the current raster position.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        /// <param name="z">Z coordinate.</param>
        public void RasterPos(short x, short y, short z)
        {
            PreGLCall();
            glRasterPos3s(x, y, z);
            PostGLCall();
        }

        /// <summary>
        /// This function sets the current raster position.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        /// <param name="z">Z coordinate.</param>
        /// <param name="w">W coordinate.</param>
        public void RasterPos(double x, double y, double z, double w)
        {
            PreGLCall();
            glRasterPos4d(x, y, z, w);
            PostGLCall();
        }

        /// <summary>
        /// This function sets the current raster position.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        /// <param name="z">Z coordinate.</param>
        /// <param name="w">W coordinate.</param>
        public void RasterPos(float x, float y, float z, float w)
        {
            PreGLCall();
            glRasterPos4f(x, y, z, w);
            PostGLCall();
        }

        /// <summary>
        /// This function sets the current raster position.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        /// <param name="z">Z coordinate.</param>
        /// <param name="w">W coordinate.</param>
        public void RasterPos(int x, int y, int z, int w)
        {
            PreGLCall();
            glRasterPos4i(x, y, z, w);
            PostGLCall();
        }

        /// <summary>
        /// This function sets the current raster position.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        /// <param name="z">Z coordinate.</param>
        /// <param name="w">W coordinate.</param>
        public void RasterPos(short x, short y, short z, short w)
        {
            PreGLCall();
            glRasterPos4s(x, y, z, w);
            PostGLCall();
        }

        /// <summary>
        /// Select	a color	buffer source for pixels.
        /// </summary>
        /// <param name="mode">Specifies a color buffer.  Accepted values are OpenGL.FRONT_LEFT, OpenGL.FRONT_RIGHT, OpenGL.BACK_LEFT, OpenGL.BACK_RIGHT, OpenGL.FRONT, OpenGL.BACK, OpenGL.LEFT, OpenGL.GL_RIGHT, and OpenGL.AUXi, where i is between 0 and OpenGL.AUX_BUFFERS - 1.</param>
		public void ReadBuffer(uint mode)
        {
            PreGLCall();
            glReadBuffer(mode);
            PostGLCall();
        }

        /// <summary>
        /// Reads a block of pixels from the frame buffer.
        /// </summary>
        /// <param name="x">Top-Left X value.</param>
        /// <param name="y">Top-Left Y value.</param>
        /// <param name="width">Width of block to read.</param>
        /// <param name="height">Height of block to read.</param>
        /// <param name="format">Specifies the format of the pixel data. The following symbolic values are accepted: OpenGL.COLOR_INDEX, OpenGL.STENCIL_INDEX, OpenGL.DEPTH_COMPONENT, OpenGL.RED, OpenGL.GREEN, OpenGL.BLUE, OpenGL.ALPHA, OpenGL.RGB, OpenGL.RGBA, OpenGL.LUMINANCE and OpenGL.LUMINANCE_ALPHA.</param>
        /// <param name="type">Specifies the data type of the pixel data.Must be one of OpenGL.UNSIGNED_BYTE, OpenGL.BYTE, OpenGL.BITMAP, OpenGL.UNSIGNED_SHORT, OpenGL.SHORT, OpenGL.UNSIGNED_INT, OpenGL.INT or OpenGL.FLOAT.</param>
        /// <param name="pixels">Storage for the pixel data received.</param>
		public void ReadPixels(int x, int y, int width, int height, uint format, 
            uint type, byte[] pixels)
        {
            PreGLCall();
            glReadPixels(x, y, width, height, format, type, pixels);
            PostGLCall();
        }

        /// <summary>
        /// Reads a block of pixels from the frame buffer.
        /// </summary>
        /// <param name="x">Top-Left X value.</param>
        /// <param name="y">Top-Left Y value.</param>
        /// <param name="width">Width of block to read.</param>
        /// <param name="height">Height of block to read.</param>
        /// <param name="format">Specifies the format of the pixel data. The following symbolic values are accepted: OpenGL.COLOR_INDEX, OpenGL.STENCIL_INDEX, OpenGL.DEPTH_COMPONENT, OpenGL.RED, OpenGL.GREEN, OpenGL.BLUE, OpenGL.ALPHA, OpenGL.RGB, OpenGL.RGBA, OpenGL.LUMINANCE and OpenGL.LUMINANCE_ALPHA.</param>
        /// <param name="type">Specifies the data type of the pixel data.Must be one of OpenGL.UNSIGNED_BYTE, OpenGL.BYTE, OpenGL.BITMAP, OpenGL.UNSIGNED_SHORT, OpenGL.SHORT, OpenGL.UNSIGNED_INT, OpenGL.INT or OpenGL.FLOAT.</param>
        /// <param name="pixels">Storage for the pixel data received.</param>
        public void ReadPixels(int x, int y, int width, int height, uint format,
            uint type, IntPtr pixels)
        {
            PreGLCall();
            glReadPixels(x, y, width, height, format, type, pixels);
            PostGLCall();
        }

		/// <summary>
		/// Draw a rectangle from two coordinates (top-left and bottom-right).
		/// </summary>
		/// <param name="x1">Top-Left X value.</param>
		/// <param name="y1">Top-Left Y value.</param>
		/// <param name="x2">Bottom-Right X Value.</param>
		/// <param name="y2">Bottom-Right Y Value.</param>
		public void Rect(double x1, double y1, double x2, double y2)
		{
			PreGLCall();
			glRectd(x1, y1, x2, y2);
			PostGLCall();
		}

		/// <summary>
		/// Draw a rectangle from two coordinates, expressed as arrays, e.g
		/// Rect(new float[] {0, 0}, new float[] {10, 10});
		/// </summary>
		/// <param name="v1">Top-Left point.</param>
		/// <param name="v2">Bottom-Right point.</param>
		public void Rect( double []v1,  double []v2)
		{
			PreGLCall();
			glRectdv(v1, v2);
			PostGLCall();
		}

		/// <summary>
		/// Draw a rectangle from two coordinates (top-left and bottom-right).
		/// </summary>
		/// <param name="x1">Top-Left X value.</param>
		/// <param name="y1">Top-Left Y value.</param>
		/// <param name="x2">Bottom-Right X Value.</param>
		/// <param name="y2">Bottom-Right Y Value.</param>
		public void Rect(float x1, float y1, float x2, float y2)
		{
			PreGLCall();
			glRectd(x1, y1, x2, y2);
			PostGLCall();
		}

		/// <summary>
		/// Draw a rectangle from two coordinates, expressed as arrays, e.g
		/// Rect(new float[] {0, 0}, new float[] {10, 10});
		/// </summary>
		/// <param name="v1">Top-Left point.</param>
		/// <param name="v2">Bottom-Right point.</param>
		public void Rect(float []v1,  float []v2)
		{
			PreGLCall();
			glRectfv(v1, v2);
			PostGLCall();
		}

		/// <summary>
		/// Draw a rectangle from two coordinates (top-left and bottom-right).
		/// </summary>
		/// <param name="x1">Top-Left X value.</param>
		/// <param name="y1">Top-Left Y value.</param>
		/// <param name="x2">Bottom-Right X Value.</param>
		/// <param name="y2">Bottom-Right Y Value.</param>
		public void Rect(int x1, int y1, int x2, int y2)
		{
			PreGLCall();
			glRecti(x1, y1, x2, y2);
			PostGLCall();
		}

		/// <summary>
		/// Draw a rectangle from two coordinates, expressed as arrays, e.g
		/// Rect(new float[] {0, 0}, new float[] {10, 10});
		/// </summary>
		/// <param name="v1">Top-Left point.</param>
		/// <param name="v2">Bottom-Right point.</param>
		public void Rect( int []v1,  int []v2)
		{
			PreGLCall();
			glRectiv(v1, v2);
			PostGLCall();
		}

		/// <summary>
		/// Draw a rectangle from two coordinates (top-left and bottom-right).
		/// </summary>
		/// <param name="x1">Top-Left X value.</param>
		/// <param name="y1">Top-Left Y value.</param>
		/// <param name="x2">Bottom-Right X Value.</param>
		/// <param name="y2">Bottom-Right Y Value.</param>
		public void Rect(short x1, short y1, short x2, short y2)
		{
			PreGLCall();
			glRects(x1, y1, x2, y2);
			PostGLCall();
		}

		/// <summary>
		/// Draw a rectangle from two coordinates, expressed as arrays, e.g
		/// Rect(new float[] {0, 0}, new float[] {10, 10});
		/// </summary>
		/// <param name="v1">Top-Left point.</param>
		/// <param name="v2">Bottom-Right point.</param>
		public void Rect(short []v1, short []v2)
		{
			PreGLCall();
			glRectsv(v1, v2);
			PostGLCall();
		}

		/// <summary>
		/// This function sets the current render mode (render, feedback or select).
		/// </summary>
		/// <param name="mode">The Render mode (RENDER, SELECT or FEEDBACK).</param>
		/// <returns>The hits that selection or feedback caused..</returns>
		public int RenderMode(uint mode)
		{
			PreGLCall();
			int hits = glRenderMode(mode);
			PostGLCall();
			return hits;
		}

        /// <summary>
        /// This function sets the current render mode (render, feedback or select).
        /// </summary>
        /// <param name="mode">The Render mode (RENDER, SELECT or FEEDBACK).</param>
        /// <returns>The hits that selection or feedback caused..</returns>
        public int RenderMode(Enumerations.RenderingMode mode)
        {
            PreGLCall();
            int hits = glRenderMode((uint)mode);
            PostGLCall();
            return hits;
        }

		/// <summary>
		/// This function applies a rotation transformation to the current matrix.
		/// </summary>
		/// <param name="angle">The angle to rotate.</param>
		/// <param name="x">Amount along x.</param>
		/// <param name="y">Amount along y.</param>
		/// <param name="z">Amount along z.</param>
		public void Rotate(double angle, double x, double y, double z)
		{
			PreGLCall();
			glRotated(angle, x, y, z);
			PostGLCall();
		}

		/// <summary>
		/// This function applies a rotation transformation to the current matrix.
		/// </summary>
		/// <param name="angle">The angle to rotate.</param>
		/// <param name="x">Amount along x.</param>
		/// <param name="y">Amount along y.</param>
		/// <param name="z">Amount along z.</param>
		public void Rotate(float angle, float x, float y, float z)
		{
			PreGLCall();
			glRotatef(angle, x, y, z);
			PostGLCall();
		}

		/// <summary>
		/// This function quickly does three rotations, one about each axis, with the
		/// given angles (it's not an OpenGL function, but very useful).
		/// </summary>
		/// <param name="anglex">The angle to rotate about x.</param>
		/// <param name="angley">The angle to rotate about y.</param>
		/// <param name="anglez">The angle to rotate about z.</param>
		public void Rotate(float anglex, float angley, float anglez)
		{
			PreGLCall();
			glRotatef(anglex, 1, 0, 0);
			glRotatef(angley, 0, 1, 0);
			glRotatef(anglez, 0, 0, 1);
			PostGLCall();
		}

		/// <summary>
		/// This function applies a scale transformation to the current matrix.
		/// </summary>
		/// <param name="x">The amount to scale along x.</param>
		/// <param name="y">The amount to scale along y.</param>
		/// <param name="z">The amount to scale along z.</param>
		public void Scale(double x, double y, double z)
		{
			PreGLCall();
			glScaled(x, y, z);
			PostGLCall();
		}

		/// <summary>
		/// This function applies a scale transformation to the current matrix.
		/// </summary>
		/// <param name="x">The amount to scale along x.</param>
		/// <param name="y">The amount to scale along y.</param>
		/// <param name="z">The amount to scale along z.</param>
		public void Scale(float x, float y, float z)
		{
			PreGLCall();
			glScalef(x, y, z);
			PostGLCall();
		}

        /// <summary>
        /// Define the scissor box.
        /// </summary>
        /// <param name="x">Specify the lower left corner of the scissor box. Initially (0, 0).</param>
        /// <param name="y">Specify the lower left corner of the scissor box. Initially (0, 0).</param>
        /// <param name="width">Specify the width and height of the scissor box. When a GL context is first attached to a window, width and height are set to the dimensions of that window.</param>
        /// <param name="height">Specify the width and height of the scissor box. When a GL context is first attached to a window, width and height are set to the dimensions of that window.</param>
		public void Scissor (int x, int y, int width, int height)
        {
            PreGLCall();
            glScissor(x, y, width, height);
            PostGLCall();
        }

		/// <summary>
		/// This function sets the current select buffer.
		/// </summary>
		/// <param name="size">The size of the buffer you are passing.</param>
		/// <param name="buffer">The buffer itself.</param>
		public void SelectBuffer(int size, uint[] buffer)
		{
			PreGLCall();
			glSelectBuffer(size, buffer);
			PostGLCall();
		}

        /// <summary>
        /// Select flat or smooth shading.
        /// </summary>
        /// <param name="mode">Specifies a symbolic value representing a shading technique. Accepted values are OpenGL.FLAT and OpenGL.SMOOTH. The default is OpenGL.SMOOTH.</param>
		public void ShadeModel (uint mode)
        {
            PreGLCall();
            glShadeModel(mode);
            PostGLCall();
        }

        /// <summary>
        /// Select flat or smooth shading.
        /// </summary>
        /// <param name="mode">Specifies a symbolic value representing a shading technique. Accepted values are OpenGL.FLAT and OpenGL.SMOOTH. The default is OpenGL.SMOOTH.</param>
        public void ShadeModel(Enumerations.ShadeModel mode)
        {
            PreGLCall();
            glShadeModel((uint)mode);
            PostGLCall();
        }

		/// <summary>
		/// This function draws a sphere from a Quadric Object.
		/// </summary>
		/// <param name="qobj">The quadric object.</param>
		/// <param name="radius">Sphere radius.</param>
		/// <param name="slices">Slices of the sphere.</param>
		/// <param name="stacks">Stakcs of the sphere.</param>
		public void Sphere(IntPtr qobj, double radius, int slices, int stacks)
		{
			PreGLCall();
			gluSphere(qobj, radius, slices, stacks);
			PostGLCall();
		}

		/// <summary>
		/// This function sets the current stencil buffer function.
		/// </summary>
		/// <param name="func">The function type.</param>
		/// <param name="reference">The function reference.</param>
		/// <param name="mask">The function mask.</param>
		public void StencilFunc(uint func, int reference, uint mask)
		{
			PreGLCall();
			glStencilFunc(func, reference, mask);
			PostGLCall();
		}

        /// <summary>
        /// This function sets the current stencil buffer function.
        /// </summary>
        /// <param name="func">The function type.</param>
        /// <param name="reference">The function reference.</param>
        /// <param name="mask">The function mask.</param>
        public void StencilFunc(Enumerations.StencilFunction func, int reference, uint mask)
        {
            PreGLCall();
            glStencilFunc((uint)func, reference, mask);
            PostGLCall();
        }

		/// <summary>
		/// This function sets the stencil buffer mask.
		/// </summary>
		/// <param name="mask">The mask.</param>
		public void StencilMask(uint mask)
		{
			PreGLCall();
			glStencilMask(mask);
			PostGLCall();
		}

		/// <summary>
		/// This function sets the stencil buffer operation.
		/// </summary>
		/// <param name="fail">Fail operation.</param>
		/// <param name="zfail">Depth fail component.</param>
		/// <param name="zpass">Depth pass component.</param>
		public void StencilOp(uint fail, uint zfail, uint zpass)
		{
			PreGLCall();
			glStencilOp(fail, zfail, zpass);
			PostGLCall();
		}

		/// <summary>
		/// This function sets the stencil buffer operation.
		/// </summary>
		/// <param name="fail">Fail operation.</param>
		/// <param name="zfail">Depth fail component.</param>
		/// <param name="zpass">Depth pass component.</param>
        public void StencilOp(Enumerations.StencilOperation fail, Enumerations.StencilOperation zfail, Enumerations.StencilOperation zpass)
		{
			PreGLCall();
			glStencilOp((uint)fail, (uint)zfail, (uint)zpass);
			PostGLCall();
		}

		/// <summary>
		/// This function sets the current texture coordinates.
		/// </summary>
		/// <param name="s">Texture Coordinate.</param>
		public void TexCoord(double s)
		{
			PreGLCall();
			glTexCoord1d(s);
			PostGLCall();
		}

		/// <summary>
		/// This function sets the current texture coordinates.
		/// </summary>
		/// <param name="v">Array of 1,2,3 or 4 Texture Coordinates.</param>
		public void TexCoord(double []v)
		{
			PreGLCall();
			if(v.Length == 1)
				glTexCoord1dv(v);
			else if(v.Length == 2)
				glTexCoord2dv(v);
			else if(v.Length == 3)
				glTexCoord3dv(v);
			else if(v.Length == 4)
				glTexCoord4dv(v);
			PostGLCall();
		}

		/// <summary>
		/// This function sets the current texture coordinates.
		/// </summary>
		/// <param name="s">Texture Coordinate.</param>
		public void TexCoord(float s)
		{
			PreGLCall();
			glTexCoord1f(s);
			PostGLCall();
		}

		/// <summary>
		/// This function sets the current texture coordinates. WARNING: if you
		/// can call something more explicit, like TexCoord2f then call that, it's
		/// much faster.
		/// </summary>
		/// <param name="v">Array of 1,2,3 or 4 Texture Coordinates.</param>
		public void TexCoord(float[] v)
		{
			PreGLCall();
			if(v.Length == 1)
				glTexCoord1fv(v);
			else if(v.Length == 2)
				glTexCoord2fv(v);
			else if(v.Length == 3)
				glTexCoord3fv(v);
			else if(v.Length == 4)
				glTexCoord4fv(v);
			PostGLCall();
		}

		/// <summary>
		/// This function sets the current texture coordinates.
		/// </summary>
		/// <param name="s">Texture Coordinate.</param>
		public void TexCoord(int s)
		{
			PreGLCall();
			glTexCoord1i(s);
			PostGLCall();
		}

		/// <summary>
		/// This function sets the current texture coordinates.
		/// </summary>
		/// <param name="v">Array of 1,2,3 or 4 Texture Coordinates.</param>
		public void TexCoord(int[] v)
		{
			PreGLCall();
			if(v.Length == 1)
				glTexCoord1iv(v);
			else if(v.Length == 2)
				glTexCoord2iv(v);
			else if(v.Length == 3)
				glTexCoord3iv(v);
			else if(v.Length == 4)
				glTexCoord4iv(v);
			PostGLCall();
		}

		/// <summary>
		/// This function sets the current texture coordinates.
		/// </summary>
		/// <param name="s">Texture Coordinate.</param>
		public void TexCoord(short s)
		{
			PreGLCall();
			glTexCoord1s(s);
			PostGLCall();
		}

		/// <summary>
		/// This function sets the current texture coordinates.
		/// </summary>
		/// <param name="v">Array of 1,2,3 or 4 Texture Coordinates.</param>
		public void TexCoord(short[] v)
		{
			PreGLCall();
			if(v.Length == 1)
				glTexCoord1sv(v);
			else if(v.Length == 2)
				glTexCoord2sv(v);
			else if(v.Length == 3)
				glTexCoord3sv(v);
			else if(v.Length == 4)
				glTexCoord4sv(v);
			PostGLCall();
		}

		/// <summary>
		/// This function sets the current texture coordinates.
		/// </summary>
		/// <param name="s">Texture Coordinate.</param>
		/// <param name="t">Texture Coordinate.</param>
		public void TexCoord(double s, double t)
		{
			PreGLCall();
			glTexCoord2d(s, t);
			PostGLCall();
		}

		/// <summary>
		/// This function sets the current texture coordinates.
		/// </summary>
		/// <param name="s">Texture Coordinate.</param>
		/// <param name="t">Texture Coordinate.</param>
		public void TexCoord(float s, float t)
		{
			PreGLCall();
			glTexCoord2f(s, t);
			PostGLCall();
		}

		/// <summary>
		/// This function sets the current texture coordinates.
		/// </summary>
		/// <param name="s">Texture Coordinate.</param>
		/// <param name="t">Texture Coordinate.</param>
		public void TexCoord(int s, int t)
		{
			PreGLCall();
			glTexCoord2i(s, t);
			PostGLCall();
		}

		/// <summary>
		/// This function sets the current texture coordinates.
		/// </summary>
		/// <param name="s">Texture Coordinate.</param>
		/// <param name="t">Texture Coordinate.</param>
		public void TexCoord(short s, short t)
		{
			PreGLCall();
			glTexCoord2s(s, t);
			PostGLCall();
		}

        /// <summary>
        /// This function sets the current texture coordinates.
        /// </summary>
        /// <param name="s">Texture Coordinate.</param>
        /// <param name="t">Texture Coordinate.</param>
        /// <param name="r">Texture Coordinate.</param>
        public void TexCoord(double s, double t, double r)
        {
            PreGLCall();
            glTexCoord3d(s, t, r);
            PostGLCall();
        }

        /// <summary>
        /// This function sets the current texture coordinates.
        /// </summary>
        /// <param name="s">Texture Coordinate.</param>
        /// <param name="t">Texture Coordinate.</param>
        /// <param name="r">Texture Coordinate.</param>
        public void TexCoord(float s, float t, float r)
        {
            PreGLCall();
            glTexCoord3f(s, t, r);
            PostGLCall();
        }

        /// <summary>
        /// This function sets the current texture coordinates.
        /// </summary>
        /// <param name="s">Texture Coordinate.</param>
        /// <param name="t">Texture Coordinate.</param>
        /// <param name="r">Texture Coordinate.</param>
        public void TexCoord(int s, int t, int r)
        {
            PreGLCall();
            glTexCoord3i(s, t, r);
            PostGLCall();
        }

        /// <summary>
        /// This function sets the current texture coordinates.
        /// </summary>
        /// <param name="s">Texture Coordinate.</param>
        /// <param name="t">Texture Coordinate.</param>
        /// <param name="r">Texture Coordinate.</param>
        public void TexCoord(short s, short t, short r)
        {
            PreGLCall();
            glTexCoord3s(s, t, r);
            PostGLCall();
        }

        /// <summary>
        /// This function sets the current texture coordinates.
        /// </summary>
        /// <param name="s">Texture Coordinate.</param>
        /// <param name="t">Texture Coordinate.</param>
        /// <param name="r">Texture Coordinate.</param>
        /// <param name="q">Texture Coordinate.</param>
        public void TexCoord(double s, double t, double r, double q)
        {
            PreGLCall();
            glTexCoord4d(s, t, r, q);
            PostGLCall();
        }

        /// <summary>
        /// This function sets the current texture coordinates.
        /// </summary>
        /// <param name="s">Texture Coordinate.</param>
        /// <param name="t">Texture Coordinate.</param>
        /// <param name="r">Texture Coordinate.</param>
        /// <param name="q">Texture Coordinate.</param>
        public void TexCoord(float s, float t, float r, float q)
        {
            PreGLCall();
            glTexCoord4f(s, t, r, q);
            PostGLCall();
        }

        /// <summary>
        /// This function sets the current texture coordinates.
        /// </summary>
        /// <param name="s">Texture Coordinate.</param>
        /// <param name="t">Texture Coordinate.</param>
        /// <param name="r">Texture Coordinate.</param>
        /// <param name="q">Texture Coordinate.</param>
        public void TexCoord(int s, int t, int r, int q)
        {
            PreGLCall();
            glTexCoord4i(s, t, r, q);
            PostGLCall();
        }

        /// <summary>
        /// This function sets the current texture coordinates.
        /// </summary>
        /// <param name="s">Texture Coordinate.</param>
        /// <param name="t">Texture Coordinate.</param>
        /// <param name="r">Texture Coordinate.</param>
        /// <param name="q">Texture Coordinate.</param>
        public void TexCoord(short s, short t, short r, short q)
        {
            PreGLCall();
            glTexCoord4s(s, t, r, q);
            PostGLCall();
        }

        /// <summary>
        /// This function sets the texture coord array.
        /// </summary>
        /// <param name="size">The number of coords per set.</param>
        /// <param name="type">The type of data.</param>
        /// <param name="stride">The number of bytes between coords.</param>
        /// <param name="pointer">The coords.</param>
        public void TexCoordPointer(int size, uint type, int stride, IntPtr pointer)
        {
            PreGLCall();
            glTexCoordPointer(size, type, stride, pointer);
            PostGLCall();
        }

		/// <summary>
		/// This function sets the texture coord array.
		/// </summary>
		/// <param name="size">The number of coords per set.</param>
		/// <param name="type">The type of data.</param>
		/// <param name="stride">The number of bytes between coords.</param>
		/// <param name="pointer">The coords.</param>
		public void TexCoordPointer(int size, uint type, int stride, float[] pointer)
		{
			PreGLCall();
			glTexCoordPointer(size, type, stride, pointer);
			PostGLCall();
		}

        /// <summary>
        /// Set texture environment parameters.
        /// </summary>
        /// <param name="target">Specifies a texture environment. Must be OpenGL.TEXTURE_ENV.</param>
        /// <param name="pname">Specifies the symbolic name of a single-valued texture environment parameter. Must be OpenGL.TEXTURE_ENV_MODE.</param>
        /// <param name="param">Specifies a single symbolic constant, one of OpenGL.MODULATE, OpenGL.DECAL, OpenGL.BLEND, or OpenGL.REPLACE.</param>
        public void TexEnv(uint target, uint pname, float param)
        {
            PreGLCall();
            glTexEnvf(target, pname, param);
            PostGLCall();
        }

        /// <summary>
        /// Set texture environment parameters.
        /// </summary>
        /// <param name="target">Specifies a texture environment. Must be OpenGL.TEXTURE_ENV.</param>
        /// <param name="pname">Specifies the symbolic name of a texture environment parameter. Accepted values are OpenGL.TEXTURE_ENV_MODE and OpenGL.TEXTURE_ENV_COLOR.</param>
        /// <param name="parameters">Specifies a pointer to a parameter array that contains either a single symbolic constant or an RGBA color.</param>
        public void TexEnv(uint target, uint pname, float[] parameters)
        {
            PreGLCall();
            glTexEnvfv(target, pname, parameters);
            PostGLCall();
        }

        /// <summary>
        /// Set texture environment parameters.
        /// </summary>
        /// <param name="target">Specifies a texture environment. Must be OpenGL.TEXTURE_ENV.</param>
        /// <param name="pname">Specifies the symbolic name of a single-valued texture environment parameter. Must be OpenGL.TEXTURE_ENV_MODE.</param>
        /// <param name="param">Specifies a single symbolic constant, one of OpenGL.MODULATE, OpenGL.DECAL, OpenGL.BLEND, or OpenGL.REPLACE.</param>
        public void TexEnv(uint target, uint pname, int param)
        {
            PreGLCall();
            glTexEnvi(target, pname, param);
            PostGLCall();
        }

        /// <summary>
        /// Set texture environment parameters.
        /// </summary>
        /// <param name="target">Specifies a texture environment. Must be OpenGL.TEXTURE_ENV.</param>
        /// <param name="pname">Specifies the symbolic name of a texture environment parameter. Accepted values are OpenGL.TEXTURE_ENV_MODE and OpenGL.TEXTURE_ENV_COLOR.</param>
        /// <param name="parameters">Specifies a pointer to a parameter array that contains either a single symbolic constant or an RGBA color.</param>
        public void TexEnv(uint target, uint pname, int[] parameters)
        {
            PreGLCall();
            glTexGeniv(target, pname, parameters);
            PostGLCall();
        }

        /// <summary>
        /// Control the generation of texture coordinates.
        /// </summary>
        /// <param name="coord">Specifies a texture coordinate. Must be one of OpenGL.S, OpenGL.T, OpenGL.R, or OpenGL.Q.</param>
        /// <param name="pname">Specifies the symbolic name of the texture-coordinate generation function. Must be OpenGL.TEXTURE_GEN_MODE.</param>
        /// <param name="param">Specifies a single-valued texture generation parameter, one of OpenGL.OBJECT_LINEAR, OpenGL.GL_EYE_LINEAR, or OpenGL.SPHERE_MAP.</param>
		public void TexGen(uint coord, uint pname, double param)
        {
            PreGLCall();
            glTexGend(coord, pname, param);
            PostGLCall();
        }

        /// <summary>
        /// Control the generation of texture coordinates.
        /// </summary>
        /// <param name="coord">Specifies a texture coordinate. Must be one of OpenGL.S, OpenGL.T, OpenGL.R, or OpenGL.Q.</param>
        /// <param name="pname">Specifies the symbolic name of the texture-coordinate generation function or function parameters. Must be OpenGL.TEXTURE_GEN_MODE, OpenGL.OBJECT_PLANE, or OpenGL.EYE_PLANE.</param>
        /// <param name="parameters">Specifies a pointer to an array of texture generation parameters. If pname is OpenGL.TEXTURE_GEN_MODE, then the array must contain a single symbolic constant, one of OpenGL.OBJECT_LINEAR, OpenGL.EYE_LINEAR, or OpenGL.SPHERE_MAP. Otherwise, params holds the coefficients for the texture-coordinate generation function specified by pname.</param>
        public void TexGen(uint coord, uint pname, double[] parameters) 
        {
            PreGLCall();
            glTexGendv(coord, pname, parameters);
            PostGLCall();
        }

        /// <summary>
        /// Control the generation of texture coordinates.
        /// </summary>
        /// <param name="coord">Specifies a texture coordinate. Must be one of OpenGL.S, OpenGL.T, OpenGL.R, or OpenGL.Q.</param>
        /// <param name="pname">Specifies the symbolic name of the texture-coordinate generation function. Must be OpenGL.TEXTURE_GEN_MODE.</param>
        /// <param name="param">Specifies a single-valued texture generation parameter, one of OpenGL.OBJECT_LINEAR, OpenGL.GL_EYE_LINEAR, or OpenGL.SPHERE_MAP.</param>
        public void TexGen(uint coord, uint pname, float param)
        {
            PreGLCall();
            glTexGenf(coord, pname, param);
            PostGLCall();
        }

        /// <summary>
        /// Control the generation of texture coordinates.
        /// </summary>
        /// <param name="coord">Specifies a texture coordinate. Must be one of OpenGL.S, OpenGL.T, OpenGL.R, or OpenGL.Q.</param>
        /// <param name="pname">Specifies the symbolic name of the texture-coordinate generation function or function parameters. Must be OpenGL.TEXTURE_GEN_MODE, OpenGL.OBJECT_PLANE, or OpenGL.EYE_PLANE.</param>
        /// <param name="parameters">Specifies a pointer to an array of texture generation parameters. If pname is OpenGL.TEXTURE_GEN_MODE, then the array must contain a single symbolic constant, one of OpenGL.OBJECT_LINEAR, OpenGL.EYE_LINEAR, or OpenGL.SPHERE_MAP. Otherwise, params holds the coefficients for the texture-coordinate generation function specified by pname.</param>
        public void TexGen(uint coord, uint pname, float[] parameters)
        {
            PreGLCall();
            glTexGenfv(coord, pname, parameters);
            PostGLCall();
        }

        /// <summary>
        /// Control the generation of texture coordinates.
        /// </summary>
        /// <param name="coord">Specifies a texture coordinate. Must be one of OpenGL.S, OpenGL.T, OpenGL.R, or OpenGL.Q.</param>
        /// <param name="pname">Specifies the symbolic name of the texture-coordinate generation function. Must be OpenGL.TEXTURE_GEN_MODE.</param>
        /// <param name="param">Specifies a single-valued texture generation parameter, one of OpenGL.OBJECT_LINEAR, OpenGL.GL_EYE_LINEAR, or OpenGL.SPHERE_MAP.</param>
        public void TexGen(uint coord, uint pname, int param)
        {
            PreGLCall();
            glTexGeni(coord, pname, param);
            PostGLCall();
        }

        /// <summary>
        /// Control the generation of texture coordinates.
        /// </summary>
        /// <param name="coord">Specifies a texture coordinate. Must be one of OpenGL.S, OpenGL.T, OpenGL.R, or OpenGL.Q.</param>
        /// <param name="pname">Specifies the symbolic name of the texture-coordinate generation function or function parameters. Must be OpenGL.TEXTURE_GEN_MODE, OpenGL.OBJECT_PLANE, or OpenGL.EYE_PLANE.</param>
        /// <param name="parameters">Specifies a pointer to an array of texture generation parameters. If pname is OpenGL.TEXTURE_GEN_MODE, then the array must contain a single symbolic constant, one of OpenGL.OBJECT_LINEAR, OpenGL.EYE_LINEAR, or OpenGL.SPHERE_MAP. Otherwise, params holds the coefficients for the texture-coordinate generation function specified by pname.</param>
        public void TexGen(uint coord, uint pname, int[] parameters)
        {
            PreGLCall();
            glTexGeniv(coord, pname, parameters);
            PostGLCall();
        }

		/// <summary>
		/// This function sets the image for the currently binded texture.
		/// </summary>
		/// <param name="target">The type of texture, TEXTURE_2D or PROXY_TEXTURE_2D.</param>
		/// <param name="level">For mip-map textures, ordinary textures should be '0'.</param>
		/// <param name="internalformat">The format of the data you are want OpenGL to create, e.g  RGB16.</param>
		/// <param name="width">The width of the texture image (must be a power of 2, e.g 64).</param>
		/// <param name="border">The width of the border (0 or 1).</param>
		/// <param name="format">The format of the data you are passing, e.g. RGBA.</param>
		/// <param name="type">The type of data you are passing, e.g GL_BYTE.</param>
		/// <param name="pixels">The actual pixel data.</param>
		public void TexImage1D(uint target, int level, uint internalformat, int width, int border, uint format, uint type,  byte[] pixels)
		{
			PreGLCall();
			glTexImage1D(target, level, internalformat, width, border, format, type, pixels);
			PostGLCall();
		}

		/// <summary>
		/// This function sets the image for the currently binded texture.
		/// </summary>
		/// <param name="target">The type of texture, TEXTURE_2D or PROXY_TEXTURE_2D.</param>
		/// <param name="level">For mip-map textures, ordinary textures should be '0'.</param>
		/// <param name="internalformat">The format of the data you are want OpenGL to create, e.g  RGB16.</param>
		/// <param name="width">The width of the texture image (must be a power of 2, e.g 64).</param>
		/// <param name="height">The height of the texture image (must be a power of 2, e.g 32).</param>
		/// <param name="border">The width of the border (0 or 1).</param>
		/// <param name="format">The format of the data you are passing, e.g. RGBA.</param>
		/// <param name="type">The type of data you are passing, e.g GL_BYTE.</param>
		/// <param name="pixels">The actual pixel data.</param>
		public void TexImage2D(uint target, int level, uint internalformat, int width, int height, int border, uint format, uint type, byte[] pixels)
		{
			PreGLCall();
			glTexImage2D(target, level, internalformat, width, height, border, format, type, pixels);
			PostGLCall();
		}

        /// <summary>
        /// This function sets the image for the currently binded texture.
        /// </summary>
        /// <param name="target">The type of texture, TEXTURE_2D or PROXY_TEXTURE_2D.</param>
        /// <param name="level">For mip-map textures, ordinary textures should be '0'.</param>
        /// <param name="internalformat">The format of the data you are want OpenGL to create, e.g  RGB16.</param>
        /// <param name="width">The width of the texture image (must be a power of 2, e.g 64).</param>
        /// <param name="height">The height of the texture image (must be a power of 2, e.g 32).</param>
        /// <param name="border">The width of the border (0 or 1).</param>
        /// <param name="format">The format of the data you are passing, e.g. RGBA.</param>
        /// <param name="type">The type of data you are passing, e.g GL_BYTE.</param>
        /// <param name="pixels">The actual pixel data.</param>
        public void TexImage2D(uint target, int level, uint internalformat, int width, int height, int border, uint format, uint type, IntPtr pixels)
        {
            PreGLCall();
            glTexImage2D(target, level, internalformat, width, height, border, format, type, pixels);
            PostGLCall();
        }

		/// <summary>
		///	This function sets the parameters for the currently binded texture object.
		/// </summary>
		/// <param name="target">The type of texture you are setting the parameter to, e.g. TEXTURE_2D</param>
		/// <param name="pname">The parameter to set.</param>
		/// <param name="param">The value to set it to.</param>
		public void TexParameter(uint target, uint pname, float param)
		{
			PreGLCall();
			glTexParameterf(target, pname, param);
			PostGLCall();
		}

        /// <summary>
        ///	This function sets the parameters for the currently binded texture object.
        /// </summary>
        /// <param name="target">The type of texture you are setting the parameter to, e.g. TEXTURE_2D</param>
        /// <param name="pname">The parameter to set.</param>
        /// <param name="param">The value to set it to.</param>
        public void TexParameter(Enumerations.TextureTarget target, Enumerations.TextureParameter pname, float param)
        {
            PreGLCall();
            glTexParameterf((uint)target, (uint)pname, param);
            PostGLCall();
        }

		/// <summary>
		///	This function sets the parameters for the currently binded texture object.
		/// </summary>
		/// <param name="target">The type of texture you are setting the parameter to, e.g. TEXTURE_2D</param>
		/// <param name="pname">The parameter to set.</param>
		/// <param name="parameters">The value to set it to.</param>
		public void TexParameter(uint target, uint pname, float[] parameters)
		{
			PreGLCall();
			glTexParameterfv(target, pname, parameters);
			PostGLCall();
		}

        /// <summary>
        ///	This function sets the parameters for the currently binded texture object.
        /// </summary>
        /// <param name="target">The type of texture you are setting the parameter to, e.g. TEXTURE_2D</param>
        /// <param name="pname">The parameter to set.</param>
        /// <param name="parameters">The value to set it to.</param>
        public void TexParameter(Enumerations.TextureTarget target, Enumerations.TextureParameter pname, float[] parameters)
        {
            PreGLCall();
            glTexParameterfv((uint)target, (uint)pname, parameters);
            PostGLCall();
        }

		/// <summary>
		///	This function sets the parameters for the currently binded texture object.
		/// </summary>
		/// <param name="target">The type of texture you are setting the parameter to, e.g. TEXTURE_2D</param>
		/// <param name="pname">The parameter to set.</param>
		/// <param name="param">The value to set it to.</param>
		public void TexParameter(uint target, uint pname, int param)
		{
			PreGLCall();
			glTexParameteri(target, pname, param);
			PostGLCall();
		}

        /// <summary>
        ///	This function sets the parameters for the currently binded texture object.
        /// </summary>
        /// <param name="target">The type of texture you are setting the parameter to, e.g. TEXTURE_2D</param>
        /// <param name="pname">The parameter to set.</param>
        /// <param name="param">The value to set it to.</param>
        public void TexParameter(Enumerations.TextureTarget target, Enumerations.TextureParameter pname, int param)
        {
            PreGLCall();
            glTexParameteri((uint)target, (uint)pname, param);
            PostGLCall();
        }

		/// <summary>
		///	This function sets the parameters for the currently binded texture object.
		/// </summary>
		/// <param name="target">The type of texture you are setting the parameter to, e.g. TEXTURE_2D</param>
		/// <param name="pname">The parameter to set.</param>
		/// <param name="parameters">The value to set it to.</param>
		public void TexParameter(uint target, uint pname, int[] parameters)
		{
			PreGLCall();
			glTexParameteriv(target, pname, parameters);
			PostGLCall();
		}

        /// <summary>
        ///	This function sets the parameters for the currently binded texture object.
        /// </summary>
        /// <param name="target">The type of texture you are setting the parameter to, e.g. TEXTURE_2D</param>
        /// <param name="pname">The parameter to set.</param>
        /// <param name="parameters">The value to set it to.</param>
        public void TexParameter(Enumerations.TextureTarget target, Enumerations.TextureParameter pname, int[] parameters)
        {
            PreGLCall();
            glTexParameteriv((uint)target, (uint)pname, parameters);
            PostGLCall();
        }

        /// <summary>
        /// Specify a two-dimensional texture subimage.
        /// </summary>
        /// <param name="target">Specifies the target texture. Must be OpenGL.TEXTURE_1D.</param>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xoffset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="width">Specifies the width of the texture subimage.</param>
        /// <param name="format">Specifies the format of the pixel data.</param>
        /// <param name="type">Specifies the data type of the pixel	data.</param>
        /// <param name="pixels">Specifies a pointer to the image data in memory.</param>
        public void TexSubImage1D(uint target, int level, int xoffset, int width, uint format, uint type, int[] pixels)
        {
            PreGLCall();
            glTexSubImage1D(target, level, xoffset, width, format, type, pixels);
            PostGLCall();
        }

        /// <summary>
        /// Specify a two-dimensional texture subimage.
        /// </summary>
        /// <param name="target">Specifies the target texture. Must be OpenGL.TEXTURE_1D.</param>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xoffset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="yoffset">Specifies a texel offset in the y direction within the texture array.</param>
        /// <param name="width">Specifies the width of the texture subimage.</param>
        /// <param name="height">Specifies the height of the texture subimage.</param>
        /// <param name="format">Specifies the format of the pixel data.</param>
        /// <param name="type">Specifies the data type of the pixel	data.</param>
        /// <param name="pixels">Specifies a pointer to the image data in memory.</param>
        public void TexSubImage2D(uint target, int level, int xoffset, int yoffset, int width, int height, uint format, uint type, int[] pixels)
        {
            PreGLCall();
            glTexSubImage2D(target, level, xoffset, yoffset, width, height, format, type, pixels);
            PostGLCall();
        }

		/// <summary>
		/// This function applies a translation transformation to the current matrix.
		/// </summary>
		/// <param name="x">The amount to translate along the x axis.</param>
		/// <param name="y">The amount to translate along the y axis.</param>
		/// <param name="z">The amount to translate along the z axis.</param>
		public void Translate(double x, double y, double z)
		{
			PreGLCall();
			glTranslated(x, y, z);
			PostGLCall();
		}

		/// <summary>
		/// This function applies a translation transformation to the current matrix.
		/// </summary>
		/// <param name="x">The amount to translate along the x axis.</param>
		/// <param name="y">The amount to translate along the y axis.</param>
		/// <param name="z">The amount to translate along the z axis.</param>
		public void Translate(float x, float y, float z)
		{
			PreGLCall();
			glTranslatef(x, y, z);
			PostGLCall();
		}

		/// <summary>
		/// This function turns a screen Coordinate into a world coordinate.
		/// </summary>
		/// <param name="winx">Screen Coordinate.</param>
		/// <param name="winy">Screen Coordinate.</param>
		/// <param name="winz">Screen Coordinate.</param>
		/// <param name="modelMatrix">Current ModelView matrix.</param>
		/// <param name="projMatrix">Current Projection matrix.</param>
		/// <param name="viewport">Current Viewport.</param>
		/// <param name="objx">The world coordinate.</param>
		/// <param name="objy">The world coordinate.</param>
		/// <param name="objz">The world coordinate.</param>
		public void UnProject(double winx, double winy, double winz, 
			double[] modelMatrix, double[] projMatrix, int[] viewport, 
			ref double objx, ref double objy, ref double objz)
		{
			PreGLCall();
			gluUnProject(winx, winy, winz, modelMatrix, projMatrix, viewport,
				ref objx, ref objy, ref objz);
			PostGLCall();
		}

		/// <summary>
		/// This is a convenience function. It calls UnProject with the current 
		/// viewport, modelview and persective matricies, saving you from getting them.
		/// To use you own matricies, all the other version of UnProject.
		/// </summary>
		/// <param name="winx">X Coordinate (Screen Coordinate).</param>
		/// <param name="winy">Y Coordinate (Screen Coordinate).</param>
		/// <param name="winz">Z Coordinate (Screen Coordinate).</param>
		/// <returns>The world coordinate.</returns>
		public double[] UnProject(double winx, double winy, double winz)
		{
			PreGLCall();

			var modelview = new double[16];
			var projection = new double[16];
			var viewport = new int[4];
            GetDouble(GL_MODELVIEW_MATRIX, modelview);
            GetDouble(GL_PROJECTION_MATRIX, projection);
            GetInteger(GL_VIEWPORT, viewport);
            var result = new double[3];
            gluUnProject(winx, winy, winz, modelview, projection, viewport, ref result[0], ref result[1], ref result[2]);

			PostGLCall();

            return result;
		}

		/// <summary>
		/// Set the current vertex (must be called between 'Begin' and 'End').
		/// </summary>
		/// <param name="x">X Value.</param>
		/// <param name="y">Y Value.</param>
		public void Vertex(double x, double y)
		{
			PreGLCall();
			glVertex2d(x, y);
			PostGLCall();
		}

        /// <summary>
        /// Set the current vertex (must be called between 'Begin' and 'End').
        /// </summary>
        /// <param name="v">Specifies the coordinate.</param>
        public void Vertex(double[] v)
        {
            PreGLCall();
            if (v.Length == 2)
                glVertex2dv(v);
            else if (v.Length == 3)
                glVertex3dv(v);
            else if (v.Length == 4)
                glVertex4dv(v);
            PostGLCall();
        }

        /// <summary>
        /// Set the current vertex (must be called between 'Begin' and 'End').
        /// </summary>
        /// <param name="x">X Value.</param>
        /// <param name="y">Y Value.</param>
        public void Vertex(float x, float y)
        {
            PreGLCall();
            glVertex2f(x, y);
            PostGLCall();
        }

        /// <summary>
        /// Set the current vertex (must be called between 'Begin' and 'End').
        /// </summary>
        /// <param name="x">X Value.</param>
        /// <param name="y">Y Value.</param>
        public void Vertex(int x, int y)
        {
            PreGLCall();
            glVertex2i(x, y);
            PostGLCall();
        }

        /// <summary>
        /// Set the current vertex (must be called between 'Begin' and 'End').
        /// </summary>
        /// <param name="v">Specifies the coordinate.</param>
        public void Vertex(int[] v)
        {
            PreGLCall();
            if (v.Length == 2)
                glVertex2iv(v);
            else if (v.Length == 3)
                glVertex3iv(v);
            else if (v.Length == 4)
                glVertex4iv(v);
            PostGLCall();
        }

        /// <summary>
        /// Set the current vertex (must be called between 'Begin' and 'End').
        /// </summary>
        /// <param name="x">X Value.</param>
        /// <param name="y">Y Value.</param>
        public void Vertex(short x, short y)
        {
            PreGLCall();
            glVertex2s(x, y);
            PostGLCall();
        }

        /// <summary>
        /// Set the current vertex (must be called between 'Begin' and 'End').
        /// </summary>
        /// <param name="v">Specifies the coordinate.</param>
        public void Vertex2sv(short[] v)
        {
            PreGLCall();
            if (v.Length == 2)
                glVertex2sv(v);
            else if (v.Length == 3)
                glVertex3sv(v);
            else if (v.Length == 4)
                glVertex4sv(v);
            PostGLCall();
        }

		/// <summary>
		/// Set the current vertex (must be called between 'Begin' and 'End').
		/// </summary>
		/// <param name="x">X Value.</param>
		/// <param name="y">Y Value.</param>
		/// <param name="z">Z Value.</param>
		public void Vertex(double x, double y, double z)
		{
			PreGLCall();
			glVertex3d(x, y, z);
			PostGLCall();
		}

		/// <summary>
		/// Set the current vertex (must be called between 'Begin' and 'End').
		/// </summary>
		/// <param name="x">X Value.</param>
		/// <param name="y">Y Value.</param>
		/// <param name="z">Z Value.</param>
		public void Vertex(float x, float y, float z)
		{
			PreGLCall();
			glVertex3f(x, y, z);
			PostGLCall();
		}

		/// <summary>
		/// Sets the current vertex (must be called between 'Begin' and 'End').
		/// </summary>
		/// <param name="v">An array of 2, 3 or 4 floats.</param>
		public void Vertex(float []v)
		{
			PreGLCall();
			if(v.Length == 2)
				glVertex2fv(v);
			else if(v.Length == 3)
				glVertex3fv(v);
			else if(v.Length == 4)
				glVertex4fv(v);
			PostGLCall();
		}

        /// <summary>
        /// Set the current vertex (must be called between 'Begin' and 'End').
        /// </summary>
        /// <param name="x">X Value.</param>
        /// <param name="y">Y Value.</param>
        /// <param name="z">Z Value.</param>
        public void Vertex(int x, int y, int z)
        {
            PreGLCall();
            glVertex3i(x, y, z);
            PostGLCall();
        }

        /// <summary>
        /// Set the current vertex (must be called between 'Begin' and 'End').
        /// </summary>
        /// <param name="x">X Value.</param>
        /// <param name="y">Y Value.</param>
        /// <param name="z">Z Value.</param>
        public void Vertex(short x, short y, short z)
        {
            PreGLCall();
            glVertex3s(x, y, z);
            PostGLCall();
        }

        /// <summary>
        /// Set the current vertex (must be called between 'Begin' and 'End').
        /// </summary>
        /// <param name="x">X Value.</param>
        /// <param name="y">Y Value.</param>
        /// <param name="z">Z Value.</param>
        /// <param name="w">W Value.</param>
        public void Vertex4d(double x, double y, double z, double w)
        {
            PreGLCall();
            glVertex4d(x, y, z, w);
            PostGLCall();
        }

        /// <summary>
        /// Set the current vertex (must be called between 'Begin' and 'End').
        /// </summary>
        /// <param name="x">X Value.</param>
        /// <param name="y">Y Value.</param>
        /// <param name="z">Z Value.</param>
        /// <param name="w">W Value.</param>
        public void Vertex4f(float x, float y, float z, float w)
        {
            PreGLCall();
            glVertex4f(x, y, z, w);
            PostGLCall();
        }

        /// <summary>
        /// Set the current vertex (must be called between 'Begin' and 'End').
        /// </summary>
        /// <param name="x">X Value.</param>
        /// <param name="y">Y Value.</param>
        /// <param name="z">Z Value.</param>
        /// <param name="w">W Value.</param>
        public void Vertex4i(int x, int y, int z, int w)
        {
            PreGLCall();
            glVertex4i(x, y, z, w);
            PostGLCall();
        }

        /// <summary>
        /// Set the current vertex (must be called between 'Begin' and 'End').
        /// </summary>
        /// <param name="x">X Value.</param>
        /// <param name="y">Y Value.</param>
        /// <param name="z">Z Value.</param>
        /// <param name="w">W Value.</param>
        public void Vertex4s(short x, short y, short z, short w)
        {
            PreGLCall();
            glVertex4s(x, y, z, w);
            PostGLCall();
        }

		/// <summary>
		/// This function sets the address of the vertex pointer array.
		/// </summary>
		/// <param name="size">The number of coords per vertex.</param>
		/// <param name="type">The data type.</param>
		/// <param name="stride">The byte offset between vertices.</param>
		/// <param name="pointer">The array.</param>
		public void VertexPointer(int size, uint type, int stride, IntPtr pointer)
		{
			PreGLCall();
			glVertexPointer(size, type, stride, pointer);
			PostGLCall();
		}

        /// <summary>
        /// This function sets the address of the vertex pointer array.
        /// </summary>
        /// <param name="size">The number of coords per vertex.</param>
        /// <param name="stride">The byte offset between vertices.</param>
        /// <param name="pointer">The array.</param>
        public void VertexPointer(int size, int stride, short[] pointer)
        {
            PreGLCall();
            glVertexPointer(size, GL_SHORT, stride, pointer);
            PostGLCall();
        }

        /// <summary>
        /// This function sets the address of the vertex pointer array.
        /// </summary>
        /// <param name="size">The number of coords per vertex.</param>
        /// <param name="stride">The byte offset between vertices.</param>
        /// <param name="pointer">The array.</param>
        public void VertexPointer(int size, int stride, int[] pointer)
        {
            PreGLCall();
            glVertexPointer(size, GL_INT, stride, pointer);
            PostGLCall();
        }

        /// <summary>
        /// This function sets the address of the vertex pointer array.
        /// </summary>
        /// <param name="size">The number of coords per vertex.</param>
        /// <param name="stride">The byte offset between vertices.</param>
        /// <param name="pointer">The array.</param>
        public void VertexPointer(int size, int stride, float[] pointer)
        {
            PreGLCall();
            glVertexPointer(size, GL_FLOAT, stride, pointer);
            PostGLCall();
        }

        /// <summary>
        /// This function sets the address of the vertex pointer array.
        /// </summary>
        /// <param name="size">The number of coords per vertex.</param>
        /// <param name="stride">The byte offset between vertices.</param>
        /// <param name="pointer">The array.</param>
        public void VertexPointer(int size, int stride, double[] pointer)
        {
            PreGLCall();
            glVertexPointer(size, GL_DOUBLE, stride, pointer);
            PostGLCall();
        }

		/// <summary>
		/// This sets the viewport of the current Render Context. Normally x and y are 0
		/// and the width and height are just those of the control/graphics you are drawing
		/// to.
		/// </summary>
		/// <param name="x">Top-Left point of the viewport.</param>
		/// <param name="y">Top-Left point of the viewport.</param>
		/// <param name="width">Width of the viewport.</param>
		/// <param name="height">Height of the viewport.</param>
		public void Viewport (int x, int y, int width, int height)
		{
			PreGLCall();
			glViewport(x, y, width, height);
			PostGLCall();
		}
		
        /// <summary>
        /// Produce an error string from a GL or GLU error code.
        /// </summary>
        /// <param name="errCode">Specifies a GL or GLU error code.</param>
        /// <returns>The OpenGL/GLU error string.</returns>
		public unsafe string ErrorString(uint errCode)
        {
            PreGLCall();
            sbyte* pStr = gluErrorString(errCode);
            var str = new string(pStr);
            PostGLCall();

            return str;
        }

        /// <summary>
        /// Return a string describing the GLU version or GLU extensions.
        /// </summary>
        /// <param name="name">Specifies a symbolic constant, one of OpenGL.VERSION, or OpenGL.EXTENSIONS.</param>
        /// <returns>The GLU string.</returns>
		public unsafe string GetString(int name)
        {
            PreGLCall();
            sbyte* pStr = gluGetString(name);
            var str = new string(pStr);
            PostGLCall();

            return str;
        }

        /// <summary>
        /// Scale an image to an arbitrary size.
        /// </summary>
        /// <param name="format">Specifies the format of the pixel data.</param>
        /// <param name="widthin">Specify the width of the source image	that is	scaled.</param>
        /// <param name="heightin">Specify the height of the source image that is scaled.</param>
        /// <param name="typein">Specifies the data type for dataIn.</param>
        /// <param name="datain">Specifies a pointer to the source image.</param>
        /// <param name="widthout">Specify the width of the destination image.</param>
        /// <param name="heightout">Specify the height of the destination image.</param>
        /// <param name="typeout">Specifies the data type for dataOut.</param>
        /// <param name="dataout">Specifies a pointer to the destination image.</param>
		public void ScaleImage(int format, int widthin, int heightin, int typein, int[] datain, int widthout, int heightout, int typeout, int[] dataout)
        {
            PreGLCall();
            gluScaleImage(format, widthin, heightin, typein, datain, widthout, heightout, typeout, dataout);
            PostGLCall();
        }

        /// <summary>
        /// Create 1-D mipmaps.
        /// </summary>
        /// <param name="target">Specifies the target texture. Must be OpenGL.TEXTURE_1D.</param>
        /// <param name="components">Specifies the number of color components in the texture. Must be 1, 2, 3, or 4.</param>
        /// <param name="width">Specifies the width of the texture image.</param>
        /// <param name="format">Specifies the format of the pixel data.</param>
        /// <param name="type">Specifies the data type for data.</param>
        /// <param name="data">Specifies a pointer to the image data in memory.</param>
        public void Build1DMipmaps(uint target, uint components, int width, uint format, uint type, IntPtr data)
        {
            PreGLCall();
            gluBuild1DMipmaps(target, components, width, format, type, data);
            PostGLCall();
        }

        /// <summary>
        /// Create 2-D mipmaps.
        /// </summary>
        /// <param name="target">Specifies the target texture. Must be OpenGL.TEXTURE_1D.</param>
        /// <param name="components">Specifies the number of color components in the texture. Must be 1, 2, 3, or 4.</param>
        /// <param name="width">Specifies the width of the texture image.</param>
        /// <param name="height">Specifies the height of the texture image.</param>
        /// <param name="format">Specifies the format of the pixel data.</param>
        /// <param name="type">Specifies the data type for data.</param>
        /// <param name="data">Specifies a pointer to the image data in memory.</param>
		public void Build2DMipmaps(uint target, uint components, int width, int height, uint format, uint type, IntPtr data)
        {
            PreGLCall();
            gluBuild2DMipmaps(target, components, width, height, format, type, data);
            PostGLCall();
        }

        /// <summary>
        /// Draw a disk.
        /// </summary>
        /// <param name="qobj">Specifies the quadrics object (created with gluNewQuadric).</param>
        /// <param name="innerRadius">Specifies the	inner radius of	the disk (may be 0).</param>
        /// <param name="outerRadius">Specifies the	outer radius of	the disk.</param>
        /// <param name="slices">Specifies the	number of subdivisions around the z axis.</param>
        /// <param name="loops">Specifies the	number of concentric rings about the origin into which the disk is subdivided.</param>
		public void Disk(IntPtr qobj, double innerRadius, double outerRadius, int slices, int loops)
        {
            PreGLCall();
            gluDisk(qobj, innerRadius, outerRadius, slices, loops);
            PostGLCall();
        }

        /// <summary>
        /// Create a tessellation object.
        /// </summary>
        /// <returns>A new GLUtesselator poiner.</returns>
		public IntPtr NewTess()
        {
            PreGLCall();
            IntPtr returnValue = gluNewTess();
            PostGLCall();

            return returnValue;
        }

        /// <summary>
        /// Delete a tesselator object.
        /// </summary>
        /// <param name="tess">The tesselator pointer.</param>
		public void DeleteTess(IntPtr tess)
        {
            PreGLCall();
            gluDeleteTess(tess);
            PostGLCall();
        }

        /// <summary>
        /// Delimit a polygon description.
        /// </summary>
        /// <param name="tess">Specifies the tessellation object (created with gluNewTess).</param>
        /// <param name="polygonData">Specifies a pointer to user polygon data.</param>
		public void TessBeginPolygon(IntPtr tess, IntPtr polygonData)
        {
            PreGLCall();
            gluTessBeginPolygon(tess, polygonData);
            PostGLCall();
        }

        /// <summary>
        /// Delimit a contour description.
        /// </summary>
        /// <param name="tess">Specifies the tessellation object (created with gluNewTess).</param>
		public void TessBeginContour(IntPtr tess)
        {
            PreGLCall();
            gluTessBeginContour(tess);
        }

        /// <summary>
        /// Specify a vertex on a polygon.
        /// </summary>
        /// <param name="tess">Specifies the tessellation object (created with gluNewTess).</param>
        /// <param name="coords">Specifies the location of the vertex.</param>
        /// <param name="data">Specifies an opaque	pointer	passed back to the program with the vertex callback (as specified by gluTessCallback).</param>
		public void TessVertex(IntPtr tess, double[] coords, double[] data)
        {
            PreGLCall();
            gluTessVertex(tess, coords, data);
            PostGLCall();
        }

        /// <summary>
        /// Delimit a contour description.
        /// </summary>
        /// <param name="tess">Specifies the tessellation object (created with gluNewTess).</param>
		public void TessEndContour(IntPtr tess)
        {
            PreGLCall();
            gluTessEndContour(tess);
            PostGLCall();
        }

        /// <summary>
        /// Delimit a polygon description.
        /// </summary>
        /// <param name="tess">Specifies the tessellation object (created with gluNewTess).</param>
		public void TessEndPolygon(IntPtr tess)
        {
            PreGLCall();
            gluTessEndPolygon(tess);
            PostGLCall();
        }

        /// <summary>
        /// Set a tessellation object property.
        /// </summary>
        /// <param name="tess">Specifies the tessellation object (created with gluNewTess).</param>
        /// <param name="which">Specifies the property to be set.</param>
        /// <param name="value">Specifies the value of	the indicated property.</param>
		public void TessProperty(IntPtr tess, int which, double value)
        {
            PreGLCall();
            gluTessProperty(tess, which, value);
            PostGLCall();
        }

        /// <summary>
        /// Specify a normal for a polygon.
        /// </summary>
        /// <param name="tess">Specifies the tessellation object (created with gluNewTess).</param>
        /// <param name="x">Specifies the first component of the normal.</param>
        /// <param name="y">Specifies the second component of the normal.</param>
        /// <param name="z">Specifies the third component of the normal.</param>
		public void TessNormal(IntPtr tess, double x, double y, double z)
        {
            PreGLCall();
            gluTessNormal(tess, x, y, z);
            PostGLCall();
        }

        /// <summary>
        /// Set a tessellation object property.
        /// </summary>
        /// <param name="tess">Specifies the tessellation object (created with gluNewTess).</param>
        /// <param name="which">Specifies the property	to be set.</param>
        /// <param name="value">Specifies the value of	the indicated property.</param>
		public void GetTessProperty(IntPtr tess, int which, double value)
        {
            PreGLCall();
            gluGetTessProperty(tess, which, value);
            PostGLCall();
        }

        /// <summary>
        /// Delimit a NURBS trimming loop definition.
        /// </summary>
        /// <param name="nobj">Specifies the NURBS object (created with gluNewNurbsRenderer).</param>
		public void BeginTrim(IntPtr nobj)
        {
            PreGLCall();
            gluBeginTrim(nobj);
            PostGLCall();
        }

        /// <summary>
        /// Delimit a NURBS trimming loop definition.
        /// </summary>
        /// <param name="nobj">Specifies the NURBS object (created with gluNewNurbsRenderer).</param>
		public void EndTrim(IntPtr nobj)
        {
            PreGLCall();
            gluEndTrim(nobj);
            PostGLCall();
        }

        /// <summary>
        /// Describe a piecewise linear NURBS trimming curve.
        /// </summary>
        /// <param name="nobj">Specifies the NURBS object (created with gluNewNurbsRenderer).</param>
        /// <param name="count">Specifies the number of points on the curve.</param>
        /// <param name="array">Specifies an array containing the curve points.</param>
        /// <param name="stride">Specifies the offset (a number of single-precision floating-point values) between points on the curve.</param>
        /// <param name="type">Specifies the type of curve. Must be either OpenGL.MAP1_TRIM_2 or OpenGL.MAP1_TRIM_3.</param>
		public void PwlCurve(IntPtr nobj, int count, float array, int stride, uint type)
        {
            PreGLCall();
            gluPwlCurve(nobj, count, array, stride, type);
            PostGLCall();
        }

        /// <summary>
        /// Load NURBS sampling and culling matrice.
        /// </summary>
        /// <param name="nobj">Specifies the NURBS object (created with gluNewNurbsRenderer).</param>
        /// <param name="modelMatrix">Specifies a modelview matrix (as from a glGetFloatv call).</param>
        /// <param name="projMatrix">Specifies a projection matrix (as from a glGetFloatv call).</param>
        /// <param name="viewport">Specifies a viewport (as from a glGetIntegerv call).</param>
		public void LoadSamplingMatrices(IntPtr nobj, float[] modelMatrix, float[] projMatrix, int[] viewport)
        {
            PreGLCall();
            gluLoadSamplingMatrices(nobj, modelMatrix, projMatrix, viewport);
            PostGLCall();
        }

        /// <summary>
        /// Get a NURBS property.
        /// </summary>
        /// <param name="nobj">Specifies the NURBS object (created with gluNewNurbsRenderer).</param>
        /// <param name="property">Specifies the property whose value is to be fetched.</param>
        /// <param name="value">Specifies a pointer to the location into which the value of the named property is written.</param>
		public void GetNurbsProperty(IntPtr nobj, int property, float value)
        {
            PreGLCall();
            gluGetNurbsProperty(nobj, property, value);
            PostGLCall();
        }
		
		#endregion

    }
}
