using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apex.MVVM;
using SharpGL;
using System.Collections.ObjectModel;
using ShaderBuilder.Shader;
using System.IO;

namespace ShaderBuilder
{
    /// <summary>
    /// The main view model.
    /// </summary>
    public class MainViewModel : ViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        public MainViewModel()
        {
            //  Get the vendor strings.
            OpenGLVendor = ApplicationState.Instance.OpenGL.Vendor;
            OpenGLVersion = ApplicationState.Instance.OpenGL.Version;
            OpenGLRenderer = ApplicationState.Instance.OpenGL.Renderer;
            OpenGLShadingLanguageVersion = ApplicationState.Instance.OpenGL.GetString(OpenGL.GL_SHADING_LANGUAGE_VERSION);
        
            //  If the strings are empty, set them to something more descriptive.
            if (string.IsNullOrEmpty(OpenGLVendor)) OpenGLVendor = "<none>";
            if (string.IsNullOrEmpty(OpenGLVersion)) OpenGLVersion = "<none>";
            if (string.IsNullOrEmpty(OpenGLRenderer)) OpenGLRenderer = "<none>";
            if (string.IsNullOrEmpty(OpenGLShadingLanguageVersion)) OpenGLShadingLanguageVersion = "<none>";

            //  Create the commands.
            FileNewCommand = new Command(DoFileNewCommand);
            FileOpenCommand = new Command(DoFileOpenCommand);
            FileSaveCommand = new Command(DoFileSaveCommand);
            FileSaveAsCommand = new Command(DoFileSaveAsCommand);
            FileExitCommand = new Command(DoFileExitCommand);
            ViewShowOutputWindowCommand = new Command(DoViewShowOutputWindowCommand);
            BuildCompileCommand = new Command(DoBuildCompileCommand);
            BuildLinkCommand = new Command(DoBuildLinkCommand);
            BuildBuildCommand = new Command(DoBuildBuildCommand);
        }

        private void DoFileNewCommand(object parameter)
        {
            //  Create a new shader view model.
            ShaderViewModel shaderViewModel = new ShaderViewModel()
            {
                Name = "New Shader"
            };

            //  Add it to the shaders.
            shaders.Add(shaderViewModel);

            //  Set it as the active shader.
            CurrentShader = shaderViewModel;
        }

        private void DoFileOpenCommand(object parameter)
        {
            //  The parameter must be a string.
            if (parameter is string == false)
                throw new ArgumentException("The parameter to the command must be a valid file path.");

            //  Read the file.
            string source;
            try
            {
                //  Open the file stream.
                using (FileStream stream = new FileStream(parameter as string, FileMode.Open))
                {
                    //  Create a streamreader and read the source.
                    TextReader reader = new StreamReader(stream);
                    source = reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                //  Re-throw the exception.
                throw new ApplicationException("Failed to load the shader source.", e);
            }

            //  Create a new shader view model.
            ShaderViewModel shaderViewModel = new ShaderViewModel()
            {
                Name = Path.GetFileNameWithoutExtension(parameter as string),
                Source = source
            };

            //  Add it to the shaders.
            shaders.Add(shaderViewModel);

            //  Set it as the active shader.
            CurrentShader = shaderViewModel;
        }

        private void DoFileSaveCommand(object parameter)
        {
            //  There must be a shader.
            if (CurrentShader == null)
                throw new ArgumentException("There is no current shader to save.");

            //  If there is no path, just use Save As.
            if (string.IsNullOrEmpty(CurrentShader.Path))
            {
                //  Call Save As.
                FileSaveAsCommand.DoExecute(null);

                //  We're done here.
                return;
            }

            //  Save the file.
            try
            {
                //  Open the file stream.
                using (FileStream stream = new FileStream(parameter as string, FileMode.Create))
                {
                    //  Create a writer and write the source.
                    TextWriter writer = new StreamWriter(stream);
                    writer.Write(CurrentShader.Source);
                    writer.Flush();
                    stream.Flush();
                }
            }
            catch (Exception e)
            {
                //  Re-throw the exception.
                throw new ApplicationException("Failed to save the shader.", e);
            }            
        }

        private void DoFileSaveAsCommand(object parameter)
        {
            //  The parameter must be a string.
            if (parameter is string == false)
                throw new ArgumentException("The parameter to the command must be a valid file path.");

            //  There must be a shader.
            if (CurrentShader == null)
                throw new ArgumentException("There is no current shader to save.");

            //  Save the file.
            try
            {
                //  Open the file stream.
                using (FileStream stream = new FileStream(parameter as string, FileMode.Create))
                {
                    //  Create a writer and write the source.
                    TextWriter writer = new StreamWriter(stream);
                    writer.Write(CurrentShader.Source);
                    writer.Flush();
                    stream.Flush();

                    //  We saved the file, so update the shader path.
                    CurrentShader.Path = parameter as string;
                }
            }
            catch (Exception e)
            {
                //  Re-throw the exception.
                throw new ApplicationException("Failed to save the shader.", e);
            }     
        }

        private void DoFileExitCommand(object parameter)
        {
        }

        private void DoViewShowOutputWindowCommand(object parameter)
        {
            //  Toggle the output window visibility.
            IsOutputWindowVisible = !IsOutputWindowVisible;
        }

        private void DoBuildCompileCommand(object parameter)
        {
            //  We must have a shader.
            if (CurrentShader == null)
                throw new ArgumentException("No shader is selected.");

            //  Compile the current shader.
            CurrentShader.CompileCommand.DoExecute(null);
        }

        private void DoBuildLinkCommand(object parameter)
        {
        }

        private void DoBuildBuildCommand(object parameter)
        {
        }


        private NotifyingProperty OpenGLVendorProperty =
          new NotifyingProperty("OpenGLVendor", typeof(string), default(string));

        public string OpenGLVendor
        {
            get { return (string)GetValue(OpenGLVendorProperty); }
            set { SetValue(OpenGLVendorProperty, value); }
        }
        
        private NotifyingProperty OpenGLRendererProperty =
          new NotifyingProperty("OpenGLRenderer", typeof(string), default(string));

        public string OpenGLRenderer
        {
            get { return (string)GetValue(OpenGLRendererProperty); }
            set { SetValue(OpenGLRendererProperty, value); }
        }

        private NotifyingProperty OpenGLVersionProperty =
          new NotifyingProperty("OpenGLVersion", typeof(string), default(string));

        public string OpenGLVersion
        {
            get { return (string)GetValue(OpenGLVersionProperty); }
            set { SetValue(OpenGLVersionProperty, value); }
        }
        
        private NotifyingProperty OpenGLShadingLanguageVersionProperty =
          new NotifyingProperty("OpenGLShadingLanguageVersion", typeof(string), default(string));

        public string OpenGLShadingLanguageVersion
        {
            get { return (string)GetValue(OpenGLShadingLanguageVersionProperty); }
            set { SetValue(OpenGLShadingLanguageVersionProperty, value); }
        }
        
        private NotifyingProperty CurrentShaderProperty =
          new NotifyingProperty("CurrentShader", typeof(ShaderViewModel), default(ShaderViewModel));

        public ShaderViewModel CurrentShader
        {
            get { return (ShaderViewModel)GetValue(CurrentShaderProperty); }
            set { SetValue(CurrentShaderProperty, value); }
        }

        
        private NotifyingProperty IsOutputWindowVisibleProperty =
          new NotifyingProperty("IsOutputWindowVisible", typeof(bool), default(bool));

        public bool IsOutputWindowVisible
        {
            get { return (bool)GetValue(IsOutputWindowVisibleProperty); }
            set { SetValue(IsOutputWindowVisibleProperty, value); }
        }
                

        private ObservableCollection<Shader.ShaderViewModel> shaders = new ObservableCollection<Shader.ShaderViewModel>();

        public ObservableCollection<Shader.ShaderViewModel> Shaders
        {
            get { return shaders; }
        }

        /// <summary>
        /// Gets the file new command.
        /// </summary>
        public Command FileNewCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the file open command.
        /// </summary>
        public Command FileOpenCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the file save command.
        /// </summary>
        public Command FileSaveCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the file save as command.
        /// </summary>
        public Command FileSaveAsCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the file exit command.
        /// </summary>
        public Command FileExitCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the view show output window command.
        /// </summary>
        public Command ViewShowOutputWindowCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the build compile command.
        /// </summary>
        public Command BuildCompileCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the build link command.
        /// </summary>
        public Command BuildLinkCommand
        {
            get;
            private set;
        }

        public Command BuildBuildCommand
        {
            get;
            private set;
        }
    }
}
