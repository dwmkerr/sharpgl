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
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Cameras;

namespace SharpGLStudio
{
    /// <summary>
    /// Interaction logic for ApplicationHost.xaml
    /// </summary>
    public partial class ApplicationHost : UserControl
    {
        public ApplicationHost()
        {
            InitializeComponent();
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ((SceneViewModel)DataContext).SelectedSceneElement = e.NewValue as SceneElement;
            propertyGrid.SelectedObject = e.NewValue;
        }

        private void SceneView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var pos = e.GetPosition(perspectiveSceneVew);

            ViewModel.SelectedElements.Clear();
            var hits  = ViewModel.Scene.DoHitTest((int)pos.X, (int)pos.Y);
            foreach (var hit in hits)
                ViewModel.SelectedElements.Add(hit);

            if (ViewModel.Camera is ArcBallCamera == false)
                return;
            ArcBallCamera camera = ViewModel.Camera as ArcBallCamera;

            Mouse.OverrideCursor = Cursors.None;
            camera.ArcBall.MouseDown((int)pos.X,
                (int)pos.Y);
       
        }

        private void SceneView_MouseMove(object sender, MouseEventArgs e)
        {
            if (ViewModel.Camera is ArcBallCamera == false)
                return;
            ArcBallCamera camera = ViewModel.Camera as ArcBallCamera;

            var pos = e.GetPosition(perspectiveSceneVew);
            if (e.LeftButton == MouseButtonState.Pressed)
                camera.ArcBall.MouseMove((int)pos.X, (int)pos.Y);
        }

        private void SceneView_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (ViewModel.Camera is ArcBallCamera == false)
                return;
            ArcBallCamera camera = ViewModel.Camera as ArcBallCamera;

            camera.ArcBall.MouseUp((int)e.GetPosition(perspectiveSceneVew).X,
                (int)e.GetPosition(perspectiveSceneVew).Y);
            Mouse.OverrideCursor = null;
        }

        /// <summary>
        /// Gets the view model.
        /// </summary>
        public SceneViewModel ViewModel
        {
            get { return (SceneViewModel)DataContext; }
        }
    }
}
