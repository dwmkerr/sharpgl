using SharpGL.Enumerations;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// Specify a cuboid that marks a model's edges.
    /// </summary>
    public class BoundingBox : IBoundingBox
    {
        /// <summary>
        /// Maximum position of this cuboid.
        /// </summary>
        private Vertex maxPosition;


        /// <summary>
        /// Minimum position of this cuboid.
        /// </summary>
        private Vertex minPosition;


        /// <summary>
        /// Cuboid's color of its lines.
        /// </summary>
        public GLColor BoxColor { get; set; }

        public BoundingBox()
        {
            BoxColor = new GLColor(1, 1, 1, 1);// white color
        }


        #region IBoundingBox 成员

        /// <summary>
        /// Maximum position of this cuboid.
        /// </summary>
        public Vertex MaxPosition
        {
            get { return maxPosition; }
            protected set { maxPosition = value; }
        }

        /// <summary>
        /// Minimum position of this cuboid.
        /// </summary>
        public Vertex MinPosition
        {
            get { return minPosition; }
            protected set { minPosition = value; }
        }

        /// <summary>
        /// Get center position of this cuboid.
        /// </summary>
        /// <param name="x">x position.</param>
        /// <param name="y">y position.</param>
        /// <param name="z">z position.</param>
        public void GetCenter(out float x, out float y, out float z)
        {
            x = (this.MaxPosition.X + this.MinPosition.X) / 2;
            y = (this.MaxPosition.Y + this.MinPosition.Y) / 2;
            z = (this.MaxPosition.Z + this.MinPosition.Z) / 2;
        }

        /// <summary>
        /// Gets the bound dimensions.
        /// </summary>
        /// <param name="x">The x size.</param>
        /// <param name="y">The y size.</param>
        /// <param name="z">The z size.</param>
        public void GetBoundDimensions(out float xSize, out float ySize, out float zSize)
        {
            Vertex diff = this.MaxPosition - this.MinPosition;
            xSize = Math.Abs(diff.X);
            ySize = Math.Abs(diff.Y);
            zSize = Math.Abs(diff.Z);
        }

        /// <summary>
        /// Render to the provided instance of OpenGL.
        /// </summary>
        /// <param name="gl">The OpenGL instance.</param>
        /// <param name="renderMode">The render mode.</param>
        public virtual void Render(OpenGL gl, RenderMode renderMode)
        {
            //  Push attributes, disable lighting.
            gl.PushAttrib(OpenGL.GL_CURRENT_BIT | OpenGL.GL_ENABLE_BIT |
                OpenGL.GL_LINE_BIT | OpenGL.GL_POLYGON_BIT);
            gl.Disable(OpenGL.GL_LIGHTING);
            gl.Disable(OpenGL.GL_TEXTURE_2D);
            gl.LineWidth(1.0f);
            gl.Color(BoxColor);

            //QuadsDraw(gl);

            //gl.Color(1.0f, 0, 0);
            gl.Begin(BeginMode.LineLoop);
            gl.Vertex(MinPosition.X, MinPosition.Y, MinPosition.Z);
            gl.Vertex(MaxPosition.X, MinPosition.Y, MinPosition.Z);
            gl.Vertex(MaxPosition.X, MinPosition.Y, MaxPosition.Z);
            gl.Vertex(MinPosition.X, MinPosition.Y, MaxPosition.Z);
            gl.End();

            //gl.Color(0, 1.0f, 0);
            gl.Begin(BeginMode.LineLoop);
            gl.Vertex(MinPosition.X, MaxPosition.Y, MinPosition.Z);
            gl.Vertex(MaxPosition.X, MaxPosition.Y, MinPosition.Z);
            gl.Vertex(MaxPosition.X, MaxPosition.Y, MaxPosition.Z);
            gl.Vertex(MinPosition.X, MaxPosition.Y, MaxPosition.Z);
            gl.End();

            //gl.Color(0, 0, 1.0f);
            gl.Begin(BeginMode.Lines);
            gl.Vertex(MinPosition.X, MinPosition.Y, MinPosition.Z);
            gl.Vertex(MinPosition.X, MaxPosition.Y, MinPosition.Z);
            gl.Vertex(MaxPosition.X, MinPosition.Y, MinPosition.Z);
            gl.Vertex(MaxPosition.X, MaxPosition.Y, MinPosition.Z);
            gl.Vertex(MaxPosition.X, MinPosition.Y, MaxPosition.Z);
            gl.Vertex(MaxPosition.X, MaxPosition.Y, MaxPosition.Z);
            gl.Vertex(MinPosition.X, MinPosition.Y, MaxPosition.Z);
            gl.Vertex(MinPosition.X, MaxPosition.Y, MaxPosition.Z);
            gl.End();

            gl.PopAttrib();
        }

        /// <summary>
        /// This simulates BoundingVolume's render method.
        /// </summary>
        /// <param name="gl"></param>
        /// <param name="renderMode"></param>
        private void QuadsDraw(OpenGL gl, RenderMode renderMode)
        {
            //  Push attributes, disable lighting.
            gl.PushAttrib(OpenGL.GL_CURRENT_BIT | OpenGL.GL_ENABLE_BIT |
                OpenGL.GL_LINE_BIT | OpenGL.GL_POLYGON_BIT);
            gl.Disable(OpenGL.GL_LIGHTING);
            gl.Disable(OpenGL.GL_TEXTURE_2D);
            gl.LineWidth(1.0f);
            gl.PolygonMode(OpenGL.GL_FRONT_AND_BACK,
                renderMode == RenderMode.HitTest ? (uint)PolygonMode.Filled : (uint)PolygonMode.Lines);

            gl.Begin(BeginMode.Quads);
            gl.Vertex(maxPosition.X, maxPosition.Y, minPosition.Z);//gl.Vertex(hhl);	// Top Right Of The Quad (Top)
            gl.Vertex(minPosition.X, maxPosition.Y, minPosition.Z);//gl.Vertex(lhl);	// Top Left Of The Quad (Top)
            gl.Vertex(minPosition.X, maxPosition.Y, maxPosition.Z);//gl.Vertex(lhh);	// Bottom Left Of The Quad (Top)
            gl.Vertex(maxPosition.X, maxPosition.Y, maxPosition.Z);//gl.Vertex(hhh);	// Bottom Right Of The Quad (Top)
            gl.Vertex(maxPosition.X, minPosition.Y, maxPosition.Z);//gl.Vertex(hlh);	// Top Right Of The Quad (Bottom)
            gl.Vertex(minPosition.X, minPosition.Y, maxPosition.Z);//gl.Vertex(llh);	// Top Left Of The Quad (Bottom)
            gl.Vertex(minPosition.X, minPosition.Y, minPosition.Z);//gl.Vertex(lll);	// Bottom Left Of The Quad (Bottom)
            gl.Vertex(maxPosition.X, minPosition.Y, minPosition.Z);//gl.Vertex(hll);	// Bottom Right Of The Quad (Bottom)
            gl.Vertex(maxPosition.X, maxPosition.Y, maxPosition.Z);//gl.Vertex(hhh);	// Top Right Of The Quad (Front)
            gl.Vertex(minPosition.X, maxPosition.Y, maxPosition.Z);//gl.Vertex(lhh);	// Top Left Of The Quad (Front)
            gl.Vertex(minPosition.X, minPosition.Y, maxPosition.Z);//gl.Vertex(llh);	// Bottom Left Of The Quad (Front)
            gl.Vertex(maxPosition.X, minPosition.Y, maxPosition.Z);//gl.Vertex(hlh);	// Bottom Right Of The Quad (Front)
            gl.Vertex(maxPosition.X, minPosition.Y, minPosition.Z);//gl.Vertex(hll);	// Top Right Of The Quad (Back)
            gl.Vertex(minPosition.X, minPosition.Y, minPosition.Z);//gl.Vertex(lll);	// Top Left Of The Quad (Back)
            gl.Vertex(minPosition.X, maxPosition.Y, minPosition.Z);//gl.Vertex(lhl);	// Bottom Left Of The Quad (Back)
            gl.Vertex(maxPosition.X, maxPosition.Y, minPosition.Z);//gl.Vertex(hhl);	// Bottom Right Of The Quad (Back)
            gl.Vertex(minPosition.X, maxPosition.Y, maxPosition.Z);//gl.Vertex(lhh);	// Top Right Of The Quad (Left)
            gl.Vertex(minPosition.X, maxPosition.Y, minPosition.Z);//gl.Vertex(lhl);	// Top Left Of The Quad (Left)
            gl.Vertex(minPosition.X, minPosition.Y, minPosition.Z);//gl.Vertex(lll);	// Bottom Left Of The Quad (Left)
            gl.Vertex(minPosition.X, minPosition.Y, maxPosition.Z);//gl.Vertex(llh);	// Bottom Right Of The Quad (Left)
            gl.Vertex(maxPosition.X, maxPosition.Y, minPosition.Z);//gl.Vertex(hhl);	// Top Right Of The Quad (Right)
            gl.Vertex(maxPosition.X, maxPosition.Y, maxPosition.Z);//gl.Vertex(hhh);	// Top Left Of The Quad (Right)
            gl.Vertex(maxPosition.X, minPosition.Y, maxPosition.Z);//gl.Vertex(hlh);	// Bottom Left Of The Quad (Right)
            gl.Vertex(maxPosition.X, minPosition.Y, minPosition.Z);//gl.Vertex(hll);	// Bottom Right Of The Quad (Right)
            gl.End();

            gl.PopAttrib();
        }

        public void Set(float minX = 0, float minY = 0, float minZ = 0, float maxX = 0, float maxY = 0, float maxZ = 0)
        {
            this.minPosition.X = minX;
            this.minPosition.Y = minY;
            this.minPosition.Z = minZ;

            this.maxPosition.X = maxX;
            this.maxPosition.Y = maxY;
            this.maxPosition.Z = maxZ;
        }

        #endregion

        /// <summary>
        /// Make sure the bounding box covers specifed vertex.
        /// </summary>
        /// <param name="vertex"></param>
        public void Extend(Vertex vertex)
        {
            if (vertex.X < this.minPosition.X) { this.minPosition.X = vertex.X; }
            if (vertex.Y < this.minPosition.Y) { this.minPosition.Y = vertex.Y; }
            if (vertex.Z < this.minPosition.Z) { this.minPosition.Z = vertex.Z; }

            if (vertex.X > this.maxPosition.X) { this.maxPosition.X = vertex.X; }
            if (vertex.Y > this.maxPosition.Y) { this.maxPosition.Y = vertex.Y; }
            if (vertex.Z > this.maxPosition.Z) { this.maxPosition.Z = vertex.Z; }

        }

    }
}
