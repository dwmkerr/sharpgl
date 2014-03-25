using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinFormsToolboxControls
{
    [ProvideToolboxControl("WinFormsToolboxControls", false)]
    public partial class ToolboxControl : UserControl
    {
        public ToolboxControl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(string.Format(System.Globalization.CultureInfo.CurrentUICulture, "We are inside {0}.button1_Click()", this.ToString()));
        }
    }
}
