using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpGL;
using System.Reflection;
using SharpGL.Version;

namespace OpenGLInfo
{
    /// <summary>
    /// The OpenGL Info form.
    /// </summary>
    public partial class OpenGLInfoForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGLInfoForm"/> class.
        /// </summary>
        public OpenGLInfoForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Load event of the OpenGLInfoForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OpenGLInfoForm_Load(object sender, EventArgs e)
        {
            //  Create the OpenGL instance.
            gl.Create(OpenGLVersion.OpenGL4_4, RenderContextType.FBO, 1, 1, 32, null);

            //  Populate the OpenGL info.
            PopulateOpenGLInfo();
        }

        /// <summary>
        /// Populates the open GL info.
        /// </summary>
        private void PopulateOpenGLInfo()
        {
            //  The OpenGL page.
            textBoxVendor.Text = gl.Vendor;
            textBoxRenderer.Text = gl.Renderer;
            textBoxVersion.Text = gl.Version;

            //  Put each extension on a new line.
            string[] extensions = gl.Extensions.Split(' ');
            textBoxExtensions.Text = string.Join(System.Environment.NewLine, extensions);

            //  Get each member of the OpenGL object.
            var members = gl.GetType().GetMembers(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Instance);

            //  Go through each member.
            foreach (var member in members)
            {
                if (member.Name.Substring(0, 2) == "gl" && member.MemberType == MemberTypes.NestedType)
                {
                    string name = member.Name;
                    bool supported = gl.IsExtensionFunctionSupported(name);

                    ListViewItem item = new ListViewItem();
                    item.Text = name;
                    item.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = supported ? "Supported" : "Not Supported" });
                    listViewExtensionFunctions.Items.Add(item);
                }
            }
        }

        /// <summary>
        /// The OpenGL Instance.
        /// </summary>
        private OpenGL gl = new OpenGL();
    }
}
