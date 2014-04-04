using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGLBase.Shaders.Parameters
{
    public class PerPixelParameters : IMaterialShaderParameters, IMVPNParameters, ISingleLightParameters
    {
        public string LightPositionId
        {
            get { return "LightPosition"; }
        }

        public string DiffuseId
        {
            get { return "DiffuseMaterial"; }
        }

        public string AmbientId
        {
            get { return "AmbientMaterial"; }
        }

        public string SpecularId
        {
            get { return "SpecularMaterial"; }
        }

        public string ShininessId
        {
            get { return "Shininess"; }
        }


        public string EmissionId
        {
            get { return "Emission"; }
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

        string ISingleLightParameters.LightPositionId
        {
            get { return "LightPosition"; }
        }
    }
}
