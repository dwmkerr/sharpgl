using GlmNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGLBase.Scene
{
    /// <summary>
    /// This class contains all functionality related to the Normal matrix.
    /// </summary>
    public class Normal
    {
        #region fields
        private mat3 _normalMatrix = mat3.identity();

        #endregion fields

        #region properties
        /// <summary>
        /// The Normal matrix.
        /// </summary>
        public mat3 NormalMatrix
        {
            get { return _normalMatrix; }
            set { _normalMatrix = value; }
        }
        #endregion properties

        /// <summary>
        /// Simplify the creation of the normal matrix by using a modelView.
        /// </summary>
        /// <param name="modelView"></param>
        public void CreateFromModelView(ModelView modelView)
        {
            _normalMatrix = modelView.ModelviewMatrix.to_mat3();
        }

        /// <summary>
        /// Converts NormalMatrix to a float[].
        /// </summary>
        /// <returns>A float[] containing all the values from the initial matrix.</returns>
        public float[] ToArray()
        {
            return _normalMatrix.to_array();
        }
    }
}
