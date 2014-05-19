using SharpGL;
using SharpGL.RenderContextProviders;
using SharpGL.SceneGraph;
using SharpGL.Version;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SharpGL.WPF
{
    /// <summary>
    /// Interaction logic for OGLViewportImage.xaml.
    /// </summary>
    public partial class OpenGLControlJOG : UserControl, INotifyPropertyChanged
    {
        #region fields

        int _viewportWidth;
        int _viewportHeight;
        OpenGL _gl = new OpenGL();
        OpenGLVersion _oglVersion = OpenGLVersion.OpenGL2_1;
        RenderContextType _renderContextType = RenderContextType.FBO;
        WriteableBitmap _writeableBitmap;

        //OGLViewport _viewport;
        Stopwatch stopwatch = new Stopwatch();
        DispatcherTimer _timer = new DispatcherTimer();
        TimeSpan _interval = new TimeSpan(0,0,0,0,1000/24); // Set default to 24 frames/sec.
        DispatcherPriority _priority = DispatcherPriority.Render;

        OpenGLEventArgs _eventArgsFast;
        Queue<double> _frameTime = new Queue<double>();

        ImageSource _imgSource;
        #endregion fields

        #region properties
        /// <summary>
        /// A queue of the last recorded frame times.
        /// </summary>
        public Queue<double> FrameTime
        {
            get { return _frameTime; }
            set { _frameTime = value; }
        }

        /// <summary>
        /// The timer interval.
        /// </summary>
        public TimeSpan Interval
        {
            get { return _interval; }
            set
            { 
                _interval = value;
                if (_timer != null)
                    _timer.Interval = value;
            }
        }

        /// <summary>
        /// The last rendered frame that was retrieved by the timer.
        /// </summary>
        public ImageSource ImgSource 
        { 
            get { return _imgSource; } 
            set { _imgSource = value; }
        }
        
        /// <summary>
        /// The timer that refreshes every frame.
        /// </summary>
        public DispatcherTimer Timer
        {
            get { return _timer; }
            private set { _timer = value; }
        }

        /// <summary>
        /// Start or stop timer.
        /// </summary>
        public bool TimerIsEnabled
        {
            get { return _timer.IsEnabled; }
            set { _timer.IsEnabled = value; }
        }

        ///// <summary>
        ///// The virtual viewport. Call "GetFrame" to get the latest image.
        ///// </summary>
        //public OGLViewport Viewport
        //{
        //    get { return _viewport; }
        //    set { _viewport = value; }
        //}
        /// <summary>
        /// Whether or not the image has to be refreshed by retrieving an update from the GPU.
        /// </summary>
        public bool RefreshImage { get; set; }






        /// <summary>
        /// A writeableBitmap for the PBO.
        /// </summary>
        public WriteableBitmap WriteableBitmap
        {
            get { return _writeableBitmap; }
            set { _writeableBitmap = value; }
        }
        public int ViewportWidth
        {
            get { return _viewportWidth; }
            private set { _viewportWidth = value; }
        }

        public int ViewportHeight
        {
            get { return _viewportHeight; }
            private set { _viewportHeight = value; }
        }
        public OpenGL Gl
        {
            get { return _gl; }
            set { _gl = value; }
        }

        public OpenGLVersion OglVersion
        {
            get { return _oglVersion; }
            set { _oglVersion = value; }
        }

        public RenderContextType RenderContextType
        {
            get { return _renderContextType; }
            set { _renderContextType = value; }
        }
        #endregion properties

        #region events
        /// <summary>
        /// Occurs when OpenGL should be initialised.
        /// </summary>
        [Description("Called when OpenGL has been initialized."), Category("SharpGL")]
        public event OpenGLEventHandler OpenGLInitialized;

        public void OnOpenGLInitialized()
        {
            if (OpenGLInitialized != null)
                OpenGLInitialized(this, _eventArgsFast);
        }

        /// <summary>
        /// Occurs when OpenGL drawing should occur.
        /// </summary>
        [Description("Called whenever OpenGL drawing can should occur."), Category("SharpGL")]
        public event OpenGLEventHandler OpenGLDraw;
        public void OnOpenGLDraw()
        {
            if (OpenGLDraw != null)
                OpenGLDraw(this, _eventArgsFast);
        }


        /// <summary>
        /// Occurs when the control is resized. This can be used to perform custom projections.
        /// </summary>
        [Description("Called when the control is resized - you can use this to do custom viewport projections."), Category("SharpGL")]
        public event OpenGLEventHandler Resized;
        public void OnResized()
        {
            if (Resized != null)
                Resized(this, _eventArgsFast);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion events

        #region constructors
        public OpenGLControlJOG()
        {
            InitializeComponent();
            DataContext = this;
            RefreshImage = true;
        }
        #endregion constructors

        /// <summary>
        /// Start the timer.
        /// </summary>
        private void StartTimer()
        {
            _timer.Start();
        }

        /// <summary>
        /// Calls Viewport.GetFrame() and updates the ImgSource.
        /// </summary>
        public void Refresh(bool notifyChanged = true)
        {
            // Update the frame.
            ImgSource = GetFrame();

            // Notify that the frame was updated.
            if (notifyChanged)
                OnPropertyChanged("ImgSource");
        }

        public ImageSource GetFrame()
        {
            lock (Gl)
            {
                Gl.Blit(IntPtr.Zero);
                IntPtr hBitmap = IntPtr.Zero;

                switch (RenderContextType)
                {
                    case RenderContextType.DIBSection:
                        {
                            DIBSectionRenderContextProvider provider = Gl.RenderContextProvider as DIBSectionRenderContextProvider;

                            //  TODO: We have to remove the alpha channel - for some reason it comes out as 0.0 
                            //  meaning the drawing comes out transparent.
                            FormatConvertedBitmap newFormatedBitmapSource = new FormatConvertedBitmap();
                            newFormatedBitmapSource.BeginInit();
                            newFormatedBitmapSource.Source = BitmapConversion.HBitmapToBitmapSource(provider.DIBSection.HBitmap);
                            newFormatedBitmapSource.DestinationFormat = PixelFormats.Rgb24;
                            newFormatedBitmapSource.EndInit();

                            //  Copy the pixels over.

                            return newFormatedBitmapSource;
                        }
                    case RenderContextType.NativeWindow:
                        break;
                    case RenderContextType.HiddenWindow:
                        break;
                    case RenderContextType.FBO:
                        {
                            FBORenderContextProvider provider = Gl.RenderContextProvider as FBORenderContextProvider;

                            //  TODO: We have to remove the alpha channel - for some reason it comes out as 0.0 
                            //  meaning the drawing comes out transparent.
                            FormatConvertedBitmap newFormatedBitmapSource = new FormatConvertedBitmap();
                            newFormatedBitmapSource.BeginInit();
                            newFormatedBitmapSource.Source = BitmapConversion.HBitmapToBitmapSource(provider.InternalDIBSection.HBitmap);
                            newFormatedBitmapSource.DestinationFormat = PixelFormats.Rgb24;
                            newFormatedBitmapSource.EndInit();

                            //  Copy the pixels over.
                            return newFormatedBitmapSource;
                        }
                    case RenderContextType.PBO:
                        {
                            PBORenderContextProvider provider = Gl.RenderContextProvider as PBORenderContextProvider;
                            var width = provider.Width;
                            var height = provider.Height;
                            var pixelsPtr = provider.PixelPtr;
                            if (pixelsPtr == IntPtr.Zero)
                                return null;
                            var size = provider.Size;
                            var stride = provider.Stride;
                            var rect = new Int32Rect(0, 0, width, height);
                            WriteableBitmap.WritePixels(rect, pixelsPtr, size, stride, 0, 0);

                            return WriteableBitmap;
                        }
                    default:
                        break;
                }

            }
            return null;
        }

        public void Resize(int width, int height)
        {
            //  Lock on OpenGL.
            lock (Gl)
            {
                // Set default size.
                WriteableBitmap = new WriteableBitmap(width, height, 96, 96, PixelFormats.Bgra32, null);

                Gl.RenderContextProvider.SetDimensions(width, height);

                //	Set the viewport.
                Gl.Viewport(0, 0, width, height);

                ViewportHeight = height;
                ViewportWidth = width;
            }
        }

        /// <summary>
        /// Initialization logic for the GL.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OGLViewportImage_Loaded(object sender, RoutedEventArgs e)
        {
            // Set default size.
            WriteableBitmap = new WriteableBitmap(50, 50, 96, 96, PixelFormats.Bgra32, null);

            //  Lock on OpenGL.
            lock (Gl)
            {
                //  Create OpenGL.
                Gl.Create(OglVersion, RenderContextType, 1, 1, 32, null);
            }

            _eventArgsFast = new OpenGLEventArgs(Gl);

            // Notify that the GL is ready to use.
            OnOpenGLInitialized();

            // Notify to update the view settings.
            OnResized();

            // Synchronise the Image size with the Viewport size.
            Resize((int)ActualWidth, (int)ActualHeight);

            this.SizeChanged += OGLViewportImage_SizeChanged;

            // Set the timer.
            Timer = new DispatcherTimer(_priority);
            Timer.Interval = Interval;
            Timer.Tick += Timer_Tick;

            // Start timer.
            StartTimer();
        }

        /// <summary>
        /// Update the Viewport sizes and trigger notification that a resize happened.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OGLViewportImage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Resize((int)e.NewSize.Width, (int)e.NewSize.Height);
            OnResized();
        }

        /// <summary>
        /// The timer tick.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Timer_Tick(object sender, EventArgs e)
        {
            stopwatch.Restart();
            if (RefreshImage)
                Refresh();
            OnOpenGLDraw();

            stopwatch.Stop();

            _frameTime.Enqueue(stopwatch.Elapsed.TotalMilliseconds);
            if (_frameTime.Count > 10)
                _frameTime.Dequeue();
        }

    }
}
