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
using Apex.MVVM;
using Microsoft.Win32;
using SharpGL.SceneGraph;
using SharpGL.Serialization;
using SharpGL.SceneGraph.Cameras;

namespace DataLoader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            viewModel.LoadDataCommand.Executing += new CancelCommandEventHandler(LoadDataCommand_Executing);
            viewModel.SaveCommand.Executing += new CancelCommandEventHandler(SaveCommand_Executing);
        }

        void SaveCommand_Executing(object sender, CancelCommandEventArgs args)
        {
            var sglSerializer = new SharpGL.Serialization.SharpGL.SharpGLXmlFormat();

            //  Create an open file dialog.
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = sglSerializer.Filter;

            //  Show the dialog.
            if (saveFileDialog.ShowDialog() != true)
            {
                //  Cancel any loading.
                args.Cancel = true;
                return;
            }


            //  Set the args.
            args.Parameter = saveFileDialog.FileName;
        }

        /// <summary>
        /// Handles the Executing event of the LoadDataCommand control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="Apex.MVVM.CancelCommandEventArgs"/> instance containing the event data.</param>
        void LoadDataCommand_Executing(object sender, CancelCommandEventArgs args)
        {
            //  Create an open file dialog.
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = SerializationEngine.Instance.Filter;

            //  Show the dialog.
            if (openFileDialog.ShowDialog() != true)
            {
                //  Cancel any loading.
                args.Cancel = true;
                return;
            }

            //  Load the scene.
            Scene scene = SerializationEngine.Instance.LoadScene(openFileDialog.FileName);

            //  Set the args.
            args.Parameter = scene;
        }

        private void SceneView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (viewModel.Camera is ArcBallCamera == false)
                return;
            ArcBallCamera camera = viewModel.Camera as ArcBallCamera;

            Mouse.OverrideCursor = Cursors.None;
            camera.ArcBall.MouseDown((int)e.GetPosition(sceneView).X, (int)e.GetPosition(sceneView).Y);
        }

        private void SceneView_MouseMove(object sender, MouseEventArgs e)
        {
            if (viewModel.Camera is ArcBallCamera == false)
                return;
            ArcBallCamera camera = viewModel.Camera as ArcBallCamera;

            var pos = e.GetPosition(sceneView);
            if(e.LeftButton == MouseButtonState.Pressed)
                camera.ArcBall.MouseMove((int)pos.X, (int)pos.Y);
        }

        private void SceneView_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (viewModel.Camera is ArcBallCamera == false)
                return;
            ArcBallCamera camera = viewModel.Camera as ArcBallCamera;

            camera.ArcBall.MouseUp((int)e.GetPosition(sceneView).X, (int)e.GetPosition(sceneView).Y);
            Mouse.OverrideCursor = null;
        }
    }
}
