namespace ExtensionsSample
{
    partial class ExtensionsSampleForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExtensionsSampleForm));
            this.openGLControl1 = new SharpGL.OpenGLControl();
            this.checkBoxDrawBumps = new System.Windows.Forms.CheckBox();
            this.checkBoxDrawColor = new System.Windows.Forms.CheckBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // openGLControl1
            // 
            this.openGLControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.openGLControl1.DrawFPS = true;
            this.openGLControl1.FrameRate = 20;
            this.openGLControl1.Location = new System.Drawing.Point(12, 12);
            this.openGLControl1.Name = "openGLControl1";
            this.openGLControl1.RenderContextType = SharpGL.RenderContextType.FBO;
            this.openGLControl1.Size = new System.Drawing.Size(740, 364);
            this.openGLControl1.TabIndex = 0;
            this.openGLControl1.OpenGLInitialized += new System.EventHandler(this.openGLControl1_OpenGLInitialized);
            this.openGLControl1.OpenGLDraw += new SharpGL.RenderEventHandler(this.openGLControl1_OpenGLDraw);
            this.openGLControl1.Resized += new System.EventHandler(this.openGLControl1_Resized);
            // 
            // checkBoxDrawBumps
            // 
            this.checkBoxDrawBumps.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxDrawBumps.AutoSize = true;
            this.checkBoxDrawBumps.Checked = true;
            this.checkBoxDrawBumps.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDrawBumps.Location = new System.Drawing.Point(96, 395);
            this.checkBoxDrawBumps.Name = "checkBoxDrawBumps";
            this.checkBoxDrawBumps.Size = new System.Drawing.Size(86, 17);
            this.checkBoxDrawBumps.TabIndex = 1;
            this.checkBoxDrawBumps.Text = "Draw Bumps";
            this.checkBoxDrawBumps.UseVisualStyleBackColor = true;
            this.checkBoxDrawBumps.CheckedChanged += new System.EventHandler(this.checkBoxDrawBumps_CheckedChanged);
            // 
            // checkBoxDrawColor
            // 
            this.checkBoxDrawColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxDrawColor.AutoSize = true;
            this.checkBoxDrawColor.Checked = true;
            this.checkBoxDrawColor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDrawColor.Location = new System.Drawing.Point(12, 395);
            this.checkBoxDrawColor.Name = "checkBoxDrawColor";
            this.checkBoxDrawColor.Size = new System.Drawing.Size(78, 17);
            this.checkBoxDrawColor.TabIndex = 1;
            this.checkBoxDrawColor.Text = "Draw Color";
            this.checkBoxDrawColor.UseVisualStyleBackColor = true;
            this.checkBoxDrawColor.CheckedChanged += new System.EventHandler(this.checkBoxDrawColor_CheckedChanged);
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabel1.Location = new System.Drawing.Point(424, 399);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(328, 13);
            this.linkLabel1.TabIndex = 2;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "http://www.paulsprojects.net/tutorials/simplebump/simplebump.html";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(345, 399);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Original Code:";
            // 
            // ExtensionsSampleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 424);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.checkBoxDrawColor);
            this.Controls.Add(this.checkBoxDrawBumps);
            this.Controls.Add(this.openGLControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ExtensionsSampleForm";
            this.Text = "Extensions Sample for SharpGL";
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SharpGL.OpenGLControl openGLControl1;
        private System.Windows.Forms.CheckBox checkBoxDrawBumps;
        private System.Windows.Forms.CheckBox checkBoxDrawColor;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label1;
    }
}

