using System;
using System.ComponentModel;
using System.Globalization;
using System.Collections;
using System.Drawing.Design;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Assets;

namespace SharpGL.SceneGraph
{
	//	This namespace contains classes for use with the .NET design surface,
	//	typeconverters, UI editors etc. Most clients can safely ingore this, 
	//	it's not important 3D code, it just makes editing it easier.
	namespace NETDesignSurface
	{
		//	Designers are used to aid design of controls, components etc.
		namespace Designers
		{
			/// <summary>
			/// This aids the design of the OpenGLCtrl
			/// </summary>
			public class OpenGLCtrlDesigner : System.Windows.Forms.Design.ControlDesigner 
			{
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGLCtrlDesigner"/> class.
        /// </summary>
				public OpenGLCtrlDesigner() {}

				/// <summary>
				/// Remove Control properties that are not supported by the control.
				/// </summary>
				/// <param name="Properties"></param>
				protected override void PostFilterProperties(IDictionary Properties)
				{
					//	Appearance
					Properties.Remove("BackColor");
					Properties.Remove("BackgroundImage");
					Properties.Remove("Font");
					Properties.Remove("ForeColor");
					Properties.Remove("RightToLeft");

					//	Behaviour
					Properties.Remove("AllowDrop");
					Properties.Remove("ContextMenu");

					//	Layout
					Properties.Remove("AutoScroll");
					Properties.Remove("AutoScrollMargin");
					Properties.Remove("AutoScrollMinSize");
				}
			}
		}


		//	Converters are used to change objects of one type into another, at design time
		//	and also programmatically.
		namespace Converters
		{
			/// <summary>
			/// The VertexConverter class allows you to edit vertices in the propties window.
			/// </summary>
			internal class VertexConverter : ExpandableObjectConverter 
			{
				public override bool CanConvertFrom(ITypeDescriptorContext context, Type t) 
				{
					//	We allow conversion from a string.
					if (t == typeof(string)) 
						return true;
			
					return base.CanConvertFrom(context, t);
				}

				public override object ConvertFrom(ITypeDescriptorContext context, 
					CultureInfo info, object value) 
				{
					//	If it's a string, we'll parse it for coords.
					if (value is string) 
					{
						try 
						{
							string s = (string) value;
										
							//	Parse the format (x, y, z).
							int openbracket = s.IndexOf('(');
							int comma = s.IndexOf(',');
							int nextcomma = s.IndexOf(',', comma + 1);
							int closebracket = s.IndexOf(')');
					
							float xValue, yValue, zValue;

							if(comma != -1 && openbracket != -1) 
							{
								//	We have the comma and open bracket, so get x.
								string parsed = s.Substring(openbracket + 1, (comma - (openbracket + 1)));
								parsed.Trim();
								xValue = float.Parse(parsed);
						
								if(comma != -1 && nextcomma != -1)
								{
									parsed = s.Substring(comma + 1, (nextcomma - (comma + 1)));
									parsed.Trim();
									yValue = float.Parse(parsed);
						
									if(nextcomma != -1 && closebracket != -1)
									{
										parsed = s.Substring(nextcomma + 1, (closebracket - (nextcomma + 1)));
										parsed.Trim();
										zValue = float.Parse(parsed);
								
										return new Vertex(xValue, yValue, zValue);
									}
								}
							}
						}
						catch {}
						//	Somehow we couldn't parse it.
						throw new ArgumentException("Can not convert '" + (string)value + 
							"' to type Vertex");
         
					}

					return base.ConvertFrom(context, info, value);
				}
                                 
				public override object ConvertTo(ITypeDescriptorContext context, 	CultureInfo culture, 
					object value, Type destType) 
				{
					if (destType == typeof(string) && value is Vertex) 
					{
						//	We can easily convert a vertex to a string, format (x, y, z).
						Vertex v = (Vertex)value;
				
						return "(" + v.X + ", " + v.Y + ", " + v.Z + ")";
					}

					return base.ConvertTo(context, culture, value, destType);
				}   
			}

			/// <summary>
			/// This converts the Material Collection into something more functional.
			/// </summary>
			internal class MaterialCollectionConverter : System.ComponentModel.CollectionConverter 
			{ 
				public override object ConvertTo(ITypeDescriptorContext context, 
					CultureInfo culture, object value, Type destinationType) 
				{ 
					if (destinationType == typeof(string)) 
					{ 
						if (value is System.Collections.ICollection) 
							return "Materials"; 
					} 
 
					return base.ConvertTo(context, culture, value, destinationType); 
				} 
			}
		}

	
		//	Editors are classes that increase the functionality of the properties window
		//	in editing objects, e.g texture objects have a thumbnail.
		namespace Editors
		{
			/// <summary>
			/// The texture editor makes Textures in the properties window much better,
			/// giving them a little thumbnail.
			/// </summary>
			internal class UITextureEditor : UITypeEditor
			{
				public override System.Boolean GetPaintValueSupported(System.ComponentModel.ITypeDescriptorContext context )
				{
					return true;
				}

				public override void PaintValue(System.Drawing.Design.PaintValueEventArgs e)
				{
					//	Make sure we are dealing with an actual texture.
					if(e.Value is Texture)
					{
						//	Get the texture.
						Texture tex = e.Value as Texture;
                    
                    	if(tex.TextureName != 0)
							e.Graphics.DrawImage(tex.ToBitmap(), e.Bounds);
					}
				}
			}

			/// <summary>
			/// This converts the Quadric Collection into something more functional, and
			/// allows you to add many types of quadrics.
			/// </summary>
			internal class QuadricCollectionEditor : System.ComponentModel.Design.CollectionEditor
			{  
				public QuadricCollectionEditor(Type type) : base(type) {}
		
				//	Return the types that you want to allow the user to add into your collection. 
				protected override Type[] CreateNewItemTypes() 
				{
					return new Type[] {typeof(Quadrics.Cylinder), typeof(Quadrics.Disk), typeof(Quadrics.Sphere)}; 
				}
			}

			/// <summary>
			/// This converts the Camera collection into something more usable (design time wise)
			/// by allowing all the types of camera to be added.
			/// </summary>
			internal class CameraCollectionEditor : System.ComponentModel.Design.CollectionEditor
			{  
				public CameraCollectionEditor(Type type) : base(type) {}
		
				//	Return the types that you want to allow the user to add into your collection. 
				protected override Type[] CreateNewItemTypes() 
				{
					return new Type[] {typeof(Cameras.FrustumCamera), typeof(Cameras.OrthographicCamera), typeof(Cameras.PerspectiveCamera)}; 
				}
			}

			/// <summary>
			/// This converts the evaluator collection into something more usable (design time wise)
			/// by allowing all the types of evaluator to be added.
			/// </summary>
			internal class EvaluatorCollectionEditor : System.ComponentModel.Design.CollectionEditor
			{  
				public EvaluatorCollectionEditor(Type type) : base(type) {}
		
				//	Return the types that you want to allow the user to add into your collection. 
				protected override Type[] CreateNewItemTypes() 
				{
					return new Type[] {typeof(Evaluators.Evaluator1D), typeof(Evaluators.Evaluator2D), typeof(Evaluators.NurbsCurve), typeof(Evaluators.NurbsSurface)}; 
				}
			}
		}		
 	}
}