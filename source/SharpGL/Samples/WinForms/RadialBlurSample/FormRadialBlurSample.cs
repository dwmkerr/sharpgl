using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpGL;

namespace RadialBlurSample
{
    public partial class FormRadialBlurSample : Form
    {
        public FormRadialBlurSample()
        {
            InitializeComponent();

       }

        //  Variables for the radial blur effect.
        protected float angle = 0;				                // Used To Rotate The Helix
        protected float[,] vertexes = new float[4, 3];		// Holds Float Info For 4 Sets Of Vertices
        protected float[] normal = new float[3];			// An Array To Store The Normal Data
        protected uint BlurTexture = 0;			                // An Unsigned Int To Store The Texture Number

        private void openGLControl1_OpenGLDraw(object sender, RenderEventArgs e)
        {
            OpenGL gl = openGLControl1.OpenGL;

            gl.ClearColor(0.0f, 0.0f, 0.0f, 0.5f);				// Set The Clear Color To Black
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);		// Clear Screen And Depth Buffer
	        gl.LoadIdentity();						// Reset The View
	        RenderToTexture();						// Render To A Texture
	        ProcessHelix();							// Draw Our Helix
	        DrawBlur(25,0.02f);						// Draw The Blur Effect
	        gl.Flush ();							// Flush The GL Rendering Pipeline

            angle += 0.5f;
        }
        
        /// <summary>
        /// Create an empty texture.
        /// </summary>
        /// <returns></returns>
        protected uint EmptyTexture()
        {
	        uint[] txtnumber = new uint[1];						// Texture ID

	        // Create Storage Space For Texture Data (128x128x4)
	        byte[] data = new byte[((128 * 128)* 4 * sizeof(uint))];
    
            //  Get a reference to opengl to make things easier.
            OpenGL gl = openGLControl1.OpenGL;

	        gl.GenTextures(1, txtnumber);					// Create 1 Texture
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, txtnumber[0]);			// Bind The Texture
            gl.TexImage2D(OpenGL.GL_TEXTURE_2D, 0, 4, 128, 128, 0,
                OpenGL.GL_RGBA, OpenGL.GL_UNSIGNED_BYTE, data);			// Build Texture Using Information In data
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_LINEAR);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_LINEAR);
    
            return txtnumber[0];						// Return The Texture ID
        }

        protected void ReduceToUnit(float[] vector)
        {									// To A Unit Normal Vector With A Length Of One.
	        float length;							// Holds Unit Length

	        // Calculates The Length Of The Vector
	        length = (float)Math.Sqrt((vector[0]*vector[0]) + (vector[1]*vector[1]) + (vector[2]*vector[2]));

	        if(length == 0.0f)						// Prevents Divide By 0 Error By Providing
		        length = 1.0f;						// An Acceptable Value For Vectors To Close To 0.

	        vector[0] /= length;						// Dividing Each Element By
	        vector[1] /= length;						// The Length Results In A
	        vector[2] /= length;						// Unit Normal Vector.
        }

        // Calculates Normal For A Quad Using 3 Points
        protected void calcNormal(float[,] v, float[] result)				
        {
	        float[] v1 = new float[3];
            float[] v2 = new float[3];	// Vector 1 (x,y,z) & Vector 2 (x,y,z)
	        const int x = 0;						// Define X Coord
	        const int y = 1;						// Define Y Coord
	        const int z = 2;						// Define Z Coord

	        // Finds The Vector Between 2 Points By Subtracting
	        // The x,y,z Coordinates From One Point To Another.

	        // Calculate The Vector From Point 1 To Point 0
	        v1[x] = v[0, x] - v[1, x];					// Vector 1.x=Vertex[0].x-Vertex[1].x
	        v1[y] = v[0, y] - v[1, y];					// Vector 1.y=Vertex[0].y-Vertex[1].y
	        v1[z] = v[0, z] - v[1, z];					// Vector 1.z=Vertex[0].y-Vertex[1].z
	        // Calculate The Vector From Point 2 To Point 1
	        v2[x] = v[1, x] - v[2, x];					// Vector 2.x=Vertex[0].x-Vertex[1].x
	        v2[y] = v[1, y] - v[2, y];					// Vector 2.y=Vertex[0].y-Vertex[1].y
	        v2[z] = v[1, z] - v[2, z];					// Vector 2.z=Vertex[0].z-Vertex[1].z
	        // Compute The Cross Product To Give Us A Surface Normal
	        result[x] = v1[y]*v2[z] - v1[z]*v2[y];				// Cross Product For Y - Z
	        result[y] = v1[z]*v2[x] - v1[x]*v2[z];				// Cross Product For X - Z
	        result[z] = v1[x]*v2[y] - v1[y]*v2[x];				// Cross Product For X - Y

	        ReduceToUnit(result);						// Normalize The Vectors
        }

        protected void ProcessHelix()							// Draws A Helix
        {
            //  Get a reference to OpenGL.
            OpenGL gl = openGLControl1.OpenGL;

	        float x;							// Helix x Coordinate
	        float y;							// Helix y Coordinate
	        float z;							// Helix z Coordinate
	        float phi;							// Angle
	        float theta;							// Angle
	        float v,u;							// Angles
	        float r;							// Radius Of Twist
	        int twists = 5;							// 5 Twists

	        float[] glfMaterialColor = new float[] {0.4f,0.2f,0.8f,1.0f};		// Set The Material Color
	        float[] specular = new float [] {1.0f,1.0f,1.0f,1.0f};			// Sets Up Specular Lighting

	        gl.LoadIdentity();						// Reset The Modelview Matrix
	        gl.LookAt(0, 5, 50, 0, 0, 0, 0, 1, 0);				// Eye Position (0,5,50) Center Of Scene (0,0,0)
									        // Up On Y Axis.
	        gl.PushMatrix();							// Push The Modelview Matrix

	        gl.Translate(0,0,-50);						// Translate 50 Units Into The Screen
	        gl.Rotate(angle/2.0f,1,0,0);					// Rotate By angle/2 On The X-Axis
	        gl.Rotate(angle/3.0f,0,1,0);					// Rotate By angle/3 On The Y-Axis

            gl.Material(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_AMBIENT_AND_DIFFUSE, glfMaterialColor);
            gl.Material(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_SPECULAR, specular);

            r=1.5f;								// Radius

            gl.Begin(OpenGL.GL_QUADS);						// Begin Drawing Quads
	        for(phi=0; phi <= 360; phi+=20.0f)				// 360 Degrees In Steps Of 20
	        {
		        for(theta=0; theta<=360*twists; theta+=20.0f)		// 360 Degrees * Number Of Twists In Steps Of 20
		        {
			        v=(phi/180.0f*3.142f);				// Calculate Angle Of First Point	(  0 )
			        u=(theta/180.0f*3.142f);			// Calculate Angle Of First Point	(  0 )

			        x= (float)(Math.Cos(u)*(2.0f+Math.Cos(v) ))*r;		// Calculate x Position (1st Point)
			        y= (float)(Math.Sin(u)*(2.0f+Math.Cos(v) ))*r;		// Calculate y Position (1st Point)
			        z= (float)((( u-(2.0f*3.142f)) + Math.Sin(v) ) * r);	// Calculate z Position (1st Point)

			        vertexes[0, 0]=x;				// Set x Value Of First Vertex
			        vertexes[0, 1]=y;				// Set y Value Of First Vertex
			        vertexes[0, 2]=z;				// Set z Value Of First Vertex

			        v=(phi/180.0f*3.142f);				// Calculate Angle Of Second Point	(  0 )
			        u=((theta+20)/180.0f*3.142f);			// Calculate Angle Of Second Point	( 20 )

			        x=(float)(Math.Cos(u)*(2.0f+Math.Cos(v) ))*r;		// Calculate x Position (2nd Point)
                    y = (float)(Math.Sin(u) * (2.0f + Math.Cos(v))) * r;		// Calculate y Position (2nd Point)
                    z = (float)(((u - (2.0f * 3.142f)) + Math.Sin(v)) * r);	// Calculate z Position (2nd Point)

			        vertexes[1, 0]=x;				// Set x Value Of Second Vertex
			        vertexes[1, 1]=y;				// Set y Value Of Second Vertex
			        vertexes[1, 2]=z;				// Set z Value Of Second Vertex

			        v=((phi+20)/180.0f*3.142f);			// Calculate Angle Of Third Point	( 20 )
			        u=((theta+20)/180.0f*3.142f);			// Calculate Angle Of Third Point	( 20 )

                    x = (float)(Math.Cos(u) * (2.0f + Math.Cos(v))) * r;		// Calculate x Position (3rd Point)
                    y = (float)(Math.Sin(u) * (2.0f + Math.Cos(v))) * r;		// Calculate y Position (3rd Point)
                    z = (float)(((u - (2.0f * 3.142f)) + Math.Sin(v)) * r);	// Calculate z Position (3rd Point)

			        vertexes[2, 0]=x;				// Set x Value Of Third Vertex
			        vertexes[2, 1]=y;				// Set y Value Of Third Vertex
			        vertexes[2, 2]=z;				// Set z Value Of Third Vertex

			        v=((phi+20)/180.0f*3.142f);			// Calculate Angle Of Fourth Point	( 20 )
			        u=((theta)/180.0f*3.142f);			// Calculate Angle Of Fourth Point	(  0 )

                    x = (float)(Math.Cos(u) * (2.0f + Math.Cos(v))) * r;		// Calculate x Position (4th Point)
                    y = (float)(Math.Sin(u) * (2.0f + Math.Cos(v))) * r;		// Calculate y Position (4th Point)
                    z = (float)(((u - (2.0f * 3.142f)) + Math.Sin(v)) * r);	// Calculate z Position (4th Point)

			        vertexes[3, 0]=x;				// Set x Value Of Fourth Vertex
			        vertexes[3, 1]=y;				// Set y Value Of Fourth Vertex
			        vertexes[3, 2]=z;				// Set z Value Of Fourth Vertex

			        calcNormal(vertexes,normal);			// Calculate The Quad Normal

			        gl.Normal(normal[0],normal[1],normal[2]);	// Set The Normal

			        // Render The Quad
			        gl.Vertex(vertexes[0, 0],vertexes[0, 1],vertexes[0, 2]);
			        gl.Vertex(vertexes[1, 0],vertexes[1, 1],vertexes[1, 2]);
			        gl.Vertex(vertexes[2, 0],vertexes[2, 1],vertexes[2, 2]);
			        gl.Vertex(vertexes[3, 0],vertexes[3, 1],vertexes[3, 2]);
		        }
	        }
	        gl.End();							// Done Rendering Quads
        	
	        gl.PopMatrix();							// Pop The Matrix

       }

       protected void ViewOrtho()							// Set Up An Ortho View
        {
            //  Get a reference to OpenGL.
            OpenGL gl = openGLControl1.OpenGL;

            gl.MatrixMode(OpenGL.GL_PROJECTION);					// Select Projection
	        gl.PushMatrix();							// Push The Matrix
	        gl.LoadIdentity();						// Reset The Matrix
	        gl.Ortho( 0, Width , Height, 0, -1, 1 );				// Select Ortho Mode (640x480)
            gl.MatrixMode(OpenGL.GL_MODELVIEW);					// Select Modelview Matrix
	        gl.PushMatrix();							// Push The Matrix
	        gl.LoadIdentity();						// Reset The Matrix
        }

        protected void ViewPerspective()							// Set Up A Perspective View
        {
            //  Get a reference to OpenGL.
            OpenGL gl = openGLControl1.OpenGL;

            gl.MatrixMode(OpenGL.GL_PROJECTION);					// Select Projection
	        gl.PopMatrix();							// Pop The Matrix
            gl.MatrixMode(OpenGL.GL_MODELVIEW);					// Select Modelview
	        gl.PopMatrix();							// Pop The Matrix
        }

        public void RenderToTexture()							// Renders To A Texture
        {
            //  Get a reference to OpenGL.
            OpenGL gl = openGLControl1.OpenGL;

	        gl.Viewport(0,0,128,128);					// Set Our Viewport (Match Texture Size)

	        ProcessHelix();							// Render The Helix

            gl.BindTexture(OpenGL.GL_TEXTURE_2D, BlurTexture);			// Bind To The Blur Texture

	        // Copy Our ViewPort To The Blur Texture (From 0,0 To 128,128... No Border)
            gl.CopyTexImage2D(OpenGL.GL_TEXTURE_2D, 0, OpenGL.GL_LUMINANCE, 0, 0, 128, 128, 0);

	        gl.ClearColor(0.0f, 0.0f, 0.5f, 0.5f);				// Set The Clear Color To Medium Blue
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);		// Clear The Screen And Depth Buffer

	        gl.Viewport(0 , 0,Width ,Height);					// Set Viewport (0,0 to 640x480)
        }

        protected void DrawBlur(int times, float inc)					// Draw The Blurred Image
        {
            //  Get a reference to OpenGL.
            OpenGL gl = openGLControl1.OpenGL;

	        float spost = 0.0f;						// Starting Texture Coordinate Offset
	        float alphainc = 0.9f / times;					// Fade Speed For Alpha Blending
	        float alpha = 0.2f;						// Starting Alpha Value

	        // Disable AutoTexture Coordinates
            gl.Disable(OpenGL.GL_TEXTURE_GEN_S);
            gl.Disable(OpenGL.GL_TEXTURE_GEN_T);

            gl.Enable(OpenGL.GL_TEXTURE_2D);					// Enable 2D Texture Mapping
            gl.Disable(OpenGL.GL_DEPTH_TEST);					// Disable Depth Testing
            gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE);				// Set Blending Mode
            gl.Enable(OpenGL.GL_BLEND);						// Enable Blending
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, BlurTexture);			// Bind To The Blur Texture
	        ViewOrtho();							// Switch To An Ortho View

	        alphainc = alpha / times;					// alphainc=0.2f / Times To Render Blur

            gl.Begin(OpenGL.GL_QUADS);			
			// Begin Drawing Quads
            for (int num = 0; num < times; num++)			// Number Of Times To Render Blur
            {
                gl.Color(1.0f, 1.0f, 1.0f, alpha);		// Set The Alpha Value (Starts At 0.2)
                gl.TexCoord(0 + spost, 1 - spost);			// Texture Coordinate	(   0,   1 )
                gl.Vertex(0, 0);				// First Vertex		(   0,   0 )

                gl.TexCoord(0 + spost, 0 + spost);			// Texture Coordinate	(   0,   0 )
                gl.Vertex(0, Height);				// Second Vertex	(   0, 480 )

                gl.TexCoord(1 - spost, 0 + spost);			// Texture Coordinate	(   1,   0 )
                gl.Vertex(Width, Height);				// Third Vertex		( 640, 480 )

                gl.TexCoord(1 - spost, 1 - spost);			// Texture Coordinate	(   1,   1 )
                gl.Vertex(Width, 0);				// Fourth Vertex	( 640,   0 )
                
                spost += inc;					// Gradually Increase spost (Zooming Closer To Texture Center)
                alpha = alpha - alphainc;			// Gradually Decrease alpha (Gradually Fading Image Out)
            }
	      gl.End();							// Done Drawing Quads
            
	        ViewPerspective();						// Switch To A Perspective View

            gl.Enable(OpenGL.GL_DEPTH_TEST);					// Enable Depth Testing
            gl.Disable(OpenGL.GL_TEXTURE_2D);					// Disable 2D Texture Mapping
            gl.Disable(OpenGL.GL_BLEND);						// Disable Blending
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, 0);					// Unbind The Blur Texture
    }

        private void openGLControl1_OpenGLInitialized(object sender, EventArgs e)
        {
            //  Get OpenGL.
            OpenGL gl = openGLControl1.OpenGL;

            // Start Of User Initialization
            angle = 0.0f;											// Set Starting Angle To Zero

            BlurTexture = EmptyTexture();								// Create Our Empty Texture

            gl.Viewport(0, 0, Width, Height);	// Set Up A Viewport
            gl.MatrixMode(OpenGL.GL_PROJECTION);								// Select The Projection Matrix
            gl.LoadIdentity();											// Reset The Projection Matrix
            gl.Perspective(50, (float)Width / (float)Height, 5, 2000); // Set Our Perspective
            gl.MatrixMode(OpenGL.GL_MODELVIEW);									// Select The Modelview Matrix
            gl.LoadIdentity();											// Reset The Modelview Matrix

            gl.Enable(OpenGL.GL_DEPTH_TEST);									// Enable Depth Testing

            float[] global_ambient = new float[] { 0.2f, 0.2f, 0.2f, 1.0f };		// Set Ambient Lighting To Fairly Dark Light (No Color)
            float[] light0pos = new float[] { 0.0f, 5.0f, 10.0f, 1.0f };		// Set The Light Position
            float[] light0ambient = new float[] { 0.2f, 0.2f, 0.2f, 1.0f };		// More Ambient Light
            float[] light0diffuse = new float[] { 0.3f, 0.3f, 0.3f, 1.0f };		// Set The Diffuse Light A Bit Brighter
            float[] light0specular = new float[] { 0.8f, 0.8f, 0.8f, 1.0f };		// Fairly Bright Specular Lighting

            float[] lmodel_ambient = new float[] { 0.2f, 0.2f, 0.2f, 1.0f };			// And More Ambient Light
            gl.LightModel(OpenGL.GL_LIGHT_MODEL_AMBIENT, lmodel_ambient);		// Set The Ambient Light Model

            gl.LightModel(OpenGL.GL_LIGHT_MODEL_AMBIENT, global_ambient);		// Set The Global Ambient Light Model
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_POSITION, light0pos);				// Set The Lights Position
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_AMBIENT, light0ambient);			// Set The Ambient Light
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_DIFFUSE, light0diffuse);			// Set The Diffuse Light
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_SPECULAR, light0specular);			// Set Up Specular Lighting
            gl.Enable(OpenGL.GL_LIGHTING);										// Enable Lighting
            gl.Enable(OpenGL.GL_LIGHT0);										// Enable Light0

            gl.ShadeModel(OpenGL.GL_SMOOTH);									// Select Smooth Shading

            gl.Material(OpenGL.GL_FRONT, OpenGL.GL_SHININESS, 128);
            gl.ClearColor(0.0f, 0.0f, 0.0f, 0.5f);						// Set The Clear Color To Black
       
        }


    }
}
