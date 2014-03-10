using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL;
using System.IO;
using System.Xml;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;

namespace ShaderBuilder
{
    public class ApplicationState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationState"/> class.
        /// </summary>
        private ApplicationState()
        {
            //  Create the OpenGL instance.
            OpenGL = new OpenGL();

            //  Initialise the OpenGL instance.
            OpenGL.Create(RenderContextType.FBO,
                800, 600, 24, null);
        }

        /// <summary>
        /// The singleton instance.
        /// </summary>
        private static ApplicationState instance = new ApplicationState();

        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static ApplicationState Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Gets or sets the OpenGL.
        /// </summary>
        /// <value>
        /// The OpenGL.
        /// </value>
        public OpenGL OpenGL
        {
            get;
            set;
        }
    }
}
