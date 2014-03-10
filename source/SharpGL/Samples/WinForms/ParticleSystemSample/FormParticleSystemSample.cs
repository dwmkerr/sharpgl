using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SharpGL;
using SharpGL.SceneGraph.Assets;

namespace ParticleSystemSample
{
    public partial class FormParticleSystemSample : Form
    {
        public FormParticleSystemSample()
        {
            InitializeComponent();

            LoadGLTextures();

            OpenGL gl = openGLControl1.OpenGL;

            gl.ShadeModel(OpenGL.GL_SMOOTH);						// Enables Smooth Shading
            gl.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);					// Black Background
            gl.ClearDepth(1.0f);							// Depth Buffer Setup
            gl.Disable(OpenGL.GL_DEPTH_TEST);						// Disables Depth Testing
            gl.Enable(OpenGL.GL_BLEND);							// Enable Blending
            gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE);					// Type Of Blending To Perform
            gl.Hint(OpenGL.GL_PERSPECTIVE_CORRECTION_HINT, OpenGL.GL_NICEST);			// Really Nice Perspective Calculations
            gl.Hint(OpenGL.GL_POINT_SMOOTH_HINT, OpenGL.GL_NICEST);					// Really Nice Point Smoothing
            gl.Enable(OpenGL.GL_TEXTURE_2D);						// Enable Texture Mapping
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, texture[0]);				// Select Our Texture

            Random random = new Random();
            int particleCount = particles.Length;

            for (loop = 0; loop < particleCount; loop++)					// Initialize All The Textures
	        {
                particles[loop] = new Particle();

                //  Particle count.

		        particles[loop].active=true;					// Make All The Particles Active
                particles[loop].life = 1.0f;					// Give All The Particles Full Life
                particles[loop].fade = (float)(random.Next(100)) / 1000.0f + 0.003f;		// Random Fade Speed

                particles[loop].r = colors[loop * (int)((float)12 / (float)particleCount), 0];		// Select Red Rainbow Color
                particles[loop].g = colors[loop * (int)((float)12 / (float)particleCount), 1];		// Select Red Rainbow Color
                particles[loop].b = colors[loop * (int)((float)12 / (float)particleCount), 2];		// Select Red Rainbow Color

                particles[loop].xi = (float)((random.Next(50) - 26.0f) * 10.0f);		// Random Speed On X Axis
                particles[loop].yi = (float)((random.Next(50) - 25.0f) * 10.0f);		// Random Speed On Y Axis
                particles[loop].zi = (float)((random.Next(50) - 25.0f) * 10.0f);		// Random Speed On Z Axis

                particles[loop].xg = 0.0f;						// Set Horizontal Pull To Zero
                particles[loop].yg = 0.0f;					// Set Vertical Pull Downward
                particles[loop].zg = 0.0f;						// Set Pull On Z Axis To Zero

            }
        }

        float slowdown = 1.0f;			// Slow Down Particles
        float zoom = -40.0f;			// Used To Zoom Out

        uint	loop;				// Misc Loop Variable
        Texture particleTexture;
        uint[]	texture = new uint[1];			// Storage For Our Particle Texture

        public class Particle
        {
        	public bool	active;					// Active (Yes/No)
            public float life;					// Particle Life
            public float fade;					// Fade Speed

            public float r;					// Red Value
            public float g;					// Green Value
            public float b;					// Blue Value

            public float x;					// X Position
            public float y;					// Y Position
            public float z;					// Z Position

            public float xi;					// X Direction
            public float yi;					// Y Direction
            public float zi;					// Z Direction

            public float xg;					// X Gravity
            public float yg;					// Y Gravity
            public float zg;					// Z Gravity
        }

        /// <summary>
        /// A Random number generator we use.
        /// </summary>
        Random random = new Random();
	
        Particle[] particles = new Particle[1000];			// Particle Array (Room For Particle Info)

        float[,] colors = new float [,] {
	{1.0f,0.5f,0.5f},{1.0f,0.75f,0.5f},{1.0f,1.0f,0.5f},{0.75f,1.0f,0.5f},
	{0.5f,1.0f,0.5f},{0.5f,1.0f,0.75f},{0.5f,1.0f,1.0f},{0.5f,0.75f,1.0f},
	{0.5f,0.5f,1.0f},{0.75f,0.5f,1.0f},{1.0f,0.5f,1.0f},{1.0f,0.5f,0.75f}
};

        public uint LoadGLTextures()						// Load Bitmaps And Convert To Textures
        {
            OpenGL gl = openGLControl1.OpenGL;

            //  We'll hitch a ride off the scene graph here.
            particleTexture = new Texture();
            particleTexture.Create(gl, "Particle.png");
           /*glTexImage2D(GL_TEXTURE_2D, 0, 3, TextureImage[0]->sizeX, TextureImage[0]->sizeY, 0, GL_RGB, GL_UNSIGNED_BYTE, TextureImage[0]->data);
            */
            texture[0] = particleTexture.TextureName;

            return texture[0];
        }

        private void openGLControl1_OpenGLDraw(object sender, RenderEventArgs e)
        {
            OpenGL gl = openGLControl1.OpenGL;

            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);			// Clear Screen And Depth Buffer
            gl.LoadIdentity();							// Reset The ModelView Matrix

            for (loop=0;loop<particles.Length;loop++)					// Loop Through All The Particles
	        {
                if (particles[loop].active)					// If The Particle Is Active
		        {
                    float x = particles[loop].x;				// Grab Our Particle X Position
                    float y = particles[loop].y;				// Grab Our Particle Y Position
                    float z = particles[loop].z +zoom;				// Particle Z Pos + Zoom

                    // Draw The Particle Using Our RGB Values, Fade The Particle Based On It's Life
                    gl.Color(particles[loop].r, particles[loop].g, particles[loop].b, particles[loop].life);

                    gl.Begin(OpenGL.GL_TRIANGLE_STRIP);				// Build Quad From A Triangle Strip
				    gl.TexCoord(1,1); gl.Vertex(x+0.5f,y+0.5f,z); // Top Right
				    gl.TexCoord(0,1); gl.Vertex(x-0.5f,y+0.5f,z); // Top Left
				    gl.TexCoord(1,0); gl.Vertex(x+0.5f,y-0.5f,z); // Bottom Right
				    gl.TexCoord(0,0); gl.Vertex(x-0.5f,y-0.5f,z); // Bottom Left
                    gl.End();						// Done Building Triangle Strip

                    particles[loop].x += particles[loop].xi / (slowdown * 1000);	// Move On The X Axis By X Speed
                    particles[loop].y += particles[loop].yi / (slowdown * 1000);	// Move On The Y Axis By Y Speed
                    particles[loop].z += particles[loop].zi / (slowdown * 1000);	// Move On The Z Axis By Z Speed

                    particles[loop].xi += particles[loop].xg;			// Take Pull On X Axis Into Account
                    particles[loop].yi += particles[loop].yg;			// Take Pull On Y Axis Into Account
                    particles[loop].zi += particles[loop].zg;			// Take Pull On Z Axis Into Account

                    particles[loop].life -= particles[loop].fade;		// Reduce Particles Life By 'Fade'
                    
                    if (particles[loop].life < 0.0f)					// If Particle Is Burned Out
			        {
                        particles[loop].life = 1.0f;				// Give It New Life
                        particles[loop].fade = (float)(random.Next(100)) / 1000.0f + 0.003f;		// Random Fade Speed

                        particles[loop].x = 0.0f;					// Center On X Axis
                        particles[loop].y = 0.0f;					// Center On Y Axis
                        particles[loop].z = 0.0f;					// Center On Z Axis

                        particles[loop].xi = (float)((random.Next(50) - 26.0f) * 10.0f);		// Random Speed On X Axis
                        particles[loop].yi = (float)((random.Next(50) - 25.0f) * 10.0f);		// Random Speed On Y Axis
                        particles[loop].zi = (float)((random.Next(50) - 25.0f) * 10.0f);		// Random Speed On Y Axis

                        int colour = random.Next(12);
                        particles[loop].r = colors[colour, 0];			// Select Red From Color Table
                        particles[loop].g = colors[colour, 1];			// Select Green From Color Table
                        particles[loop].b = colors[colour, 2];			// Select Blue From Color Table
			        }
                }
            }
        }

        private void openGLControl1_Resize(object sender, EventArgs e)
        {
            OpenGL gl = openGLControl1.OpenGL;

            gl.MatrixMode(OpenGL.GL_PROJECTION);				// Select The Projection Matrix
            gl.LoadIdentity();					// Reset The Projection Matrix

            // Calculate The Aspect Ratio Of The Window
            gl.Perspective(45.0, Width / Height, 0.1, 200.0);

            gl.MatrixMode(OpenGL.GL_MODELVIEW);				// Select The Modelview Matrix
            gl.LoadIdentity();					// Reset The Modelview Matrix

        }

        private void buttonBurst_Click(object sender, EventArgs e)
        {
            Random random = new Random();

            //  Reset the position of everything!
            for(int i=0; i < particles.Length; i++)
            {
                particles[i].x=0.0f;					// Center On X Axis
                particles[i].y=0.0f;					// Center On Y Axis
				particles[i].z=0.0f;					// Center On Z Axis
				particles[i].xi=(float)((random.Next(50))-26.0f)*10.0f;	// Random Speed On X Axis
				particles[i].yi=(float)((random.Next(50))-25.0f)*10.0f;	// Random Speed On Y Axis
                particles[i].zi = (float)((random.Next(50)) - 25.0f) * 10.0f;	// Random Speed On Z Axis
            }
        }

        private void checkBoxGravity_CheckedChanged(object sender, EventArgs e)
        {
            //  set the gravity of everything!
            for (int i = 0; i < particles.Length; i++)
            {
                particles[i].yg = checkBoxGravity.Checked ? -20.0f : 0.0f;					// Center On X Axis
            }
        }


        private void openGLControl1_OpenGLInitialized(object sender, EventArgs e)
        {
            LoadGLTextures();

            OpenGL gl = openGLControl1.OpenGL;

            gl.ShadeModel(OpenGL.GL_SMOOTH);						// Enables Smooth Shading
            gl.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);					// Black Background
            gl.ClearDepth(1.0f);							// Depth Buffer Setup
            gl.Disable(OpenGL.GL_DEPTH_TEST);						// Disables Depth Testing
            gl.Enable(OpenGL.GL_BLEND);							// Enable Blending
            gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE);					// Type Of Blending To Perform
            gl.Hint(OpenGL.GL_PERSPECTIVE_CORRECTION_HINT, OpenGL.GL_NICEST);			// Really Nice Perspective Calculations
            gl.Hint(OpenGL.GL_POINT_SMOOTH_HINT, OpenGL.GL_NICEST);					// Really Nice Point Smoothing
            gl.Enable(OpenGL.GL_TEXTURE_2D);						// Enable Texture Mapping
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, texture[0]);				// Select Our Texture

            Random random = new Random();
            int particleCount = particles.Length;

            for (loop = 0; loop < particleCount; loop++)					// Initialize All The Textures
            {
                particles[loop] = new Particle();

                //  Particle count.

                particles[loop].active = true;					// Make All The Particles Active
                particles[loop].life = 1.0f;					// Give All The Particles Full Life
                particles[loop].fade = (float)(random.Next(100)) / 1000.0f + 0.003f;		// Random Fade Speed

                particles[loop].r = colors[loop * (int)((float)12 / (float)particleCount), 0];		// Select Red Rainbow Color
                particles[loop].g = colors[loop * (int)((float)12 / (float)particleCount), 1];		// Select Red Rainbow Color
                particles[loop].b = colors[loop * (int)((float)12 / (float)particleCount), 2];		// Select Red Rainbow Color

                particles[loop].xi = (float)((random.Next(50) - 26.0f) * 10.0f);		// Random Speed On X Axis
                particles[loop].yi = (float)((random.Next(50) - 25.0f) * 10.0f);		// Random Speed On Y Axis
                particles[loop].zi = (float)((random.Next(50) - 25.0f) * 10.0f);		// Random Speed On Z Axis

                particles[loop].xg = 0.0f;						// Set Horizontal Pull To Zero
                particles[loop].yg = 0.0f;					// Set Vertical Pull Downward
                particles[loop].zg = 0.0f;						// Set Pull On Z Axis To Zero

            }
        }
    }
}
