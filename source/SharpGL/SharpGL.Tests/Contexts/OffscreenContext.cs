using System;
using SharpGL.Version;

namespace SharpGL.Tests.Contexts
{
    /// <summary>
    /// EXPERIMENTAL: This class may well make it into the core for 
    /// version 3.0 as one of the new Context Types.
    /// The Offscreen Context creates a hidden window (as is common for
    /// OpenGL contexts) but renders to a framebuffer. This can be read
    /// into a bitmap.
    /// </summary>
    internal class OffscreenContext
    {
        public void Create(OpenGLVersion requiredVersion, OpenGL gl, int width, int height, int bitDepth)
        {
            this.width = width;
            this.height = height;
            this.bitDepth = bitDepth;

            //	Create a window class suitable for getting OpenGL functions.
            var wndClass = new Win32.WNDCLASSEX();
            wndClass.Init();
            wndClass.style = Win32.ClassStyles.HorizontalRedraw | Win32.ClassStyles.VerticalRedraw | Win32.ClassStyles.OwnDC;
            wndClass.lpfnWndProc = wndProcDelegate;
            wndClass.cbClsExtra = 0;
            wndClass.cbWndExtra = 0;
            wndClass.hInstance = IntPtr.Zero;
            wndClass.hIcon = IntPtr.Zero;
            wndClass.hCursor = IntPtr.Zero;
            wndClass.hbrBackground = IntPtr.Zero;
            wndClass.lpszMenuName = null;
            wndClass.lpszClassName = "SharpGLRenderWindow";
            wndClass.hIconSm = IntPtr.Zero;
            Win32.RegisterClassEx(ref wndClass);

            //	Create the invisible window.
            windowHandle = Win32.CreateWindowEx(0,
                          "SharpGLRenderWindow",
                          "",
                          Win32.WindowStyles.WS_CLIPCHILDREN | Win32.WindowStyles.WS_CLIPSIBLINGS | Win32.WindowStyles.WS_POPUP,
                          0, 0, 1, 1,
                          IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);

            //	Get the window device context.
            deviceContextHandle = Win32.GetDC(windowHandle);

            //	Setup a pixel format.
            var pfd = new Win32.PIXELFORMATDESCRIPTOR();
            pfd.Init();
            pfd.nVersion = 1;
            pfd.dwFlags = Win32.PFD_DRAW_TO_WINDOW | Win32.PFD_SUPPORT_OPENGL | Win32.PFD_DOUBLEBUFFER;
            pfd.iPixelType = Win32.PFD_TYPE_RGBA;
            pfd.cColorBits = (byte)bitDepth;
            pfd.cDepthBits = 16;
            pfd.cStencilBits = 8;
            pfd.iLayerType = Win32.PFD_MAIN_PLANE;

            //	Match an appropriate pixel format 
            int iPixelformat;
            if ((iPixelformat = Win32.ChoosePixelFormat(deviceContextHandle, pfd)) == 0)
                throw new Exception("Unable to create an appropriate pixel format.");

            //	Sets the pixel format
            if (Win32.SetPixelFormat(deviceContextHandle, iPixelformat, pfd) == 0)
                throw new Exception("Unable to set an appropriate pixel format.");

            //	Create the render context.
            renderContextHandle = Win32.wglCreateContext(deviceContextHandle);
            Win32.wglMakeCurrent(deviceContextHandle, renderContextHandle);

            //  TODO: This shows what's wrong with the OpenGL Version attributes.
            //  An OpenGL version should probably be a struct, with a helper to load it's value 
            //  from a string and some overriden comparison operators.

            //  Get the OpenGL version.
            var versionString = gl.GetString(OpenGL.GL_VERSION);
            var version = VersionAttribute.FromVersionString(versionString);
            var checkVersion = VersionAttribute.GetVersionAttribute(requiredVersion);
            if(version.IsAtLeastVersion(checkVersion.Major, checkVersion.Minor) == false)
                throw new OpenGLVersionException(string.Format("{0} doesn't meet the required version {1}.", version, checkVersion));

            //  First, create the frame buffer and bind it.
            framebuffer = gl.GenFramebuffers(1)[0];
            gl.BindFramebuffer(OpenGL.GL_FRAMEBUFFER, framebuffer);

            //	Create the colour render buffer and bind it, then allocate storage for it.
            var renderBuffers = gl.GenRenderbuffers(2);
            colourRenderbuffer = renderBuffers[0];
            depthRenderbuffer = renderBuffers[1];
            gl.BindRenderbuffer(OpenGL.GL_RENDERBUFFER, colourRenderbuffer);
            gl.RenderbufferStorage(OpenGL.GL_RENDERBUFFER, OpenGL.GL_RGBA, width, height);

            //	Create the depth render buffer and bind it, then allocate storage for it.
            gl.BindRenderbuffer(OpenGL.GL_RENDERBUFFER, depthRenderbuffer);
            gl.RenderbufferStorage(OpenGL.GL_RENDERBUFFER, OpenGL.GL_DEPTH_COMPONENT24, width, height);

            //  Set the render buffer for colour and depth.
            gl.FramebufferRenderbuffer(OpenGL.GL_FRAMEBUFFER, OpenGL.GL_COLOR_ATTACHMENT0,
                OpenGL.GL_RENDERBUFFER, colourRenderbuffer);
            gl.FramebufferRenderbuffer(OpenGL.GL_FRAMEBUFFER, OpenGL.GL_DEPTH_ATTACHMENT,
                OpenGL.GL_RENDERBUFFER, depthRenderbuffer);

            dibSectionDeviceContext = Win32.CreateCompatibleDC(deviceContextHandle);

            //  Create the DIB section.
            dibSection.Create(dibSectionDeviceContext, width, height, bitDepth);

            //  Are we complete?
            var status = gl.CheckFramebufferStatus(OpenGL.GL_FRAMEBUFFER);
            if (status != OpenGL.GL_FRAMEBUFFER_COMPLETE)
            {
                //  TODO: Consider how best to deal with this.
                throw new Exception("The framebuffer created is not complete.");
                //public const uint GL_FRAMEBUFFER_INCOMPLETE_ATTACHMENT = 0x8CD6;
                //public const uint GL_FRAMEBUFFER_INCOMPLETE_MISSING_ATTACHMENT = 0x8CD7;
                //public const uint GL_FRAMEBUFFER_INCOMPLETE_DRAW_BUFFER = 0x8CDB;
                //public const uint GL_FRAMEBUFFER_INCOMPLETE_READ_BUFFER = 0x8CDC;
                //public const uint GL_FRAMEBUFFER_UNSUPPORTED = 0x8CDD;
                //public const uint GL_FRAMEBUFFER_INCOMPLETE_MULTISAMPLE = 0x8D56;
                //public const uint GL_FRAMEBUFFER_UNDEFINED = 0x8219;
            }
        }

        public void SetDimensions(OpenGL gl, int width, int height)
        {
            this.width = width;
            this.height = height;

            //	Resize dib section.
            dibSection.Resize(width, height, bitDepth);

            //  Resize the render buffer storage.
            gl.BindRenderbuffer(OpenGL.GL_RENDERBUFFER, colourRenderbuffer);
            gl.RenderbufferStorage(OpenGL.GL_RENDERBUFFER, OpenGL.GL_RGBA, width, height);
            gl.BindRenderbuffer(OpenGL.GL_RENDERBUFFER, depthRenderbuffer);
            gl.RenderbufferStorage(OpenGL.GL_RENDERBUFFER, OpenGL.GL_DEPTH_ATTACHMENT, width, height);
            var complete = gl.CheckFramebufferStatus(OpenGL.GL_FRAMEBUFFER);
        }

        public void Destroy(OpenGL gl)
        {
            //  Delete the render buffers.
            gl.DeleteRenderbuffers(2, new [] { colourRenderbuffer, depthRenderbuffer});

            //	Delete the framebuffer.
            gl.DeleteFramebuffers(1, new [] { framebuffer });

            //  Reset the IDs.
            colourRenderbuffer = 0;
            depthRenderbuffer = 0;
            framebuffer = 0;
            
            //  Destroy the internal dc.
            Win32.wglMakeCurrent(IntPtr.Zero, IntPtr.Zero);
            Win32.DeleteDC(dibSectionDeviceContext);
            Win32.ReleaseDC(windowHandle, deviceContextHandle);
            Win32.DestroyWindow(windowHandle);
        }

        public void ReadBuffer(OpenGL gl)
        {
            //  Set the read buffer.
            gl.ReadBuffer(OpenGL.GL_COLOR_ATTACHMENT0);

            //	Read the pixels into the DIB section.
            gl.ReadPixels(0, 0, width, height, OpenGL.GL_BGRA,
                OpenGL.GL_UNSIGNED_BYTE, dibSection.Bits);
        }

        /// <summary>
        /// Gets the dib section.
        /// </summary>
        public DIBSection DIBSection { get { return dibSection; } }

        static private IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            return Win32.DefWindowProc(hWnd, msg, wParam, lParam);
        }

        private static readonly Win32.WndProc wndProcDelegate = new Win32.WndProc(WndProc);

        /// <summary>
        /// The dib section used internally.
        /// </summary>
        private readonly DIBSection dibSection = new DIBSection();

        private IntPtr windowHandle;
        private IntPtr deviceContextHandle;
        private IntPtr renderContextHandle;
        private IntPtr dibSectionDeviceContext;
        private uint framebuffer;
        private uint colourRenderbuffer;
        private uint depthRenderbuffer;
        private int width;
        private int height;
        private int bitDepth;

    }
}
