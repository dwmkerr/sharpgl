﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// This type's instance is used in <see cref="MyScene.Draw(RenderMode.HitTest)"/>
    /// by <see cref="IColorCodedPicking"/> so that sceneElements can get their updated PickingBaseID.
    /// </summary>
    public class SharedStageInfo
    {
        /// <summary>
        /// Gets or sets how many vertices have been rendered during hit test.
        /// </summary>
        public virtual int RenderedVertexCount { get; set; }

        /// <summary>
        /// Reset this instance's fields' values to initial state so that it can be used again during rendering.
        /// </summary>
        public virtual void Reset()
        {
            RenderedVertexCount = 0;
        }

        public override string ToString()
        {
            return string.Format("rendered {0} primitives during hit test(picking).", RenderedVertexCount);
            //return base.ToString();
        }
    }
}