using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL.Version;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace SharpGL.RenderContextProviders
{
    public class PBORenderContextProvider : HiddenWindowRenderContextProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FBORenderContextProvider"/> class.
        /// </summary>
        public PBORenderContextProvider()
        {
            //  We can layer GDI drawing on top of open gl drawing.
            GDIDrawingEnabled = true;

        }

        /// <summary>
        /// Creates the render context provider. Must also create the OpenGL extensions.
        /// </summary>
        /// <param name="openGLVersion">The desired OpenGL version.</param>
        /// <param name="gl">The OpenGL context.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="bitDepth">The bit depth.</param>
        /// <param name="parameter">The parameter</param>
        /// <returns></returns>
        public override bool Create(OpenGLVersion openGLVersion, OpenGL gl, int width, int height, int bitDepth, object parameter)
        {
            this.gl = gl;

            //  Call the base class. 	        
            base.Create(openGLVersion, gl, width, height, bitDepth, parameter);

            // Request an id for our pixel buffer.
            uint[] ids = new uint[1];

            ids = new uint[1];
            gl.GenBuffers(1, ids);

            pixelBufferID = ids[0];

            // The writeable bitmap needs to be initialized, so trigger setDimensions for this.
            SetDimensions(width, height);

            
            return true;
	    }

        private void DestroyPixelbuffers()
        {
            //	Delete the framebuffer.
            gl.DeleteBuffers(1, new uint[] { pixelBufferID });

            //  Reset the IDs.
            pixelBufferID = 0;
        }

        public override void Destroy()
        {
            //  Delete the render buffers.
            DestroyPixelbuffers();


		    //	Call the base, which will delete the render context handle and window.
            base.Destroy();
	    }

        public override void SetDimensions(int width, int height)
        {
            //  Call the base.
            base.SetDimensions(width, height);

		    //	Recreate the bitmap with the new dimensions.
            var bitsPerPixel = 32;
            var bytesPerPixel = 4;
            Stride = (int)(((width * bitsPerPixel + bitsPerPixel - 1) / bitsPerPixel) * bytesPerPixel);


            var size = height * width * 4 * sizeof(byte);

            // Don't bother updating if the size is the same already.
            if (size == this.size)
                return;

            Size = size;

            gl.BindBuffer(target, pixelBufferID);
            gl.BufferData(target, Size, IntPtr.Zero, usage); // Reserve space.
            gl.BindBuffer(target, 0);
        }

        public override void Blit(IntPtr hdc)
        {
            if (deviceContextHandle != IntPtr.Zero)
            {
                gl.ReadBuffer(OpenGL.GL_COLOR_ATTACHMENT0);
                gl.BindBuffer(target, pixelBufferID);
                gl.ReadPixels(0, 0, width, height, OpenGL.GL_BGRA, OpenGL.GL_UNSIGNED_BYTE, IntPtr.Zero);
                pixelPtr = gl.MapBufferRange(OpenGL.GL_PIXEL_PACK_BUFFER, 0, size, OpenGL.GL_MAP_READ_BIT);

                gl.UnmapBuffer(OpenGL.GL_PIXEL_PACK_BUFFER);
                gl.BindBuffer(OpenGL.GL_PIXEL_PACK_BUFFER, 0);
		    }
        }

        protected uint pixelBufferID = 0;
        private int size = 0;
        private int stride;
        protected uint target = OpenGL.GL_PIXEL_PACK_BUFFER;
        protected uint usage = OpenGL.GL_DYNAMIC_DRAW;
        private IntPtr pixelPtr;
        //protected WriteableBitmap writableBitmap;
        protected OpenGL gl;

        /// <summary>
        /// Gets the pointer to the bitmap data.
        /// </summary>
        public IntPtr PixelPtr
        {
            get { return pixelPtr; }
            private set { pixelPtr = value; }
        }

        /// <summary>
        /// The size of the bitmap.
        /// </summary>
        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        /// <summary>
        /// The stride for the bitmap.
        /// </summary>
        public int Stride
        {
            get { return stride; }
            private set { stride = value; }
        }

        
    }
}
