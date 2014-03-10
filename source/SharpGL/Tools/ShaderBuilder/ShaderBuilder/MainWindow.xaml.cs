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

namespace ShaderBuilder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //  Listen out for certain commands.
            viewModel.FileOpenCommand.Executing += new CancelCommandEventHandler(FileOpenCommand_Executing);
            viewModel.FileSaveAsCommand.Executing += new CancelCommandEventHandler(FileSaveAsCommand_Executing);
            viewModel.FileExitCommand.Executing += new CancelCommandEventHandler(FileExitCommand_Executing);

            Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //  If we have no shaders, create a new one.
            if (viewModel.Shaders.Count == 0)
                viewModel.FileNewCommand.DoExecute(null);
        }

        /// <summary>
        /// Handles the Executing event of the FileOpenCommand control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="Apex.MVVM.CancelCommandEventArgs"/> instance containing the event data.</param>
        void FileOpenCommand_Executing(object sender, CancelCommandEventArgs args)
        {
            //  Create an Open File Dialog.
            OpenFileDialog openFileDialog = new OpenFileDialog();

            //  If the user cancels, end the command.
            if (openFileDialog.ShowDialog() != true)
            {
                //  Cancel the command.
                args.Cancel = true;
                return;
            }

            //  Set the parameter to the path.
            args.Parameter = openFileDialog.FileName;
        }

        /// <summary>
        /// Handles the Executing event of the FileSaveAsCommand control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="Apex.MVVM.CancelCommandEventArgs"/> instance containing the event data.</param>
        void FileSaveAsCommand_Executing(object sender, CancelCommandEventArgs args)
        {
            //  Create a Save File Dialog.
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            //  If the user cancels, end the command.
            if (saveFileDialog.ShowDialog() != true)
            {
                //  Cancel the command.
                args.Cancel = true;
                return;
            }

            //  Set the parameter to the path.
            args.Parameter = saveFileDialog.FileName;
        }

        /// <summary>
        /// Handles the Executing event of the FileExitCommand control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="Apex.MVVM.CancelCommandEventArgs"/> instance containing the event data.</param>
        void FileExitCommand_Executing(object sender, CancelCommandEventArgs args)
        {
            //  Close the application.
            Close();
        }
    }
}
