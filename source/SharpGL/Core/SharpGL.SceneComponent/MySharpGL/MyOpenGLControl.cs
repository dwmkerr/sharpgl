using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
        void ISupportInitialize.BeginInit()
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Signals the object that initialization is complete.
        /// </summary>
        void ISupportInitialize.EndInit()
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
            throw new NotImplementedException();
        }

        #endregion

        protected OpenGL gl = new OpenGL();
    }
}
