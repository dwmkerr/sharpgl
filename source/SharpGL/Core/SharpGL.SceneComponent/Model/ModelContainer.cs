using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// maintains bounding box that contains all models.
    /// </summary>
    public class ModelContainer : SceneElement, IRenderable
    {
        BoundingBox boundingBox = new BoundingBox();

        /// <summary>
        /// Gets bounding box that contains all models.
        /// </summary>
        public BoundingBox BoundingBox
        {
            get { return boundingBox; }
        }

        /// <summary>
        /// Determins whether render the bounding box or not.
        /// </summary>
        public bool RenderBoundingBox { get; set; }

        public ModelContainer(bool renderBoundingBox = true)
        {
            this.RenderBoundingBox = renderBoundingBox;
        }

        #region IRenderable 成员

        void IRenderable.Render(OpenGL gl, RenderMode renderMode)
        {
            if (renderMode == RenderMode.HitTest) { return; }
            if (!this.RenderBoundingBox) { return; }
            //IBoundingBox boundingBox = this.expandedBoundingBox;
            IBoundingBox boundingBox = this.boundingBox;
            if (boundingBox == null) { return; }

            boundingBox.Render(gl, renderMode);
        }

        #endregion
    }
}
