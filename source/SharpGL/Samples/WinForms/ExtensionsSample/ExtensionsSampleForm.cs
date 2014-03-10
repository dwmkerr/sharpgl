using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpGL.SceneGraph;
using SharpGL;

using SharpGL.SceneGraph.Assets;
using System.Runtime.InteropServices;
using SharpGL.Enumerations;

//  See: http://www.paulsprojects.net/tutorials/simplebump/simplebump.html

namespace ExtensionsSample
{
    /// <summary>
    /// The main form.
    /// </summary>
    public partial class ExtensionsSampleForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionsSampleForm"/> class.
        /// </summary>
        public ExtensionsSampleForm()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// Handles the OpenGLDraw event of the openGLControl1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RenderEventArgs"/> instance containing the event data.</param>
        private void openGLControl1_OpenGLDraw(object sender, RenderEventArgs e)
        {
            //  Get OpenGL.
            var gl = openGLControl1.OpenGL;
       
            //  Clear and load identity.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.LoadIdentity();
            
             // Use gluLookAt to look at torus.
            gl.LookAt(0.0f,10.0f,10.0f,
              0.0f, 0.0f, 0.0f,
              0.0f, 1.0f, 0.0f);

            //  Rotate torus.
            angle += .5f;
            gl.Rotate(angle, 0.0f, 1.0f, 0.0f);

            //  Push all attribute bits.
            gl.PushAttrib(OpenGL.GL_ALL_ATTRIB_BITS);

            //  Get the inverse model matrix
            gl.PushMatrix();
            gl.LoadIdentity();
            gl.Rotate(-angle, 0.0f, 1.0f, 0.0f);
            var inverseModelMatrix = gl.GetModelViewMatrix();
            gl.PopMatrix();

            //  Get the object space light vector.
            Vertex objectLightPosition = inverseModelMatrix * worldLightPosition;

            //  Loop through vertices
            for(int i=0; i<torus.NumVertices; ++i)
            {
                Vertex lightVector = objectLightPosition - torus.Vertices[i].position;

                //Calculate tangent space light vector
                torus.Vertices[i].tangentSpaceLight.X =
                    torus.Vertices[i].sTangent.ScalarProduct(lightVector);
                torus.Vertices[i].tangentSpaceLight.Y =
                    torus.Vertices[i].tTangent.ScalarProduct(lightVector);
                torus.Vertices[i].tangentSpaceLight.Z =
                    torus.Vertices[i].normal.ScalarProduct(lightVector);
            }

            //  Draw bump pass
            if(drawBumps)
            {
                //  Bind normal map to texture unit 0
                normalMap.Bind(gl);
                gl.Enable(OpenGL.GL_TEXTURE_2D);

                //  Bind normalisation cube map to texture unit 1
                
                //  Extensions: We can use the Extension format, such as below. However in this
                //  case we'll get a warning saying that this particular extension is deprecated
                //  in OpenGL 3.0. However...
                gl.ActiveTextureARB(OpenGL.GL_TEXTURE1_ARB);
                gl.BindTexture(OpenGL.GL_TEXTURE_CUBE_MAP_EXT, normalisationCubeMap);
                gl.Enable(OpenGL.GL_TEXTURE_CUBE_MAP_EXT);
                //  Extensions: ...it's deprecated because it's actually core functionality
                //  in OpenGL 3.0.
                gl.ActiveTexture(OpenGL.GL_TEXTURE0_ARB);

                //Set vertex arrays for torus
                var vertexHandle = GCHandle.Alloc(torus.Vertices, GCHandleType.Pinned);
                //  Address of TorusVertex.Position
                gl.VertexPointer(3, OpenGL.GL_FLOAT, Marshal.SizeOf(typeof(TorusVertex)), IntPtr.Add(vertexHandle.AddrOfPinnedObject(), 0));
                gl.EnableClientState(OpenGL.GL_VERTEX_ARRAY);

                //Send texture coords for normal map to unit 0
                //  Address of TorusVertex.S
                gl.TexCoordPointer(2, OpenGL.GL_FLOAT, Marshal.SizeOf(typeof(TorusVertex)), IntPtr.Add(vertexHandle.AddrOfPinnedObject(), 12));
                gl.EnableClientState(OpenGL.GL_TEXTURE_COORD_ARRAY);

                //Send tangent space light vectors for normalisation to unit 1
                gl.ClientActiveTexture(OpenGL.GL_TEXTURE1_ARB);
                //  Address of TorusVertex.TangentSpaceLight
                gl.TexCoordPointer(3, OpenGL.GL_FLOAT, Marshal.SizeOf(typeof(TorusVertex)), IntPtr.Add(vertexHandle.AddrOfPinnedObject(), 56));
                gl.EnableClientState(OpenGL.GL_TEXTURE_COORD_ARRAY);
                gl.ClientActiveTexture(OpenGL.GL_TEXTURE0_ARB);
                vertexHandle.Free();

                //Set up texture environment to do (tex0 dot tex1)*color
                gl.TexEnv(OpenGL.GL_TEXTURE_ENV, OpenGL.GL_TEXTURE_ENV_MODE, OpenGL.GL_COMBINE_ARB);
                gl.TexEnv(OpenGL.GL_TEXTURE_ENV, OpenGL.GL_SOURCE0_RGB_ARB, OpenGL.GL_TEXTURE);
                gl.TexEnv(OpenGL.GL_TEXTURE_ENV, OpenGL.GL_COMBINE_RGB_ARB, OpenGL.GL_REPLACE);

                gl.ActiveTexture(OpenGL.GL_TEXTURE1_ARB);

                gl.TexEnv(OpenGL.GL_TEXTURE_ENV, OpenGL.GL_TEXTURE_ENV_MODE, OpenGL.GL_COMBINE_ARB);
                gl.TexEnv(OpenGL.GL_TEXTURE_ENV, OpenGL.GL_SOURCE0_RGB_ARB, OpenGL.GL_TEXTURE);
                gl.TexEnv(OpenGL.GL_TEXTURE_ENV, OpenGL.GL_COMBINE_RGB_ARB, OpenGL.GL_DOT3_RGB_ARB);
                gl.TexEnv(OpenGL.GL_TEXTURE_ENV, OpenGL.GL_SOURCE1_RGB_ARB, OpenGL.GL_PREVIOUS_ARB);

                gl.ActiveTexture(OpenGL.GL_TEXTURE0_ARB);

                //  Draw torus
                gl.DrawElements(OpenGL.GL_TRIANGLES, (int)torus.NumIndices, torus.Indices);

                //  Disable textures
                gl.Disable(OpenGL.GL_TEXTURE_2D);

                gl.ActiveTexture(OpenGL.GL_TEXTURE1_ARB);
                gl.Disable(OpenGL.GL_TEXTURE_CUBE_MAP);
                gl.ActiveTexture(OpenGL.GL_TEXTURE0_ARB);

                //  Disable vertex arrays
                gl.DisableClientState(OpenGL.GL_VERTEX_ARRAY);

                gl.DisableClientState(OpenGL.GL_TEXTURE_COORD_ARRAY);

                gl.ClientActiveTexture(OpenGL.GL_TEXTURE1_ARB);
                gl.DisableClientState(OpenGL.GL_TEXTURE_COORD_ARRAY);
                gl.ClientActiveTexture(OpenGL.GL_TEXTURE0_ARB);

                //  Return to standard modulate texenv
                gl.TexEnv(OpenGL.GL_TEXTURE_ENV, OpenGL.GL_TEXTURE_ENV_MODE, OpenGL.GL_MODULATE);
            }
            
            //  If we are drawing both passes, enable blending to multiply them together
            if(drawBumps && drawColor)
            {
                //  Enable multiplicative blending
                gl.BlendFunc(OpenGL.GL_DST_COLOR, OpenGL.GL_ZERO);
                gl.Enable(OpenGL.GL_BLEND);
            }
            
            //  Perform a second pass to color the torus
            if(drawColor)
            {
                if(!drawBumps)
                {
                    gl.Light(OpenGL.GL_LIGHT1, OpenGL.GL_POSITION, objectLightPosition);
                    gl.Light(OpenGL.GL_LIGHT1, OpenGL.GL_DIFFUSE, white);
                    gl.Light(OpenGL.GL_LIGHT1, OpenGL.GL_AMBIENT, black);
                    gl.LightModel(OpenGL.GL_LIGHT_MODEL_AMBIENT, black);
                    gl.Enable(OpenGL.GL_LIGHT1);
                    gl.Enable(OpenGL.GL_LIGHTING);

                    gl.Material(OpenGL.GL_FRONT, OpenGL.GL_DIFFUSE, white);
                }

                //  Bind decal texture
                decalImage.Bind(gl);
                gl.Enable(OpenGL.GL_TEXTURE_2D);
                
                //  Set vertex arrays for torus
                var torusVertices = GCHandle.Alloc(torus.Vertices, GCHandleType.Pinned);
                //  Address of TorusVertex.Position
                gl.VertexPointer(3, OpenGL.GL_FLOAT, Marshal.SizeOf(typeof(TorusVertex)), IntPtr.Add(torusVertices.AddrOfPinnedObject(), 0));
                gl.EnableClientState(OpenGL.GL_VERTEX_ARRAY);

                //  Address of TorusVertex.Normal
                gl.NormalPointer(OpenGL.GL_FLOAT, Marshal.SizeOf(typeof(TorusVertex)), IntPtr.Add(torusVertices.AddrOfPinnedObject(), 44 ));
                gl.EnableClientState(OpenGL.GL_NORMAL_ARRAY);

                //  Address of TorusVertex.S
                gl.TexCoordPointer(2, OpenGL.GL_FLOAT, Marshal.SizeOf(typeof(TorusVertex)), IntPtr.Add(torusVertices.AddrOfPinnedObject(), 12));
                gl.EnableClientState(OpenGL.GL_TEXTURE_COORD_ARRAY);
                torusVertices.Free();
                
                //  Draw torus
                gl.DrawElements(OpenGL.GL_TRIANGLES, (int)torus.NumIndices, torus.Indices);

                if(!drawBumps)
                    gl.Disable(OpenGL.GL_LIGHTING);

                //  Disable texture
                gl.Disable(OpenGL.GL_TEXTURE_2D);

                //  Disable vertex arrays
                gl.DisableClientState(OpenGL.GL_VERTEX_ARRAY);
                gl.DisableClientState(OpenGL.GL_NORMAL_ARRAY);
                gl.DisableClientState(OpenGL.GL_TEXTURE_COORD_ARRAY);
            }

            //  Disable blending if it is enabled
            if(drawBumps && drawColor)
                gl.Disable(OpenGL.GL_BLEND);

            gl.PopAttrib();

            gl.Finish();
            
        }
        
        /// <summary>
        /// Handles the OpenGLInitialized event of the openGLControl1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void openGLControl1_OpenGLInitialized(object sender, EventArgs e)
        {
            //  Get OpenGL.
            var gl = openGLControl1.OpenGL;

            //  Load identity modelview.
            gl.MatrixMode(MatrixMode.Modelview);
            gl.LoadIdentity();

            //  Shading states
            gl.ShadeModel(ShadeModel.Smooth);
            gl.ClearColor(1f, 1f, 1f, 0.0f);
            gl.Color(1.0f, 1.0f, 1.0f, 1.0f);
            gl.Hint(HintTarget.PerspectiveCorrection, HintMode.Nicest);
            gl.PolygonMode(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_FILL);
            
            //  Depth states
            gl.ClearDepth(1.0f);
            gl.DepthFunc(DepthFunction.LessThanOrEqual);
            gl.Enable(OpenGL.GL_DEPTH_TEST);

            //  Enable cull face.
            gl.Enable(OpenGL.GL_CULL_FACE);

            //  Load decal texture
            decalImage = new Texture();
            decalImage.Create(gl, "decal.bmp");
            decalImage.Bind(gl);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_LINEAR);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_LINEAR);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_S, OpenGL.GL_REPEAT);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_T, OpenGL.GL_REPEAT);

            //  Load normal map
            normalMap = new Texture();
            normalMap.Create(gl, "Normal map.bmp");
            normalMap.Bind(gl);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_LINEAR);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_LINEAR);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_S, OpenGL.GL_REPEAT);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_T, OpenGL.GL_REPEAT);

            //  Create normalisation cube map
            uint[] textures = new uint[1];
            gl.GenTextures(1, textures);
            normalisationCubeMap = textures[0];
    
            //  Bind and generate the normalisation cube map.
            gl.BindTexture(OpenGL.GL_TEXTURE_CUBE_MAP, normalisationCubeMap);
            GenerateNormalisationCubeMap();

            //  Set the cube map parameters.

            //  Extensions: Note - you can use the old fasioned extension name (as below)...
            gl.TexParameter(OpenGL.GL_TEXTURE_CUBE_MAP_EXT, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_LINEAR);
            gl.TexParameter(OpenGL.GL_TEXTURE_CUBE_MAP_EXT, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_LINEAR);

            //  Extensions: ...or the standard OpenGL name. For example, EXT_Texture_Cube_Map
            //  became standard in OpenGL 1.3 - but we still have the old fasioned extension functions,
            //  constants etc.
            gl.TexParameter(OpenGL.GL_TEXTURE_CUBE_MAP, OpenGL.GL_TEXTURE_WRAP_S, OpenGL.GL_CLAMP_TO_EDGE);
            gl.TexParameter(OpenGL.GL_TEXTURE_CUBE_MAP, OpenGL.GL_TEXTURE_WRAP_T, OpenGL.GL_CLAMP_TO_EDGE);
            gl.TexParameter(OpenGL.GL_TEXTURE_CUBE_MAP, OpenGL.GL_TEXTURE_WRAP_R, OpenGL.GL_CLAMP_TO_EDGE);
        }
        
        /// <summary>
        /// Handles the Resized event of the openGLControl1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void openGLControl1_Resized(object sender, EventArgs e)
        {
            //  Get OpenGL.
            var gl = openGLControl1.OpenGL;

            //  Set the projection matrix.
            gl.MatrixMode(MatrixMode.Projection);
            gl.LoadIdentity();
            gl.Perspective(45, (double)Width / (double)Height, 1.0, 100.0);

            //  Back to modelview.
            gl.MatrixMode(MatrixMode.Modelview);
        }

        
        /// <summary>
        /// Generates the normalisation cube map.
        /// </summary>
        /// <returns></returns>
        private bool GenerateNormalisationCubeMap()
        {
            var gl = openGLControl1.OpenGL;

            //  First we create space to hold the data for a single face.
            //  Each face is 32x32, and we need to store the R, G and B components of the color at each point.
            byte[] data = new byte[32 * 32 * 3];

            //  Some useful variables.
            int size = 32;
            float offset = 0.5f;
            float halfSize = 16.0f;
            Vertex tempVector = new Vertex();
            uint byteCounter = 0;

            //  Positive x.
            for (int j = 0; j < size; j++)
            {
                for (int i = 0; i < size; i++)
                {
                    tempVector.X = (halfSize);
                    tempVector.Y = (-(j + offset - halfSize));
                    tempVector.Z = (-(i + offset - halfSize));
                    tempVector.UnitLength();
                    tempVector = tempVector.GetPackedTo01();
                    data[byteCounter++] = (byte)(tempVector.X * 255f);
                    data[byteCounter++] = (byte)(tempVector.Y * 255f);
                    data[byteCounter++] = (byte)(tempVector.Z * 255f);
                }
            }

            //  Set the texture image.
            gl.TexImage2D(OpenGL.GL_TEXTURE_CUBE_MAP_POSITIVE_X,
                0, OpenGL.GL_RGBA8, 32, 32, 0, OpenGL.GL_RGB, OpenGL.GL_UNSIGNED_BYTE, data);

            //negative x
            byteCounter = 0;

            for (int j = 0; j < size; j++)
            {
                for (int i = 0; i < size; i++)
                {
                    tempVector.X = (-halfSize);
                    tempVector.Y = (-(j + offset - halfSize));
                    tempVector.Z = ((i + offset - halfSize));

                    tempVector.UnitLength();
                    tempVector = tempVector.GetPackedTo01();

                    data[byteCounter++] = (byte)(tempVector.X * 255f);
                    data[byteCounter++] = (byte)(tempVector.Y * 255f);
                    data[byteCounter++] = (byte)(tempVector.Z * 255f);
                }
            }
            gl.TexImage2D(OpenGL.GL_TEXTURE_CUBE_MAP_NEGATIVE_X,
                            0, OpenGL.GL_RGBA8, 32, 32, 0, OpenGL.GL_RGB, OpenGL.GL_UNSIGNED_BYTE, data);

            //positive y
            byteCounter = 0;

            for (int j = 0; j < size; j++)
            {
                for (int i = 0; i < size; i++)
                {
                    tempVector.X = (i + offset - halfSize);
                    tempVector.Y = (halfSize);
                    tempVector.Z = ((j + offset - halfSize));

                    tempVector.UnitLength();
                    tempVector = tempVector.GetPackedTo01();

                    data[byteCounter++] = (byte)(tempVector.X * 255f);
                    data[byteCounter++] = (byte)(tempVector.Y * 255f);
                    data[byteCounter++] = (byte)(tempVector.Z * 255f);
                }
            }
            gl.TexImage2D(OpenGL.GL_TEXTURE_CUBE_MAP_POSITIVE_Y,
                            0, OpenGL.GL_RGBA8, 32, 32, 0, OpenGL.GL_RGB, OpenGL.GL_UNSIGNED_BYTE, data);

            //negative y
            byteCounter = 0;

            for (int j = 0; j < size; j++)
            {
                for (int i = 0; i < size; i++)
                {
                    tempVector.X = (i + offset - halfSize);
                    tempVector.Y = (-halfSize);
                    tempVector.Z = (-(j + offset - halfSize));

                    tempVector.UnitLength();
                    tempVector = tempVector.GetPackedTo01();

                    data[byteCounter++] = (byte)(tempVector.X * 255f);
                    data[byteCounter++] = (byte)(tempVector.Y * 255f);
                    data[byteCounter++] = (byte)(tempVector.Z * 255f);

                }
            }
            gl.TexImage2D(OpenGL.GL_TEXTURE_CUBE_MAP_NEGATIVE_Y,
                            0, OpenGL.GL_RGBA8, 32, 32, 0, OpenGL.GL_RGB, OpenGL.GL_UNSIGNED_BYTE, data);

            //positive z
            byteCounter = 0;

            for (int j = 0; j < size; j++)
            {
                for (int i = 0; i < size; i++)
                {
                    tempVector.X = (i + offset - halfSize);
                    tempVector.Y = (-(j + offset - halfSize));
                    tempVector.Z = (halfSize);

                    tempVector.UnitLength();
                    tempVector = tempVector.GetPackedTo01();

                    data[byteCounter++] = (byte)(tempVector.X * 255f);
                    data[byteCounter++] = (byte)(tempVector.Y * 255f);
                    data[byteCounter++] = (byte)(tempVector.Z * 255f);

                }
            }
            gl.TexImage2D(OpenGL.GL_TEXTURE_CUBE_MAP_POSITIVE_Z,
                            0, OpenGL.GL_RGBA8, 32, 32, 0, OpenGL.GL_RGB, OpenGL.GL_UNSIGNED_BYTE, data);

            //negative z
            byteCounter = 0;

            for (int j = 0; j < size; j++)
            {
                for (int i = 0; i < size; i++)
                {
                    tempVector.X = (-(i + offset - halfSize));
                    tempVector.Y = (-(j + offset - halfSize));
                    tempVector.Z = (-halfSize);

                    tempVector.UnitLength();
                    tempVector = tempVector.GetPackedTo01();

                    data[byteCounter++] = (byte)(tempVector.X * 255f);
                    data[byteCounter++] = (byte)(tempVector.Y * 255f);
                    data[byteCounter++] = (byte)(tempVector.Z * 255f);
                }
            }
            gl.TexImage2D(OpenGL.GL_TEXTURE_CUBE_MAP_NEGATIVE_Z,
                            0, OpenGL.GL_RGBA8, 32, 32, 0, OpenGL.GL_RGB, OpenGL.GL_UNSIGNED_BYTE, data);

            return true;
        }


        /// <summary>
        /// The white color.
        /// </summary>
        private GLColor white = new GLColor(1, 1, 1, 1);

        /// <summary>
        /// The black color.
        /// </summary>
        private GLColor black = new GLColor(0, 0, 0, 0);

        /// <summary>
        /// Our torus.
        /// </summary>
        private Torus torus = new Torus();

        /// <summary>
        /// Normal map
        /// </summary>
        private Texture normalMap = new Texture();

        /// <summary>
        /// Decal texture
        /// </summary>
        private Texture decalImage = new Texture();

        /// <summary>
        /// The normalisation cube map.
        /// </summary>
        private uint normalisationCubeMap;

        /// <summary>
        /// The world light position.
        /// </summary>
        private Vertex worldLightPosition = new Vertex(10f, 10f, 10f);

        /// <summary>
        /// Do we draw bumps?
        /// </summary>
        private bool drawBumps = true;

        /// <summary>
        /// Do we draw color?
        /// </summary>
        private bool drawColor = true;

        /// <summary>
        /// The rotation angle.
        /// </summary>
        private float angle = 0.0f;

        private void checkBoxDrawColor_CheckedChanged(object sender, EventArgs e)
        {
            drawColor = !drawColor;
        }

        private void checkBoxDrawBumps_CheckedChanged(object sender, EventArgs e)
        {
            drawBumps = !drawBumps;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.paulsprojects.net/tutorials/simplebump/simplebump.html");
        }
    }
}