namespace OpenGLInfo
{
    partial class OpenGLInfoForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.tabControlInfo = new System.Windows.Forms.TabControl();
            this.tabPageInfo = new System.Windows.Forms.TabPage();
            this.textBoxExtensions = new System.Windows.Forms.TextBox();
            this.textBoxVersion = new System.Windows.Forms.TextBox();
            this.textBoxRenderer = new System.Windows.Forms.TextBox();
            this.textBoxVendor = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPageExtensionFunctions = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.listViewExtensionFunctions = new System.Windows.Forms.ListView();
            this.columnHeaderExtensionFunction = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderAvailability = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControlInfo.SuspendLayout();
            this.tabPageInfo.SuspendLayout();
            this.tabPageExtensionFunctions.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(483, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "The OpenGL info application shows information about the OpenGL drivers, extension" +
    "s etc.";
            // 
            // tabControlInfo
            // 
            this.tabControlInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlInfo.Controls.Add(this.tabPageInfo);
            this.tabControlInfo.Controls.Add(this.tabPageExtensionFunctions);
            this.tabControlInfo.Location = new System.Drawing.Point(15, 40);
            this.tabControlInfo.Name = "tabControlInfo";
            this.tabControlInfo.SelectedIndex = 0;
            this.tabControlInfo.Size = new System.Drawing.Size(480, 349);
            this.tabControlInfo.TabIndex = 1;
            // 
            // tabPageInfo
            // 
            this.tabPageInfo.Controls.Add(this.textBoxExtensions);
            this.tabPageInfo.Controls.Add(this.textBoxVersion);
            this.tabPageInfo.Controls.Add(this.textBoxRenderer);
            this.tabPageInfo.Controls.Add(this.textBoxVendor);
            this.tabPageInfo.Controls.Add(this.label5);
            this.tabPageInfo.Controls.Add(this.label4);
            this.tabPageInfo.Controls.Add(this.label3);
            this.tabPageInfo.Controls.Add(this.label2);
            this.tabPageInfo.Location = new System.Drawing.Point(4, 22);
            this.tabPageInfo.Name = "tabPageInfo";
            this.tabPageInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInfo.Size = new System.Drawing.Size(472, 323);
            this.tabPageInfo.TabIndex = 0;
            this.tabPageInfo.Text = "OpenGL";
            this.tabPageInfo.UseVisualStyleBackColor = true;
            // 
            // textBoxExtensions
            // 
            this.textBoxExtensions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxExtensions.Location = new System.Drawing.Point(95, 99);
            this.textBoxExtensions.Multiline = true;
            this.textBoxExtensions.Name = "textBoxExtensions";
            this.textBoxExtensions.ReadOnly = true;
            this.textBoxExtensions.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxExtensions.Size = new System.Drawing.Size(358, 204);
            this.textBoxExtensions.TabIndex = 1;
            // 
            // textBoxVersion
            // 
            this.textBoxVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxVersion.Location = new System.Drawing.Point(95, 73);
            this.textBoxVersion.Name = "textBoxVersion";
            this.textBoxVersion.ReadOnly = true;
            this.textBoxVersion.Size = new System.Drawing.Size(358, 20);
            this.textBoxVersion.TabIndex = 1;
            // 
            // textBoxRenderer
            // 
            this.textBoxRenderer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRenderer.Location = new System.Drawing.Point(95, 47);
            this.textBoxRenderer.Name = "textBoxRenderer";
            this.textBoxRenderer.ReadOnly = true;
            this.textBoxRenderer.Size = new System.Drawing.Size(358, 20);
            this.textBoxRenderer.TabIndex = 1;
            // 
            // textBoxVendor
            // 
            this.textBoxVendor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxVendor.Location = new System.Drawing.Point(95, 21);
            this.textBoxVendor.Name = "textBoxVendor";
            this.textBoxVendor.ReadOnly = true;
            this.textBoxVendor.Size = new System.Drawing.Size(358, 20);
            this.textBoxVendor.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Extensions";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(47, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Version";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Renderer";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Vendor";
            // 
            // tabPageExtensionFunctions
            // 
            this.tabPageExtensionFunctions.Controls.Add(this.listViewExtensionFunctions);
            this.tabPageExtensionFunctions.Controls.Add(this.label6);
            this.tabPageExtensionFunctions.Location = new System.Drawing.Point(4, 22);
            this.tabPageExtensionFunctions.Name = "tabPageExtensionFunctions";
            this.tabPageExtensionFunctions.Size = new System.Drawing.Size(472, 323);
            this.tabPageExtensionFunctions.TabIndex = 1;
            this.tabPageExtensionFunctions.Text = "Extension Functions";
            this.tabPageExtensionFunctions.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.Location = new System.Drawing.Point(10, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(450, 19);
            this.label6.TabIndex = 0;
            this.label6.Text = "All extension functions are listed below.";
            // 
            // listViewExtensionFunctions
            // 
            this.listViewExtensionFunctions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewExtensionFunctions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderExtensionFunction,
            this.columnHeaderAvailability});
            this.listViewExtensionFunctions.Location = new System.Drawing.Point(13, 32);
            this.listViewExtensionFunctions.Name = "listViewExtensionFunctions";
            this.listViewExtensionFunctions.Size = new System.Drawing.Size(447, 279);
            this.listViewExtensionFunctions.TabIndex = 1;
            this.listViewExtensionFunctions.UseCompatibleStateImageBehavior = false;
            this.listViewExtensionFunctions.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderExtensionFunction
            // 
            this.columnHeaderExtensionFunction.Text = "Extension Functions";
            this.columnHeaderExtensionFunction.Width = 240;
            // 
            // columnHeaderAvailability
            // 
            this.columnHeaderAvailability.Text = "Availability";
            this.columnHeaderAvailability.Width = 180;
            // 
            // OpenGLInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 401);
            this.Controls.Add(this.tabControlInfo);
            this.Controls.Add(this.label1);
            this.Name = "OpenGLInfoForm";
            this.Text = "OpenGL Info";
            this.Load += new System.EventHandler(this.OpenGLInfoForm_Load);
            this.tabControlInfo.ResumeLayout(false);
            this.tabPageInfo.ResumeLayout(false);
            this.tabPageInfo.PerformLayout();
            this.tabPageExtensionFunctions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControlInfo;
        private System.Windows.Forms.TabPage tabPageInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxExtensions;
        private System.Windows.Forms.TextBox textBoxVersion;
        private System.Windows.Forms.TextBox textBoxRenderer;
        private System.Windows.Forms.TextBox textBoxVendor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage tabPageExtensionFunctions;
        private System.Windows.Forms.ListView listViewExtensionFunctions;
        private System.Windows.Forms.ColumnHeader columnHeaderExtensionFunction;
        private System.Windows.Forms.ColumnHeader columnHeaderAvailability;
        private System.Windows.Forms.Label label6;
    }
}

