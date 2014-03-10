using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apex.MVVM;
using ICSharpCode.AvalonEdit.Document;

namespace ShaderBuilder.Shader
{
    public class ShaderViewModel : ViewModel
    {
        public ShaderViewModel()
        {
            CompileCommand = new Command(DoCompileCommand, true);
        }

        private void DoCompileCommand(object parameter)
        {
            //  Clear the output.
            Output = string.Empty;

            //  Create a shader ref.
            List<SharpGL.SceneGraph.Shaders.Shader> shaders =
                new List<SharpGL.SceneGraph.Shaders.Shader>();
            
            //  Create the appropriate type.
            switch (ShaderType)
            {
                case ShaderType.FragmentShader:
                    {
                        var shader = new SharpGL.SceneGraph.Shaders.FragmentShader();
                        shader.CreateInContext(ApplicationState.Instance.OpenGL);
                        shader.SetSource(Source);
                        shaders.Add(shader);
                    }
                    break;
                case ShaderType.VertexShader:
                    {
                        var shader = new SharpGL.SceneGraph.Shaders.FragmentShader();
                        shader.CreateInContext(ApplicationState.Instance.OpenGL);
                        shader.SetSource(Source);
                        shaders.Add(shader);
                    }
                    break;
                case ShaderType.Shader:
                    {
                        //  Use the shader program to load the shader.
                        var program = new SharpGL.SceneGraph.Shaders.ShaderProgram();
                        program.CreateInContext(ApplicationState.Instance.OpenGL);
                        program.SetFullShaderSource(Source);

                        //  Add the attached shaders.
                        foreach (var shader in program.AttachedShaders)
                            shaders.Add(shader);
                    }
                    break;
                default:
                    throw new ArgumentException("Unknown shader type.");
            }

            //  Go through each shader.
            foreach (var shader in shaders)
            {
                CompilerOutput("Preparing to compile " + Name + " as " + ShaderType.ToString());
                
                 //  Compile the shader.
                shader.Compile();

                //  Set the output.
                CompilerOutput(shader.InfoLog);

                //  Set the status.
                CompilerOutput(shader.CompileStatus == true ? 
                    shader.Name + " compiled Successfully." :
                    shader.Name + " failed to compile.");

                //  Free the shader.
                shader.DestroyInContext(ApplicationState.Instance.OpenGL);
            }
        }

        private void CompilerOutput(string output)
        {
            Output += output + System.Environment.NewLine;
        }
        
        private NotifyingProperty NameProperty =
          new NotifyingProperty("Name", typeof(string), default(string));

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }
        
        private NotifyingProperty SourceProperty =
          new NotifyingProperty("Source", typeof(string), default(string));

        public string Source
        {
            get 
            { 
                SourceProperty.Value = Document != null ? Document.Text : string.Empty;
                return (string)GetValue(SourceProperty); 
            }
            set 
            { 
                SetValue(SourceProperty, value);
                Document = new TextDocument() { Text = value };
            }
        }

        
        private NotifyingProperty PathProperty =
          new NotifyingProperty("Path", typeof(string), default(string));

        public string Path
        {
            get { return (string)GetValue(PathProperty); }
            set { SetValue(PathProperty, value); }
        }
        
        private NotifyingProperty OutputProperty =
          new NotifyingProperty("Output", typeof(string), default(string));

        public string Output
        {
            get { return (string)GetValue(OutputProperty); }
            set { SetValue(OutputProperty, value); }
        }
        
        private NotifyingProperty ShaderTypeProperty =
          new NotifyingProperty("ShaderType", typeof(ShaderType), ShaderType.FragmentShader);

        public ShaderType ShaderType
        {
            get { return (ShaderType)GetValue(ShaderTypeProperty); }
            set { SetValue(ShaderTypeProperty, value); }
        }

        
        private NotifyingProperty DocumentProperty =
          new NotifyingProperty("Document", typeof(TextDocument), new TextDocument());

        public TextDocument Document
        {
            get { return (TextDocument)GetValue(DocumentProperty); }
            set { SetValue(DocumentProperty, value); }
        }
                

        public Command CompileCommand
        {
            get;
            private set;
        }
                
    }
}
