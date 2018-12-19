using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    public static class BeginModeHelper
    {
        /// <summary>
        /// Convert <see cref="BeginMode"/> to <see cref="GeometryTypes"/>.
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static GeometryTypes ToGeometryType(this SharpGL.Enumerations.BeginMode mode)
        {
            GeometryTypes result = GeometryTypes.Point;
            switch (mode)
            {
                case SharpGL.Enumerations.BeginMode.Points:
                    result = GeometryTypes.Point;
                    break;
                case SharpGL.Enumerations.BeginMode.Lines:
                    result = GeometryTypes.Line;
                    break;
                case SharpGL.Enumerations.BeginMode.LineLoop:
                    result = GeometryTypes.Line;
                    break;
                case SharpGL.Enumerations.BeginMode.LineStrip:
                    result = GeometryTypes.Line;
                    break;
                case SharpGL.Enumerations.BeginMode.Triangles:
                    result = GeometryTypes.Triangle;
                    break;
                case SharpGL.Enumerations.BeginMode.TriangleString:
                    result = GeometryTypes.Triangle;
                    break;
                case SharpGL.Enumerations.BeginMode.TriangleFan:
                    result = GeometryTypes.Triangle;
                    break;
                case SharpGL.Enumerations.BeginMode.Quads:
                    result = GeometryTypes.Quad;
                    break;
                case SharpGL.Enumerations.BeginMode.QuadStrip:
                    result = GeometryTypes.Quad;
                    break;
                case SharpGL.Enumerations.BeginMode.Polygon:
                    result = GeometryTypes.Polygon;
                    break;
                default:
                    throw new NotImplementedException();
            }

            return result;
        }
    }
}
