using SharpGLBase.Primitives;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SharpGLBase.Events
{
    // A delegate type for hooking up change notifications.
    public delegate void ModelSelectedEvent(object sender, ModelSelectedEventArgs e);
    public class ModelSelectedEventArgs : EventArgs
    {
        public Point Point { get; set; }
        public Model3DBase SelectedModel { get; set; }

        public ModelSelectedEventArgs(Point p, Model3DBase m)
        {
            Point = p;
            SelectedModel = m;
        }
    }
}
