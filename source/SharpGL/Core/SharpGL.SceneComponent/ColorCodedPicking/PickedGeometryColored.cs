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
    public class PickedGeometryColored : PickedGeometryBase 
    {
        /// <summary>
        /// Gets or sets colors of this primitive.
        /// </summary>
        public float[] colors { get; set; }

        public override string ToString()
        {
            var positions = this.positions;
            if (positions == null) { positions = new float[0]; }
            var colors = this.colors;
            if (colors == null) { colors = new float[0]; }

            string strPositions = positions.PrintVectors();
            string strColors = colors.PrintVectors();

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

            string result = string.Format("{0}: P: {1} C: {2} ID:{3}/{4} ∈{5}",
                GeometryType, strPositions, strColors, lastVertexID, stageVertexID, From);

            return result;
            //return base.ToString();
        }

    }
}
