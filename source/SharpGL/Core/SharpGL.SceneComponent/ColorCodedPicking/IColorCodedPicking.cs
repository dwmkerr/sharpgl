using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// Scene element that implemented this interface will take part in color-coded picking when using <see cref="MyScene.Draw(RenderMode.HitTest);"/>.
    /// </summary>
    public interface IColorCodedPicking
    {
        /// <summary>
        /// Gets or internal sets how many primitived have been rendered till now during hit test.
        /// <para>This will be set up by <see cref="MyScene.Draw(RenderMode.HitTest)"/>, so just use the get method.</para>
        /// </summary>
        int PickingBaseID { get; set; }

        /// <summary>
        /// Gets vertex's count in this element's model's VBO representing positions.
        /// </summary>
        int VertexCount { get; }

        /// <summary>
        /// Get the primitive according to vertex's id.
        /// <para>Note: the <paramref name="stageVertexID"/> refers to the last vertex that constructs the primitive.</para>
        /// </summary>
        /// <param name="stageVertexID"></param>
        /// <returns></returns>
        IPickedPrimitive Pick(int stageVertexID);
    }
}
