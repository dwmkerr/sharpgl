using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestVAO.Model
{
    public class ColorVertexesFactory
    {

        public static ColorVertexes Create(int nx, int ny, int nz,float radius, float minValue,float maxValue)
        {


            int points = nx * ny * nz;
            ColorVertexes colorVertexes = new ColorVertexes(points);
            Random random = new Random();
            Random colorRandom = new Random();

            float xmin=0, xmax=0, ymin=0, ymax=0, zmin=0, zmax=0;
            bool  isInit = false;

            unsafe{
              for (long i = 0; i < colorVertexes.Size; i++)
              {
                  float x = minValue+ ((float)random.NextDouble()) *maxValue;
                  float y = minValue+ ((float)random.NextDouble()) * maxValue;
                  float z = minValue+ ((float)random.NextDouble()) * maxValue;
                  if (!isInit)
                  {
                      xmin = x;
                      xmax = x;
                      ymin = y;
                      ymax = y;
                      zmin = z;
                      zmax = z;
                      isInit = true;
                  }
                  if (x < xmin) xmin = x;
                  if (x > xmax) xmax = x;
                  if (y < ymin) ymin = y;
                  if (y > ymax) ymax = y;
                  if (z < zmin) zmin = z;
                  if (z > zmax) zmax = z;

                  Point3D* centers = colorVertexes.Centers;
                  centers[i].x = x;
                  centers[i].y = y;
                  centers[i].z = z;

                  Color* colors = colorVertexes.Colors;
                  colors[i].red = (byte)colorRandom.Next(0, 256);
                  colors[i].green = (byte)colorRandom.Next(0, 256);
                  colors[i].blue = (byte)colorRandom.Next(0, 256);

              }
              Point3D location;
              location.x = xmin;
              location.y = ymin;
              location.z = zmin;
              
              Size3D size;
              size.x = (xmax - xmin);
              size.y = (ymax - ymin);
              size.z = (zmax - zmin);
              Rect3D rect = new Rect3D(location, size);
              colorVertexes.Bounds = rect;
            }
            return colorVertexes;
        }
    }
}
