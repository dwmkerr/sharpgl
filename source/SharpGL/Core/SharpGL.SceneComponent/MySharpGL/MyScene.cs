using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// replace of <see cref="SharpGL.SceneGraph.Scene"/>
    /// <see cref="MyScene"/> pushes and pops <see cref="IBindable"/> scene elements.</para>
    /// </summary>
    public class MyScene : SharpGL.SceneGraph.Scene
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isClear">Execute gl.ClearColor() and gl.Clear() if true.</param>
        public MyScene(SharedStageInfo stagetInfo = null, bool isClear = true)
        {
            if (stagetInfo != null)
            {
                this.StageInfo = stagetInfo;
            }
            else
            {
                this.StageInfo = new SharedStageInfo();
            }
            this.IsClear = isClear;
        }

        /// <summary>
        /// Execute gl.ClearColor() and gl.Clear() if true.
        /// </summary>
        public bool IsClear { get; set; }

        public SharedStageInfo StageInfo { get; set; }

        /// <summary>
        /// Draw the scene.
        /// </summary>
        /// <param name="renderMode">Use Render for normal rendering and HitTest for picking.</param>
        /// <param name="camera">Keep this to null if <see cref="CurrentCamera"/> is already set up.</param>
        public void Draw(RenderMode renderMode = RenderMode.Render, SceneGraph.Cameras.Camera camera = null)
        {
            var gl = OpenGL;
            if (gl == null) { return; }

            //  TODO: we must decide what to do about drawing - are 
            //  cameras completely outside of the responsibility of the scene?
            //  If no camera has been provided, use the current one.
            if (camera == null)
                camera = CurrentCamera;

            if (IsClear)
            {
                if (renderMode == RenderMode.HitTest)
                {
                    // When picking on a position that no model exists, 
                    // the picked color would be
                    // = 255
                    // + 255 << 8
                    // + 255 << 16
                    // + 255 << 24
                    // = 255
                    // + 65280
                    // + 16711680
                    // + 4278190080
                    // = 4294967295
                    // This makes it easier to determin whether we picked something or not.
                    gl.ClearColor(1, 1, 1, 1);
                }
                else
                {
                    //	Set the clear color.
                    float[] clear = (SharpGL.SceneGraph.GLColor)ClearColor;

                    gl.ClearColor(clear[0], clear[1], clear[2], clear[3]);
                }
            }

            //  Reproject.
            if (camera != null)
                camera.Project(gl);

            if (IsClear)
            {
                //	Clear.
                gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT |
                    OpenGL.GL_STENCIL_BUFFER_BIT);
            }

            //gl.BindTexture(OpenGL.GL_TEXTURE_2D, 0);

            SharedStageInfo info = this.StageInfo;
            info.Reset();

            //  Render the root element, this will then render the whole
            //  of the scene tree.
            MyRenderElement(SceneContainer, gl, renderMode, info);

            //  TODO: Adding this code here re-enables textures- it should work without it but it
            //  doesn't, look into this.
            //gl.BindTexture(OpenGL.GL_TEXTURE_2D, 0);
            //gl.Enable(OpenGL.GL_TEXTURE_2D);

            gl.Flush();
        }

        //public override void Draw(SceneGraph.Cameras.Camera camera = null)
        //{
        //    this.Draw(camera, RenderMode.Design);
        //}

        /// <summary>
        /// Renders the element.
        /// </summary>
        /// <param name="gl">The gl.</param>
        /// <param name="renderMode">The render mode.</param>
        public void MyRenderElement(SceneElement sceneElement, OpenGL gl, RenderMode renderMode, SharedStageInfo info)
        {
            //  If the element is disabled, we're done.
            if (sceneElement.IsEnabled == false)
                return;

            //  Push each effect.
            foreach (var effect in sceneElement.Effects)
                if (effect.IsEnabled)
                    effect.Push(gl, sceneElement);

            //  If the element can be bound, bind it.
            IBindable bindable = sceneElement as IBindable;// example: Light
            if (bindable != null) bindable.Push(gl);

            //  If the element has an object space, transform into it.
            IHasObjectSpace hasObjectSpace = sceneElement as IHasObjectSpace;// example: Polygon, quadric, Teapot
            if (hasObjectSpace != null) hasObjectSpace.PushObjectSpace(gl);

            //  Render self.
            {
                //  If the element has a material, push it.
                IHasMaterial hasMaterial = sceneElement as IHasMaterial;// example: Polygon, quadric, Teapot
                if (hasMaterial != null && hasMaterial.Material != null)
                { hasMaterial.Material.Push(gl); }

                if (renderMode == RenderMode.HitTest)
                {
                    IColorCodedPicking picking = sceneElement as IColorCodedPicking;
                    info.RenderForPicking(picking, gl, renderMode);
                }
                else
                {
                    //  If the element can be rendered, render it.
                    IRenderable renderable = sceneElement as IRenderable;
                    if (renderable != null) renderable.Render(gl, renderMode);
                }

                //  If the element has a material, pop it.
                if (hasMaterial != null && hasMaterial.Material != null)
                { hasMaterial.Material.Pop(gl); }
            }

            //  If the element is volume bound and we are rendering volumes, render the volume.
            IVolumeBound volumeBound = null;
            if (RenderBoundingVolumes)
            {
                volumeBound = sceneElement as IVolumeBound;
                if (volumeBound != null)
                { volumeBound.BoundingVolume.Render(gl, renderMode); }
            }

            //  Recurse through the children.
            foreach (var childElement in sceneElement.Children)
                MyRenderElement(childElement, gl, renderMode, info);

            //  If the element has an object space, transform out of it.
            if (hasObjectSpace != null) hasObjectSpace.PopObjectSpace(gl);

            //  If the element can be bound, bind it.
            if (bindable != null) bindable.Pop(gl);

            //  Pop each effect.
            for (int i = sceneElement.Effects.Count - 1; i >= 0; i--)
                if (sceneElement.Effects[i].IsEnabled)
                    sceneElement.Effects[i].Pop(gl, sceneElement);
        }

        /// <summary>
        /// Gets the current camera.
        /// </summary>
        [Description("The current camera being used to view the scene."), Category("Scene")]
        public new ScientificCamera CurrentCamera
        {
            get { return base.CurrentCamera as ScientificCamera; }
            internal set { base.CurrentCamera = value; }
        }

        /// <summary>
        /// Get picked primitive by <paramref name="stageVertexID"/> as the last vertex that constructs the primitive.
        /// </summary>
        /// <param name="stageVertexID">The last vertex that constructs the primitive.</param>
        /// <returns></returns>
        public IPickedGeometry Pick(uint stageVertexID)
        {
            if (stageVertexID < 0) { return null; }

            SceneElement element = this.SceneContainer;
            IPickedGeometry pickedGeometry = Pick(element, stageVertexID);

            return pickedGeometry;
        }

        private IPickedGeometry Pick(SceneElement element, uint stageVertexID)
        {
            IPickedGeometry pickedGeometry = null;
            IColorCodedPicking pickingElement = element as IColorCodedPicking;
            if (pickingElement != null)
            {
                pickedGeometry = pickingElement.Pick(stageVertexID);
            }

            if (pickedGeometry == null)
            {
                if (element != null)
                {
                    foreach (var item in element.Children)
                    {
                        pickedGeometry = Pick(item, stageVertexID);
                        if (pickedGeometry != null)
                        { break; }
                    }
                }
            }

            return pickedGeometry;
        }
    }
}
