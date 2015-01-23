using System;
using System.ComponentModel;
using System.Windows.Forms;
using SharpGL.SceneGraph;

namespace SharpGL
{
   	/// <summary>
	/// The SceneControl is an OpenGLControl that contains and draws a Scene object.
    /// </summary>
    [System.Drawing.ToolboxBitmap(typeof(SceneControl), "SharpGL.png")]
	public class SceneControl : SharpGL.OpenGLControl
	{
		private System.ComponentModel.IContainer components = null;

        public SceneControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.UserPaint, true);

            //  Initialise the scene.
            SceneGraph.Helpers.SceneHelper.InitialiseModelingScene(Scene);
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// OpenGLCtrl
			// 
			this.Name = "OpenGLCtrl";

		}
		#endregion

		protected override void OnPaint(PaintEventArgs e)
        {
            //  Start the stopwatch so that we can time the rendering.
            stopwatch.Restart();
            
			//	Make sure it's our instance of openSharpGL that's active.
			OpenGL.MakeCurrent();

			//	Do the scene drawing.
			scene.Draw();

			//	If there is a draw handler, then call it.
            DoOpenGLDraw(new RenderEventArgs(e.Graphics));

            //  Draw the FPS.
            if (DrawFPS)
            {
                OpenGL.DrawText(5, 5, 1.0f, 0.0f, 0.0f, "Courier New", 12.0f,
                    string.Format("Draw Time: {0:0.0000} ms ~ {1:0.0} FPS", frameTime, 1000.0 / frameTime));
                OpenGL.Flush();
            }

            //	Blit our offscreen bitmap.
            IntPtr handleDeviceContext = e.Graphics.GetHdc();
            OpenGL.Blit(handleDeviceContext);
            e.Graphics.ReleaseHdc(handleDeviceContext);

            //	If's there's a GDI draw handler, then call it.
            DoGDIDraw(new RenderEventArgs(e.Graphics));

            //  Stop the stopwatch.
            stopwatch.Stop();

            //  Store the frame time.
            frameTime = stopwatch.Elapsed.TotalMilliseconds;   
		}


        /// <summary>
        /// Raises the <see cref="E:PaintBackground"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs"/> instance containing the event data.</param>
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			//	We override this, and don't call the base, i.e we don't paint
			//	the background.
		}

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
		protected override void OnSizeChanged(EventArgs e)
		{
			//  Don't call the base- we handle sizing ourselves.

			//	OpenGL needs to resize the viewport.
            OpenGL.SetDimensions(Width, Height);
			scene.Resize(Width, Height);

			Invalidate();
		}
                		
		/// <summary>
		/// This is the scene itself.
		/// </summary>
		private Scene scene = new Scene();
        
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Scene Scene 
		{
			get {return scene;}
			set {scene = value;}
		}
	}
}
