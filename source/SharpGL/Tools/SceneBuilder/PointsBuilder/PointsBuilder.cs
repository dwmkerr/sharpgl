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

namespace SceneBuilder
{
	/// <summary>
	/// Summary description for PolygonBuilder.
	/// </summary>
	public class PointsBuilder : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBoxIdentity;
		private System.Windows.Forms.TextBox textBoxName;
		private System.Windows.Forms.GroupBox groupBoxVertices;
		private SharpGL.SceneControl openGLCtrlPointsbuild;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxMaxDist;
		private System.Windows.Forms.Button buttonGeneratePoints;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxPointsCount;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public PointsBuilder()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
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
			this.groupBoxVertices = new System.Windows.Forms.GroupBox();
			this.textBoxMaxDist = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
            this.openGLCtrlPointsbuild = new SharpGL.SceneControl();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonGeneratePoints = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxPointsCount = new System.Windows.Forms.TextBox();
			this.groupBoxIdentity.SuspendLayout();
			this.groupBoxVertices.SuspendLayout();
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
			this.textBoxName.Text = "Points Name";
			this.textBoxName.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
			// 
			// groupBoxVertices
			// 
			this.groupBoxVertices.Controls.AddRange(new System.Windows.Forms.Control[] {
																						   this.textBoxPointsCount,
																						   this.label2,
																						   this.textBoxMaxDist,
																						   this.label1,
																						   this.buttonGeneratePoints});
			this.groupBoxVertices.Location = new System.Drawing.Point(8, 64);
			this.groupBoxVertices.Name = "groupBoxVertices";
			this.groupBoxVertices.Size = new System.Drawing.Size(216, 136);
			this.groupBoxVertices.TabIndex = 2;
			this.groupBoxVertices.TabStop = false;
			this.groupBoxVertices.Text = "Points";
			// 
			// textBoxMaxDist
			// 
			this.textBoxMaxDist.Location = new System.Drawing.Point(112, 64);
			this.textBoxMaxDist.Name = "textBoxMaxDist";
			this.textBoxMaxDist.Size = new System.Drawing.Size(96, 21);
			this.textBoxMaxDist.TabIndex = 3;
			this.textBoxMaxDist.Text = "10";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 64);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(104, 32);
			this.label1.TabIndex = 2;
			this.label1.Text = "Max Distance From Origin";
			// 
			// openGLCtrlPointsbuild
			// 
			this.openGLCtrlPointsbuild.AutoSelect = true;
			this.openGLCtrlPointsbuild.Location = new System.Drawing.Point(232, 16);
			this.openGLCtrlPointsbuild.Mouse = SharpGL.SceneGraph.MouseOperation.Translate;
			this.openGLCtrlPointsbuild.Name = "openGLCtrlPointsbuild";
			this.openGLCtrlPointsbuild.ShowHandOnHover = false;
			this.openGLCtrlPointsbuild.Size = new System.Drawing.Size(448, 416);
			this.openGLCtrlPointsbuild.TabIndex = 4;
			// 
			// buttonOK
			// 
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Location = new System.Drawing.Point(608, 440);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.TabIndex = 5;
			this.buttonOK.Text = "OK";
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(528, 440);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.TabIndex = 8;
			this.buttonCancel.Text = "Cancel";
			// 
			// buttonGeneratePoints
			// 
			this.buttonGeneratePoints.Location = new System.Drawing.Point(8, 24);
			this.buttonGeneratePoints.Name = "buttonGeneratePoints";
			this.buttonGeneratePoints.Size = new System.Drawing.Size(200, 23);
			this.buttonGeneratePoints.TabIndex = 9;
			this.buttonGeneratePoints.Text = "&Generate Points";
			this.buttonGeneratePoints.Click += new System.EventHandler(this.buttonGeneratePoints_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 96);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(104, 32);
			this.label2.TabIndex = 10;
			this.label2.Text = "Number of Points";
			// 
			// textBoxPointsCount
			// 
			this.textBoxPointsCount.Location = new System.Drawing.Point(112, 96);
			this.textBoxPointsCount.Name = "textBoxPointsCount";
			this.textBoxPointsCount.Size = new System.Drawing.Size(96, 21);
			this.textBoxPointsCount.TabIndex = 11;
			this.textBoxPointsCount.Text = "10000";
			// 
			// PointsBuilder
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(690, 472);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonCancel,
																		  this.buttonOK,
																		  this.openGLCtrlPointsbuild,
																		  this.groupBoxVertices,
																		  this.groupBoxIdentity});
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "PointsBuilder";
			this.Text = "PointsBuilder";
			this.Load += new System.EventHandler(this.PolygonBuilder_Load);
			this.groupBoxIdentity.ResumeLayout(false);
			this.groupBoxVertices.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void PolygonBuilder_Load(object sender, System.EventArgs e)
		{			
		//	openGLCtrlPolybuild.Scene.DrawExtras = true;
		
			//	Add the points to the scene.
			openGLCtrlPointsbuild.Scene.CustomObjects.Add(points);

			//	Update the data in the controls from the points data.
			PopulateControls();
		}

		/// <summary>
		/// This function repopulates the controls with the polygon data.
		/// </summary>
		protected void PopulateControls()
		{
			//	Set the name control.
			textBoxName.Text = points.Name;
		}

		public Points Points
		{
			get {return points;}
			set {points = value;}
		}

		protected Points points = new Points();

		private void textBoxName_TextChanged(object sender, System.EventArgs e)
		{
			points.Name = textBoxName.Text;
		}

		private void buttonGeneratePoints_Click(object sender, System.EventArgs e)
		{
			//	Create the points.
			int number = Int32.Parse(textBoxPointsCount.Text);
            int maxDist = Int32.Parse(textBoxMaxDist.Text);

			//	Create a coupla useful variables.
			double bounds = maxDist*2;

			//	Create the array.
			points.PointsRaw = new float[number*4];

			Random rand = new Random();

			//	Set the values.
			for(int i=0, index=0; i<number; i++, index+=4)
			{
				
				points.PointsRaw[index] = (float)(rand.NextDouble() * bounds - (double)maxDist);
				points.PointsRaw[index+1] = (float)(rand.NextDouble() * bounds - (double)maxDist);
				points.PointsRaw[index+2] = (float)(rand.NextDouble() * bounds - (double)maxDist);
				points.PointsRaw[index+3] = (float)rand.NextDouble();
			}
		}

	}
}
