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

using SharpGL;
using SharpGL.SceneGraph;
using SharpGL.Persistence;

namespace SceneBuilder
{
	/// <summary>
	/// Summary description for PolygonBuilder.
	/// </summary>
	public class PolygonBuilder : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBoxIdentity;
		private System.Windows.Forms.TextBox textBoxName;
		private System.Windows.Forms.GroupBox groupBoxVertices;
		private System.Windows.Forms.ListView listViewVertices;
		private System.Windows.Forms.Button buttonNew;
		private System.Windows.Forms.GroupBox groupBoxFaces;
		private System.Windows.Forms.ListView listViewFaces;
		private System.Windows.Forms.ColumnHeader columnHeaderIndicies;
		private System.Windows.Forms.Label label1;
		private SharpGL.SceneControl openGLCtrlPolybuild;
		private System.Windows.Forms.Button buttonDeleteFace;
		private System.Windows.Forms.Button buttonReorderFace;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonLoad;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Button buttonCancel;
		private SharpGL.Controls.VertexControl vertexControl1;
		private System.Windows.Forms.Button buttonDeleteVertex;
		private System.Windows.Forms.Button buttonNewPoly;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public PolygonBuilder()
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
			this.buttonDeleteVertex = new System.Windows.Forms.Button();
			this.vertexControl1 = new SharpGL.Controls.VertexControl();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonNew = new System.Windows.Forms.Button();
			this.listViewVertices = new System.Windows.Forms.ListView();
			this.groupBoxFaces = new System.Windows.Forms.GroupBox();
			this.buttonReorderFace = new System.Windows.Forms.Button();
			this.buttonDeleteFace = new System.Windows.Forms.Button();
			this.listViewFaces = new System.Windows.Forms.ListView();
			this.columnHeaderIndicies = new System.Windows.Forms.ColumnHeader();
			this.openGLCtrlPolybuild = new SharpGL.SceneControl();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonLoad = new System.Windows.Forms.Button();
			this.buttonSave = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonNewPoly = new System.Windows.Forms.Button();
			this.groupBoxIdentity.SuspendLayout();
			this.groupBoxVertices.SuspendLayout();
			this.groupBoxFaces.SuspendLayout();
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
			this.textBoxName.Text = "Polygon Name";
			this.textBoxName.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
			// 
			// groupBoxVertices
			// 
			this.groupBoxVertices.Controls.AddRange(new System.Windows.Forms.Control[] {
																						   this.buttonDeleteVertex,
																						   this.vertexControl1,
																						   this.label1,
																						   this.buttonNew,
																						   this.listViewVertices});
			this.groupBoxVertices.Location = new System.Drawing.Point(8, 64);
			this.groupBoxVertices.Name = "groupBoxVertices";
			this.groupBoxVertices.Size = new System.Drawing.Size(216, 216);
			this.groupBoxVertices.TabIndex = 2;
			this.groupBoxVertices.TabStop = false;
			this.groupBoxVertices.Text = "Vertices";
			// 
			// buttonDeleteVertex
			// 
			this.buttonDeleteVertex.Location = new System.Drawing.Point(136, 56);
			this.buttonDeleteVertex.Name = "buttonDeleteVertex";
			this.buttonDeleteVertex.Size = new System.Drawing.Size(72, 23);
			this.buttonDeleteVertex.TabIndex = 4;
			this.buttonDeleteVertex.Text = "&Delete";
			this.buttonDeleteVertex.Click += new System.EventHandler(this.buttonDeleteVertex_Click);
			// 
			// vertexControl1
			// 
			this.vertexControl1.Location = new System.Drawing.Point(8, 176);
			this.vertexControl1.Name = "vertexControl1";
			this.vertexControl1.Size = new System.Drawing.Size(144, 32);
			this.vertexControl1.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(136, 88);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 88);
			this.label1.TabIndex = 2;
			this.label1.Text = "Drag the vertices into the face panel to make a new face.";
			// 
			// buttonNew
			// 
			this.buttonNew.Location = new System.Drawing.Point(136, 24);
			this.buttonNew.Name = "buttonNew";
			this.buttonNew.Size = new System.Drawing.Size(72, 23);
			this.buttonNew.TabIndex = 1;
			this.buttonNew.Text = "&New";
			this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
			// 
			// listViewVertices
			// 
			this.listViewVertices.FullRowSelect = true;
			this.listViewVertices.GridLines = true;
			this.listViewVertices.Location = new System.Drawing.Point(8, 24);
			this.listViewVertices.MultiSelect = false;
			this.listViewVertices.Name = "listViewVertices";
			this.listViewVertices.Size = new System.Drawing.Size(120, 152);
			this.listViewVertices.TabIndex = 0;
			this.listViewVertices.View = System.Windows.Forms.View.SmallIcon;
			this.listViewVertices.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewVertices_KeyDown);
			this.listViewVertices.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewVertices_MouseDown);
			this.listViewVertices.SelectedIndexChanged += new System.EventHandler(this.listViewVertices_SelectedIndexChanged);
			// 
			// groupBoxFaces
			// 
			this.groupBoxFaces.Controls.AddRange(new System.Windows.Forms.Control[] {
																						this.buttonReorderFace,
																						this.buttonDeleteFace,
																						this.listViewFaces});
			this.groupBoxFaces.Location = new System.Drawing.Point(8, 280);
			this.groupBoxFaces.Name = "groupBoxFaces";
			this.groupBoxFaces.Size = new System.Drawing.Size(216, 184);
			this.groupBoxFaces.TabIndex = 3;
			this.groupBoxFaces.TabStop = false;
			this.groupBoxFaces.Text = "Faces";
			// 
			// buttonReorderFace
			// 
			this.buttonReorderFace.Location = new System.Drawing.Point(88, 152);
			this.buttonReorderFace.Name = "buttonReorderFace";
			this.buttonReorderFace.TabIndex = 2;
			this.buttonReorderFace.Text = "&Reorder";
			this.buttonReorderFace.Click += new System.EventHandler(this.buttonReorderFace_Click);
			// 
			// buttonDeleteFace
			// 
			this.buttonDeleteFace.Location = new System.Drawing.Point(8, 152);
			this.buttonDeleteFace.Name = "buttonDeleteFace";
			this.buttonDeleteFace.TabIndex = 1;
			this.buttonDeleteFace.Text = "D&elete";
			this.buttonDeleteFace.Click += new System.EventHandler(this.buttonDeleteFace_Click);
			// 
			// listViewFaces
			// 
			this.listViewFaces.AllowDrop = true;
			this.listViewFaces.CheckBoxes = true;
			this.listViewFaces.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							this.columnHeaderIndicies});
			this.listViewFaces.GridLines = true;
			this.listViewFaces.HideSelection = false;
			this.listViewFaces.Location = new System.Drawing.Point(8, 24);
			this.listViewFaces.Name = "listViewFaces";
			this.listViewFaces.Size = new System.Drawing.Size(200, 120);
			this.listViewFaces.TabIndex = 0;
			this.listViewFaces.View = System.Windows.Forms.View.Details;
			this.listViewFaces.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewFaces_KeyDown);
			this.listViewFaces.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.listViewFaces_KeyPress);
			this.listViewFaces.DragOver += new System.Windows.Forms.DragEventHandler(this.listViewFaces_DragOver);
			this.listViewFaces.DragDrop += new System.Windows.Forms.DragEventHandler(this.listViewFaces_DragDrop);
			this.listViewFaces.DragEnter += new System.Windows.Forms.DragEventHandler(this.listViewFaces_DragEnter);
			// 
			// columnHeaderIndicies
			// 
			this.columnHeaderIndicies.Text = "Indicies";
			this.columnHeaderIndicies.Width = 196;
			// 
			// openGLCtrlPolybuild
			// 
			this.openGLCtrlPolybuild.AutoSelect = true;
			this.openGLCtrlPolybuild.Location = new System.Drawing.Point(232, 16);
			this.openGLCtrlPolybuild.Mouse = SharpGL.SceneGraph.MouseOperation.Translate;
			this.openGLCtrlPolybuild.Name = "openGLCtrlPolybuild";
			this.openGLCtrlPolybuild.ShowHandOnHover = false;
			this.openGLCtrlPolybuild.Size = new System.Drawing.Size(448, 416);
			this.openGLCtrlPolybuild.TabIndex = 4;
			// 
			// buttonOK
			// 
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Location = new System.Drawing.Point(608, 440);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.TabIndex = 5;
			this.buttonOK.Text = "OK";
			// 
			// buttonLoad
			// 
			this.buttonLoad.Location = new System.Drawing.Point(336, 440);
			this.buttonLoad.Name = "buttonLoad";
			this.buttonLoad.TabIndex = 6;
			this.buttonLoad.Text = "&Load...";
			this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(416, 440);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.TabIndex = 7;
			this.buttonSave.Text = "&Save...";
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(528, 440);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.TabIndex = 8;
			this.buttonCancel.Text = "Cancel";
			// 
			// buttonNewPoly
			// 
			this.buttonNewPoly.Location = new System.Drawing.Point(232, 440);
			this.buttonNewPoly.Name = "buttonNewPoly";
			this.buttonNewPoly.TabIndex = 9;
			this.buttonNewPoly.Text = "Ne&w";
			this.buttonNewPoly.Click += new System.EventHandler(this.buttonNewPoly_Click);
			// 
			// PolygonBuilder
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(690, 472);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonNewPoly,
																		  this.buttonCancel,
																		  this.buttonSave,
																		  this.buttonLoad,
																		  this.buttonOK,
																		  this.openGLCtrlPolybuild,
																		  this.groupBoxFaces,
																		  this.groupBoxVertices,
																		  this.groupBoxIdentity});
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "PolygonBuilder";
			this.Text = "PolygonBuilder";
			this.Load += new System.EventHandler(this.PolygonBuilder_Load);
			this.groupBoxIdentity.ResumeLayout(false);
			this.groupBoxVertices.ResumeLayout(false);
			this.groupBoxFaces.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void PolygonBuilder_Load(object sender, System.EventArgs e)
		{			
			openGLCtrlPolybuild.Scene.SceneContainer.AddChild(new Cube());


			//	Set the polygon to draw itself as points.
			//polygon.CurrentContext = Polygon.Context.EditVertices;
			//polygon.DrawNormals = true;

			//	Update the data in the controls from the polygon data.
			PopulateControls();
		}

		private void buttonNew_Click(object sender, System.EventArgs e)
		{
			//	Get the data for the newly created vertex.
			Vertex vertex = new Vertex(0, 0, 0);
			int index = polygon.Vertices.Count;

			//	Create a list view item.
			ListViewItem lvItem = new ListViewItem();

			//	Set the list view item data.
			lvItem.Text = index.ToString();
			lvItem.SubItems.Add("0");
			lvItem.SubItems.Add("0");
			lvItem.SubItems.Add("0");
			lvItem.Tag = vertex;

			//	Add the vertex to the polygon and to the list.
			polygon.Vertices.Add(vertex);
			listViewVertices.Items.Add(lvItem);
		}

		private void listViewFaces_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			//	Get the index.
			string index = (string)e.Data.GetData(typeof(string));

			//	Add this index.
			AddIndexToFaces(int.Parse(index));
		}

		protected void AddIndexToFaces(int index)
		{
			//	Are there any items?
			if(listViewFaces.SelectedItems.Count == 0)
			{
				//	We need to add a brand new face to the polygon.
				
				//	Create the face, and add the index.
				Face face = new Face();
				face.Indices.Add(new Index(index));

				//	Add the face to the polygon.
				polygon.Faces.Add(face);

				//	Add the face to the list view.
				ListViewItem item = listViewFaces.Items.Add(index.ToString());
				item.Tag = face;
				item.Checked = true;
			}
			else
			{
				//	It's an exisiting face, so we simply add the index to it.
				ListViewItem faceItem = listViewFaces.SelectedItems[0];
				Face face = (Face)faceItem.Tag;
				face.Indices.Add(new Index(index));

				faceItem.Text += ", " + index.ToString();
			}
		}

		private void listViewVertices_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(listViewVertices.Items.Count == 0)
				return;
    
			//	Get the item at the point clicked.
			ListViewItem item = listViewVertices.GetItemAt(e.X, e.Y);

			if(item != null)
			{

				//	Start dragging and dropping.
				DragDropEffects effects = DoDragDrop(item.Text, DragDropEffects.Copy);
			}
		}

		private void listViewFaces_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
		{
			//	Check the type of object is valid.
			if(e.Data.GetDataPresent(typeof(string)))
				e.Effect = DragDropEffects.Copy;
			else
				e.Effect = DragDropEffects.None;
		}

		private void listViewFaces_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
		{
			Point client = listViewFaces.PointToClient(new Point(e.X, e.Y));

			//	Are we dragging the sort of item we can take? (a string).
			ListViewItem itemOver = listViewFaces.GetItemAt(client.X, client.Y);

			//	Clear any items that might be selected.
			listViewFaces.SelectedItems.Clear();

			//	Select the item that is over.	
			if(itemOver != null)
				itemOver.Selected = true;
		}

		/// <summary>
		/// This function repopulates the controls with the polygon data.
		/// </summary>
		protected void PopulateControls()
		{
			//	Clear the vertices control.
			listViewVertices.Items.Clear();

			//	Add each vertex.
			for(int i = 0; i < polygon.Vertices.Count; i++)
			{
				Vertex v = polygon.Vertices[i];

				ListViewItem lvItem = new ListViewItem();
				lvItem.Text = i.ToString();
				lvItem.SubItems.Add(v.X.ToString());
				lvItem.SubItems.Add(v.Y.ToString());
				lvItem.SubItems.Add(v.Z.ToString());
				lvItem.Tag = v;

				listViewVertices.Items.Add(lvItem);
			}

			//	Clear the faces control.
			listViewFaces.Items.Clear();

			//	Add each face.
			for(int f = 0; f < polygon.Faces.Count; f++)
			{
				Face face = polygon.Faces[f];

				ListViewItem lvItem = new ListViewItem();
				lvItem.Tag = face;
				lvItem.Checked = true;
				
				foreach(Index index in face.Indices)
					lvItem.Text += index.Vertex.ToString() + ", ";

				lvItem.Text.Remove(lvItem.Text.Length - 3, 2);

				listViewFaces.Items.Add(lvItem);
			}

			//	Set the name control.
			textBoxName.Text = polygon.Name;
		}

		public Polygon BuildingPolygon
		{
			get {return polygon.ToPolygon();}
			set {polygon = new BuildablePolygon(value);}
		}

		protected BuildablePolygon polygon = new BuildablePolygon();

		private void buttonDeleteFace_Click(object sender, System.EventArgs e)
		{
			DeleteCurrentFace();
		}

		protected void DeleteCurrentFace()
		{
			//	Get the current face.
			foreach(ListViewItem item in listViewFaces.SelectedItems)
			{
				//	Delete the face from the polygon.
				polygon.Faces.Remove((Face)item.Tag);
			}

			//	Delete each selected face from the list view.
			while(listViewFaces.SelectedItems.Count != 0)
				listViewFaces.Items.Remove(listViewFaces.SelectedItems[0]);

			listViewFaces.SelectedItems.Clear();
		}

		private void listViewFaces_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyData == Keys.Delete)
			{
				DeleteCurrentFace();
			}
		}

		private void buttonReorderFace_Click(object sender, System.EventArgs e)
		{
			foreach(ListViewItem item in listViewFaces.SelectedItems)
			{
				//	Get the face.
				Face face = (Face)item.Tag;

				//	Reorder the face.
				face.Reorder(polygon);

				//	Now rename the item.
				item.Text = "";
				foreach(Index index in face.Indices)
					item.Text += index.Vertex.ToString() + ", ";

				item.Text.Remove(item.Text.Length - 3, 2);
			}
		}

		private void buttonSave_Click(object sender, System.EventArgs e)
		{
            /*TODO
			//	Save the polygon.
			engine.UserSave(BuildingPolygon);
             * */
		}

		private void buttonLoad_Click(object sender, System.EventArgs e)
		{
            /*TODO
			//	Load the polygon.
			object data = engine.UserLoad(typeof(Polygon));

			if(data != null)
			{
				//	Set the new polygon.
				BuildingPolygon = (Polygon)data;

				//	Remove the old one.
				openGLCtrlPolybuild.Scene.Polygons.Clear();
				openGLCtrlPolybuild.Scene.Polygons.Add(polygon);

				//	Update the controls.
				PopulateControls();
			}*/
		}

		private void textBoxName_TextChanged(object sender, System.EventArgs e)
		{
			polygon.Name = textBoxName.Text;
		}

		private void listViewFaces_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			//	Is this key a digit?
			if(char.IsDigit(e.KeyChar))
			{
				//	Get the digit.
				int index = int.Parse(e.KeyChar.ToString());

				//	Add this index.
				AddIndexToFaces(index);
			}
		}

		private void listViewVertices_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(listViewVertices.SelectedItems.Count > 0)
			{
				ListViewItem item = listViewVertices.SelectedItems[0];
                
				//	update the vertex editor.
				vertexControl1.Vertex = (Vertex)item.Tag;
			}
		}

		private void buttonDeleteVertex_Click(object sender, System.EventArgs e)
		{
			DeleteCurrentVertices();
		}

		private void listViewVertices_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyData == Keys.Delete)
				DeleteCurrentVertices();
		}

		protected void DeleteCurrentVertices()
		{
			//	Warn the user of the danger of this.
			if(MessageBox.Show(this, "Warning, deleting a vertex reorders the array, meaning all faces must be deleted, continue?",
				"Warning", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
			{
				polygon.Faces.Clear();
				listViewFaces.Items.Clear();

				//	Get the current face.
				foreach(ListViewItem item in listViewVertices.SelectedItems)
				{
					//	Delete the face from the polygon.
					polygon.Vertices.Remove((Vertex)item.Tag);
				}

				//	Delete each selected vertex from the list view.
				while(listViewVertices.SelectedItems.Count != 0)
					listViewVertices.Items.Remove(listViewVertices.SelectedItems[0]);

				listViewVertices.SelectedItems.Clear();
			}
		}

		private void buttonNewPoly_Click(object sender, System.EventArgs e)
		{
			polygon = new BuildablePolygon();
			polygon.DrawNormals = true;

			PopulateControls();
			openGLCtrlPolybuild.Scene.Polygons.Clear();
			openGLCtrlPolybuild.Scene.Polygons.Add(polygon);

			PopulateControls();
		}
	}
}
