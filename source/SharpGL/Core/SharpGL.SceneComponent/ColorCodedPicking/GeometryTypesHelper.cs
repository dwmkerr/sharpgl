using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    public static class GeometryTypesHelper
    {
        /// <summary>
        /// Get vertex count of specified geometry's type.
        /// <para>returns -1 if type is <see cref="Geometry.Polygon"/>.</para>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int GetVertexCount(this GeometryTypes type)
        {
            int result = -1;

            switch (type)
            {
                case GeometryTypes.Point:
                    result = 1;
                    break;
                case GeometryTypes.Line:
                    result = 2;
                    break;
                case GeometryTypes.Triangle:
                    result = 3;
                    break;
                case GeometryTypes.Quad:
                    result = 4;
                    break;
                case GeometryTypes.Polygon:
                    result = -1;
                    break;
                default:
                    throw new NotImplementedException();
            }

            return result;
        }
    }
}
