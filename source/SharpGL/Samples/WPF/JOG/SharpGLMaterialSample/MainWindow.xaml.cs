using GlmNet;
using SharpGL.SceneGraph;
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

namespace SharpGLMaterialSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        #region fields
        private GLController _controller = new GLController();
        private Point _previousPosition;

        
        #endregion fields

        #region properties

        public GLController Controller
        {
            get { return _controller; }
            set { _controller = value; }
        }

        public Action<object, OpenGLEventArgs> Draw { get { return Controller.Draw; } }
        public Action<object, OpenGLEventArgs> Init { get { return Controller.Init; } }
        public Action<object, OpenGLEventArgs> Resized { get { return Controller.Resized; } }
        #endregion properties

        #region events


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion events

        #region constructors
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            MouseMove += MainWindow_MouseMove;
            MouseWheel += MainWindow_MouseWheel;
            MouseDown += MainWindow_MouseDown;


        }
        #endregion constructors

        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Click
            _previousPosition = e.GetPosition(sender as Window);
        }

        private void MainWindow_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            // Zoom
            Controller.MvpBuilder.TranslationZ += e.Delta / 1000f;
            Controller.RefreshModelview();
        }

        private void MainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.GetPosition(sender as Window);

            float xDiff = (float)(_previousPosition.X - pos.X);
            float yDiff = (float)(_previousPosition.Y - pos.Y);

            if (e.LeftButton == MouseButtonState.Pressed) // Translate.
            {
                Controller.MvpBuilder.TranslationX += xDiff/10;
                Controller.MvpBuilder.TranslationY += yDiff / 10;
                Controller.RefreshModelview();
            }
            else if (e.RightButton == MouseButtonState.Pressed) // Rotate.
            {
                var scaleFac = 1f;
                var horiDeg = scaleFac * (float)(xDiff / Width * Math.PI);
                var vertiDeg = scaleFac * (float)(yDiff / Height * Math.PI);

                Controller.MvpBuilder.AngleY += horiDeg;
                Controller.MvpBuilder.AngleX += vertiDeg;
                Controller.RefreshModelview();
            }

            _previousPosition = pos;
        }
    }
}
