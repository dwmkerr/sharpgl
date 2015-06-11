using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpGL.Version;

namespace SharpGL.SceneComponent
{
    public partial class MyOpenGLControl : UserControl, ISupportInitialize
    {
        public MyOpenGLControl()
        {
            InitializeComponent();


        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

        }

        #region ISupportInitialize 成员
        
        /// <summary>
        /// Signals the object that initialization is starting.
        /// </summary>
        public virtual void BeginInit()
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Signals the object that initialization is complete.
        /// </summary>
        public virtual void EndInit()
        {
            InitialiseOpenGL();
        }

        /// <summary>
        /// create render context and then set basic opengl styles.
        /// </summary>
        protected virtual void InitialiseOpenGL()
        {
            CreateRenderContextProvider();
            SetBasicOpenGLStyles();
        }

        protected void SetBasicOpenGLStyles()
        {
            //  Set the most basic OpenGL styles.
            gl.ShadeModel(OpenGL.GL_SMOOTH);
            gl.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            gl.ClearDepth(1.0f);
            gl.Enable(OpenGL.GL_DEPTH_TEST);
            gl.DepthFunc(OpenGL.GL_LEQUAL);
            gl.Hint(OpenGL.GL_PERSPECTIVE_CORRECTION_HINT, OpenGL.GL_NICEST);
        }

        protected void CreateRenderContextProvider()
        {
            object parameter = null;

            //  Native render context providers need a little bit more attention.
            if (RenderContextType == RenderContextType.NativeWindow)
            {
                SetStyle(ControlStyles.OptimizedDoubleBuffer, false);
                parameter = Handle;
            }

            //  Create the render context.
            OpenGL gl = OpenGL.CreateGLContext(OpenGLVersion, RenderContextType, Width, Height, 32, parameter);

            this.gl = gl;
        }

        #endregion

        protected OpenGL gl;// = new OpenGL();
        /// <summary>
        /// Gets the OpenGL object.
        /// </summary>
        /// <value>The OpenGL.</value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public OpenGL OpenGL
        {
            get { return gl; }
        }

        /// <summary>
        /// The render context type.
        /// </summary>
        protected RenderContextType renderContextType = RenderContextType.DIBSection;
        /// <summary>
        /// Gets or sets the type of the render context.
        /// </summary>
        /// <value>
        /// The type of the render context.
        /// </value>
        [Description("The render context type."), Category("SharpGL")]
        public RenderContextType RenderContextType
        {
            get { return renderContextType; }
            set { renderContextType = value; }
        }

        /// <summary>
        /// The default desired OpenGL version.
        /// </summary>
        private OpenGLVersion openGLVersion = OpenGLVersion.OpenGL2_1;
        /// <summary>
        /// Gets or sets the desired OpenGL version.
        /// </summary>
        /// <value>
        /// The desired OpenGL version.
        /// </value>
        [Description("The desired OpenGL version for the control."), Category("SharpGL")]
        public OpenGLVersion OpenGLVersion
        {
            get { return openGLVersion; }
            set { openGLVersion = value; }
        }

        /// <summary>
        /// Gets or sets the render trigger.
        /// </summary>
        /// <value>
        /// The render trigger.
        /// </value>
        [Description("The render trigger - determines when rendering will occur."), Category("SharpGL")]
        public RenderTrigger RenderTrigger { get; set; }
    }
}
