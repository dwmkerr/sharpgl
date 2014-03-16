using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using GlmNet;
using SharpGL;
using SharpGL.Shaders;
using SharpGL.VertexBuffers;

namespace ModernOpenGLSample
{
    public partial class FormModernOpenGLSample : Form
    {
        mat4 projectionMatrix;
        mat4 viewMatrix;
        mat4 modelMatrix;

        VertexBufferArray vertexArray;

        const uint attributeIndexPosition = 0;
        const uint attributeIndexColour = 1;

        public FormModernOpenGLSample()
        {
            InitializeComponent();
        }

        private void openGLControl1_OpenGLInitialized(object sender, EventArgs e)
        {
            var gl = openGLControl1.OpenGL;

            gl.ClearColor(0.4f, 0.6f, 0.9f, 0.0f);

            //  Create the shader program.
            var vertexShaderSource = LoadManifestResourceTextFile("Shader.vert");
            var fragmentShaderSource = LoadManifestResourceTextFile("Shader.frag");
            shaderProgram = new ShaderProgram();
            shaderProgram.Create(gl, vertexShaderSource, fragmentShaderSource);
            shaderProgram.BindAttributeLocation(gl, attributeIndexPosition, "in_Position");
            shaderProgram.BindAttributeLocation(gl, attributeIndexColour, "in_Color");
            shaderProgram.AssertValid(gl);

            var rads = (60.0f / 360.0f) * (float)Math.PI* 2.0f;

            projectionMatrix = glm.perspective(rads, (float)Width / (float)Height, 0.1f, 100.0f);  // Create our perspective projection ma
            viewMatrix = glm.translate(new mat4(1.0f), new vec3(0.0f, 0.0f, -5.0f)); // Create our view matrix which will translate us back 5 units  
            modelMatrix = glm.scale(new mat4(1.0f), new vec3(0.5f));  // Create our model matrix which will halve the size of our model  

            CreateVerticesForSquare(gl);
        }

        private void openGLControl1_OpenGLDraw(object sender, RenderEventArgs args)
        {
            var gl = openGLControl1.OpenGL;

            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT | OpenGL.GL_STENCIL_BUFFER_BIT);

            shaderProgram.Bind(gl);

            shaderProgram.SetUniformMatrix4(gl, "projectionMatrix", projectionMatrix.to_array());
            shaderProgram.SetUniformMatrix4(gl, "viewMatrix", viewMatrix.to_array());
            shaderProgram.SetUniformMatrix4(gl, "modelMatrix", modelMatrix.to_array());

            //  Bind the out vertex array.
            vertexArray.Bind(gl);
            
            gl.DrawArrays(OpenGL.GL_TRIANGLES, 0, 6); // Draw our square  

            //  Unbind our vertex array.
            vertexArray.Unbind(gl);

            //  Unbind the shader.
            shaderProgram.Unbind(gl);
        }
        private string LoadManifestResourceTextFile(string textFileName)
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(string.Format("ModernOpenGLSample.{0}", textFileName)))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private void CreateVerticesForSquare(OpenGL gl)
        {
            float[] vertices = new float[18];
            float[] colors = new float[18]; // Colors for our vertices  
            vertices[0] = -0.5f; vertices[1] = -0.5f; vertices[2] = 0.0f; // Bottom left corner  
            colors[0] = 1.0f; colors[1] = 1.0f; colors[2] = 1.0f; // Bottom left corner  
            vertices[3] = -0.5f; vertices[4] = 0.5f; vertices[5] = 0.0f; // Top left corner  
            colors[3] = 1.0f; colors[4] = 0.0f; colors[5] = 0.0f; // Top left corner  
            vertices[6] = 0.5f; vertices[7] = 0.5f; vertices[8] = 0.0f; // Top Right corner  
            colors[6] = 0.0f; colors[7] = 1.0f; colors[8] = 0.0f; // Top Right corner  
            vertices[9] = 0.5f; vertices[10] = -0.5f; vertices[11] = 0.0f; // Bottom right corner  
            colors[9] = 0.0f; colors[10] = 0.0f; colors[11] = 1.0f; // Bottom right corner  
            vertices[12] = -0.5f; vertices[13] = -0.5f; vertices[14] = 0.0f; // Bottom left corner  
            colors[12] = 1.0f; colors[13] = 1.0f; colors[14] = 1.0f; // Bottom left corner  
            vertices[15] = 0.5f; vertices[16] = 0.5f; vertices[17] = 0.0f; // Top Right corner  
            colors[15] = 0.0f; colors[16] = 1.0f; colors[17] = 0.0f; // Top Right corner  

            //  Create the vertex array object.
            vertexArray = new VertexBufferArray();
            vertexArray.Create(gl);
            vertexArray.Bind(gl);

            //  Create a vertex buffer for the vertex data.
            var vertexDataBuffer = new VertexBuffer();
            vertexDataBuffer.Create(gl);
            vertexDataBuffer.Bind(gl);
            vertexDataBuffer.SetData(gl, 0, vertices, false, 3);

            //  Now do the same for the colour data.
            var colourDataBuffer = new VertexBuffer();
            colourDataBuffer.Create(gl);
            colourDataBuffer.Bind(gl);
            colourDataBuffer.SetData(gl, 1, colors, false, 3);
                        
            //  Unbind the vertex array, we've finished specifying data for it.
            vertexArray.Unbind(gl);
        }

        private ShaderProgram shaderProgram;
    }
}
