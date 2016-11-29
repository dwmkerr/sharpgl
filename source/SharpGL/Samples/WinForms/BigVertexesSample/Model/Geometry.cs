using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TestVAO.Model
{
    public unsafe struct Point3D
    {
        public float x;
        public float y;
        public float z;
        
    }

    public unsafe struct Color
    {
        public byte red;
        public byte green;
        public byte blue;
    }

    public unsafe struct Size3D{
        public float x;
        public float y;
        public float z;
    }

    public unsafe struct Rect3D
    {
        public Point3D location;
        public Size3D size3D;

        public Rect3D(Point3D location, Size3D size3D)
        {
            this.location = location;
            this.size3D = size3D;
        }


        public Point3D Location
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
                return location.x;
            }
        }

        public float Y
        {
            get
            {
                return location.y;
            }
        }


        public float Z
        {
            get
            {
                return location.z;
            }
        }


    }



    public class ColorVertexes : IDisposable
    {
        private bool _disposed = false;

        /// <summary>
        /// 中心点数组
        /// </summary>
        private IntPtr _pointHeader = IntPtr.Zero;


        private IntPtr _colorHeader = IntPtr.Zero;


        /// <summary>
        /// 大小
        /// </summary>
        private int _size;

       


        private Rect3D rect;



        public Rect3D Bounds
        {
            get
            {
                return this.rect;
            }
            set
            {
                this.rect = value;
            }
           
        }
       
       
        public ColorVertexes(int size)
        { 
            if(size <=0) 
                throw new ArgumentException("size can not less equal to zero");

            long bytes = Marshal.SizeOf(new Point3D()) * (size);
            if (bytes >= int.MaxValue)
                throw new ArgumentException("size exceed");

            IntPtr ptrBytes = new IntPtr(bytes);
            _pointHeader =  Marshal.AllocHGlobal(ptrBytes);

            unsafe{
              long colorBytes = sizeof(Color) * size;
              IntPtr ptrColors = new IntPtr(colorBytes);
              this._colorHeader = Marshal.AllocHGlobal(ptrColors);
            }
            this._size = size;
        }


        public int Size
        {
            get
            {
                return _size;
            }
        }

       

       
        public unsafe Point3D* Centers
        {
            get
            {
                Point3D* centers = (Point3D*)this._pointHeader;
                return centers;
            }
        }


        public unsafe Color* Colors
        {
            get
            {
                Color* colors = (Color*)this._colorHeader;
                return colors;
            }
        }





        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }



        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
               
               if (this._pointHeader != IntPtr.Zero)
               {
                  Marshal.FreeHGlobal(this._pointHeader);
                  this._pointHeader = IntPtr.Zero;
               }

               if (this._colorHeader != IntPtr.Zero)
               {
                   Marshal.FreeHGlobal(this._colorHeader);
                   this._colorHeader = IntPtr.Zero;
               }

               this._disposed = true;
            }
        }

        ~ColorVertexes()
        {
            Dispose(false);
        }
    }
}
