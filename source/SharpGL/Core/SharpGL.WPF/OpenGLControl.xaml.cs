using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using SharpGL.RenderContextProviders;
using SharpGL.SceneGraph;
using SharpGL.Version;

namespace SharpGL.WPF
{
    /// <summary>
    /// Interaction logic for OpenGLControl.xaml
    /// </summary>
    public partial class OpenGLControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGLControl"/> class.
        /// </summary>
        public OpenGLControl()
        {
            InitializeComponent();

            Unloaded += OpenGLControl_Unloaded;
            Loaded += OpenGLControl_Loaded;
        }

        /// <summary>
        /// Handles the Loaded event of the OpenGLControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> Instance containing the event data.</param>
        private void OpenGLControl_Loaded(object sender, RoutedEventArgs routedEventArgs)
        {
            SizeChanged += OpenGLControl_SizeChanged;

            if (timer != null)
            {
                timer.Tick -= timer_Tick;
                timer.Tick += timer_Tick; // to avoid twice call, ensure timer_Tick event handler is registered only once, because it is also registred on apply template
            }

            UpdateOpenGLControl((int) RenderSize.Width, (int) RenderSize.Height);

            //  DispatcherTimer setup
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, (int)(1000.0 / FrameRate));
            timer.Start();
        }

        /// <summary>
        /// Handles the Unloaded event of the OpenGLControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> Instance containing the event data.</param>
        private void OpenGLControl_Unloaded(object sender, RoutedEventArgs routedEventArgs)
        {
            SizeChanged -= OpenGLControl_SizeChanged;

            if (timer != null)
            {
                timer.Stop();
                timer.Tick -= timer_Tick;
            }
        }

        /// <summary>
        /// Handles the SizeChanged event of the OpenGLControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.SizeChangedEventArgs"/> Instance containing the event data.</param>
        void OpenGLControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateOpenGLControl((int) e.NewSize.Width, (int) e.NewSize.Height);
        }

        /// <summary>
        /// This method is used to set the dimensions and the viewport of the opengl control.
        /// </summary>
        /// <param name="width">The width of the OpenGL drawing area.</param>
        /// <param name="height">The height of the OpenGL drawing area.</param>
        private void UpdateOpenGLControl(int width, int height)
        {
            SizeChangedEventArgs e;
            // Lock on OpenGL.
            lock (gl)
            {
                gl.SetDimensions(width, height);

                //	Set the viewport.
                gl.Viewport(0, 0, width, height);

                //  If we have a project handler, call it...
                if (width != -1 && height != -1)
                {
                    var handler = Resized;
                    if (handler != null)
                        handler(this, eventArgsFast);
                    else
                    {
                        //  Otherwise we do our own projection.
                        gl.MatrixMode(OpenGL.GL_PROJECTION);
                        gl.LoadIdentity();

                        // Calculate The Aspect Ratio Of The Window
                        gl.Perspective(45.0f, (float) width/(float) height, 0.1f, 100.0f);

                        gl.MatrixMode(OpenGL.GL_MODELVIEW);
                        gl.LoadIdentity();
                    }
                }
            }
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or 
        /// internal processes call <see cref="M:System.Windows.FrameworkElement.ApplyTemplate"/>.
        /// </summary>
        public override void OnApplyTemplate()
        {
            //  Call the base.
            base.OnApplyTemplate();

            //  Lock on OpenGL.
            lock (gl)
            {
                //  Create OpenGL.
                gl.Create(OpenGLVersion, RenderContextType, 1, 1, 32, null);
            }

            //  Create our fast event args.
            eventArgsFast = new OpenGLEventArgs(gl);

            //  Set the most basic OpenGL styles.
            gl.ShadeModel(OpenGL.GL_SMOOTH);
            gl.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            gl.ClearDepth(1.0f);
            gl.Enable(OpenGL.GL_DEPTH_TEST);
            gl.DepthFunc(OpenGL.GL_LEQUAL);
            gl.Hint(OpenGL.GL_PERSPECTIVE_CORRECTION_HINT, OpenGL.GL_NICEST);

            //  Fire the OpenGL initialised event.
            var handler = OpenGLInitialized;
            if (handler != null)
                handler(this, eventArgsFast);
        }

        /// <summary>
        /// Handles the Tick event of the timer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void timer_Tick(object sender, EventArgs e)
        {
            //  Lock on OpenGL.
            lock (gl)
            {
                //  Start the stopwatch so that we can time the rendering.
                stopwatch.Restart();

                //  Make GL current.
                gl.MakeCurrent();

                //	If there is a draw handler, then call it.
                var handler = OpenGLDraw;
                if (handler != null)
                    handler(this, eventArgsFast);
                else
                    gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT);

                //  Draw the FPS.
                if (DrawFPS)
                {
                    gl.DrawText(5, 5, 1.0f, 0.0f, 0.0f, "Courier New", 12.0f,  string.Format("Draw Time: {0:0.0000} ms ~ {1:0.0} FPS", frameTime, 1000.0 / frameTime));
                    gl.Flush();
                }

                //  Render.
                gl.Blit(IntPtr.Zero);

                switch (RenderContextType)
                {
                    case RenderContextType.DIBSection:
                        {
                            var provider = gl.RenderContextProvider as DIBSectionRenderContextProvider;
                            var hBitmap = provider.DIBSection.HBitmap;

                            if (hBitmap != IntPtr.Zero)
                            {
                                var newFormatedBitmapSource = GetFormatedBitmapSource(hBitmap);

                                //  Copy the pixels over.
                                image.Source = newFormatedBitmapSource;
                            }
                        }
                        break;
                    case RenderContextType.NativeWindow:
                        break;
                    case RenderContextType.HiddenWindow:
                        break;
                    case RenderContextType.FBO:
                        {
                            var provider = gl.RenderContextProvider as FBORenderContextProvider;
                            var hBitmap = provider.InternalDIBSection.HBitmap;

                            if (hBitmap != IntPtr.Zero)
                            {
                                var newFormatedBitmapSource = GetFormatedBitmapSource(hBitmap);

                                //  Copy the pixels over.
                                image.Source = newFormatedBitmapSource;
                            }
                        }
                        break;
                    default:
                        break;
                }

                //  Stop the stopwatch.
                stopwatch.Stop();

                //  Store the frame time.
                frameTime = stopwatch.Elapsed.TotalMilliseconds;      
            }
        }

        /// <summary>
        /// This method converts the output from the OpenGL render context provider to a 
        /// FormatConvertedBitmap in order to show it in the image.
        /// </summary>
        /// <param name="hBitmap">The handle of the bitmap from the OpenGL render context.</param>
        /// <returns>Returns the new format converted bitmap.</returns>
        private static FormatConvertedBitmap GetFormatedBitmapSource(IntPtr hBitmap)
        {
            //  TODO: We have to remove the alpha channel - for some reason it comes out as 0.0 
            //  meaning the drawing comes out transparent.

            FormatConvertedBitmap newFormatedBitmapSource = new FormatConvertedBitmap();
            newFormatedBitmapSource.BeginInit();
            newFormatedBitmapSource.Source = BitmapConversion.HBitmapToBitmapSource(hBitmap);
            newFormatedBitmapSource.DestinationFormat = PixelFormats.Rgb24;
            newFormatedBitmapSource.EndInit();

            return newFormatedBitmapSource;
        }

        /// <summary>
        /// Called when the frame rate is changed.
        /// </summary>
        /// <param name="o">The object.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnFrameRateChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            //  Get the control.
            OpenGLControl me = o as OpenGLControl;

            //  If we have the timer, set the time.
            if (me.timer != null)
            {
                //  Stop the timer.
                me.timer.Stop();

                //  Set the timer.
                me.timer.Interval = new TimeSpan(0, 0, 0, 0, (int)(1000f / me.FrameRate));

                //  Start the timer.
                me.timer.Start();
            }
        }

        /// <summary>
        /// A single event args for all our needs.
        /// </summary>
        private OpenGLEventArgs eventArgsFast;

        /// <summary>
        /// The OpenGL instance.
        /// </summary>
        private OpenGL gl = new OpenGL();
        
        /// <summary>
        /// The dispatcher timer.
        /// </summary>
        DispatcherTimer timer = null;

        /// <summary>
        /// A stopwatch used for timing rendering.
        /// </summary>
        protected Stopwatch stopwatch = new Stopwatch();

        /// <summary>
        /// The last frame time in milliseconds.
        /// </summary>
        protected double frameTime = 0;

        /// <summary>
        /// Occurs when OpenGL should be initialised.
        /// </summary>
        [Description("Called when OpenGL has been initialized."), Category("SharpGL")]
        public event OpenGLEventHandler OpenGLInitialized;

        /// <summary>
        /// Occurs when OpenGL drawing should occur.
        /// </summary>
        [Description("Called whenever OpenGL drawing can should occur."), Category("SharpGL")]
        public event OpenGLEventHandler OpenGLDraw;

        /// <summary>
        /// Occurs when the control is resized. This can be used to perform custom projections.
        /// </summary>
        [Description("Called when the control is resized - you can use this to do custom viewport projections."), Category("SharpGL")]
        public event OpenGLEventHandler Resized;
        
        /// <summary>
        /// The frame rate dependency property.
        /// </summary>
        private static readonly DependencyProperty FrameRateProperty =
          DependencyProperty.Register("FrameRate", typeof(double), typeof(OpenGLControl),
          new PropertyMetadata(28.0, new PropertyChangedCallback(OnFrameRateChanged)));

        /// <summary>
        /// Gets or sets the frame rate.
        /// </summary>
        /// <value>The frame rate.</value>
        public double FrameRate
        {
            get { return (double)GetValue(FrameRateProperty); }
            set { SetValue(FrameRateProperty, value); }
        }

        /// <summary>
        /// The render context type property.
        /// </summary>
        private static readonly DependencyProperty RenderContextTypeProperty =
          DependencyProperty.Register("RenderContextType", typeof(RenderContextType), typeof(OpenGLControl),
          new PropertyMetadata(RenderContextType.DIBSection, new PropertyChangedCallback(OnRenderContextTypeChanged)));

        /// <summary>
        /// Gets or sets the type of the render context.
        /// </summary>
        /// <value>The type of the render context.</value>
        public RenderContextType RenderContextType
        {
            get { return (RenderContextType)GetValue(RenderContextTypeProperty); }
            set { SetValue(RenderContextTypeProperty, value); }
        }

        /// <summary>
        /// Called when [render context type changed].
        /// </summary>
        /// <param name="o">The o.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnRenderContextTypeChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            OpenGLControl me = o as OpenGLControl;
        }

        /// <summary>
        /// The OpenGL Version property.
        /// </summary>
        private static readonly DependencyProperty OpenGLVersionProperty =
          DependencyProperty.Register("OpenGLVersion", typeof(OpenGLVersion), typeof(OpenGLControl),
          new PropertyMetadata(OpenGLVersion.OpenGL2_1));

        /// <summary>
        /// Gets or sets the OpenGL Version requested for the control.
        /// </summary>
        /// <value>The type of the render context.</value>
        public OpenGLVersion OpenGLVersion
        {
            get { return (OpenGLVersion)GetValue(OpenGLVersionProperty); }
            set { SetValue(OpenGLVersionProperty, value); }
        }

        /// <summary>
        /// The DrawFPS property.
        /// </summary>
        private static readonly DependencyProperty DrawFPSProperty =
          DependencyProperty.Register("DrawFPS", typeof(bool), typeof(OpenGLControl),
          new PropertyMetadata(false, null));

        /// <summary>
        /// Gets or sets a value indicating whether to draw FPS.
        /// </summary>
        /// <value>
        ///   <c>true</c> if draw FPS; otherwise, <c>false</c>.
        /// </value>
        public bool DrawFPS
        {
            get { return (bool)GetValue(DrawFPSProperty); }
            set { SetValue(DrawFPSProperty, value); }
        }

        /// <summary>
        /// Gets the OpenGL instance.
        /// </summary>
        public OpenGL OpenGL
        {
            get { return gl; }
        }
    }
}
