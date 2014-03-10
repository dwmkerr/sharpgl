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
using Microsoft.Windows.Controls.Ribbon;
using Apex.MVVM;
using Microsoft.Win32;
using SharpGL.SceneGraph;

namespace SharpGLStudio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            viewModel.RibbonViewModel.LoadSceneCommand.Executing += new CancelCommandEventHandler(LoadSceneCommand_Executing);
        }

        void LoadSceneCommand_Executing(object sender, CancelCommandEventArgs args)
        {
            //  Use the serialization engine.
            var engine = SharpGL.Serialization.SerializationEngine.Instance;

            //  Create an open file dialog.
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = engine.Filter;

            //  Show the dialog.
            if (openFileDialog.ShowDialog() != true)
            {
                //  Cancel any loading.
                args.Cancel = true;
                return;
            }

            //  Load the scene.
            Scene scene = engine.LoadScene(openFileDialog.FileName);

            //  Set the args.
            args.Parameter = scene;

            //  Create in the current OpenGL context.
            scene.CreateInContext(viewModel.Scene.CurrentOpenGLContext);

            //  Switch the scene over.
            viewModel.SelectedElements.Clear();
            viewModel.Scene = scene;
        }
    }
}
