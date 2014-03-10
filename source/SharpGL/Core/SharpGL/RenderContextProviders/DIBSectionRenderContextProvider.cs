using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.RenderContextProviders
{
    public class DIBSectionRenderContextProvider : RenderContextProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DIBSectionRenderContextProvider"/> class.
        /// </summary>
        public DIBSectionRenderContextProvider()
        {
            //  We can layer GDI drawing on top of open gl drawing.
            GDIDrawingEnabled = true;
        }

        /// <summary>
        /// Creates the render context provider. Must also create the OpenGL extensions.
        /// </summary>
        /// <param name="gl">The OpenGL context.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="bitDepth">The bit depth.</param>
        /// <returns></returns>
        public override bool Create(OpenGL gl, int width, int height, int bitDepth, object parameter)
        {
            //  Call the base.
            base.Create(gl, width, height, bitDepth, parameter);

            //  Get the desktop DC.
            IntPtr desktopDC = Win32.GetDC(IntPtr.Zero);

            //  Create our DC as a compatible DC for the desktop.
            deviceContextHandle = Win32.CreateCompatibleDC(desktopDC);

            //  Release the desktop DC.
            Win32.ReleaseDC(IntPtr.Zero, desktopDC);

            //  Create our dib section.
            dibSection.Create(deviceContextHandle, width, height, bitDepth);

            //  Create the render context.
            Win32.wglMakeCurrent(IntPtr.Zero, IntPtr.Zero);
            renderContextHandle = Win32.wglCreateContext(deviceContextHandle);

            //  Make current.
            MakeCurrent();

            //  Return success.
            return true;
        }

        /// <summary>
        /// Destroys the render context provider instance.
        /// </summary>
	    public override void Destroy()
	    {
            //  Destroy the bitmap.
            dibSection.Destroy();

		    //	Release the device context.
            Win32.ReleaseDC(IntPtr.Zero, deviceContextHandle);

            //	Call the base, which will delete the render context handle.
            base.Destroy();
	    }

	    public override void SetDimensions(int width, int height)
	    {
            //  Call the base.
            base.SetDimensions(width, height);

		    //	Resize.
            dibSection.Resize(width, height, BitDepth);
	    }

	    public override void Blit(IntPtr hdc) 
	    {
            //  We must have a device context.
            if (deviceContextHandle != null)
		    {
			    //	Swap the buffers.
                Win32.SwapBuffers(deviceContextHandle);

                //  Blit to the device context.
                Win32.BitBlt(hdc, 0, 0, Width, Height, deviceContextHandle, 0, 0, Win32.SRCCOPY);
		    }
	    }
	
	    public override void MakeCurrent()
	    {
            //  If we have a render context and DC make current.
            if (renderContextHandle != IntPtr.Zero && deviceContextHandle != IntPtr.Zero)
                Win32.wglMakeCurrent(deviceContextHandle, renderContextHandle);
	    }

        /// <summary>
        /// The DIB Section object.
        /// </summary>
        protected DIBSection dibSection = new DIBSection();

        /// <summary>
        /// Gets the DIB section.
        /// </summary>
        /// <value>The DIB section.</value>
        public DIBSection DIBSection
        {
            get { return dibSection; }
        }
    }
}
