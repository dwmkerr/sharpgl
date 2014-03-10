namespace PolygonLoadingSample
{
    partial class FormPolygonLoadingSample
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
        	System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPolygonLoadingSample));
        	this.label1 = new System.Windows.Forms.Label();
        	this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
        	this.buttonLoad = new System.Windows.Forms.Button();
        	this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
        	this.openGLControl1 = new SharpGL.OpenGLControl();
        	this.menuStrip1 = new System.Windows.Forms.MenuStrip();
        	this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        	this.importPolygonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        	this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        	this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
        	this.freezeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        	this.unfreezeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        	this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
        	this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        	this.renderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        	this.wireframeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        	this.solidToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        	this.lightedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        	this.toolStripContainer1.ContentPanel.SuspendLayout();
        	this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
        	this.toolStripContainer1.SuspendLayout();
        	((System.ComponentModel.ISupportInitialize)(this.openGLControl1)).BeginInit();
        	this.menuStrip1.SuspendLayout();
        	this.SuspendLayout();
        	// 
        	// label1
        	// 
        	this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
        	        	        	| System.Windows.Forms.AnchorStyles.Right)));
        	this.label1.Location = new System.Drawing.Point(12, 394);
        	this.label1.Name = "label1";
        	this.label1.Size = new System.Drawing.Size(684, 35);
        	this.label1.TabIndex = 1;
        	this.label1.Text = "Press \'Load...\' to open any polygon file supported by SharpGL.";
        	// 
        	// openFileDialog1
        	// 
        	this.openFileDialog1.FileName = "openFileDialog1";
        	this.openFileDialog1.Filter = "Image Files|*.bmp;*.jpg;*.jpeg|All Files|*.*";
        	// 
        	// buttonLoad
        	// 
        	this.buttonLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        	this.buttonLoad.Location = new System.Drawing.Point(621, 425);
        	this.buttonLoad.Name = "buttonLoad";
        	this.buttonLoad.Size = new System.Drawing.Size(75, 23);
        	this.buttonLoad.TabIndex = 4;
        	this.buttonLoad.Text = "Load...";
        	this.buttonLoad.UseVisualStyleBackColor = true;
        	// 
        	// toolStripContainer1
        	// 
        	// 
        	// toolStripContainer1.ContentPanel
        	// 
        	this.toolStripContainer1.ContentPanel.Controls.Add(this.openGLControl1);
        	this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(708, 436);
        	this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
        	this.toolStripContainer1.Name = "toolStripContainer1";
        	this.toolStripContainer1.Size = new System.Drawing.Size(708, 460);
        	this.toolStripContainer1.TabIndex = 5;
        	this.toolStripContainer1.Text = "toolStripContainer1";
        	// 
        	// toolStripContainer1.TopToolStripPanel
        	// 
        	this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
        	// 
        	// openGLControl1
        	// 
        	this.openGLControl1.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.openGLControl1.DrawFPS = true;
        	this.openGLControl1.FrameRate = 28;
        	this.openGLControl1.Location = new System.Drawing.Point(0, 0);
        	this.openGLControl1.Name = "openGLControl1";
        	this.openGLControl1.RenderContextType = SharpGL.RenderContextType.NativeWindow;
        	this.openGLControl1.Size = new System.Drawing.Size(708, 436);
        	this.openGLControl1.TabIndex = 0;
        	this.openGLControl1.OpenGLInitialized += new System.EventHandler(this.openGLControl1_OpenGLInitialized);
        	this.openGLControl1.OpenGLDraw += new SharpGL.RenderEventHandler(this.openGLControl1_OpenGLDraw);
        	// 
        	// menuStrip1
        	// 
        	this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
        	this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        	        	        	this.fileToolStripMenuItem,
        	        	        	this.renderToolStripMenuItem});
        	this.menuStrip1.Location = new System.Drawing.Point(0, 0);
        	this.menuStrip1.Name = "menuStrip1";
        	this.menuStrip1.Size = new System.Drawing.Size(708, 24);
        	this.menuStrip1.TabIndex = 0;
        	this.menuStrip1.Text = "menuStrip1";
        	// 
        	// fileToolStripMenuItem
        	// 
        	this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        	        	        	this.importPolygonToolStripMenuItem,
        	        	        	this.clearToolStripMenuItem,
        	        	        	this.toolStripMenuItem1,
        	        	        	this.freezeAllToolStripMenuItem,
        	        	        	this.unfreezeAllToolStripMenuItem,
        	        	        	this.toolStripMenuItem2,
        	        	        	this.exitToolStripMenuItem});
        	this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
        	this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
        	this.fileToolStripMenuItem.Text = "&File";
        	// 
        	// importPolygonToolStripMenuItem
        	// 
        	this.importPolygonToolStripMenuItem.Name = "importPolygonToolStripMenuItem";
        	this.importPolygonToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
        	this.importPolygonToolStripMenuItem.Text = "Import Polygon...";
        	this.importPolygonToolStripMenuItem.Click += new System.EventHandler(this.importPolygonToolStripMenuItem_Click);
        	// 
        	// clearToolStripMenuItem
        	// 
        	this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
        	this.clearToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
        	this.clearToolStripMenuItem.Text = "&Clear";
        	this.clearToolStripMenuItem.Click += new System.EventHandler(this.ClearToolStripMenuItemClick);
        	// 
        	// toolStripMenuItem1
        	// 
        	this.toolStripMenuItem1.Name = "toolStripMenuItem1";
        	this.toolStripMenuItem1.Size = new System.Drawing.Size(163, 6);
        	// 
        	// freezeAllToolStripMenuItem
        	// 
        	this.freezeAllToolStripMenuItem.Name = "freezeAllToolStripMenuItem";
        	this.freezeAllToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
        	this.freezeAllToolStripMenuItem.Text = "Freeze All";
        	this.freezeAllToolStripMenuItem.Click += new System.EventHandler(this.freezeAllToolStripMenuItem_Click);
        	// 
        	// unfreezeAllToolStripMenuItem
        	// 
        	this.unfreezeAllToolStripMenuItem.Name = "unfreezeAllToolStripMenuItem";
        	this.unfreezeAllToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
        	this.unfreezeAllToolStripMenuItem.Text = "Unfreeze All";
        	this.unfreezeAllToolStripMenuItem.Click += new System.EventHandler(this.unfreezeAllToolStripMenuItem_Click);
        	// 
        	// toolStripMenuItem2
        	// 
        	this.toolStripMenuItem2.Name = "toolStripMenuItem2";
        	this.toolStripMenuItem2.Size = new System.Drawing.Size(163, 6);
        	// 
        	// exitToolStripMenuItem
        	// 
        	this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
        	this.exitToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
        	this.exitToolStripMenuItem.Text = "E&xit";
        	this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
        	// 
        	// renderToolStripMenuItem
        	// 
        	this.renderToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        	        	        	this.wireframeToolStripMenuItem,
        	        	        	this.solidToolStripMenuItem,
        	        	        	this.lightedToolStripMenuItem});
        	this.renderToolStripMenuItem.Name = "renderToolStripMenuItem";
        	this.renderToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
        	this.renderToolStripMenuItem.Text = "&Render";
        	// 
        	// wireframeToolStripMenuItem
        	// 
        	this.wireframeToolStripMenuItem.Checked = true;
        	this.wireframeToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
        	this.wireframeToolStripMenuItem.Name = "wireframeToolStripMenuItem";
        	this.wireframeToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
        	this.wireframeToolStripMenuItem.Text = "&Wireframe";
        	this.wireframeToolStripMenuItem.Click += new System.EventHandler(this.WireframeToolStripMenuItemClick);
        	// 
        	// solidToolStripMenuItem
        	// 
        	this.solidToolStripMenuItem.Name = "solidToolStripMenuItem";
        	this.solidToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
        	this.solidToolStripMenuItem.Text = "&Solid";
        	this.solidToolStripMenuItem.Click += new System.EventHandler(this.SolidToolStripMenuItemClick);
        	// 
        	// lightedToolStripMenuItem
        	// 
        	this.lightedToolStripMenuItem.Name = "lightedToolStripMenuItem";
        	this.lightedToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
        	this.lightedToolStripMenuItem.Text = "&Lighted";
        	this.lightedToolStripMenuItem.Click += new System.EventHandler(this.LightedToolStripMenuItemClick);
        	// 
        	// FormPolygonLoadingSample
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.ClientSize = new System.Drawing.Size(708, 460);
        	this.Controls.Add(this.toolStripContainer1);
        	this.Controls.Add(this.buttonLoad);
        	this.Controls.Add(this.label1);
        	this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        	this.MainMenuStrip = this.menuStrip1;
        	this.Name = "FormPolygonLoadingSample";
        	this.Text = "Polygon Loading Sample";
        	this.toolStripContainer1.ContentPanel.ResumeLayout(false);
        	this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
        	this.toolStripContainer1.TopToolStripPanel.PerformLayout();
        	this.toolStripContainer1.ResumeLayout(false);
        	this.toolStripContainer1.PerformLayout();
        	((System.ComponentModel.ISupportInitialize)(this.openGLControl1)).EndInit();
        	this.menuStrip1.ResumeLayout(false);
        	this.menuStrip1.PerformLayout();
        	this.ResumeLayout(false);
        }
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lightedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem solidToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wireframeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renderToolStripMenuItem;

        #endregion

        private SharpGL.OpenGLControl openGLControl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importPolygonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem freezeAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unfreezeAllToolStripMenuItem;

    }
}

