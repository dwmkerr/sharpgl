using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using SharpGL;
using SlimDX;
using SlimDX.Direct3D9;

namespace FastGL
{
	/// <summary>
	/// Logic for FastGLControl.xaml
	/// </summary>
	public partial class FastGLControl : UserControl
	{
		/// <summary>
		/// The background-image of the control, uses Direct3D for rendering.
		/// </summary>
		D3DImage _image;

		/// <summary>
		/// Our OpenGL-Instance.
		/// </summary>
		OpenGL _gl;

		/// <summary>
		/// Our instance of Direct3DEx to create devices.
		/// </summary>
		Direct3DEx _d3dex = new Direct3DEx();

		/// <summary>
		/// The current device.
		/// </summary>
		DeviceEx _device;

		/// <summary>
		/// Handle of the DirectX-device for NV_DX_Interop
		/// </summary>
		IntPtr _dxDeviceGLHandle;

		/// <summary>
		/// Handle of the DirectX-texture for NV_DX_interop.
		/// </summary>
		IntPtr _glRenderBufferHandle;

		/// <summary>
		/// Name of our renderbuffer.
		/// </summary>
		uint _glRenderBufferName;

		/// <summary>
		/// Name of our FBO
		/// </summary>
		uint _framebuffer;

		/// <summary>
		/// The DirectX-surface to render on (a surface of _texture).
		/// </summary>
		Surface _renderBuffer;

		/// <summary>
		/// The DirectX-texture to render on.
		/// </summary>
		Texture _texture;

		/// <summary>
		/// Creates a new instance of the FastGLControl-class.
		/// </summary>
		public FastGLControl()
		{
			InitializeComponent();

			_image = new D3DImage();
			_image.IsFrontBufferAvailableChanged += IsFrontBufferAvailableChanged;

			Background = new ImageBrush(_image);
			Loaded += new RoutedEventHandler((object sender, RoutedEventArgs args) => { StartRendering(); });
			SizeChanged += new SizeChangedEventHandler(FastGLControl_SizeChanged);
			Dispatcher.ShutdownStarted += Dispatcher_ShutdownStarted;
		}

		/// <summary>
		/// Clean-up everything.
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Arguments of the event</param>
		void Dispatcher_ShutdownStarted(object sender, EventArgs e)
		{
			StopRendering();
			_d3dex.Dispose();
		}

		/// <summary>
		/// Create device and renderbuffer, initialize NV_DX_interop and start rendering.
		/// </summary>
		private void StartRendering()
		{
			_gl = new OpenGL();

			IntPtr hwnd = new HwndSource(0, 0, 0, 0, 0, "test", IntPtr.Zero).Handle;

			_gl.Create(SharpGL.Version.OpenGLVersion.OpenGL2_1, RenderContextType.HiddenWindow, 1, 1, 32, hwnd);

			PresentParameters pp = new PresentParameters();
			pp.DeviceWindowHandle = hwnd;
			pp.Windowed = true;
			pp.SwapEffect = SwapEffect.Flip;
			pp.PresentationInterval = PresentInterval.Default;
			pp.BackBufferWidth = (int)ActualWidth;
			pp.BackBufferHeight = (int)ActualHeight;
			pp.BackBufferFormat = Format.X8R8G8B8;
			pp.BackBufferCount = 1;

			_device = new DeviceEx(_d3dex, 0, DeviceType.Hardware, hwnd, CreateFlags.HardwareVertexProcessing | CreateFlags.PureDevice | CreateFlags.FpuPreserve | CreateFlags.Multithreaded, pp);
			_gl.MakeCurrent();
			_dxDeviceGLHandle = _gl.DXOpenDeviceNV(_device.ComPointer);

			//  Set the most basic OpenGL styles.
			_gl.ShadeModel(OpenGL.GL_SMOOTH);
			_gl.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
			_gl.ClearDepth(1.0f);

			_gl.Enable(OpenGL.GL_BLEND);
			_gl.Disable(OpenGL.GL_DEPTH_TEST);

			_gl.Enable(OpenGL.GL_TEXTURE_2D);
			_gl.TexEnv(OpenGL.GL_TEXTURE_ENV, OpenGL.GL_TEXTURE_ENV_MODE, OpenGL.GL_REPLACE);

			_gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);

			ResizeRendering();

			// leverage the Rendering event of WPF's composition target to
			// update the custom D3D scene
			CompositionTarget.Rendering += OnRenderOpenGL;

			_image.Lock();
			_image.SetBackBuffer(D3DResourceType.IDirect3DSurface9, _device.GetBackBuffer(0, 0).ComPointer);
			_image.Unlock();

			if (GLInitialize != null)
				GLInitialize(this, new EventArgs());
		}

		/// <summary>
		/// Change the size of the rendering.
		/// </summary>
		private void ResizeRendering()
		{	
			if (_gl.IsTexture(_glRenderBufferName) == 1)
				_gl.DeleteTextures(1, new uint[] { _glRenderBufferName });

			if (_gl.IsFramebufferEXT(_framebuffer))
				_gl.DeleteFramebuffersEXT(1, new uint[] { _framebuffer });

			uint[] names = new uint[1];
			_gl.GenTextures(1, names);
			_glRenderBufferName = names[0];

			_gl.GenFramebuffersEXT(1, names);
			_framebuffer = names[0];

			if (_texture != null)
			{
				_gl.DXUnregisterObjectNV(_dxDeviceGLHandle, _glRenderBufferHandle);
				_texture.Dispose();
			}

			if (_renderBuffer != null)
			{
				_renderBuffer.Dispose();
			}

			_texture = new Texture(_device, (int)ActualWidth, (int)ActualHeight, 1, Usage.None, Format.X8R8G8B8, Pool.Default);
			_renderBuffer = _texture.GetSurfaceLevel(0);
			_glRenderBufferHandle = _gl.DXRegisterObjectNV(_dxDeviceGLHandle, _texture.ComPointer, _glRenderBufferName, OpenGL.GL_TEXTURE_2D, OpenGL.WGL_ACCESS_READ_WRITE_NV);
			_gl.DXLockObjectsNV(_dxDeviceGLHandle, new IntPtr[] { _glRenderBufferHandle });

			_gl.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, _framebuffer);
			_gl.FramebufferTexture2DEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_COLOR_ATTACHMENT0_EXT, OpenGL.GL_TEXTURE_2D, _glRenderBufferName, 0);

			uint status = _gl.CheckFramebufferStatusEXT(OpenGL.GL_FRAMEBUFFER_EXT);
			switch (status)
			{
				case OpenGL.GL_FRAMEBUFFER_COMPLETE_EXT:
					break;
				case OpenGL.GL_FRAMEBUFFER_UNSUPPORTED_EXT:
					throw new InvalidOperationException("Not supported framebuffer-format!");
				default:
					throw new InvalidOperationException(status.ToString());
			}

			_gl.SetDimensions((int)ActualWidth, (int)ActualHeight);
			_gl.Viewport(0, 0, (int)ActualWidth, (int)ActualHeight);
			_gl.MatrixMode(OpenGL.GL_PROJECTION);
			_gl.LoadIdentity();
			_gl.Ortho(0, ActualWidth, ActualHeight, 0, 0, 1);
			_gl.MatrixMode(OpenGL.GL_MODELVIEW);
			_gl.LoadIdentity();

			// Correction for rendering pixel-accurate.
			// http://msdn.microsoft.com/en-us/library/windows/desktop/dd374282(v=vs.85).aspx
			_gl.Translate(0.375, 0.375, 0);

			_gl.DXUnlockObjectsNV(_dxDeviceGLHandle, new IntPtr[] { _glRenderBufferHandle });
		}

		/// <summary>
		/// Dispose framebuffer, textures and device and stop rendering.
		/// </summary>
		private void StopRendering()
		{
			_gl.DeleteTextures(1, new uint[] { _glRenderBufferName });
			_gl.DeleteFramebuffersEXT(1, new uint[] { _framebuffer });
			_gl.DXUnregisterObjectNV(_dxDeviceGLHandle, _glRenderBufferHandle);
			_gl.DXCloseDeviceNV(_dxDeviceGLHandle);

			_renderBuffer.Dispose();
			_device.Dispose();

			// This method is called when WPF loses its D3D device.
			// In such a circumstance, it is very likely that we have lost 
			// our custom D3D device also, so we should just release the scene.
			// We will create a new scene when a D3D device becomes 
			// available again.
			CompositionTarget.Rendering -= OnRenderOpenGL;
		}

		/// <summary>
		/// Gets called when the availability of the Frontbuffer has changed.
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Arguments of the event</param>
		void IsFrontBufferAvailableChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if (_image.IsFrontBufferAvailable)
			{
				StartRendering();
			}
			else
			{
				StopRendering();
			}
		}

		/// <summary>
		/// Returns the OpenGL-instance of this control.
		/// </summary>
		public OpenGL OpenGL
		{
			get { return _gl; }
		}

		/// <summary>
		/// Handler for the Render-event.
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Arguments of the event</param>
		public delegate void RenderHandler(object sender, RenderEventArgs args);

		/// <summary>
		/// Raised when we have to render with GL.
		/// </summary>
		public event RenderHandler Render;

		/// <summary>
		/// Raised when initializing OpenGL, you can set stuff here.
		/// </summary>
		public event EventHandler GLInitialize;

		/// <summary>
		/// Gets called when the size of the control has been changed.
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Arguments of the event</param>
		void FastGLControl_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			if (_gl != null)
			{
				ResizeRendering();
			}
		}

		/// <summary>
		/// Here we do the actual rendering.
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Arguments of the event</param>
		void OnRenderOpenGL(object sender, EventArgs e)
		{
			// Lock objects shared between OpenGL and Direct3D9
			_gl.DXLockObjectsNV(_dxDeviceGLHandle, new IntPtr[] { _glRenderBufferHandle });

			// Enable textures and set framebuffer
			_gl.Enable(OpenGL.GL_TEXTURE_2D);
			_gl.BindTexture(OpenGL.GL_TEXTURE_2D, 0);
			_gl.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, _framebuffer);

			// Raise Render-event.
			if (Render != null)
				Render(this, new RenderEventArgs(_gl));

			// Unlock objects shared between OpenGL and Direct3D9
			_gl.DXUnlockObjectsNV(_dxDeviceGLHandle, new IntPtr[] { _glRenderBufferHandle });

			// Copy OpenGL-Framebuffer to backbuffer of our device (this is done on the GPU)
			_device.StretchRectangle(_renderBuffer, _device.GetBackBuffer(0, 0), TextureFilter.None);

			// Refresh D3DImage
			_image.Lock();
			_image.AddDirtyRect(new Int32Rect(0, 0, _image.PixelWidth, _image.PixelHeight));
			_image.Unlock();
		}

		/// <summary>
		/// Argument for the Render-event.
		/// </summary>
		public class RenderEventArgs
		{
			/// <summary>
			/// The GL-instance you can use for drawing.
			/// </summary>
			public readonly OpenGL GL;

			/// <summary>
			/// Creates a new instance of the RenderEventArgs-class.
			/// </summary>
			/// <param name="gl">GL-instance to render on</param>
			public RenderEventArgs(OpenGL gl)
			{
				GL = gl;
			}
		}
	}
}
