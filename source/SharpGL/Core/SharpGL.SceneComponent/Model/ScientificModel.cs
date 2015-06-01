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
    /// Generic model that contains positions, colors and a bouding box.
    /// </summary>
    public class ScientificModel
    {
        /// <summary>
        /// Gets or sets primitive's rendering type.
        /// </summary>
        public BeginMode Mode { get; set; }

        /// <summary>
        /// Gets bouding box.
        /// </summary>
        public IBoundingBox BoundingBox { get; internal set; }

        /// <summary>
        /// Gets vertex's count.
        /// </summary>
        public int VertexCount
        {
            get
            {
                float[] positions = this.Positions;
                if (positions == null) { return 0; }

                return positions.Length / 3;
            }
        }

        /// <summary>
        /// Gets position array.
        /// </summary>
        public float[] Positions { get; protected set; }

        /// <summary>
        /// Gets color array.
        /// <para>Color values range from 0 to 1.</para>
        /// </summary>
        public float[] Colors { get; protected set; }

        /// <summary>
        /// Generic model that contains positions, colors and a bouding box.
        /// </summary>
        /// <param name="vertexCount"></param>
        /// <param name="mode"></param>
        public ScientificModel(int vertexCount, BeginMode mode)
        {
            if (vertexCount <= 0)
                throw new ArgumentException("size can not less equal to zero");

            this.Positions = new float[vertexCount * 3];
            this.Colors = new float[vertexCount * 3];

            this.BoundingBox = new BoundingBox();

            this.Mode = mode;
        }

    }

    ///// <summary>
    ///// Implements <see cref="IScientificModel"/> using unsafe <see cref="Vertex"/>* and <see cref="ByteColor"/>*. 
    ///// </summary>
    //public class ScientificModel : //IScientificModel, 
    //    IDisposable
    //{
    //    internal IntPtr positions;
    //    internal IntPtr colors;
    //    protected bool _disposed;

    //    public ScientificModel(int pointCount, BeginMode mode)
    //    {
    //        if (pointCount <= 0)
    //            throw new ArgumentException("size can not less equal to zero");

    //        unsafe
    //        {
    //            long bytes = sizeof(Vertex) * (pointCount);
    //            if (bytes >= int.MaxValue)
    //                throw new ArgumentException("size exceed");

    //            IntPtr ptrBytes = new IntPtr(bytes);
    //            positions = Marshal.AllocHGlobal(ptrBytes);
    //        }
    //        unsafe
    //        {
    //            long colorBytes = sizeof(ByteColor) * pointCount;
    //            IntPtr ptrColors = new IntPtr(colorBytes);
    //            this.colors = Marshal.AllocHGlobal(ptrColors);
    //        }
    //        this.VertexCount = pointCount;
    //        this.BoundingBox = new BoundingBox();

    //        this.Mode = mode;
    //    }



    //    public int VertexCount { get; protected set; }


    //    public unsafe Vertex* Positions
    //    {
    //        get
    //        {
    //            Vertex* positions = (Vertex*)this.positions;
    //            return positions;
    //        }
    //    }

    //    public unsafe ByteColor* Colors
    //    {
    //        get
    //        {
    //            ByteColor* colors = (ByteColor*)this.colors;
    //            return colors;
    //        }
    //    }

    //    public void Dispose()
    //    {
    //        this.Dispose(true);
    //        GC.SuppressFinalize(this);
    //    }

    //    protected virtual void Dispose(bool disposing)
    //    {
    //        if (!this._disposed)
    //        {
    //            if (this.positions != IntPtr.Zero)
    //            {
    //                Marshal.FreeHGlobal(this.positions);
    //                this.positions = IntPtr.Zero;
    //            }

    //            if (this.colors != IntPtr.Zero)
    //            {
    //                Marshal.FreeHGlobal(this.colors);
    //                this.colors = IntPtr.Zero;
    //            }

    //            this._disposed = true;
    //        }
    //    }

    //    ~ScientificModel()
    //    {
    //        Dispose(false);
    //    }

    //    public BeginMode Mode { get; set; }

    //    //public int PrimitivesCount { get; set; }


    //    //#region IScientificModel 成员

    //    //public virtual void Render(SharpGL.OpenGL gl, RenderMode renderMode)
    //    //{
    //    //    if (this.VertexCount <= 0)
    //    //        return;

    //    //    // render with Vertex Array(not VAO)
    //    //    gl.Enable(OpenGL.GL_POINT_SPRITE_ARB);

    //    //    gl.EnableClientState(OpenGL.GL_VERTEX_ARRAY);
    //    //    gl.EnableClientState(OpenGL.GL_COLOR_ARRAY);

    //    //    gl.VertexPointer(3, OpenGL.GL_FLOAT, 0, this.positions);
    //    //    gl.ColorPointer(3, OpenGL.GL_BYTE, 0, this.colors);

    //    //    gl.DrawArrays((uint)Mode, 0, this.VertexCount);

    //    //    gl.DisableClientState(OpenGL.GL_VERTEX_ARRAY);
    //    //    gl.DisableClientState(OpenGL.GL_COLOR_ARRAY);
    //    //}

    //    ////public virtual void AdjustCamera(SharpGL.OpenGL gl, SceneGraph.Cameras.Camera camera)
    //    ////{
    //    ////}

    //    //public IBoundingBox BoundingBox { get; internal set; }

    //    //#endregion

    //    public IBoundingBox BoundingBox { get; internal set; }

    //}
}
