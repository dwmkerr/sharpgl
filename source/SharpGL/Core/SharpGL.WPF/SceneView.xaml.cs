using System;
using System.Collections.Generic;
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
using SharpGL.SceneGraph;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Threading;
using SharpGL.SceneGraph.Cameras;

namespace SharpGL.WPF
{
    /// <summary>
    /// Interaction logic for SceneView.xaml
    /// </summary>
    public partial class SceneView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SceneView"/> class.
        /// </summary>
        public SceneView()
        {
            InitializeComponent();

            //  Handle the size changed event.
            SizeChanged += new SizeChangedEventHandler(SceneView_SizeChanged);
        }

        /// <summary>
        /// Handles the SizeChanged event of the SceneView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.SizeChangedEventArgs"/> instance containing the event data.</param>
        void SceneView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //  If we don't have a scene, we're done.
            if (Scene == null)
                return;

            //  Lock on the scene.
            lock (Scene)
            {
                //  Get the dimensions.
                int width = (int)e.NewSize.Width;
                int height = (int)e.NewSize.Height;

                //  Resize the scene.
                Scene.CurrentOpenGLContext.SetDimensions(width, height);
                Scene.Resize(width, height);
            }
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or 
        /// internal processes call <see cref="M:System.Windows.FrameworkElement.ApplyTemplate"/>.
        /// </summary>
        public override void OnApplyTemplate()
        {
            //  DispatcherTimer setup
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, (int)(1000.0 / FrameRate));
            timer.Start();
        }

        /// <summary>
        /// Handles the Tick event of the timer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void timer_Tick(object sender, EventArgs e)
        {
            //  If we don't have a scene, we're done.
            if (Scene == null)
                return;

            //  Lock on the Scene.
            lock (Scene)
            {
                //  Start the stopwatch so that we can time the rendering.
                stopwatch.Restart();

                //  Draw the scene.
                Scene.Draw(Camera);
                
                //  Draw the FPS.
                if (DrawFPS)
                {
                    Scene.CurrentOpenGLContext.DrawText(5, 5, 1.0f, 0.0f, 0.0f, "Courier New", 12.0f, string.Format("Draw Time: {0:0.0000} ms ~ {1:0.0} FPS", frameTime, 1000.0 / frameTime));
                    Scene.CurrentOpenGLContext.Flush();
                }

                if (Scene.CurrentOpenGLContext.RenderContextProvider is RenderContextProviders.DIBSectionRenderContextProvider)
                {
                    RenderContextProviders.DIBSectionRenderContextProvider provider = Scene.CurrentOpenGLContext.RenderContextProvider as RenderContextProviders.DIBSectionRenderContextProvider;

                    //  TODO: We have to remove the alpha channel - for some reason it comes out as 0.0 
                    //  meaning the drawing comes out transparent.
                    FormatConvertedBitmap newFormatedBitmapSource = new FormatConvertedBitmap();
                    newFormatedBitmapSource.BeginInit();
                    newFormatedBitmapSource.Source = BitmapConversion.HBitmapToBitmapSource(provider.DIBSection.HBitmap);
                    newFormatedBitmapSource.DestinationFormat = PixelFormats.Rgb24;
                    newFormatedBitmapSource.EndInit();

                    //  Copy the pixels over.
                    image.Source = newFormatedBitmapSource;
                }
                else if (Scene.CurrentOpenGLContext.RenderContextProvider is RenderContextProviders.FBORenderContextProvider)
                {
                    RenderContextProviders.FBORenderContextProvider provider = Scene.CurrentOpenGLContext.RenderContextProvider as RenderContextProviders.FBORenderContextProvider;

                    //  TODO: We have to remove the alpha channel - for some reason it comes out as 0.0 
                    //  meaning the drawing comes out transparent.
                    FormatConvertedBitmap newFormatedBitmapSource = new FormatConvertedBitmap();
                    newFormatedBitmapSource.BeginInit();
                    newFormatedBitmapSource.Source = BitmapConversion.HBitmapToBitmapSource(provider.InternalDIBSection.HBitmap);
                    newFormatedBitmapSource.DestinationFormat = PixelFormats.Rgb24;
                    newFormatedBitmapSource.EndInit();

                    //  Copy the pixels over.
                    image.Source = newFormatedBitmapSource;
                }

                //  Stop the stopwatch.
                stopwatch.Stop();

                //  Store the frame time.
                frameTime = stopwatch.Elapsed.TotalMilliseconds;
            }
        }

        /// <summary>
        /// Called when the frame rate is changed.
        /// </summary>
        /// <param name="o">The object.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnFrameRateChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            //  Get the control.
            SceneView me = o as SceneView;

            //  Stop the timer.
            me.timer.Stop();

            //  Set the timer.
            me.timer.Interval = new TimeSpan(0, 0, 0, 0, (int)(1000f / me.FrameRate));

            //  Start the timer.
            me.timer.Start();
        }
        
        /// <summary>
        /// The dispatcher timer.
        /// </summary>
        private DispatcherTimer timer = null;

        /// <summary>
        /// A stopwatch used for timing rendering.
        /// </summary>
        private Stopwatch stopwatch = new Stopwatch();

        /// <summary>
        /// The last frame time in milliseconds.
        /// </summary>
        private double frameTime = 0;
        
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
        /// The Scene Dependency Property.
        /// </summary>
        private static readonly DependencyProperty SceneProperty =
          DependencyProperty.Register("Scene", typeof(Scene), typeof(SceneView),
          new PropertyMetadata(null, new PropertyChangedCallback(OnSceneChanged)));

        /// <summary>
        /// Gets or sets the scene.
        /// </summary>
        /// <value>
        /// The scene.
        /// </value>
        public Scene Scene
        {
            get { return (SceneGraph.Scene)GetValue(SceneProperty); }
            set { SetValue(SceneProperty, value); }
        }

        /// <summary>
        /// Called when [scene changed].
        /// </summary>
        /// <param name="o">The o.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnSceneChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            //  Get the scene view.
            SceneView me = o as SceneView;
        }


        /// <summary>
        /// The camera dependency property.
        /// </summary>
        private static readonly DependencyProperty CameraProperty =
          DependencyProperty.Register("Camera", typeof(Camera), typeof(SceneView),
          new PropertyMetadata(null, new PropertyChangedCallback(OnCameraChanged)));

        /// <summary>
        /// Gets or sets the camera.
        /// </summary>
        /// <value>
        /// The camera.
        /// </value>
        public Camera Camera
        {
            get { return (Camera)GetValue(CameraProperty); }
            set { SetValue(CameraProperty, value); }
        }

        /// <summary>
        /// Called when [camera changed].
        /// </summary>
        /// <param name="o">The o.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnCameraChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            SceneView me = o as SceneView;
        }
                
                
    }
}
