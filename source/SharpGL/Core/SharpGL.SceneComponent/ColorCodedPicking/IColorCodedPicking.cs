using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// Scene element that implemented this interface will take part in color-coded picking when using <see cref="MyScene.Draw(RenderMode.HitTest);"/>.
    /// </summary>
    public interface IColorCodedPicking : SharpGL.SceneGraph.Core.IRenderable
    {
        /// <summary>
        /// Gets or internal sets how many primitived have been rendered till now during hit test.
        /// <para>This will be set up by <see cref="MyScene.Draw(RenderMode.HitTest)"/>, so just use it to set shader's uniform variable.</para>
        /// </summary>
        uint PickingBaseID { get; set; }

        /// <summary>
        /// Gets vertices' count in this element's VBO representing positions.
        /// </summary>
        /// <returns></returns>
        uint GetVertexCount();

        /// <summary>
        /// Get the primitive according to vertex's id.
        /// <para>Note: the <paramref name="stageVertexID"/> refers to the last vertex that constructs the primitive. And it's unique in scene's all elements.</para>
        /// <para>You can use <see cref="PickedPrimitiveHelper.TryPick()"/> to simplify your work.</para>
        /// </summary>
        /// <param name="stageVertexID">Refers to the last vertex that constructs the primitive. And it's unique in scene's all elements.</param>
        /// <returns></returns>
        IPickedGeometry Pick(uint stageVertexID);
    }
}
