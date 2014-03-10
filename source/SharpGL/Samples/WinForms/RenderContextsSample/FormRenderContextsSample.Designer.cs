using SharpGL;
namespace RenderContextsSample
{
    partial class FormRenderContextsSample
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRenderContextsSample));
            this.labelVendor1 = new System.Windows.Forms.Label();
            this.labelVendor3 = new System.Windows.Forms.Label();
            this.labelVersion1 = new System.Windows.Forms.Label();
            this.labelVersion3 = new System.Windows.Forms.Label();
            this.labelRenderer1 = new System.Windows.Forms.Label();
            this.labelRenderer3 = new System.Windows.Forms.Label();
            this.labelExtensions1 = new System.Windows.Forms.Label();
            this.labelExtensions3 = new System.Windows.Forms.Label();
            this.openGLControl3 = new SharpGL.OpenGLControl();
            this.openGLControl1 = new SharpGL.OpenGLControl();
            this.tabControlProviders = new System.Windows.Forms.TabControl();
            this.tabPageDIBSection = new System.Windows.Forms.TabPage();
            this.tabPageNativeWindow = new System.Windows.Forms.TabPage();
            this.tabPageHiddenWindow = new System.Windows.Forms.TabPage();
            this.tabPageFBO = new System.Windows.Forms.TabPage();
            this.labelVersion2 = new System.Windows.Forms.Label();
            this.labelRenderer2 = new System.Windows.Forms.Label();
            this.labelExtensions2 = new System.Windows.Forms.Label();
            this.labelVendor2 = new System.Windows.Forms.Label();
            this.openGLControl2 = new SharpGL.OpenGLControl();
            this.openGLControlNativeWindow = new SharpGL.OpenGLControl();
            this.labelVendor = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl1)).BeginInit();
            this.tabControlProviders.SuspendLayout();
            this.tabPageDIBSection.SuspendLayout();
            this.tabPageNativeWindow.SuspendLayout();
            this.tabPageHiddenWindow.SuspendLayout();
            this.tabPageFBO.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControlNativeWindow)).BeginInit();
            this.SuspendLayout();
            // 
            // labelVendor1
            // 
            this.labelVendor1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelVendor1.Location = new System.Drawing.Point(3, 349);
            this.labelVendor1.Name = "labelVendor1";
            this.labelVendor1.Size = new System.Drawing.Size(250, 17);
            this.labelVendor1.TabIndex = 4;
            this.labelVendor1.Text = "Vendor";
            // 
            // labelVendor3
            // 
            this.labelVendor3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelVendor3.Location = new System.Drawing.Point(3, 348);
            this.labelVendor3.Name = "labelVendor3";
            this.labelVendor3.Size = new System.Drawing.Size(250, 17);
            this.labelVendor3.TabIndex = 4;
            this.labelVendor3.Text = "Vendor";
            // 
            // labelVersion1
            // 
            this.labelVersion1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelVersion1.Location = new System.Drawing.Point(3, 366);
            this.labelVersion1.Name = "labelVersion1";
            this.labelVersion1.Size = new System.Drawing.Size(250, 17);
            this.labelVersion1.TabIndex = 4;
            this.labelVersion1.Text = "Version";
            // 
            // labelVersion3
            // 
            this.labelVersion3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelVersion3.Location = new System.Drawing.Point(3, 365);
            this.labelVersion3.Name = "labelVersion3";
            this.labelVersion3.Size = new System.Drawing.Size(250, 17);
            this.labelVersion3.TabIndex = 4;
            this.labelVersion3.Text = "Version";
            // 
            // labelRenderer1
            // 
            this.labelRenderer1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelRenderer1.Location = new System.Drawing.Point(3, 383);
            this.labelRenderer1.Name = "labelRenderer1";
            this.labelRenderer1.Size = new System.Drawing.Size(250, 17);
            this.labelRenderer1.TabIndex = 4;
            this.labelRenderer1.Text = "Renderer";
            // 
            // labelRenderer3
            // 
            this.labelRenderer3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelRenderer3.Location = new System.Drawing.Point(3, 382);
            this.labelRenderer3.Name = "labelRenderer3";
            this.labelRenderer3.Size = new System.Drawing.Size(250, 17);
            this.labelRenderer3.TabIndex = 4;
            this.labelRenderer3.Text = "Renderer";
            // 
            // labelExtensions1
            // 
            this.labelExtensions1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelExtensions1.Location = new System.Drawing.Point(3, 400);
            this.labelExtensions1.Name = "labelExtensions1";
            this.labelExtensions1.Size = new System.Drawing.Size(580, 121);
            this.labelExtensions1.TabIndex = 4;
            this.labelExtensions1.Text = "Extensions";
            // 
            // labelExtensions3
            // 
            this.labelExtensions3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelExtensions3.Location = new System.Drawing.Point(3, 399);
            this.labelExtensions3.Name = "labelExtensions3";
            this.labelExtensions3.Size = new System.Drawing.Size(250, 121);
            this.labelExtensions3.TabIndex = 4;
            this.labelExtensions3.Text = "Extensions";
            // 
            // openGLControl3
            // 
            this.openGLControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.openGLControl3.DrawFPS = true;
            this.openGLControl3.FrameRate = 28;
            this.openGLControl3.Location = new System.Drawing.Point(3, 3);
            this.openGLControl3.Name = "openGLControl3";
            this.openGLControl3.RenderContextType = SharpGL.RenderContextType.FBO;
            this.openGLControl3.Size = new System.Drawing.Size(747, 342);
            this.openGLControl3.TabIndex = 0;
            this.openGLControl3.OpenGLInitialized += new System.EventHandler(this.openGLControl3_OpenGLInitialized);
            this.openGLControl3.OpenGLDraw += new RenderEventHandler(this.openGLControl3_OpenGLDraw);
            // 
            // openGLControl1
            // 
            this.openGLControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.openGLControl1.DrawFPS = true;
            this.openGLControl1.FrameRate = 28;
            this.openGLControl1.Location = new System.Drawing.Point(3, 3);
            this.openGLControl1.Name = "openGLControl1";
            this.openGLControl1.RenderContextType = SharpGL.RenderContextType.DIBSection;
            this.openGLControl1.Size = new System.Drawing.Size(741, 343);
            this.openGLControl1.TabIndex = 0;
            this.openGLControl1.OpenGLDraw += new RenderEventHandler(this.openGLControl1_OpenGLDraw);
            // 
            // tabControlProviders
            // 
            this.tabControlProviders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlProviders.Controls.Add(this.tabPageDIBSection);
            this.tabControlProviders.Controls.Add(this.tabPageNativeWindow);
            this.tabControlProviders.Controls.Add(this.tabPageHiddenWindow);
            this.tabControlProviders.Controls.Add(this.tabPageFBO);
            this.tabControlProviders.Location = new System.Drawing.Point(12, 12);
            this.tabControlProviders.Name = "tabControlProviders";
            this.tabControlProviders.SelectedIndex = 0;
            this.tabControlProviders.Size = new System.Drawing.Size(761, 550);
            this.tabControlProviders.TabIndex = 5;
            // 
            // tabPageDIBSection
            // 
            this.tabPageDIBSection.Controls.Add(this.openGLControl1);
            this.tabPageDIBSection.Controls.Add(this.labelVendor1);
            this.tabPageDIBSection.Controls.Add(this.labelVersion1);
            this.tabPageDIBSection.Controls.Add(this.labelRenderer1);
            this.tabPageDIBSection.Controls.Add(this.labelExtensions1);
            this.tabPageDIBSection.Location = new System.Drawing.Point(4, 22);
            this.tabPageDIBSection.Name = "tabPageDIBSection";
            this.tabPageDIBSection.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDIBSection.Size = new System.Drawing.Size(753, 524);
            this.tabPageDIBSection.TabIndex = 0;
            this.tabPageDIBSection.Text = "DIB Section";
            this.tabPageDIBSection.UseVisualStyleBackColor = true;
            // 
            // tabPageNativeWindow
            // 
            this.tabPageNativeWindow.Controls.Add(this.openGLControlNativeWindow);
            this.tabPageNativeWindow.Controls.Add(this.labelVendor);
            this.tabPageNativeWindow.Controls.Add(this.label2);
            this.tabPageNativeWindow.Controls.Add(this.label3);
            this.tabPageNativeWindow.Controls.Add(this.label4);
            this.tabPageNativeWindow.Location = new System.Drawing.Point(4, 22);
            this.tabPageNativeWindow.Name = "tabPageNativeWindow";
            this.tabPageNativeWindow.Size = new System.Drawing.Size(753, 524);
            this.tabPageNativeWindow.TabIndex = 1;
            this.tabPageNativeWindow.Text = "Native Window";
            this.tabPageNativeWindow.UseVisualStyleBackColor = true;
            // 
            // tabPageHiddenWindow
            // 
            this.tabPageHiddenWindow.Controls.Add(this.openGLControl2);
            this.tabPageHiddenWindow.Controls.Add(this.labelVendor2);
            this.tabPageHiddenWindow.Controls.Add(this.labelVersion2);
            this.tabPageHiddenWindow.Controls.Add(this.labelRenderer2);
            this.tabPageHiddenWindow.Controls.Add(this.labelExtensions2);
            this.tabPageHiddenWindow.Location = new System.Drawing.Point(4, 22);
            this.tabPageHiddenWindow.Name = "tabPageHiddenWindow";
            this.tabPageHiddenWindow.Size = new System.Drawing.Size(753, 524);
            this.tabPageHiddenWindow.TabIndex = 2;
            this.tabPageHiddenWindow.Text = "Hidden Window";
            this.tabPageHiddenWindow.UseVisualStyleBackColor = true;
            // 
            // tabPageFBO
            // 
            this.tabPageFBO.Controls.Add(this.openGLControl3);
            this.tabPageFBO.Controls.Add(this.labelVendor3);
            this.tabPageFBO.Controls.Add(this.labelExtensions3);
            this.tabPageFBO.Controls.Add(this.labelVersion3);
            this.tabPageFBO.Controls.Add(this.labelRenderer3);
            this.tabPageFBO.Location = new System.Drawing.Point(4, 22);
            this.tabPageFBO.Name = "tabPageFBO";
            this.tabPageFBO.Size = new System.Drawing.Size(753, 524);
            this.tabPageFBO.TabIndex = 3;
            this.tabPageFBO.Text = "FBO";
            this.tabPageFBO.UseVisualStyleBackColor = true;
            // 
            // labelVersion2
            // 
            this.labelVersion2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelVersion2.Location = new System.Drawing.Point(3, 366);
            this.labelVersion2.Name = "labelVersion2";
            this.labelVersion2.Size = new System.Drawing.Size(250, 17);
            this.labelVersion2.TabIndex = 4;
            this.labelVersion2.Text = "Version";
            // 
            // labelRenderer2
            // 
            this.labelRenderer2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelRenderer2.Location = new System.Drawing.Point(3, 383);
            this.labelRenderer2.Name = "labelRenderer2";
            this.labelRenderer2.Size = new System.Drawing.Size(250, 17);
            this.labelRenderer2.TabIndex = 4;
            this.labelRenderer2.Text = "Renderer";
            // 
            // labelExtensions2
            // 
            this.labelExtensions2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelExtensions2.Location = new System.Drawing.Point(3, 400);
            this.labelExtensions2.Name = "labelExtensions2";
            this.labelExtensions2.Size = new System.Drawing.Size(250, 121);
            this.labelExtensions2.TabIndex = 4;
            this.labelExtensions2.Text = "Extensions";
            // 
            // labelVendor2
            // 
            this.labelVendor2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelVendor2.Location = new System.Drawing.Point(3, 349);
            this.labelVendor2.Name = "labelVendor2";
            this.labelVendor2.Size = new System.Drawing.Size(250, 17);
            this.labelVendor2.TabIndex = 4;
            this.labelVendor2.Text = "Vendor";
            // 
            // openGLControl2
            // 
            this.openGLControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.openGLControl2.DrawFPS = true;
            this.openGLControl2.FrameRate = 28;
            this.openGLControl2.Location = new System.Drawing.Point(3, 3);
            this.openGLControl2.Name = "openGLControl2";
            this.openGLControl2.RenderContextType = SharpGL.RenderContextType.HiddenWindow;
            this.openGLControl2.Size = new System.Drawing.Size(747, 343);
            this.openGLControl2.TabIndex = 0;
            this.openGLControl2.OpenGLDraw += new RenderEventHandler(this.openGLControl2_OpenGLDraw);
            // 
            // openGLControlNativeWindow
            // 
            this.openGLControlNativeWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.openGLControlNativeWindow.DrawFPS = true;
            this.openGLControlNativeWindow.FrameRate = 28;
            this.openGLControlNativeWindow.Location = new System.Drawing.Point(3, 3);
            this.openGLControlNativeWindow.Name = "openGLControlNativeWindow";
            this.openGLControlNativeWindow.RenderContextType = SharpGL.RenderContextType.NativeWindow;
            this.openGLControlNativeWindow.Size = new System.Drawing.Size(747, 343);
            this.openGLControlNativeWindow.TabIndex = 5;
            this.openGLControlNativeWindow.OpenGLDraw += new RenderEventHandler(this.openGLControlNativeWindow_OpenGLDraw);
            // 
            // labelVendor
            // 
            this.labelVendor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelVendor.Location = new System.Drawing.Point(3, 349);
            this.labelVendor.Name = "labelVendor";
            this.labelVendor.Size = new System.Drawing.Size(250, 17);
            this.labelVendor.TabIndex = 8;
            this.labelVendor.Text = "Vendor";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.Location = new System.Drawing.Point(3, 366);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(250, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "Version";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.Location = new System.Drawing.Point(3, 383);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(250, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Renderer";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.Location = new System.Drawing.Point(3, 400);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(250, 121);
            this.label4.TabIndex = 7;
            this.label4.Text = "Extensions";
            // 
            // FormRenderContextsSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 574);
            this.Controls.Add(this.tabControlProviders);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormRenderContextsSample";
            this.Text = "Render Context Providers Sample";
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl1)).EndInit();
            this.tabControlProviders.ResumeLayout(false);
            this.tabPageDIBSection.ResumeLayout(false);
            this.tabPageNativeWindow.ResumeLayout(false);
            this.tabPageHiddenWindow.ResumeLayout(false);
            this.tabPageFBO.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControlNativeWindow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private OpenGLControl openGLControl1;
        private OpenGLControl openGLControl3;
        private System.Windows.Forms.Label labelVendor1;
        private System.Windows.Forms.Label labelVendor3;
        private System.Windows.Forms.Label labelVersion1;
        private System.Windows.Forms.Label labelVersion3;
        private System.Windows.Forms.Label labelRenderer1;
        private System.Windows.Forms.Label labelRenderer3;
        private System.Windows.Forms.Label labelExtensions1;
        private System.Windows.Forms.Label labelExtensions3;
        private System.Windows.Forms.TabControl tabControlProviders;
        private System.Windows.Forms.TabPage tabPageDIBSection;
        private System.Windows.Forms.TabPage tabPageNativeWindow;
        private System.Windows.Forms.TabPage tabPageHiddenWindow;
        private OpenGLControl openGLControl2;
        private System.Windows.Forms.Label labelVendor2;
        private System.Windows.Forms.Label labelVersion2;
        private System.Windows.Forms.Label labelRenderer2;
        private System.Windows.Forms.Label labelExtensions2;
        private System.Windows.Forms.TabPage tabPageFBO;
        private OpenGLControl openGLControlNativeWindow;
        private System.Windows.Forms.Label labelVendor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

