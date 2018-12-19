using System;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.InteropServices;
using SharpGL.RenderContextProviders;
using SharpGL.Version;

namespace SharpGL
{
	/// <summary>
	/// The OpenGL class wraps Suns OpenGL 3D library.
    /// </summary>
	public partial class OpenGL
	{        
      
        #region The GLU DLL Functions (Exactly the same naming).

        internal const string LIBRARY_GLU = "Glu32.dll";

		[DllImport(LIBRARY_GLU, SetLastError = true)] private static unsafe extern sbyte* gluErrorString(uint errCode);
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static unsafe extern sbyte* gluGetString(int name);
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluOrtho2D(double left, double right, double bottom, double top);
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluPerspective (double fovy, double aspect, double zNear, double zFar);
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluPickMatrix ( double x, double y, double width, double height, int[] viewport);
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluLookAt ( double eyex, double eyey, double eyez, double centerx, double centery, double centerz, double upx, double upy, double upz);
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluProject (double objx, double        objy, double        objz,   double[]  modelMatrix,  double[]  projMatrix,  int[] viewport, double [] winx, double        []winy, double        []winz);
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluUnProject (double winx, double winy, double winz, double[] modelMatrix, double[] projMatrix, int[] viewport, ref double objx, ref double objy, ref double objz);
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluScaleImage (int format, int widthin, int heightin,  int typein,  int  []datain, int       widthout, int       heightout, int      typeout, int[] dataout);
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluBuild1DMipmaps (uint target, uint components, int width, uint format, uint type,  IntPtr data);
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluBuild2DMipmaps (uint target, uint components, int width, int height, uint format, uint type, IntPtr data);
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern IntPtr gluNewQuadric();
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluDeleteQuadric (IntPtr state);
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluQuadricNormals (IntPtr quadObject, uint normals);
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluQuadricTexture (IntPtr quadObject, int textureCoords);
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluQuadricOrientation (IntPtr quadObject, int orientation);
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluQuadricDrawStyle (IntPtr quadObject, uint drawStyle);
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluCylinder(IntPtr           qobj,double            baseRadius, double topRadius, double height,int slices,int stacks);
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluDisk(IntPtr qobj, double innerRadius,double outerRadius,int slices, int loops);
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluPartialDisk(IntPtr qobj,double innerRadius,double outerRadius, int slices, int loops, double startAngle, double sweepAngle);
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluSphere(IntPtr qobj, double radius, int slices, int stacks);
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern IntPtr gluNewTess();
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void  gluDeleteTess(IntPtr tess);
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void  gluTessBeginPolygon(IntPtr tess, IntPtr polygonData);
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void  gluTessBeginContour(IntPtr tess);
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void  gluTessVertex(IntPtr tess,double[] coords, double[] data );
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void  gluTessEndContour(   IntPtr        tess );
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void  gluTessEndPolygon(   IntPtr        tess );
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void  gluTessProperty(     IntPtr        tess,int              which, double            value );
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void  gluTessNormal(       IntPtr        tess, double            x,double            y, double            z );
//		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void  gluTessCallback(IntPtr tess, int which, SharpGL.Delegates.Tesselators.Begin callback);
//		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void  gluTessCallback(IntPtr tess, int which, SharpGL.Delegates.Tesselators.BeginData callback);
//		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void  gluTessCallback(IntPtr tess, int which, SharpGL.Delegates.Tesselators.Combine callback);
//		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void  gluTessCallback(IntPtr tess, int which, SharpGL.Delegates.Tesselators.CombineData callback);
//		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void  gluTessCallback(IntPtr tess, int which, SharpGL.Delegates.Tesselators.EdgeFlag callback);
//		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void  gluTessCallback(IntPtr tess, int which, SharpGL.Delegates.Tesselators.EdgeFlagData callback);
//		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void  gluTessCallback(IntPtr tess, int which, SharpGL.Delegates.Tesselators.End callback);
//		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void  gluTessCallback(IntPtr tess, int which, SharpGL.Delegates.Tesselators.EndData callback);
//		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void  gluTessCallback(IntPtr tess, int which, SharpGL.Delegates.Tesselators.Error callback);
//		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void  gluTessCallback(IntPtr tess, int which, SharpGL.Delegates.Tesselators.ErrorData callback);
//		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void  gluTessCallback(IntPtr tess, int which, SharpGL.Delegates.Tesselators.Vertex callback);
//		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void  gluTessCallback(IntPtr tess, int which, SharpGL.Delegates.Tesselators.VertexData callback);
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void  gluGetTessProperty(  IntPtr        tess,int              which, double            value );
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern IntPtr gluNewNurbsRenderer ();
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluDeleteNurbsRenderer (IntPtr            nobj);
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluBeginSurface (IntPtr            nobj);
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluBeginCurve (IntPtr            nobj);
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluEndCurve (IntPtr            nobj);
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluEndSurface (IntPtr            nobj);
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluBeginTrim (IntPtr            nobj);
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluEndTrim (IntPtr            nobj);
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluPwlCurve (IntPtr            nobj, int               count, float             array, int stride, uint type);
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluNurbsCurve(IntPtr nobj, int nknots, float[] knot, int               stride, float[] ctlarray, int               order, uint type);
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluNurbsSurface(IntPtr nobj, int sknot_count, float[] sknot, int tknot_count, float[]             tknot, int               s_stride, int               t_stride, float[] ctlarray, int sorder, int               torder, uint              type);
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluLoadSamplingMatrices (IntPtr            nobj,  float[] modelMatrix,  float[] projMatrix, int[] viewport);
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluNurbsProperty(IntPtr nobj, int property, float value);
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluGetNurbsProperty (IntPtr            nobj, int              property, float             value );
		[DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void IntPtrCallback(IntPtr            nobj, int              which, IntPtr Callback );

		#endregion

    }
}
