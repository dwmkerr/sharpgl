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

namespace SharpGL.WPF.SceneTree
{
    /// <summary>
    /// Interaction logic for SceneTree.xaml
    /// </summary>
    public partial class SceneTree : UserControl
    {
        /// <summary>
        /// Constructor for the SceneTree.
        /// </summary>
        public SceneTree()
        {
            InitializeComponent();

            if (Apex.Design.DesignTime.IsDesignTime)
            {
                //  Initialise the scene.
                Scene = new SceneGraph.Scene();
                SceneGraph.Helpers.SceneHelper.InitialiseModelingScene(Scene);
            }
        }

        private static readonly DependencyProperty SceneProperty =
          DependencyProperty.Register("Scene", typeof(Scene), typeof(SceneTree),
          new PropertyMetadata(null, new PropertyChangedCallback(OnSceneChanged)));

        /// <summary>
        /// The Scene which is being presented.
        /// </summary>
        public Scene Scene
        {
            get { return (Scene)GetValue(SceneProperty); }
            set { SetValue(SceneProperty, value); }
        }

        private static void OnSceneChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
        }
    }
}
