using SharpGL;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SharpGLBase.Extensions;
using GlmNet;

namespace SharpGLTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region events
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion events

        #region fields
        MainWindowViewModel _viewPortViewModel = new MainWindowViewModel();
        #endregion fields

        #region properties
        public string OnScreenData { get; set; }

        public MainWindowViewModel ViewPortViewModel
        {
            get { return _viewPortViewModel; }
            set { _viewPortViewModel = value; }
        }

        #endregion properties


        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            MouseMove += MainWindow_MouseMove;
            MouseWheel += MainWindow_MouseWheel;
            MouseDown += MainWindow_MouseDown;

            ViewPortViewModel.Scene.ModelView.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "ModelviewMatrix")
                {
                    var modelviewMatrix = ViewPortViewModel.Scene.ModelView.ModelviewMatrix;
                    var translationMatrix = ViewPortViewModel.Scene.ModelView.TranslationMatrix;
                    var scalingMatrix = ViewPortViewModel.Scene.ModelView.ScalingMatrix;
                    var rotationMatrix = ViewPortViewModel.Scene.ModelView.RotationMatrix;

                    string txt = "";

                    txt += "ModelviewMatrix:\n" + modelviewMatrix.ToValueString();
                    txt += "\n\nTranslationMatrix:\n" + translationMatrix.ToValueString();
                    txt += "\n\nScalingMatrix:\n" + scalingMatrix.ToValueString();
                    txt += "\n\nRotationMatrix:\n" + rotationMatrix.ToValueString();



                    OnScreenData = txt;
                    OnPropertyChanged("OnScreenData");
                }
            };
        }


        void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point p = e.MouseDevice.GetPosition(this);
                _viewPortViewModel.PointerClicked(PointedClickedAction.SelectModel, new System.Drawing.Point((int)p.X, (int)p.Y));
                //_viewPortViewModel.CameraLookAt.RotateCameraAroundRelativeX(.1);
            }
        }

        void MainWindow_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                _viewPortViewModel.MouseScroll(MouseScrollAction.ChangePerspectiveDepth, e.Delta > 0);
            }
            else
            {
                _viewPortViewModel.MouseScroll(MouseScrollAction.Zoom, e.Delta > 0);
            }
        }

        void MainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (_viewPortViewModel.LastPointerLocation != null)
            {
                long t1 = DateTime.Now.Ticks;
                long t2 = _viewPortViewModel.TimeLastPointerLocation.Value;
                if ( t1 > t2 + 3000000)
                {
                    _viewPortViewModel.PointerMoveEnd();
                }
            }

            if (e.RightButton == MouseButtonState.Pressed)
            {
                var p1 = e.MouseDevice.GetPosition(this);
                if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                {
                }
                else
                {
                    _viewPortViewModel.PointerMoved(new System.Drawing.Point((int)p1.X, (int)p1.Y), PointedMovedAction.RotateAroundCenter);
                }
            }
            else if (e.LeftButton == MouseButtonState.Pressed)
            {
                var p1 = e.MouseDevice.GetPosition(this);
                _viewPortViewModel.PointerMoved(new System.Drawing.Point((int)p1.X, (int)p1.Y), PointedMovedAction.MoveRelatively);
            }
            else
            {
                _viewPortViewModel.PointerMoveEnd();
            }
        }

        /// <summary>
        /// Handles the OpenGLDraw event of the OpenGLControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="SharpGL.SceneGraph.OpenGLEventArgs"/> instance containing the event data.</param>
        private void OpenGLControl_OpenGLDraw(object sender, OpenGLEventArgs args)
        {
            OpenGL gl = args.OpenGL;
            _viewPortViewModel.OpenGLDraw(gl);
        }

        /// <summary>
        /// Handles the OpenGLInitialized event of the OpenGLControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="SharpGL.SceneGraph.OpenGLEventArgs"/> instance containing the event data.</param>
        private void OpenGLControl_OpenGLInitialized(object sender, OpenGLEventArgs args)
        {
            OpenGL gl = args.OpenGL;
            _viewPortViewModel.OpenGLInitialized(gl);
        }

        private void OpenGLControl_Resized(object sender, OpenGLEventArgs args)
        {
            var glCtrl = sender as SharpGL.WPF.OpenGLControl;
            //  Get the OpenGL instance.
            var gl = args.OpenGL;
            _viewPortViewModel.ViewResized(gl, glCtrl.ActualWidth, glCtrl.ActualHeight);
        }

        private void MainWindow_MouseLeave(object sender, MouseEventArgs e)
        {
            _viewPortViewModel.PointerMoveEnd();
        }
    }
}
