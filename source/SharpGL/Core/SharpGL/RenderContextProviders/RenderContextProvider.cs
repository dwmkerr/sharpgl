using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.RenderContextProviders
{
    public abstract class RenderContextProvider : IRenderContextProvider
    {
        /// <summary>
        /// Creates the render context provider. Must also create the OpenGL extensions.
        /// </summary>
        /// <param name="gl">The OpenGL context.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="bitDepth">The bit depth.</param>
        /// <returns></returns>
        public virtual bool Create(OpenGL gl, int width, int height, int bitDepth, object parameter)
        {
	        //  Set the width, height and bit depth.
            Width = width;
            Height = height;
            BitDepth = bitDepth;

            return true;
        }

        /// <summary>
        /// Destroys the render context provider instance.
        /// </summary>
        public virtual void Destroy()
        {
            //  If we have a render context, destroy it.
            if(renderContextHandle != IntPtr.Zero)
            {
                Win32.wglDeleteContext(renderContextHandle);
                renderContextHandle = IntPtr.Zero;
            }
        }

        /// <summary>
        /// Sets the dimensions of the render context provider.
        /// </summary>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        public virtual void SetDimensions(int width, int height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Makes the render context current.
        /// </summary>
        public abstract void MakeCurrent();

        /// <summary>
        /// Blit the rendered data to the supplied device context.
        /// </summary>
        /// <param name="hdc">The HDC.</param>
        public abstract void Blit(IntPtr hdc);

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        void IDisposable.Dispose()
        {
            //  Destroy the context provider.
            Destroy();
        }

        /// <summary>
        /// Gets the render context handle.
        /// </summary>
        public IntPtr RenderContextHandle
        {
            get { return renderContextHandle; }
            protected set { renderContextHandle = value; }
        }

        /// <summary>
        /// Gets the device context handle.
        /// </summary>
        public IntPtr DeviceContextHandle
        {
            get { return deviceContextHandle; }
            protected set { deviceContextHandle = value; }
        }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>The width.</value>
        public int Width
        {
            get { return width; }
            protected set { width = value; } 
        }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>The height.</value>
        public int Height
        {
            get { return height; }
            protected set { height = value; } 
        }

        /// <summary>
        /// Gets or sets the bit depth.
        /// </summary>
        /// <value>The bit depth.</value>
        public int BitDepth
        {
            get { return bitDepth; }
            protected set { bitDepth = value; } 
        }

        /// <summary>
        /// Gets a value indicating whether GDI drawing is enabled for this type of render context.
        /// </summary>
        /// <value><c>true</c> if GDI drawing is enabled; otherwise, <c>false</c>.</value>
        public bool GDIDrawingEnabled
        {
            get { return gdiDrawingEnabled; }
            protected set { gdiDrawingEnabled = value; }
        }

        /// <summary>
        /// The render context handle.
        /// </summary>
        protected IntPtr renderContextHandle = IntPtr.Zero;

        /// <summary>
        /// The device context handle.
        /// </summary>
        protected IntPtr deviceContextHandle = IntPtr.Zero;

        /// <summary>
        /// The width.
        /// </summary>
        protected int width = 0;

        /// <summary>
        /// The height.
        /// </summary>
        protected int height = 0;

        /// <summary>
        /// The bit depth.
        /// </summary>
        protected int bitDepth = 0;

        /// <summary>
        /// Is gdi drawing enabled?
        /// </summary>
        protected bool gdiDrawingEnabled = true;
    }
}
