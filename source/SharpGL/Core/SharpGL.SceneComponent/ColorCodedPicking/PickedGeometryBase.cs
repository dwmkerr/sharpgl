using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// The color-coded picking result.
    /// <para>Representing a primitive.</para>
    /// </summary>
    public abstract class PickedGeometryBase : IPickedGeometry
    {
        /// <summary>
        /// Gets or sets primitive's geometry type.
        /// </summary>
        public GeometryTypes GeometryType { get; set; }

        /// <summary>
        /// Gets or sets positions of this primitive's vertices.
        /// </summary>
        public float[] positions { get; set; }

        /// <summary>
        /// The last vertex's id that constructs the picked primitive.
        /// <para>This id is in scene's all <see cref="IColorCodedPicking"/>s' order.</para>
        /// </summary>
        public uint StageVertexID { get; set; }

        /// <summary>
        /// The element that this picked primitive belongs to.
        /// </summary>
        public virtual IColorCodedPicking From { get; set; }

        public override string ToString()
        {
            var positions = this.positions;
            if (positions == null) { positions = new float[0]; }

            string strPositions = positions.PrintVectors();

            uint stageVertexID = this.StageVertexID;
            IColorCodedPicking picking = this.From;

            string lastVertexID = "?";
            if (picking != null)
            {
                uint tmp;
                if (picking.GetLastVertexIDOfPickedGeometry(stageVertexID, out tmp))
                {
                    lastVertexID = string.Format("{0}", tmp);
                }
            }

            string result = string.Format("{0}: P: {1} ID:{2}/{3} ∈{4}",
                GeometryType, strPositions, lastVertexID, stageVertexID, From);
            return result;
            //return base.ToString();
        }

    }
}
