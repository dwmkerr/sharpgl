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
using System.Windows.Shapes;

namespace FastGL
{
	/// <summary>
	/// Interaktionslogik für MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			fastGLControl1.Render += new FastGLControl.RenderHandler(fastGLControl1_Render);
		}

		void fastGLControl1_Render(object sender, FastGLControl.RenderEventArgs args)
		{
			args.GL.Begin(SharpGL.Enumerations.BeginMode.Triangles);
			
			args.GL.Color(1f, 0f, 0f);
			args.GL.Vertex(200, 10);

			args.GL.Color(0f, 1f, 0f);
			args.GL.Vertex(100, 200);

			args.GL.Color(0f, 0f, 1f);
			args.GL.Vertex(300, 200);
			
			args.GL.End();
		}
	}
}