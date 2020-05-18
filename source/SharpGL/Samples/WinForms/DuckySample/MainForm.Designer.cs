namespace DuckySample
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sceneControl = new SharpGL.SceneControl();
            ((System.ComponentModel.ISupportInitialize)(this.sceneControl)).BeginInit();
            this.SuspendLayout();
            // 
            // sceneControl
            // 
            this.sceneControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sceneControl.DrawFPS = true;
            this.sceneControl.Location = new System.Drawing.Point(0, 0);
            this.sceneControl.Name = "sceneControl";
            this.sceneControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.sceneControl.RenderContextType = SharpGL.RenderContextType.FBO;
            this.sceneControl.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.sceneControl.Size = new System.Drawing.Size(784, 561);
            this.sceneControl.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.sceneControl);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ducky Sample";
            ((System.ComponentModel.ISupportInitialize)(this.sceneControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SharpGL.SceneControl sceneControl;
    }
}

