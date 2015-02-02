using System;
using System.Text;

namespace SharpGL
{
// ReSharper disable InconsistentNaming

    public partial class OpenGL
    {
        #region ARB_vertex_array_bgra

        //  No new tokens or functions.

        #endregion

        #region ARB_draw_elements_base_vertex

        private delegate void glDrawElementsBaseVertex(uint mode, int count, uint type,
            IntPtr indices, int basevertex);

        private delegate void glDrawRangeElementsBaseVertex(uint mode, uint start, uint end,
            int count, uint type, IntPtr indices, int basevertex);

        private delegate void glDrawElementsInstancedBaseVertex(
            uint mode, int count, uint type, IntPtr indices, int primcount, int basevertex);

        private delegate void glMultiDrawElementsBaseVertex(uint mode, int[] count, uint type,
            IntPtr[] indices, int primcount, int[] basevertex);

        /// <summary>
        /// Render primitives from array data with a per-element offset.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render. Symbolic constants GL_POINTS, GL_LINE_STRIP, GL_LINE_LOOP, GL_LINES, GL_TRIANGLE_STRIP, GL_TRIANGLE_FAN, GL_TRIANGLES, GL_LINES_ADJACENCY, GL_LINE_STRIP_ADJACENCY, GL_TRIANGLES_ADJACENCY, GL_TRIANGLE_STRIP_ADJACENCY and GL_PATCHES are accepted.</param>
        /// <param name="count">Specifies the number of elements to be rendered.</param>
        /// <param name="type">Specifies the type of the values in indices. Must be one of GL_UNSIGNED_BYTE, GL_UNSIGNED_SHORT, or GL_UNSIGNED_INT.</param>
        /// <param name="indices">Specifies a pointer to the location where the indices are stored.</param>
        /// <param name="basevertex">Specifies a constant that should be added to each element of indices when chosing elements from the enabled vertex arrays.</param>
        public void DrawElementsBaseVertex(uint mode, int count, uint type,
            IntPtr indices, int basevertex)
        {
            GetDelegateFor<glDrawElementsBaseVertex>()(mode, count, type, indices, basevertex);
        }

        /// <summary>
        /// Render primitives from array data with a per-element offset.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render. Symbolic constants GL_POINTS, GL_LINE_STRIP, GL_LINE_LOOP, GL_LINES, GL_TRIANGLE_STRIP, GL_TRIANGLE_FAN, GL_TRIANGLES, GL_LINES_ADJACENCY, GL_LINE_STRIP_ADJACENCY, GL_TRIANGLES_ADJACENCY, GL_TRIANGLE_STRIP_ADJACENCY and GL_PATCHES are accepted.</param>
        /// <param name="start">Specifies the minimum array index contained in indices.</param>
        /// <param name="end">Specifies the maximum array index contained in indices.</param>
        /// <param name="count">Specifies the number of elements to be rendered.</param>
        /// <param name="type">Specifies the type of the values in indices. Must be one of GL_UNSIGNED_BYTE, GL_UNSIGNED_SHORT, or GL_UNSIGNED_INT.</param>
        /// <param name="indices">Specifies a pointer to the location where the indices are stored.</param>
        /// <param name="basevertex">Specifies a constant that should be added to each element of indices when chosing elements from the enabled vertex arrays.</param>
        public void DrawRangeElementsBaseVertex(uint mode, uint start, uint end,
            int count, uint type, IntPtr indices, int basevertex)
        {
            GetDelegateFor<glDrawRangeElementsBaseVertex>()(mode, start, end, count, type, indices, basevertex);
        }

        /// <summary>
        /// Render multiple instances of a set of primitives from array data with a per-element offset.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render. Symbolic constants GL_POINTS, GL_LINE_STRIP, GL_LINE_LOOP, GL_LINES, GL_TRIANGLE_STRIP, GL_TRIANGLE_FAN, GL_TRIANGLES, GL_LINES_ADJACENCY, GL_LINE_STRIP_ADJACENCY, GL_TRIANGLES_ADJACENCY, GL_TRIANGLE_STRIP_ADJACENCY and GL_PATCHES are accepted.</param>
        /// <param name="count">Specifies the number of elements to be rendered.</param>
        /// <param name="type">Specifies the type of the values in indices. Must be one of GL_UNSIGNED_BYTE, GL_UNSIGNED_SHORT, or GL_UNSIGNED_INT.</param>
        /// <param name="indices">Specifies a pointer to the location where the indices are stored.</param>
        /// <param name="primcount">Specifies the number of instances of the indexed geometry that should be drawn.</param>
        /// <param name="basevertex">Specifies a constant that should be added to each element of indices when chosing elements from the enabled vertex arrays.</param>
        public void DrawElementsInstancedBaseVertex(uint mode, int count,
            uint type, IntPtr indices, int primcount, int basevertex)
        {
            GetDelegateFor<glDrawElementsInstancedBaseVertex>()(mode, count, type, indices, primcount, basevertex);
        }

        /// <summary>
        /// Render multiple sets of primitives by specifying indices of array data elements and an index to apply to each index.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render. Symbolic constants GL_POINTS, GL_LINE_STRIP, GL_LINE_LOOP, GL_LINES, GL_LINE_STRIP_ADJACENCY, GL_LINES_ADJACENCY, GL_TRIANGLE_STRIP, GL_TRIANGLE_FAN, GL_TRIANGLES, GL_TRIANGLE_STRIP_ADJACENCY and GL_TRIANGLES_ADJACENCY are accepted.</param>
        /// <param name="count">Points to an array of the elements counts.</param>
        /// <param name="type">Specifies the type of the values in indices. Must be one of GL_UNSIGNED_BYTE, GL_UNSIGNED_SHORT, or GL_UNSIGNED_INT.</param>
        /// <param name="indices">Specifies a pointer to the location where the indices are stored.</param>
        /// <param name="primcount">Specifies the size of the count array.</param>
        /// <param name="basevertex">Specifies a pointer to the location where the base vertices are stored.</param>
        public void MultiDrawElementsBaseVertex(uint mode, int[] count, uint type,
            IntPtr[] indices, int primcount, int[] basevertex)
        {
            GetDelegateFor<glMultiDrawElementsBaseVertex>()(mode, count, type, indices, primcount, basevertex);
        }

        #endregion

        #region ARB_fragment_coord_conventions

        //  No new tokens or functions.

        #endregion

        #region ARB_provoking_vertex

        private delegate void glProvokingVertex(uint mode);

        public const uint GL_FIRST_VERTEX_CONVENTION = 0x8E4D;
        public const uint GL_LAST_VERTEX_CONVENTION = 0x8E4E;
        public const uint GL_PROVOKING_VERTEX = 0x8E4F;
        public const uint GL_QUADS_FOLLOW_PROVOKING_VERTEX_CONVENTION = 0x8E4;

        /// <summary>
        /// Specifiy the vertex to be used as the source of data for flat shaded varyings.
        /// </summary>
        /// <param name="mode">Specifies the vertex to be used as the source of data for flat shaded varyings.</param>
        public void ProvokingVertex(uint mode)
        {
            GetDelegateFor<glProvokingVertex>()(mode);
        }

        #endregion

        #region ARB_seamless_cube_map

        public const uint GL_TEXTURE_CUBE_MAP_SEAMLESS = 0x884F;

        #endregion

        #region ARB_texture_multisample

        //  TODO

        #endregion
    }

// ReSharper restore InconsistentNaming
}
