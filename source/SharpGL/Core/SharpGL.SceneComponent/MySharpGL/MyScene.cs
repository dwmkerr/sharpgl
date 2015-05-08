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
        public MyScene(bool isClear = true)
        {
            this.IsClear = isClear;
        }

        /// <summary>
        /// Execute gl.ClearColor() and gl.Clear() if true.
        /// </summary>
        public bool IsClear { get; set; }

        public override void Draw(SceneGraph.Cameras.Camera camera = null)
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
                //	Set the clear color.
                float[] clear = (SharpGL.SceneGraph.GLColor)ClearColor;

                gl.ClearColor(clear[0], clear[1], clear[2], clear[3]);
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

            //  Render the root element, this will then render the whole
            //  of the scene tree.
            MyRenderElement(SceneContainer, gl, RenderMode.Design);

            //  TODO: Adding this code here re-enables textures- it should work without it but it
            //  doesn't, look into this.
            //gl.BindTexture(OpenGL.GL_TEXTURE_2D, 0);
            //gl.Enable(OpenGL.GL_TEXTURE_2D);

            gl.Flush();
        }

        /// <summary>
        /// Renders the element.
        /// </summary>
        /// <param name="gl">The gl.</param>
        /// <param name="renderMode">The render mode.</param>
        public void MyRenderElement(SceneElement sceneElement, OpenGL gl, RenderMode renderMode)
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

                //  If the element can be rendered, render it.
                IRenderable renderable = sceneElement as IRenderable;
                if (renderable != null) renderable.Render(gl, renderMode);

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
                MyRenderElement(childElement, gl, renderMode);

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
        /// <value>
        /// The current camera.
        /// </value>
        [Description("The current camera being used to view the scene."), Category("Scene")]
        public new Camera CurrentCamera
        {
            get { return base.CurrentCamera; }
            internal set { base.CurrentCamera = value; }
        }
    }
}