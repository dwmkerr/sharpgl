//  Copyright (c) 2007 - Dave Kerr
//  http://www.dopecode.co.uk
//
//  
//  This file is part of SharpGL.  
//
//  SharpGL is free software; you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation; either version 3 of the License, or
//  (at your option) any later version.
//
//  SharpGL is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see http://www.gnu.org/licenses/.
//
//  If you are using this code for commerical purposes then you MUST
//  purchase a license. See http://www.dopecode.co.uk for details.

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using SharpGL.SceneGraph;
using SharpGL.SceneGraph.ParticleSystems;


namespace SceneBuilder
{
	/// <summary>
	/// Summary description for PolygonBuilder.
	/// </summary>
	public class ParticleSystemBuilderForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBoxIdentity;
		private System.Windows.Forms.TextBox textBoxName;
		private System.Windows.Forms.GroupBox groupBoxType;
		private System.Windows.Forms.GroupBox groupBoxProperties;
		private SharpGL.SceneControl openGLCtrlParticlebuild;
		private System.Windows.Forms.RadioButton radioButtonType1;
		private System.Windows.Forms.RadioButton radioButtonType2;
		private System.Windows.Forms.RadioButton radioButtonType4;
		private System.Windows.Forms.RadioButton radioButtonType3;
		private System.Windows.Forms.RadioButton radioButtonType5;
		private System.Windows.Forms.Label labelTypes;
		private System.Windows.Forms.PropertyGrid propertyGridSystem;
		private System.Timers.Timer timerSystemTick;
		private System.Windows.Forms.Button buttonOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ParticleSystemBuilderForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			particleSystem.Initialise(100);
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.groupBoxIdentity = new System.Windows.Forms.GroupBox();
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.groupBoxType = new System.Windows.Forms.GroupBox();
			this.labelTypes = new System.Windows.Forms.Label();
			this.radioButtonType5 = new System.Windows.Forms.RadioButton();
			this.radioButtonType3 = new System.Windows.Forms.RadioButton();
			this.radioButtonType4 = new System.Windows.Forms.RadioButton();
			this.radioButtonType2 = new System.Windows.Forms.RadioButton();
			this.radioButtonType1 = new System.Windows.Forms.RadioButton();
			this.groupBoxProperties = new System.Windows.Forms.GroupBox();
			this.propertyGridSystem = new System.Windows.Forms.PropertyGrid();
            this.openGLCtrlParticlebuild = new SharpGL.SceneControl();
			this.timerSystemTick = new System.Timers.Timer();
			this.buttonOK = new System.Windows.Forms.Button();
			this.groupBoxIdentity.SuspendLayout();
			this.groupBoxType.SuspendLayout();
			this.groupBoxProperties.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.timerSystemTick)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBoxIdentity
			// 
			this.groupBoxIdentity.Controls.AddRange(new System.Windows.Forms.Control[] {
																						   this.textBoxName});
			this.groupBoxIdentity.Location = new System.Drawing.Point(8, 8);
			this.groupBoxIdentity.Name = "groupBoxIdentity";
			this.groupBoxIdentity.Size = new System.Drawing.Size(216, 56);
			this.groupBoxIdentity.TabIndex = 1;
			this.groupBoxIdentity.TabStop = false;
			this.groupBoxIdentity.Text = "Name";
			// 
			// textBoxName
			// 
			this.textBoxName.Location = new System.Drawing.Point(8, 24);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(200, 21);
			this.textBoxName.TabIndex = 0;
			this.textBoxName.Text = "System Name";
			// 
			// groupBoxType
			// 
			this.groupBoxType.Controls.AddRange(new System.Windows.Forms.Control[] {
																					   this.labelTypes,
																					   this.radioButtonType5,
																					   this.radioButtonType3,
																					   this.radioButtonType4,
																					   this.radioButtonType2,
																					   this.radioButtonType1});
			this.groupBoxType.Location = new System.Drawing.Point(8, 64);
			this.groupBoxType.Name = "groupBoxType";
			this.groupBoxType.Size = new System.Drawing.Size(216, 192);
			this.groupBoxType.TabIndex = 2;
			this.groupBoxType.TabStop = false;
			this.groupBoxType.Text = "Type";
			// 
			// labelTypes
			// 
			this.labelTypes.Location = new System.Drawing.Point(8, 24);
			this.labelTypes.Name = "labelTypes";
			this.labelTypes.Size = new System.Drawing.Size(200, 40);
			this.labelTypes.TabIndex = 5;
			this.labelTypes.Text = "Warning: Changing the System Type resets ALL system properties!";
			// 
			// radioButtonType5
			// 
			this.radioButtonType5.Location = new System.Drawing.Point(8, 160);
			this.radioButtonType5.Name = "radioButtonType5";
			this.radioButtonType5.Size = new System.Drawing.Size(200, 24);
			this.radioButtonType5.TabIndex = 4;
			this.radioButtonType5.Text = "Fire";
			this.radioButtonType5.CheckedChanged += new System.EventHandler(this.radioButtonType5_CheckedChanged);
			// 
			// radioButtonType3
			// 
			this.radioButtonType3.Location = new System.Drawing.Point(8, 112);
			this.radioButtonType3.Name = "radioButtonType3";
			this.radioButtonType3.Size = new System.Drawing.Size(200, 24);
			this.radioButtonType3.TabIndex = 3;
			this.radioButtonType3.Text = "Cloud of Dust";
			this.radioButtonType3.CheckedChanged += new System.EventHandler(this.radioButtonType3_CheckedChanged);
			// 
			// radioButtonType4
			// 
			this.radioButtonType4.Location = new System.Drawing.Point(8, 136);
			this.radioButtonType4.Name = "radioButtonType4";
			this.radioButtonType4.Size = new System.Drawing.Size(200, 24);
			this.radioButtonType4.TabIndex = 2;
			this.radioButtonType4.Text = "Electricity";
			this.radioButtonType4.CheckedChanged += new System.EventHandler(this.radioButtonType4_CheckedChanged);
			// 
			// radioButtonType2
			// 
			this.radioButtonType2.Location = new System.Drawing.Point(8, 88);
			this.radioButtonType2.Name = "radioButtonType2";
			this.radioButtonType2.Size = new System.Drawing.Size(200, 24);
			this.radioButtonType2.TabIndex = 1;
			this.radioButtonType2.Text = "Bolt";
			this.radioButtonType2.CheckedChanged += new System.EventHandler(this.radioButtonType2_CheckedChanged);
			// 
			// radioButtonType1
			// 
			this.radioButtonType1.Checked = true;
			this.radioButtonType1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.radioButtonType1.Location = new System.Drawing.Point(8, 64);
			this.radioButtonType1.Name = "radioButtonType1";
			this.radioButtonType1.Size = new System.Drawing.Size(200, 24);
			this.radioButtonType1.TabIndex = 0;
			this.radioButtonType1.TabStop = true;
			this.radioButtonType1.Text = "Particle System";
			this.radioButtonType1.CheckedChanged += new System.EventHandler(this.radioButtonType1_CheckedChanged);
			// 
			// groupBoxProperties
			// 
			this.groupBoxProperties.Controls.AddRange(new System.Windows.Forms.Control[] {
																							 this.propertyGridSystem});
			this.groupBoxProperties.Location = new System.Drawing.Point(8, 264);
			this.groupBoxProperties.Name = "groupBoxProperties";
			this.groupBoxProperties.Size = new System.Drawing.Size(216, 200);
			this.groupBoxProperties.TabIndex = 3;
			this.groupBoxProperties.TabStop = false;
			this.groupBoxProperties.Text = "Properties";
			// 
			// propertyGridSystem
			// 
			this.propertyGridSystem.CommandsVisibleIfAvailable = true;
			this.propertyGridSystem.LargeButtons = false;
			this.propertyGridSystem.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.propertyGridSystem.Location = new System.Drawing.Point(8, 24);
			this.propertyGridSystem.Name = "propertyGridSystem";
			this.propertyGridSystem.Size = new System.Drawing.Size(200, 168);
			this.propertyGridSystem.TabIndex = 0;
			this.propertyGridSystem.Text = "propertyGrid1";
			this.propertyGridSystem.ViewBackColor = System.Drawing.SystemColors.Window;
			this.propertyGridSystem.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// openGLCtrlParticlebuild
			// 
			this.openGLCtrlParticlebuild.Location = new System.Drawing.Point(232, 16);
			this.openGLCtrlParticlebuild.Mouse = SharpGL.SceneGraph.MouseOperation.Translate;
			this.openGLCtrlParticlebuild.Name = "openGLCtrlParticlebuild";
			this.openGLCtrlParticlebuild.Size = new System.Drawing.Size(448, 416);
			this.openGLCtrlParticlebuild.TabIndex = 4;
			// 
			// timerSystemTick
			// 
			this.timerSystemTick.Enabled = true;
			this.timerSystemTick.SynchronizingObject = this;
			this.timerSystemTick.Elapsed += new System.Timers.ElapsedEventHandler(this.timerSystemTick_Elapsed);
			// 
			// buttonOK
			// 
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Location = new System.Drawing.Point(608, 440);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.TabIndex = 5;
			this.buttonOK.Text = "OK";
			// 
			// ParticleSystemBuilderForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(690, 472);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonOK,
																		  this.openGLCtrlParticlebuild,
																		  this.groupBoxProperties,
																		  this.groupBoxType,
																		  this.groupBoxIdentity});
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "ParticleSystemBuilderForm";
			this.Text = "ParticleSystem Builder";
			this.Load += new System.EventHandler(this.ParticleSystemBuilder_Load);
			this.groupBoxIdentity.ResumeLayout(false);
			this.groupBoxType.ResumeLayout(false);
			this.groupBoxProperties.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.timerSystemTick)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void ParticleSystemBuilder_Load(object sender, System.EventArgs e)
		{						
			//	Set the system.
			SetSystem();
		}

		public ParticleSystem BuildingSystem
		{
			get {return particleSystem;}
			set {particleSystem = value;}
		}

		protected ParticleSystem particleSystem = new ParticleSystem();

		private void timerSystemTick_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			//	Tick the particle system.
			particleSystem.Tick();
		}

		protected virtual void SetSystem()
		{
			//	Clear the old system.
			openGLCtrlParticlebuild.Scene.CustomObjects.Clear();

			//	Add the new one.
			openGLCtrlParticlebuild.Scene.CustomObjects.Add(particleSystem);

			//	Update the property grid.
			propertyGridSystem.SelectedObject = particleSystem;
		}

		private void radioButtonType1_CheckedChanged(object sender, System.EventArgs e)
		{
			particleSystem = new ParticleSystem();
			particleSystem.Initialise(100);
			SetSystem();
		}

		private void radioButtonType2_CheckedChanged(object sender, System.EventArgs e)
		{
		//	particleSystem = new BoltParticleSystem();
			particleSystem.Initialise(100);
			SetSystem();
		}

		private void radioButtonType3_CheckedChanged(object sender, System.EventArgs e)
		{
		//	particleSystem = new CloudOfDust();
			particleSystem.Initialise(100);
			SetSystem();
		}

		private void radioButtonType4_CheckedChanged(object sender, System.EventArgs e)
		{
		//	particleSystem = new ElectricityParticleSystem();
			particleSystem.Initialise(100);
			SetSystem();
		}

		private void radioButtonType5_CheckedChanged(object sender, System.EventArgs e)
		{
		//	particleSystem = new FireParticleSystem();
			particleSystem.Initialise(100);
			SetSystem();
		}

		public string ParticleSystemName
		{
			get {return textBoxName.Text;}
		}


	}
}
