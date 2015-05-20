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
        /// <summary>
        /// Creates the OpenGL instance.
        /// </summary>
        /// <param name="openGLVersion">The OpenGL version requested.</param>
        /// <param name="renderContextType">Type of the render context.</param>
        /// <param name="width">The drawing context width.</param>
        /// <param name="height">The drawing context height.</param>
        /// <param name="bitDepth">The bit depth.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        public virtual bool Create(OpenGLVersion openGLVersion, RenderContextType renderContextType, int width, int height, int bitDepth, object parameter)
        {
            //  Return if we don't have a sensible width or height.
            if (width == 0 || height == 0 || bitDepth == 0)
                return false;

            //	Create an instance of the render context provider.
            switch (renderContextType)
            {
                case RenderContextType.DIBSection:
                    renderContextProvider = new DIBSectionRenderContextProvider();
                    break;
                case RenderContextType.NativeWindow:
                    renderContextProvider = new NativeWindowRenderContextProvider();
                    break;
                case RenderContextType.HiddenWindow:
                    renderContextProvider = new HiddenWindowRenderContextProvider();
                    break;
                case RenderContextType.FBO:
                    renderContextProvider = new FBORenderContextProvider();
                    break;
            }

            //	Create the render context.
            renderContextProvider.Create(openGLVersion, this, width, height, bitDepth, parameter);

            return true;
        }

        /// <summary>
        /// Creates the OpenGL instance using an external, existing render context.
        /// </summary>
        /// <param name="openGLVersion">The OpenGL version requested.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="bitDepth">The bit depth.</param>
        /// <param name="windowHandle">The window handle.</param>
        /// <param name="renderContextHandle">The render context handle.</param>
        /// <param name="deviceContextHandle">The device context handle.</param>
        /// <returns>
        /// True on success
        /// </returns>
        public virtual bool CreateFromExternalContext(OpenGLVersion openGLVersion, int width, int height, int bitDepth, IntPtr windowHandle, IntPtr renderContextHandle, IntPtr deviceContextHandle)
        {
            // Return if we don't have a sensible width or height.
            if (width == 0 || height == 0 || bitDepth == 0)
            {
                return false;
            }

            renderContextProvider = new ExternalRenderContextProvider(windowHandle, renderContextHandle, deviceContextHandle);
            renderContextProvider.Create(openGLVersion, this, width, height, bitDepth, null);

            return true;
        }

        /// <summary>
        /// Makes the OpenGL instance current.
        /// </summary>
        public virtual void MakeCurrent()
        {
            //  As long as we have a render context provider, make it current.
            if (renderContextProvider != null)
            {
                renderContextProvider.MakeCurrent();
                currentOpenGLInstance = this;
            }
        }

        /// <summary>
        /// Makes no render context current.
        /// </summary>
        public virtual void MakeNothingCurrent()
        {
            Win32.wglMakeCurrent(IntPtr.Zero, IntPtr.Zero);
        }

        /// <summary>
        /// Blits to the specified device context handle.
        /// </summary>
        /// <param name="deviceContextHandle">The device context handle.</param>
        public virtual void Blit(IntPtr deviceContextHandle)
        {
            //  As long as we have a render context provider, blit to it.
            if (renderContextProvider != null)
                renderContextProvider.Blit(deviceContextHandle);
        }

        /// <summary>
        /// Set the render context dimensions.
        /// </summary>
        /// <param name="width">The width (in pixels).</param>
        /// <param name="height">The height (in pixels).</param>
        public virtual void SetDimensions(int width, int height)
        {
            if (renderContextProvider != null)
                renderContextProvider.SetDimensions(width, height);
        }
    }
}
