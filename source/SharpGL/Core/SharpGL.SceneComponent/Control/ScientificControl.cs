using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using SharpGL.Version;
using SharpGL.SceneGraph;
using System.Collections.ObjectModel;
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph.Core;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// Scene control which contains axis, color indicator, etc.
    /// <para>Set the <see cref="ScientificModel"/> property to view a model.</para>
    /// </summary>
    public partial class ScientificControl : MySceneControl
    {
        /// <summary>
        /// maintains bounding box that contains all models.
        /// </summary>
        internal ModelContainer modelContainer;

        /// <summary>
        /// Gets the root element of all models.
        /// </summary>
        public ModelContainer ModelContainer
        { get { return this.modelContainer; } }


        public ScientificControl()
        {
            ScientificControlHelper.InitializeScene(this);
            //ScientificControlHelper.InitializeUIScene(this);

            this.MouseDown += ScientificVisual3DControl_MouseDown;
            this.MouseMove += ScientificVisual3DControl_MouseMove;
            this.MouseUp += ScientificVisual3DControl_MouseUp;
            this.MouseWheel += ScientificControl_MouseWheel;
            this.Resized += ScientificVisual3DControl_Resized;
        }

        void ScientificVisual3DControl_Resized(object sender, EventArgs e)
        {
            //this.modelContainer.AdjustCamera(this.OpenGL, this.Scene.CurrentCamera);
            //CameraHelper.AdjustCamera(this.modelContainer.BoundingBox, this.OpenGL, this.Scene.CurrentCamera);
            this.UpdateCamera();
        }

        void ScientificControl_MouseWheel(object sender, MouseEventArgs e)
        {
            ScientificCamera camera = this.Scene.CurrentCamera;
            //if (camera == null) { return; }

            camera.Scale(e.Delta);

            ManualRender(this);
        }

        void ScientificVisual3DControl_MouseUp(object sender, MouseEventArgs e)
        {
            bool render = false;

            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                IMouseRotation rotation = this.CameraRotation;
                if (rotation != null)
                {
                    rotation.MouseUp(e.X, e.Y);

                    render = true;
                }
            }

            if (render)
            { ManualRender(this); }
        }

        void ScientificVisual3DControl_MouseMove(object sender, MouseEventArgs e)
        {
            bool render = false;
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                IMouseRotation cameraRotation = this.CameraRotation;
                if (cameraRotation != null)
                {
                    cameraRotation.MouseMove(e.X, e.Y);

                    render = true;
                }
            }

            if (render)
            { ManualRender(this); }
        }

        void ScientificVisual3DControl_MouseDown(object sender, MouseEventArgs e)
        {
            bool render = false;

            if ((e.Button & MouseButtons.Left) == System.Windows.Forms.MouseButtons.Left)
            {
                IMouseRotation cameraRotation = this.CameraRotation;
                if (cameraRotation != null)
                {
                    cameraRotation.SetBounds(this.Width, this.Height);
                    cameraRotation.MouseDown(e.X, e.Y);

                    render = true;
                }
            }

            if (render)
            { ManualRender(this); }
        }

        private void ManualRender(Control control)
        {
            control.Invalidate();// this will invokes OnPaint(PaintEventArgs e);
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    //  Start the stopwatch so that we can time the rendering.
        //    stopwatch.Restart();

        //    //	Make sure it's our instance of openSharpGL that's active.
        //    OpenGL.MakeCurrent();

        //    //	Do the scene drawing.
        //    Scene.Draw();

        //    if (this.CameraType == ECameraType.Ortho)
        //    {
        //        // Redraw model container's bounding box so that it appears in front of models.
        //        // TODO: this is not needed in ECameraType.Perspecitive mode. fix this.
        //        //this.modelContainer.Render(this.OpenGL, SceneGraph.Core.RenderMode.Render);
        //    }

        //    UIScene.Draw();

        //    //	If there is a draw handler, then call it.
        //    DoOpenGLDraw(new RenderEventArgs(e.Graphics));

        //    //  Draw the FPS.
        //    if (DrawFPS)
        //    {
        //        OpenGL.DrawText(5, 5, 1.0f, 0.0f, 0.0f, "Courier New", 12.0f,
        //            string.Format("Draw Time: {0:0.0000} ms ~ {1:0.0} FPS", frameTime, 1000.0 / frameTime));
        //        OpenGL.Flush();
        //    }

        //    //	Blit our offscreen bitmap.
        //    IntPtr handleDeviceContext = e.Graphics.GetHdc();
        //    OpenGL.Blit(handleDeviceContext);
        //    e.Graphics.ReleaseHdc(handleDeviceContext);

        //    //	If's there's a GDI draw handler, then call it.
        //    DoGDIDraw(new RenderEventArgs(e.Graphics));

        //    //  Stop the stopwatch.
        //    stopwatch.Stop();

        //    //  Store the frame time.
        //    frameTime = stopwatch.Elapsed.TotalMilliseconds;
        //}

       
        ///// <summary>
        ///// holds UI elements(axis, color indicator etc).
        ///// </summary>
        //internal MyScene UIScene { get; set; }

        /// <summary>
        /// rotate and translate camera on a sphere, whose center is camera's Target.
        /// </summary>
        internal SatelliteRotation CameraRotation { get; set; }

        ///// <summary>
        ///// Draw axis with arc ball rotation effect on viewport as an UI.
        ///// </summary>
        //internal SimpleUIAxis uiAxis { get; set; }

        ///// <summary>
        ///// Draw color indicator on viewport as an UI.
        ///// </summary>
        //internal SimpleUIColorIndicator uiColorIndicator { get; set; }

        /// <summary>
        /// Add a model's element to model container.
        /// </summary>
        /// <param name="element"></param>
        public void AddModelElement(SceneElement element)
        {
            this.modelContainer.AddChild(element);

            ManualRender(this);
        }

        //public void AddScientificModel(IScientificModel model)
        //{
        //    if (model == null) { return; }

        //    ScientificCamera camera = this.Scene.CurrentCamera;
        //    ScientificModelElement element = new ScientificModelElement(model, this.renderModelsBoundingBox);
        //    this.modelContainer.AddChild(element);
        //    //this.modelContainer.AdjustCamera(this.OpenGL, camera);
        //    CameraHelper.AdjustCamera(this.modelContainer.BoundingBox, this.OpenGL, camera);
        //    // force CameraRotation to udpate.
        //    this.CameraRotation.Camera = this.Scene.CurrentCamera;

        //    ManualRender(this);
        //}

        public void ClearScientificModels()
        {
            //this.modelContainer.ClearChild();
            this.modelContainer.Children.Clear();

            ManualRender(this);
        }

        ///// <summary>
        ///// Determins whether render every model's bounding box or not.
        ///// </summary>
        //public bool RenderModelsBoundingBox
        //{
        //    get { return this.renderModelsBoundingBox; }
        //    set
        //    {
        //        if (this.renderModelsBoundingBox != value)
        //        {
        //            this.renderModelsBoundingBox = value;
        //            foreach (var item in this.modelContainer.Children)
        //            {
        //                ScientificModelElement element = item as ScientificModelElement;
        //                if (element != null)
        //                {
        //                    element.RenderBoundingBox = value;
        //                }
        //            }
        //            ManualRender(this);
        //        }
        //    }
        //}

        ///// <summary>
        ///// Determins whether render every model or not.
        ///// </summary>
        //public bool RenderModels
        //{
        //    get { return this.renderModels; }
        //    set
        //    {
        //        if (this.renderModels != value)
        //        {
        //            this.renderModels = value;
        //            foreach (var item in this.modelContainer.Children)
        //            {
        //                ScientificModelElement element = item as ScientificModelElement;
        //                if (element != null)
        //                {
        //                    element.RenderModel = value;
        //                }
        //            }
        //            ManualRender(this);
        //        }
        //    }
        //}

        /// <summary>
        /// Determins whether to render model container's bounding box or not.
        /// </summary>
        public bool RenderBoundingBox
        {
            get { return this.modelContainer.RenderBoundingBox; }
            set
            {
                if (this.modelContainer.RenderBoundingBox != value)
                {
                    this.modelContainer.RenderBoundingBox = value;

                    ManualRender(this);
                }
            }
        }

        /// <summary>
        /// Gets or sets camera's view type.
        /// </summary>
        public ECameraType CameraType
        {
            get { return this.Scene.CurrentCamera.CameraType; }
            set
            {
                if (this.Scene.CurrentCamera.CameraType != value)
                {
                    this.Scene.CurrentCamera.CameraType = value;

                    ManualRender(this);
                }
            }
        }

        private EViewType viewType;

        /// <summary>
        /// Gets or sets view type(top, bottom, left, right, front, back and userView).
        /// </summary>
        public EViewType ViewType
        {
            get { return this.viewType; }
            set
            {
                this.viewType = value;
                ScientificCamera camera = this.Scene.CurrentCamera;

                if (camera.CameraType == ECameraType.Perspecitive)
                {
                    IPerspectiveViewCamera perspecitive = camera;
                    perspecitive.ApplyViewType(this.modelContainer.BoundingBox, this.OpenGL, value);
                }
                else if (camera.CameraType == ECameraType.Ortho)
                {
                    IOrthoViewCamera orthoCamera = camera;
                    orthoCamera.ApplyViewType(this.modelContainer.BoundingBox, this.OpenGL, value);
                }
                else
                {
                    throw new NotImplementedException();
                } 
                //// force CameraRotation to udpate.
                //this.CameraRotation.Camera = this.Scene.CurrentCamera;
                ManualRender(this);
            }
        }

        //public void SetColorIndicator(float minValue, float maxValue, float step)
        //{
        //    this.uiColorIndicator.Data.MinValue = minValue;
        //    this.uiColorIndicator.Data.MaxValue = maxValue;
        //    this.uiColorIndicator.Data.Step = step;
        //    ManualRender(this);
        //}

        /// <summary>
        /// Update camera when resized, model container's bounding box updated, etc.
        /// </summary>
        public void UpdateCamera()
        {
            ScientificCamera camera = this.Scene.CurrentCamera;

            if (camera.CameraType == ECameraType.Perspecitive)
            {
                IPerspectiveViewCamera perspecitive = camera;
                perspecitive.AdjustCamera(this.modelContainer.BoundingBox, this.OpenGL);
            }
            else if (camera.CameraType == ECameraType.Ortho)
            {
                IOrthoViewCamera orthoCamera = camera;
                orthoCamera.AdjustCamera(this.modelContainer.BoundingBox, this.OpenGL);
            }
            else
            {
                throw new NotImplementedException();
            }
            //// force CameraRotation to udpate.
            //this.CameraRotation.Camera = this.Scene.CurrentCamera;
            ManualRender(this);
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="viewType"></param>
        //void UpdateCamera(EViewType viewType)
        //{
        //    CameraHelper.ApplyViewType(this.modelContainer.BoundingBox, this.OpenGL, this.Scene.CurrentCamera, viewType);
        //    // force CameraRotation to udpate.
        //    this.CameraRotation.Camera = this.Scene.CurrentCamera;
        //    ManualRender(this);
        //}
    }
}
