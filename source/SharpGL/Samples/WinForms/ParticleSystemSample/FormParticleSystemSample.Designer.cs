using SharpGL;
namespace ParticleSystemSample
{
    partial class FormParticleSystemSample
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormParticleSystemSample));
            this.buttonBurst = new System.Windows.Forms.Button();
            this.checkBoxGravity = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.openGLControl1 = new SharpGL.OpenGLControl();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonBurst
            // 
            this.buttonBurst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonBurst.Location = new System.Drawing.Point(8, 376);
            this.buttonBurst.Name = "buttonBurst";
            this.buttonBurst.Size = new System.Drawing.Size(75, 23);
            this.buttonBurst.TabIndex = 1;
            this.buttonBurst.Text = "&Burst!";
            this.buttonBurst.UseVisualStyleBackColor = true;
            this.buttonBurst.Click += new System.EventHandler(this.buttonBurst_Click);
            // 
            // checkBoxGravity
            // 
            this.checkBoxGravity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxGravity.AutoSize = true;
            this.checkBoxGravity.Location = new System.Drawing.Point(96, 384);
            this.checkBoxGravity.Name = "checkBoxGravity";
            this.checkBoxGravity.Size = new System.Drawing.Size(59, 17);
            this.checkBoxGravity.TabIndex = 2;
            this.checkBoxGravity.Text = "Gravity";
            this.checkBoxGravity.UseVisualStyleBackColor = true;
            this.checkBoxGravity.CheckedChanged += new System.EventHandler(this.checkBoxGravity_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 408);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(238, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Based on Jeff Molofee\'s great tutorial from NeHe!";
            // 
            // openGLControl1
            // 
            this.openGLControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.openGLControl1.DrawFPS = false;
            this.openGLControl1.FrameRate = 28;
            this.openGLControl1.Location = new System.Drawing.Point(10, 8);
            this.openGLControl1.Name = "openGLControl1";
            this.openGLControl1.RenderContextType = SharpGL.RenderContextType.FBO;
            this.openGLControl1.Size = new System.Drawing.Size(392, 344);
            this.openGLControl1.TabIndex = 0;
            this.openGLControl1.OpenGLInitialized += new System.EventHandler(this.openGLControl1_OpenGLInitialized);
            this.openGLControl1.OpenGLDraw += new RenderEventHandler(this.openGLControl1_OpenGLDraw);
            this.openGLControl1.Resize += new System.EventHandler(this.openGLControl1_Resize);
            // 
            // FormParticleSystemSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 438);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxGravity);
            this.Controls.Add(this.buttonBurst);
            this.Controls.Add(this.openGLControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormParticleSystemSample";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Particle Systems Sample";
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SharpGL.OpenGLControl openGLControl1;
        private System.Windows.Forms.Button buttonBurst;
        private System.Windows.Forms.CheckBox checkBoxGravity;
        private System.Windows.Forms.Label label1;
    }
}

