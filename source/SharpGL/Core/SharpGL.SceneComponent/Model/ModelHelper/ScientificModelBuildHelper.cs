using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    public static class ScientifiModelBuildHelper
    {
        /// <summary>
        /// Generate random positions and colors for specified model.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="minPosition">minimum position in model's vertices.</param>
        /// <param name="maxPosition">maximum position in model's vertices.</param>
        public static void Build(this ScientificModel model, Vertex minPosition, Vertex maxPosition)
        {
            if (model == null) { return; }

            Random random = new Random();
            Vertex min = new Vertex(), max = new Vertex();
            bool isInit = false;

            for (int i = 0; i < model.VertexCount; i++)
            {
                var x = (float)((maxPosition.X - minPosition.X) * random.NextDouble() + minPosition.X);
                var y = (float)((maxPosition.Y - minPosition.Y) * random.NextDouble() + minPosition.Y);
                var z = (float)((maxPosition.Z - minPosition.Z) * random.NextDouble() + minPosition.Z);
                if (!isInit)
                {
                    min = new Vertex(x, y, z);
                    max = new Vertex(x, y, z);
                    isInit = true;
                }
                if (x < min.X) min.X = x;
                if (x > max.X) max.X = x;
                if (y < min.Y) min.Y = y;
                if (y > max.Y) max.Y = y;
                if (z < min.Z) min.Z = z;
                if (z > max.Z) max.Z = z;

                model.Positions[i * 3 + 0] = x;
                model.Positions[i * 3 + 1] = y;
                model.Positions[i * 3 + 2] = z;

                model.Colors[i * 3 + 0] = (float)random.NextDouble();
                model.Colors[i * 3 + 1] = (float)random.NextDouble();
                model.Colors[i * 3 + 2] = (float)random.NextDouble();
            }

            model.BoundingBox.Set(min.X, min.Y, min.Z, max.X, max.Y, max.Z);
        }
    }
}
