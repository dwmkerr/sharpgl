//  Copyright (c) 2007 - Dave Kerr
//  http://www.dopecode.co.uk
//
//  
//  This file is part of SharpGL.  
//
//  SharpGL is free software; you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation; either version 3 of the License, or
//  (at your option) any later version.
//
//  SharpGL is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see http://www.gnu.org/licenses/.
//
//  If you are using this code for commerical purposes then you MUST
//  purchase a license. See http://www.dopecode.co.uk for details.

using System;
using System.Drawing;

using SharpGL;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;

namespace SceneBuilder
{
	public class BuildablePolygon : Polygon//todo, IMouseInteractable
	{
		public BuildablePolygon()
		{
		}
		public BuildablePolygon(Polygon poly)
		{
			 Attributes = poly.Attributes;
			 currentContext = poly.CurrentContext;
			 drawNormals = poly.DrawNormals;
			 faces = poly.Faces;
			 Name = poly.Name;
			 normals = poly.Normals;
			 Rotate = poly.Rotate;
			 Scale = poly.Scale;
			 TransformOrder = poly.TransformOrder;
			 Translate = poly.Translate;
			 uvs = poly.UVs;
			 vertices = poly.Vertices;
		}

		public override void Render(OpenGL gl, RenderMode renderMode)
		{
			//	Do the ordinary polygon drawing.
			base.Render(gl, renderMode);

			//	Now we do our own.
            PushGeometricTransformation(gl);

			//	Get the viewport.
			int[] viewport = new int[4];
            gl.GetInteger(OpenGL.GL_VIEWPORT, viewport);

			int index = 0;
			
			//	Here we're going to go through every vertex..
			foreach(Vertex vertex in Vertices)
			{
				//	Convert the vertex into a coord.
				Vertex screen = gl.Project(vertex);

				//	Get the OpenGL coord as a GDI coord.
				float x = screen.X;
				float y = viewport[3] - screen.Y;
				
				/*todoif(gl.GDIGraphics != null)
				{
					//	Label the vertex.
					gl.GDIGraphics.DrawString(index.ToString(),	new System.Drawing.Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold),
						Brushes.Red, new PointF(x, y));
				}*/

				index++;
			}

			PopGeometricTransformation(gl);
		}
        /*
		public override void DrawPick(OpenGL gl)
		{
			base.DrawPick(
		}*/

		public virtual Polygon ToPolygon()
		{
			Polygon poly = new Polygon();
			poly.Attributes = Attributes;
			poly.CurrentContext = currentContext;
			poly.DrawNormals = drawNormals;
			poly.Faces = faces;
			poly.Name = Name;
			poly.Normals = normals;
			poly.Rotate = Rotate;
			poly.Scale = Scale;
            poly.TransformOrder = TransformOrder;
            poly.Translate = Translate;
			poly.UVs = uvs;
			poly.Vertices = vertices;

			return poly;
		}
	}
}