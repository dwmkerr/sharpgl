using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;


namespace SharpGL.SceneComponent.Model
{

    public  struct Size3D
    {
        public float x;
        public float y;
        public float z;

        public Size3D(float x, float y, float z)
        {
            this.x = x; this.y = y; this.z = z;
        }

        public static implicit operator Size3D(Vertex size)
        {
            return new Size3D(size.X, size.Y, size.Z);
        }

        public static implicit operator Vertex(Size3D size)
        {
            return new Vertex(size.x, size.y, size.z);
        }
    }

    public  struct Rect3D
    {
        public Vertex location;
        public Size3D size3D;

        public Rect3D(Vertex location, Size3D size3D)
        {
            this.location = location;
            this.size3D = size3D;
        }


        public Vertex Location
        {
            get
            {
                return location;
            }
        }


        public Size3D Size
        {
            get
            {
                return this.size3D;
            }
        }

        public float X
        {
            get
            {
                return location.X;
            }
        }

        public float Y
        {
            get
            {
                return location.Y;
            }
        }


        public float Z
        {
            get
            {
                return location.Z;
            }
        }


    }

    /// <summary>
    /// model space
    /// </summary>
    public struct ModelSpace
    {
        private Vertex location;
        private Size3D size;
        public ModelSpace(Vertex location, float size)
        {
            this.location = location;
            this.size = new Size3D() { x = size, y = size, z = size };
        }

        public Vertex Location
        {
            get
            {
                return location;
            }
        }

        public Size3D Size
        {
            get
            {
                return this.size;
            }
        }
    }

}
