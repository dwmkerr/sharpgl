using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL.SceneGraph;

namespace ExtensionsSample
{
    public static class VertexExtensions
    {
        public static Vertex GetRotatedX(this Vertex me, float angle)
        {
            if (angle == 0.0)
                return new Vertex(me);

            float sinAngle = (float)Math.Sin(Math.PI * angle / 180);
            float cosAngle = (float)Math.Cos(Math.PI * angle / 180);

            return new Vertex(me.X,
                                me.Y * cosAngle - me.Z * sinAngle,
                                me.Y * sinAngle + me.Z * cosAngle);
        }

        public static Vertex GetRotatedY(this Vertex me, float angle)
        {
            if (angle == 0.0)
                return new Vertex(me);

            float sinAngle = (float)Math.Sin(Math.PI * angle / 180);
            float cosAngle = (float)Math.Cos(Math.PI * angle / 180);

            return new Vertex(me.X * cosAngle + me.Z * sinAngle,
                        me.Y,
                        -me.X * sinAngle + me.Z * cosAngle);
        }

        public static Vertex GetRotatedZ(this Vertex me, float angle)
        {
            if (angle == 0.0)
                return new Vertex(me);

            float sinAngle = (float)Math.Sin(Math.PI * angle / 180);
            float cosAngle = (float)Math.Cos(Math.PI * angle / 180);

            return new Vertex(me.X*cosAngle - me.Y*sinAngle,
					me.X*sinAngle + me.Y*cosAngle,
					me.Z);
        }

        public static Vertex GetPackedTo01(this Vertex me)
        {
        	Vertex temp = new Vertex(me);
            temp.Normalize();
            
            temp= (temp * 0.5f) + new Vertex(0.5f, 0.5f, 0.5f);
	
	        return temp;
        }
    }
}
