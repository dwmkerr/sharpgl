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
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using SharpGL;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.NET;
using SharpGL.SceneGraph.Collections;
using Apex.Controls;
using SharpGL.SceneGraph.Core;

namespace SceneBuilder
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class SceneBuilderForm : System.Windows.Forms.Form
	{
		private SharpGL.SceneControl openGLCtrlScene;
		private System.Windows.Forms.PropertyGrid propertyGridCurrent;
		private System.Windows.Forms.ToolBar toolBarMouse;
		private System.Windows.Forms.ToolBarButton toolBarButtonAutoSelect;
		private System.Windows.Forms.ToolBarButton toolBarButtonSeparator;
		private System.Windows.Forms.ToolBarButton toolBarButtonSelect;
		private System.Windows.Forms.ToolBarButton toolBarButtonTranslate;
		private System.Windows.Forms.ToolBarButton toolBarButtonRotate;
		private System.Windows.Forms.ToolBarButton toolBarButtonScale;
		private System.Windows.Forms.MainMenu mainMenuSceneBuilder;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItemNew;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItemSave;
		private System.Windows.Forms.MenuItem menuItemSaveAs;
		private System.Windows.Forms.MenuItem menuItemOpen;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItemExit;
		private System.Windows.Forms.ImageList imageListMaterialLibrary;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.TabPage tabPageScene;
		private System.Windows.Forms.TreeView treeViewScene;
		private System.Windows.Forms.TabPage tabPageCreate;
		private System.Windows.Forms.Button buttonPolygonBuilder;
		private System.Windows.Forms.Button buttonCreateCube;
		private System.Windows.Forms.Button buttonImportPoly;
		private System.Windows.Forms.Button buttonSphere;
		private System.Windows.Forms.Button buttonDisk;
		private System.Windows.Forms.Button buttonCylinder;
		private System.Windows.Forms.Button button1DCurve;
		private System.Windows.Forms.Button button2DPatch;
		private System.Windows.Forms.Button buttonParticleSystem;
		private System.Windows.Forms.TabControl tabControlTools;
		private System.Windows.Forms.Button button1DNURBS;
		private System.Windows.Forms.Button button2DNURBS;
        private System.Windows.Forms.Button buttonHeightMap;
		private System.Windows.Forms.MenuItem menuItemScene;
		private System.Windows.Forms.MenuItem menuItemRender;
		private System.Windows.Forms.Button buttonCreatePoints;

		protected MaterialPreviewEngine materialPreviewEngine = new MaterialPreviewEngine(64, 64);
		private System.Windows.Forms.Button buttonCone;
		private System.Windows.Forms.TabPage tabPageStats;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label labelGLVendor;
		private System.Windows.Forms.Label labelGLRenderer;
        private System.Windows.Forms.Label labelGLVersion;
        private Apex.Controls.ExpandingPanel expandingPanel1;
        private Apex.Controls.ExpandingPanel expandingPanel2;
        private Apex.Controls.ExpandingPanel expandingPanel3;
        private Apex.Controls.ExpandingPanel expandingPanel5;
        private Apex.Controls.ExpandingPanelContainer expandingPanelContainer1;
		private System.Windows.Forms.TabPage tabPageModify;
        private Apex.Controls.ExpandingPanelContainer expandingPanelContainer2;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TrackBar trackBarShininess;
		private Apex.Controls.ColorMap colourMapSpecular;
        private Apex.Controls.ColorMap colourMapEmission;
        private Apex.Controls.ColorMap colourMapDiffuse;
        private Apex.Controls.ColorMap colourMapAmbient;
        private Apex.Controls.ExpandingPanel expandingPanelMaterial;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Button buttonTextureLoad;
		private System.Windows.Forms.PictureBox pictureBoxTexture;
		private System.Windows.Forms.Button buttonTextureDelete;
        private Apex.Controls.ExpandingPanel expandingPanelProperties;
        private Apex.Controls.ExpandingPanel expandingPanelSelection;
        private Apex.Controls.ExpandingPanel expandingPanel4;
		private System.Windows.Forms.Button buttonSpherer;
		private System.Windows.Forms.Button button2DPolygon;
		private System.Windows.Forms.ListView listViewSelection;
		private System.Windows.Forms.ColumnHeader columnHeaderName;
		private System.Windows.Forms.ListView listViewSelectionMask;
		private System.Windows.Forms.ColumnHeader columnHeadeMask;
		private System.Windows.Forms.CheckBox checkBoxSelectionLimit;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.ListBox listBoxExtensions;
        private System.Windows.Forms.Button buttonBuildPlane;
		private System.Windows.Forms.ColumnHeader columnHeaderType; 

		public SceneBuilderForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//	Add the types to the selection mask.
			ListViewItem item = new ListViewItem();
			item.Checked = true;
			item.Tag = typeof(SceneObject);
			item.Text = "Any Scene Object";
			listViewSelectionMask.Items.Add(item);

			item = new ListViewItem();
			item.Checked = true;
			item.Tag = typeof(Face);
			item.Text = "Polygon Faces";
			listViewSelectionMask.Items.Add(item);

			item = new ListViewItem();
			item.Checked = true;
			item.Tag = typeof(Vertex);
			item.Text = "Vertices";
			listViewSelectionMask.Items.Add(item);

            labelGLVendor.Text = openGLCtrlScene.OpenGL.Vendor;
            labelGLRenderer.Text = openGLCtrlScene.OpenGL.Renderer;
            labelGLVersion.Text = openGLCtrlScene.OpenGL.Version;

            string extensions = openGLCtrlScene.OpenGL.Extensions;
			int iStart = 0;
			do
			{
				int iPos = extensions.IndexOf(' ', iStart);
				if(iPos == -1)
					break;
				string ext = extensions.Substring(iStart, iPos-iStart);
				listBoxExtensions.Items.Add(ext);
				iStart = iPos+1;
			} while(true);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SceneBuilderForm));
            this.openGLCtrlScene = new SharpGL.SceneControl();
            this.imageListMaterialLibrary = new System.Windows.Forms.ImageList(this.components);
            this.propertyGridCurrent = new System.Windows.Forms.PropertyGrid();
            this.toolBarMouse = new System.Windows.Forms.ToolBar();
            this.toolBarButtonAutoSelect = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonSeparator = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonSelect = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonTranslate = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonRotate = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonScale = new System.Windows.Forms.ToolBarButton();
            this.mainMenuSceneBuilder = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItemNew = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItemOpen = new System.Windows.Forms.MenuItem();
            this.menuItemSave = new System.Windows.Forms.MenuItem();
            this.menuItemSaveAs = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItemExit = new System.Windows.Forms.MenuItem();
            this.menuItemScene = new System.Windows.Forms.MenuItem();
            this.menuItemRender = new System.Windows.Forms.MenuItem();
            this.tabPageScene = new System.Windows.Forms.TabPage();
            this.treeViewScene = new System.Windows.Forms.TreeView();
            this.tabPageCreate = new System.Windows.Forms.TabPage();
            this.expandingPanelContainer1 = new Apex.Controls.ExpandingPanelContainer();
            this.expandingPanel1 = new Apex.Controls.ExpandingPanel();
            this.buttonPolygonBuilder = new System.Windows.Forms.Button();
            this.buttonImportPoly = new System.Windows.Forms.Button();
            this.buttonCreateCube = new System.Windows.Forms.Button();
            this.buttonHeightMap = new System.Windows.Forms.Button();
            this.expandingPanel2 = new Apex.Controls.ExpandingPanel();
            this.buttonCylinder = new System.Windows.Forms.Button();
            this.buttonSphere = new System.Windows.Forms.Button();
            this.buttonDisk = new System.Windows.Forms.Button();
            this.buttonCone = new System.Windows.Forms.Button();
            this.expandingPanel3 = new Apex.Controls.ExpandingPanel();
            this.button2DNURBS = new System.Windows.Forms.Button();
            this.button1DCurve = new System.Windows.Forms.Button();
            this.button2DPatch = new System.Windows.Forms.Button();
            this.button1DNURBS = new System.Windows.Forms.Button();
            this.expandingPanel5 = new Apex.Controls.ExpandingPanel();
            this.buttonCreatePoints = new System.Windows.Forms.Button();
            this.buttonParticleSystem = new System.Windows.Forms.Button();
            this.expandingPanel4 = new Apex.Controls.ExpandingPanel();
            this.buttonBuildPlane = new System.Windows.Forms.Button();
            this.buttonSpherer = new System.Windows.Forms.Button();
            this.button2DPolygon = new System.Windows.Forms.Button();
            this.tabControlTools = new System.Windows.Forms.TabControl();
            this.tabPageModify = new System.Windows.Forms.TabPage();
            this.expandingPanelContainer2 = new Apex.Controls.ExpandingPanelContainer();
            this.expandingPanelMaterial = new Apex.Controls.ExpandingPanel();
            this.buttonTextureDelete = new System.Windows.Forms.Button();
            this.buttonTextureLoad = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.pictureBoxTexture = new System.Windows.Forms.PictureBox();
            this.trackBarShininess = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.colourMapSpecular = new Apex.Controls.ColorMap();
            this.label3 = new System.Windows.Forms.Label();
            this.colourMapEmission = new Apex.Controls.ColorMap();
            this.label2 = new System.Windows.Forms.Label();
            this.colourMapDiffuse = new Apex.Controls.ColorMap();
            this.label1 = new System.Windows.Forms.Label();
            this.colourMapAmbient = new Apex.Controls.ColorMap();
            this.expandingPanelProperties = new Apex.Controls.ExpandingPanel();
            this.expandingPanelSelection = new Apex.Controls.ExpandingPanel();
            this.checkBoxSelectionLimit = new System.Windows.Forms.CheckBox();
            this.listViewSelectionMask = new System.Windows.Forms.ListView();
            this.columnHeadeMask = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listViewSelection = new System.Windows.Forms.ListView();
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPageStats = new System.Windows.Forms.TabPage();
            this.listBoxExtensions = new System.Windows.Forms.ListBox();
            this.label13 = new System.Windows.Forms.Label();
            this.labelGLVersion = new System.Windows.Forms.Label();
            this.labelGLRenderer = new System.Windows.Forms.Label();
            this.labelGLVendor = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.openGLCtrlScene)).BeginInit();
            this.tabPageScene.SuspendLayout();
            this.tabPageCreate.SuspendLayout();
            this.expandingPanelContainer1.SuspendLayout();
            this.expandingPanel1.SuspendLayout();
            this.expandingPanel2.SuspendLayout();
            this.expandingPanel3.SuspendLayout();
            this.expandingPanel5.SuspendLayout();
            this.expandingPanel4.SuspendLayout();
            this.tabControlTools.SuspendLayout();
            this.tabPageModify.SuspendLayout();
            this.expandingPanelContainer2.SuspendLayout();
            this.expandingPanelMaterial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTexture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarShininess)).BeginInit();
            this.expandingPanelProperties.SuspendLayout();
            this.expandingPanelSelection.SuspendLayout();
            this.tabPageStats.SuspendLayout();
            this.SuspendLayout();
            // 
            // openGLCtrlScene
            // 
            this.openGLCtrlScene.AutoSelect = true;
            this.openGLCtrlScene.BitDepth = 24;
            this.openGLCtrlScene.DrawFPS = false;
            this.openGLCtrlScene.FrameRate = 10F;
            this.openGLCtrlScene.Location = new System.Drawing.Point(296, 48);
            this.openGLCtrlScene.Mouse = SharpGL.SceneGraph.MouseOperation.Translate;
            this.openGLCtrlScene.Name = "openGLCtrlScene";
            this.openGLCtrlScene.RenderContextType = SharpGL.RenderContextType.FBO;
            this.openGLCtrlScene.ShowHandOnHover = false;
            this.openGLCtrlScene.Size = new System.Drawing.Size(424, 488);
            this.openGLCtrlScene.TabIndex = 1;
            this.openGLCtrlScene.SelectedItemChanged += new System.EventHandler(this.openGLCtrlScene_SelectedItemChanged);
            // 
            // imageListMaterialLibrary
            // 
            this.imageListMaterialLibrary.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.imageListMaterialLibrary.ImageSize = new System.Drawing.Size(64, 64);
            this.imageListMaterialLibrary.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // propertyGridCurrent
            // 
            this.propertyGridCurrent.CommandsBackColor = System.Drawing.SystemColors.ControlLight;
            this.propertyGridCurrent.HelpVisible = false;
            this.propertyGridCurrent.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.propertyGridCurrent.Location = new System.Drawing.Point(16, 24);
            this.propertyGridCurrent.Name = "propertyGridCurrent";
            this.propertyGridCurrent.Size = new System.Drawing.Size(216, 240);
            this.propertyGridCurrent.TabIndex = 3;
            this.propertyGridCurrent.ToolbarVisible = false;
            // 
            // toolBarMouse
            // 
            this.toolBarMouse.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.toolBarMouse.AutoSize = false;
            this.toolBarMouse.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.toolBarButtonAutoSelect,
            this.toolBarButtonSeparator,
            this.toolBarButtonSelect,
            this.toolBarButtonTranslate,
            this.toolBarButtonRotate,
            this.toolBarButtonScale});
            this.toolBarMouse.DropDownArrows = true;
            this.toolBarMouse.Location = new System.Drawing.Point(0, 0);
            this.toolBarMouse.Name = "toolBarMouse";
            this.toolBarMouse.ShowToolTips = true;
            this.toolBarMouse.Size = new System.Drawing.Size(728, 40);
            this.toolBarMouse.TabIndex = 4;
            this.toolBarMouse.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBarMouse_ButtonClick);
            // 
            // toolBarButtonAutoSelect
            // 
            this.toolBarButtonAutoSelect.Name = "toolBarButtonAutoSelect";
            this.toolBarButtonAutoSelect.Pushed = true;
            this.toolBarButtonAutoSelect.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.toolBarButtonAutoSelect.Text = "Auto Select";
            // 
            // toolBarButtonSeparator
            // 
            this.toolBarButtonSeparator.Name = "toolBarButtonSeparator";
            this.toolBarButtonSeparator.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBarButtonSelect
            // 
            this.toolBarButtonSelect.Name = "toolBarButtonSelect";
            this.toolBarButtonSelect.Text = "Select";
            // 
            // toolBarButtonTranslate
            // 
            this.toolBarButtonTranslate.ImageIndex = 0;
            this.toolBarButtonTranslate.Name = "toolBarButtonTranslate";
            this.toolBarButtonTranslate.Pushed = true;
            this.toolBarButtonTranslate.Text = "Translate";
            // 
            // toolBarButtonRotate
            // 
            this.toolBarButtonRotate.ImageIndex = 1;
            this.toolBarButtonRotate.Name = "toolBarButtonRotate";
            this.toolBarButtonRotate.Text = "Rotate";
            // 
            // toolBarButtonScale
            // 
            this.toolBarButtonScale.ImageIndex = 2;
            this.toolBarButtonScale.Name = "toolBarButtonScale";
            this.toolBarButtonScale.Text = "Scale";
            // 
            // mainMenuSceneBuilder
            // 
            this.mainMenuSceneBuilder.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItemScene});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemNew,
            this.menuItem3,
            this.menuItemOpen,
            this.menuItemSave,
            this.menuItemSaveAs,
            this.menuItem7,
            this.menuItemExit});
            this.menuItem1.Text = "&File";
            // 
            // menuItemNew
            // 
            this.menuItemNew.Index = 0;
            this.menuItemNew.Text = "&New";
            this.menuItemNew.Click += new System.EventHandler(this.menuItemNew_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 1;
            this.menuItem3.Text = "-";
            // 
            // menuItemOpen
            // 
            this.menuItemOpen.Index = 2;
            this.menuItemOpen.Text = "&Open...";
            this.menuItemOpen.Click += new System.EventHandler(this.menuItemOpen_Click);
            // 
            // menuItemSave
            // 
            this.menuItemSave.Index = 3;
            this.menuItemSave.Text = "&Save...";
            // 
            // menuItemSaveAs
            // 
            this.menuItemSaveAs.Index = 4;
            this.menuItemSaveAs.Text = "Save &As...";
            this.menuItemSaveAs.Click += new System.EventHandler(this.menuItemSaveAs_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 5;
            this.menuItem7.Text = "-";
            // 
            // menuItemExit
            // 
            this.menuItemExit.Index = 6;
            this.menuItemExit.Text = "&Exit";
            // 
            // menuItemScene
            // 
            this.menuItemScene.Index = 1;
            this.menuItemScene.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemRender});
            this.menuItemScene.Text = "&Scene";
            // 
            // menuItemRender
            // 
            this.menuItemRender.Index = 0;
            this.menuItemRender.Text = "&Render...";
            this.menuItemRender.Click += new System.EventHandler(this.menuItemRender_Click);
            // 
            // tabPageScene
            // 
            this.tabPageScene.Controls.Add(this.treeViewScene);
            this.tabPageScene.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPageScene.Location = new System.Drawing.Point(4, 29);
            this.tabPageScene.Name = "tabPageScene";
            this.tabPageScene.Size = new System.Drawing.Size(272, 391);
            this.tabPageScene.TabIndex = 0;
            this.tabPageScene.Text = "Scene";
            // 
            // treeViewScene
            // 
            this.treeViewScene.AllowDrop = true;
            this.treeViewScene.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewScene.ForeColor = System.Drawing.Color.Navy;
            this.treeViewScene.HideSelection = false;
            this.treeViewScene.ItemHeight = 16;
            this.treeViewScene.Location = new System.Drawing.Point(0, 0);
            this.treeViewScene.Name = "treeViewScene";
            this.treeViewScene.Size = new System.Drawing.Size(272, 391);
            this.treeViewScene.TabIndex = 0;
            this.treeViewScene.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeViewScene_ItemDrag);
            this.treeViewScene.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewScene_AfterSelect);
            this.treeViewScene.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeViewScene_DragDrop);
            this.treeViewScene.DragOver += new System.Windows.Forms.DragEventHandler(this.treeViewScene_DragOver);
            this.treeViewScene.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeViewScene_KeyDown);
            // 
            // tabPageCreate
            // 
            this.tabPageCreate.Controls.Add(this.expandingPanelContainer1);
            this.tabPageCreate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPageCreate.Location = new System.Drawing.Point(4, 29);
            this.tabPageCreate.Name = "tabPageCreate";
            this.tabPageCreate.Size = new System.Drawing.Size(272, 391);
            this.tabPageCreate.TabIndex = 1;
            this.tabPageCreate.Text = "Create";
            // 
            // expandingPanelContainer1
            // 
            this.expandingPanelContainer1.AllowDrop = true;
            this.expandingPanelContainer1.AutoScroll = true;
            this.expandingPanelContainer1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.expandingPanelContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.expandingPanelContainer1.Controls.Add(this.expandingPanel1);
            this.expandingPanelContainer1.Controls.Add(this.expandingPanel2);
            this.expandingPanelContainer1.Controls.Add(this.expandingPanel3);
            this.expandingPanelContainer1.Controls.Add(this.expandingPanel5);
            this.expandingPanelContainer1.Controls.Add(this.expandingPanel4);
            this.expandingPanelContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.expandingPanelContainer1.Location = new System.Drawing.Point(0, 0);
            this.expandingPanelContainer1.Name = "expandingPanelContainer1";
            this.expandingPanelContainer1.Size = new System.Drawing.Size(272, 391);
            this.expandingPanelContainer1.TabIndex = 19;
            // 
            // expandingPanel1
            // 
            this.expandingPanel1.AllowDrop = true;
            this.expandingPanel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.expandingPanel1.Controls.Add(this.buttonPolygonBuilder);
            this.expandingPanel1.Controls.Add(this.buttonImportPoly);
            this.expandingPanel1.Controls.Add(this.buttonCreateCube);
            this.expandingPanel1.Controls.Add(this.buttonHeightMap);
            this.expandingPanel1.Location = new System.Drawing.Point(4, 4);
            this.expandingPanel1.Name = "expandingPanel1";
            this.expandingPanel1.PanelContainer = this.expandingPanelContainer1;
            this.expandingPanel1.PanelName = "Polygons";
            this.expandingPanel1.Size = new System.Drawing.Size(243, 84);
            this.expandingPanel1.SizeWidthToContainer = false;
            this.expandingPanel1.TabIndex = 17;
            // 
            // buttonPolygonBuilder
            // 
            this.buttonPolygonBuilder.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonPolygonBuilder.Location = new System.Drawing.Point(8, 24);
            this.buttonPolygonBuilder.Name = "buttonPolygonBuilder";
            this.buttonPolygonBuilder.Size = new System.Drawing.Size(184, 20);
            this.buttonPolygonBuilder.TabIndex = 0;
            this.buttonPolygonBuilder.Text = "Polygon Builder...";
            this.buttonPolygonBuilder.Click += new System.EventHandler(this.buttonPolygonBuilder_Click);
            // 
            // buttonImportPoly
            // 
            this.buttonImportPoly.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonImportPoly.Location = new System.Drawing.Point(8, 56);
            this.buttonImportPoly.Name = "buttonImportPoly";
            this.buttonImportPoly.Size = new System.Drawing.Size(56, 20);
            this.buttonImportPoly.TabIndex = 2;
            this.buttonImportPoly.Text = "Import...";
            this.buttonImportPoly.Click += new System.EventHandler(this.buttonImportPoly_Click);
            // 
            // buttonCreateCube
            // 
            this.buttonCreateCube.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonCreateCube.Location = new System.Drawing.Point(136, 56);
            this.buttonCreateCube.Name = "buttonCreateCube";
            this.buttonCreateCube.Size = new System.Drawing.Size(56, 20);
            this.buttonCreateCube.TabIndex = 1;
            this.buttonCreateCube.Text = "Cube";
            this.buttonCreateCube.Click += new System.EventHandler(this.buttonCreateCube_Click);
            // 
            // buttonHeightMap
            // 
            this.buttonHeightMap.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonHeightMap.Location = new System.Drawing.Point(72, 56);
            this.buttonHeightMap.Name = "buttonHeightMap";
            this.buttonHeightMap.Size = new System.Drawing.Size(56, 20);
            this.buttonHeightMap.TabIndex = 14;
            this.buttonHeightMap.Text = "H.Map...";
            this.buttonHeightMap.Click += new System.EventHandler(this.buttonHeightMap_Click);
            // 
            // expandingPanel2
            // 
            this.expandingPanel2.AllowDrop = true;
            this.expandingPanel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.expandingPanel2.Controls.Add(this.buttonCylinder);
            this.expandingPanel2.Controls.Add(this.buttonSphere);
            this.expandingPanel2.Controls.Add(this.buttonDisk);
            this.expandingPanel2.Controls.Add(this.buttonCone);
            this.expandingPanel2.Location = new System.Drawing.Point(4, 92);
            this.expandingPanel2.Name = "expandingPanel2";
            this.expandingPanel2.PanelContainer = this.expandingPanelContainer1;
            this.expandingPanel2.PanelName = "Quadrics";
            this.expandingPanel2.Size = new System.Drawing.Size(243, 84);
            this.expandingPanel2.SizeWidthToContainer = false;
            this.expandingPanel2.TabIndex = 9;
            // 
            // buttonCylinder
            // 
            this.buttonCylinder.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonCylinder.Location = new System.Drawing.Point(8, 24);
            this.buttonCylinder.Name = "buttonCylinder";
            this.buttonCylinder.Size = new System.Drawing.Size(80, 20);
            this.buttonCylinder.TabIndex = 1;
            this.buttonCylinder.Text = "Cylinder";
            this.buttonCylinder.Click += new System.EventHandler(this.buttonCylinder_Click);
            // 
            // buttonSphere
            // 
            this.buttonSphere.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonSphere.Location = new System.Drawing.Point(96, 24);
            this.buttonSphere.Name = "buttonSphere";
            this.buttonSphere.Size = new System.Drawing.Size(88, 20);
            this.buttonSphere.TabIndex = 0;
            this.buttonSphere.Text = "Sphere";
            this.buttonSphere.Click += new System.EventHandler(this.buttonSphere_Click);
            // 
            // buttonDisk
            // 
            this.buttonDisk.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonDisk.Location = new System.Drawing.Point(8, 56);
            this.buttonDisk.Name = "buttonDisk";
            this.buttonDisk.Size = new System.Drawing.Size(80, 20);
            this.buttonDisk.TabIndex = 2;
            this.buttonDisk.Text = "Disk";
            this.buttonDisk.Click += new System.EventHandler(this.buttonDisk_Click);
            // 
            // buttonCone
            // 
            this.buttonCone.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonCone.Location = new System.Drawing.Point(96, 56);
            this.buttonCone.Name = "buttonCone";
            this.buttonCone.Size = new System.Drawing.Size(88, 20);
            this.buttonCone.TabIndex = 16;
            this.buttonCone.Text = "Cone";
            this.buttonCone.Click += new System.EventHandler(this.buttonCone_Click);
            // 
            // expandingPanel3
            // 
            this.expandingPanel3.AllowDrop = true;
            this.expandingPanel3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.expandingPanel3.Controls.Add(this.button2DNURBS);
            this.expandingPanel3.Controls.Add(this.button1DCurve);
            this.expandingPanel3.Controls.Add(this.button2DPatch);
            this.expandingPanel3.Controls.Add(this.button1DNURBS);
            this.expandingPanel3.Location = new System.Drawing.Point(4, 180);
            this.expandingPanel3.Name = "expandingPanel3";
            this.expandingPanel3.PanelContainer = this.expandingPanelContainer1;
            this.expandingPanel3.PanelName = "Evaluators";
            this.expandingPanel3.Size = new System.Drawing.Size(243, 88);
            this.expandingPanel3.SizeWidthToContainer = false;
            this.expandingPanel3.TabIndex = 10;
            // 
            // button2DNURBS
            // 
            this.button2DNURBS.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2DNURBS.Location = new System.Drawing.Point(96, 56);
            this.button2DNURBS.Name = "button2DNURBS";
            this.button2DNURBS.Size = new System.Drawing.Size(88, 20);
            this.button2DNURBS.TabIndex = 13;
            this.button2DNURBS.Text = "NURBS Surface";
            this.button2DNURBS.Click += new System.EventHandler(this.button2DNURBS_Click);
            // 
            // button1DCurve
            // 
            this.button1DCurve.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1DCurve.Location = new System.Drawing.Point(8, 24);
            this.button1DCurve.Name = "button1DCurve";
            this.button1DCurve.Size = new System.Drawing.Size(80, 20);
            this.button1DCurve.TabIndex = 0;
            this.button1DCurve.Text = "1D Curve";
            this.button1DCurve.Click += new System.EventHandler(this.button1DCurve_Click);
            // 
            // button2DPatch
            // 
            this.button2DPatch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2DPatch.Location = new System.Drawing.Point(96, 24);
            this.button2DPatch.Name = "button2DPatch";
            this.button2DPatch.Size = new System.Drawing.Size(88, 20);
            this.button2DPatch.TabIndex = 1;
            this.button2DPatch.Text = "2D Patch";
            this.button2DPatch.Click += new System.EventHandler(this.button2DPatch_Click);
            // 
            // button1DNURBS
            // 
            this.button1DNURBS.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1DNURBS.Location = new System.Drawing.Point(8, 56);
            this.button1DNURBS.Name = "button1DNURBS";
            this.button1DNURBS.Size = new System.Drawing.Size(80, 20);
            this.button1DNURBS.TabIndex = 12;
            this.button1DNURBS.Text = "NURBS Curve";
            this.button1DNURBS.Click += new System.EventHandler(this.button1DNURBS_Click);
            // 
            // expandingPanel5
            // 
            this.expandingPanel5.AllowDrop = true;
            this.expandingPanel5.BackColor = System.Drawing.SystemColors.ControlLight;
            this.expandingPanel5.Controls.Add(this.buttonCreatePoints);
            this.expandingPanel5.Controls.Add(this.buttonParticleSystem);
            this.expandingPanel5.Location = new System.Drawing.Point(4, 272);
            this.expandingPanel5.Name = "expandingPanel5";
            this.expandingPanel5.PanelContainer = this.expandingPanelContainer1;
            this.expandingPanel5.PanelName = "Misc.";
            this.expandingPanel5.Size = new System.Drawing.Size(243, 88);
            this.expandingPanel5.SizeWidthToContainer = false;
            this.expandingPanel5.TabIndex = 18;
            // 
            // buttonCreatePoints
            // 
            this.buttonCreatePoints.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonCreatePoints.Location = new System.Drawing.Point(8, 56);
            this.buttonCreatePoints.Name = "buttonCreatePoints";
            this.buttonCreatePoints.Size = new System.Drawing.Size(176, 20);
            this.buttonCreatePoints.TabIndex = 15;
            this.buttonCreatePoints.Text = "Points...";
            this.buttonCreatePoints.Click += new System.EventHandler(this.buttonCreatePoints_Click);
            // 
            // buttonParticleSystem
            // 
            this.buttonParticleSystem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonParticleSystem.Location = new System.Drawing.Point(8, 24);
            this.buttonParticleSystem.Name = "buttonParticleSystem";
            this.buttonParticleSystem.Size = new System.Drawing.Size(176, 20);
            this.buttonParticleSystem.TabIndex = 0;
            this.buttonParticleSystem.Text = "Particle System...";
            this.buttonParticleSystem.Click += new System.EventHandler(this.buttonParticleSystem_Click);
            // 
            // expandingPanel4
            // 
            this.expandingPanel4.AllowDrop = true;
            this.expandingPanel4.BackColor = System.Drawing.SystemColors.ControlLight;
            this.expandingPanel4.Controls.Add(this.buttonBuildPlane);
            this.expandingPanel4.Controls.Add(this.buttonSpherer);
            this.expandingPanel4.Controls.Add(this.button2DPolygon);
            this.expandingPanel4.Location = new System.Drawing.Point(4, 364);
            this.expandingPanel4.Name = "expandingPanel4";
            this.expandingPanel4.PanelContainer = this.expandingPanelContainer1;
            this.expandingPanel4.PanelName = "UI Objects";
            this.expandingPanel4.Size = new System.Drawing.Size(243, 88);
            this.expandingPanel4.SizeWidthToContainer = false;
            this.expandingPanel4.TabIndex = 5;
            // 
            // buttonBuildPlane
            // 
            this.buttonBuildPlane.Location = new System.Drawing.Point(16, 56);
            this.buttonBuildPlane.Name = "buttonBuildPlane";
            this.buttonBuildPlane.Size = new System.Drawing.Size(75, 23);
            this.buttonBuildPlane.TabIndex = 2;
            this.buttonBuildPlane.Text = "Plane";
            this.buttonBuildPlane.Click += new System.EventHandler(this.buttonBuildPlane_Click);
            // 
            // buttonSpherer
            // 
            this.buttonSpherer.Location = new System.Drawing.Point(16, 24);
            this.buttonSpherer.Name = "buttonSpherer";
            this.buttonSpherer.Size = new System.Drawing.Size(75, 23);
            this.buttonSpherer.TabIndex = 1;
            this.buttonSpherer.Text = "Sphere";
            this.buttonSpherer.Click += new System.EventHandler(this.buttonSpherer_Click);
            // 
            // button2DPolygon
            // 
            this.button2DPolygon.Location = new System.Drawing.Point(104, 24);
            this.button2DPolygon.Name = "button2DPolygon";
            this.button2DPolygon.Size = new System.Drawing.Size(75, 23);
            this.button2DPolygon.TabIndex = 0;
            this.button2DPolygon.Text = "Polygon";
            this.button2DPolygon.Click += new System.EventHandler(this.button2DPolygon_Click);
            // 
            // tabControlTools
            // 
            this.tabControlTools.Controls.Add(this.tabPageScene);
            this.tabControlTools.Controls.Add(this.tabPageCreate);
            this.tabControlTools.Controls.Add(this.tabPageModify);
            this.tabControlTools.Controls.Add(this.tabPageStats);
            this.tabControlTools.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlTools.ItemSize = new System.Drawing.Size(40, 25);
            this.tabControlTools.Location = new System.Drawing.Point(8, 48);
            this.tabControlTools.Name = "tabControlTools";
            this.tabControlTools.SelectedIndex = 0;
            this.tabControlTools.Size = new System.Drawing.Size(280, 424);
            this.tabControlTools.TabIndex = 2;
            // 
            // tabPageModify
            // 
            this.tabPageModify.Controls.Add(this.expandingPanelContainer2);
            this.tabPageModify.Location = new System.Drawing.Point(4, 29);
            this.tabPageModify.Name = "tabPageModify";
            this.tabPageModify.Size = new System.Drawing.Size(272, 391);
            this.tabPageModify.TabIndex = 4;
            this.tabPageModify.Text = "Modify";
            // 
            // expandingPanelContainer2
            // 
            this.expandingPanelContainer2.AllowDrop = true;
            this.expandingPanelContainer2.AutoScroll = true;
            this.expandingPanelContainer2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.expandingPanelContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.expandingPanelContainer2.Controls.Add(this.expandingPanelMaterial);
            this.expandingPanelContainer2.Controls.Add(this.expandingPanelProperties);
            this.expandingPanelContainer2.Controls.Add(this.expandingPanelSelection);
            this.expandingPanelContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.expandingPanelContainer2.Location = new System.Drawing.Point(0, 0);
            this.expandingPanelContainer2.Name = "expandingPanelContainer2";
            this.expandingPanelContainer2.Size = new System.Drawing.Size(272, 391);
            this.expandingPanelContainer2.TabIndex = 0;
            // 
            // expandingPanelMaterial
            // 
            this.expandingPanelMaterial.AllowDrop = true;
            this.expandingPanelMaterial.BackColor = System.Drawing.SystemColors.ControlLight;
            this.expandingPanelMaterial.Controls.Add(this.buttonTextureDelete);
            this.expandingPanelMaterial.Controls.Add(this.buttonTextureLoad);
            this.expandingPanelMaterial.Controls.Add(this.label12);
            this.expandingPanelMaterial.Controls.Add(this.pictureBoxTexture);
            this.expandingPanelMaterial.Controls.Add(this.trackBarShininess);
            this.expandingPanelMaterial.Controls.Add(this.label4);
            this.expandingPanelMaterial.Controls.Add(this.label5);
            this.expandingPanelMaterial.Controls.Add(this.colourMapSpecular);
            this.expandingPanelMaterial.Controls.Add(this.label3);
            this.expandingPanelMaterial.Controls.Add(this.colourMapEmission);
            this.expandingPanelMaterial.Controls.Add(this.label2);
            this.expandingPanelMaterial.Controls.Add(this.colourMapDiffuse);
            this.expandingPanelMaterial.Controls.Add(this.label1);
            this.expandingPanelMaterial.Controls.Add(this.colourMapAmbient);
            this.expandingPanelMaterial.Enabled = false;
            this.expandingPanelMaterial.Location = new System.Drawing.Point(4, 4);
            this.expandingPanelMaterial.Name = "expandingPanelMaterial";
            this.expandingPanelMaterial.PanelContainer = this.expandingPanelContainer2;
            this.expandingPanelMaterial.PanelName = "Material";
            this.expandingPanelMaterial.Size = new System.Drawing.Size(243, 408);
            this.expandingPanelMaterial.SizeWidthToContainer = false;
            this.expandingPanelMaterial.TabIndex = 20;
            this.expandingPanelMaterial.Paint += new System.Windows.Forms.PaintEventHandler(this.expandingPanelMaterial_Paint);
            // 
            // buttonTextureDelete
            // 
            this.buttonTextureDelete.Location = new System.Drawing.Point(168, 296);
            this.buttonTextureDelete.Name = "buttonTextureDelete";
            this.buttonTextureDelete.Size = new System.Drawing.Size(64, 23);
            this.buttonTextureDelete.TabIndex = 22;
            this.buttonTextureDelete.Text = "Delete";
            this.buttonTextureDelete.Click += new System.EventHandler(this.buttonTextureDelete_Click);
            // 
            // buttonTextureLoad
            // 
            this.buttonTextureLoad.Location = new System.Drawing.Point(168, 264);
            this.buttonTextureLoad.Name = "buttonTextureLoad";
            this.buttonTextureLoad.Size = new System.Drawing.Size(64, 23);
            this.buttonTextureLoad.TabIndex = 21;
            this.buttonTextureLoad.Text = "Load...";
            this.buttonTextureLoad.Click += new System.EventHandler(this.buttonTextureLoad_Click);
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(16, 240);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 16);
            this.label12.TabIndex = 20;
            this.label12.Text = "Texture";
            // 
            // pictureBoxTexture
            // 
            this.pictureBoxTexture.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxTexture.Location = new System.Drawing.Point(16, 264);
            this.pictureBoxTexture.Name = "pictureBoxTexture";
            this.pictureBoxTexture.Size = new System.Drawing.Size(144, 136);
            this.pictureBoxTexture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxTexture.TabIndex = 19;
            this.pictureBoxTexture.TabStop = false;
            // 
            // trackBarShininess
            // 
            this.trackBarShininess.Location = new System.Drawing.Point(72, 192);
            this.trackBarShininess.Maximum = 100;
            this.trackBarShininess.Name = "trackBarShininess";
            this.trackBarShininess.Size = new System.Drawing.Size(160, 45);
            this.trackBarShininess.TabIndex = 18;
            this.trackBarShininess.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarShininess.ValueChanged += new System.EventHandler(this.trackBarShininess_ValueChanged);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(16, 200);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 17;
            this.label4.Text = "Shininess";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(16, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 16);
            this.label5.TabIndex = 16;
            this.label5.Text = "Specular";
            // 
            // colourMapSpecular
            // 
            this.colourMapSpecular.BackColor = System.Drawing.Color.Red;
            this.colourMapSpecular.CurrentColor = System.Drawing.Color.Red;
            this.colourMapSpecular.Location = new System.Drawing.Point(72, 152);
            this.colourMapSpecular.Name = "colourMapSpecular";
            this.colourMapSpecular.ShowColorBorder = true;
            this.colourMapSpecular.Size = new System.Drawing.Size(160, 32);
            this.colourMapSpecular.TabIndex = 15;
            this.colourMapSpecular.ColorChanged += new Apex.Controls.ColorChangedDelegate(this.colourMapSpecular_ColourChanged);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 14;
            this.label3.Text = "Emission";
            // 
            // colourMapEmission
            // 
            this.colourMapEmission.BackColor = System.Drawing.Color.Red;
            this.colourMapEmission.CurrentColor = System.Drawing.Color.Red;
            this.colourMapEmission.Location = new System.Drawing.Point(72, 112);
            this.colourMapEmission.Name = "colourMapEmission";
            this.colourMapEmission.ShowColorBorder = true;
            this.colourMapEmission.Size = new System.Drawing.Size(160, 32);
            this.colourMapEmission.TabIndex = 13;
            this.colourMapEmission.ColorChanged += new Apex.Controls.ColorChangedDelegate(this.colourMapEmission_ColourChanged);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 12;
            this.label2.Text = "Diffuse";
            // 
            // colourMapDiffuse
            // 
            this.colourMapDiffuse.BackColor = System.Drawing.Color.Red;
            this.colourMapDiffuse.CurrentColor = System.Drawing.Color.Red;
            this.colourMapDiffuse.Location = new System.Drawing.Point(72, 72);
            this.colourMapDiffuse.Name = "colourMapDiffuse";
            this.colourMapDiffuse.ShowColorBorder = true;
            this.colourMapDiffuse.Size = new System.Drawing.Size(160, 32);
            this.colourMapDiffuse.TabIndex = 11;
            this.colourMapDiffuse.ColorChanged += new Apex.Controls.ColorChangedDelegate(this.colourMapDiffuse_ColourChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "Ambient";
            // 
            // colourMapAmbient
            // 
            this.colourMapAmbient.BackColor = System.Drawing.Color.Red;
            this.colourMapAmbient.CurrentColor = System.Drawing.Color.Red;
            this.colourMapAmbient.Location = new System.Drawing.Point(72, 32);
            this.colourMapAmbient.Name = "colourMapAmbient";
            this.colourMapAmbient.ShowColorBorder = true;
            this.colourMapAmbient.Size = new System.Drawing.Size(160, 32);
            this.colourMapAmbient.TabIndex = 9;
            this.colourMapAmbient.ColorChanged += new Apex.Controls.ColorChangedDelegate(this.colourMapAmbient_ColourChanged);
            // 
            // expandingPanelProperties
            // 
            this.expandingPanelProperties.AllowDrop = true;
            this.expandingPanelProperties.BackColor = System.Drawing.SystemColors.ControlLight;
            this.expandingPanelProperties.Controls.Add(this.propertyGridCurrent);
            this.expandingPanelProperties.Location = new System.Drawing.Point(4, 416);
            this.expandingPanelProperties.Name = "expandingPanelProperties";
            this.expandingPanelProperties.PanelContainer = this.expandingPanelContainer2;
            this.expandingPanelProperties.PanelName = "Properties";
            this.expandingPanelProperties.Size = new System.Drawing.Size(243, 272);
            this.expandingPanelProperties.SizeWidthToContainer = false;
            this.expandingPanelProperties.TabIndex = 19;
            // 
            // expandingPanelSelection
            // 
            this.expandingPanelSelection.AllowDrop = true;
            this.expandingPanelSelection.BackColor = System.Drawing.SystemColors.ControlLight;
            this.expandingPanelSelection.Controls.Add(this.checkBoxSelectionLimit);
            this.expandingPanelSelection.Controls.Add(this.listViewSelectionMask);
            this.expandingPanelSelection.Controls.Add(this.listViewSelection);
            this.expandingPanelSelection.Location = new System.Drawing.Point(4, 692);
            this.expandingPanelSelection.Name = "expandingPanelSelection";
            this.expandingPanelSelection.PanelContainer = this.expandingPanelContainer2;
            this.expandingPanelSelection.PanelName = "Selection";
            this.expandingPanelSelection.Size = new System.Drawing.Size(243, 264);
            this.expandingPanelSelection.SizeWidthToContainer = false;
            this.expandingPanelSelection.TabIndex = 9;
            // 
            // checkBoxSelectionLimit
            // 
            this.checkBoxSelectionLimit.Location = new System.Drawing.Point(8, 24);
            this.checkBoxSelectionLimit.Name = "checkBoxSelectionLimit";
            this.checkBoxSelectionLimit.Size = new System.Drawing.Size(224, 24);
            this.checkBoxSelectionLimit.TabIndex = 11;
            this.checkBoxSelectionLimit.Text = "Limit selection to one object only";
            // 
            // listViewSelectionMask
            // 
            this.listViewSelectionMask.CheckBoxes = true;
            this.listViewSelectionMask.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeadeMask});
            this.listViewSelectionMask.Location = new System.Drawing.Point(8, 48);
            this.listViewSelectionMask.Name = "listViewSelectionMask";
            this.listViewSelectionMask.Size = new System.Drawing.Size(224, 120);
            this.listViewSelectionMask.TabIndex = 10;
            this.listViewSelectionMask.UseCompatibleStateImageBehavior = false;
            this.listViewSelectionMask.View = System.Windows.Forms.View.Details;
            // 
            // columnHeadeMask
            // 
            this.columnHeadeMask.Text = "Select Objects Of Type";
            this.columnHeadeMask.Width = 220;
            // 
            // listViewSelection
            // 
            this.listViewSelection.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderType});
            this.listViewSelection.GridLines = true;
            this.listViewSelection.Location = new System.Drawing.Point(8, 176);
            this.listViewSelection.Name = "listViewSelection";
            this.listViewSelection.Size = new System.Drawing.Size(224, 80);
            this.listViewSelection.TabIndex = 9;
            this.listViewSelection.UseCompatibleStateImageBehavior = false;
            this.listViewSelection.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Name";
            this.columnHeaderName.Width = 120;
            // 
            // columnHeaderType
            // 
            this.columnHeaderType.Text = "Type";
            this.columnHeaderType.Width = 100;
            // 
            // tabPageStats
            // 
            this.tabPageStats.Controls.Add(this.listBoxExtensions);
            this.tabPageStats.Controls.Add(this.label13);
            this.tabPageStats.Controls.Add(this.labelGLVersion);
            this.tabPageStats.Controls.Add(this.labelGLRenderer);
            this.tabPageStats.Controls.Add(this.labelGLVendor);
            this.tabPageStats.Controls.Add(this.label9);
            this.tabPageStats.Controls.Add(this.label8);
            this.tabPageStats.Controls.Add(this.label7);
            this.tabPageStats.Controls.Add(this.label6);
            this.tabPageStats.Location = new System.Drawing.Point(4, 29);
            this.tabPageStats.Name = "tabPageStats";
            this.tabPageStats.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tabPageStats.Size = new System.Drawing.Size(272, 391);
            this.tabPageStats.TabIndex = 2;
            this.tabPageStats.Text = "Stats.";
            this.tabPageStats.ToolTipText = "Stats";
            this.tabPageStats.Click += new System.EventHandler(this.tabPageStats_Click);
            // 
            // listBoxExtensions
            // 
            this.listBoxExtensions.Location = new System.Drawing.Point(8, 136);
            this.listBoxExtensions.Name = "listBoxExtensions";
            this.listBoxExtensions.Size = new System.Drawing.Size(256, 95);
            this.listBoxExtensions.TabIndex = 8;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(8, 104);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(208, 24);
            this.label13.TabIndex = 7;
            this.label13.Text = "Extensions";
            // 
            // labelGLVersion
            // 
            this.labelGLVersion.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGLVersion.ForeColor = System.Drawing.Color.Navy;
            this.labelGLVersion.Location = new System.Drawing.Point(96, 64);
            this.labelGLVersion.Name = "labelGLVersion";
            this.labelGLVersion.Size = new System.Drawing.Size(120, 16);
            this.labelGLVersion.TabIndex = 6;
            this.labelGLVersion.Text = "Example Text";
            // 
            // labelGLRenderer
            // 
            this.labelGLRenderer.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGLRenderer.ForeColor = System.Drawing.Color.Navy;
            this.labelGLRenderer.Location = new System.Drawing.Point(96, 48);
            this.labelGLRenderer.Name = "labelGLRenderer";
            this.labelGLRenderer.Size = new System.Drawing.Size(120, 16);
            this.labelGLRenderer.TabIndex = 5;
            this.labelGLRenderer.Text = "Example Text";
            // 
            // labelGLVendor
            // 
            this.labelGLVendor.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGLVendor.ForeColor = System.Drawing.Color.Navy;
            this.labelGLVendor.Location = new System.Drawing.Point(96, 32);
            this.labelGLVendor.Name = "labelGLVendor";
            this.labelGLVendor.Size = new System.Drawing.Size(120, 16);
            this.labelGLVendor.TabIndex = 4;
            this.labelGLVendor.Text = "Example Text";
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(8, 64);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 16);
            this.label9.TabIndex = 3;
            this.label9.Text = "Version";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(8, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 16);
            this.label8.TabIndex = 2;
            this.label8.Text = "Renderer";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(8, 32);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 16);
            this.label7.TabIndex = 1;
            this.label7.Text = "Vendor";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(8, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(208, 24);
            this.label6.TabIndex = 0;
            this.label6.Text = "OpenGL Implementation";
            // 
            // SceneBuilderForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(728, 479);
            this.Controls.Add(this.toolBarMouse);
            this.Controls.Add(this.tabControlTools);
            this.Controls.Add(this.openGLCtrlScene);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenuSceneBuilder;
            this.Name = "SceneBuilderForm";
            this.Text = "SharpGL Scene Builder";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SceneBuilderForm_Load);
            this.SizeChanged += new System.EventHandler(this.SceneBuilderForm_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.openGLCtrlScene)).EndInit();
            this.tabPageScene.ResumeLayout(false);
            this.tabPageCreate.ResumeLayout(false);
            this.expandingPanelContainer1.ResumeLayout(false);
            this.expandingPanel1.ResumeLayout(false);
            this.expandingPanel2.ResumeLayout(false);
            this.expandingPanel3.ResumeLayout(false);
            this.expandingPanel5.ResumeLayout(false);
            this.expandingPanel4.ResumeLayout(false);
            this.tabControlTools.ResumeLayout(false);
            this.tabPageModify.ResumeLayout(false);
            this.expandingPanelContainer2.ResumeLayout(false);
            this.expandingPanelMaterial.ResumeLayout(false);
            this.expandingPanelMaterial.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTexture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarShininess)).EndInit();
            this.expandingPanelProperties.ResumeLayout(false);
            this.expandingPanelSelection.ResumeLayout(false);
            this.tabPageStats.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new SceneBuilderForm());
		}

		private void SceneBuilderForm_Load(object sender, System.EventArgs e)
		{
			//	Initialise the Scene Tree.
			PopulateSceneTree();
		}

		/// <summary>
		/// This function sets up the nodes of the Scene Tree Control.
		/// </summary>
		protected virtual void PopulateSceneTree()
		{
			//	Get a reference to the Scene.
			Scene scene = openGLCtrlScene.Scene;

			//	Delete all current nodes.
			treeViewScene.Nodes.Clear();

			//	Add the Scene itself.
			TreeNode nodeScene = new TreeNode("Scene");
			nodeScene.Tag = scene;
			treeViewScene.Nodes.Add(nodeScene);

			//	Add the scene polygons.
			TreeNode nodePolygons = new TreeNode("Polygons");
			nodePolygons.Tag = scene.Polygons;
			nodeScene.Nodes.Add(nodePolygons);

			foreach(SceneObject sceneObject in scene.Polygons)
			{
				TreeNode node = new TreeNode(sceneObject.Name);
				node.Tag = sceneObject;
				nodePolygons.Nodes.Add(node);
			}

			//	Add the scene cameras.
			TreeNode nodeCameras = new TreeNode("Cameras");
			nodeCameras.Tag = scene.Cameras;
			nodeScene.Nodes.Add(nodeCameras);

			foreach(SceneObject sceneObject in scene.Cameras)
			{
				TreeNode node = new TreeNode(sceneObject.Name);
				node.Tag = sceneObject;
				nodeCameras.Nodes.Add(node);
			}

			//	Add the scene lights.
			TreeNode nodeLights = new TreeNode("Lights");
			nodeLights.Tag = scene.Cameras;
			nodeScene.Nodes.Add(nodeLights);

			foreach(SceneObject sceneObject in scene.Lights)
			{
				TreeNode node = new TreeNode(sceneObject.Name);
				node.Tag = sceneObject;
				nodeLights.Nodes.Add(node);
			}

			//	Add the scene quadrics.
			TreeNode nodeQuadrics = new TreeNode("Quadrics");
			nodeQuadrics.Tag = scene.Quadrics;
			nodeScene.Nodes.Add(nodeQuadrics);

			foreach(SceneObject sceneObject in scene.Quadrics)
			{
				TreeNode node = new TreeNode(sceneObject.Name);
				node.Tag = sceneObject;
				nodeQuadrics.Nodes.Add(node);
			}

			//	Add the scene evaluators.
			TreeNode nodeEvaluators = new TreeNode("Evaluators");
			nodeEvaluators.Tag = scene.Evaluators;
			nodeScene.Nodes.Add(nodeEvaluators);

			foreach(SceneObject sceneObject in scene.Evaluators)
			{
				TreeNode node = new TreeNode(sceneObject.Name);
				node.Tag = sceneObject;
				nodeEvaluators.Nodes.Add(node);
			}

			//	Add the scene materials.
			TreeNode nodeMaterials = new TreeNode("Materials");
			nodeMaterials.Tag = scene.Materials;
			nodeScene.Nodes.Add(nodeMaterials);
            
			foreach(Material material in scene.Materials)
			{
                TreeNode node = new TreeNode(material.Name);
				node.Tag = material;
				nodeMaterials.Nodes.Add(node);
			}

			//	Add the scene custom objects.
			TreeNode nodeCustomObjects = new TreeNode("Custom Objects");
			nodeCustomObjects.Tag = scene.CustomObjects;
			nodeScene.Nodes.Add(nodeCustomObjects);

			foreach(SceneObject sceneObject in scene.CustomObjects)
			{
				TreeNode node = new TreeNode(sceneObject.Name);
				node.Tag = sceneObject;
				nodeCustomObjects.Nodes.Add(node);
			}

			//	Add the control to the tree as well.
			TreeNode control = new TreeNode("Control");
			control.Tag = openGLCtrlScene;
			treeViewScene.Nodes.Add(control);
		}

		private void treeViewScene_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			//	Something has been selected in the Scene Tree. Put the tag as the
			//	object for the property grid.
			SelectObject(treeViewScene, treeViewScene.SelectedNode.Tag);
		}

		private void buttonPolygonBuilder_Click(object sender, System.EventArgs e)
		{
			//	Create a polygon builder.
			PolygonBuilder polygonBuilder = new PolygonBuilder();

			if(propertyGridCurrent.SelectedObject != null)
			{
				if(propertyGridCurrent.SelectedObject.GetType() == typeof(Polygon))
					polygonBuilder.BuildingPolygon = (Polygon)propertyGridCurrent.SelectedObject;
			}

			if(polygonBuilder.ShowDialog(this) == DialogResult.OK)
			{
				AddObjectToScene(polygonBuilder.BuildingPolygon);
			}
		}

		private void buttonCreateCube_Click(object sender, System.EventArgs e)
		{
			//	Create and add a cube. Do it via the scene tree to keep it updated.
			AddObjectToScene(new Cube());
		}

		protected void AddObjectToScene(object sceneObject)
		{
			//	Create a treeview item for it.
			TreeNode newNode = new TreeNode(sceneObject.ToString());
			newNode.Tag = sceneObject;

			//	Add the object to the scene.
			IList listAddedTo = openGLCtrlScene.Scene.Jam(sceneObject);

			//	Find the treeview item with this list as it's tag.
			foreach(TreeNode node in treeViewScene.Nodes)
			{
				TreeNode find = FindNodeWithTag(node, listAddedTo);
				if(find != null)
				{
					//	We found it, shove it into this node.
					find.Nodes.Add(newNode);
					find.Expand();
				}
			}

			openGLCtrlScene.Invalidate();
		}

		protected TreeNode FindNodeWithTag(TreeNode node, object tag)
		{
			foreach(TreeNode childNode in node.Nodes)
			{
				//	Is it this?
				if(childNode.Tag == tag)
					return childNode;
				
				//	Is it it's child?
				TreeNode result = FindNodeWithTag(childNode, tag);
				if(result != null)
					return result;
			}

			return null;
		}

		private void buttonDisk_Click(object sender, System.EventArgs e)
		{
			AddObjectToScene(new SharpGL.SceneGraph.Quadrics.Disk());
		}

		private void buttonSphere_Click(object sender, System.EventArgs e)
		{
			AddObjectToScene(new SharpGL.SceneGraph.Quadrics.Sphere());	
		}

		private void buttonCylinder_Click(object sender, System.EventArgs e)
		{
			SharpGL.SceneGraph.Quadrics.Cylinder cy = new SharpGL.SceneGraph.Quadrics.Cylinder();
			cy.TopRadius = cy.BaseRadius;
			AddObjectToScene(cy);	
		}

		private void buttonCone_Click(object sender, System.EventArgs e)
		{
			AddObjectToScene(new SharpGL.SceneGraph.Quadrics.Cylinder());	
		}


		private void button1DCurve_Click(object sender, System.EventArgs e)
		{
			AddObjectToScene(new SharpGL.SceneGraph.Evaluators.Evaluator1D());
		}

		private void button2DPatch_Click(object sender, System.EventArgs e)
		{
			AddObjectToScene(new SharpGL.SceneGraph.Evaluators.Evaluator2D());
		}

		private void buttonImportPoly_Click(object sender, System.EventArgs e)
		{
            /* todo
			//	Try to load a polygon using the persistence engine.
			SharpGL.Persistence.PersistenceEngine engine = new SharpGL.Persistence.PersistenceEngine();

			object data = engine.UserLoad(typeof(List<Polygon>));
			if(data != null)
			{
				List<Polygon> polys = data as List<Polygon>;
				foreach(Polygon poly in polys)
					AddObjectToScene(poly);
			}*/
		}

		private void buttonParticleSystem_Click(object sender, System.EventArgs e)
		{
			//	Show the particle system designer.
			ParticleSystemBuilderForm form = new ParticleSystemBuilderForm();
			
			if(form.ShowDialog(this) == DialogResult.OK)
			{
				form.BuildingSystem.Name = form.ParticleSystemName;
				AddObjectToScene(form.BuildingSystem);
			}
		}

		/// <summary>
		/// Use this to select a scene object, by doing so the object will be highlighted
		/// in the tree, and the property grid will show the object properties.
		/// </summary>
		/// <param name="sender">The object that has selected it, and won't be 
		/// modified again. Generally the treeview or scene.</param>
		/// <param name="sceneObject">The object to select.</param>
		protected void SelectObject(object sender, object sceneObject)
		{
			//	If the treeview didn't select it, select it in there now.
			if(sender != treeViewScene)
			{
				//	Find the treeview item with this list as it's tag.
				foreach(TreeNode node in treeViewScene.Nodes)
				{
					TreeNode find = FindNodeWithTag(node, sceneObject);
					if(find != null)
					{
						//	We found it, select it.
						treeViewScene.SelectedNode = find;
					}
				}
			}

			//	Check if the object is a sceneobject.
			if(sceneObject.GetType() == typeof(SceneObject) ||
				sceneObject.GetType().IsSubclassOf(typeof(SceneObject)))
			{
				//	Set the material properties.
				currentMaterialsObject = (SceneObject)sceneObject;
				CurrentMaterial = ((SceneObject)sceneObject).Material;
				expandingPanelMaterial.Enabled = true;
			}
			else
			{
				//	Set the material properties.
				currentMaterialsObject = null;
				currentMaterial = null;
				expandingPanelMaterial.Enabled = false;
			}


			//	Select it in the properties grid.
			propertyGridCurrent.SelectedObject = sceneObject;
			expandingPanelProperties.PanelName = "'" + sceneObject.ToString() + "' Properties";
		}

		private void openGLCtrlScene_SelectedItemChanged(object sender, System.EventArgs e)
		{
			//	Reference the hits.
			List<IInteractable> hits = openGLCtrlScene.Hits;

			//	Remove any object that is not allowed.
			List<IInteractable> newHits = new List<IInteractable>();
			foreach(IInteractable interact in hits)
			{
				//	Check that the object is selectable.
				if(IsSelectable(interact))
					newHits.Add(interact);
			}
			

			//	If we have to limit, do so now.
			if(checkBoxSelectionLimit.Checked && newHits.Count > 1)
			{
				IInteractable single = newHits[0];
				newHits.Clear();
				newHits.Add(single);
			}
			
			openGLCtrlScene.Hits = newHits;
			hits = newHits;

			//	Select the object in the tree.
			if(hits.Count != 0)
				SelectObject(openGLCtrlScene, hits[0]);

			//	Add the items to the selected items list.
			listViewSelection.Items.Clear();
			foreach(IInteractable interact in hits)
			{
				ListViewItem item = new ListViewItem();
				item.Tag = interact;
				item.Text = interact.ToString();
				item.SubItems.Add(interact.GetType().ToString());
				listViewSelection.Items.Add(item);
			}

		}

		/// <summary>
		/// Determines whether a given type is selectable by
		/// the user.
		/// </summary>
		/// <param name="ob">The object.</param>
		/// <returns>True if the user can select that type.</returns>
		protected bool IsSelectable(object ob)
		{
			foreach(ListViewItem item in listViewSelectionMask.Items)
			{
				if((ob.GetType().IsSubclassOf((Type)item.Tag) ||
					((Type)item.Tag).IsInstanceOfType(ob)) && item.Checked == true)
					return true;
			}

			return false;
		}

		private void SceneBuilderForm_SizeChanged(object sender, System.EventArgs e)
		{
			//	The size is changed. First scale the property window down.
			tabControlTools.SetBounds(0, 0, 0, 
				(ClientSize.Height - tabControlTools.Location.Y) - 8, BoundsSpecified.Height);

			//	Now scale the OpenGL window.
			openGLCtrlScene.SetBounds(0, 0, (ClientSize.Width - openGLCtrlScene.Location.X) - 8,
				(ClientSize.Height - openGLCtrlScene.Location.Y) - 8, BoundsSpecified.Size);

		}

		private void toolBarMouse_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			//	If it's the auto-select button, switch auto select mode.

			if(e.Button == toolBarMouse.Buttons[0])
			{
				openGLCtrlScene.AutoSelect = !openGLCtrlScene.AutoSelect;
			}
			else
			{
				//	Turn off all of the current mouse function buttons.
				for(int i=2; i<6; i++)
					toolBarMouse.Buttons[i].Pushed = false;

				e.Button.Pushed = true;

				int mode = toolBarMouse.Buttons.IndexOf(e.Button);

				if(mode == 2)		openGLCtrlScene.Mouse = MouseOperation.Select;
				else if(mode == 3)	openGLCtrlScene.Mouse = MouseOperation.Translate;
				else if(mode == 4)	openGLCtrlScene.Mouse = MouseOperation.Rotate;
				else if(mode == 5)	openGLCtrlScene.Mouse = MouseOperation.Scale;
			}

		}

		private void menuItemNew_Click(object sender, System.EventArgs e)
		{
			//	We need to reset the scene.
			Scene scene = new Scene();
			scene.Initialise(openGLCtrlScene.Width, openGLCtrlScene.Height, SceneType.HighQuality);
			openGLCtrlScene.Scene = scene;

			//	Reset the current selection and scenetree.
			PopulateSceneTree();
			propertyGridCurrent.SelectedObject = null;
		}

		private void menuItemOpen_Click(object sender, System.EventArgs e)
		{
            /*todo
			//	Create a new persistence engine.
			SharpGL.Persistence.PersistenceEngine engine = new SharpGL.Persistence.PersistenceEngine();

			//	Try and open a scene.
			object data = engine.UserLoad(typeof(Scene));

			//	Did it work?
			if(data != null)
			{
				//	Initialise the scene.
				Scene scene = (Scene)data;

				scene.IntialiseFromSerial(openGLCtrlScene.Width, 
					openGLCtrlScene.Height, SceneType.HighQuality);
				openGLCtrlScene.Scene = scene;
				PopulateSceneTree();
				propertyGridCurrent.SelectedObject = null;
			}*/
		}

		private void menuItemSaveAs_Click(object sender, System.EventArgs e)
		{
            /*todo
			//	Create a new persistence engine.
			SharpGL.Persistence.PersistenceEngine engine = new SharpGL.Persistence.PersistenceEngine();

			//	Save the scene.
			engine.UserSave(openGLCtrlScene.Scene);
             * */
		}

		private void treeViewScene_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			//	Get the selected node.
			TreeNode node = treeViewScene.SelectedNode;

			//	Check for delete key.
			if(e.KeyData == Keys.Delete && node != null)
			{
				//	See if the node is something we can delete, a sceneobject.
				if(node.Tag != null)
				{
					if(node.Tag.GetType().IsSubclassOf(typeof(SceneObject)))
					{
						//	Delete the object from the scene.
						openGLCtrlScene.Scene.UnJam(node.Tag);

						//	Delete the node from the tree.
						node.Remove();
					}
				}
			}
		}

		private void buttonImportMaterial_Click(object sender, System.EventArgs e)
		{
            /*todo
			//	Try to load a material object.
			SharpGL.Persistence.PersistenceEngine engine = new SharpGL.Persistence.PersistenceEngine();

			object data = engine.UserLoad(typeof(Material));

			if(data != null)
			{
				//	Create the material.
				Material material = (Material)data;
				material.Texture.Create(openGLCtrlScene.Scene.OpenGL);
				AddObjectToScene(material);
			}*/
		}

		private void buttonCreatePoints_Click(object sender, System.EventArgs e)
		{
			//	Create and modal a points builder.
			PointsBuilder pointsBuilder = new PointsBuilder();

			if(pointsBuilder.ShowDialog(this) == DialogResult.OK)
			{
				//	Add the material to the scene.
				AddObjectToScene(pointsBuilder.Points);
			}
		}

		private void treeViewScene_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			//	Get the node.
			TreeNode node = (TreeNode)e.Data.GetData(typeof(TreeNode));

			
			//	Get the node we're over.
			Point client = treeViewScene.PointToClient(new Point(e.X, e.Y));
			TreeNode nodeOver = treeViewScene.GetNodeAt(client.X, client.Y);

			//	Make sure they are compatible.
			if(node.Tag.GetType() == typeof(Material))
			{
				if(nodeOver.Tag.GetType() == typeof(SceneObject) ||
					nodeOver.Tag.GetType().IsSubclassOf(typeof(SceneObject)))
				{
					((SceneObject)nodeOver.Tag).Material = (Material)node.Tag;
				}
			}
		}

		private void treeViewScene_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
		{
			Point client = treeViewScene.PointToClient(new Point(e.X, e.Y));

			//	Get the node we're over.
			TreeNode nodeOver = treeViewScene.GetNodeAt(client.X, client.Y);

			if(nodeOver != null)
			{
				if(nodeOver.Tag != null)
				{
					if(nodeOver.Tag.GetType() == typeof(SceneObject)
						|| nodeOver.Tag.GetType().IsSubclassOf(typeof(SceneObject)))
					{
						e.Effect = DragDropEffects.Link;
						return;
					}
				}
			}
			
			e.Effect = DragDropEffects.None;
		}

		private void treeViewScene_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
		{
			TreeNode item = (TreeNode)e.Item;

			//	If there's data associated with the item, then drag it.
			if(item.Tag != null)
				DoDragDrop(item, DragDropEffects.Link);		
		}

		private void button1DNURBS_Click(object sender, System.EventArgs e)
		{
			AddObjectToScene(new SharpGL.SceneGraph.Evaluators.NURBSCurve());
		}

		private void button2DNURBS_Click(object sender, System.EventArgs e)
		{
			AddObjectToScene(new SharpGL.SceneGraph.Evaluators.NURBSSurface());
		}

		private void buttonHeightMap_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Filter = "Image Files (*.bmp,*.jpg,*.gif)|*.jpg;*.bmp;*.gif";

			if(dialog.ShowDialog(this) == DialogResult.OK)
			{
				Polygon poly = new Polygon();
				poly.CreateFromMap(dialog.FileName, 40, 40);
				AddObjectToScene(poly);
			}
		}

		private void menuItemRender_Click(object sender, System.EventArgs e)
		{
			//	Take the last render and save it to file.
			SaveFileDialog dialog = new SaveFileDialog();
			dialog.Filter = "Image Files (*.bmp,*.jpg,*.gif)|*.jpg;*.bmp;*.gif";

			if(dialog.ShowDialog(this) == DialogResult.OK)
			{
                //  TODO: Render to bitmap.
				//	Save the file.
                //Bitmap bitmap = openGLCtrlScene.OpenGL.OpenGLBitmap;
				//bitmap.Save(dialog.FileName);
			}
		}

		private Material currentMaterial = null;
		private SceneElement currentMaterialsObject = null;

		Material CurrentMaterial
		{
			set 
			{
				currentMaterial = value;
				colourMapAmbient.CurrentColor = currentMaterial.Ambient;
				colourMapDiffuse.CurrentColor = currentMaterial.Diffuse;
				colourMapEmission.CurrentColor = currentMaterial.Emission;
				colourMapSpecular.CurrentColor = currentMaterial.Specular;
				trackBarShininess.Value = (int)(currentMaterial.Shininess * 100.0f);

				if(pictureBoxTexture.Image != null)
					pictureBoxTexture.Image.Dispose();

				if(currentMaterial != null && currentMaterial.Texture != null)
                    pictureBoxTexture.Image = currentMaterial.Texture.ToBitmap();
			}
		}
		private void colourMapAmbient_ColourChanged(object sender, ColorEventArgs e)
		{
			if(currentMaterial != null)
			{
				currentMaterial.Ambient = colourMapAmbient.CurrentColor;
				openGLCtrlScene.Invalidate();
			}
		}

		private void colourMapDiffuse_ColourChanged(object sender, ColorEventArgs e)
		{
			if(currentMaterial != null)
			{
				currentMaterial.Diffuse = colourMapDiffuse.CurrentColor;
				openGLCtrlScene.Invalidate();
			}
		}

		private void colourMapEmission_ColourChanged(object sender, ColorEventArgs e)
		{
			if(currentMaterial != null)
			{
				currentMaterial.Emission = colourMapEmission.CurrentColor;
				openGLCtrlScene.Invalidate();
			}
		}

		private void colourMapSpecular_ColourChanged(object sender, ColorEventArgs e)
		{
			if(currentMaterial != null)
			{
				currentMaterial.Specular = colourMapSpecular.CurrentColor;
				openGLCtrlScene.Invalidate();
			}
		}	

		private void trackBarShininess_ValueChanged(object sender, System.EventArgs e)
		{
			if(currentMaterial != null)
			{
				currentMaterial.Shininess = (float)trackBarShininess.Value / 100.0f;
				openGLCtrlScene.Invalidate();
			}
		}

		private void expandingPanelMaterial_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
		
		}

		private void buttonTextureLoad_Click(object sender, System.EventArgs e)
		{
			if(currentMaterial != null)
			{
				//	Ask the user for the texture file.
				System.Windows.Forms.OpenFileDialog dlg = new OpenFileDialog();
			
				dlg.Filter = "Image Files(*.bmp;*.jpg;*.jpeg;*.gif)|*.bmp;*.jpg;*.jpeg;*.gif|All files (*.*)|*.*";

				if(dlg.ShowDialog(this) == DialogResult.OK)
				{
					//	Get the path.
					string path = dlg.FileName;

					//	Create the texture.
					if(currentMaterial.Texture.Create(openGLCtrlScene.OpenGL, path))
					{
						if(pictureBoxTexture.Image != null)
							pictureBoxTexture.Image.Dispose();

						pictureBoxTexture.Image = currentMaterial.Texture.ToBitmap();
						openGLCtrlScene.Invalidate();
					}
				}
				
				
			}
		}

		private void buttonTextureDelete_Click(object sender, System.EventArgs e)
		{
			//	This is easy to do.
			if(currentMaterial != null)
			{
				currentMaterial.Texture.Destroy(openGLCtrlScene.OpenGL);
				pictureBoxTexture.Image.Dispose();
				pictureBoxTexture.Image = null;
			}
		}

		private void buttonSpherer_Click(object sender, System.EventArgs e)
		{
			//	Set the current builder.
			//todoopenGLCtrlScene.StartBuilding(new SharpGL.SceneGraph.Interaction.SphereBuilder());
		}
		
		private void buttonBuildPlane_Click(object sender, System.EventArgs e)
		{
			//	Set a plane builder.
			//todoopenGLCtrlScene.StartBuilding(new SharpGL.SceneGraph.Interaction.PlaneBuilder());
		}

		private void button2DPolygon_Click(object sender, System.EventArgs e)
		{
			//	The 2D Polygon object is actually implemented 
			//	via a Mouser, specifically a Polygon2Der.
		//	openGLCtrlScene.CurrentMouser = new SharpGL.SceneGraph.Interaction.Polygon2Der();
		//	openGLCtrlScene.Scene.Jam(openGLCtrlScene.CurrentMouser);
		}

		private void tabPageStats_Click(object sender, System.EventArgs e)
		{
		
		}

        private void buttonTestOpenGL21_Click(object sender, EventArgs e)
        {
            //  Call an OpenGL 2.1 function.
            openGLCtrlScene.OpenGL.ActiveTexture(1);
        }

		
		
	}
}
