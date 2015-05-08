using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using SharpGL.Enumerations;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// Implements <see cref="IScientificModel"/> using unsafe <see cref="Vertex"/>* and <see cref="ByteColor"/>*. 
    /// </summary>
    public class PointerScientificModel : IScientificModel, IDisposable
    {
        protected IntPtr positions;
        protected IntPtr colors;
        protected bool _disposed;

        public PointerScientificModel(int pointCount, BeginMode mode)
        {
            if (pointCount <= 0)
                throw new ArgumentException("size can not less equal to zero");
            unsafe
            {
                long bytes = sizeof(Vertex) * (pointCount);
                if (bytes >= int.MaxValue)
                    throw new ArgumentException("size exceed");

                IntPtr ptrBytes = new IntPtr(bytes);
                positions = Marshal.AllocHGlobal(ptrBytes);
            }
            unsafe
            {
                long colorBytes = sizeof(ByteColor) * pointCount;
                IntPtr ptrColors = new IntPtr(colorBytes);
                this.colors = Marshal.AllocHGlobal(ptrColors);
            }
            this.PointCount = pointCount;

            this.mode = mode;
        }

      

        public int PointCount { get; protected set; }


        public unsafe Vertex* Positions
        {
            get
            {
                Vertex* positions = (Vertex*)this.positions;
                return positions;
            }
        }

        public unsafe ByteColor* Colors
        {
            get
            {
                ByteColor* colors = (ByteColor*)this.colors;
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
                if (this.positions != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(this.positions);
                    this.positions = IntPtr.Zero;
                }

                if (this.colors != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(this.colors);
                    this.colors = IntPtr.Zero;
                }

                this._disposed = true;
            }
        }

        ~PointerScientificModel()
        {
            Dispose(false);
        }

        public BeginMode mode { get; set; }

        public int PrimitivesCount { get; set; }


        #region IScientificModel 成员

        public virtual void Render(SharpGL.OpenGL gl, RenderMode renderMode)
        {
            if (this.PointCount <= 0)
                return;

            unsafe
            {
                gl.Enable(OpenGL.GL_POINT_SPRITE_ARB);

                gl.EnableClientState(OpenGL.GL_VERTEX_ARRAY);
                gl.EnableClientState(OpenGL.GL_COLOR_ARRAY);

                gl.VertexPointer(3, OpenGL.GL_FLOAT, 0, this.positions);
                gl.ColorPointer(3, OpenGL.GL_BYTE, 0, this.colors);

                gl.DrawArrays((uint)mode, 0, this.PointCount);

                gl.DisableClientState(OpenGL.GL_VERTEX_ARRAY);
                gl.DisableClientState(OpenGL.GL_COLOR_ARRAY);
            }
        }

        public virtual void AdjustCamera(SharpGL.OpenGL gl, SceneGraph.Cameras.Camera camera)
        {
        }

        #endregion

        #region ITranslation 成员

        public virtual Vertex Translate { get; set; }
        
        #endregion
    }
}
