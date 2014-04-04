using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGLBase.Shaders.Parameters
{

    public class SimpleShaderParameters : IMaterialShaderParameters, IMVPNParameters
    {
        public string LightPositionId
        {
            get { return null; }
        }

        public string DiffuseId
        {
            get { return null; }
        }

        public string AmbientId
        {
            get { return "PickingColor"; }
        }

        public string SpecularId
        {
            get { return null; }
        }

        public string ShininessId
        {
            get { return null; }
        }


        public string EmissionId
        {
            get { return null; }
        }

        public string ProjectionMatrixId
        {
            get { return "Projection"; }
        }

        public string ModelviewMatrixId
        {
            get { return "Modelview"; }
        }

        public string NormalMatrixId
        {
            get { return "NormalMatrix"; }
        }

        string IMVPNParameters.ProjectionMatrixId
        {
            get { return "Projection"; }
        }

        string IMVPNParameters.ModelviewMatrixId
        {
            get { return "Modelview"; }
        }

        string IMVPNParameters.NormalMatrixId
        {
            get { return "NormalMatrix"; }
        }
    }
}
